
// Lab2PripremaView.cpp : implementation of the CLab2PripremaView class
//

#include "pch.h"
#include "framework.h"
// SHARED_HANDLERS can be defined in an ATL project implementing preview, thumbnail
// and search filter handlers and allows sharing of document code with that project.
#ifndef SHARED_HANDLERS
#include "Lab2Priprema.h"
#endif

#include "Lab2PripremaDoc.h"
#include "Lab2PripremaView.h"
#include "math.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif



// CLab2PripremaView
const double PI = 3.14159265358979323846;
bool mreza_aktivna = false;

IMPLEMENT_DYNCREATE(CLab2PripremaView, CView)

BEGIN_MESSAGE_MAP(CLab2PripremaView, CView)
	// Standard printing commands
	ON_COMMAND(ID_FILE_PRINT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, &CView::OnFilePrintPreview)
	ON_WM_KEYDOWN()
END_MESSAGE_MAP()

// CLab2PripremaView construction/destruction

CLab2PripremaView::CLab2PripremaView() noexcept
{
	// TODO: add construction code here
	this->k = 25;
	this->svetlo = GetEnhMetaFile(CString("cactus_part_light.emf"));
	this->tamno = GetEnhMetaFile(CString("cactus_part.emf"));

	this->kaktusRotacija = 0;
	this->deoRotacija = 0;
	this->okvirni = CRect(-30, 0, 30, -75);
}

CLab2PripremaView::~CLab2PripremaView()
{
}

BOOL CLab2PripremaView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	return CView::PreCreateWindow(cs);
}
void CLab2PripremaView::Grid(CDC* pDC)
{
	CPen* sivaOlovka = new CPen(PS_SOLID, 2, RGB(245, 245, 245));
	CPen* staraOlovka = pDC->SelectObject(sivaOlovka);
	for (int i = this->k; i <= this->k * 20; i += this->k)
	{
		pDC->MoveTo(0, i);
		pDC->LineTo(this->k * 20, i);

		pDC->MoveTo(i, 0);
		pDC->LineTo(i, this->k * 20);
	}
	mreza_aktivna = true;
	pDC->SelectObject(staraOlovka);
	delete sivaOlovka;
}
void CLab2PripremaView::Translate(CDC* pDC, float dx, float dy, bool rightMultiply)
{
	XFORM trans;
	trans.eM11 = 1;
	trans.eM12 = 0;
	trans.eM21 = 0;
	trans.eM22 = 1;
	trans.eDx = dx;
	trans.eDy = dy;

	if (rightMultiply)
		pDC->ModifyWorldTransform(&trans, MWT_RIGHTMULTIPLY);
	else
		pDC->ModifyWorldTransform(&trans, MWT_LEFTMULTIPLY);
}
void CLab2PripremaView::Rotate(CDC* pDC, float angle, bool rightMultiply)
{
	XFORM trans;
	trans.eM11 = cos(angle);
	trans.eM12 = sin(angle);
	trans.eM21 = -sin(angle);
	trans.eM22 = cos(angle);
	trans.eDx = 0;
	trans.eDy = 0;

	if (rightMultiply)
		pDC->ModifyWorldTransform(&trans, MWT_RIGHTMULTIPLY);
	else
		pDC->ModifyWorldTransform(&trans, MWT_LEFTMULTIPLY);
}
void CLab2PripremaView::Scale(CDC* pDC, float sX, float sY, bool rightMultiply)
{
	XFORM trans;
	trans.eM11 = sX;
	trans.eM12 = 0;
	trans.eM21 = 0;
	trans.eM22 = sY;
	trans.eDx = 0;
	trans.eDy = 0;

	if (rightMultiply)
		pDC->ModifyWorldTransform(&trans, MWT_RIGHTMULTIPLY);
	else
		pDC->ModifyWorldTransform(&trans, MWT_LEFTMULTIPLY);
}

