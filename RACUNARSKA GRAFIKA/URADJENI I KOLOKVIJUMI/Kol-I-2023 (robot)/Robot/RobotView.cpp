
// RobotView.cpp : implementation of the CRobotView class
//

#include "pch.h"
#include "framework.h"
#include <vector>
#include <iostream>
#include <fstream>
#include <string>
#include <sstream>
// SHARED_HANDLERS can be defined in an ATL project implementing preview, thumbnail
// and search filter handlers and allows sharing of document code with that project.
#ifndef SHARED_HANDLERS
#include "Robot.h"
#endif

#include "RobotDoc.h"
#include "RobotView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

const float PI = 3.14f;
#define TO_RAD(x) { x * PI / 189 };

const int POMERAJ_FIGURE_X = 412;
const int POMERAJ_FIGURE_Y = 170;
const int CENTER_X = 512 - POMERAJ_FIGURE_X;
const int CENTER_Y = 512 - POMERAJ_FIGURE_Y;
const int MIRROR_POMERAJ = 210;

const int POMERAJ_GLAVE_X = 55;
const int POMERAJ_GLAVE_Y = -100;

const int NADLAKTICA_ROTACIJA_X = 35;
const int NADLAKTICA_ROTACIJA_Y = 35;
const int NADLAKTICA_POMERAJ_X = 25 - 35;
const int NADLAKTICA_POMERAJ_Y = 65 - 35;

const int PODLAKTICA_ROTACIJA_X = 30;
const int PODLAKTICA_ROTACIJA_Y = 33;
const int PODLAKTICA_POMERAJ_X = 22 - 30 + NADLAKTICA_POMERAJ_X;
const int PODLAKTICA_POMERAJ_Y = 167 - 33 + NADLAKTICA_POMERAJ_Y;

const int SAKA_ROTACIJA_X = 25;
const int SAKA_ROTACIJA_Y = 3;
const int SAKA_POMERAJ_X = 30 - 25 + PODLAKTICA_POMERAJ_X;
const int SAKA_POMERAJ_Y = 140 - 3 + PODLAKTICA_POMERAJ_Y;

const int NADKOLENICA_POMERAJ_X = 61 - 29;
const int NADKOLENICA_POMERAJ_Y = 262 - 20;

const int PODKOLENICA_POMERAJ_X = 30 - 25 + NADKOLENICA_POMERAJ_X;
const int PODKOLENICA_POMERAJ_Y = 184 - 37 + NADKOLENICA_POMERAJ_Y;

const int STOPALO_POMERAJ_X = 25 - 20 + PODKOLENICA_POMERAJ_X;
const int STOPALO_POMERAJ_Y = 248 - 16 + PODKOLENICA_POMERAJ_Y;

// CRobotView

IMPLEMENT_DYNCREATE(CRobotView, CView)

BEGIN_MESSAGE_MAP(CRobotView, CView)
	// Standard printing commands
	ON_COMMAND(ID_FILE_PRINT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, &CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, &CView::OnFilePrintPreview)
	ON_WM_KEYDOWN()
	ON_WM_ERASEBKGND()
END_MESSAGE_MAP()

CRobotView::CRobotView() noexcept
{
	glava = new DImage();
	nadkolenica = new DImage();
	nadlaktica = new DImage();
	podkolenica = new DImage();
	podlaktica = new DImage();
	saka = new DImage();
	stopalo = new DImage();
	telo = new DImage();
	pozadina = new DImage();

	glava->Load(CString("slike/glava.png"));
	nadkolenica->Load(CString("slike/nadkolenica.png"));
	nadlaktica->Load(CString("slike/nadlaktica.png"));
	podkolenica->Load(CString("slike/podkolenica.png"));
	podlaktica->Load(CString("slike/podlaktica.png"));
	saka->Load(CString("slike/saka.png"));
	stopalo->Load(CString("slike/stopalo.png"));
	telo->Load(CString("slike/telo.png"));
	pozadina->Load(CString("slike/pozadina.jpg"));
}

CRobotView::~CRobotView()
{
	delete glava, nadkolenica, nadlaktica, podkolenica, podlaktica, saka, stopalo, telo, pozadina;
}

BOOL CRobotView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	return CView::PreCreateWindow(cs);
}

#pragma region Funkcije Transformacija

void CRobotView::Translate(CDC* pDC, float dX, float dY, bool rightMultiply)
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

void CRobotView::Rotate(CDC* pDC, float angle, bool rightMultiply)
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

void CRobotView::Scale(CDC* pDC, float sX, float sY, bool rightMultiply)
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

#pragma region Pomocne Funkcije i pozadina

