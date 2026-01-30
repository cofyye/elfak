
// Transporter.h : main header file for the Transporter application
//
#pragma once

#ifndef __AFXWIN_H__
	#error "include 'pch.h' before including this file for PCH"
#endif

#include "resource.h"       // main symbols


// CTransporterApp:
// See Transporter.cpp for the implementation of this class
//

class CTransporterApp : public CWinApp
{
public:
	CTransporterApp() noexcept;


// Overrides
public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();

// Implementation
	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
};

extern CTransporterApp theApp;