void CLab2PripremaView::Saksija(CDC* pDC)
{
	CBrush* braonCetka = new CBrush(RGB(218, 165, 32));
	CPen* crnaOlovka = new CPen(PS_SOLID , 1, RGB(0, 0, 0));
	CBrush* staraCetka = pDC->SelectObject(braonCetka);
	CPen* staraOlovka = pDC->SelectObject(crnaOlovka);
	
	CPoint TemenaSaksije[4] = { CPoint(8*this->k+ 8 , 20*this->k) , CPoint(11*this->k+17 , 20*this->k) , CPoint(12*this->k , 18*this->k) , CPoint(8*this->k , 18*this->k)};
	pDC->Polygon(TemenaSaksije, 4);
	pDC->Rectangle(7 * this->k + 15, 17 * this->k + 3, 12 * this->k + 10, 18 * this->k);
	pDC->SelectObject(staraCetka);
	pDC->SelectObject(staraOlovka);

	delete braonCetka;
}

void CLab2PripremaView::Okvir(CDC* pDC) 
{
	CBrush* plavaCetka = new CBrush(RGB(173, 216, 230));
	CPen* plavaOlovka = new CPen(PS_SOLID, 1, RGB(173, 216, 230));

	CBrush* staraCetka = pDC->SelectObject(plavaCetka);
	CPen* staraOlovka = pDC->SelectObject(plavaOlovka);

	pDC->Rectangle(0, 0, this->k * 20, this->k * 20);
	pDC->SelectObject(staraCetka);
	pDC->SelectObject(staraOlovka);
	delete plavaCetka;
	delete plavaOlovka;
}

void CLab2PripremaView::DrawFigure(CDC* pDC)
{
	int prevMode = SetGraphicsMode(pDC->m_hDC, GM_ADVANCED); 
	XFORM XformOld;
	pDC->GetWorldTransform(&XformOld);

	

	Translate(pDC, 10 * this->k, 17 * this->k, false);
	Rotate(pDC, this->kaktusRotacija, false);

	pDC->PlayMetaFile(this->svetlo, okvirni);
	pDC->Ellipse(-10, -10, 10, 10);

	Translate(pDC, 0, -3 * this->k, false);
	Scale(pDC, 0.4, 1, false);
	pDC->PlayMetaFile(this->tamno, okvirni);
	Scale(pDC, 2.5, 1, false);

	Rotate(pDC, -PI / 4, false);
	Scale(pDC, 0.4, 1, false);
	pDC->PlayMetaFile(this->tamno, okvirni);
	Scale(pDC, 2.5, 1, false);

	Rotate(pDC, PI/2, false);
	Scale(pDC, 0.4, 1, false);
	pDC->PlayMetaFile(this->tamno, okvirni);
	Scale(pDC, 2.5, 1, false);

	pDC->Ellipse(-10, -10, 10, 10);
	Rotate(pDC, PI / 4 - PI / 2, false); // normalan koordinatni sistem
	Translate(pDC, -2 * this->k, -2 * this->k, false);
	Scale(pDC, 0.67, 1, false);
	pDC->PlayMetaFile(this->tamno, okvirni);
	Scale(pDC, 1.5, 1, false);

	Rotate(pDC, -PI / 2, false);
	Scale(pDC, 0.67, 1, false);
	pDC->PlayMetaFile(this->tamno, okvirni);
	Scale(pDC, 1.5, 1, false);

	pDC->Ellipse(-10, -10, 10, 10);
	Rotate(pDC, PI / 2, false);
	Translate(pDC, 0, -3 * this->k, false);
	pDC->PlayMetaFile(this->tamno, okvirni);
	pDC->Ellipse(-10, -10, 10, 10);

	Translate(pDC, 4*this->k, 3 * this->k, false);
	Rotate(pDC, deoRotacija, false);
	Scale(pDC, 0.67, 1, false);
	pDC->PlayMetaFile(this->svetlo, okvirni);
	Scale(pDC, 1.5, 1, false);

	Rotate(pDC, PI/2 - deoRotacija , false);
	Scale(pDC, 0.67, 1, false);
	pDC->PlayMetaFile(this->tamno, okvirni);
	Scale(pDC, 1.5, 1, false);

	pDC->Ellipse(-10, -10, 10, 10);
	Rotate(pDC, -PI / 2, false);
	Translate(pDC, 3 * this->k, 0, false);
	Rotate(pDC, PI / 4, false);
	Scale(pDC, 0.67, 1, false);
	pDC->PlayMetaFile(this->tamno, okvirni);
	Scale(pDC, 1.5, 1, false);
	Rotate(pDC, PI/2, false);
	Scale(pDC, 0.67, 1, false);
	pDC->PlayMetaFile(this->tamno, okvirni);
	Scale(pDC, 1.5, 1, false);
	pDC->Ellipse(-10, -10, 10, 10);
	Rotate(pDC, -3 * PI / 4, false);

	pDC->SetWorldTransform(&XformOld); 
	pDC->SetGraphicsMode(prevMode);
}