void CRobotView::DrawBackground(CDC* pDC)
{
	int bgWidth = pozadina->Width();
	int bgHeight = pozadina->Height();

	pozadina->Draw(pDC, CRect(0, 0, bgWidth, bgHeight), CRect(0, 0, bgWidth, bgHeight));
}

void CRobotView::DrawImgTransparent(CDC* pDC, DImage* pImage)
{
	XFORM oldTransform;
	pDC->GetWorldTransform(&oldTransform);

	int w = pImage->Width();
	int h = pImage->Height();

	/*Translate(pDC, -w/2, -h/2);*/

	CBitmap bmpImg, bmpMask;
	bmpImg.CreateCompatibleBitmap(pDC, w, h);
	bmpMask.CreateBitmap(w, h, 1, 1, NULL);

	CDC* pSrcDC = new CDC();
	pSrcDC->CreateCompatibleDC(pDC);
	pSrcDC->SelectObject(bmpImg);

	CDC* pMaskDC = new CDC();
	pMaskDC->CreateCompatibleDC(pDC);
	pMaskDC->SelectObject(bmpMask);

	pImage->Draw(pSrcDC, CRect(0, 0, w, h), CRect(0, 0, w, h));

	COLORREF bkColor = pSrcDC->GetPixel(0, 0);
	pSrcDC->SetBkColor(bkColor);
	pMaskDC->BitBlt(0, 0, w, h, pSrcDC, 0, 0, SRCCOPY);

	pSrcDC->SetBkColor(RGB(0, 0, 0));
	pSrcDC->SetTextColor(RGB(255, 255, 255));
	pSrcDC->BitBlt(0, 0, w, h, pMaskDC, 0, 0, SRCAND);

	pDC->BitBlt(0, 0, w, h, pMaskDC, 0, 0, SRCAND);
	pDC->BitBlt(0, 0, w, h, pSrcDC, 0, 0, SRCPAINT);

	delete pSrcDC, pMaskDC;

	pDC->SetWorldTransform(&oldTransform);
}

void CRobotView::DrawNode(CDC* pDC, int x, int y)
{
	XFORM old;
	pDC->GetWorldTransform(&old);

	CBrush brush(RGB(0, 0, 0));
	CBrush* oldBrush = pDC->SelectObject(&brush);
	Translate(pDC, x, y);
	pDC->Ellipse(-5, -5, 5, 5);

	pDC->SelectObject(oldBrush);
	pDC->SetWorldTransform(&old);
}

#pragma endregion

#pragma region Crtanje Delova Robota

void CRobotView::DrawTelo(CDC* pDC)
{
	XFORM oldForm;
	pDC->GetWorldTransform(&oldForm);

	Translate(pDC, -CENTER_X, -CENTER_Y);
	Scale(pDC, robotScale, robotScale);
	Rotate(pDC, mirrorBody ? -robotAngle : robotAngle);
	Translate(pDC, CENTER_X, CENTER_Y);

	if (mirrorBody) Scale(pDC, -1, 1);
	DrawImgTransparent(pDC, telo);

	pDC->SetWorldTransform(&oldForm);
}

void CRobotView::DrawNadlaktica(CDC* pDC)
{
	XFORM oldForm;
	pDC->GetWorldTransform(&oldForm);

	// Nadlaktica
	Translate(pDC, -NADLAKTICA_ROTACIJA_X, -NADLAKTICA_ROTACIJA_Y);
	Rotate(pDC, nadlakticaAngle);
	Translate(pDC, NADLAKTICA_ROTACIJA_X, NADLAKTICA_ROTACIJA_Y);
	// Pomeraj nadlaktice
	Translate(pDC, NADLAKTICA_POMERAJ_X, NADLAKTICA_POMERAJ_Y);

	Translate(pDC, -CENTER_X, -CENTER_Y);
	Scale(pDC, robotScale, robotScale);
	Rotate(pDC, mirrorBody ? -robotAngle : robotAngle);
	Translate(pDC, CENTER_X, CENTER_Y);

	if (mirrorBody) Scale(pDC, -1, 1);
	DrawImgTransparent(pDC, nadlaktica);

	pDC->SetWorldTransform(&oldForm);
}

