
// LampView.cpp : implementation of the CLampView class
//

#include "pch.h"
#include "framework.h"
#include "DImage.h"
// SHARED_HANDLERS can be defined in an ATL project implementing preview, thumbnail
// and search filter handlers and allows sharing of document code with that project.
#ifndef SHARED_HANDLERS
#include "Lamp.h"
#endif

#include "LampDoc.h"
#include "LampView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

const float PI = 3.14159f;
#define TO_RAD(x) { x * PI / 180 }

const int POMERAJ_LAMPE_X = 0;
const int POMERAJ_LAMPE_Y = 0;
const int KOORDINATNI_POCETAK_X = 950;
const int KOORDINATNI_POCETAK_Y = 820;

const int ARM1_VECI_KRUG_X = 58;
const int ARM1_VECI_KRUG_Y = 61;
const int ARM1_MANJI_KRUG_X = 309;
const int ARM1_MANJI_KRUG_Y = 61;
const int ARM1_POMERAJ_X = 100 + POMERAJ_LAMPE_X;
const int ARM1_POMERAJ_Y = - 50 + POMERAJ_LAMPE_Y;

const int ARM2_VECI_KRUG_X = 36;
const int ARM2_VECI_KRUG_Y = 40;
const int ARM2_MANJI_KRUG_X = 272;
const int ARM2_MANJI_KRUG_Y = 40;
const int ARM2_POMERAJ_X = ARM1_MANJI_KRUG_X - ARM2_VECI_KRUG_X + ARM1_POMERAJ_X;
const int ARM2_POMERAJ_Y = ARM1_MANJI_KRUG_Y - ARM2_VECI_KRUG_Y + ARM1_POMERAJ_Y;

const int HEAD_KRUG_X = 178;
const int HEAD_KRUG_Y = 100;
const int HEAD_POMERAJ_X = ARM2_MANJI_KRUG_X - HEAD_KRUG_X + ARM2_POMERAJ_X;
const int HEAD_POMERAJ_Y = ARM2_MANJI_KRUG_Y - HEAD_KRUG_Y + ARM2_POMERAJ_Y;


// CLampView

IMPLEMENT_DYNCREATE(CLampView, CView)

BEGIN_MESSAGE_MAP(CLampView, CView)
	// Standard printing commands
	ON_COMMAND(ID_FILE_PRINT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, &CView::OnFilePrintPreview)
	ON_WM_KEYDOWN()
	ON_WM_ERASEBKGND()
END_MESSAGE_MAP()

CLampView::CLampView() noexcept
{
	base = new DImage();
	arm1 = new DImage();
	arm2 = new DImage();
	head = new DImage();
	pozadina = new DImage();
	baseShadow = new DImage();
	arm1Shadow = new DImage();
	arm2Shadow = new DImage();
	headShadow = new DImage();

	base->Load(CString("./Slike/base.png"));
	arm1->Load(CString("./Slike/arm1.png"));
	arm2->Load(CString("./Slike/arm2.png"));
	head->Load(CString("./Slike/head.png"));
	pozadina->Load(CString("./Slike/pozadina.jpg"));
	baseShadow->Load(CString("./Slike/base_shadow.png"));
	arm1Shadow->Load(CString("./Slike/arm1_shadow.png"));
	arm2Shadow->Load(CString("./Slike/arm2_shadow.png"));
	headShadow->Load(CString("./Slike/head_shadow.png"));
}

CLampView::~CLampView()
{
	delete base, arm1, arm2, head, pozadina, baseShadow, arm1Shadow, arm2Shadow, headShadow;
}

BOOL CLampView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	return CView::PreCreateWindow(cs);
}

#pragma region Funkcije Transformacija

void CLampView::Translate(CDC* pDC, float dX, float dY, bool rightMultiply)
{
	XFORM xform;
	xform.eM11 = 1.0;
	xform.eM12 = 0.0;
	xform.eM21 = 0.0;
	xform.eM22 = 1.0;
	xform.eDx = dX;
	xform.eDy = dY;

	pDC->ModifyWorldTransform(&xform, rightMultiply ? MWT_RIGHTMULTIPLY : MWT_LEFTMULTIPLY);
}

