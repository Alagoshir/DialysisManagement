================================================================================
  DIALYSIS MANAGEMENT SYSTEM - ISTRUZIONI DI INSTALLAZIONE
================================================================================

Versione: 1.0.0
Data: 14/01/2026

================================================================================
REQUISITI DI SISTEMA
================================================================================

REQUISITI MINIMI:
- Sistema Operativo: Windows 10 (64-bit) o superiore
- Processore: Intel Core i3 o equivalente
- RAM: 4 GB
- Spazio su disco: 2 GB disponibili
- Risoluzione schermo: 1366x768 o superiore
- Microsoft .NET Framework 4.8 o superiore

REQUISITI CONSIGLIATI:
- Sistema Operativo: Windows 11 (64-bit)
- Processore: Intel Core i5 o superiore
- RAM: 8 GB o superiore
- Spazio su disco: 5 GB disponibili
- Risoluzione schermo: 1920x1080 o superiore
- SSD per migliori prestazioni

================================================================================
PREREQUISITI
================================================================================

1. Microsoft .NET Framework 4.8
   - Il programma di installazione verificherà automaticamente la presenza
   - Se non installato, verrà chiesto di scaricare e installare
   - Download: https://dotnet.microsoft.com/download/dotnet-framework/net48

2. Privilegi di Amministratore
   - L'installazione richiede diritti di amministratore per:
     * Installare il servizio MySQL
     * Creare cartelle di sistema
     * Configurare permessi file

================================================================================
PROCEDURA DI INSTALLAZIONE
================================================================================

1. AVVIO INSTALLAZIONE
   - Eseguire "DialysisManagement_Setup_v1.0.0.exe"
   - Cliccare "Sì" quando richiesto dal Controllo Account Utente (UAC)

2. CONFIGURAZIONE
   - Seguire le istruzioni della procedura guidata
   - Selezionare la cartella di installazione (default: C:\Program Files\Dialysis Management System)
   - Scegliere se creare icone sul desktop

3. INSTALLAZIONE COMPONENTI
   Il setup installerà automaticamente:
   - Applicazione principale
   - Database MySQL Portable
   - File di configurazione
   - Documentazione

4. INIZIALIZZAZIONE DATABASE
   - Il setup creerà e configurerà automaticamente il database
   - Verrà creato un utente amministratore di default

5. COMPLETAMENTO
   - Al termine dell'installazione, l'applicazione può essere avviata automaticamente

================================================================================
PRIMO AVVIO
================================================================================

CREDENZIALI DI ACCESSO DEFAULT:
   Username: admin
   Password: Admin@2026

IMPORTANTE: Cambiare la password di default al primo accesso!

PASSAGGI INIZIALI:
1. Accedere con le credenziali di default
2. Cambiare immediatamente la password amministratore
3. Configurare le impostazioni del centro (menu Impostazioni)
4. Creare utenti aggiuntivi (medici, infermieri)
5. Eseguire un backup di test

================================================================================
CONFIGURAZIONE POST-INSTALLAZIONE
================================================================================

1. PERCORSI IMPORTANTI:
   - Applicazione: C:\Program Files\Dialysis Management System\
   - Database: C:\Program Files\Dialysis Management System\Database\MySQL\
   - Backup: C:\Program Files\Dialysis Management System\Backups\
   - Log: C:\Program Files\Dialysis Management System\Logs\
   - Allegati: C:\Program Files\Dialysis Management System\Data\Attachments\

2. SERVIZIO MYSQL:
   - Nome servizio: DialysisMySQL
   - Porta: 3306
   - Avvio automatico: Sì
   
   Comandi utili:
   - Avvio: net start DialysisMySQL
   - Stop: net stop DialysisMySQL
   - Stato: sc query DialysisMySQL

3. BACKUP AUTOMATICO:
   - Configurare orario backup automatico (default: 02:00)
   - Verificare spazio disco sufficiente
   - Configurare eventuale backup su cloud

4. FIREWALL:
   - Se necessario, autorizzare l'applicazione nel firewall Windows
   - MySQL usa la porta 3306 (solo locale, non esposta in rete)

================================================================================
MIGRAZIONE DATI (se presente sistema precedente)
================================================================================

1. Esportare dati dal sistema precedente
2. Utilizzare la funzione "Importa Dati" nel menu Strumenti
3. Verificare l'integrità dei dati importati
4. Eseguire un backup completo dopo l'importazione

================================================================================
RISOLUZIONE PROBLEMI
================================================================================

PROBLEMA: L'applicazione non si avvia
SOLUZIONE: 
- Verificare che .NET Framework 4.8 sia installato
- Verificare che il servizio MySQL sia avviato
- Controllare i log in: C:\Program Files\Dialysis Management System\Logs\

PROBLEMA: Errore di connessione al database
SOLUZIONE:
- Verificare che il servizio DialysisMySQL sia in esecuzione
- Controllare i file di configurazione (app.config)
- Verificare permessi cartella Database

PROBLEMA: Impossibile accedere con le credenziali di default
SOLUZIONE:
- Verificare che il database sia stato inizializzato correttamente
- Eseguire manualmente gli script SQL in: Database\Scripts\
- Contattare il supporto tecnico

================================================================================
DISINSTALLAZIONE
================================================================================

1. Utilizzare "Installazione applicazioni" di Windows
2. Selezionare "Dialysis Management System"
3. Cliccare "Disinstalla"
4. Scegliere se conservare database e backup

NOTA: Per conservare i dati, scegliere "Sì" quando richiesto
      Per rimozione completa, scegliere "No"

================================================================================
SUPPORTO TECNICO
================================================================================

Per assistenza tecnica contattare:

Email: support@dialisi.it
Telefono: 081-1234567
Orari: Lun-Ven 9:00-18:00

Documentazione completa disponibile in: 
C:\Program Files\Dialysis Management System\Docs\

================================================================================
NOTE LEGALI
================================================================================

© 2026 Centro Dialisi. Tutti i diritti riservati.

Questo software è distribuito "così com'è" senza garanzie di alcun tipo.
L'utilizzo del software è soggetto all'accettazione dei termini di licenza.

================================================================================