void CRobotView::DrawPodlaktica(CDC* pDC)
{
	XFORM oldForm;
	pDC->GetWorldTransform(&oldForm);

	//Podlaktica
	Translate(pDC, -PODLAKTICA_ROTACIJA_X, -PODLAKTICA_ROTACIJA_Y);
	Rotate(pDC, podlakticaAngle);
	Translate(pDC, PODLAKTICA_ROTACIJA_X, PODLAKTICA_ROTACIJA_Y);
	// Pomeraj
	Translate(pDC, PODLAKTICA_POMERAJ_X, PODLAKTICA_POMERAJ_Y);

	Translate(pDC, -(NADLAKTICA_ROTACIJA_X + NADLAKTICA_POMERAJ_X), -(NADLAKTICA_ROTACIJA_Y + NADLAKTICA_POMERAJ_Y));
	Rotate(pDC, nadlakticaAngle);
	Translate(pDC, NADLAKTICA_ROTACIJA_X + NADLAKTICA_POMERAJ_X, NADLAKTICA_ROTACIJA_Y + NADLAKTICA_POMERAJ_Y);

	Translate(pDC, -CENTER_X, -CENTER_Y);
	Scale(pDC, robotScale, robotScale);
	Rotate(pDC, mirrorBody ? -robotAngle : robotAngle);
	Translate(pDC, CENTER_X, CENTER_Y);

	if (mirrorBody) Scale(pDC, -1, 1);
	DrawImgTransparent(pDC, podlaktica);

	pDC->SetWorldTransform(&oldForm);
}

void CRobotView::DrawSaka(CDC* pDC)
{
	XFORM oldForm;
	pDC->GetWorldTransform(&oldForm);

	// Saka
	Translate(pDC, -SAKA_ROTACIJA_X, -SAKA_ROTACIJA_Y);
	Rotate(pDC, sakaAngle);
	Translate(pDC, SAKA_ROTACIJA_X, SAKA_ROTACIJA_Y);
	// Pomeraj
	Translate(pDC, SAKA_POMERAJ_X, SAKA_POMERAJ_Y);

	// Rotacija oko podlaktice
	Translate(pDC, -(PODLAKTICA_ROTACIJA_X + PODLAKTICA_POMERAJ_X), -(PODLAKTICA_ROTACIJA_Y + PODLAKTICA_POMERAJ_Y));
	Rotate(pDC, podlakticaAngle);
	Translate(pDC, PODLAKTICA_ROTACIJA_X + PODLAKTICA_POMERAJ_X, PODLAKTICA_ROTACIJA_Y + PODLAKTICA_POMERAJ_Y);

	Translate(pDC, -(NADLAKTICA_ROTACIJA_X + NADLAKTICA_POMERAJ_X), -(NADLAKTICA_ROTACIJA_Y + NADLAKTICA_POMERAJ_Y));
	Rotate(pDC, nadlakticaAngle);
	Translate(pDC, NADLAKTICA_ROTACIJA_X + NADLAKTICA_POMERAJ_X, NADLAKTICA_ROTACIJA_Y + NADLAKTICA_POMERAJ_Y);

	Translate(pDC, -CENTER_X, -CENTER_Y);
	Scale(pDC, robotScale, robotScale);
	Rotate(pDC, mirrorBody ? -robotAngle : robotAngle);
	Translate(pDC, CENTER_X, CENTER_Y);

	if (mirrorBody) Scale(pDC, -1, 1);
	DrawImgTransparent(pDC, saka);

	pDC->SetWorldTransform(&oldForm);
}

void CRobotView::DrawNadkolenica(CDC* pDC)
{
	XFORM oldForm;
	pDC->GetWorldTransform(&oldForm);

	// Nadkolenica
	Translate(pDC, NADKOLENICA_POMERAJ_X, NADKOLENICA_POMERAJ_Y);
	Translate(pDC, -CENTER_X, -CENTER_Y);
	Scale(pDC, robotScale, robotScale);
	Rotate(pDC, mirrorBody ? -robotAngle : robotAngle);
	Translate(pDC, CENTER_X, CENTER_Y);

	if (mirrorBody) Scale(pDC, -1, 1);
	DrawImgTransparent(pDC, nadkolenica);

	pDC->SetWorldTransform(&oldForm);
}

void CRobotView::DrawPodkolenica(CDC* pDC)
{
	XFORM oldForm;
	pDC->GetWorldTransform(&oldForm);

	// Podkolenica
	Translate(pDC, PODKOLENICA_POMERAJ_X, PODKOLENICA_POMERAJ_Y);

	Translate(pDC, -CENTER_X, -CENTER_Y);
	Scale(pDC, robotScale, robotScale);
	Rotate(pDC, mirrorBody ? -robotAngle : robotAngle);
	Translate(pDC, CENTER_X, CENTER_Y);

	if (mirrorBody) Scale(pDC, -1, 1);
	DrawImgTransparent(pDC, podkolenica);

	pDC->SetWorldTransform(&oldForm);
}

