#pragma once

#ifdef OCXDLL_EXPORTS
#define OCXDLL_API __declspec(dllexport)
#else
#define OCXDLL_API __declspec(dllimport)
#endif

#include "OCXDLLDialog.h"

OCXDLL_API OCXDLLDialog*VideoCreate (CWnd *parent);
OCXDLL_API HRESULT       VideoDestroy(OCXDLLDialog *wnd);
OCXDLL_API HRESULT       VideoPlay   (OCXDLLDialog *wnd, unsigned long IP, unsigned short port, LPCTSTR user, LPCTSTR pass, __int64 historicalTime);
OCXDLL_API HRESULT       VideoStop   (OCXDLLDialog *wnd);
