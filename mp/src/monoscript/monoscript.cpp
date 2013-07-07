//============== Copyright Source Mono, All rights reserved. ================//
//
// Purpose: 
//
// $NoKeywords: $
//
//===========================================================================//

#include "monoscript.h"

#include <mono/mini/jit.h>
#include <mono/metadata/assembly.h>
#include <mono/metadata/mono-config.h>

#include "filesystem.h"

// memdbgon must be the last include file in a .cpp file!!!
#include "tier0/memdbgon.h"

typedef void (__stdcall *MonoMessageFn)( EMonoScriptDomain target, EMonoScriptMsgID msgid, void *msg, int length );
MonoMessageFn g_pMonoMessageFn = NULL;

IFileSystem *g_pFileSystem;
CSysModule **g_pFileSystemModule;

MonoDomain *g_pMonoDomain;
MonoAssembly *g_pMonoAssembly;

bool bInitialized = false;

void CMonoScript::Initialize()
{
	if( !bInitialized )
	{
		// Get the filesystem interface to find where our files are
		Sys_LoadInterface( "filesystem_stdio.dll", FILESYSTEM_INTERFACE_VERSION, g_pFileSystemModule, (void **)&g_pFileSystem );

		DevMsg( "[CMonoScript] Initializing Mono VM\n" );

		// Get the directories we need
		char libpath[256];
		g_pFileSystem->RelativePathToFullPath( "bin/mono/lib", "MOD", libpath, sizeof(libpath) );
		char etcpath[256];
		g_pFileSystem->RelativePathToFullPath( "bin/mono/etc", "MOD", etcpath, sizeof(etcpath) );
		char bindir[256];
		// Set our assembly path to the top level lib directory - we will change it for other app domains
		g_pFileSystem->RelativePathToFullPath( "mono/lib", "MOD", bindir, sizeof(bindir) );
		char binpath[256];
		Q_strcpy( binpath, bindir );
		Q_strcat( binpath, "/Source.exe", 255 );

		// Set the mono paths
		DevMsg( "[CMonoScript] lib: %s etc: %s\n", libpath, etcpath );
		mono_set_dirs( libpath, etcpath );
		mono_set_assemblies_path( bindir );

		// Initialize the mono vm, this will create a .NET 4.5 compatible vm
		g_pMonoDomain = mono_jit_init_version( "sourcemono", "v4.0" );

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
		char* argvv[1] = { "Source" };
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

DLL_EXPORT void MONOFUNC(msg)( const char *string )
{
	Msg( string );
}

DLL_EXPORT void MONOFUNC(devmsg)( const char *string )
{
	DevMsg( string );
}