@echo off
echo Riavvio MySQL 9.5.0...
net stop MySQL_Dialysis
timeout /t 2 /nobreak > nul
net start MySQL_Dialysis
if %errorlevel% equ 0 (
    echo MySQL riavviato con successo!
) else (
    echo Errore nel riavvio di MySQL
)
pause
