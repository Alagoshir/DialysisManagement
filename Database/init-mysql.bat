@echo off
cls
echo ========================================
echo MySQL 9.5.0 - Inizializzazione
echo ========================================
echo.

REM Percorsi
set MYSQL_DIR=C:\Users\gianl\source\repos\DialysisManagement\Database\mysql-9.5.0-winx64
set MYSQL_BIN=%MYSQL_DIR%\bin
set MYSQL_DATA=%MYSQL_DIR%\data
set MY_INI=C:\Users\gianl\source\repos\DialysisManagement\Database\my.ini

echo MySQL Directory: %MYSQL_DIR%
echo.

REM Verifica file
if not exist "%MYSQL_BIN%\mysqld.exe" (
    echo ERRORE: mysqld.exe non trovato
    pause
    exit /b 1
)

if not exist "%MY_INI%" (
    echo ERRORE: my.ini non trovato
    pause
    exit /b 1
)

echo File trovati: OK
echo.
pause

REM Elimina data se esiste
if exist "%MYSQL_DATA%" (
    echo Eliminazione cartella data esistente...
    rd /s /q "%MYSQL_DATA%"
    timeout /t 2 /nobreak > nul
)

REM Inizializzazione
echo.
echo Inizializzazione database in corso...
echo Attendi 30-60 secondi...
echo.
"%MYSQL_BIN%\mysqld.exe" --defaults-file="%MY_INI%" --initialize-insecure --console

REM Verifica creazione data
timeout /t 3 /nobreak > nul
if not exist "%MYSQL_DATA%" (
    echo.
    echo ERRORE: Cartella data non creata
    pause
    exit /b 1
)

echo.
echo Inizializzazione completata!
echo.

REM Rimuovi servizio esistente
sc query MySQL_Dialysis > nul 2>&1
if %errorlevel% equ 0 (
    echo Rimozione servizio esistente...
    net stop MySQL_Dialysis > nul 2>&1
    sc delete MySQL_Dialysis > nul 2>&1
    timeout /t 2 /nobreak > nul
)

REM Installa servizio
echo Installazione servizio...
"%MYSQL_BIN%\mysqld.exe" --install MySQL_Dialysis --defaults-file="%MY_INI%"
if %errorlevel% neq 0 (
    echo ERRORE installazione servizio
    pause
    exit /b 1
)

echo Servizio installato
echo.

REM Avvia servizio
echo Avvio servizio...
net start MySQL_Dialysis
if %errorlevel% neq 0 (
    echo ERRORE avvio servizio
    pause
    exit /b 1
)

echo Servizio avviato
echo.

REM Attesa stabilizzazione
echo Attesa 10 secondi...
timeout /t 10 /nobreak > nul

REM Configura password
echo Configurazione password...
"%MYSQL_BIN%\mysql.exe" -u root --skip-password -e "ALTER USER 'root'@'localhost' IDENTIFIED BY 'DialysisAdmin2026!'; FLUSH PRIVILEGES;"

REM Test finale
echo.
echo Test connessione...
"%MYSQL_BIN%\mysql.exe" -u root -pDialysisAdmin2026! -e "SELECT VERSION();"

echo.
echo ========================================
echo INSTALLAZIONE COMPLETATA
echo ========================================
echo.
echo Username: root
echo Password: DialysisAdmin2026!
echo Port: 3306
echo.
pause
