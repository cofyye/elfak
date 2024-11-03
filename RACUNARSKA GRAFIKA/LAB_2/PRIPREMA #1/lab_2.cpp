
// IND_18627View.cpp : implementation of the CIND_18627View class
//

#include "pch.h"
#include "framework.h"
// SHARED_HANDLERS can be defined in an ATL project implementing preview, thumbnail
// and search filter handlers and allows sharing of document code with that project.
#ifndef SHARED_HANDLERS
#include "IND_18627.h"
#endif

#include "IND_18627Doc.h"
#include "IND_18627View.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#define TO_RAD(x) (x*3.14159/180) //za konverziju stepena u radijane
#endif


// CIND_18627View

IMPLEMENT_DYNCREATE(CIND_18627View, CView)

BEGIN_MESSAGE_MAP(CIND_18627View, CView)
	// Standard printing commands
	ON_COMMAND(ID_FILE_PRINT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, &CView::OnFilePrintPreview)
	ON_WM_KEYDOWN()
END_MESSAGE_MAP()

// CIND_18627View construction/destruction

CIND_18627View::CIND_18627View() noexcept
{
	// TODO: add construction code here
	showGrid = false;
	angle1 = 0;
	angle2 = 0;
}

CIND_18627View::~CIND_18627View()
{
}

//crtanje kaktusa
void CIND_18627View::DrawFigure(CDC* pDC)
{
	//uzimanje meta fajlova
	HENHMETAFILE lightPart = GetEnhMetaFile(_T("cactus_part_light.emf")); 
	HENHMETAFILE darkPart = GetEnhMetaFile(_T("cactus_part.emf"));

	if (lightPart == NULL || darkPart == NULL)  AfxMessageBox(_T("Greska pri ucitavanju emf datoteka."));
	
	//postavljam graficki mod na advanced
	int prevMode = pDC->SetGraphicsMode(GM_ADVANCED); //mod za koriscenje transformacija

	CactusPart(pDC, &lightPart, 250, 389, 60, 75, angle1, 0);
	CactusPart(pDC, &lightPart, 304, 262, 40, 75, angle1, 0, angle2);
	CactusPart(pDC, &darkPart, 198, 183, 60, 75, angle1, 0);
	CactusPart(pDC, &darkPart, 198, 259, 40, 75, angle1, 0);
	CactusPart(pDC, &darkPart, 275, 325, 20, 75, angle1, 45);
	CactusPart(pDC, &darkPart, 250, 312, 20, 75, angle1, 0);
	CactusPart(pDC, &darkPart, 225, 325, 20, 75, angle1, -45);
	CactusPart(pDC, &darkPart, 163, 300, 40, 75, angle1, -90);
	CactusPart(pDC, &darkPart, 344, 300, 40, 75, angle1, 90);
	CactusPart(pDC, &darkPart, 408, 271, 40, 75, angle1, 45);
	CactusPart(pDC, &darkPart, 408, 324, 40, 75, angle1, 135);

	RootPart(pDC, 250, 350, angle1);
	RootPart(pDC, 200, 300, angle1);
	RootPart(pDC, 199, 223, angle1);
	RootPart(pDC, 305, 300, angle1);
	RootPart(pDC, 380, 300, angle1);
	RootPart(pDC, 250, 425, angle1);

	pDC->SetGraphicsMode(prevMode);
}

void CIND_18627View::DrawPot(CDC* pDC)
{
	POINT points[8] = {
		CPoint(191, 432), 
		CPoint(312, 432),
		CPoint(312, 450),
		CPoint(302, 450), 
		CPoint(292, 499),
		CPoint(211, 499),
		CPoint(201, 450),
		CPoint(191, 450)
	};

	CBrush *colorPot = new CBrush(RGB(222, 148, 0));
	CPen* penPot = new CPen(PS_SOLID, 1, RGB(144, 125, 58));

	CBrush* oldBrush = pDC->SelectObject(colorPot);
	CPen* oldPen = pDC->SelectObject(penPot);

	pDC->Polygon(points, 8);

	pDC->SelectObject(oldBrush);
	pDC->SelectObject(oldPen);

	colorPot->DeleteObject();
	penPot->DeleteObject();
}

void CIND_18627View::CactusPart(CDC* pDC, HENHMETAFILE* texture, int x, int y, int width, int height, int angleCenterRot, int angleSelfRot, int angle)
{
	XFORM xFormOld;

	//cuvam trenutnu transformaciju kako bi je kasnije vratila
	pDC->GetWorldTransform(&xFormOld);

	//radim bitsko pomeranje za jednu poziciju da bi se segment vrteo oko donje tacke a ne centra
	width >>= 1; 
	height >>= 1;

	//rotiram deo kaktusa oko sopstvene ose za ugao selfrot
	Rotate(pDC, TO_RAD(angleSelfRot), true);

	//ako imamo dodatnu rotaciju
	if (angle != 0) {
		Translate(pDC, 0, -height, true); //transliram segmet navise za -visinu pomerajuci koord sistem tako da rotacija utice na gornji deo 
		Rotate(pDC, TO_RAD(angle), true); //rotacija
		Translate(pDC, 0, height, true); //vracanje na pocetnu poziciju
	}

	Translate(pDC, x - 250, y - 425, true); // translira koord sistem do pozicije x,y gde ce deo da bude nacrtan
	Rotate(pDC, TO_RAD(angleCenterRot), true); //rotacija oko centra kaktusa
	Translate(pDC, 250, 425, true); //povratak na pocetak

	//iscrtavanje teksture segmenta
	pDC->PlayMetaFile(*texture, CRect(-width, -height, width, height));
	pDC->SetWorldTransform(&xFormOld); //vracamo stanje prethodno sacuvano
}

