#pragma once

class CGLRenderer
{
public:
	CGLRenderer(void);
	virtual ~CGLRenderer(void);

	bool CreateGLContext(CDC* pDC);			// kreira OpenGL Rendering Context
	void PrepareScene(CDC* pDC);			// inicijalizuje scenu,
	void Reshape(CDC* pDC, int w, int h);	// kod koji treba da se izvrsi svaki put kada se promeni velicina prozora ili pogleda i
	void DrawScene(CDC* pDC);				// iscrtava scenu
	void DestroyScene(CDC* pDC);			// dealocira resurse alocirane u drugim funkcijama ove klase,
	void DrawScene();

	unsigned int ProcessHits(int hits, unsigned int buffer[]);
	unsigned int PickObject(CDC* pDC, int x, int y);
protected:
	HGLRC	 m_hrc; //OpenGL Rendering Context 
	float	m_aspect;
public:
	double m_z, m_x, m_y, m_Rx, m_Ry;

};