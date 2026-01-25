
// BagerView.cpp : implementation of the CBagerView class
//

#include "pch.h"
#include "framework.h"
// SHARED_HANDLERS can be defined in an ATL project implementing preview, thumbnail
// and search filter handlers and allows sharing of document code with that project.
#ifndef SHARED_HANDLERS
#include "Bager.h"
#endif

#include "BagerDoc.h"
#include "BagerView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

const float PI = 3.14159f;
#define TO_RAD(x) { x * PI / 180 };

const int ARM1_VECI_KRUG_X = 58;
const int ARM1_VECI_KRUG_Y = 61;
const int ARM1_MANJI_KRUG_X = 309;
const int ARM1_MANJI_KRUG_Y = 61;
const int ARM1_POMERAJ_X = 5;
const int ARM1_POMERAJ_Y = 105;

const int ARM2_VECI_KRUG_X = 36;
const int ARM2_VECI_KRUG_Y = 40;
const int ARM2_MANJI_KRUG_X = 272;
const int ARM2_MANJI_KRUG_Y = 40;
const int ARM2_POMERAJ_X = ARM1_MANJI_KRUG_X - ARM2_VECI_KRUG_X + ARM1_POMERAJ_X;
const int ARM2_POMERAJ_Y = ARM1_MANJI_KRUG_Y - ARM2_VECI_KRUG_Y + ARM1_POMERAJ_Y;

const int FORK_ROTATE_X = 14;
const int FORK_ROTATE_Y = 20;
const int FORK_POMERAJ_X = ARM2_MANJI_KRUG_X - FORK_ROTATE_X * 2.5 + ARM2_POMERAJ_X;
const int FORK_POMERAJ_Y = ARM2_MANJI_KRUG_Y - FORK_ROTATE_Y * 2.5 + ARM2_POMERAJ_Y;


// CBagerView

IMPLEMENT_DYNCREATE(CBagerView, CView)

BEGIN_MESSAGE_MAP(CBagerView, CView)
	// Standard printing commands
	ON_COMMAND(ID_FILE_PRINT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, &CView::OnFilePrintPreview)
	ON_WM_ERASEBKGND()
	ON_WM_KEYDOWN()
END_MESSAGE_MAP()


CBagerView::CBagerView() noexcept
{
	bager = new DImage();
	arm1 = new DImage();
	arm2 = new DImage();
	pozadina = new DImage();

	bager->Load(CString("./Slike/bager.png"));
	arm1->Load(CString("./Slike/arm1.png"));
	arm2->Load(CString("./Slike/arm2.png"));
	pozadina->Load(CString("./Slike/pozadina.png"));

	fork = GetEnhMetaFile(CString("./Slike/viljuska.emf"));
	GetEnhMetaFileHeader(fork, sizeof(ENHMETAHEADER), &forkHeader);
}

CBagerView::~CBagerView()
{
	DeleteEnhMetaFile(fork);
	delete bager, arm1, arm2, pozadina;
}

BOOL CBagerView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	return CView::PreCreateWindow(cs);
}

// CBagerView drawing

#pragma region Funkcije Transformacije
void CBagerView::Translate(CDC* pDC, float dX, float dY, bool rightMultiply)
{
	XFORM xform;
	xform.eM11 = 1;
	xform.eM12 = 0;
	xform.eM21 = 0;
	xform.eM22 = 1;
	xform.eDx = dX;
	xform.eDy = dY;

	pDC->ModifyWorldTransform(&xform, rightMultiply ? MWT_RIGHTMULTIPLY : MWT_LEFTMULTIPLY);
}

void CBagerView::Rotate(CDC* pDC, float angle, bool rightMultiply)
{
	angle = TO_RAD(angle);

	XFORM xform;
	xform.eM11 = cos(angle);
	xform.eM12 = sin(angle);
	xform.eM21 = -sin(angle);
	xform.eM22 = cos(angle);
	xform.eDx = 0;
	xform.eDy = 0;

	pDC->ModifyWorldTransform(&xform, rightMultiply ? MWT_RIGHTMULTIPLY : MWT_LEFTMULTIPLY);
}

void CBagerView::Scale(CDC* pDC, float sX, float sY, bool rightMultiply)
{
	XFORM xform;
	xform.eM11 = sX;
	xform.eM12 = 0;
	xform.eM21 = 0;
	xform.eM22 = sY;
	xform.eDx = 0;
	xform.eDy = 0;

	pDC->ModifyWorldTransform(&xform, rightMultiply ? MWT_RIGHTMULTIPLY : MWT_LEFTMULTIPLY);
}

