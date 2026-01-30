
// TransporterView.h : interface of the CTransporterView class
//

#include "DImage.h"
#pragma once


class CTransporterView : public CView
{
protected: // create from serialization only
	CTransporterView() noexcept;
	DECLARE_DYNCREATE(CTransporterView)

// Attributes
public:
	CTransporterDoc* GetDocument() const;

	DImage* body, * leg1, * leg2, * leg3, * pozadina;
	float transporterScale;
	float legAngles[4];
	float legPrevAngels[4];
	float kretanjeTransportera;
	int offsetPozadineX = 0, offsetPozadineY = 0;

// Operations
public:

// Overrides
public:
	void DrawPozadina(CDC* pDC, CRect rect);
	void ChangeAngle(float* angle, float* prevAngle);
	void DrawBody(CDC* pDC);
	void DrawLeg(CDC* pDC, double alpha, double dx, double dy);
	void DrawScene(CDC* pDC, CRect rect);
	virtual void OnDraw(CDC* pDC);  // overridden to draw this view
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
	void LoadIdentity(CDC* pDC);
	void Translate(CDC* pDC, float dX, float dY, bool rightMultiply = true);
	void Rotate(CDC* pDC, float angle, bool rightMultiply = true);
	void Scale(CDC* pDC, float sX, float sY, bool rightMultiply = true);
	void DrawImgTransparent(CDC* pDC, DImage* pImage);
protected:
	virtual BOOL OnPreparePrinting(CPrintInfo* pInfo);
	virtual void OnBeginPrinting(CDC* pDC, CPrintInfo* pInfo);
	virtual void OnEndPrinting(CDC* pDC, CPrintInfo* pInfo);

// Implementation
public:
	virtual ~CTransporterView();
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

#ifndef _DEBUG  // debug version in TransporterView.cpp
inline CTransporterDoc* CTransporterView::GetDocument() const
   { return reinterpret_cast<CTransporterDoc*>(m_pDocument); }
#endif

