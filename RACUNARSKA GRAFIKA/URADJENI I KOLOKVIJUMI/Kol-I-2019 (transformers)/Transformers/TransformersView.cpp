
// TransformersView.cpp : implementation of the CTransformersView class
//

#include "pch.h"
#include "framework.h"
#include "DImage.h"
#include <iostream>
#include <fstream>
#include <sstream>
#include <string>
#include <vector>
// SHARED_HANDLERS can be defined in an ATL project implementing preview, thumbnail
// and search filter handlers and allows sharing of document code with that project.
#ifndef SHARED_HANDLERS
#include "Transformers.h"
#endif

using namespace std;

#include "TransformersDoc.h"
#include "TransformersView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

const float PI = 3.141592653f;
#define TO_RAD(x) { x * PI/180 }

const int OFFSET_X = 200;
const int OFFSET_Y = 270;

const int BODY_ROT_X = 26;
const int BODY_ROT_Y = 133;
const int BODY_OFFSET_X = 0;
const int BODY_OFFSET_Y = 0;

const int LEG1_ROT_X = 30;
const int LEG1_ROT_Y = 125;
const int LEG1_SECOND_POINT_X = 237;
const int LEG1_SECOND_POINT_Y = 125;
const int LEG1_OFFSET_X = BODY_ROT_X - LEG1_SECOND_POINT_X + 242;
const int LEG1_OFFSET_Y = BODY_ROT_Y - LEG1_SECOND_POINT_Y + 206;

const int ARM1_ROT_X = 34;
const int ARM1_ROT_Y = 31;
const int ARM1_SECOND_POINT_X = 210;
const int ARM1_SECOND_POINT_Y = 102;
const int ARM1_OFFSET_X = 176 + BODY_OFFSET_X;
const int ARM1_OFFSET_Y = 54 + BODY_OFFSET_Y;

const int ARM2_ROT_X = 23;
const int ARM2_ROT_Y = 61;
const int ARM2_OFFSET_X = ARM1_SECOND_POINT_X - ARM2_ROT_X + ARM1_OFFSET_X;
const int ARM2_OFFSET_Y = ARM1_SECOND_POINT_Y - ARM2_ROT_Y + ARM1_OFFSET_Y;

const int LEG2_ROT_X = 35;
const int LEG2_ROT_Y = 60;
const int LEG2_OFFSET_X = 26;
const int LEG2_OFFSET_Y = 282;

// CTransformersView

IMPLEMENT_DYNCREATE(CTransformersView, CView)

BEGIN_MESSAGE_MAP(CTransformersView, CView)
	// Standard printing commands
	ON_COMMAND(ID_FILE_PRINT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, &CView::OnFilePrintPreview)
	ON_WM_KEYDOWN()
	ON_WM_ERASEBKGND()
END_MESSAGE_MAP()

// CTransformersView construction/destruction

CTransformersView::CTransformersView() noexcept
{
	body = new DImage();
	arm1 = new DImage();
	arm2 = new DImage();
	leg1 = new DImage();
	leg2 = new DImage();
	background = new DImage();

	body->Load(CString("./slike/body1.png"));
	arm1->Load(CString("./slike/arm1.png"));
	arm2->Load(CString("./slike/arm2.png"));
	leg1->Load(CString("./slike/leg1.png"));
	leg2->Load(CString("./slike/leg2.png"));
	background->Load(CString("./slike/background.jpg"));
}

CTransformersView::~CTransformersView()
{
	delete body, arm1, arm2, leg1, leg2, background;
}

BOOL CTransformersView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	return CView::PreCreateWindow(cs);
}

#pragma region Pomocne Funkcije

void CTransformersView::DrawBackground(CDC* pDC, CRect rc)
{
	int bgWidth = background->Width();
	int bgHeight = background->Height();

	CRect pozadina(0, 0, bgWidth, bgHeight);
	background->Draw(pDC, pozadina, rc);
}

