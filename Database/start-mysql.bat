@echo off
echo Avvio MySQL 9.5.0...
net start MySQL_Dialysis
if %errorlevel% equ 0 (
    echo MySQL avviato con successo!
) else (
    echo Errore nell'avvio di MySQL
)
pause
