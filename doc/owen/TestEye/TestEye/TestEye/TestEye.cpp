// TestEye.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <string>
#include <windows.h>
#include "stdio.h"

int _tmain(int argc, _TCHAR* argv[])
{
	if (argc < 2)
	{
		return 0;
	}

	std::string strIP = argv[1];

	HMODULE hInst;
	hInst = LoadLibrary("FALCONDLL.dll");    //Load DLL file.
	if(!hInst)
	{
		printf("Dll load fail.\n");
		return 1;
	}

	void (*pFalcon)(char*, unsigned char*, int&);    //Declare function pointer.
	//Falcon(char* IP, uchar* buffer, int DataLength)
	(FARPROC&)( pFalcon) = GetProcAddress(hInst, "_Z5GetIRPKcPhRi");  //Get function.
	if(pFalcon == NULL)    //Get function fail.
	{
		printf("Function pointer is NULL\n");
		system("PAUSE");
		return 2;
	}

	int iLen = 0;
	unsigned char uBuf[1024] = {'\0'};
	pFalcon((char*) strIP.c_str(), uBuf, iLen);    //Execute function.

	FreeLibrary(hInst);    //Release dll.
	if (iLen == 0) 
	{
		return 3;
	}

	char* pBuf = new char[iLen + 1];
	memcpy(pBuf,uBuf, iLen);
	pBuf[iLen] = '\0';

	std::string strContent = pBuf;
	delete pBuf;

	//std::string strContent = "<IR:[1,�f�������ҷū�|1,�f�����������|1,�f���ӥ��q�q��|1,�f����UPS��X�q��|1,�o�q���B��|1,�o�q���L��|1,�o�q���L��|1,�o�q���o�줣��|1,|1,|1][2,Buzzer|0,ALARM|0,|0,|0,|0][3,�o�q���B��|1,�o�q���L��|1,�o�q���L��|1,�o�q���o�줣��|1,�Ʀ��J�I��|3,�Ʀ��J�I��|3][4,�f�������ҷū�|1|24.10|1|54.90][5][6,�f���ӥ��q�q��|1|123.20,�f����UPS��X�q��|1|112.30][7][8,�f���������q���q��|1|12.89]:>";
	//std::string strContent = "<IR:[1,�I�������ҷū�|1,�I�����������|1,�I�������q�q��|1,�I����UPS��X�q��|1,�I����UPS�Ǹ�|1,�I����UPS�q�����`|1,�I����UPS�Ұ�|1,�I����UPS�G��|1,�|���@|1,�|���G|1][2,Buzzer|0,ALARM|0,|0,|0,|0][3,�I����UPS�Ǹ�|3,�I����UPS�q�����`|3,�I����UPS�Ұ�|3,�I����UPS�G��|1,����|3,�N��|3][4,�I�������ҷū�|1|26.10|1|70.70][5][6,�I�������q�q��|1|115.20,�I����UPS��X�q��|1|103.80][7,�|���@|1|23.36][8,�I���������q���q�����`|1|12.59]:>";
	//std::string strContent = "<IR:[1,�j�W���ҷū�|1,�j�W�������|1,�j�W���q�q��|1,�j�WUPS��X�q��|2,�j�WUPS�Ǹ�|1,�j�WUPS�q�����`|1,�j�WUPS�Ұ�|2,�j�WUPS�G��|1,|1,|1][2,Buzzer|2,ALARM|1,|0,|0,|0][3,�j�WUPS�Ǹ�|1,�j�WUPS�q�����`|1,�j�WUPS�Ұ�|2,�j�WUPS�G��|1,����|3,���T|3][4,�j�W���ҷū�|1|22.10|1|88.70][5][6,�j�W���q�q��|1|113.30,�j�WUPS��X�q��|2| 0.00][7][8,�j�W�����q���q�����`|1|12.68]:>";
	//std::string strContent = "<IR:[1,�������ҷū�|1,�����������|1,�������q�q��|1,����UPS��X�q��|1,���d�ū� 1|1,���d�ū� 2|1,|1,|1,|1,|1][2,Buzzer|0,ALARM|0,|0,|0,|0][3,����UPS���q|1,����UPS�Ǹ�|3,����UPS�q���q��|3,���T|3,��������|3,�N��|3][4,�������ҷū�|1|21.90|1|86.10][5][6,�������q�q��|1|110.90,����UPS��X�q��|1|107.10][7][8,�q���q��|1|12.87]:>";
	//std::string strContent = "<IR:[1,�ӥ��s���ҷū�|1,�ӥ��s�������|1,�ӥ��s���q�q��|1,�ӥ��sUPS��X�q��|1,�ӥ��sUPS�Ǹ�|1,�ӥ��sUPS�q�����`|1,�ӥ��sUPS�Ұ�|1,�ӥ��sUPS�G��|1,|1,|1][2,Buzzer|0,ALARM|0,|0,|0,|0][3,�ӥ��sUPS�Ǹ�|1,�ӥ��sUPS�q�����`|1,�ӥ��sUPS�Ұ�|1,�ӥ��sUPS�G��|1,����|3,���T|3][4,�ӥ��s���ҷū�|1|20.30|1|69.80][5][6,�ӥ��s���q�q��|1|103.40,�ӥ��sUPS��X�q��|1|105.70][7][8,�ӥ��s�����q���q��|1|12.89]:>";

	size_t uiPreIdx = strContent.rfind("���ҷū�");
	size_t uiPostIdx = strContent.rfind("�q���q��");
	std::string strSub = strContent.substr(uiPreIdx, uiPostIdx - uiPreIdx);  

	size_t uiIdx = strSub.find("|");
	uiIdx = strSub.find("|", uiIdx + 1);
	size_t uiIdx2 = strSub.find("|", uiIdx + 1);
	std::string strTemp = strSub.substr(uiIdx + 1, uiIdx2 -1 - uiIdx);

	uiIdx = strSub.find("|", uiIdx2 + 1);	
	uiIdx2 = strSub.find("]", uiIdx + 1);
	std::string strHumid = strSub.substr(uiIdx + 1, uiIdx2 -1 - uiIdx);

	uiIdx = strSub.find("���q�q��", uiIdx2 + 1);	
	uiIdx = strSub.find("|", uiIdx + 1);	
	uiIdx = strSub.find("|", uiIdx + 1);	
	uiIdx2 = strSub.find(",", uiIdx + 1);
	std::string strDi = strSub.substr(uiIdx + 1,  uiIdx2 -1 - uiIdx);

	uiIdx = strSub.find("UPS��X�q��", uiIdx2 + 1);
	uiIdx = strSub.find("|", uiIdx + 1);	
	uiIdx = strSub.find("|", uiIdx + 1);	
	uiIdx2 = strSub.find("]", uiIdx + 1);
	std::string strDo = strSub.substr(uiIdx + 1,  uiIdx2 -1 - uiIdx);
	//printf("%s", strSub.c_str());
	printf("%s %s %s %s", strTemp.c_str(), strHumid.c_str(), strDi.c_str(), strDo.c_str());
	
	return 0;
}

