//============== Copyright Source Mono, All rights reserved. ================//
//
// Purpose: Mathlib 3D qangle binding for P/Invoke
//
//===========================================================================//

#include "cbase.h"
#include "monoscript/imonoscript.h"

// memdbgon must be the last include file in a .cpp file!!!
#include "tier0/memdbgon.h"

DLL_EXPORT QAngle *MONOFUNC(qangle_create_class)( float x, float y, float z )
{
	return new QAngle( x, y, z );
}

DLL_EXPORT void MONOFUNC(qangle_destroy_class)( QAngle *pObject )
{
	if( pObject == NULL )
		return;

	delete pObject;
	pObject = NULL;
}

DLL_EXPORT void MONOFUNC(qangle_set_qangle)( QAngle *pObject, float x, float y, float z )
{
	if( pObject == NULL )
		return;

	pObject->x = x;
	pObject->y = y;
	pObject->z = z;
}

DLL_EXPORT float MONOFUNC(qangle_get_x)( QAngle *pObject )
{
	if( pObject == NULL )
		return 0;

	return pObject->x;
}

DLL_EXPORT float MONOFUNC(qangle_get_y)( QAngle *pObject )
{
	if( pObject == NULL )
		return 0;

	return pObject->y;
}

DLL_EXPORT float MONOFUNC(qangle_get_z)( QAngle *pObject )
{
	if( pObject == NULL )
		return 0;

	return pObject->z;
}

DLL_EXPORT float MONOFUNC(qangle_length)( QAngle *pObject )
{
	if( pObject == NULL )
		return 0;

	return pObject->Length();
}

DLL_EXPORT float MONOFUNC(qangle_lengthsqr)( QAngle *pObject )
{
	if( pObject == NULL )
		return 0;

	return pObject->LengthSqr();
}

DLL_EXPORT QAngle *MONOFUNC(qangle_add)( QAngle *pObject, QAngle *pOther )
{
	if( pObject == NULL || pOther == NULL )
		return NULL;

	return new QAngle( *pObject + *pOther );
}

DLL_EXPORT QAngle *MONOFUNC(qangle_sub)( QAngle *pObject, QAngle *pOther )
{
	if( pObject == NULL || pOther == NULL )
		return NULL;

	return new QAngle( *pObject - *pOther );
}

DLL_EXPORT QAngle *MONOFUNC(qangle_mul_float)( QAngle *pObject, float other )
{
	if( pObject == NULL )
		return NULL;

	return new QAngle( *pObject * other );
}

DLL_EXPORT QAngle *MONOFUNC(qangle_div_float)( QAngle *pObject, float other )
{
	if( pObject == NULL )
		return NULL;

	return new QAngle( *pObject / other );
}

DLL_EXPORT bool MONOFUNC(qangle_equal)( QAngle *pObject, QAngle *pOther )
{
	if( pObject == NULL || pOther == NULL )
		return NULL;

	return *pObject == *pOther;
}