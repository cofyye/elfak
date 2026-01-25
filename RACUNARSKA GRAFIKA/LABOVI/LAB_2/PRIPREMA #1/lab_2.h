// IND_18627View.h : interface of the CIND_18627View class
//

#pragma once


class CIND_18627View : public CView
{
protected: // create from serialization only
    CIND_18627View() noexcept;
    DECLARE_DYNCREATE(CIND_18627View)

// Attributes
public:
    CIND18627Doc* GetDocument() const;
    bool showGrid = false;
    int angle1 = 0;
    int angle2 = 0;

// Operations
public:

// Overrides
public:
    virtual void OnDraw(CDC* pDC);  // overridden to draw this view
    void Translate(CDC* pDC, float dX, float dY, bool rightMultiply);
    void Scale(CDC* pDC, float sX, float sY, bool rightMultiply);
    void Rotate(CDC* pDC, float angle, bool rightMultiply);

    void DrawFigure(CDC* pDC);
    void DrawPot(CDC* pDC);
    void CactusPart(CDC* pDC, HENHMETAFILE *texture, int x, int y, int width, int hight, int angleCenterRot, int angleSelfRot, int angle = 0);
    void RootPart(CDC* pDC, int x, int y, int angleCenterRot);
    virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
protected:
    virtual BOOL OnPreparePrinting(CPrintInfo* pInfo);
    virtual void OnBeginPrinting(CDC* pDC, CPrintInfo* pInfo);
    virtual void OnEndPrinting(CDC* pDC, CPrintInfo* pInfo);

// Implementation
public:
    virtual ~CIND_18627View();
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
};

#ifndef _DEBUG  // debug version in IND_18627View.cpp
inline CIND18627Doc* CIND_18627View::GetDocument() const
   { return reinterpret_cast<CIND18627Doc*>(m_pDocument); }
#endif