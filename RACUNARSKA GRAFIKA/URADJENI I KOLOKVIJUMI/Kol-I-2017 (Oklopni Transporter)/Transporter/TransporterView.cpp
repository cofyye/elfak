
// TransporterView.cpp : implementation of the CTransporterView class
//

#include "pch.h"
#include "framework.h"
// SHARED_HANDLERS can be defined in an ATL project implementing preview, thumbnail
// and search filter handlers and allows sharing of document code with that project.
#ifndef SHARED_HANDLERS
#include "Transporter.h"
#endif

#include "TransporterDoc.h"
#include "TransporterView.h"

#include "DImage.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

const float PI = 3.14159f;
#define TO_RAD(x) { x * PI / 180 }

const int OFFSET_X = 100;
const int OFFSET_Y = 100;

const int LEG_ROT_X = 28;
const int LEG_ROT_Y = 29;

const int LEGS_OFFSET_Y = 168;
const int BACK_LEGS_OFFSET_X = 72 - 20;
const int FRONT_LEGS_OFFSET_X = 270;

const int LEG2_OFFSET_X = 1;
const int LEG2_OFFSET_Y = LEGS_OFFSET_Y - 28;

const int LEG3_OFFSET_X = LEG2_OFFSET_X - 35;
const int LEG3_OFFSET_Y = LEG2_OFFSET_Y + 90;

// CTransporterView

IMPLEMENT_DYNCREATE(CTransporterView, CView)

BEGIN_MESSAGE_MAP(CTransporterView, CView)
	// Standard printing commands
	ON_COMMAND(ID_FILE_PRINT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, &CView::OnFilePrintPreview)
	ON_WM_ERASEBKGND()
	ON_WM_KEYDOWN()
END_MESSAGE_MAP()

// CTransporterView construction/destruction

CTransporterView::CTransporterView() noexcept
{
	body = new DImage();
	leg1 = new DImage();
	leg2 = new DImage();
	leg3 = new DImage();
	pozadina = new DImage();

	body->Load(CString("./Slike/Body.png"));
	leg1->Load(CString("./Slike/Leg1.png"));
	leg2->Load(CString("./Slike/Leg2.png"));
	leg3->Load(CString("./Slike/Leg3.png"));
	pozadina->Load(CString("./Slike/Back2.jpg"));

	legAngles[0] = -20;
	legAngles[1] = 20;
	legAngles[2] = legAngles[3] = 0;

	legPrevAngels[0] = -10;
	legPrevAngels[1] = 10;
	legPrevAngels[2] = 10;
	legPrevAngels[3] = -10;

	transporterScale = 1.2;
	kretanjeTransportera = 0;
}

CTransporterView::~CTransporterView()
{
	delete body, leg1, leg2, leg3, pozadina;
}

BOOL CTransporterView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	return CView::PreCreateWindow(cs);
}

// CTransporterView drawing

#pragma region Funkcije Transformacije

void CTransporterView::LoadIdentity(CDC* pDC)
{
	XFORM xform = {
		1.0f, 0.0f,
		0.0f, 1.0f,
		0.0f, 0.0f
	};

	pDC->SetWorldTransform(&xform);
}

void CTransporterView::Translate(CDC* pDC, float dX, float dY, bool rightMultiply)
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

void CTransporterView::Rotate(CDC* pDC, float angle, bool rightMultiply)
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

void CTransporterView::Scale(CDC* pDC, float sX, float sY, bool rightMultiply)
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

#pragma region Pomocne Funkcije i pozadina

