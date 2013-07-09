//============== Copyright Source Mono, All rights reserved. ================//
//
// Purpose: Partial filesystem binding for P/Invoke
//
//===========================================================================//

#include "cbase.h"
#include "filesystem.h"

#include "monoscript/imonoscript.h"

// memdbgon must be the last include file in a .cpp file!!!
#include "tier0/memdbgon.h"

DLL_EXPORT void MONOFUNC(fs_relative_to_full_path)( char *path, char *pathid, char *out )
{
	filesystem->RelativePathToFullPath( path, pathid, out, 260 ); // 260 == MAX_PATH though that may change, C# won't
}

DLL_EXPORT void MONOFUNC(fs_add_search_path)( char *path, char *pathID )
{
	filesystem->AddSearchPath( path, pathID );
}

DLL_EXPORT void MONOFUNC(fs_remove_search_path)( char *path, char *pathID )
{
	filesystem->RemoveSearchPath( path, pathID );
}