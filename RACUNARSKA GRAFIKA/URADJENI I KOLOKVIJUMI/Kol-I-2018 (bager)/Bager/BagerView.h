
// BagerView.h : interface of the CBagerView class
//

#include "DImage.h"
#pragma once


class CBagerView : public CView
{
protected: // create from serialization only
	CBagerView() noexcept;
	DECLARE_DYNCREATE(CBagerView)

// Attributes
public:
	CBagerDoc* GetDocument() const;

	DImage* bager, * arm1, * arm2, * pozadina;
	HENHMETAFILE fork;
	ENHMETAHEADER forkHeader;

	int arm1Angle = -90, arm2Angle = -90, forkAngle = -90;
	int kretanjeBagera = 0;


// Operations
public:

// Overrides
public:
	void Translate(CDC* pDC, float dX, float dY, bool rightMultiply = true);
	void Rotate(CDC* pDC, float angle, bool rightMultiply = true);
	void Scale(CDC* pDC, float sX, float sY, bool rightMultiply = true);
	void DrawBackground(CDC* pDC);
	void DrawImgTransparent(CDC* pDC, DImage* pImage);
	void DrawBody(CDC* pDC);
	void DrawArm1(CDC* pDC);
	void DrawArm2(CDC* pDC);
	void DrawFork(CDC* pDC);
	void DrawExcavator(CDC* pDC);
	virtual void OnDraw(CDC* pDC);  // overridden to draw this view
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
protected:
	virtual BOOL OnPreparePrinting(CPrintInfo* pInfo);
	virtual void OnBeginPrinting(CDC* pDC, CPrintInfo* pInfo);
	virtual void OnEndPrinting(CDC* pDC, CPrintInfo* pInfo);

// Implementation
public:
	virtual ~CBagerView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Generated message map functions
protected:
	DECLARE_MESSAGE_MAP()
public:
	afx_msg BOOL OnEraseBkgnd(CDC* pDC);
	afx_msg void OnKeyDown(UINT nChar, UINT nRepCnt, UINT nFlags);
};

#ifndef _DEBUG  // debug version in BagerView.cpp
inline CBagerDoc* CBagerView::GetDocument() const
   { return reinterpret_cast<CBagerDoc*>(m_pDocument); }
#endif

