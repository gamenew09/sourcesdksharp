//============== Copyright Source Mono, All rights reserved. ================//
//
// Purpose: 
//
// $NoKeywords: $
//
//===========================================================================//

#ifndef MONOSCRIPT_H
#define MONOSCRIPT_H

#ifdef _WIN32
#pragma once
#endif

#include "tier0/dbg.h"
#include "tier1/interface.h"
#include "monoscript/imonoscript.h"

class CMonoScript : public IMonoScript
{
public:
	void	Initialize();
	void	SendMessage( EMonoScriptDomain target, EMonoScriptMsgID msgid, void* buffer, int length );
};

EXPOSE_SINGLE_INTERFACE( CMonoScript, IMonoScript, MONOSCRIPT_INTERFACE_VERSION );

#endif // MONOSCRIPT_H