
// RobotView.h : interface of the CRobotView class
//

#include "DImage.h"
#pragma once


class CRobotView : public CView
{
protected: // create from serialization only
	CRobotView() noexcept;
	DECLARE_DYNCREATE(CRobotView)

// Attributes
public:
	CRobotDoc* GetDocument() const;

	DImage* glava;
	DImage* nadkolenica;
	DImage* nadlaktica;
	DImage* podkolenica;
	DImage* podlaktica;
	DImage* saka;
	DImage* telo;
	DImage* stopalo;
	DImage* pozadina;

	int nadlakticaAngle = 0;
	int podlakticaAngle = 0;
	int sakaAngle = 0;
	int robotAngle = 0;
	float robotScale = 1;
	bool mirrorBody = false;

// Operations
public:

// Overrides
public:
	virtual void OnDraw(CDC* pDC);  // overridden to draw this view
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
	void Translate(CDC* pDC, float dX, float dY, bool rightMultiply = true);
	void Rotate(CDC* pDC, float angle, bool rightMultiply = true);
	void Scale(CDC* pDC, float sX, float sY, bool rightMultiply = true);
	void DrawBackground(CDC* pDC);
	void DrawImgTransparent(CDC* pDC, DImage* pImage);
	void DrawHalf(CDC* pDC);
	void DrawNode(CDC* pDC, int x, int y);
	void DrawTelo(CDC* pDC);
	void DrawNadlaktica(CDC* pDC);
	void DrawPodlaktica(CDC* pDC);
	void DrawSaka(CDC* pDC);
	void DrawNadkolenica(CDC* pDC);
	void DrawPodkolenica(CDC* pDC);
	void DrawStopalo(CDC* pDC);
	void DrawHead(CDC* pDC);
	void DrawRobot(CDC* pDC);
protected:
	virtual BOOL OnPreparePrinting(CPrintInfo* pInfo);
	virtual void OnBeginPrinting(CDC* pDC, CPrintInfo* pInfo);
	virtual void OnEndPrinting(CDC* pDC, CPrintInfo* pInfo);

// Implementation
public:
	virtual ~CRobotView();
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

#ifndef _DEBUG  // debug version in RobotView.cpp
inline CRobotDoc* CRobotView::GetDocument() const
   { return reinterpret_cast<CRobotDoc*>(m_pDocument); }
#endif

