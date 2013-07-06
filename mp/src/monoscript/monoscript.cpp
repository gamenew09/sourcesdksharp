//============== Copyright Source Mono, All rights reserved. ================//
//
// Purpose: 
//
// $NoKeywords: $
//
//===========================================================================//

#include "monoscript.h"

CMonoScript::CMonoScript()
{
	Msg( "Do Stuff!\n" );
}

void CMonoScript::SendMessage( int id, void *msg, int length )
{
	Msg( "Do More Stuff!\n" );
}