void CLampView::Rotate(CDC* pDC, float angle, bool rightMultiply)
{
	angle = TO_RAD(angle);

	XFORM xform;
	xform.eM11 = cos(angle);
	xform.eM12 = sin(angle);
	xform.eM21 = -sin(angle);
	xform.eM22 = cos(angle);
	xform.eDx = 0.0;
	xform.eDy = 0.0;

	pDC->ModifyWorldTransform(&xform, rightMultiply ? MWT_RIGHTMULTIPLY : MWT_LEFTMULTIPLY);
}

void CLampView::Scale(CDC* pDC, float sX, float sY, bool rightMultiply)
{
	XFORM xform{};
	xform.eM11 = sX;
	xform.eM12 = 0.0;
	xform.eM21 = 0.0;
	xform.eM22 = sY;
	xform.eDx = 0.0;
	xform.eDy = 0.0;

	pDC->ModifyWorldTransform(&xform, rightMultiply ? MWT_RIGHTMULTIPLY : MWT_LEFTMULTIPLY);
}

#pragma endregion

#pragma region Pomocne Funkcije
void CLampView::DrawBackground(CDC* pDC)
{
	CRect clientRect;
	GetClientRect(&clientRect);

	int pozadinaW = pozadina->Width();
	int pozadinaH = pozadina->Height();
	
	int clientW = clientRect.Width();
	int clientH = clientRect.Height();

	int topLeftX = (clientW - pozadinaW) / 2;
	int topLeftY = clientH - pozadinaH;
	int bottomRightX = topLeftX + pozadinaW;
	int bottomRightY = topLeftY + pozadinaH;

	pozadina->Draw(pDC, CRect(0, 0, pozadinaW, pozadinaH), CRect(topLeftX, topLeftY, bottomRightX, bottomRightY));
}

void CLampView::DrawImgTransparent(CDC* pDC, DImage* pImage)
{
	int w = pImage->Width();
	int h = pImage->Height();

	CBitmap bmpImage, bmpMask;
	bmpImage.CreateCompatibleBitmap(pDC, w, h);
	bmpMask.CreateBitmap(w, h, 1, 1, NULL);

	CDC* pSrcDC = new CDC();
	pSrcDC->CreateCompatibleDC(pDC);
	pSrcDC->SelectObject(bmpImage);

	CDC* pMaskDC = new CDC();
	pMaskDC->CreateCompatibleDC(pDC);
	pMaskDC->SelectObject(bmpMask);

	pImage->Draw(
		pSrcDC,
		CRect(0, 0, w, h),
		CRect(0, 0, w, h)
	);

	COLORREF pozadina = pSrcDC->GetPixel(0, 0);
	pSrcDC->SetBkColor(pozadina);
	pMaskDC->BitBlt(0, 0, w, h, pSrcDC, 0, 0, SRCCOPY);

	pSrcDC->SetBkColor(RGB(0, 0, 0));
	pSrcDC->SetTextColor(RGB(255, 255, 255));
	pSrcDC->BitBlt(0, 0, w, h, pMaskDC, 0, 0, SRCAND);

	pDC->BitBlt(0, 0, w, h, pMaskDC, 0, 0, SRCAND);
	pDC->BitBlt(0, 0, w, h, pSrcDC, 0, 0, SRCPAINT);

	delete pSrcDC, pMaskDC;
}

#pragma endregion

#pragma region Crtanje Lampe

void CLampView::DrawLampBase(CDC* pDC, bool blsShadow)
{
	XFORM oldTrans;
	pDC->GetWorldTransform(&oldTrans);

	/*Translate(pDC, POMERAJ_LAMPE_X, POMERAJ_LAMPE_Y);*/

	// Racunanje pomeraja na osnovu velicine prozora, jer se pozadina pomera na osnovu velicine prozora
	CRect clientRect;
	GetClientRect(&clientRect);
	int clientW = clientRect.Width();
	int clientH = clientRect.Height();
	int pozadinaW = pozadina->Width();
	int pozadinaH = pozadina->Height();

	int pozadinaX = (clientW - pozadinaW) / 2;
	int pozadinaY = clientH - pozadinaH;

	// Postavljanje koordinatnog pocetka onako kako pise u zadatku, ne znam da li se radi ovako.
	// Moze da se radi sa translate i tada kada pozicioniramo ostale delove lampe moramo da ih pomerimo za ovaj POMERAJ_LAMPE
	// Kada pomeramo koordinatni pocetak onda ne moramo da uracunamo POMERAJ_LAMPE jer se koordinatni pocetak pomerio i ja sam samo stavio da je POMERAJ_LAMPE = 0
	pDC->SetViewportOrg(pozadinaX + KOORDINATNI_POCETAK_X, pozadinaY + KOORDINATNI_POCETAK_Y);
	if (blsShadow)
		DrawImgTransparent(pDC, baseShadow);
	else
		DrawImgTransparent(pDC, base);
	
	pDC->SetWorldTransform(&oldTrans);
}

