
// Robot.h : main header file for the Robot application
//
#pragma once

#ifndef __AFXWIN_H__
	#error "include 'pch.h' before including this file for PCH"
#endif

#include "resource.h"       // main symbols


// CRobotApp:
// See Robot.cpp for the implementation of this class
//

class CRobotApp : public CWinApp
{
public:
	CRobotApp() noexcept;


// Overrides
public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();

// Implementation
	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
};

extern CRobotApp theApp;