void CTransporterView::DrawImgTransparent(CDC* pDC, DImage* pImage)
{
	int w = pImage->Width();
	int h = pImage->Height();

	CBitmap bmpImage, bmpMask;
	bmpImage.CreateCompatibleBitmap(pDC, w, h);
	bmpMask.CreateBitmap(w, h, 1, 1, NULL);

	CDC* pSrcDC = new CDC();
	pSrcDC->CreateCompatibleDC(pDC);
	CBitmap* oldSrcBmp = pSrcDC->SelectObject(&bmpImage);

	CDC* pMaskDC = new CDC();
	pMaskDC->CreateCompatibleDC(pDC);
	CBitmap* oldMaskBitmap = pMaskDC->SelectObject(&bmpMask);

	pImage->Draw(
		pSrcDC,
		CRect(0, 0, w, h),
		CRect(0, 0, w, h)
	);

	pSrcDC->SetBkColor(pSrcDC->GetPixel(0, 0));
	pMaskDC->BitBlt(0, 0, w, h, pSrcDC, 0, 0, SRCCOPY);

	pSrcDC->SetBkColor(RGB(0, 0, 0));
	pSrcDC->SetTextColor(RGB(255, 255, 255));
	pSrcDC->BitBlt(0, 0, w, h, pMaskDC, 0, 0, SRCAND);

	pDC->BitBlt(0, 0, w, h, pMaskDC, 0, 0, SRCAND);
	pDC->BitBlt(0, 0, w, h, pSrcDC, 0, 0, SRCPAINT);

	pSrcDC->SelectObject(oldSrcBmp);
	pMaskDC->SelectObject(oldSrcBmp);
	pSrcDC->DeleteDC();
	pMaskDC->DeleteDC();
	delete pSrcDC, pMaskDC;
}

void CTransporterView::DrawPozadina(CDC* pDC, CRect rect)
{
	int h = pozadina->Height();
	int w = pozadina->Width();

	pozadina->Draw(
		pDC,
		CRect(0, 0, w, h),
		rect
	);
}

void CTransporterView::ChangeAngle(float* angle, float* prevAngle)
{
	if ((*angle == -20 ||
		*angle > *prevAngle) &&
		*angle != 20)
	{
		*prevAngle = *angle;
		*angle += 10;
	}
	else 
	{
		*prevAngle = *angle;
		*angle -= 10;
	}
}

#pragma endregion

#pragma region Crtanje transportera

void CTransporterView::DrawBody(CDC* pDC)
{
	Scale(pDC, transporterScale, transporterScale);
	DrawImgTransparent(pDC, body);

	LoadIdentity(pDC);
}

void CTransporterView::DrawLeg(CDC* pDC, double alpha, double dx, double dy)
{
	// Leg1
	// Rotacija oko sebe
	Translate(pDC, -LEG_ROT_X, -LEG_ROT_Y);
	Rotate(pDC, alpha);
	Translate(pDC, LEG_ROT_X, LEG_ROT_Y);
	// Pomeraj
	Translate(pDC, dx, dy);
	Scale(pDC, transporterScale, transporterScale);

	DrawImgTransparent(pDC, leg1);

	LoadIdentity(pDC);



	// Leg2
	// Rotacija oko sebe
	Translate(pDC, -LEG_ROT_X, -LEG_ROT_Y);
	Rotate(pDC, -alpha);
	Translate(pDC, LEG_ROT_X, LEG_ROT_Y);
	// Pomeraj
	Translate(pDC, dx + LEG2_OFFSET_X, dy + LEG2_OFFSET_Y);

	// Rotacija oko leg1
	Translate(pDC, -LEG_ROT_X - dx, -LEG_ROT_Y - dy);
	Rotate(pDC, alpha);
	Translate(pDC, LEG_ROT_X + dx, LEG_ROT_Y + dy);

	Scale(pDC, transporterScale, transporterScale);
	DrawImgTransparent(pDC, leg2);

	LoadIdentity(pDC);



	// Leg3
	// Pomeraj
	Translate(pDC, dx + LEG3_OFFSET_X, dy + LEG3_OFFSET_Y);

	// Rotacija oko leg2
	Translate(pDC, -LEG_ROT_X - dx - LEG2_OFFSET_X, -LEG_ROT_Y - dy - LEG2_OFFSET_Y);
	Rotate(pDC, -alpha);
	Translate(pDC, LEG_ROT_X + dx + LEG2_OFFSET_X, LEG_ROT_Y + dy + LEG2_OFFSET_Y);

	// Rotacija oko leg1
	Translate(pDC, -LEG_ROT_X - dx, -LEG_ROT_Y - dy);
	Rotate(pDC, alpha);
	Translate(pDC, LEG_ROT_X + dx, LEG_ROT_Y + dy);

	Scale(pDC, transporterScale, transporterScale);
	DrawImgTransparent(pDC, leg3);

	LoadIdentity(pDC);
}

