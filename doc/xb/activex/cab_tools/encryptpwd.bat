echo off
if NOT Exist mypassword.pwd goto createpwd
echo Delete mypassword.pwd 
DEL mypassword.pwd

:createpwd
echo Create mypassword.pwd
signcodepwd.exe -e mypassword.pwd "happy2011"
echo Done
pause