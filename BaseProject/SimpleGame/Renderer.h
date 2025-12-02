#pragma once

#include <string>
#include <cstdlib>
#include <fstream>
#include <iostream>
#include <cassert>

#include "Dependencies\glew.h"
#include "LoadPng.h"

#define MAX_POINTS 500
class Renderer
{
public:
	Renderer(int windowSizeX, int windowSizeY);
	~Renderer();

	bool IsInitialized();
	void ReloadAllShaderPrograms();
	void DrawSolidRect(float x, float y, float z, float size, float r, float g, float b, float a);
	void DrawTest();
	void DrawParticle();
	void DrawGridMesh();
	void DrawFullScreenColor(float r, float g, float b, float a);
	void DrawFS();
	void DrawDebugTextures();
	void DrawFBOs();
	void DrawBloomParticle();

private:
	void Initialize(int windowSizeX, int windowSizeY);
	void DeleteAllShaderPrograms();
	void CompileAllShaderPrograms();
	bool ReadFile(char* filename, std::string *target);
	void AddShader(GLuint ShaderProgram, const char* pShaderText, GLenum ShaderType);
	GLuint CompileShaders(char* filenameVS, char* filenameFS);
	void CreateVertexBufferObjects();
	void GetGLPosition(float x, float y, float *newX, float *newY);
	void CreateParticles(int cnt);
	void CreateGridMesh(int x, int y);
	GLuint CreatePngTexture(char* filePath, GLuint samplingMethod);
	void DrawTexture(float x, float y, float sizeX, float sizeY, GLuint textureID, GLuint textureID1, GLuint method);
	void CreateFBOs();

	bool m_Initialized = false;
	
	unsigned int m_WindowSizeX = 0;
	unsigned int m_WindowSizeY = 0;

	GLuint m_VBORect = 0;
	GLuint m_TestShader = 0;
	GLuint m_SolidRectShader = 0;

	GLuint m_VBOTestPos = 0;
	GLuint m_VBOTestColor = 0;

	float m_Time = 0;

	GLuint m_VBOParticles = 0;
	GLuint m_VBOParticlesVertexCount = 0;
	GLuint m_ParticleShader = 0;

	GLuint m_GridMeshVBO = 0;
	GLuint m_GridMeshVertexCount = 0;
	GLuint m_GridMeshShader = 0;

	GLuint m_FullScreenVBO = 0;
	//GLuint m_FullScreenVertexCount = 0;
	GLuint m_FullScreenShader = 0;

	// raindrop
	int m_Dc = 100;
	float m_Points[MAX_POINTS * 4];

	// fragment shader factory
	GLuint m_FSVBO = 0;
	GLuint m_FSShader = 0;

	// Textures
	GLuint m_RGBTexture = 0;
	GLuint m_ParticleTexture = 0;

	GLuint m_0Texture = 0;
	GLuint m_1Texture = 0;
	GLuint m_2Texture = 0;
	GLuint m_3Texture = 0;
	GLuint m_4Texture = 0;
	GLuint m_5Texture = 0;
	GLuint m_6Texture = 0;
	GLuint m_7Texture = 0;
	GLuint m_8Texture = 0;
	GLuint m_9Texture = 0;
	GLuint m_TotalNumTexture = 0;

	//Texture
	GLuint m_TexVBO = 0;
	GLuint m_texShader = 0;

	// FBOs
	GLuint m_FBO0 = 0;
	GLuint m_FBO1 = 0;
	GLuint m_FBO2 = 0;
	GLuint m_FBO3 = 0;
	GLuint m_FBO4 = 0;
	
	GLuint m_RT0 = 0;
	GLuint m_RT0_1 = 0;
	GLuint m_RT1 = 0;
	GLuint m_RT1_1 = 0;
	GLuint m_RT2 = 0;
	GLuint m_RT2_1 = 0;
	GLuint m_RT3 = 0;
	GLuint m_RT3_1 = 0;
	GLuint m_RT4 = 0;

	GLuint m_HDRFBO0 = 0;
	GLuint m_HDRRT0_0 = 0;
	GLuint m_HDRRT0_1 = 0;

	GLuint m_PingpongFBO[2] = { 0,0 };
	GLuint m_PingpongTexture[2] = { 0,0 };
};

