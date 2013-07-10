//============== Copyright Source Mono, All rights reserved. ================//
//
// Purpose: Tier0 commandline binding for P/Invoke
//
//===========================================================================//

#include "cbase.h"
#include "tier0/icommandline.h"

#include "monoscript/imonoscript.h"

// memdbgon must be the last include file in a .cpp file!!!
#include "tier0/memdbgon.h"

DLL_EXPORT char *MONOFUNC(commandline_get)()
{
	return (char *)CommandLine()->GetCmdLine();
}

DLL_EXPORT char *MONOFUNC(commandline_check_parm)(char *parm, const char **value)
{
	return (char *)CommandLine()->CheckParm(parm, value);
}

DLL_EXPORT int MONOFUNC(commandline_parm_count)()
{
	return CommandLine()->ParmCount();
}

DLL_EXPORT int MONOFUNC(commandline_find_parm)(char *parm)
{
	return CommandLine()->FindParm(parm);
}

DLL_EXPORT char *MONOFUNC(commandline_get_parm)(int index)
{
	return (char *)CommandLine()->GetParm(index);
}