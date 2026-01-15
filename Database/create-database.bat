@echo off
echo ========================================
echo Creazione Database Dialysis Management
echo ========================================
echo.

set MYSQL_BIN=C:\Users\gianl\source\repos\DialysisManagement\Database\mysql-9.5.0-winx64\bin
set SQL_FILE=create_database.sql

echo Verifica connessione MySQL...
"%MYSQL_BIN%\mysql.exe" -u root -pDialysisAdmin2026! -e "SELECT VERSION();" > nul 2>&1
if %errorlevel% neq 0 (
    echo ERRORE: Impossibile connettersi a MySQL
    echo Verifica che il servizio sia avviato: net start MySQL_Dialysis
    pause
    exit /b 1
)

echo Connessione OK
echo.
echo Esecuzione script SQL...
echo File: %SQL_FILE%
echo.

"%MYSQL_BIN%\mysql.exe" -u root -pDialysisAdmin2026! < %SQL_FILE%

if %errorlevel% equ 0 (
    echo.
    echo ========================================
    echo Database creato con SUCCESSO!
    echo ========================================
    echo.
    echo Verifica database creato:
    "%MYSQL_BIN%\mysql.exe" -u root -pDialysisAdmin2026! -e "USE dialysis_management; SHOW TABLES;"
    echo.
) else (
    echo.
    echo ERRORE durante la creazione del database
    echo Controlla gli errori sopra
)

pause
