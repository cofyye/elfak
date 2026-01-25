
// Bager.h : main header file for the Bager application
//
#pragma once

#ifndef __AFXWIN_H__
	#error "include 'pch.h' before including this file for PCH"
#endif

#include "resource.h"       // main symbols


// CBagerApp:
// See Bager.cpp for the implementation of this class
//

class CBagerApp : public CWinApp
{
public:
	CBagerApp() noexcept;


// Overrides
public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();

// Implementation
	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
};

extern CBagerApp theApp;
