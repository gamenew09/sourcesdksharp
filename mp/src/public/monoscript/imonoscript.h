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

abstract_class IMonoScript
{
public:
	virtual void		SendMessage(int id, void* buffer, int length) = 0;
};

#define MONOSCRIPT_INTERFACE_VERSION "MonoScript001"

#endif // IMONOSCRIPT_H