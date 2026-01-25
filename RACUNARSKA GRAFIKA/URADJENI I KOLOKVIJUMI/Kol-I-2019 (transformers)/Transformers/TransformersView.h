
// TransformersView.h : interface of the CTransformersView class
//

#pragma once
#include "DImage.h"


class CTransformersView : public CView
{
protected: // create from serialization only
	CTransformersView() noexcept;
	DECLARE_DYNCREATE(CTransformersView)

// Attributes
public:
	CTransformersDoc* GetDocument() const;
	DImage* body;
	DImage* arm1;
	DImage* arm2;
	DImage* leg1;
	DImage* leg2;
	DImage* background;

	float arm1Angle = 0;
	float arm2Angle = 0;
	float leg2Angle = 0;
	float bodyAngle = -90;

	int kreanjeRobota = 0;

// Operations
public:

// Overrides
public:
	virtual void OnDraw(CDC* pDC);  // overridden to draw this view
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
	void DrawBackground(CDC* pDC, CRect rc);
	void DrawImgTransparent(CDC* pDC, DImage* pImage);
	void Translate(CDC* pDC, float dX, float dY, bool rightMultiply = true);
	void Rotate(CDC* pDC, float angle, bool rightMultiply = true);
	void DrawArm1(CDC* pDC);
	void DrawArm2(CDC* pDC);
	void DrawLeg1(CDC* pDC);
	void DrawLeg2(CDC* pDC);
	void Body1(CDC* pDC);
	void DrawPoint(CDC* pDC, int x, int y);
	void DrawTransformer(CDC* pDC);
	void Upisi();
	int Ucitaj();
protected:
	virtual BOOL OnPreparePrinting(CPrintInfo* pInfo);
	virtual void OnBeginPrinting(CDC* pDC, CPrintInfo* pInfo);
	virtual void OnEndPrinting(CDC* pDC, CPrintInfo* pInfo);

// Implementation
public:
	virtual ~CTransformersView();
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

#ifndef _DEBUG  // debug version in TransformersView.cpp
inline CTransformersDoc* CTransformersView::GetDocument() const
   { return reinterpret_cast<CTransformersDoc*>(m_pDocument); }
#endif

