@echo off
echo Arresto MySQL 9.5.0...
net stop MySQL_Dialysis
if %errorlevel% equ 0 (
    echo MySQL arrestato con successo!
) else (
    echo Errore nell'arresto di MySQL
)
pause
