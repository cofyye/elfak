
// MFCApplication1View.cpp : implementation of the CMFCApplication1View class
//

#include "pch.h"
#include <math.h>
#include "framework.h"
// SHARED_HANDLERS can be defined in an ATL project implementing preview, thumbnail
// and search filter handlers and allows sharing of document code with that project.
#ifndef SHARED_HANDLERS
#include "MFCApplication1.h"
#endif

#include "MFCApplication1Doc.h"
#include "MFCApplication1View.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

#define SQUARE_SIZE 25

bool clickedSpace = false;

// CMFCApplication1View

IMPLEMENT_DYNCREATE(CMFCApplication1View, CView)

BEGIN_MESSAGE_MAP(CMFCApplication1View, CView)
	// Standard printing commands
	ON_COMMAND(ID_FILE_PRINT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, &CView::OnFilePrintPreview)
	ON_WM_KEYDOWN()
END_MESSAGE_MAP()

// CMFCApplication1View construction/destruction

CMFCApplication1View::CMFCApplication1View() noexcept
{
	// TODO: add construction code here

}

CMFCApplication1View::~CMFCApplication1View()
{
}

BOOL CMFCApplication1View::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	return CView::PreCreateWindow(cs);
}

// CMFCApplication1View drawing

void CMFCApplication1View::OnDraw(CDC* pDC)
{
	CMFCApplication1Doc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	if (!pDoc)
		return;

	// TODO: add draw code for native data here

	// Olovke
	CPen* grayPencil = new CPen(PS_SOLID, 1, RGB(211, 211, 211));
	CPen* pinkPencil = new CPen(PS_SOLID, 5, RGB(255, 105, 180));
	CPen* purplePencil = new CPen(PS_SOLID, 3, RGB(148, 0, 211));

	// Bojice
	CBrush* grayBrush = new CBrush(RGB(211, 211, 211));
	CBrush* redBrush = new CBrush(RGB(255, 0, 0));
	CBrush* orangeBrush = new CBrush(RGB(255, 165, 0));
	CBrush* greenBrush = new CBrush(RGB(0, 255, 0));
	CBrush* whiteBrush = new CBrush(HS_HORIZONTAL, RGB(0, 0, 255));
	CBrush* pinkBrush = new CBrush(RGB(255, 192, 203));
	CBrush* yellowBrush = new CBrush(RGB(255, 255, 0));
	CBrush* purpleBrush = new CBrush(RGB(138, 43, 226));

	// Default bojica i olovka
	CPen* oldPen = pDC->SelectObject(grayPencil);
	CBrush* oldBrush = pDC->SelectObject(grayBrush);

	// Crtanje kvadrata tj background
	pDC->Rectangle(0, 0, 20 * SQUARE_SIZE, 20 * SQUARE_SIZE);

	// Selektovanje roze olovke
	pDC->SelectObject(pinkPencil);

	// Selektovanje crvene bojice
	pDC->SelectObject(redBrush);

	// Definicija koordinata za crveni trougao
	CPoint redTriangle[3] = {
		CPoint(3 * SQUARE_SIZE + SQUARE_SIZE / 1.2, SQUARE_SIZE + SQUARE_SIZE / 2.5),
		CPoint(3 * SQUARE_SIZE + SQUARE_SIZE / 1.2, 5 * SQUARE_SIZE + SQUARE_SIZE / 2.5),
		CPoint(5 * SQUARE_SIZE + SQUARE_SIZE / 1.2, 3 * SQUARE_SIZE + SQUARE_SIZE / 5),
	};

	// Crtanje crvenog trougla
	pDC->Polygon(redTriangle, 3);

	// Selektovanje narandzaste bojice
	pDC->SelectObject(orangeBrush);

	// Definicija koordinata za narandzasti trougao
	CPoint orangeTriangle[3] = {
		CPoint(5 * SQUARE_SIZE + SQUARE_SIZE / 1.2, 3 * SQUARE_SIZE + SQUARE_SIZE / 5),
		CPoint(7 * SQUARE_SIZE + SQUARE_SIZE / 1.2, SQUARE_SIZE + SQUARE_SIZE / 2.5),
		CPoint(7 * SQUARE_SIZE + SQUARE_SIZE / 1.2, 5 * SQUARE_SIZE + SQUARE_SIZE / 2.5)
	};

	// Crtanje narandzastog trougla
	pDC->Polygon(orangeTriangle, 3);

	// Selektovanje zelene bojice
	pDC->SelectObject(greenBrush);

	// Definicija koordinata za zeleni kvadrat
	CPoint greenSquare[4] = {
		CPoint(3 * SQUARE_SIZE + SQUARE_SIZE / 1.2, 5 * SQUARE_SIZE + SQUARE_SIZE / 1.8),
		CPoint(5 * SQUARE_SIZE + SQUARE_SIZE / 1.2, 3 * SQUARE_SIZE + SQUARE_SIZE / 2),
		CPoint(7 * SQUARE_SIZE + SQUARE_SIZE / 1.2, 5 * SQUARE_SIZE + SQUARE_SIZE / 2),
		CPoint(5 * SQUARE_SIZE + SQUARE_SIZE / 1.2, 7 * SQUARE_SIZE + SQUARE_SIZE / 1.2),
	};

	// Crtanje zelenog kvadrata
	pDC->Polygon(greenSquare, 4);

	// Selektovanje bele bojice
	pDC->SelectObject(whiteBrush);

	// Definicija koordinata za beli trougao
	CPoint whiteTriangle[3] = {
		CPoint(5 * SQUARE_SIZE + SQUARE_SIZE / 1.2, 8 * SQUARE_SIZE),
		CPoint(2 * SQUARE_SIZE + SQUARE_SIZE / 2, 11 * SQUARE_SIZE),
		CPoint(5 * SQUARE_SIZE + SQUARE_SIZE / 1.2, 14 * SQUARE_SIZE)
	};

	// Crtanje belog trougla
	pDC->Polygon(whiteTriangle, 3);

	// Selektovanje roze bojice
	pDC->SelectObject(pinkBrush);

	// Definicija koordinata za rozi trougao
	CPoint pinkTriangle[3] = {
		CPoint(5 * SQUARE_SIZE + SQUARE_SIZE / 1.1, 8 * SQUARE_SIZE),
		CPoint(10 * SQUARE_SIZE + SQUARE_SIZE / 2.5, 12 * SQUARE_SIZE + SQUARE_SIZE / 2),
		CPoint(5 * SQUARE_SIZE + SQUARE_SIZE / 1.1, 16 * SQUARE_SIZE + SQUARE_SIZE / 2)
	};

	// Crtanje rozeg trougla
	pDC->Polygon(pinkTriangle, 3);

	// Selektovanje zute bojice
	pDC->SelectObject(yellowBrush);

	// Definicija koordinata za zuti trougao
	CPoint yellowTriangle[3] = {
		CPoint(3 * SQUARE_SIZE + SQUARE_SIZE / 1.1, 18 * SQUARE_SIZE + SQUARE_SIZE / 2),
		CPoint(9 * SQUARE_SIZE + SQUARE_SIZE / 0.7, 18 * SQUARE_SIZE + SQUARE_SIZE / 2),
		CPoint(10 * SQUARE_SIZE + SQUARE_SIZE / 2.5, 12 * SQUARE_SIZE + SQUARE_SIZE / 2)
	};

	// Crtanje zutrog trougla
	pDC->Polygon(yellowTriangle, 3);

	// Selektovanje ljubicaste bojice
	pDC->SelectObject(purpleBrush);

	// Definicija koordinata za ljubicasti kvadrat
	CPoint purpleTriangle[4] = {
		CPoint(10 * SQUARE_SIZE + SQUARE_SIZE / 2, 18 * SQUARE_SIZE + SQUARE_SIZE / 2),
		CPoint(12 * SQUARE_SIZE + SQUARE_SIZE / 1.1, 16 * SQUARE_SIZE + SQUARE_SIZE / 2),
		CPoint(17 * SQUARE_SIZE, 16 * SQUARE_SIZE + SQUARE_SIZE / 2),
		CPoint(15 * SQUARE_SIZE, 18 * SQUARE_SIZE + SQUARE_SIZE / 2)
	};

	// Crtanje ljubicastog kvadrata
	pDC->Polygon(purpleTriangle, 4);

	// Selektovanje ljubicaste olovke i crvene bojice
	pDC->SelectObject(purplePencil);
	pDC->SelectObject(redBrush);

	// Crtanje N=8 unutar crvenog trougla
	DrawRegularPolygon(pDC, 4 * SQUARE_SIZE + SQUARE_SIZE / 1.6, 3 * SQUARE_SIZE + SQUARE_SIZE / 2.7, 10, 8, 0);

	// Selektovanje narandzaste bojice
	pDC->SelectObject(orangeBrush);

	// Crtanje N=7 unutar narandzastog trougla
	DrawRegularPolygon(pDC, 7 * SQUARE_SIZE + SQUARE_SIZE / 11, 3 * SQUARE_SIZE + SQUARE_SIZE / 2.7, 10, 7, 0);

	// Selektovanje bele bojice
	pDC->SelectObject(whiteBrush);

	// Crtanje N=6 unutar belog trougla
	DrawRegularPolygon(pDC, 4 * SQUARE_SIZE + SQUARE_SIZE / 2, 11 * SQUARE_SIZE + SQUARE_SIZE / 8, 15, 6, 0);

	// Selektovanje roze bojice
	pDC->SelectObject(pinkBrush);

	// Crtanje N=5 unutar rozeg trougla
	DrawRegularPolygon(pDC, 7 * SQUARE_SIZE + SQUARE_SIZE / 1.2, 12 * SQUARE_SIZE + SQUARE_SIZE / 2, 20, 5, 0);

	// Selektovanje zute bojice
	pDC->SelectObject(yellowBrush);

	// Crtanje N=4 unutar zutog trougla
	DrawRegularPolygon(pDC, 8 * SQUARE_SIZE + SQUARE_SIZE / 2.2, 16 * SQUARE_SIZE + SQUARE_SIZE / 1.2, 20, 4, 0);

	// Selektovanje defaultne olovke i bojice
	pDC->SelectObject(oldPen);
	pDC->SelectObject(oldBrush);

	// Brisanje bojica
	delete redBrush;
	delete grayBrush;
	delete orangeBrush;
	delete greenBrush;
	delete whiteBrush;
	delete pinkBrush;
	delete yellowBrush;
	delete purpleBrush;

	// Brisanje olovaka
	delete grayPencil;
	delete pinkPencil;
	delete purplePencil;
}


