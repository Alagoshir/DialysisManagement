# DIALYSIS MANAGEMENT SYSTEM
## Manuale Utente v1.0.0

---

## INDICE

1. [Introduzione](#introduzione)
2. [Accesso al Sistema](#accesso-al-sistema)
3. [Dashboard Principale](#dashboard-principale)
4. [Gestione Pazienti](#gestione-pazienti)
5. [Sedute Dialitiche](#sedute-dialitiche)
6. [Esami Laboratorio](#esami-laboratorio)
7. [Esami Strumentali](#esami-strumentali)
8. [Prescrizioni e Terapie](#prescrizioni-e-terapie)
9. [Report e Statistiche](#report-e-statistiche)
10. [Backup e Sicurezza](#backup-e-sicurezza)
11. [Impostazioni](#impostazioni)
12. [FAQ](#faq)

---

## 1. INTRODUZIONE

### Scopo del Software

Dialysis Management System è un'applicazione completa per la gestione di un centro dialisi ospedaliero pubblico italiano, specificamente progettata per la Regione Campania.

### Funzionalità Principali

- ✅ Anagrafica pazienti completa con consensi GDPR
- ✅ Gestione sedute dialitiche con parametri vitali
- ✅ Gestione esami laboratorio e strumentali
- ✅ Prescrizioni dialitiche e terapie farmacologiche
- ✅ Accessi vascolari con storico
- ✅ Report clinici e statistiche
- ✅ Export flussi SDO e Registro Dialisi Regionale
- ✅ Backup automatico e manuale
- ✅ Audit trail completo per sicurezza

### Requisiti Minimi

- Windows 10 64-bit o superiore
- 4 GB RAM (8 GB consigliati)
- 2 GB spazio disco disponibile
- Risoluzione schermo 1366x768 (1920x1080 consigliata)
- .NET Framework 4.8

---

## 2. ACCESSO AL SISTEMA

### Login

1. Avviare l'applicazione dall'icona sul desktop o dal menu Start
2. Inserire username e password
3. Cliccare "Accedi"

**Credenziali di default** (cambiare al primo accesso):
- Username: `admin`
- Password: `Admin@2026`

### Ruoli Utente

Il sistema prevede 4 livelli di accesso:

| Ruolo | Permessi |
|-------|----------|
| **Admin** | Accesso completo, gestione utenti, backup, configurazioni |
| **Medico** | Accesso completo ai dati clinici, prescrizioni, report |
| **Infermiere** | Gestione sedute dialitiche, inserimento parametri vitali |
| **Sola lettura** | Solo visualizzazione dati, nessuna modifica |

### Cambio Password

1. Menu **Utente** → **Cambia Password**
2. Inserire password attuale
3. Inserire nuova password (min. 8 caratteri)
4. Confermare nuova password
5. Cliccare "Salva"

### Timeout Sessione

- Timeout predefinito: 30 minuti di inattività
- Avviso automatico 5 minuti prima della scadenza
- Logout automatico alla scadenza

---

## 3. DASHBOARD PRINCIPALE

La dashboard fornisce una panoramica completa dell'attività del centro.

### Sezioni Dashboard

**Statistiche Principali**:
- Pazienti totali attivi
- Sedute programmate oggi
- Sedute completate oggi
- Esami in scadenza

**Grafici e Trend**:
- Andamento sedute ultimi 30 giorni
- Distribuzione pazienti per tipo trattamento
- Media Kt/V mensile
- Trend emoglobina media

**Alert e Notifiche**:
- ⚠️ Esami laboratorio fuori range
- ⚠️ Esami strumentali in scadenza
- ⚠️ Manutenzioni apparecchiature dovute
- ⚠️ Pazienti senza seduta programmata

**Quick Actions**:
- Registra nuova seduta
- Aggiungi paziente
- Visualizza report giornaliero
- Esegui backup

---

## 4. GESTIONE PAZIENTI

### Anagrafica Paziente

#### Dati Anagrafici
- Cognome e Nome (obbligatori)
- Codice Fiscale (con validazione automatica)
- Data e luogo di nascita
- Sesso
- Indirizzo completo
- Contatti (telefono, cellulare, email)
- Numero tessera sanitaria
- Medico di base

#### Dati Clinici
- Gruppo sanguigno
- Peso secco e altezza
- Data inizio dialisi
- Nefropatia di base
- Tipo trattamento (HD/HDF/HF)
- Frequenza settimanale

#### Contumaciali
- Flag HBsAg+ (Epatite B)
- Flag HCV+ (Epatite C)
- Flag HIV+

⚠️ **IMPORTANTE**: I pazienti contumaciali sono evidenziati con colori specifici

#### Lista Trapianto
- In lista trapianto (Sì/No)
- Data inserimento lista
- Centro trapianti

#### Consensi GDPR
- Consenso privacy
- Consenso trattamento dati
- Data consenso
- Possibilità di stampare moduli

### Accessi Vascolari

#### Tipi di Accesso
- **FAV**: Fistola Artero-Venosa
- **CVC**: Catetere Venoso Centrale
- **Protesi**: Protesi vascolare

#### Dati Accesso
- Tipo accesso
- Sede anatomica
- Data creazione/posizionamento
- Chirurgo e ospedale
- Stato (attivo/rimosso/malfunzionante)
- Portata (per FAV, in ml/min)
- Complicanze (infezione/stenosi/trombosi)

#### Storico
- Visualizzazione completa di tutti gli accessi vascolari
- Alert per accessi malfunzionanti
- Grafici evolutivi

### Ricerca Pazienti

**Criteri di ricerca**:
- Codice fiscale
- Cognome e nome
- Numero tessera sanitaria
- Filtri: attivi/non attivi, contumaciali

**Filtri avanzati**:
- Per tipo trattamento
- Per accesso vascolare
- Per presenza in lista trapianto

---

## 5. SEDUTE DIALITICHE

### Programmazione Seduta

1. Selezionare paziente
2. Scegliere data e orario
3. Assegnare postazione (sala/stazione/monitor)
4. Verificare prescrizione attiva
5. Salvare programmazione

### Registrazione Seduta

#### Pre-Dialisi
- Orario inizio
- Peso pre-dialisi
- Pressione arteriosa pre
- Temperatura
- Accesso vascolare utilizzato
- Tipo trattamento
- Composizione bagno
- Flussi pompa (QB) e dialysato (QD)
- Anticoagulazione (tipo ed eparina UI)

#### Intra-Dialisi
Registrazione parametri vitali ogni 30-60 minuti:
- Orario rilevazione
- Pressione arteriosa (sistolica/diastolica)
- Frequenza cardiaca
- Temperatura
- Saturazione O2
- Note eventuali

#### Post-Dialisi
- Orario fine
- Peso post-dialisi
- Pressione arteriosa post
- UF raggiunta
- Kt/V calcolato
- URR (Urea Reduction Ratio)
- Complicanze intra-dialitiche
- Note infermieristiche

#### Stato Seduta
- **Programmata**: seduta pianificata
- **In corso**: dialisi in svolgimento
- **Completata**: seduta terminata con successo
- **Annullata**: seduta non effettuata

### Calcolo Parametri Adeguatezza

**Kt/V** (calcolato automaticamente):
- K = clearance dializzatore
- t = tempo dialisi
- V = volume distribuzione urea

**URR** (Urea Reduction Ratio):
- URR = (1 - [Urea post / Urea pre]) × 100

### Visualizzazione Storico

- Lista tutte le sedute del paziente
- Grafici evoluzione parametri
- Export dati per periodo

---

## 6. ESAMI LABORATORIO

### Inserimento Esami

1. Selezionare paziente
2. Inserire data prelievo
3. Compilare valori esami disponibili
4. Salvare

### Categorie Esami

#### Emocromo
- Emoglobina (Hb)
- Ematocrito (Ht)
- Globuli rossi (RBC)
- Globuli bianchi (WBC)
- Piastrine (PLT)

#### Funzionalità Renale
- Creatinina
- Azotemia
- Acido urico

#### Elettroliti
- Sodio (Na)
- Potassio (K)
- Calcio (Ca)
- Fosforo (P)
- Magnesio (Mg)
- Cloro (Cl)
- Bicarbonato (HCO3)

#### Metabolismo Minerale
- PTH (paratormone)
- Vitamina D

#### Metabolismo Ferro
- Ferro sierico
- Ferritina
- Transferrina
- Saturazione transferrina

#### Funzionalità Epatica
- AST (GOT)
- ALT (GPT)
- Gamma-GT
- Bilirubina totale
- Albumina sierica

#### Lipidi
- Colesterolo totale
- Trigliceridi
- HDL
- LDL

#### Glicemia
- Glicemia
- Emoglobina glicata (HbA1c)

#### Sierologie
- HBsAg
- Anti-HCV
- Anti-HIV

#### Altro
- PCR (proteina C-reattiva)

### Valori di Riferimento

- Valori normali pre-impostati
- Possibilità di personalizzazione
- Alert automatici per valori fuori range

### Grafici Trend

- Visualizzazione andamento temporale
- Confronto parametri multipli
- Export grafici

---

## 7. ESAMI STRUMENTALI

### Tipi di Esami Supportati

- **ECG** (Elettrocardiogramma)
- **Ecocardiogramma**
- **Ecografia** (addome, vasi, altro)
- **RX Torace**
- **TAC** (Tomografia Computerizzata)
- **RMN** (Risonanza Magnetica)
- Altro (personalizzabile)

### Inserimento Esame

1. Selezionare paziente
2. Scegliere tipo esame
3. Inserire data esecuzione
4. Compilare dati esame:
   - Medico richiedente
   - Indicazione clinica
   - Centro esecuzione
   - Data referto
   - Esito/referto testuale
5. Allegare file (PDF, DICOM, immagini)
6. Salvare

### Gestione Allegati

**Formati supportati**:
- PDF
- DICOM (.dcm)
- Immagini (JPG, PNG, BMP)
- Documenti Office (DOC, DOCX, XLS, XLSX)

**Caratteristiche**:
- Upload multiplo
- Crittografia automatica (AES-256)
- Anteprima file
- Download sicuro
- Calcolo hash per integrità

### Esami Interni

- Flag "Esame Interno" per esami eseguiti nella struttura
- Codice nomenclatore per SDO
- Tracciamento ai fini billing

### Scadenze Esami

- Data scadenza validità esame
- Alert automatici X giorni prima
- Flag "Da ripetere"

---

## 8. PRESCRIZIONI E TERAPIE

### Prescrizione Dialitica

#### Parametri Trattamento
- Tipo trattamento (HD/HDF/HF)
- Frequenza settimanale
- Durata sessione (minuti)
- Data inizio/fine validità

#### Composizione Bagno
- Tipo bagno
- Bicarbonato (mEq/L)
- Potassio (mEq/L)
- Calcio (mEq/L)
- Sodio (mEq/L)

#### Parametri Dialisi
- Flusso pompa sangue (QB ml/min)
- Flusso dialysato (QD ml/min)
- UF target (litri)
- Peso secco target (kg)

#### Anticoagulazione
- Tipo anticoagulazione
- Dose anticoagulante (UI)

### Terapia Farmacologica

#### Dati Farmaco
- Nome commerciale
- Principio attivo
- Dosaggio
- Via di somministrazione (orale/EV/SC/IM)
- Frequenza
- Orario assunzione

#### Somministrazione
- **Intra-dialisi**: farmaci somministrati durante seduta
- **Extra-dialisi**: terapia domiciliare

#### Farmaci Comuni in Dialisi
- Eritropoietina (EPO)
- Ferro EV
- Chelanti del fosforo
- Antipertensivi
- Vitamine

### Vaccinazioni

#### Tipi Vaccino
- HBV (Epatite B) - ciclo completo e richiami
- Influenza (stagionale)
- COVID-19
- Pneumococco

#### Dati Vaccinazione
- Data somministrazione
- Numero richiamo
- Lotto vaccino
- Sede inoculazione
- Data scadenza richiamo

#### Alert Richiami
- Notifica automatica scadenza richiamo
- Calendario vaccinazioni

---

## 9. REPORT E STATISTICHE

### Report Clinici

#### Report Paziente Singolo
- **Cartella clinica completa**:
  - Anagrafica
  - Storico sedute
  - Esami laboratorio
  - Esami strumentali
  - Prescrizioni attive
  - Terapia in corso

- **Report periodico**:
  - Selezionare periodo
  - Include tutti gli eventi
  - Export PDF

#### Report Laboratorio
- Trend parametri selezionati
- Grafici evoluzione
- Tabelle valori

### Report Attività Centro

#### Report Giornaliero
- Sedute programmate
- Sedute completate
- Sedute annullate
- Pazienti assenti
- Complicanze registrate

#### Report Mensile
- Numero totale sedute
- Ore dialisi erogate
- Distribuzione per tipo trattamento
- Indicatori qualità (Kt/V medio, Hb media)
- Utilizzo postazioni

#### Report Annuale
- Statistiche complete
- Confronto anni precedenti
- Indicatori performance

### Statistiche e Indicatori

#### Indicatori Clinici
- Kt/V medio per paziente
- Emoglobina media
- % pazienti con Hb in range target
- % pazienti con fosforo controllato
- % pazienti con PTH in range

#### Indicatori Organizzativi
- Tasso occupazione postazioni
- Tempo medio seduta
- % complicanze intra-dialitiche
- % accessi vascolari FAV vs CVC

### Export Dati

#### Flussi Ministeriali

**SDO (Scheda Dimissione Ospedaliera)**:
- Generazione file SDO per periodo
- Include esami interni con codici nomenclatore
- Formato standard ministeriale
- Validazione automatica dati

**Registro Dialisi Regionale Campania**:
- Export CSV/XML secondo tracciato regionale
- Dati aggregati per centro
- Frequenza invio: trimestrale
- Controlli completezza dati

#### Export Personalizzati
- CSV per elaborazioni Excel
- XML per integrazioni esterne
- PDF per stampa report

---

## 10. BACKUP E SICUREZZA

### Backup Database

#### Backup Manuale

1. Menu **Strumenti** → **Backup e Ripristino**
2. Cliccare "Crea Backup Ora"
3. Opzioni:
   - Comprimi (ZIP) - riduce dimensione
   - Cripta (AES-256) - sicurezza
   - Upload su cloud - automatico
4. Inserire note opzionali
5. Confermare

Percorso salvataggio: `C:\Program Files\Dialysis Management System\Backups\`

#### Backup Automatico

**Configurazione**:
1. Menu **Impostazioni** → **Backup**
2. Abilitare "Backup automatico"
3. Impostare orario (es: 02:00)
4. Scegliere giorni conservazione (default: 30)
5. Opzionale: configurare upload cloud
6. Salvare

**Frequenze disponibili**:
- Giornaliero
- Settimanale
- Mensile

#### Backup su Cloud

**Provider supportati**:
- Google Drive
- Microsoft OneDrive
- Dropbox
- FTP/SFTP personalizzato

**Configurazione Cloud**:
1. Scegliere provider
2. Autenticare account
3. Selezionare cartella destinazione
4. Testare connessione
5. Abilitare upload automatico

### Ripristino Database

⚠️ **ATTENZIONE**: Il ripristino sovrascrive tutti i dati attuali!

**Procedura**:
1. Menu **Strumenti** → **Backup e Ripristino**
2. Selezionare backup da elenco o file esterno
3. Verificare data e dimensione backup
4. Cliccare "Ripristina Selezionato"
5. Confermare operazione
6. Attendere completamento
7. L'applicazione si riavvierà automaticamente

**Consiglio**: Creare sempre un backup corrente prima di ripristinare!

### Pulizia Backup Vecchi

- Eliminazione automatica backup oltre soglia giorni
- Backup su cloud NON vengono eliminati
- Operazione manuale: "Pulisci Backup Vecchi"

### Sicurezza

#### Crittografia
- Tutti i dati sensibili sono crittografati (AES-256)
- Password utenti con hash BCrypt
- Allegati file crittografati su disco
- Backup crittografati (opzionale)

#### Audit Trail
- Tracciamento completo di tutte le operazioni
- Log login/logout utenti
- Registro modifiche dati (chi, quando, cosa)
- Conservazione log: 90 giorni (configurabile)
- Non eliminabile dagli utenti

#### Controllo Accessi
- Permessi granulari per ruolo
- Blocco account dopo 5 tentativi falliti
- Timeout sessione automatico
- Password con requisiti minimi

#### Conformità GDPR
- Consensi privacy tracciati
- Diritto accesso dati paziente
- Diritto cancellazione (richiesta formale)
- Log accessi dati personali

---

## 11. IMPOSTAZIONI

### Impostazioni Generali

#### Dati Centro
- Nome centro dialisi
- Indirizzo completo
- Telefono e email
- Logo centro (per report)

#### Configurazioni Applicazione
- Lingua interfaccia (Italiano)
- Formato data (gg/mm/aaaa)
- Formato orario (24h)
- Timeout sessione (minuti)

### Impostazioni Cliniche

#### Valori Target
- Range emoglobina target (g/dL)
- Range fosforo target (mg/dL)
- Range PTH target (pg/mL)
- Kt/V minimo accettabile

#### Alert
- Giorni preavviso scadenza esami
- Giorni preavviso manutenzioni
- Alert valori lab fuori range (On/Off)

### Gestione Utenti

⚠️ **Solo per amministratori**

#### Creazione Utente
1. Menu **Impostazioni** → **Utenti**
2. Cliccare "Nuovo Utente"
3. Compilare:
   - Username (univoco)
   - Nome e Cognome
   - Email
   - Ruolo
   - Password temporanea
4. Flag "Forza cambio password al primo accesso"
5. Salvare

#### Modifica Utente
- Cambio ruolo
- Disattivazione account
- Reset password
- Sblocco account

#### Eliminazione Utente
- Solo se nessuna attività registrata
- Altrimenti: disattivare account

### Gestione Apparecchiature

#### Registrazione Monitor
- Numero monitor (univoco)
- Sala e postazione
- Marca e modello
- Matricola
- Anno installazione
- Stato (attivo/inattivo)

#### Manutenzioni
- Data ultima manutenzione
- Tipo (ordinaria/straordinaria/riparazione)
- Tecnico responsabile
- Fornitore
- Costo
- Data prossima manutenzione

#### Alert Scadenze
- Notifica X giorni prima scadenza
- Blocco utilizzo se manutenzione scaduta (configurabile)

---

## 12. FAQ

### Domande Generali

**D: Come cambio la password?**
R: Menu Utente → Cambia Password

**D: Quanto dura la sessione prima del logout automatico?**
R: 30 minuti di inattività (configurabile da admin)

**D: Posso accedere da più computer contemporaneamente?**
R: Sì, ogni utente può avere più sessioni attive

**D: Come recupero la password dimenticata?**
R: Contattare l'amministratore di sistema per reset

### Gestione Pazienti

**D: Posso eliminare un paziente?**
R: No, si può solo disattivare. Conservazione dati per obblighi legali.

**D: Come gestisco un paziente che ha trapiantato?**
R: Impostare data uscita e motivo "Trapianto", disattivare paziente

**D: Come inserisco un paziente contumaciale?**
R: Abilitare i relativi flag (HBsAg+/HCV+/HIV+) nella scheda anagrafica

### Sedute Dialitiche

**D: Posso modificare una seduta già completata?**
R: Solo medici e admin, entro 24h. Modifiche tracciate in audit log.

**D: Come annullo una seduta programmata?**
R: Selezionare seduta, cambiare stato in "Annullata", inserire motivo

**D: Calcolo automatico Kt/V non funziona**
R: Verificare che siano inseriti: urea pre/post, durata dialisi, peso

### Esami

**D: Quali formati file posso allegare?**
R: PDF, DICOM, JPG, PNG, DOC, DOCX, XLS, XLSX (max 50MB per file)

**D: Gli allegati sono sicuri?**
R: Sì, crittografati AES-256 e hash per integrità

**D: Come imposto alert per esami in scadenza?**
R: Impostazioni → Alert → Giorni preavviso scadenza esami

### Backup

**D: Dove vengono salvati i backup?**
R: `C:\Program Files\Dialysis Management System\Backups\`

**D: Quanto occupa un backup?**
R: Circa 50-200 MB se compresso, dipende da volume dati

**D: I backup su cloud sono sicuri?**
R: Sì, crittografati prima dell'upload

**D: Posso ripristinare un backup su un altro PC?**
R: Sì, copiare file backup e usare "Ripristina da file"

### Problemi Tecnici

**D: L'applicazione è lenta**
R: Possibili cause:
- Disco pieno (verificare spazio)
- Troppi log accumulati (pulizia automatica)
- Database da ottimizzare (contattare supporto)

**D: Errore "Impossibile connettersi al database"**
R: Verificare:
1. Servizio MySQL attivo: `sc query DialysisMySQL`
2. Se fermo: `net start DialysisMySQL`
3. Se persiste: contattare supporto

**D: Non riesco a stampare report**
R: Verificare:
- Stampante configurata in Windows
- Anteprima PDF funziona? → problema stampante
- Generazione PDF fallisce? → problema iTextSharp

**D: Manca voce di menu**
R: Verificare permessi ruolo utente (alcune funzioni solo admin)

---

## SUPPORTO TECNICO

**Email**: support@dialisi.it  
**Telefono**: 081-1234567  
**Orari**: Lun-Ven 9:00-18:00

**In caso di emergenza** (fuori orario):
- Telefono reperibilità: 340-1234567

**Richiesta assistenza**:
Fornire sempre:
- Versione software
- Descrizione problema
- Screenshot errore (se presente)
- File log (Logs\DialysisManagement_[data].log)

---

## APPENDICI

### A. Codici Nomenclatore SDO

| Codice | Descrizione |
|--------|-------------|
| 39.95 | Emodialisi |
| 39.96 | Dialisi peritoneale |
| 88.72 | Ecografia diagnostica apparato cardiovascolare |
| 89.52 | Elettrocardiografia |
| ... | ... |

### B. Scorciatoie Tastiera

| Comando | Azione |
|---------|--------|
| Ctrl+N | Nuovo record |
| Ctrl+S | Salva |
| Ctrl+F | Cerca |
| Ctrl+P | Stampa |
| F1 | Guida |
| F5 | Aggiorna |
| Esc | Annulla/Chiudi |

### C. Glossario

**Kt/V**: Indice di adeguatezza dialitica  
**URR**: Urea Reduction Ratio  
**FAV**: Fistola Artero-Venosa  
**CVC**: Catetere Venoso Centrale  
**PTH**: Paratormone  
**EPO**: Eritropoietina  
**UF**: Ultrafiltrazione

---

**Versione documento**: 1.0.0  
**Data**: 14/01/2026  
**© 2026 Centro Dialisi - Tutti i diritti riservati**
