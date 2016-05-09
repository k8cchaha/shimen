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

	//std::string strContent = "<IR:[1,惠賓樓環境溫度|1,惠賓樓環境濕度|1,惠賓樓市電電壓|1,惠賓樓UPS輸出電壓|1,發電機運轉|1,發電機過載|1,發電機過熱|1,發電機油位不足|1,|1,|1][2,Buzzer|0,ALARM|0,|0,|0,|0][3,發電機運轉|1,發電機過載|1,發電機過熱|1,發電機油位不足|1,數位輸入點五|3,數位輸入點六|3][4,惠賓樓環境溫度|1|24.10|1|54.90][5][6,惠賓樓市電電壓|1|123.20,惠賓樓UPS輸出電壓|1|112.30][7][8,惠賓樓環控電源電壓|1|12.89]:>";
	//std::string strContent = "<IR:[1,沉澱池環境溫度|1,沉澱池環境濕度|1,沉澱池市電電壓|1,沉澱池UPS輸出電壓|1,沉澱池UPS旁路|1,沉澱池UPS電池異常|1,沉澱池UPS啟動|1,沉澱池UPS故障|1,漏水一|1,漏水二|1][2,Buzzer|0,ALARM|0,|0,|0,|0][3,沉澱池UPS旁路|3,沉澱池UPS電池異常|3,沉澱池UPS啟動|3,沉澱池UPS故障|1,煙霧|3,冷氣|3][4,沉澱池環境溫度|1|26.10|1|70.70][5][6,沉澱池市電電壓|1|115.20,沉澱池UPS輸出電壓|1|103.80][7,漏水一|1|23.36][8,沉澱池環控電源電壓異常|1|12.59]:>";
	//std::string strContent = "<IR:[1,大灣環境溫度|1,大灣環境濕度|1,大灣市電電壓|1,大灣UPS輸出電壓|2,大灣UPS旁路|1,大灣UPS電池異常|1,大灣UPS啟動|2,大灣UPS故障|1,|1,|1][2,Buzzer|2,ALARM|1,|0,|0,|0][3,大灣UPS旁路|1,大灣UPS電池異常|1,大灣UPS啟動|2,大灣UPS故障|1,煙霧|3,門禁|3][4,大灣環境溫度|1|22.10|1|88.70][5][6,大灣市電電壓|1|113.30,大灣UPS輸出電壓|2| 0.00][7][8,大灣環控電源電壓異常|1|12.68]:>";
	//std::string strContent = "<IR:[1,霞雲環境溫度|1,霞雲環境濕度|1,霞雲市電電壓|1,霞雲UPS輸出電壓|1,機櫃溫度 1|1,機櫃溫度 2|1,|1,|1,|1,|1][2,Buzzer|0,ALARM|0,|0,|0,|0][3,霞雲UPS市電|1,霞雲UPS旁路|3,霞雲UPS電池電位|3,門禁|3,煙霧消防|3,冷氣|3][4,霞雲環境溫度|1|21.90|1|86.10][5][6,霞雲市電電壓|1|110.90,霞雲UPS輸出電壓|1|107.10][7][8,電源電壓|1|12.87]:>";
	//std::string strContent = "<IR:[1,太平山環境溫度|1,太平山環境濕度|1,太平山市電電壓|1,太平山UPS輸出電壓|1,太平山UPS旁路|1,太平山UPS電池異常|1,太平山UPS啟動|1,太平山UPS故障|1,|1,|1][2,Buzzer|0,ALARM|0,|0,|0,|0][3,太平山UPS旁路|1,太平山UPS電池異常|1,太平山UPS啟動|1,太平山UPS故障|1,煙霧|3,門禁|3][4,太平山環境溫度|1|20.30|1|69.80][5][6,太平山市電電壓|1|103.40,太平山UPS輸出電壓|1|105.70][7][8,太平山環控電源電壓|1|12.89]:>";

	size_t uiPreIdx = strContent.rfind("環境溫度");
	size_t uiPostIdx = strContent.rfind("電源電壓");
	std::string strSub = strContent.substr(uiPreIdx, uiPostIdx - uiPreIdx);  

	size_t uiIdx = strSub.find("|");
	uiIdx = strSub.find("|", uiIdx + 1);
	size_t uiIdx2 = strSub.find("|", uiIdx + 1);
	std::string strTemp = strSub.substr(uiIdx + 1, uiIdx2 -1 - uiIdx);

	uiIdx = strSub.find("|", uiIdx2 + 1);	
	uiIdx2 = strSub.find("]", uiIdx + 1);
	std::string strHumid = strSub.substr(uiIdx + 1, uiIdx2 -1 - uiIdx);

	uiIdx = strSub.find("市電電壓", uiIdx2 + 1);	
	uiIdx = strSub.find("|", uiIdx + 1);	
	uiIdx = strSub.find("|", uiIdx + 1);	
	uiIdx2 = strSub.find(",", uiIdx + 1);
	std::string strDi = strSub.substr(uiIdx + 1,  uiIdx2 -1 - uiIdx);

	uiIdx = strSub.find("UPS輸出電壓", uiIdx2 + 1);
	uiIdx = strSub.find("|", uiIdx + 1);	
	uiIdx = strSub.find("|", uiIdx + 1);	
	uiIdx2 = strSub.find("]", uiIdx + 1);
	std::string strDo = strSub.substr(uiIdx + 1,  uiIdx2 -1 - uiIdx);
	//printf("%s", strSub.c_str());
	printf("%s %s %s %s", strTemp.c_str(), strHumid.c_str(), strDi.c_str(), strDo.c_str());
	
	return 0;
}

