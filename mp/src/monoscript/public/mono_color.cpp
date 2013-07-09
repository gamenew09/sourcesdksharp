//============== Copyright Source Mono, All rights reserved. ================//
//
// Purpose: Color binding for P/Invoke
//
//===========================================================================//

#include "cbase.h"
#include "Color.h"

#include "monoscript/imonoscript.h"

// memdbgon must be the last include file in a .cpp file!!!
#include "tier0/memdbgon.h"

DLL_EXPORT Color *MONOFUNC(color_create_class)( int r, int g, int b, int a )
{
	return new Color( r, g, b, a );
}

DLL_EXPORT void MONOFUNC(color_destroy_class)( Color *pObject )
{
	if( pObject == NULL )
		return;

	delete pObject;
	pObject = NULL;
}

DLL_EXPORT void MONOFUNC(color_set_color)( Color *pObject, int r, int g, int b, int a )
{
	if( pObject == NULL )
		return;

	pObject->SetColor( r, g, b, a );
}

DLL_EXPORT int MONOFUNC(color_get_r)( Color *pObject )
{
	if( pObject == NULL )
		return 0;

	return pObject->r();
}

DLL_EXPORT int MONOFUNC(color_get_g)( Color *pObject )
{
	if( pObject == NULL )
		return 0;

	return pObject->g();
}

DLL_EXPORT int MONOFUNC(color_get_b)( Color *pObject )
{
	if( pObject == NULL )
		return 0;

	return pObject->b();
}

DLL_EXPORT int MONOFUNC(color_get_a)( Color *pObject )
{
	if( pObject == NULL )
		return 0;

	return pObject->a();
}