void CTransformersView::DrawImgTransparent(CDC* pDC, DImage* pImage)
{
	XFORM oldForm;
	pDC->GetWorldTransform(&oldForm);

	int w = pImage->Width();
	int h = pImage->Height();

	//Translate(pDC, -(w >> 1), -(h >> 1));

	CBitmap bmpImage, bmpMask;

	bmpImage.CreateCompatibleBitmap(pDC, w, h);
	bmpMask.CreateBitmap(w, h, 1, 1, NULL);

	CDC* pSrcDC = new CDC();
	pSrcDC->CreateCompatibleDC(pDC);
	pSrcDC->SelectObject(&bmpImage);
	CDC* pMaskDC = new CDC();
	pMaskDC->CreateCompatibleDC(pDC);
	pMaskDC->SelectObject(&bmpMask);

	pImage->Draw(pSrcDC, CRect(0, 0, w, h), CRect(0, 0, w, h));

	COLORREF pozadina = pSrcDC->GetPixel(0, 0);
	pSrcDC->SetBkColor(pozadina);
	pMaskDC->BitBlt(0, 0, w, h, pSrcDC, 0, 0, SRCCOPY);

	pSrcDC->SetTextColor(RGB(255, 255, 255));
	pSrcDC->SetBkColor(RGB(0, 0, 0));
	pSrcDC->BitBlt(0, 0, w, h, pMaskDC, 0, 0, SRCAND);


	pDC->BitBlt(0, 0, w, h, pMaskDC, 0, 0, SRCAND);
	pDC->BitBlt(0, 0, w, h, pSrcDC, 0, 0, SRCPAINT);

	delete pSrcDC;
	delete pMaskDC;

	pDC->SetWorldTransform(&oldForm);
}

void CTransformersView::DrawPoint(CDC* pDC, int x, int y)
{
	XFORM oldForm;
	pDC->GetWorldTransform(&oldForm);

	Translate(pDC, x, y);
	pDC->Ellipse(-5, -5, 5, 5);

	pDC->SetWorldTransform(&oldForm);
}

#pragma endregion

#pragma region Funkcije Transformacije

void CTransformersView::Translate(CDC* pDC, float dX, float dY, bool rightMultiply)
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

void CTransformersView::Rotate(CDC* pDC, float angle, bool rightMultiply)
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

#pragma endregion

#pragma region Crtanje Transformersa

void CTransformersView::DrawArm1(CDC* pDC)
{
	XFORM oldForm;
	pDC->GetWorldTransform(&oldForm);

	// Rotacija oko sebe
	Translate(pDC, -ARM1_ROT_X, -ARM1_ROT_Y);
	Rotate(pDC, (arm1Angle * 2.69) + 90);
	Translate(pDC, ARM1_ROT_X, ARM1_ROT_Y);
	// Pomeraj figure
	Translate(pDC, ARM1_OFFSET_X, ARM1_OFFSET_Y);

	// Rotacija oko body
	Translate(pDC, -BODY_ROT_X - BODY_OFFSET_X, -BODY_ROT_Y - BODY_OFFSET_Y);
	Rotate(pDC, bodyAngle);
	Translate(pDC, BODY_ROT_X + BODY_OFFSET_X, BODY_ROT_Y + BODY_OFFSET_Y);

	// Rotacija oko leg1
	Translate(pDC, -LEG1_ROT_X - LEG1_OFFSET_X, -LEG1_ROT_Y - LEG1_OFFSET_Y);
	Rotate(pDC, arm1Angle);
	Translate(pDC, LEG1_ROT_X + LEG1_OFFSET_X, LEG1_ROT_Y + LEG1_OFFSET_Y);

	DrawImgTransparent(pDC, arm1);

	pDC->SetWorldTransform(&oldForm);
}

