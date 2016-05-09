#include "stdafx.h"
#include "OCXDLLInterface.h"
#include "OCXDLLDialog.h"

OCXDLL_API OCXDLLDialog*VideoCreate (CWnd *parent) {
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	OCXDLLDialog *wnd = new OCXDLLDialog();
	wnd->Create(OCXDLLDialog::IDD, parent);
	return wnd;
}

OCXDLL_API HRESULT       VideoDestroy(OCXDLLDialog *wnd) {
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	wnd->DestroyWindow();
	delete wnd;
	return S_OK;
}

OCXDLL_API HRESULT       VideoPlay   (OCXDLLDialog *wnd, unsigned long IP, unsigned short port, LPCTSTR user, LPCTSTR pass, __int64 historicalTime) {
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	return wnd->Play(IP, port, user, pass, historicalTime);
}

OCXDLL_API HRESULT       VideoStop   (OCXDLLDialog *wnd) {
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	return wnd->Stop();
}
