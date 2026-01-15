-- ========================================
-- DATABASE SCHEMA DIALYSIS MANAGEMENT
-- MySQL 9.5.0
-- Centro Dialisi ASL Napoli 1
-- ========================================

-- Elimina database se esiste (solo per sviluppo)
DROP DATABASE IF EXISTS dialysis_management;

-- Crea database
CREATE DATABASE dialysis_management
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE dialysis_management;

-- ========================================
-- TABELLA: users
-- Utenti del sistema (medici, infermieri, admin)
-- ========================================
CREATE TABLE users (
    user_id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    email VARCHAR(100),
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    role ENUM('admin', 'medico', 'infermiere', 'oss', 'amministrativo') NOT NULL,
    is_active BOOLEAN DEFAULT TRUE,
    last_login_date DATETIME,
    failed_login_attempts INT DEFAULT 0,
    account_locked_until DATETIME,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    created_by INT,
    updated_by INT,
    INDEX idx_username (username),
    INDEX idx_role (role),
    INDEX idx_active (is_active)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ========================================
-- TABELLA: patients
-- Anagrafica pazienti in dialisi
-- ========================================
CREATE TABLE patients (
    patient_id INT AUTO_INCREMENT PRIMARY KEY,
    codice_fiscale CHAR(16) NOT NULL UNIQUE,
    cognome VARCHAR(50) NOT NULL,
    nome VARCHAR(50) NOT NULL,
    data_nascita DATE NOT NULL,
    luogo_nascita VARCHAR(100),
    sesso ENUM('M', 'F') NOT NULL,
    indirizzo VARCHAR(200),
    citta VARCHAR(100),
    cap VARCHAR(10),
    provincia CHAR(2),
    telefono VARCHAR(20),
    cellulare VARCHAR(20),
    email VARCHAR(100),
    
    -- Dati sanitari
    codice_sanitario VARCHAR(50),
    medico_curante VARCHAR(100),
    gruppo_sanguigno ENUM('A+', 'A-', 'B+', 'B-', 'AB+', 'AB-', '0+', '0-'),
    peso_secco DECIMAL(5,2),
    altezza INT,
    bmi DECIMAL(5,2),
    
    -- Contumaciali
    hbsag_positive BOOLEAN DEFAULT FALSE,
    hcv_positive BOOLEAN DEFAULT FALSE,
    hiv_positive BOOLEAN DEFAULT FALSE,
    
    -- Lista trapianto
    in_lista_trapianto BOOLEAN DEFAULT FALSE,
    data_inserimento_lista DATE,
    
    -- Stato paziente
    stato ENUM('attivo', 'sospeso', 'deceduto', 'trapiantato', 'trasferito') DEFAULT 'attivo',
    data_ingresso DATE,
    data_uscita DATE,
    motivo_uscita TEXT,
    
    -- GDPR
    consenso_trattamento_dati BOOLEAN DEFAULT FALSE,
    data_consenso DATE,
    
    -- Foto e QR
    foto_path VARCHAR(255),
    qr_code_path VARCHAR(255),
    
    -- Note
    note TEXT,
    
    -- Audit
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    created_by INT,
    updated_by INT,
    
    INDEX idx_codice_fiscale (codice_fiscale),
    INDEX idx_cognome_nome (cognome, nome),
    INDEX idx_stato (stato),
    INDEX idx_contumaciali (hbsag_positive, hcv_positive, hiv_positive),
    INDEX idx_data_nascita (data_nascita),
    FOREIGN KEY (created_by) REFERENCES users(user_id),
    FOREIGN KEY (updated_by) REFERENCES users(user_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ========================================
-- TABELLA: vascular_accesses
-- Accessi vascolari (FAV, CVC, protesi)
-- ========================================
CREATE TABLE vascular_accesses (
    access_id INT AUTO_INCREMENT PRIMARY KEY,
    patient_id INT NOT NULL,
    tipo_accesso ENUM('FAV', 'CVC', 'Protesi', 'Altro') NOT NULL,
    sede VARCHAR(100),
    lato ENUM('Sinistro', 'Destro', 'Centrale'),
    data_posizionamento DATE NOT NULL,
    data_rimozione DATE,
    funzionante BOOLEAN DEFAULT TRUE,
    complicanze TEXT,
    note TEXT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    created_by INT,
    
    FOREIGN KEY (patient_id) REFERENCES patients(patient_id) ON DELETE CASCADE,
    FOREIGN KEY (created_by) REFERENCES users(user_id),
    INDEX idx_patient (patient_id),
    INDEX idx_tipo (tipo_accesso),
    INDEX idx_funzionante (funzionante)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ========================================
-- TABELLA: rooms
-- Sale dialisi
-- ========================================
CREATE TABLE rooms (
    room_id INT AUTO_INCREMENT PRIMARY KEY,
    nome_sala VARCHAR(50) NOT NULL,
    numero_postazioni INT NOT NULL,
    piano INT,
    note TEXT,
    attiva BOOLEAN DEFAULT TRUE,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    INDEX idx_attiva (attiva)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ========================================
-- TABELLA: stations
-- Postazioni dialisi (reni)
-- ========================================
CREATE TABLE stations (
    station_id INT AUTO_INCREMENT PRIMARY KEY,
    room_id INT NOT NULL,
    numero_postazione INT NOT NULL,
    isolamento BOOLEAN DEFAULT FALSE,
    note TEXT,
    attiva BOOLEAN DEFAULT TRUE,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    
    FOREIGN KEY (room_id) REFERENCES rooms(room_id) ON DELETE CASCADE,
    INDEX idx_room (room_id),
    INDEX idx_isolamento (isolamento),
    INDEX idx_attiva (attiva),
    UNIQUE KEY uk_room_station (room_id, numero_postazione)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ========================================
-- TABELLA: devices
-- Apparecchiature (monitor dialisi)
-- ========================================
CREATE TABLE devices (
    device_id INT AUTO_INCREMENT PRIMARY KEY,
    station_id INT,
    tipo_dispositivo ENUM('Monitor', 'Osmosi', 'Altro') DEFAULT 'Monitor',
    marca VARCHAR(50),
    modello VARCHAR(50),
    matricola VARCHAR(100) UNIQUE,
    anno_acquisto INT,
    data_installazione DATE,
    data_dismissione DATE,
    stato ENUM('operativo', 'manutenzione', 'guasto', 'dismesso') DEFAULT 'operativo',
    note TEXT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    
    FOREIGN KEY (station_id) REFERENCES stations(station_id) ON DELETE SET NULL,
    INDEX idx_station (station_id),
    INDEX idx_stato (stato),
    INDEX idx_matricola (matricola)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ========================================
-- TABELLA: device_maintenance
-- Manutenzioni apparecchiature
-- ========================================
CREATE TABLE device_maintenance (
    maintenance_id INT AUTO_INCREMENT PRIMARY KEY,
    device_id INT NOT NULL,
    tipo_manutenzione ENUM('ordinaria', 'straordinaria', 'calibrazione', 'riparazione') NOT NULL,
    data_manutenzione DATE NOT NULL,
    data_prossima_manutenzione DATE,
    tecnico VARCHAR(100),
    azienda VARCHAR(100),
    costo DECIMAL(10,2),
    descrizione TEXT,
    note TEXT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    created_by INT,
    
    FOREIGN KEY (device_id) REFERENCES devices(device_id) ON DELETE CASCADE,
    FOREIGN KEY (created_by) REFERENCES users(user_id),
    INDEX idx_device (device_id),
    INDEX idx_data (data_manutenzione),
    INDEX idx_prossima (data_prossima_manutenzione)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ========================================
-- TABELLA: dialysis_sessions
-- Sedute dialitiche
-- ========================================
CREATE TABLE dialysis_sessions (
    session_id INT AUTO_INCREMENT PRIMARY KEY,
    patient_id INT NOT NULL,
    data_seduta DATE NOT NULL,
    turno ENUM('mattina', 'pomeriggio', 'sera') NOT NULL,
    
    -- Assegnazione risorse
    room_id INT,
    station_id INT,
    device_id INT,
    medico_id INT,
    infermiere_id INT,
    
    -- Orari
    ora_inizio TIME,
    ora_fine TIME,
    durata_effettiva INT, -- minuti
    durata_prevista INT DEFAULT 240, -- minuti
    
    -- Accesso vascolare
    access_id INT,
    
    -- Parametri pre-dialisi
    peso_pre DECIMAL(5,2),
    pa_sistolica_pre INT,
    pa_diastolica_pre INT,
    fc_pre INT,
    temperatura_pre DECIMAL(4,2),
    
    -- Parametri post-dialisi
    peso_post DECIMAL(5,2),
    pa_sistolica_post INT,
    pa_diastolica_post INT,
    fc_post INT,
    temperatura_post DECIMAL(4,2),
    
    -- Ultrafiltrazione
    uf_programmata INT, -- ml
    uf_effettuata INT, -- ml
    
    -- Parametri tecnici
    tipo_trattamento ENUM('HD', 'HDF', 'HF', 'Altro') DEFAULT 'HD',
    tipo_filtro VARCHAR(50),
    superficie_filtro INT,
    qb INT, -- flusso sangue ml/min
    qd INT, -- flusso dialisato ml/min
    bicarbonato DECIMAL(4,1),
    sodio DECIMAL(5,1),
    calcio DECIMAL(4,2),
    potassio DECIMAL(4,2),
    
    -- Anticoagulazione
    anticoagulante VARCHAR(50),
    dose_anticoagulante VARCHAR(50),
    
    -- Kt/V
    ktv DECIMAL(4,2),
    urr DECIMAL(5,2), -- Urea Reduction Ratio
    
    -- Complicanze
    complicanze TEXT,
    
    -- Stato seduta
    stato ENUM('programmata', 'in_corso', 'completata', 'annullata', 'interrotta') DEFAULT 'programmata',
    motivo_annullamento TEXT,
    
    -- Note
    note TEXT,
    
    -- Audit
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    created_by INT,
    updated_by INT,
    
    FOREIGN KEY (patient_id) REFERENCES patients(patient_id) ON DELETE CASCADE,
    FOREIGN KEY (room_id) REFERENCES rooms(room_id),
    FOREIGN KEY (station_id) REFERENCES stations(station_id),
    FOREIGN KEY (device_id) REFERENCES devices(device_id),
    FOREIGN KEY (access_id) REFERENCES vascular_accesses(access_id),
    FOREIGN KEY (medico_id) REFERENCES users(user_id),
    FOREIGN KEY (infermiere_id) REFERENCES users(user_id),
    FOREIGN KEY (created_by) REFERENCES users(user_id),
    FOREIGN KEY (updated_by) REFERENCES users(user_id),
    
    INDEX idx_patient (patient_id),
    INDEX idx_data (data_seduta),
    INDEX idx_stato (stato),
    INDEX idx_patient_data (patient_id, data_seduta),
    INDEX idx_data_turno (data_seduta, turno)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ========================================
-- TABELLA: vital_signs
-- Parametri vitali intra-dialisi
-- ========================================
CREATE TABLE vital_signs (
    vital_id INT AUTO_INCREMENT PRIMARY KEY,
    session_id INT NOT NULL,
    ora_rilevazione TIME NOT NULL,
    pa_sistolica INT,
    pa_diastolica INT,
    frequenza_cardiaca INT,
    temperatura DECIMAL(4,2),
    saturazione_o2 INT,
    note TEXT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    created_by INT,
    
    FOREIGN KEY (session_id) REFERENCES dialysis_sessions(session_id) ON DELETE CASCADE,
    FOREIGN KEY (created_by) REFERENCES users(user_id),
    INDEX idx_session (session_id),
    INDEX idx_ora (ora_rilevazione)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ========================================
-- TABELLA: lab_tests
-- Esami di laboratorio
-- ========================================
CREATE TABLE lab_tests (
    test_id INT AUTO_INCREMENT PRIMARY KEY,
    patient_id INT NOT NULL,
    data_prelievo DATE NOT NULL,
    data_referto DATE,
    
    -- Emocromo
    hb DECIMAL(5,2), -- emoglobina g/dL
    hct DECIMAL(5,2), -- ematocrito %
    rbc DECIMAL(5,2), -- globuli rossi M/uL
    wbc DECIMAL(5,2), -- globuli bianchi K/uL
    plt DECIMAL(6,2), -- piastrine K/uL
    
    -- Funzionalità renale
    creatinina DECIMAL(5,2), -- mg/dL
    azotemia DECIMAL(6,2), -- mg/dL
    urea_pre DECIMAL(6,2), -- mg/dL
    urea_post DECIMAL(6,2), -- mg/dL
    
    -- Elettroliti
    sodio DECIMAL(5,2), -- mEq/L
    potassio DECIMAL(4,2), -- mEq/L
    calcio DECIMAL(4,2), -- mg/dL
    fosforo DECIMAL(4,2), -- mg/dL
    magnesio DECIMAL(4,2), -- mg/dL
    cloro DECIMAL(5,2), -- mEq/L
    
    -- Metabolismo minerale
    pth DECIMAL(7,2), -- pg/mL
    vitamina_d DECIMAL(5,2), -- ng/mL
    
    -- Metabolismo del ferro
    ferritina DECIMAL(7,2), -- ng/mL
    transferrina DECIMAL(6,2), -- mg/dL
    saturazione_transferrina DECIMAL(5,2), -- %
    ferro_sierico DECIMAL(6,2), -- mcg/dL
    
    -- Funzionalità epatica
    alt DECIMAL(6,2), -- U/L
    ast DECIMAL(6,2), -- U/L
    bilirubina_totale DECIMAL(4,2), -- mg/dL
    albumina DECIMAL(4,2), -- g/dL
    proteine_totali DECIMAL(4,2), -- g/dL
    
    -- Metabolismo lipidico
    colesterolo_totale DECIMAL(6,2), -- mg/dL
    colesterolo_hdl DECIMAL(5,2), -- mg/dL
    colesterolo_ldl DECIMAL(6,2), -- mg/dL
    trigliceridi DECIMAL(6,2), -- mg/dL
    
    -- Glicemia
    glicemia DECIMAL(5,2), -- mg/dL
    emoglobina_glicata DECIMAL(4,2), -- %
    
    -- Coagulazione
    pt DECIMAL(5,2), -- secondi
    inr DECIMAL(4,2),
    aptt DECIMAL(5,2), -- secondi
    
    -- Sierologie
    hbsag ENUM('negativo', 'positivo', 'non_eseguito') DEFAULT 'non_eseguito',
    hcv_ab ENUM('negativo', 'positivo', 'non_eseguito') DEFAULT 'non_eseguito',
    hiv_ab ENUM('negativo', 'positivo', 'non_eseguito') DEFAULT 'non_eseguito',
    
    -- PCR e infiammazione
    pcr DECIMAL(6,2), -- mg/L
    
    -- Note e alert
    note TEXT,
    alert_generato BOOLEAN DEFAULT FALSE,
    
    -- Audit
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    created_by INT,
    
    FOREIGN KEY (patient_id) REFERENCES patients(patient_id) ON DELETE CASCADE,
    FOREIGN KEY (created_by) REFERENCES users(user_id),
    INDEX idx_patient (patient_id),
    INDEX idx_data_prelievo (data_prelievo),
    INDEX idx_patient_data (patient_id, data_prelievo)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ========================================
-- TABELLA: lab_reference_values
-- Valori di riferimento esami laboratorio
-- ========================================
CREATE TABLE lab_reference_values (
    ref_id INT AUTO_INCREMENT PRIMARY KEY,
    parametro VARCHAR(50) NOT NULL UNIQUE,
    valore_min DECIMAL(10,4),
    valore_max DECIMAL(10,4),
    unita_misura VARCHAR(20),
    note TEXT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ========================================
-- TABELLA: instrumental_exams
-- Anagrafica esami strumentali
-- ========================================
CREATE TABLE instrumental_exams (
    exam_id INT AUTO_INCREMENT PRIMARY KEY,
    patient_id INT NOT NULL,
    tipo_esame ENUM('ECG', 'Ecocardiografia', 'RX Torace', 'TAC', 'RMN', 'Ecografia', 'Altro') NOT NULL,
    descrizione VARCHAR(200),
    data_esecuzione DATE NOT NULL,
    data_referto DATE,
    esito TEXT,
    esame_interno BOOLEAN DEFAULT FALSE,
    codice_nomenclatore VARCHAR(20), -- per SDO
    note TEXT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    created_by INT,
    
    FOREIGN KEY (patient_id) REFERENCES patients(patient_id) ON DELETE CASCADE,
    FOREIGN KEY (created_by) REFERENCES users(user_id),
    INDEX idx_patient (patient_id),
    INDEX idx_tipo (tipo_esame),
    INDEX idx_data (data_esecuzione)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ========================================
-- TABELLA: exam_attachments
-- File allegati esami strumentali
-- ========================================
CREATE TABLE exam_attachments (
    attachment_id INT AUTO_INCREMENT PRIMARY KEY,
    exam_id INT NOT NULL,
    nome_file VARCHAR(255) NOT NULL,
    file_path VARCHAR(500) NOT NULL,
    tipo_file VARCHAR(50), -- PDF, DICOM, JPG, PNG
    dimensione_kb INT,
    encrypted BOOLEAN DEFAULT TRUE,
    checksum VARCHAR(64), -- SHA-256
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    created_by INT,
    
    FOREIGN KEY (exam_id) REFERENCES instrumental_exams(exam_id) ON DELETE CASCADE,
    FOREIGN KEY (created_by) REFERENCES users(user_id),
    INDEX idx_exam (exam_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ========================================
-- TABELLA: prescriptions
-- Prescrizioni dialitiche
-- ========================================
CREATE TABLE prescriptions (
    prescription_id INT AUTO_INCREMENT PRIMARY KEY,
    patient_id INT NOT NULL,
    data_prescrizione DATE NOT NULL,
    data_inizio_validita DATE NOT NULL,
    data_fine_validita DATE,
    attiva BOOLEAN DEFAULT TRUE,
    
    -- Parametri dialisi
    frequenza_settimanale INT DEFAULT 3,
    durata_seduta INT DEFAULT 240, -- minuti
    tipo_trattamento ENUM('HD', 'HDF', 'HF') DEFAULT 'HD',
    peso_secco_target DECIMAL(5,2),
    uf_target INT, -- ml
    
    -- Parametri tecnici
    qb_prescritto INT, -- ml/min
    qd_prescritto INT, -- ml/min
    tipo_filtro VARCHAR(50),
    
    -- Bagno dialitico
    sodio DECIMAL(5,1),
    potassio DECIMAL(4,2),
    calcio DECIMAL(4,2),
    bicarbonato DECIMAL(4,1),
    temperatura DECIMAL(4,1),
    
    -- Anticoagulazione
    anticoagulante VARCHAR(50),
    dosaggio_anticoagulante VARCHAR(100),
    
    -- Note
    note TEXT,
    
    -- Audit
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    created_by INT,
    
    FOREIGN KEY (patient_id) REFERENCES patients(patient_id) ON DELETE CASCADE,
    FOREIGN KEY (created_by) REFERENCES users(user_id),
    INDEX idx_patient (patient_id),
    INDEX idx_attiva (attiva),
    INDEX idx_validita (data_inizio_validita, data_fine_validita)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ========================================
-- TABELLA: medications
-- Farmaci somministrati
-- ========================================
CREATE TABLE medications (
    medication_id INT AUTO_INCREMENT PRIMARY KEY,
    patient_id INT NOT NULL,
    nome_farmaco VARCHAR(100) NOT NULL,
    principio_attivo VARCHAR(100),
    via_somministrazione ENUM('intra_dialitica', 'extra_dialitica', 'orale', 'intramuscolare', 'sottocutanea') NOT NULL,
    dose VARCHAR(50),
    unita_misura VARCHAR(20),
    frequenza VARCHAR(100), -- es. "3 volte/settimana", "al bisogno"
    data_inizio DATE NOT NULL,
    data_fine DATE,
    attivo BOOLEAN DEFAULT TRUE,
    note TEXT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    created_by INT,
    
    FOREIGN KEY (patient_id) REFERENCES patients(patient_id) ON DELETE CASCADE,
    FOREIGN KEY (created_by) REFERENCES users(user_id),
    INDEX idx_patient (patient_id),
    INDEX idx_attivo (attivo),
    INDEX idx_farmaco (nome_farmaco)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ========================================
-- TABELLA: vaccinations
-- Vaccinazioni
-- ========================================
CREATE TABLE vaccinations (
    vaccination_id INT AUTO_INCREMENT PRIMARY KEY,
    patient_id INT NOT NULL,
    tipo_vaccino ENUM('HBV', 'Influenza', 'COVID-19', 'Pneumococco', 'Altro') NOT NULL,
    nome_vaccino VARCHAR(100),
    lotto VARCHAR(50),
    data_somministrazione DATE NOT NULL,
    dose_numero INT, -- es. 1, 2, 3 per ciclo vaccinale
    data_richiamo DATE,
    sede_inoculazione VARCHAR(50),
    reazioni_avverse TEXT,
    note TEXT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    created_by INT,
    
    FOREIGN KEY (patient_id) REFERENCES patients(patient_id) ON DELETE CASCADE,
    FOREIGN KEY (created_by) REFERENCES users(user_id),
    INDEX idx_patient (patient_id),
    INDEX idx_tipo (tipo_vaccino),
    INDEX idx_data (data_somministrazione),
    INDEX idx_richiamo (data_richiamo)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ========================================
-- TABELLA: audit_log
-- Log audit per GDPR compliance
-- ========================================
CREATE TABLE audit_log (
    audit_id BIGINT AUTO_INCREMENT PRIMARY KEY,
    user_id INT,
    username VARCHAR(50),
    azione VARCHAR(100) NOT NULL,
    tabella VARCHAR(50),
    record_id INT,
    dettagli TEXT,
    ip_address VARCHAR(45),
    timestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
    
    FOREIGN KEY (user_id) REFERENCES users(user_id) ON DELETE SET NULL,
    INDEX idx_user (user_id),
    INDEX idx_timestamp (timestamp),
    INDEX idx_azione (azione),
    INDEX idx_tabella (tabella)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ========================================
-- TABELLA: settings
-- Impostazioni applicazione
-- ========================================
CREATE TABLE settings (
    setting_id INT AUTO_INCREMENT PRIMARY KEY,
    setting_key VARCHAR(100) NOT NULL UNIQUE,
    setting_value TEXT,
    setting_type ENUM('string', 'int', 'bool', 'json') DEFAULT 'string',
    descrizione TEXT,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    updated_by INT,
    
    FOREIGN KEY (updated_by) REFERENCES users(user_id),
    INDEX idx_key (setting_key)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ========================================
-- DATI INIZIALI
-- ========================================

-- Utente admin predefinito
-- Password: Admin123! (hash BCrypt)
INSERT INTO users (username, password_hash, email, first_name, last_name, role, is_active) 
VALUES ('admin', '$2a$11$5Z9P4QZ8KQGmBo0Z9P4QZ.KQGmBo0Z9P4QZ8KQGmBo0Z9P4QZ8KQGm', 'admin@aslnapoli1.it', 'Amministratore', 'Sistema', 'admin', TRUE);

-- Sale dialisi esempio
INSERT INTO rooms (nome_sala, numero_postazioni, piano, attiva) VALUES
('Sala A', 8, 1, TRUE),
('Sala B', 8, 1, TRUE),
('Sala Isolamento', 2, 1, TRUE);

-- Postazioni Sala A
INSERT INTO stations (room_id, numero_postazione, isolamento, attiva)
SELECT 1, n, FALSE, TRUE FROM (
    SELECT 1 AS n UNION SELECT 2 UNION SELECT 3 UNION SELECT 4 UNION
    SELECT 5 UNION SELECT 6 UNION SELECT 7 UNION SELECT 8
) AS numbers;

-- Postazioni Sala B
INSERT INTO stations (room_id, numero_postazione, isolamento, attiva)
SELECT 2, n, FALSE, TRUE FROM (
    SELECT 1 AS n UNION SELECT 2 UNION SELECT 3 UNION SELECT 4 UNION
    SELECT 5 UNION SELECT 6 UNION SELECT 7 UNION SELECT 8
) AS numbers;

-- Postazioni Sala Isolamento
INSERT INTO stations (room_id, numero_postazione, isolamento, attiva) VALUES
(3, 1, TRUE, TRUE),
(3, 2, TRUE, TRUE);

-- Valori di riferimento laboratorio
INSERT INTO lab_reference_values (parametro, valore_min, valore_max, unita_misura, note) VALUES
('hb', 10.0, 12.0, 'g/dL', 'Emoglobina target per pazienti dializzati'),
('hct', 30.0, 36.0, '%', 'Ematocrito'),
('creatinina', 8.0, 15.0, 'mg/dL', 'Creatinina pazienti dializzati'),
('azotemia', 100.0, 200.0, 'mg/dL', 'Azotemia pre-dialisi'),
('potassio', 3.5, 5.5, 'mEq/L', 'Potassio sierico'),
('calcio', 8.4, 10.2, 'mg/dL', 'Calcio sierico'),
('fosforo', 2.5, 4.5, 'mg/dL', 'Fosforo sierico'),
('pth', 150.0, 300.0, 'pg/mL', 'Paratormone target'),
('ferritina', 200.0, 500.0, 'ng/mL', 'Ferritina target'),
('saturazione_transferrina', 20.0, 50.0, '%', 'Saturazione transferrina'),
('albumina', 3.5, 5.0, 'g/dL', 'Albumina sierica');

-- Impostazioni predefinite
INSERT INTO settings (setting_key, setting_value, setting_type, descrizione) VALUES
('centro_nome', 'Centro Dialisi ASL Napoli 1', 'string', 'Nome del centro dialisi'),
('centro_codice', '150901', 'string', 'Codice regionale centro'),
('backup_auto_enabled', 'true', 'bool', 'Backup automatico abilitato'),
('backup_retention_days', '30', 'int', 'Giorni di retention backup'),
('alert_lab_enabled', 'true', 'bool', 'Alert valori laboratorio abilitati'),
('ktv_target_min', '1.2', 'string', 'Kt/V minimo target');

-- ========================================
-- FINE SCRIPT CREAZIONE DATABASE
-- ========================================
