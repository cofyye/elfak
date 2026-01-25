
// Transformers.h : main header file for the Transformers application
//
#pragma once

#ifndef __AFXWIN_H__
	#error "include 'pch.h' before including this file for PCH"
#endif

#include "resource.h"       // main symbols


// CTransformersApp:
// See Transformers.cpp for the implementation of this class
//

class CTransformersApp : public CWinApp
{
public:
	CTransformersApp() noexcept;


// Overrides
public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();

// Implementation
	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
};

extern CTransformersApp theApp;