void CTransformersView::DrawArm2(CDC* pDC)
{
	XFORM oldForm;
	pDC->GetWorldTransform(&oldForm);

	// Rotacija oko sebe
	Translate(pDC, -ARM2_ROT_X, -ARM2_ROT_Y);
	Rotate(pDC, arm2Angle - 90);
	Translate(pDC, ARM2_ROT_X, ARM2_ROT_Y);
	// Pomeraj figure
	Translate(pDC, ARM2_OFFSET_X, ARM2_OFFSET_Y);

	// Rotacija oko arm1
	Translate(pDC, -ARM1_ROT_X - ARM1_OFFSET_X, -ARM1_ROT_Y - ARM1_OFFSET_Y);
	Rotate(pDC, (arm1Angle * 2.69) + 90);
	Translate(pDC, ARM1_ROT_X + ARM1_OFFSET_X, ARM1_ROT_Y + ARM1_OFFSET_Y);

	// Rotacija oko body
	Translate(pDC, -BODY_ROT_X - BODY_OFFSET_X, -BODY_ROT_Y - BODY_OFFSET_Y);
	Rotate(pDC, bodyAngle);
	Translate(pDC, BODY_ROT_X + BODY_OFFSET_X, BODY_ROT_Y + BODY_OFFSET_Y);

	// Rotacija oko leg1
	Translate(pDC, -LEG1_ROT_X - LEG1_OFFSET_X, -LEG1_ROT_Y - LEG1_OFFSET_Y);
	Rotate(pDC, arm1Angle);
	Translate(pDC, LEG1_ROT_X + LEG1_OFFSET_X, LEG1_ROT_Y + LEG1_OFFSET_Y);

	DrawImgTransparent(pDC, arm2);

	pDC->SetWorldTransform(&oldForm);
}

void CTransformersView::DrawLeg1(CDC* pDC)
{
	XFORM oldForm;
	pDC->GetWorldTransform(&oldForm);

	// Rotira oko sebe
	Translate(pDC, -LEG1_ROT_X, -LEG1_ROT_Y);
	Rotate(pDC, arm1Angle - 100);
	Translate(pDC, LEG1_ROT_X, LEG1_ROT_Y);

	// Pomeraj figure
	Translate(pDC, LEG1_OFFSET_X, LEG1_OFFSET_Y);

	DrawImgTransparent(pDC, leg1);

	pDC->SetWorldTransform(&oldForm);
}

void CTransformersView::DrawLeg2(CDC* pDC)
{
	XFORM oldForm;
	pDC->GetWorldTransform(&oldForm);

	// Rotacija oko sebe
	Translate(pDC, -LEG2_ROT_X, -LEG2_ROT_Y);
	Rotate(pDC, leg2Angle + 110);
	Translate(pDC, LEG2_ROT_X, LEG2_ROT_Y);
	// Pomeraj figure
	Translate(pDC, LEG2_OFFSET_X, LEG2_OFFSET_Y);

	// Rotacija oko leg1
	Translate(pDC, -LEG1_ROT_X - LEG1_OFFSET_X, -LEG1_ROT_Y - LEG1_OFFSET_Y);
	Rotate(pDC, arm1Angle);
	Translate(pDC, LEG1_ROT_X + LEG1_OFFSET_X, LEG1_ROT_Y + LEG1_OFFSET_Y);

	DrawImgTransparent(pDC, leg2);

	pDC->SetWorldTransform(&oldForm);
}

void CTransformersView::Body1(CDC* pDC)
{
	XFORM oldForm;
	pDC->GetWorldTransform(&oldForm);

	// Rotacije oko svoje ose
	Translate(pDC, -BODY_ROT_X, -BODY_ROT_Y);
	Rotate(pDC, bodyAngle);
	Translate(pDC, BODY_ROT_X, BODY_ROT_Y);
	// Pomeraj figure
	Translate(pDC, BODY_OFFSET_X, BODY_OFFSET_Y);

	// Rotacija oko leg1
	Translate(pDC, -LEG1_ROT_X - LEG1_OFFSET_X, -LEG1_ROT_Y - LEG1_OFFSET_Y);
	Rotate(pDC, arm1Angle);
	Translate(pDC, LEG1_ROT_X + LEG1_OFFSET_X, LEG1_ROT_Y + LEG1_OFFSET_Y);

	DrawImgTransparent(pDC, body);

	pDC->SetWorldTransform(&oldForm);
}

void CTransformersView::DrawTransformer(CDC* pDC)
{
	pDC->SetViewportOrg(OFFSET_X + kreanjeRobota, OFFSET_Y);
	DrawArm2(pDC);
	Body1(pDC);
	DrawLeg2(pDC);
	DrawLeg1(pDC);
	DrawArm1(pDC);
	pDC->SetViewportOrg(0, 0);
}

#pragma endregion