// CLab2PripremaView drawing

void CLab2PripremaView::OnDraw(CDC* pDC)
{
	CLab2PripremaDoc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	if (!pDoc)
		return;

	CRect drawRect(0, 0, this->k*20, this->k * 20);
	pDC->IntersectClipRect(&drawRect);
	Okvir(pDC);

	CPen pozadinskaOlovka(PS_SOLID, 0, RGB(221, 221, 221));
	CPen kaktusOlovka(PS_SOLID, 1, RGB(0, 0, 0));

	CBrush pozadinskaCetka(RGB(135, 206, 235));
	CBrush kaktusCetka(RGB(0, 204, 0));


	pDC->SelectObject(kaktusOlovka);
	pDC->SelectObject(kaktusCetka);

	DrawFigure(pDC);
	Saksija(pDC);
	if (mreza_aktivna)
		Grid(pDC);
}


// CLab2PripremaView printing

BOOL CLab2PripremaView::OnPreparePrinting(CPrintInfo* pInfo)
{
	// default preparation
	return DoPreparePrinting(pInfo);
}

void CLab2PripremaView::OnBeginPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add extra initialization before printing
}

void CLab2PripremaView::OnEndPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add cleanup after printing
}


// CLab2PripremaView diagnostics

#ifdef _DEBUG
void CLab2PripremaView::AssertValid() const
{
	CView::AssertValid();
}

void CLab2PripremaView::Dump(CDumpContext& dc) const
{
	CView::Dump(dc);
}

CLab2PripremaDoc* CLab2PripremaView::GetDocument() const // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CLab2PripremaDoc)));
	return (CLab2PripremaDoc*)m_pDocument;
}
#endif //_DEBUG


// CLab2PripremaView message handlers


void CLab2PripremaView::OnKeyDown(UINT nChar, UINT nRepCnt, UINT nFlags)
{
	// TODO: Add your message handler code here and/or call default
	CDC* pDc = GetDC();
	if (nChar == VK_SPACE && mreza_aktivna == false) 
	{
		Grid(pDc);
	}
	else if (nChar == VK_SPACE && mreza_aktivna == true)
	{
		Invalidate();
		mreza_aktivna = false;
	}

	if (nChar == VK_LEFT) 
	{
		this->kaktusRotacija -= PI / 12;
		Invalidate();
		
	}
	else if (nChar == VK_RIGHT) 
	{
		this->kaktusRotacija += PI / 12;
		Invalidate();
		
	}
	else if (nChar == 'A')
	{
		this->deoRotacija -= PI / 12;
		Invalidate();
		
	}
	else if (nChar == 'D')
	{
		this->deoRotacija += PI / 12;
		
		Invalidate();
	}


	CView::OnKeyDown(nChar, nRepCnt, nFlags);
}
