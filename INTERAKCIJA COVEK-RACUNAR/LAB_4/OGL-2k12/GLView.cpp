// GLView.cpp : implementation of the CGLView class
//

#include "stdafx.h"
#include "GL.h"
#include <string>
#include "GLDoc.h"
#include "GLView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CGLView

IMPLEMENT_DYNCREATE(CGLView, CView)

BEGIN_MESSAGE_MAP(CGLView, CView)
	//{{AFX_MSG_MAP(CGLView)
	ON_WM_CREATE()
	ON_WM_DESTROY()
	ON_WM_ERASEBKGND()
	ON_WM_SIZE()
	//}}AFX_MSG_MAP
	// Standard printing commands
	ON_COMMAND(ID_FILE_PRINT, CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, CView::OnFilePrintPreview)
	ON_WM_KEYDOWN()
	//	ON_WM_MOUSEHWHEEL()
	ON_WM_MOUSEWHEEL()
	ON_WM_LBUTTONDOWN()
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CGLView construction/destruction

CGLView::CGLView()
{
	// TODO: add construction code here
	bClick = false;
}

CGLView::~CGLView()
{
}

BOOL CGLView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	return CView::PreCreateWindow(cs);
}

/////////////////////////////////////////////////////////////////////////////
// CGLView drawing

void CGLView::OnDraw(CDC* pDC)
{
	CGLDoc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	// TODO: add draw code for native data here
	m_glRenderer.DrawScene(pDC);
}

/////////////////////////////////////////////////////////////////////////////
// CGLView printing

BOOL CGLView::OnPreparePrinting(CPrintInfo* pInfo)
{
	// default preparation
	return DoPreparePrinting(pInfo);
}

void CGLView::OnBeginPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add extra initialization before printing
}

void CGLView::OnEndPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add cleanup after printing
}

/////////////////////////////////////////////////////////////////////////////
// CGLView diagnostics

#ifdef _DEBUG
void CGLView::AssertValid() const
{
	CView::AssertValid();
}

void CGLView::Dump(CDumpContext& dc) const
{
	CView::Dump(dc);
}

CGLDoc* CGLView::GetDocument() // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CGLDoc)));
	return (CGLDoc*)m_pDocument;
}
#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// CGLView message handlers

int CGLView::OnCreate(LPCREATESTRUCT lpCreateStruct)
{
	if (CView::OnCreate(lpCreateStruct) == -1)
		return -1;

	CDC* pDC = GetDC();
	m_glRenderer.CreateGLContext(pDC);
	ReleaseDC(pDC);

	return 0;
}

void CGLView::OnDestroy()
{
	CView::OnDestroy();

	CDC* pDC = GetDC();
	m_glRenderer.DestroyScene(pDC);
	ReleaseDC(pDC);
}

BOOL CGLView::OnEraseBkgnd(CDC* pDC)
{
	return TRUE;

	//return CView::OnEraseBkgnd(pDC);
}

void CGLView::OnSize(UINT nType, int cx, int cy)
{
	CView::OnSize(nType, cx, cy);

	CDC* pDC = GetDC();
	m_glRenderer.Reshape(pDC, cx, cy);
	ReleaseDC(pDC);
}

void CGLView::OnInitialUpdate()
{
	CView::OnInitialUpdate();

	CDC* pDC = GetDC();
	m_glRenderer.PrepareScene(pDC);
	ReleaseDC(pDC);

}

// zumiranje preko tastature
// pomeranje preko tastature
// rotacija preko tastature
void CGLView::OnKeyDown(UINT nChar, UINT nRepCnt, UINT nFlags)
{

	// zumiranje
	if (nChar == VK_ADD)// dugme +
		m_glRenderer.m_z += 0.2;
	if (nChar == VK_SUBTRACT)// dugme -
		m_glRenderer.m_z -= 0.2;

	bool bShift = GetKeyState(VK_SHIFT) & 0x8000; // rotacija
	if (nChar == VK_RIGHT) 
		if (bShift)
			m_glRenderer.m_Ry -= 5.0;
		else
			m_glRenderer.m_Rx -= 0.2;
	if (nChar == VK_LEFT)
		if (bShift)
			m_glRenderer.m_Ry += 5.0;
		else
			m_glRenderer.m_Rx += 0.2;

	// pomeranje
	if (nChar == VK_RIGHT)
		m_glRenderer.m_x -= 0.2;
	if (nChar == VK_LEFT)
		m_glRenderer.m_x += 0.2;
	if (nChar == VK_UP)
		m_glRenderer.m_y -= 0.2;
	if (nChar == VK_DOWN)
		m_glRenderer.m_y += 0.2;

	Invalidate();
	CView::OnKeyDown(nChar, nRepCnt, nFlags);
}