// CMFCApplication1View printing

BOOL CMFCApplication1View::OnPreparePrinting(CPrintInfo* pInfo)
{
	// default preparation
	return DoPreparePrinting(pInfo);
}

void CMFCApplication1View::OnBeginPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add extra initialization before printing
}

void CMFCApplication1View::OnEndPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add cleanup after printing
}


// CMFCApplication1View diagnostics

#ifdef _DEBUG
void CMFCApplication1View::AssertValid() const
{
	CView::AssertValid();
}

void CMFCApplication1View::Dump(CDumpContext& dc) const
{
	CView::Dump(dc);
}

CMFCApplication1Doc* CMFCApplication1View::GetDocument() const // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CMFCApplication1Doc)));
	return (CMFCApplication1Doc*)m_pDocument;
}
#endif //_DEBUG


// CMFCApplication1View message handlers


void CMFCApplication1View::OnKeyDown(UINT nChar, UINT nRepCnt, UINT nFlags)
{
	CDC* pDC = GetDC();

	CPen* whitePencil = new CPen(PS_SOLID, 1, RGB(255, 255, 255));

	if (nChar == VK_SPACE && !clickedSpace) {
		clickedSpace = true;
		pDC->SelectObject(whitePencil);

		for (int i = SQUARE_SIZE; i < SQUARE_SIZE * 20; i += SQUARE_SIZE) {
			pDC->MoveTo(0, i);
			pDC->LineTo(SQUARE_SIZE * 20, i);

			pDC->MoveTo(i, 0);
			pDC->LineTo(i, SQUARE_SIZE * 20);
		}
	}
	else if(clickedSpace && nChar == VK_SPACE) {
		clickedSpace = false;

		Invalidate();
	}

	delete whitePencil;

	CView::OnKeyDown(nChar, nRepCnt, nFlags);
}

void CMFCApplication1View::DrawRegularPolygon(CDC* pDC, int cx, int cy, int r, int n, float rotAngle)
{
	if (n < 3) return; // Ensure we have at least 3 points for a polygon

	float alfa = 2 * 3.14 / n; // Angle between vertices
	float rotacija = rotAngle * 3.14 / 180; // Convert degrees to radians
	CPoint* nizTemenaMnogougla = new CPoint[n];

	for (int i = 0; i < n; i++)
	{
		float stepen = i * alfa;
		nizTemenaMnogougla[i] = CPoint(r * cos(stepen + rotacija) + cx, r * sin(stepen + rotacija) + cy);
	}

	pDC->Polygon(nizTemenaMnogougla, n);

	delete[] nizTemenaMnogougla; // Clean up memory
}