void CTransporterView::DrawScene(CDC* pDC, CRect rect)
{
	DrawPozadina(pDC, rect);
	
	pDC->SetViewportOrg(OFFSET_X + kretanjeTransportera, OFFSET_Y);
	
	DrawBody(pDC);
	DrawLeg(pDC, legAngles[0], BACK_LEGS_OFFSET_X, LEGS_OFFSET_Y);
	DrawLeg(pDC, legAngles[1], BACK_LEGS_OFFSET_X, LEGS_OFFSET_Y);
	DrawLeg(pDC, legAngles[2], FRONT_LEGS_OFFSET_X, LEGS_OFFSET_Y);
	DrawLeg(pDC, legAngles[3], FRONT_LEGS_OFFSET_X, LEGS_OFFSET_Y);

	pDC->SetViewportOrg(0, 0);
	LoadIdentity(pDC);
}

#pragma endregion

void CTransporterView::OnDraw(CDC* pDC)
{
	CTransporterDoc* pDoc = GetDocument();
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
	pMemDC->FillSolidRect(clientRect, pDC->GetBkColor());
	int oldMode = pMemDC->SetGraphicsMode(GM_ADVANCED);


	DrawScene(pMemDC, CRect(offsetPozadineX, offsetPozadineY, pozadina->Width() + offsetPozadineX, pozadina->Height() + offsetPozadineY));


	pDC->BitBlt(0, 0, clientRect.Width(), clientRect.Height(), pMemDC, 0, 0, SRCCOPY);
	
	pMemDC->SetGraphicsMode(oldMode);
	pMemDC->SelectObject(oldBitmap);
	pMemDC->DeleteDC();
	delete pMemDC;
}


// CTransporterView printing

BOOL CTransporterView::OnPreparePrinting(CPrintInfo* pInfo)
{
	// default preparation
	return DoPreparePrinting(pInfo);
}

void CTransporterView::OnBeginPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add extra initialization before printing
}

void CTransporterView::OnEndPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add cleanup after printing
}


// CTransporterView diagnostics

#ifdef _DEBUG
void CTransporterView::AssertValid() const
{
	CView::AssertValid();
}

void CTransporterView::Dump(CDumpContext& dc) const
{
	CView::Dump(dc);
}

CTransporterDoc* CTransporterView::GetDocument() const // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CTransporterDoc)));
	return (CTransporterDoc*)m_pDocument;
}
#endif //_DEBUG


// CTransporterView message handlers


BOOL CTransporterView::OnEraseBkgnd(CDC* pDC)
{
	return 1;
}


void CTransporterView::OnKeyDown(UINT nChar, UINT nRepCnt, UINT nFlags)
{
	CView::OnKeyDown(nChar, nRepCnt, nFlags);

	switch (nChar)
	{
	case VK_RIGHT:
		for (int i = 0; i < 4; i++)
			ChangeAngle(&legAngles[i], &legPrevAngels[i]);
		kretanjeTransportera += 25 * transporterScale;
		break;
	case VK_LEFT:
		for (int i = 0; i < 4; i++)
			ChangeAngle(&legAngles[i], &legPrevAngels[i]);
		kretanjeTransportera -= 25 * transporterScale;
		break;
	case VK_UP: transporterScale += 0.1f; break;
	case VK_DOWN: transporterScale -= 0.1f; break;
	case 'W':
		if (offsetPozadineY <= 0)
			break;
		offsetPozadineY -= 200;
		break;
	case 'S':
		if (offsetPozadineY >= pozadina->Width())
			break;
		offsetPozadineY += 200;
		break;
	case 'A':
		if (offsetPozadineX <= 0)
			break;
		offsetPozadineX -= 200;
		break;
	case 'D':
		if (offsetPozadineX >= pozadina->Height())
			break;
		offsetPozadineX += 200;
		break;
	default:
		break;
	}
	Invalidate();
}