void CLampView::DrawLampArm1(CDC* pDC, bool blsShadow)
{
	XFORM oldForm;
	pDC->GetWorldTransform(&oldForm);

	// Rotacija oko veceg kruga
	Translate(pDC, -ARM1_VECI_KRUG_X, -ARM1_VECI_KRUG_Y);
	Rotate(pDC, arm1Angle);
	Translate(pDC, ARM1_VECI_KRUG_X, ARM1_VECI_KRUG_Y);
	// Pomeraj figure
	Translate(pDC, ARM1_POMERAJ_X, ARM1_POMERAJ_Y);

	if (blsShadow)
	{
		Translate(pDC, -ARM1_VECI_KRUG_X - ARM1_POMERAJ_X, -ARM1_VECI_KRUG_Y - ARM1_POMERAJ_Y);
		Scale(pDC, 0.25, 1);
		Rotate(pDC, -90);
		Translate(pDC, ARM1_VECI_KRUG_X + ARM1_POMERAJ_X - 130, ARM1_VECI_KRUG_Y + ARM1_POMERAJ_Y + 30);
	}

	if (blsShadow)
		DrawImgTransparent(pDC, arm1Shadow);
	else
		DrawImgTransparent(pDC, arm1);

	pDC->SetWorldTransform(&oldForm);
}

void CLampView::DrawLampArm2(CDC* pDC, bool blsShadow)
{
	XFORM oldForm;
	pDC->GetWorldTransform(&oldForm);

	// Rotacija oko veceg kruga
	Translate(pDC, -ARM2_VECI_KRUG_X, -ARM2_VECI_KRUG_Y);
	Rotate(pDC, arm2Angle);
	Translate(pDC, ARM2_VECI_KRUG_X, ARM2_VECI_KRUG_Y);
	// Pomeraj
	Translate(pDC, ARM2_POMERAJ_X, ARM2_POMERAJ_Y);

	// Rotacija oko arm1
	Translate(pDC, -ARM1_VECI_KRUG_X - ARM1_POMERAJ_X, -ARM1_VECI_KRUG_Y - ARM1_POMERAJ_Y);
	Rotate(pDC, arm1Angle);
	Translate(pDC, ARM1_VECI_KRUG_X + ARM1_POMERAJ_X, ARM1_VECI_KRUG_Y + ARM1_POMERAJ_Y);

	if (blsShadow)
	{
		Translate(pDC, -ARM1_VECI_KRUG_X - ARM1_POMERAJ_X, -ARM1_VECI_KRUG_Y - ARM1_POMERAJ_Y);
	Scale(pDC, 0.25, 1);
		Rotate(pDC, -90);
		Translate(pDC, ARM1_VECI_KRUG_X + ARM1_POMERAJ_X - 130, ARM1_VECI_KRUG_Y + ARM1_POMERAJ_Y + 30);
	}

	if (blsShadow)
		DrawImgTransparent(pDC, arm2Shadow);
	else
		DrawImgTransparent(pDC, arm2);

	pDC->SetWorldTransform(&oldForm);
	pDC->SetViewportOrg(0, 0);
}

