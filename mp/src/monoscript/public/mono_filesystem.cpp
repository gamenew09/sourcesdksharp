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

DLL_EXPORT void *MONOFUNC(fs_file_open)( char *filename, char *options, char *pathid )
{
	return filesystem->Open( filename, options, pathid );
}

DLL_EXPORT void MONOFUNC(fs_file_close)( void *file )
{
	if( file != NULL )
		filesystem->Close( file );
}

DLL_EXPORT int MONOFUNC(fs_file_read)( void *pOut, int size, void *file )
{
	if( file != NULL )
		return filesystem->Read( pOut, size, file );

	return 0;
}

DLL_EXPORT void MONOFUNC(fs_file_write)( void *pIn, int size, void *file )
{
	if( file != NULL )
		filesystem->Write( pIn, size, file );
}

DLL_EXPORT void MONOFUNC(fs_file_seek)( void *file, int pos, FileSystemSeek_t seekType )
{
	if( file != NULL )
		filesystem->Seek( file, pos, seekType );
}

DLL_EXPORT unsigned int MONOFUNC(fs_file_tell)( void *file )
{
	if( file != NULL )
		return filesystem->Tell( file );

	return 0;
}

DLL_EXPORT unsigned int MONOFUNC(fs_file_size)( void *file )
{
	if( file != NULL )
		return filesystem->Size( file );

	return 0;
}

DLL_EXPORT void MONOFUNC(fs_file_flush)( void *file )
{
	if( file != NULL )
		filesystem->Flush( file );
}

DLL_EXPORT bool MONOFUNC(fs_file_exists)( char *filename, char *pathid )
{
	return filesystem->FileExists( filename, pathid );
}

DLL_EXPORT bool MONOFUNC(fs_file_writable)( char *filename, char *pathid )
{
	return filesystem->IsFileWritable( filename, pathid );
}

DLL_EXPORT void MONOFUNC(fs_file_delete)( char *filename, char *pathid )
{
	filesystem->RemoveFile( filename, pathid );
}

DLL_EXPORT void MONOFUNC(fs_file_set_buffer_size)( void *file, unsigned int size )
{
	if( file != NULL )
		filesystem->SetBufferSize( file, size );
}

DLL_EXPORT bool MONOFUNC(fs_file_is_ok)( void *file )
{
	return filesystem->IsOk( file );
}