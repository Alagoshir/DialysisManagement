-- =============================================
-- Dialysis Management System - Database Creation
-- Versione: 1.0.0
-- Data: 2026-01-14
-- =============================================

-- Crea database se non esiste
CREATE DATABASE IF NOT EXISTS dialysis_db
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;

USE dialysis_db;

-- =============================================
-- TABELLA: users - Utenti del sistema
-- =============================================
CREATE TABLE IF NOT EXISTS users (
    UserId INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(100) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    Email VARCHAR(150),
    Role VARCHAR(50) NOT NULL DEFAULT 'readonly' COMMENT 'admin, medico, infermiere, readonly',
    IsActive BOOLEAN NOT NULL DEFAULT TRUE,
    FailedLoginAttempts INT NOT NULL DEFAULT 0,
    IsLocked BOOLEAN NOT NULL DEFAULT FALSE,
    LockoutEndDate DATETIME,
    LastLoginDate DATETIME,
    LastPasswordChangeDate DATETIME,
    MustChangePassword BOOLEAN NOT NULL DEFAULT FALSE,
    CreatedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CreatedBy INT,
    ModifiedDate DATETIME,
    ModifiedBy INT,
    INDEX idx_username (Username),
    INDEX idx_role (Role),
    INDEX idx_active (IsActive)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =============================================
-- TABELLA: patients - Anagrafica Pazienti
-- =============================================
CREATE TABLE IF NOT EXISTS patients (
    PatientId INT AUTO_INCREMENT PRIMARY KEY,
    CodiceFiscale VARCHAR(16) NOT NULL UNIQUE,
    Cognome VARCHAR(100) NOT NULL,
    Nome VARCHAR(100) NOT NULL,
    DataNascita DATE NOT NULL,
    Sesso CHAR(1) NOT NULL COMMENT 'M/F',
    LuogoNascita VARCHAR(200),
    Indirizzo VARCHAR(255),
    Citta VARCHAR(100),
    CAP VARCHAR(10),
    Provincia VARCHAR(2),
    Telefono VARCHAR(50),
    Cellulare VARCHAR(50),
    Email VARCHAR(150),
    
    -- Dati Sanitari
    NumeroTesseraSanitaria VARCHAR(50),
    MedicoBase VARCHAR(200),
    GruppoSanguigno VARCHAR(10),
    PesoSecco DECIMAL(5,2),
    Altezza INT,
    
    -- Dialisi
    DataInizioDialitica DATE,
    NefropatiaBase VARCHAR(255),
    TipoTrattamento VARCHAR(50) DEFAULT 'HD' COMMENT 'HD, HDF, HF, DP',
    FrequenzaSettimanale INT DEFAULT 3,
    
    -- Lista Trapianto
    InListaTrapianto BOOLEAN DEFAULT FALSE,
    DataInserimentoLista DATE,
    
    -- Contumaciali
    HBsAgPositivo BOOLEAN DEFAULT FALSE,
    HCVPositivo BOOLEAN DEFAULT FALSE,
    HIVPositivo BOOLEAN DEFAULT FALSE,
    
    -- Consensi GDPR
    ConsensoPrivacy BOOLEAN DEFAULT FALSE,
    DataConsensoPrivacy DATE,
    ConsensoTrattamentoDati BOOLEAN DEFAULT FALSE,
    
    -- Foto
    FotoPath VARCHAR(500),
    
    -- Stato
    IsActive BOOLEAN DEFAULT TRUE,
    DataDecesso DATE,
    MotivoUscita VARCHAR(255),
    
    -- Note
    Note TEXT,
    
    -- Audit
    CreatedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CreatedBy INT,
    ModifiedDate DATETIME,
    ModifiedBy INT,
    
    INDEX idx_codicefiscale (CodiceFiscale),
    INDEX idx_cognome_nome (Cognome, Nome),
    INDEX idx_active (IsActive),
    INDEX idx_contumaciali (HBsAgPositivo, HCVPositivo, HIVPositivo)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =============================================
-- TABELLA: vascular_accesses - Accessi Vascolari
-- =============================================
CREATE TABLE IF NOT EXISTS vascular_accesses (
    AccessId INT AUTO_INCREMENT PRIMARY KEY,
    PatientId INT NOT NULL,
    TipoAccesso VARCHAR(50) NOT NULL COMMENT 'FAV, CVC, Protesi',
    Sede VARCHAR(200) COMMENT 'Sede anatomica',
    DataCreazione DATE NOT NULL,
    DataRimozione DATE,
    Stato VARCHAR(50) DEFAULT 'attivo' COMMENT 'attivo, rimosso, malfunzionante, infetto',
    Chirurgo VARCHAR(200),
    Ospedale VARCHAR(200),
    Portata INT COMMENT 'Flusso in ml/min',
    Infezione BOOLEAN DEFAULT FALSE,
    Stenosi BOOLEAN DEFAULT FALSE,
    Trombosi BOOLEAN DEFAULT FALSE,
    DataUltimaValutazione DATE,
    Note TEXT,
    CreatedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CreatedBy INT,
    ModifiedDate DATETIME,
    ModifiedBy INT,
    FOREIGN KEY (PatientId) REFERENCES patients(PatientId) ON DELETE CASCADE,
    INDEX idx_patient (PatientId),
    INDEX idx_stato (Stato),
    INDEX idx_tipo (TipoAccesso)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =============================================
-- TABELLA: dialysis_sessions - Sedute Dialitiche
-- =============================================
CREATE TABLE IF NOT EXISTS dialysis_sessions (
    SessionId INT AUTO_INCREMENT PRIMARY KEY,
    PatientId INT NOT NULL,
    DataSeduta DATE NOT NULL,
    OrarioInizio TIME,
    OrarioFine TIME,
    DurataMinuti INT,
    
    -- Postazione
    Sala VARCHAR(50),
    Postazione VARCHAR(50),
    NumeroMonitor VARCHAR(50),
    
    -- Trattamento
    TipoTrattamento VARCHAR(50) DEFAULT 'HD' COMMENT 'HD, HDF, HF',
    TipoBagno VARCHAR(50),
    Bicarbonato DECIMAL(5,2),
    Potassio DECIMAL(5,2),
    Calcio DECIMAL(5,2),
    Sodio DECIMAL(5,2),
    
    -- Pesi e UF
    PesoPreDialisi DECIMAL(5,2),
    PesoPostDialisi DECIMAL(5,2),
    PesoSecco DECIMAL(5,2),
    UFProgrammata DECIMAL(5,2),
    UFRaggiunta DECIMAL(5,2),
    
    -- Parametri
    FlussoPompa INT COMMENT 'QB ml/min',
    FlussoDialisato INT COMMENT 'QD ml/min',
    Eparina INT COMMENT 'UI',
    TipoEparina VARCHAR(50),
    
    -- Accesso Vascolare
    AccessoVascolare VARCHAR(100),
    
    -- Parametri Adeguatezza
    KtV DECIMAL(4,2),
    URR DECIMAL(5,2),
    
    -- Complicanze
    Complicanze TEXT,
    
    -- Personale
    InfermiereResponsabile VARCHAR(200),
    MedicoResponsabile VARCHAR(200),
    
    -- Stato
    StatoSeduta VARCHAR(50) DEFAULT 'programmata' COMMENT 'programmata, in_corso, completata, annullata',
    
    Note TEXT,
    
    CreatedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CreatedBy INT,
    ModifiedDate DATETIME,
    ModifiedBy INT,
    
    FOREIGN KEY (PatientId) REFERENCES patients(PatientId) ON DELETE CASCADE,
    INDEX idx_patient_data (PatientId, DataSeduta),
    INDEX idx_data (DataSeduta),
    INDEX idx_stato (StatoSeduta)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =============================================
-- TABELLA: vital_signs - Parametri Vitali Intra-Dialisi
-- =============================================
CREATE TABLE IF NOT EXISTS vital_signs (
    VitalSignId INT AUTO_INCREMENT PRIMARY KEY,
    SessionId INT NOT NULL,
    Orario TIME NOT NULL,
    PressioneSistolica INT,
    PressioneDiastolica INT,
    FrequenzaCardiaca INT,
    Temperatura DECIMAL(4,2),
    SaturazioneO2 INT,
    Note VARCHAR(500),
    CreatedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CreatedBy INT,
    FOREIGN KEY (SessionId) REFERENCES dialysis_sessions(SessionId) ON DELETE CASCADE,
    INDEX idx_session (SessionId)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =============================================
-- TABELLA: lab_tests - Esami Laboratorio
-- =============================================
CREATE TABLE IF NOT EXISTS lab_tests (
    TestId INT AUTO_INCREMENT PRIMARY KEY,
    PatientId INT NOT NULL,
    DataPrelievo DATE NOT NULL,
    DataReferto DATE,
    
    -- Emocromo
    Emoglobina DECIMAL(5,2),
    Ematocrito DECIMAL(5,2),
    GlobuliRossi DECIMAL(5,2),
    GlobuliBianchi DECIMAL(7,2),
    Piastrine DECIMAL(7,2),
    
    -- Funzionalità Renale
    Creatinina DECIMAL(5,2),
    Azotemia DECIMAL(6,2),
    AcidoUrico DECIMAL(5,2),
    
    -- Elettroliti
    Sodio DECIMAL(5,2),
    Potassio DECIMAL(5,2),
    Calcio DECIMAL(5,2),
    Fosforo DECIMAL(5,2),
    Magnesio DECIMAL(5,2),
    Cloro DECIMAL(5,2),
    
    -- Equilibrio Acido-Base
    Bicarbonato DECIMAL(5,2),
    pH DECIMAL(4,2),
    
    -- Metabolismo Minerale
    PTH DECIMAL(7,2) COMMENT 'pg/ml',
    VitaminaD DECIMAL(6,2) COMMENT 'ng/ml',
    
    -- Metabolismo Ferro
    Ferro DECIMAL(6,2),
    Ferritina DECIMAL(7,2),
    Transferrina DECIMAL(6,2),
    Saturazione DECIMAL(5,2),
    
    -- Funzionalità Epatica
    AST DECIMAL(6,2),
    ALT DECIMAL(6,2),
    GammaGT DECIMAL(7,2),
    BilirubinaTotale DECIMAL(5,2),
    AlbuminaSierica DECIMAL(4,2),
    
    -- Lipidi
    Colesterolo DECIMAL(6,2),
    Trigliceridi DECIMAL(6,2),
    HDL DECIMAL(6,2),
    LDL DECIMAL(6,2),
    
    -- Glicemia
    Glicemia DECIMAL(6,2),
    Emoglobina DECIMAL(5,2),
    
    -- Sierologie
    HBsAg VARCHAR(20),
    AntiHCV VARCHAR(20),
    AntiHIV VARCHAR(20),
    
    -- Altro
    PCR DECIMAL(7,2) COMMENT 'Proteina C-reattiva',
    
    Note TEXT,
    
    CreatedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CreatedBy INT,
    ModifiedDate DATETIME,
    ModifiedBy INT,
    
    FOREIGN KEY (PatientId) REFERENCES patients(PatientId) ON DELETE CASCADE,
    INDEX idx_patient_data (PatientId, DataPrelievo),
    INDEX idx_data (DataPrelievo)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =============================================
-- TABELLA: instrumental_exams - Esami Strumentali
-- =============================================
CREATE TABLE IF NOT EXISTS instrumental_exams (
    ExamId INT AUTO_INCREMENT PRIMARY KEY,
    PatientId INT NOT NULL,
    TipoEsame VARCHAR(100) NOT NULL COMMENT 'ECG, Ecocardiogramma, RX, TAC, RMN, etc.',
    DataEsecuzione DATE NOT NULL,
    DataReferto DATE,
    Medico VARCHAR(200),
    IndicazioneClinica VARCHAR(500),
    Referto TEXT,
    EsameInterno BOOLEAN DEFAULT FALSE,
    CodiceNomenclatore VARCHAR(50) COMMENT 'Codice per SDO',
    CentroEsecuzione VARCHAR(200),
    Urgente BOOLEAN DEFAULT FALSE,
    DaRipetere BOOLEAN DEFAULT FALSE,
    DataScadenza DATE,
    Note TEXT,
    CreatedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CreatedBy INT,
    ModifiedDate DATETIME,
    ModifiedBy INT,
    FOREIGN KEY (PatientId) REFERENCES patients(PatientId) ON DELETE CASCADE,
    INDEX idx_patient_data (PatientId, DataEsecuzione),
    INDEX idx_tipo (TipoEsame),
    INDEX idx_scadenza (DataScadenza)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =============================================
-- TABELLA: exam_attachments - Allegati Esami
-- =============================================
CREATE TABLE IF NOT EXISTS exam_attachments (
    AttachmentId INT AUTO_INCREMENT PRIMARY KEY,
    ExamId INT NOT NULL,
    FileName VARCHAR(255) NOT NULL,
    FilePath VARCHAR(500) NOT NULL,
    MimeType VARCHAR(100),
    FileSize BIGINT,
    IsEncrypted BOOLEAN DEFAULT FALSE,
    FileHash VARCHAR(64) COMMENT 'SHA256',
    Description VARCHAR(500),
    UploadDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UploadedBy INT,
    FOREIGN KEY (ExamId) REFERENCES instrumental_exams(ExamId) ON DELETE CASCADE,
    INDEX idx_exam (ExamId)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =============================================
-- TABELLA: prescriptions - Prescrizioni Dialitiche
-- =============================================
CREATE TABLE IF NOT EXISTS prescriptions (
    PrescriptionId INT AUTO_INCREMENT PRIMARY KEY,
    PatientId INT NOT NULL,
    DataPrescrizione DATE NOT NULL,
    DataInizioValidita DATE NOT NULL,
    DataFineValidita DATE,
    
    -- Trattamento
    TipoTrattamento VARCHAR(50) DEFAULT 'HD',
    FrequenzaSettimanale INT DEFAULT 3,
    DurataSessione INT DEFAULT 240 COMMENT 'minuti',
    
    -- Bagno
    TipoBagno VARCHAR(50),
    Bicarbonato DECIMAL(5,2),
    Potassio DECIMAL(5,2),
    Calcio DECIMAL(5,2),
    Sodio DECIMAL(5,2),
    
    -- Parametri
    FlussoPompa INT,
    FlussoDialisato INT,
    UFTarget DECIMAL(5,2),
    PesoSeccoTarget DECIMAL(5,2),
    
    -- Anticoagulazione
    TipoAnticoagulazione VARCHAR(100),
    DoseAnticoagulante INT,
    
    MedicoPrescrittore VARCHAR(200) NOT NULL,
    Note TEXT,
    IsActive BOOLEAN DEFAULT TRUE,
    
    CreatedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CreatedBy INT,
    ModifiedDate DATETIME,
    ModifiedBy INT,
    
    FOREIGN KEY (PatientId) REFERENCES patients(PatientId) ON DELETE CASCADE,
    INDEX idx_patient_validita (PatientId, DataInizioValidita, DataFineValidita),
    INDEX idx_active (IsActive)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =============================================
-- TABELLA: medications - Farmaci
-- =============================================
CREATE TABLE IF NOT EXISTS medications (
    MedicationId INT AUTO_INCREMENT PRIMARY KEY,
    PatientId INT NOT NULL,
    NomeFarmaco VARCHAR(200) NOT NULL,
    PrincipioAttivo VARCHAR(200),
    Dosaggio VARCHAR(100),
    ViaAssunzione VARCHAR(50) COMMENT 'orale, EV, SC, IM',
    Frequenza VARCHAR(100),
    OrarioAssunzione VARCHAR(100),
    
    -- Somministrazione
    IntraDialisi BOOLEAN DEFAULT FALSE,
    ExtraDialisi BOOLEAN DEFAULT FALSE,
    
    DataInizio DATE NOT NULL,
    DataFine DATE,
    
    IndicazioneTerapeutica VARCHAR(500),
    MedicoPrescri VARCHAR(200),
    Note TEXT,
    IsActive BOOLEAN DEFAULT TRUE,
    
    CreatedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CreatedBy INT,
    ModifiedDate DATETIME,
    ModifiedBy INT,
    
    FOREIGN KEY (PatientId) REFERENCES patients(PatientId) ON DELETE CASCADE,
    INDEX idx_patient_active (PatientId, IsActive),
    INDEX idx_intradialisi (IntraDialisi)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =============================================
-- TABELLA: vaccinations - Vaccinazioni
-- =============================================
CREATE TABLE IF NOT EXISTS vaccinations (
    VaccinationId INT AUTO_INCREMENT PRIMARY KEY,
    PatientId INT NOT NULL,
    TipoVaccino VARCHAR(100) NOT NULL COMMENT 'HBV, Influenza, COVID-19, Pneumococco',
    DataSomministrazione DATE NOT NULL,
    NumeroRichiamo INT DEFAULT 1,
    Lotto VARCHAR(100),
    Sede VARCHAR(50),
    DataScadenzaRichiamo DATE,
    Medico VARCHAR(200),
    Note TEXT,
    CreatedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CreatedBy INT,
    FOREIGN KEY (PatientId) REFERENCES patients(PatientId) ON DELETE CASCADE,
    INDEX idx_patient_tipo (PatientId, TipoVaccino),
    INDEX idx_scadenza (DataScadenzaRichiamo)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =============================================
-- TABELLA: devices - Apparecchiature/Postazioni
-- =============================================
CREATE TABLE IF NOT EXISTS devices (
    DeviceId INT AUTO_INCREMENT PRIMARY KEY,
    NumeroMonitor VARCHAR(50) NOT NULL UNIQUE,
    Sala VARCHAR(50),
    Postazione VARCHAR(50),
    Marca VARCHAR(100),
    Modello VARCHAR(100),
    Matricola VARCHAR(100),
    AnnoInstallazione INT,
    DataUltimaManutenzione DATE,
    DataProssimaManutenzione DATE,
    IsAttivo BOOLEAN DEFAULT TRUE,
    Note TEXT,
    CreatedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CreatedBy INT,
    ModifiedDate DATETIME,
    ModifiedBy INT,
    INDEX idx_numero (NumeroMonitor),
    INDEX idx_attivo (IsAttivo)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =============================================
-- TABELLA: device_maintenance - Manutenzioni
-- =============================================
CREATE TABLE IF NOT EXISTS device_maintenance (
    MaintenanceId INT AUTO_INCREMENT PRIMARY KEY,
    DeviceId INT NOT NULL,
    DataManutenzione DATE NOT NULL,
    TipoManutenzione VARCHAR(100) COMMENT 'Ordinaria, Straordinaria, Riparazione',
    Descrizione TEXT,
    TecnicoResponsabile VARCHAR(200),
    Fornitore VARCHAR(200),
    Costo DECIMAL(10,2),
    DataProssimaScadenza DATE,
    Note TEXT,
    CreatedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CreatedBy INT,
    FOREIGN KEY (DeviceId) REFERENCES devices(DeviceId) ON DELETE CASCADE,
    INDEX idx_device_data (DeviceId, DataManutenzione),
    INDEX idx_scadenza (DataProssimaScadenza)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =============================================
-- TABELLA: backups - Storico Backup
-- =============================================
CREATE TABLE IF NOT EXISTS backups (
    BackupId INT AUTO_INCREMENT PRIMARY KEY,
    FileName VARCHAR(255) NOT NULL,
    FilePath VARCHAR(500) NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FileSizeMB DECIMAL(10,2),
    IsCompressed BOOLEAN DEFAULT FALSE,
    IsEncrypted BOOLEAN DEFAULT FALSE,
    UploadedToCloud BOOLEAN DEFAULT FALSE,
    CloudProvider VARCHAR(50),
    CloudFileId VARCHAR(500),
    CloudUploadDate DATETIME,
    BackupType VARCHAR(50) DEFAULT 'manual' COMMENT 'manual, scheduled, auto',
    Notes VARCHAR(500),
    FileHash VARCHAR(32) COMMENT 'MD5',
    CreatedBy INT,
    RestoreSuccess BOOLEAN,
    RestoreDate DATETIME,
    INDEX idx_date (CreatedDate),
    INDEX idx_cloud (UploadedToCloud)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =============================================
-- TABELLA: audit_log - Audit Trail
-- =============================================
CREATE TABLE IF NOT EXISTS audit_log (
    AuditId INT AUTO_INCREMENT PRIMARY KEY,
    UserId INT,
    Username VARCHAR(100),
    Action VARCHAR(50) NOT NULL COMMENT 'Login, Logout, Create, Update, Delete, View, Export',
    EntityType VARCHAR(100),
    EntityId INT,
    Details TEXT,
    OldValues TEXT COMMENT 'JSON',
    NewValues TEXT COMMENT 'JSON',
    IpAddress VARCHAR(45),
    MachineName VARCHAR(100),
    Timestamp DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Success BOOLEAN DEFAULT TRUE,
    ErrorMessage TEXT,
    INDEX idx_user_timestamp (UserId, Timestamp),
    INDEX idx_entity (EntityType, EntityId),
    INDEX idx_action (Action),
    INDEX idx_timestamp (Timestamp)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =============================================
-- TABELLA: settings - Impostazioni Sistema
-- =============================================
CREATE TABLE IF NOT EXISTS settings (
    SettingId INT AUTO_INCREMENT PRIMARY KEY,
    SettingKey VARCHAR(100) NOT NULL UNIQUE,
    SettingValue TEXT,
    SettingType VARCHAR(50) DEFAULT 'string' COMMENT 'string, int, bool, json',
    Description VARCHAR(500),
    Category VARCHAR(100),
    ModifiedDate DATETIME,
    ModifiedBy INT,
    INDEX idx_key (SettingKey),
    INDEX idx_category (Category)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
