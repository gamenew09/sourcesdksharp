//============== Copyright Source Mono, All rights reserved. ================//
//
// Purpose: Tier0 dbg binding for P/Invoke
//
//===========================================================================//

#include "cbase.h"
#include "tier0/dbg.h"

#include "monoscript/imonoscript.h"

// memdbgon must be the last include file in a .cpp file!!!
#include "tier0/memdbgon.h"

DLL_EXPORT void MONOFUNC(dbg_msg)( char *fmt )
{
	Msg( fmt );
}

DLL_EXPORT void MONOFUNC(dbg_warning)( char *fmt )
{
	Warning( fmt );
}

DLL_EXPORT void MONOFUNC(dbg_error)( char *fmt )
{
	Error( fmt );
}

DLL_EXPORT void MONOFUNC(dbg_devmsg)( char *fmt )
{
	DevMsg( fmt );
}

DLL_EXPORT void MONOFUNC(dbg_devwarning)( char *fmt )
{
	DevWarning( fmt );
}

DLL_EXPORT void MONOFUNC(dbg_colormsg)( Color *pColor, char *fmt )
{
	if( pColor == NULL )
		return;

	ConColorMsg( *pColor, fmt );
}