void CTransformersView::OnDraw(CDC* pDC)
{
	CTransformersDoc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	if (!pDoc)
		return;

	int oldDCMode = pDC->SetGraphicsMode(GM_ADVANCED);

	CRect clientRect;
	GetClientRect(&clientRect);
	CDC* pMemDC = new CDC();
	pMemDC->CreateCompatibleDC(pDC);

	int oldMode = pMemDC->SetGraphicsMode(GM_ADVANCED);

	CBitmap memBitmap;
	memBitmap.CreateCompatibleBitmap(pDC, clientRect.Width(), clientRect.Height());
	CBitmap* oldBitMap = pMemDC->SelectObject(&memBitmap);

	DrawBackground(pMemDC, clientRect);
	DrawTransformer(pMemDC);

	CString text;
	text.Format(_T("arm1Angle: %f, arm2Angle: %f, lef2Angle: %f, bodyAngle: %f"), arm1Angle, arm2Angle, leg2Angle, bodyAngle);
	pMemDC->TextOutW(0, 0, text);

	pDC->BitBlt(0, 0, clientRect.Width(), clientRect.Height(), pMemDC, 0, 0, SRCCOPY);

	pMemDC->SetGraphicsMode(oldMode);
	pMemDC->SelectObject(oldBitMap);
	delete pMemDC;
}


// CTransformersView printing

BOOL CTransformersView::OnPreparePrinting(CPrintInfo* pInfo)
{
	// default preparation
	return DoPreparePrinting(pInfo);
}

void CTransformersView::OnBeginPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add extra initialization before printing
}

void CTransformersView::OnEndPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add cleanup after printing
}


// CTransformersView diagnostics

#ifdef _DEBUG
void CTransformersView::AssertValid() const
{
	CView::AssertValid();
}

void CTransformersView::Dump(CDumpContext& dc) const
{
	CView::Dump(dc);
}

CTransformersDoc* CTransformersView::GetDocument() const // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CTransformersDoc)));
	return (CTransformersDoc*)m_pDocument;
}
#endif //_DEBUG


// CTransformersView message handlers


void CTransformersView::OnKeyDown(UINT nChar, UINT nRepCnt, UINT nFlags)
{
	CView::OnKeyDown(nChar, nRepCnt, nFlags);

	switch (nChar)
	{
	case 'Q': 
		if (arm1Angle < 100)
			arm1Angle += 5; 
		break;
	case 'A':
		if (arm1Angle > 0)
			arm1Angle -= 5;
		break;
	case 'R':
		if (leg2Angle > -210)
			leg2Angle += 5; 
		break;
	case 'F':
		if (leg2Angle < 0)
			leg2Angle -= 5;
		break;
	case 'T':
		if (arm2Angle < 280)
			arm2Angle += 5;
		break;
	case 'G':
		if (arm2Angle > 0)
			arm2Angle -= 5; 
		break;
	case 'W':
		if (bodyAngle < -90)
			bodyAngle += 5; 
		break;
	case 'S':
		if (bodyAngle > -100)
			bodyAngle -= 5;
		break;
	case VK_RIGHT:
		if (arm1Angle < 100)
			arm1Angle += 5;
		if (arm2Angle < 280)
			arm2Angle += 14;
		if (arm2Angle > 280)
			arm2Angle = 280;
		if (leg2Angle > -210)
			leg2Angle -= 11;
		if (leg2Angle < -210)
			leg2Angle = -210;
		if (bodyAngle > -100)
			bodyAngle -= 1;
		break;
	case VK_LEFT:
		if (arm1Angle > 0)
			arm1Angle -= 5;
		if (arm2Angle > 0)
			arm2Angle -= 14;
		if (leg2Angle < 0)
			leg2Angle += 11;
		if (bodyAngle < -90)
			bodyAngle += 1;
		break;
	case VK_UP:
		if (arm1Angle == 100 &&
			arm2Angle == 280 &&
			leg2Angle == -210 &&
			bodyAngle == -100)
			kreanjeRobota += 10;
		break;
	case VK_DOWN:
		if (arm1Angle == 100 &&
			arm2Angle == 280 &&
			leg2Angle == -210 &&
			bodyAngle == -100)
			kreanjeRobota -= 10;
		break;
	default:
		break;
	}
	Invalidate();
}


BOOL CTransformersView::OnEraseBkgnd(CDC* pDC)
{
	return 1;
}