#pragma endregion

#pragma region Pomocne Funkcije

void CBagerView::DrawBackground(CDC* pDC)
{
	int pozadinaW = pozadina->Width();
	int pozadinaH = pozadina->Height();
	CRect clientRect;
	GetClientRect(&clientRect);
	int clientW = clientRect.Width();
	int clientH = clientRect.Height();

	int topX = (clientW - pozadinaW) / 2;
	int topY = (clientH - pozadinaH);
	int bottomX = topX + pozadinaW;
	int bottomY = topY + pozadinaH;

	pozadina->Draw(
		pDC,
		CRect(0, 0, pozadinaW, pozadinaH),
		CRect(topX, topY, bottomX, bottomY)
	);
}

void CBagerView::DrawImgTransparent(CDC* pDC, DImage* pImage)
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

	pSrcDC->SetBkColor(pSrcDC->GetPixel(0, 0));
	pMaskDC->BitBlt(0, 0, w, h, pSrcDC, 0, 0, SRCCOPY);

	pSrcDC->SetTextColor(RGB(255, 255, 255));
	pSrcDC->SetBkColor(RGB(0, 0, 0));
	pSrcDC->BitBlt(0, 0, w, h, pMaskDC, 0, 0, SRCAND);

	pDC->BitBlt(0, 0, w, h, pMaskDC, 0, 0, SRCAND);
	pDC->BitBlt(0, 0, w, h, pSrcDC, 0, 0, SRCPAINT);

	pSrcDC->DeleteDC();
	pMaskDC->DeleteDC();
	delete pSrcDC, pMaskDC;
}

#pragma endregion

#pragma region Crtanje Bagera

void CBagerView::DrawBody(CDC* pDC)
{
	int pozadinaW = pozadina->Width();
	int pozadinaH = pozadina->Height();
	CRect clientRect;
	GetClientRect(&clientRect);
	int clientW = clientRect.Width();
	int clientH = clientRect.Height();
	int bodyW = bager->Width();
	int bodyH = bager->Height();

	int x = ((clientW - pozadinaW) / 2) + pozadinaW - bodyW + kretanjeBagera;
	int y = clientH - bodyH;

	pDC->SetViewportOrg(x, y);

	DrawImgTransparent(pDC, bager);
}

void CBagerView::DrawArm1(CDC* pDC)
{
	XFORM oldForm;
	pDC->GetWorldTransform(&oldForm);

	// Rotacija oko arm1 ose
	Translate(pDC, -ARM1_VECI_KRUG_X, -ARM1_VECI_KRUG_Y);
	Rotate(pDC, arm1Angle);
	Translate(pDC, ARM1_VECI_KRUG_X, ARM1_VECI_KRUG_Y);
	// Pomeraj
	Translate(pDC, ARM1_POMERAJ_X, ARM1_POMERAJ_Y);

	DrawImgTransparent(pDC, arm1);

	pDC->SetWorldTransform(&oldForm);
}

void CBagerView::DrawArm2(CDC* pDC)
{
	XFORM oldForm;
	pDC->GetWorldTransform(&oldForm);

	// Rotacija oko arm2 ose
	Translate(pDC, -ARM2_VECI_KRUG_X, -ARM2_VECI_KRUG_Y);
	Rotate(pDC, arm2Angle);
	Translate(pDC, ARM2_VECI_KRUG_X, ARM2_VECI_KRUG_Y);
	// Pomeraj
	Translate(pDC, ARM2_POMERAJ_X, ARM2_POMERAJ_Y);

	// Rotacija oko arm1
	Translate(pDC, -ARM1_VECI_KRUG_X - ARM1_POMERAJ_X, -ARM1_VECI_KRUG_Y - ARM1_POMERAJ_Y);
	Rotate(pDC, arm1Angle);
	Translate(pDC, ARM1_VECI_KRUG_X + ARM1_POMERAJ_X, ARM1_VECI_KRUG_Y + ARM1_POMERAJ_Y);

	DrawImgTransparent(pDC, arm2);

	pDC->SetWorldTransform(&oldForm);
}