void CRobotView::DrawStopalo(CDC* pDC)
{
	XFORM oldForm;
	pDC->GetWorldTransform(&oldForm);

	int mirrorRotacijaPomeraj = mirrorBody ? MIRROR_POMERAJ : 0;
	// Stopalo
	Translate(pDC, STOPALO_POMERAJ_X, STOPALO_POMERAJ_Y);
	Translate(pDC, -CENTER_X, -CENTER_Y);
	Scale(pDC, robotScale, robotScale);
	Rotate(pDC, mirrorBody ? -robotAngle : robotAngle);
	Translate(pDC, CENTER_X, CENTER_Y);
	
	if (mirrorBody) Scale(pDC, -1, 1);
	DrawImgTransparent(pDC, stopalo);

	pDC->SetWorldTransform(&oldForm);
}

#pragma endregion

#pragma region Crtanje Robota

void CRobotView::DrawHalf(CDC* pDC)
{
	DrawTelo(pDC);

	DrawNadkolenica(pDC);
	DrawPodkolenica(pDC);
	DrawStopalo(pDC);

	DrawNadlaktica(pDC);
	DrawPodlaktica(pDC);
	DrawSaka(pDC);
}

void CRobotView::DrawHead(CDC* pDC)
{
	XFORM old;
	pDC->GetWorldTransform(&old);

	Translate(pDC, POMERAJ_GLAVE_X, POMERAJ_GLAVE_Y);
	Translate(pDC, -CENTER_X, -CENTER_Y);
	Scale(pDC, robotScale, robotScale);
	Rotate(pDC, robotAngle);
	Translate(pDC, CENTER_X, CENTER_Y);

	DrawImgTransparent(pDC, glava);

	pDC->SetWorldTransform(&old);
}

void CRobotView::DrawRobot(CDC* pDC)
{
	XFORM old;
	pDC->GetWorldTransform(&old);

	pDC->SetViewportOrg(POMERAJ_FIGURE_X, POMERAJ_FIGURE_Y);
	DrawHead(pDC);
	DrawHalf(pDC);

	pDC->SetViewportOrg(POMERAJ_FIGURE_X + MIRROR_POMERAJ, POMERAJ_FIGURE_Y);
	mirrorBody = true;
	//Translate(pDC, MIRROR_POMERAJ, 0);
	DrawHalf(pDC);
	mirrorBody = false;

	pDC->SetViewportOrg(0, 0);
	pDC->SetWorldTransform(&old);
}

#pragma endregion

void CRobotView::OnDraw(CDC* pDC)
{
	CRobotDoc* pDoc = GetDocument();
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
	DrawRobot(pMemDC);
	
	pDC->BitBlt(0, 0, clientRect.Width(), clientRect.Height(), pMemDC, 0, 0, SRCCOPY);

	pMemDC->SelectObject(oldBitmap);
	pMemDC->SetGraphicsMode(oldMode);
	delete pMemDC;
}

// CRobotView printing

BOOL CRobotView::OnPreparePrinting(CPrintInfo* pInfo)
{
	// default preparation
	return DoPreparePrinting(pInfo);
}

void CRobotView::OnBeginPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add extra initialization before printing
}

void CRobotView::OnEndPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add cleanup after printing
}


// CRobotView diagnostics

#ifdef _DEBUG
void CRobotView::AssertValid() const
{
	CView::AssertValid();
}

void CRobotView::Dump(CDumpContext& dc) const
{
	CView::Dump(dc);
}

CRobotDoc* CRobotView::GetDocument() const // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CRobotDoc)));
	return (CRobotDoc*)m_pDocument;
}
#endif //_DEBUG


// CRobotView message handlers


void CRobotView::OnKeyDown(UINT nChar, UINT nRepCnt, UINT nFlags)
{
	// TODO: Add your message handler code here and/or call default

	CView::OnKeyDown(nChar, nRepCnt, nFlags);
	switch (nChar)
	{
	case 'G':
		if (nadlakticaAngle >= 90) break;
		nadlakticaAngle += 5;
		break;
	case 'H':
		if (nadlakticaAngle <= -10) break;
		nadlakticaAngle -= 5;
		break;
	case 'D':
		if (podlakticaAngle >= 80) break;
		podlakticaAngle += 5;
		break;
	case 'F':
		if (podlakticaAngle <= -10) break;
		podlakticaAngle -= 5;
		break;
	case 'A':
		if (sakaAngle >= 30) break;
		sakaAngle += 5;
		break;
	case 'S':
		if (sakaAngle <= -10) break;
		sakaAngle -= 5;
		break;
	case '1':
		robotAngle -= 10;
		robotScale -= 0.05;
		break;
	case '2':
		robotAngle += 10;
		robotScale += 0.05;
		break;
	default:
		break;
	}

	Invalidate();
}


BOOL CRobotView::OnEraseBkgnd(CDC* pDC)
{
	return 1;
}
