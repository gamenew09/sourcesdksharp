//============== Copyright Source Mono, All rights reserved. ================//
//
// Purpose: 
//
// $NoKeywords: $
//
//===========================================================================//

#ifndef IMONOSCRIPT_H
#define IMONOSCRIPT_H

#ifdef _WIN32
#pragma once
#endif

#include "tier1/interface.h"

#ifdef MONOSCRIPT_DLL_EXPORT
// Interfaces that are provided in the monoscript dll should be here if they are normally avaliable in game
// This allows us to share code between monoscript and game, we also need the names the same
#include "filesystem.h"
extern IFileSystem *filesystem;
#endif

#define TOKENPASTE(x, y) x ## y
#define TOKENPASTE2(x, y) TOKENPASTE(x, y)
#define MONOFUNC(x) TOKENPASTE2(DLLNAME,_##x)

// Keep these enums synced with those found on the managed side
enum EMonoScriptDomain
{
	SCRIPTDOMAIN_SERVER = 0,
	SCRIPTDOMAIN_CLIENT = 1,
	SCRIPTDOMAIN_MENU = 2,
};

enum EMonoScriptMsgID
{
	SCRIPTMSGID_INITIALIZE = 0,
};

abstract_class IMonoScript
{
public:
	virtual void		Initialize() = 0;
	virtual void		SendMessage( EMonoScriptDomain target, EMonoScriptMsgID msgid, void* buffer, int length ) = 0;
};

#define MONOSCRIPT_INTERFACE_VERSION "MonoScript001"

#endif // IMONOSCRIPT_H