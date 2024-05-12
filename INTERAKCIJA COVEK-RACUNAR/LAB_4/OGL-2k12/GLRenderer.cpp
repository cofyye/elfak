#include "StdAfx.h"
#include "GLRenderer.h"
#include "GL\gl.h"
#include "GL\glu.h"
#include "GL\glaux.h"
#include "GL\glut.h"

CGLRenderer::CGLRenderer(void)
{
	m_x = 0.0f;
	m_y = 0.0f;
	m_z = 0.0f;
	m_Rx = 0.0f;
	m_Ry = 0.0f;
	m_aspect = 0.0f;
	m_hrc = {};
}

CGLRenderer::~CGLRenderer(void)
{
}

bool CGLRenderer::CreateGLContext(CDC* pDC)
{
	PIXELFORMATDESCRIPTOR pfd;
	memset(&pfd, 0, sizeof(PIXELFORMATDESCRIPTOR));
	pfd.nSize = sizeof(PIXELFORMATDESCRIPTOR);
	pfd.nVersion = 1;
	pfd.dwFlags = PFD_DOUBLEBUFFER | PFD_SUPPORT_OPENGL | PFD_DRAW_TO_WINDOW;
	pfd.iPixelType = PFD_TYPE_RGBA;
	pfd.cColorBits = 32;
	pfd.cDepthBits = 24;
	pfd.iLayerType = PFD_MAIN_PLANE;

	int nPixelFormat = ChoosePixelFormat(pDC->m_hDC, &pfd);

	if (nPixelFormat == 0) return false;

	BOOL bResult = SetPixelFormat(pDC->m_hDC, nPixelFormat, &pfd);

	if (!bResult) return false;

	m_hrc = wglCreateContext(pDC->m_hDC);

	if (!m_hrc) return false;

	return true;
}

void CGLRenderer::PrepareScene(CDC* pDC)
{
	wglMakeCurrent(pDC->m_hDC, m_hrc);
	//---------------------------------
	glClearColor(1.0, 1.0, 1.0, 1.0);
	glEnable(GL_DEPTH_TEST);
	//glCullFace(GL_BACK);
	//glEnable(GL_CULL_FACE);
	//---------------------------------
	wglMakeCurrent(NULL, NULL);
}

void CGLRenderer::DrawScene(CDC* pDC)
{
	wglMakeCurrent(pDC->m_hDC, m_hrc);
	//---------------------------------
	DrawScene();
	//---------------------------------
	SwapBuffers(pDC->m_hDC);
	wglMakeCurrent(NULL, NULL);
}

void CGLRenderer::DrawScene()
{
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	glLoadIdentity();

	glTranslatef(m_x, m_y, -3.0 + m_z);

	glRotatef(m_Rx, 1.0, 0.0, 0.0);
	glRotatef(m_Ry, 0.0, 1.0, 0.0);
	glPushMatrix();
	glPopName();
	//-------------------------------------
	glPushName(1);
	glColor3f(0, 1.0, 0);//zelena
	glutSolidCube(1.0);

	glPushName(5);
	glTranslatef(0.0, 0.5, 0.0);
	glColor3f(1.0, 0, 1.0);//ljubicasta
	glutSolidCube(0.5);
	glPopName();

	glTranslatef(0.0, -0.5, 0.5);

	glPushName(2);
	glTranslatef(1.0, 0.0, -1.0);
	glColor3f(1.0, 0, 0);//crvena
	glutSolidCube(1.0);
	glPopName();

	glPushName(3);
	glTranslatef(-2.0, 0.0, 0.0);
	glColor3f(0, 0, 1.0);//plava
	glutSolidCube(1.0);
	glPopName();

	glPushName(4);
	glTranslatef(1.0, 0.0, -1.0);
	glColor3f(1.0, 1.0, 0);//zuta
	glutSolidCube(1.0);
	//-------------------------------------

	glPopMatrix();

	glFlush();

}
void CGLRenderer::Reshape(CDC* pDC, int w, int h)
{
	wglMakeCurrent(pDC->m_hDC, m_hrc);
	//---------------------------------
	glViewport(0, 0, (GLsizei)w, (GLsizei)h);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	m_aspect = (float)w / (float)h;
	gluPerspective(40, m_aspect, 0.1, 100);
	glMatrixMode(GL_MODELVIEW);

	//---------------------------------
	wglMakeCurrent(NULL, NULL);
}

void CGLRenderer::DestroyScene(CDC* pDC)
{
	//wglMakeCurrent(pDC->m_hDC, m_hrc);
	wglMakeCurrent(NULL, NULL);
	if (m_hrc)
	{
		wglDeleteContext(m_hrc);
		m_hrc = NULL;
	}
}

unsigned int CGLRenderer::PickObject(CDC* pDC, int x, int y)
{
	wglMakeCurrent(pDC->m_hDC, m_hrc);
	//---------------------------------
	GLint viewport[4];
	GLuint selectBuf[512];

	glSelectBuffer(512, selectBuf);
	glRenderMode(GL_SELECT);

	glMatrixMode(GL_PROJECTION);
	glPushMatrix();
	glLoadIdentity();

	glGetIntegerv(GL_VIEWPORT, viewport);
	gluPickMatrix(x, viewport[3] - y, 3, 3, viewport);
	//-----------------------------
	gluPerspective(40.0, m_aspect, 0.1, 100.0);
	//-----------------------------
	glMatrixMode(GL_MODELVIEW);
	glInitNames();
	glPushName(0);
	DrawScene();
	glMatrixMode(GL_PROJECTION);
	glPopMatrix();
	glMatrixMode(GL_MODELVIEW);
	glFlush();

	int hits = glRenderMode(GL_RENDER);
	unsigned int retVal = 0;
	if (hits != 0)
		retVal = ProcessHits(hits, selectBuf);
	//---------------------------------------
	wglMakeCurrent(NULL, NULL);
	return retVal;
}

unsigned int CGLRenderer::ProcessHits(int hits, unsigned int buffer[])
{
	unsigned int i = 0;
	unsigned int names, *ptr, minZ, *ptrNames, numberOfNames;
	unsigned int pom = 0;
	ptr = (unsigned int*)buffer;
	minZ = 0xffffffff;
	for (i = 0; i < hits; i++)
	{
		names = *ptr;
		ptr++;
		if (*ptr < minZ)
		{
			numberOfNames = names;	//uzimamo broj imena
			minZ = *ptr;		//minZ is updated to the value pointed to by ptr
			ptrNames = ptr + 2;	//ptrNames is assigned the memory address of ptr + 2
			pom *= 10;		//da bi preskocili minZ i maxZ u slogu selekcije, 5 imena * 2 jer je data hit 2
			pom += *ptrNames;
		}
		ptr += names + 2;	//ptr is incremented by names+2 to skip over the names in the buffer and move to the next hit data
	}
	*ptr = pom;
	return pom;
}