//============== Copyright Source Mono, All rights reserved. ================//
//
// Purpose:
//
//===========================================================================//

#include "tier0/icommandline.h"
#include "filesystem.h"

#include "monoscript.h"

#include <mono/mini/jit.h>
#include <mono/metadata/assembly.h>
#include <mono/metadata/mono-config.h>
#include <mono/metadata/mono-debug.h>

// Mono debugging hacks
#ifdef _WIN32
#define WIN32_LEAN_AND_MEAN
#include <Windows.h>
// Stupid damn Windows.h screws our SendMessage function
#undef SendMessage
#endif

// memdbgon must be the last include file in a .cpp file!!!
#include "tier0/memdbgon.h"

typedef void (__stdcall *MonoMessageFn)( EMonoScriptDomain target, EMonoScriptMsgID msgid, void *msg, int length );
MonoMessageFn g_pMonoMessageFn = NULL;

IFileSystem *filesystem;
CSysModule **g_pFileSystemModule;

MonoDomain *g_pMonoDomain;
MonoAssembly *g_pMonoAssembly;

bool bInitialized = false;

// Despite CMonoScript being a class all our variables are global as there will only be one instance
// This is ensured by only exposing a singleton through EXPOSE_SINGLE_INTERFACE

void CMonoScript::Initialize()
{
	if( !bInitialized )
	{
		bool bDebug = CommandLine()->CheckParm( "-monodebug" ) != NULL;
		if( bDebug )
			Msg( "[CMonoScript] Enabling Mono debugging\n" );
		// Get the filesystem interface to find where our files are
		Sys_LoadInterface( "filesystem_stdio.dll", FILESYSTEM_INTERFACE_VERSION, g_pFileSystemModule, (void **)&filesystem );

		DevMsg( "[CMonoScript] Initializing Mono VM\n" );

		// Get the directories we need
		char libpath[MAX_PATH];
		filesystem->RelativePathToFullPath( "mono/lib", "GAMEBIN", libpath, sizeof(libpath) );
		char etcpath[MAX_PATH];
		filesystem->RelativePathToFullPath( "mono/etc", "GAMEBIN", etcpath, sizeof(etcpath) );
		char bindir[MAX_PATH];
		// Set our assembly path to the top level lib directory - we will change it for other app domains
		filesystem->RelativePathToFullPath( "mono/lib/monoscript", "GAME", bindir, sizeof(bindir) );
		char binpath[MAX_PATH];
		filesystem->RelativePathToFullPath( "mono/lib/monoscript/Source.Host.exe", "GAME", binpath, sizeof(binpath) );

		// Set the mono paths
		DevMsg( "[CMonoScript] Setting mono paths\n" );
		DevMsg( "\tlib: %s\n", libpath);
		DevMsg( "\tetc: %s\n", etcpath);
		DevMsg( "\tbin: %s\n", bindir);
		mono_set_dirs( libpath, etcpath );
		mono_set_assemblies_path( bindir );

		// Enable debugging if requested
		if( bDebug )
		{
			char *monoargs = "--debugger-agent=transport=dt_socket,embedding=1,address=127.0.0.1:10000";
			mono_jit_parse_options( 1, &monoargs );
			mono_debug_init( MONO_DEBUG_FORMAT_MONO );
		}

		// Initialize the mono vm, this will create a .NET 4.5 compatible vm
		g_pMonoDomain = mono_jit_init_version( "monoscript", "v4.0" );

#ifdef _WIN32
		if( bDebug )
		{
			LPTOP_LEVEL_EXCEPTION_FILTER pSehHandler = SetUnhandledExceptionFilter( NULL );
			AddVectoredExceptionHandler( 1, pSehHandler );
			SetUnhandledExceptionFilter( pSehHandler );
		}
#endif

		// Load our base assembly - client server is one at this point
		DevMsg( "[CMonoScript] Loading mono assembly from %s\n", binpath );
		g_pMonoAssembly = mono_domain_assembly_open( g_pMonoDomain, binpath );
		if( !g_pMonoAssembly )
		{
			// If we fail exit, this is now a fatal error
			Error( "[CMonoScript] Failed to load mono assembly from %s\n", binpath );
			return;
		}

		// Run the assembly with the correct argv
		char* argvv[1] = { "Source.Script" };
		mono_jit_exec( g_pMonoDomain, g_pMonoAssembly, 1, argvv );

		// The main assembly should give us our callback
		if( g_pMonoMessageFn == NULL )
		{
			Error( "[CMonoScript] Did not recieve Mono message function\n" );
			return;
		}

		bInitialized = true;
	}
}

void CMonoScript::SendMessage( EMonoScriptDomain target, EMonoScriptMsgID msgid, void *msg, int length )
{
	if( g_pMonoMessageFn != NULL )
	{
		g_pMonoMessageFn( target, msgid, msg, length );
	}
}

DLL_EXPORT void MONOFUNC(set_mono_message_fn)( MonoMessageFn msgfn )
{
	DevMsg( "[CMonoScript] Setting Mono message function\n" );
	g_pMonoMessageFn = msgfn;
}