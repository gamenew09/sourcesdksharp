//================ Copyright © Ben Pye, All rights reserved. ================//
//
// Purpose: Contains the implementation of PNG screenshotting
//
//===========================================================================//

#include "cbase.h"
#include "qlimits.h"
#include "filesystem.h"

#include "miniz.h"

#include "ScreenCapture.h"

// memdbgon must be the last include file in a .cpp file!!!
#include "tier0/memdbgon.h"

static bool cl_takepng;

static int cl_pngcompression = MZ_BEST_SPEED;
static ConVar png_compression( "png_compression", "1", 0, "png compression level (higher level smaller file)." );

static int	cl_snapshotnum = 0;
static char cl_snapshotname[MAX_OSPATH];
static char cl_snapshot_subdirname[MAX_OSPATH];

ConVarRef cl_screenshotname( "cl_screenshotname" );

void TakePNG(const char *name, int compression)
{
	cl_takepng = true;
	cl_pngcompression = clamp( compression, 0, 10 ); // max for libpng is 9 not 10

	if ( name != NULL )
	{
		V_strncpy( cl_snapshotname, name, sizeof( cl_snapshotname ) );		
	}
	else
	{
		cl_snapshotname[0] = 0;
		if ( V_strlen( cvar->FindVar("cl_screenshotname")->GetString() ) ) //cl_screenshotname.GetString() returns 0, not a null string
		{
			V_snprintf( cl_snapshotname, sizeof( cl_snapshotname ), "%s", cl_screenshotname.GetString() );		
		}
	}

	cl_snapshot_subdirname[0] = 0;
}

void ReadScreenPixels( int x, int y, int w, int h, void *pBuffer, ImageFormat format )
{
	CMatRenderContextPtr pRenderContext( materials );

	Rect_t rect;
	rect.x = x;
	rect.y = y;
	rect.width = w;
	rect.height = h;

	pRenderContext->ReadPixelsAndStretch( &rect, &rect, (unsigned char*)pBuffer, format, w * ImageLoader::SizeInBytes( format ) );
}

void TakeScreenshotPNG( const char *pFilename, int compression )
{
	int screenWidth, screenHeight;
	g_pMaterialSystem->GetBackBufferDimensions( screenWidth, screenHeight );

	// bitmap bits
	uint8 *pImage = new uint8[ screenWidth * 3 * screenHeight ];

	// Get Bits from the material system
	ReadScreenPixels( 0, 0, screenWidth, screenHeight, pImage, IMAGE_FORMAT_RGB888 );

	compression = clamp( compression, 0, 9 );
	
	int finalSize = 0;
	//we use miniz, seems libpng is actually slower once miniz has taken one
	size_t out_size;
	void *png = tdefl_write_image_to_png_file_in_memory( pImage, screenWidth, screenHeight, 3, &out_size, cl_pngcompression );

	FileHandle_t fh = filesystem->Open( pFilename, "wb" );
	if ( FILESYSTEM_INVALID_HANDLE != fh )
	{
		//filesystem->Write( state.buffer, state.size, fh );
		filesystem->Write( png, out_size, fh );
		finalSize = filesystem->Tell( fh );
		filesystem->Close( fh );
	}

	// Show info to console.
	char orig[ 64 ];
	char final[ 64 ];
	V_strncpy( orig, V_pretifymem( screenWidth * 3 * screenHeight, 2 ), sizeof( orig ) );
	V_strncpy( final, V_pretifymem( finalSize, 2 ), sizeof( final ) );

	// Let's try and have a similar message to that of the jpeg command, we don't need the quality output
	Msg( "Wrote '%s':  %s (%dx%d) compresssed (compression %i) to %s\n",
		pFilename, orig, screenWidth, screenHeight, compression, final );

	//free(state.buffer);
	mz_free( png );

	delete[] pImage;
}

void TakePNGScreenshot()
{
	if (cl_takepng)
	{
		char base[MAX_OSPATH];
		char filename[MAX_OSPATH];

		filesystem->CreateDirHierarchy( "screenshots", "DEFAULT_WRITE_PATH" );

		//Do we want the map name or just snapshot if we're in a menu
		if ( engine->IsInGame() )
		{
			V_FileBase( engine->GetLevelName(), base, sizeof( base ) );
		}
		else
		{
			V_strncpy( base, "Snapshot", sizeof( base ) );
		}

		char extension[MAX_OSPATH];
		V_snprintf( extension, sizeof( extension ), "%s.%s", GetPlatformExt(), "png" );

		// Using a subdir? If so, create it
		if ( cl_snapshot_subdirname[0] )
		{
			V_snprintf( filename, sizeof( filename ), "screenshots/%s/%s", base, cl_snapshot_subdirname );
			filesystem->CreateDirHierarchy( filename, "DEFAULT_WRITE_PATH" );
		}

		if ( cl_snapshotname[0] )
		{
			V_strncpy( base, cl_snapshotname, sizeof( base ) );
			V_snprintf( filename, sizeof( filename ), "screenshots/%s%s", base, extension );

			int iNumber = 0;
			char renamedfile[MAX_OSPATH];

			while ( 1 )
			{
				V_snprintf( renamedfile, sizeof( renamedfile ), "screenshots/%s_%04d%s", base, iNumber++, extension );	
				if( !filesystem->GetFileTime( renamedfile ) )
					break;
			}

			if (iNumber > 0)
			{
				filesystem->RenameFile(filename, renamedfile);
			}

			cl_screenshotname.SetValue( "" );
		}
		else
		{
			while( 1 )
			{
				if ( cl_snapshot_subdirname[0] )
				{
					V_snprintf( filename, sizeof( filename ), "screenshots/%s/%s/%s%04d%s", base, cl_snapshot_subdirname, base, cl_snapshotnum++, extension  );
				}
				else
				{
					V_snprintf( filename, sizeof( filename ), "screenshots/%s%04d%s", base, cl_snapshotnum++, extension  );
				}

				if( !filesystem->GetFileTime( filename ) )
				{
					// woo hoo!  The file doesn't exist already, so use it.
					break;
				}
			}
		}

		//Take the screenshot
		TakeScreenshotPNG(filename, cl_pngcompression);

		cl_takepng = false;
	}
}

CON_COMMAND_F( png, "Take a png screenshot:  png <filename> <compression 0-9>.", FCVAR_CLIENTCMD_CAN_EXECUTE )
{
	if( args.ArgC() >= 2 )
	{
		if ( args.ArgC() == 3 )
		{
			TakePNG( args[ 1 ], V_atoi( args[2] ) );
		}
		else
		{
			TakePNG( args[ 1 ], png_compression.GetInt() );
		}
	}
	else
	{
		TakePNG( NULL, png_compression.GetInt() );
	}
}