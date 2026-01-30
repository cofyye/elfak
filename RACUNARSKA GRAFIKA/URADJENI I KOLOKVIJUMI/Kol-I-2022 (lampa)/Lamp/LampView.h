
// LampView.h : interface of the CLampView class
//
#include "DImage.h"

#pragma once


class CLampView : public CView
{
protected: // create from serialization only
	CLampView() noexcept;
	DECLARE_DYNCREATE(CLampView)

// Attributes
public:
	CLampDoc* GetDocument() const;

	DImage* base;
	DImage* arm1;
	DImage* arm2;
	DImage* head;
	DImage* pozadina;
	DImage* baseShadow;
	DImage* arm1Shadow;
	DImage* arm2Shadow;
	DImage* headShadow;

	int arm1Angle = -45, arm2Angle = -90, headAngle = 180;
	//int arm1Angle = 0, arm2Angle = 0, headAngle = 0;

// Operations
public:

// Overrides
public:
	void DrawBackground(CDC* pDC);
	void DrawImgTransparent(CDC* pDC, DImage* pImage);
	void DrawLampBase(CDC* pDC, bool blsShadow = false);
	void DrawLampArm1(CDC* pDC, bool blsShadow = false);
	void DrawLampArm2(CDC* pDC, bool blsShadow = false);
	void DrawLampHead(CDC* pDC, bool blsShadow = false);
	void DrawLamp(CDC* pDC, bool blsShadow = false);
	void DrawLampShadow(CDC* pDC);
	virtual void OnDraw(CDC* pDC);  // overridden to draw this view
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
	void Translate(CDC* pDC, float dX, float dY, bool rightMultiply = true);
	void Rotate(CDC* pDC, float angle, bool rightMultiply = true);
	void Scale(CDC* pDC, float sX, float sY, bool rightMultiply = true);
protected:
	virtual BOOL OnPreparePrinting(CPrintInfo* pInfo);
	virtual void OnBeginPrinting(CDC* pDC, CPrintInfo* pInfo);
	virtual void OnEndPrinting(CDC* pDC, CPrintInfo* pInfo);

// Implementation
public:
	virtual ~CLampView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Generated message map functions
protected:
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnKeyDown(UINT nChar, UINT nRepCnt, UINT nFlags);
	afx_msg BOOL OnEraseBkgnd(CDC* pDC);
};

#ifndef _DEBUG  // debug version in LampView.cpp
inline CLampDoc* CLampView::GetDocument() const
   { return reinterpret_cast<CLampDoc*>(m_pDocument); }
#endif