void CBagerView::DrawFork(CDC* pDC)
{
	XFORM oldForm;
	pDC->GetWorldTransform(&oldForm);

	int w = forkHeader.rclBounds.right - forkHeader.rclBounds.left;
	int h = forkHeader.rclBounds.bottom - forkHeader.rclBounds.top;

	// Rotacija oko fork ose
	Translate(pDC, -FORK_ROTATE_X, -FORK_ROTATE_Y);
	Rotate(pDC, forkAngle);
	Translate(pDC, FORK_ROTATE_X, FORK_ROTATE_Y);

	// Pomeraj (scale mora ovde nemam pojma sto)
	// Ako se izbaci scale vidi se da je fork dobro pomeren, odnosno, da se nalazi tacno tu gde treba.
	// Medjutim, kada se skalira poremete se koordinate i onda mora ova tacka (14, 20) sto je data u zadatku da se pomnozi sa 2.5 da bi se fork lepo namestio
	Scale(pDC, 2.5, 2.5);
	Translate(pDC, FORK_POMERAJ_X, FORK_POMERAJ_Y);

	// Rotacija oko arm1
	Translate(pDC, -ARM2_VECI_KRUG_X - ARM2_POMERAJ_X, -ARM2_VECI_KRUG_Y - ARM2_POMERAJ_Y);
	Rotate(pDC, arm2Angle);
	Translate(pDC, ARM2_VECI_KRUG_X + ARM2_POMERAJ_X, ARM2_VECI_KRUG_Y + ARM2_POMERAJ_Y);

	// Rotacija oko arm1
	Translate(pDC, -ARM1_VECI_KRUG_X - ARM1_POMERAJ_X, -ARM1_VECI_KRUG_Y - ARM1_POMERAJ_Y);
	Rotate(pDC, arm1Angle);
	Translate(pDC, ARM1_VECI_KRUG_X + ARM1_POMERAJ_X, ARM1_VECI_KRUG_Y + ARM1_POMERAJ_Y);

	pDC->PlayMetaFile(fork, CRect(0, 0, w, h));

	pDC->SetWorldTransform(&oldForm);
	pDC->SetViewportOrg(0, 0);
}

void CBagerView::DrawExcavator(CDC* pDC)
{
	DrawBody(pDC);
	DrawArm1(pDC);
	DrawArm2(pDC);
	DrawFork(pDC);
}

#pragma endregion

void CBagerView::OnDraw(CDC* pDC)
{
	CBagerDoc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	if (!pDoc)
		return;

	CRect clientRect;
	GetClientRect(&clientRect);

	CDC* pMemDC = new CDC();
	pMemDC->CreateCompatibleDC(pDC);

	CBitmap memBitmap;
	memBitmap.CreateCompatibleBitmap(pDC, clientRect.Width(), clientRect.Height());
	CBitmap* oldBitmap = pMemDC->SelectObject(&memBitmap);
	int oldMode = pMemDC->SetGraphicsMode(GM_ADVANCED);
	pMemDC->FillSolidRect(clientRect, pDC->GetBkColor());

	DrawBackground(pMemDC);
	DrawExcavator(pMemDC);

	pDC->BitBlt(0, 0, clientRect.Width(), clientRect.Height(), pMemDC, 0, 0, SRCCOPY);

	pMemDC->SelectObject(oldBitmap);
	pMemDC->SetGraphicsMode(oldMode);
	pMemDC->DeleteDC();
	delete pMemDC;
}


// CBagerView printing

BOOL CBagerView::OnPreparePrinting(CPrintInfo* pInfo)
{
	// default preparation
	return DoPreparePrinting(pInfo);
}

void CBagerView::OnBeginPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add extra initialization before printing
}

void CBagerView::OnEndPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add cleanup after printing
}


// CBagerView diagnostics

#ifdef _DEBUG
void CBagerView::AssertValid() const
{
	CView::AssertValid();
}

void CBagerView::Dump(CDumpContext& dc) const
{
	CView::Dump(dc);
}

CBagerDoc* CBagerView::GetDocument() const // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CBagerDoc)));
	return (CBagerDoc*)m_pDocument;
}
#endif //_DEBUG


// CBagerView message handlers


BOOL CBagerView::OnEraseBkgnd(CDC* pDC)
{
	return 1;
}


void CBagerView::OnKeyDown(UINT nChar, UINT nRepCnt, UINT nFlags)
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
		forkAngle -= 10;
		break;
	case '6':
		forkAngle += 10;
		break;
	case 'A':
	case VK_LEFT:
		kretanjeBagera -= 10;
		break;
	case 'D':
	case VK_RIGHT:
		kretanjeBagera += 10;
		break;
	default:
		break;
	}
	Invalidate();
}
