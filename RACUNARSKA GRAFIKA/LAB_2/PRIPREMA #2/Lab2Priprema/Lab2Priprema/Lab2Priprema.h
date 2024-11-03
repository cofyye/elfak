
// Lab2Priprema.h : main header file for the Lab2Priprema application
//
#pragma once

#ifndef __AFXWIN_H__
	#error "include 'pch.h' before including this file for PCH"
#endif

#include "resource.h"       // main symbols


// CLab2PripremaApp:
// See Lab2Priprema.cpp for the implementation of this class
//

class CLab2PripremaApp : public CWinApp
{
public:
	CLab2PripremaApp() noexcept;


// Overrides
public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();

// Implementation
	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
};

extern CLab2PripremaApp theApp;