void CIND_18627View::RootPart(CDC* pDC, int x, int y, int angleCenterRot)
{
	XFORM xFormOld;
	pDC->GetWorldTransform(&xFormOld);

	CBrush brush(RGB(0, 204, 0));
	CBrush* oldBrush = pDC->SelectObject(&brush);
	CPen pen(PS_SOLID, 1, RGB(0, 0, 0));
	CPen* oldPen = pDC->SelectObject(&pen);

	Translate(pDC, x - 250, y - 425, true);
	Rotate(pDC, TO_RAD(angleCenterRot), true);
	Translate(pDC, 250, 425, true);
	pDC->Ellipse(-10, -10, 10, 10);

	pDC->SelectObject(oldBrush);
	pDC->SelectObject(oldPen);

	pDC->SetWorldTransform(&xFormOld);
}

BOOL CIND_18627View::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	return CView::PreCreateWindow(cs);
}

// CIND_18627View drawing

void CIND_18627View::OnDraw(CDC* pDC)
{
	CIND18627Doc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	if (!pDoc)
		return;

	// TODO: add draw code for native data here

	CBrush *backgroundBrush = new CBrush(RGB(135, 206, 235));
	CPen *gridPen = new CPen(PS_SOLID, 1, RGB(210, 237, 247));
	CPen *startPen = new CPen(PS_SOLID, 1, RGB(255, 255, 255));

	CBrush *oldBrush = pDC->SelectObject(backgroundBrush);
	CPen *oldPen = pDC->SelectObject(startPen);

	pDC->Rectangle(0, 0, 500, 500); // grid 500, 500


	DrawFigure(pDC); //pozivam fju za crtanje kaktusa
	DrawPot(pDC); //za crtanje saksije

	//za grid deo
	if (showGrid) {
		pDC->SelectObject(gridPen);
		
		for (int i = 25; i < 500; i+=25)
		{
			pDC->MoveTo(0, i);
			pDC->LineTo(500, i);

			pDC->MoveTo(i, 0);
			pDC->LineTo(i, 500);
		}
	}

	pDC->SelectObject(oldBrush);
	pDC->SelectObject(oldPen);

	gridPen->DeleteObject();
	startPen->DeleteObject();
	backgroundBrush->DeleteObject();
}

void CIND_18627View::Translate(CDC* pDC, float dX, float dY, bool rightMultiply)
{
	XFORM xForm{};
	xForm.eM11 = 1.0;
	xForm.eM12 = 0.0;
	xForm.eM21 = 0.0;
	xForm.eM22 = 1.0;
	xForm.eDx = dX;
	xForm.eDy = dY;

	pDC->ModifyWorldTransform(&xForm, rightMultiply ? MWT_RIGHTMULTIPLY : MWT_LEFTMULTIPLY);
}

void CIND_18627View::Scale(CDC* pDC, float sX, float sY, bool rightMultiply)
{

	XFORM xForm{};
	xForm.eM11 = sX;
	xForm.eM12 = 0.0;
	xForm.eM21 = 0.0;
	xForm.eM22 = sY;
	xForm.eDx = 0.0;
	xForm.eDy = 0.0;

	pDC->ModifyWorldTransform(&xForm, rightMultiply ? MWT_RIGHTMULTIPLY : MWT_LEFTMULTIPLY);
}

void CIND_18627View::Rotate(CDC* pDC, float angle, bool rightMultiply)
{
	XFORM xForm{};
	xForm.eM11 = cos(angle);
	xForm.eM12 = sin(angle);
	xForm.eM21 = -sin(angle);
	xForm.eM22 = cos(angle);
	xForm.eDx = 0.0;
	xForm.eDy = 0.0;

	pDC->ModifyWorldTransform(&xForm, rightMultiply ? MWT_RIGHTMULTIPLY : MWT_LEFTMULTIPLY);
}


// CIND_18627View printing

BOOL CIND_18627View::OnPreparePrinting(CPrintInfo* pInfo)
{
	// default preparation
	return DoPreparePrinting(pInfo);
}

void CIND_18627View::OnBeginPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add extra initialization before printing
}

void CIND_18627View::OnEndPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add cleanup after printing
}


// CIND_18627View diagnostics

#ifdef _DEBUG
void CIND_18627View::AssertValid() const
{
	CView::AssertValid();
}

void CIND_18627View::Dump(CDumpContext& dc) const
{
	CView::Dump(dc);
}

void CIND_18627View::OnKeyDown(UINT nChar, UINT nRepCnt, UINT nFlags)
{
	CView::OnKeyDown(nChar, nRepCnt, nFlags);
	switch (nChar)
	{
	case VK_SPACE:
		showGrid = !showGrid; break;
	case 'A':
		angle1 -= 6;
		break;
	case 'D':
		angle1 += 6;
		break;
	case VK_LEFT:  
		angle2 -= 6;
		break;
	case VK_RIGHT: 
		angle2 += 6;
		break;
	default:
		return;
	}

	Invalidate();
}

CIND18627Doc* CIND_18627View::GetDocument() const // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CIND18627Doc)));
	return (CIND18627Doc*)m_pDocument;
}
#endif //_DEBUG


// CIND_18627View message handlers