// skrol na misa
BOOL CGLView::OnMouseWheel(UINT nFlags, short zDelta, CPoint pt)
{
	if (zDelta > 0.0)
		m_glRenderer.m_z += 0.2;
	if (zDelta < 0.0)
		m_glRenderer.m_z -= 0.2;
	Invalidate();
	return CView::OnMouseWheel(nFlags, zDelta, pt);
}

// pomeranje pogleda pomocu misa.
void CGLView::OnLButtonDown(UINT nFlags, CPoint point)
{
	bool bShift = GetKeyState(VK_SHIFT) & 0x8000;

	CDC* pDC = GetDC();
	int p = m_glRenderer.PickObject(pDC, point.x, point.y);

	if (p == 0) // ako je kliknuto na scenu, p=0 je scena
	{
		if (point.x < 400)
		{
			if (bShift)
			{
				if (point.y < 200)
				{
					m_glRenderer.m_Rx += 2.0;
					m_glRenderer.m_Ry -= 2.0;
				}
				else if (point.y > 400)
				{
					m_glRenderer.m_Ry -= 2.0;
					m_glRenderer.m_Rx -= 2.0;
				}
				else
				{
					m_glRenderer.m_Ry -= 2.0;

				}
			}
			else
			{
				if (point.y < 200)
				{
					m_glRenderer.m_y -= 0.2;
					m_glRenderer.m_x += 0.2;
				}
				else if (point.y > 400)
				{
					m_glRenderer.m_y += 0.2;
					m_glRenderer.m_x += 0.2;
				}
				else
				{
					m_glRenderer.m_x += 0.2;
				}

			}
		}
		else if (point.x > 800)
		{
			if (bShift)
			{
				if (point.y < 200)
				{
					m_glRenderer.m_Rx += 2.0;
					m_glRenderer.m_Ry += 2.0;
				}
				else if (point.y > 400)
				{
					m_glRenderer.m_Ry += 2.0;
					m_glRenderer.m_Rx -= 2.0;
				}
				else
				{
					m_glRenderer.m_Ry += 2.0;
				}
			}
			else
			{
				if (point.y < 200)
				{
					m_glRenderer.m_y -= 0.2;
					m_glRenderer.m_x -= 0.2;
				}
				else if (point.y > 400)
				{
					m_glRenderer.m_y += 0.2;
					m_glRenderer.m_x -= 0.2;
				}
				else
				{
					m_glRenderer.m_x -= 0.2;
				}
			}
		}
		else
		{
			if (bShift)
			{
				if (point.y < 200)
				{
					m_glRenderer.m_Rx -= 2.0;
				}
				else if (point.y > 400)
				{
					m_glRenderer.m_Rx += 2.0;
				}
			}
			else
			{
				if (point.y < 200)
				{
					m_glRenderer.m_y -= 0.2;
				}
				else if (point.y > 400)
				{
					m_glRenderer.m_y += 0.2;
				}
			}
		}
	}

	Invalidate();

	CString text = "Kliknuto na:";

	if (p != 0)
	{
		if (p == 1)
		{
			text = "Kliknuto na zelenu kocku";
		}
		else if (p == 2)
		{
			text = "Kliknuto na crvenu kocku";
		}
		else if (p == 3)
		{
			text = "Kliknuto na plavu kocku";
		}
		else if (p == 4)
		{
			text = "Kliknuto na zutu kocku";
		}
		else {
			text = "Kliknuto na ljubicastu kocku unutar zelene";
		}

		AfxMessageBox(text);
	}
	CView::OnLButtonDown(nFlags, point);
}