void CLampView::DrawLampHead(CDC* pDC, bool blsShadow)
{
	XFORM oldForm;
	pDC->GetWorldTransform(&oldForm);

	// Rotacija oko head tacke
	Translate(pDC, -HEAD_KRUG_X, -HEAD_KRUG_Y);
	Rotate(pDC, headAngle);
	Translate(pDC, HEAD_KRUG_X, HEAD_KRUG_Y);
	// Pomeraj
	Translate(pDC, HEAD_POMERAJ_X, HEAD_POMERAJ_Y);

	// Rotacija oko arm2
	Translate(pDC, -ARM2_VECI_KRUG_X - ARM2_POMERAJ_X, -ARM2_VECI_KRUG_Y - ARM2_POMERAJ_Y);
	Rotate(pDC, arm2Angle);
	Translate(pDC, ARM2_VECI_KRUG_X + ARM2_POMERAJ_X, ARM2_VECI_KRUG_Y + ARM2_POMERAJ_Y);

	// Rotacija oko arm1
	Translate(pDC, -ARM1_VECI_KRUG_X - ARM1_POMERAJ_X, -ARM1_VECI_KRUG_Y - ARM1_POMERAJ_Y);
	Rotate(pDC, arm1Angle);
	Translate(pDC, ARM1_VECI_KRUG_X + ARM1_POMERAJ_X, ARM1_VECI_KRUG_Y + ARM1_POMERAJ_Y);

	// Rotacija oko veceg kruga arm1
	if (blsShadow)
	{
		Translate(pDC, -ARM1_VECI_KRUG_X - ARM1_POMERAJ_X, -ARM1_VECI_KRUG_Y - ARM1_POMERAJ_Y);
		Scale(pDC, 0.25, 1);
		Rotate(pDC, -90);
		Translate(pDC, ARM1_VECI_KRUG_X + ARM1_POMERAJ_X - 130, ARM1_VECI_KRUG_Y + ARM1_POMERAJ_Y + 30);
	}

	if (blsShadow)
		DrawImgTransparent(pDC, headShadow);
	else
		DrawImgTransparent(pDC, head);

	pDC->SetWorldTransform(&oldForm);
}

void CLampView::DrawLamp(CDC* pDC, bool blsShadow)
{
	DrawLampBase(pDC);
	DrawLampArm1(pDC, blsShadow);
	DrawLampHead(pDC, blsShadow);
	DrawLampArm2(pDC, blsShadow);
}

void CLampView::DrawLampShadow(CDC* pDC)
{
	XFORM oldTrans;
	pDC->GetWorldTransform(&oldTrans);

	DrawLamp(pDC, true);

	pDC->SetWorldTransform(&oldTrans);
}

#pragma endregion

void CLampView::OnDraw(CDC* pDC)
{
	CLampDoc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	if (!pDoc)
		return;

	CRect clientRect;
	GetClientRect(&clientRect);
	CDC* pMemDC = new CDC();
	pMemDC->CreateCompatibleDC(pDC);

	CBitmap bmpMem;
	bmpMem.CreateCompatibleBitmap(pDC, clientRect.Width(), clientRect.Height());
	CBitmap* bmpOld = pMemDC->SelectObject(&bmpMem);
	int oldMode = pMemDC->SetGraphicsMode(GM_ADVANCED);
	pMemDC->FillSolidRect(clientRect, pDC->GetBkColor());

	DrawBackground(pMemDC);
	DrawLampShadow(pMemDC);
	DrawLamp(pMemDC);

	pDC->BitBlt(0, 0, clientRect.Width(), clientRect.Height(), pMemDC, 0, 0, SRCCOPY);

	pMemDC->SelectObject(bmpOld);
	pMemDC->SetGraphicsMode(oldMode);
	delete pMemDC;
}


// CLampView printing

BOOL CLampView::OnPreparePrinting(CPrintInfo* pInfo)
{
	// default preparation
	return DoPreparePrinting(pInfo);
}

void CLampView::OnBeginPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add extra initialization before printing
}

void CLampView::OnEndPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add cleanup after printing
}


// CLampView diagnostics

#ifdef _DEBUG
void CLampView::AssertValid() const
{
	CView::AssertValid();
}

void CLampView::Dump(CDumpContext& dc) const
{
	CView::Dump(dc);
}

CLampDoc* CLampView::GetDocument() const // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CLampDoc)));
	return (CLampDoc*)m_pDocument;
}
#endif //_DEBUG


// CLampView message handlers


void CLampView::OnKeyDown(UINT nChar, UINT nRepCnt, UINT nFlags)
{
	CView::OnKeyDown(nChar, nRepCnt, nFlags);

	switch (nChar)
	{
	case '1':
		arm1Angle -= 10;
		break;
	case '2':
		arm1Angle += 10;
		break;
	case '3':
		arm2Angle -= 10;
		break;
	case '4':
		arm2Angle += 10;
		break;
	case '5':
		headAngle -= 10;
		break;
	case '6':
		headAngle += 10;
		break;
	default:
		break;
	}
	Invalidate();
}


BOOL CLampView::OnEraseBkgnd(CDC* pDC)
{
	return 1;
}
