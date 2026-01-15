-- =============================================
-- Dati Iniziali - Utente Admin e Configurazioni
-- =============================================

USE dialysis_db;

-- Utente Admin di default
-- Password: Admin@2026 (hash BCrypt)
INSERT INTO users (Username, PasswordHash, FirstName, LastName, Email, Role, IsActive, CreatedDate)
VALUES ('admin', '$2a$12$LQv3c1yqBWVHxkd0LHAkCOYz6TtxMQJqhN8/LewY5eo5wMQopD1Za', 
        'Amministratore', 'Sistema', 'admin@dialisi.it', 'admin', TRUE, NOW())
ON DUPLICATE KEY UPDATE UserId=UserId;

-- Impostazioni di default
INSERT INTO settings (SettingKey, SettingValue, SettingType, Description, Category) VALUES
('CentroNome', 'Centro Dialisi Ospedale', 'string', 'Nome del centro dialisi', 'Generale'),
('CentroIndirizzo', 'Via Roma 1, 80100 Napoli', 'string', 'Indirizzo del centro', 'Generale'),
('CentroTelefono', '081-1234567', 'string', 'Telefono del centro', 'Generale'),
('CentroEmail', 'dialisi@ospedale.it', 'string', 'Email del centro', 'Generale'),
('BackupAutoEnabled', 'true', 'bool', 'Backup automatico abilitato', 'Backup'),
('BackupAutoTime', '02:00', 'string', 'Orario backup automatico', 'Backup'),
('BackupRetentionDays', '30', 'int', 'Giorni conservazione backup', 'Backup'),
('SessionTimeoutMinutes', '30', 'int', 'Timeout sessione (minuti)', 'Sicurezza'),
('PasswordMinLength', '8', 'int', 'Lunghezza minima password', 'Sicurezza'),
('MaxFailedLoginAttempts', '5', 'int', 'Tentativi login falliti max', 'Sicurezza'),
('EncryptAttachments', 'true', 'bool', 'Cripta allegati file', 'Sicurezza'),
('EnableAuditLog', 'true', 'bool', 'Abilita audit log', 'Sicurezza'),
('AuditLogRetentionDays', '90', 'int', 'Giorni conservazione audit log', 'Sicurezza')
ON DUPLICATE KEY UPDATE SettingKey=SettingKey;

-- =============================================
-- Fine Script Dati Iniziali
-- =============================================
