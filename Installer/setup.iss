; =============================================
; Dialysis Management System - Installer Script
; Inno Setup 6.x
; =============================================

#define MyAppName "Dialysis Management System"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "Centro Dialisi"
#define MyAppURL "http://www.dialisi.it"
#define MyAppExeName "DialysisManagement.exe"
#define MyAppId "{{A1B2C3D4-E5F6-7890-ABCD-EF1234567890}"

[Setup]
AppId={#MyAppId}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
LicenseFile=..\LICENSE.txt
InfoBeforeFile=..\README_INSTALL.txt
OutputDir=..\Release
OutputBaseFilename=DialysisManagement_Setup_v{#MyAppVersion}
SetupIconFile=..\Resources\app.ico
Compression=lzma2/ultra64
SolidCompression=yes
WizardStyle=modern
PrivilegesRequired=admin
ArchitecturesInstallIn64BitMode=x64
UninstallDisplayIcon={app}\{#MyAppExeName}
UninstallDisplayName={#MyAppName}

[Languages]
Name: "italian"; MessagesFile: "compiler:Languages\Italian.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 6.1; Check: not IsAdminInstallMode

[Files]
; Applicazione principale
Source: "..\bin\Release\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\*.dll"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs
Source: "..\bin\Release\*.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\appsettings.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\bin\Release\nlog.config"; DestDir: "{app}"; Flags: ignoreversion

; Database MySQL Portable
Source: "..\Database\MySQL\*"; DestDir: "{app}\Database\MySQL"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "..\Database\Scripts\*.sql"; DestDir: "{app}\Database\Scripts"; Flags: ignoreversion

; Risorse
Source: "..\Resources\*"; DestDir: "{app}\Resources"; Flags: ignoreversion recursesubdirs createallsubdirs

; Documentazione
Source: "..\Docs\*"; DestDir: "{app}\Docs"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "..\LICENSE.txt"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\README.md"; DestDir: "{app}"; DestName: "README.txt"; Flags: ignoreversion

; .NET Framework 4.8 (se necessario)
; Source: "..\Prerequisites\ndp48-web.exe"; DestDir: "{tmp}"; Flags: deleteafterinstall; Check: not IsNetFramework48Installed

[Dirs]
Name: "{app}\Data"; Permissions: users-full
Name: "{app}\Data\Attachments"; Permissions: users-full
Name: "{app}\Backups"; Permissions: users-full
Name: "{app}\Logs"; Permissions: users-full
Name: "{app}\Reports"; Permissions: users-full
Name: "{app}\Temp"; Permissions: users-full
Name: "{app}\Database\MySQL\data"; Permissions: users-full

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\Documentazione"; Filename: "{app}\Docs\Manuale_Utente.pdf"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon

[Run]
; Installa MySQL Service
Filename: "{app}\Database\MySQL\bin\mysqld.exe"; Parameters: "--install DialysisMySQL --defaults-file=""{app}\Database\MySQL\my.ini"""; WorkingDir: "{app}\Database\MySQL\bin"; Flags: runhidden; StatusMsg: "Installazione servizio MySQL..."; Check: not MySQLServiceExists

; Avvia MySQL Service
Filename: "net"; Parameters: "start DialysisMySQL"; Flags: runhidden waituntilterminated; StatusMsg: "Avvio servizio MySQL..."; Check: not MySQLServiceRunning

; Inizializza database
Filename: "{app}\Database\MySQL\bin\mysql.exe"; Parameters: "-u root < ""{app}\Database\Scripts\01_CreateDatabase.sql"""; WorkingDir: "{app}\Database\MySQL\bin"; Flags: runhidden waituntilterminated; StatusMsg: "Creazione database..."

Filename: "{app}\Database\MySQL\bin\mysql.exe"; Parameters: "-u root dialysis_db < ""{app}\Database\Scripts\02_InitialData.sql"""; WorkingDir: "{app}\Database\MySQL\bin"; Flags: runhidden waituntilterminated; StatusMsg: "Inizializzazione dati..."

; Avvia applicazione
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[UninstallRun]
; Ferma servizio MySQL
Filename: "net"; Parameters: "stop DialysisMySQL"; Flags: runhidden; RunOnceId: "StopMySQL"

; Rimuovi servizio MySQL
Filename: "{app}\Database\MySQL\bin\mysqld.exe"; Parameters: "--remove DialysisMySQL"; Flags: runhidden; RunOnceId: "RemoveMySQL"

[Code]
// Funzioni helper

function IsNetFramework48Installed: Boolean;
var
  Release: Cardinal;
begin
  Result := False;
  if RegQueryDWordValue(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full', 'Release', Release) then
  begin
    Result := Release >= 528040; // .NET Framework 4.8 = 528040
  end;
end;

function MySQLServiceExists: Boolean;
var
  ResultCode: Integer;
begin
  Exec('sc', 'query DialysisMySQL', '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
  Result := (ResultCode = 0);
end;

function MySQLServiceRunning: Boolean;
var
  ResultCode: Integer;
  Output: AnsiString;
begin
  Result := False;
  if MySQLServiceExists then
  begin
    Exec('sc', 'query DialysisMySQL', '', SW_HIDE, ewWaitUntilTerminated, ResultCode);
    Result := (ResultCode = 0);
  end;
end;

procedure InitializeWizard;
begin
  // Personalizzazioni wizard se necessarie
end;

function InitializeSetup(): Boolean;
begin
  Result := True;
  
  // Verifica .NET Framework
  if not IsNetFramework48Installed then
  begin
    if MsgBox('Questa applicazione richiede Microsoft .NET Framework 4.8 o superiore.' + #13#10 + 
              'Vuoi scaricare e installare .NET Framework ora?', 
              mbConfirmation, MB_YESNO) = IDYES then
    begin
      ShellExec('open', 'https://dotnet.microsoft.com/download/dotnet-framework/net48', '', '', SW_SHOW, ewNoWait, ResultCode);
      Result := False;
    end
    else
      Result := False;
  end;
end;

procedure CurStepChanged(CurStep: TSetupStep);
var
  ConfigFile: String;
  ConnectionString: String;
begin
  if CurStep = ssPostInstall then
  begin
    // Aggiorna connection string nel config
    ConfigFile := ExpandConstant('{app}\app.config');
    ConnectionString := 'Server=localhost;Port=3306;Database=dialysis_db;Uid=dialysis_user;Pwd=Dialysis@2026;SslMode=none;';
    
    // Qui si potrebbe aggiungere logica per modificare il file config
  end;
end;

procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
var
  Response: Integer;
begin
  if CurUninstallStep = usUninstall then
  begin
    Response := MsgBox('Vuoi conservare il database e i file di backup?' + #13#10 + 
                       'Scegli No per eliminarli completamente.', 
                       mbConfirmation, MB_YESNO or MB_DEFBUTTON1);
    
    if Response = IDNO then
    begin
      // Elimina database e backup
      DelTree(ExpandConstant('{app}\Database\MySQL\data'), True, True, True);
      DelTree(ExpandConstant('{app}\Backups'), True, True, True);
      DelTree(ExpandConstant('{app}\Data'), True, True, True);
    end;
  end;
end;
