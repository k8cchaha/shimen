// OCXDLL.h : main header file for the OCXDLL DLL
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"		// main symbols


// COCXDLLApp
// See OCXDLL.cpp for the implementation of this class
//

class COCXDLLApp : public CWinApp
{
public:
	COCXDLLApp();

// Overrides
public:
	virtual BOOL InitInstance();

	DECLARE_MESSAGE_MAP()
};
