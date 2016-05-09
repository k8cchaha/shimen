// OCXDLLDialog.cpp : implementation file
//

#include "stdafx.h"
#include "OCXDLL.h"
#include "OCXDLLDialog.h"


// OCXDLLDialog dialog

IMPLEMENT_DYNAMIC(OCXDLLDialog, CDialog)

OCXDLLDialog::OCXDLLDialog(CWnd* pParent /*=NULL*/)
	: CDialog(OCXDLLDialog::IDD, pParent)
{

}

OCXDLLDialog::~OCXDLLDialog()
{
}

void OCXDLLDialog::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_VIDEORECVCTRLCTRL1, m_OCX);
}


BEGIN_MESSAGE_MAP(OCXDLLDialog, CDialog)
END_MESSAGE_MAP()


// OCXDLLDialog message handlers
HRESULT OCXDLLDialog::Play(unsigned long IP, unsigned short port, LPCTSTR user, LPCTSTR pass, __int64 historicalTime)
{
	HRESULT hr;

	if (FAILED(hr = m_OCX.Init()))
		return hr;
	if (FAILED(hr = m_OCX.Connect(IP, port, user, pass, historicalTime)))
		return hr;
	if (FAILED(hr = m_OCX.StartVideo()))
		return hr;
	return S_OK;
}

HRESULT OCXDLLDialog::Stop(void)
{
	HRESULT hr;
	if (FAILED(hr = m_OCX.StopVideo()))
		return hr;
	if (FAILED(hr = m_OCX.Destroy()))
		return hr;
	return S_OK;
}
