
// Lamp.h : main header file for the Lamp application
//
#pragma once

#ifndef __AFXWIN_H__
	#error "include 'pch.h' before including this file for PCH"
#endif

#include "resource.h"       // main symbols


// CLampApp:
// See Lamp.cpp for the implementation of this class
//

class CLampApp : public CWinApp
{
public:
	CLampApp() noexcept;


// Overrides
public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();

// Implementation
	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
};

extern CLampApp theApp;
