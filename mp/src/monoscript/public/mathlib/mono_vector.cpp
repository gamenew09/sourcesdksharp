//============== Copyright Source Mono, All rights reserved. ================//
//
// Purpose: Mathlib 3D vector binding for P/Invoke
//
//===========================================================================//

#include "cbase.h"
#include "monoscript/imonoscript.h"

// memdbgon must be the last include file in a .cpp file!!!
#include "tier0/memdbgon.h"

DLL_EXPORT Vector *MONOFUNC(vector_create_class)( float x, float y, float z )
{
	return new Vector( x, y, z );
}

DLL_EXPORT void MONOFUNC(vector_destroy_class)( Vector *pObject )
{
	if( pObject == NULL )
		return;

	delete pObject;
	pObject = NULL;
}

DLL_EXPORT void MONOFUNC(vector_set_vector)( Vector *pObject, float x, float y, float z )
{
	if( pObject == NULL )
		return;

	pObject->x = x;
	pObject->y = y;
	pObject->z = z;
}

DLL_EXPORT float MONOFUNC(vector_get_x)( Vector *pObject )
{
	if( pObject == NULL )
		return 0;

	return pObject->x;
}

DLL_EXPORT float MONOFUNC(vector_get_y)( Vector *pObject )
{
	if( pObject == NULL )
		return 0;

	return pObject->y;
}

DLL_EXPORT float MONOFUNC(vector_get_z)( Vector *pObject )
{
	if( pObject == NULL )
		return 0;

	return pObject->z;
}

DLL_EXPORT void MONOFUNC(vector_set_x)( Vector *pObject, float val )
{
	if( pObject == NULL )
		return;

	pObject->x = val;
}

DLL_EXPORT void MONOFUNC(vector_set_y)( Vector *pObject, float val )
{
	if( pObject == NULL )
		return;

	pObject->y = val;
}

DLL_EXPORT void MONOFUNC(vector_set_z)( Vector *pObject, float val )
{
	if( pObject == NULL )
		return;

	pObject->z = val;
}

DLL_EXPORT float MONOFUNC(vector_length)( Vector *pObject )
{
	if( pObject == NULL )
		return 0;

	return pObject->Length();
}

DLL_EXPORT float MONOFUNC(vector_lengthsqr)( Vector *pObject )
{
	if( pObject == NULL )
		return 0;

	return pObject->LengthSqr();
}

DLL_EXPORT float MONOFUNC(vector_length2d)( Vector *pObject )
{
	if( pObject == NULL )
		return 0;

	return pObject->Length2D();
}

DLL_EXPORT float MONOFUNC(vector_length2dsqr)( Vector *pObject )
{
	if( pObject == NULL )
		return 0;

	return pObject->Length2DSqr();
}

DLL_EXPORT float MONOFUNC(vector_dotproduct)( Vector *pObject, Vector *pOther )
{
	if( pObject == NULL || pOther == NULL )
		return 0;

	return pObject->Dot( *pOther );
}

DLL_EXPORT Vector *MONOFUNC(vector_crosspoduct)( Vector *pObject, Vector *pOther )
{
	if( pObject == NULL || pOther == NULL )
		return 0;

	return new Vector( pObject->Cross( *pOther ) );
}

DLL_EXPORT float MONOFUNC(vector_distto)( Vector *pObject, Vector *pOther )
{
	if( pObject == NULL || pOther == NULL )
		return 0;

	return pObject->DistTo( *pOther );
}

DLL_EXPORT float MONOFUNC(vector_disttosqr)( Vector *pObject, Vector *pOther )
{
	if( pObject == NULL || pOther == NULL )
		return 0;

	return pObject->DistToSqr( *pOther );
}

DLL_EXPORT bool MONOFUNC(vector_withinaabox)( Vector *pObject, Vector *pOtherA, Vector *pOtherB )
{
	if( pObject == NULL || pOtherA == NULL || pOtherB == NULL )
		return false;

	return pObject->WithinAABox( *pOtherA, *pOtherB );
}

DLL_EXPORT Vector *MONOFUNC(vector_normalized)( Vector *pObject )
{
	if( pObject == NULL )
		return NULL;

	return new Vector( pObject->Normalized() );
}

DLL_EXPORT Vector *MONOFUNC(vector_add)( Vector *pObject, Vector *pOther )
{
	if( pObject == NULL || pOther == NULL )
		return NULL;

	return new Vector( *pObject + *pOther );
}

DLL_EXPORT Vector *MONOFUNC(vector_sub)( Vector *pObject, Vector *pOther )
{
	if( pObject == NULL || pOther == NULL )
		return NULL;

	return new Vector( *pObject - *pOther );
}

DLL_EXPORT Vector *MONOFUNC(vector_mul)( Vector *pObject, Vector *pOther )
{
	if( pObject == NULL || pOther == NULL )
		return NULL;

	return new Vector( *pObject * *pOther );
}

DLL_EXPORT Vector *MONOFUNC(vector_div)( Vector *pObject, Vector *pOther )
{
	if( pObject == NULL || pOther == NULL )
		return NULL;

	return new Vector( *pObject / *pOther );
}

DLL_EXPORT Vector *MONOFUNC(vector_mul_float)( Vector *pObject, float other )
{
	if( pObject == NULL )
		return NULL;

	return new Vector( *pObject * other );
}

DLL_EXPORT Vector *MONOFUNC(vector_div_float)( Vector *pObject, float other )
{
	if( pObject == NULL )
		return NULL;

	return new Vector( *pObject / other );
}

DLL_EXPORT bool MONOFUNC(vector_equal)( Vector *pObject, Vector *pOther )
{
	if( pObject == NULL || pOther == NULL )
		return NULL;

	return *pObject == *pOther;
}