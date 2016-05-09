#pragma once

#include "resource.h" 
#include "videorecvctrlctrl1.h"
// OCXDLLDialog dialog

class OCXDLLDialog : public CDialog
{
	DECLARE_DYNAMIC(OCXDLLDialog)

public:
	OCXDLLDialog(CWnd* pParent = NULL);   // standard constructor
	virtual ~OCXDLLDialog();

// Dialog Data
	enum { IDD = IDD_DIALOG1 };

	HRESULT Play(unsigned long IP, unsigned short port, LPCTSTR user, LPCTSTR pass, __int64 historicalTime);
	HRESULT Stop(void);

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
	CVideorecvctrlctrl1 m_OCX;
};
