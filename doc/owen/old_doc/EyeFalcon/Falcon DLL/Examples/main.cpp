#include <stdio.h>
#include <cstdlib>
#include <windows.h>

#define uchar unsigned char

int main(int argc, char *argv[])
{
    HMODULE hInst;
    char    cIP[20];
    uchar   uBuf[1024];
    int     iLen, i;
    
    hInst = LoadLibrary("..\\DLL\\FALCONDLL.dll");    //Load DLL file.
      if(!hInst)
      {
        printf("Dll load fail.\n");
        system("PAUSE");
        return 1;
      }
    void (*pFalcon)(char*, uchar*, int&);    //Declare function pointer.
    //Falcon(char* IP, uchar* buffer, int DataLength)
    (FARPROC&)( pFalcon) = GetProcAddress(hInst, "_Z5GetIRPKcPhRi");  //Get function.
      if(pFalcon == NULL)    //Get function fail.
      {
        printf("Function pointer is NULL\n");
        system("PAUSE");
        return 2;
      }
    //Initial IP and buffer.
    memset(cIP, 0, 20);
    memset(uBuf, 0, 1024);
    printf("Enter your Falcon IP:> ");
    scanf("%s", cIP);
    pFalcon(cIP, uBuf, iLen);    //Execute function.
    //fpFalcon("100.100.67.102", uBuf, iLen);
    
    printf("Length: %d\n", iLen);
      for(i = 0; i < iLen; i++)
      {
        printf("%c", (char)uBuf[i]);
      }
    printf("\n");
    FreeLibrary(hInst);    //Release dll.
    
    system("PAUSE");
    return 0;
}
