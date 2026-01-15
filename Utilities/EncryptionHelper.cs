using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;

namespace DialysisManagement.Utilities
{
    /// <summary>
    /// Helper per crittografia e hashing
    /// </summary>
    public class EncryptionHelper : IEncryptionHelper
    {
        private readonly byte[] _encryptionKey;
        private readonly byte[] _encryptionIV;

        /// <summary>
        /// Costruttore con chiave e IV da configurazione
        /// </summary>
        public EncryptionHelper()
        {
            // Carica chiave e IV da app.config o usa valori default
            var keyString = System.Configuration.ConfigurationManager.AppSettings["EncryptionKey"]
                ?? "YourSecure32CharEncryptionKey!!";
            var ivString = System.Configuration.ConfigurationManager.AppSettings["EncryptionIV"]
                ?? "YourSecure16IV!!";

            // Converte in byte array con padding se necessario
            _encryptionKey = GetBytes(keyString, 32);
            _encryptionIV = GetBytes(ivString, 16);
        }

        /// <summary>
        /// Costruttore con chiave e IV espliciti
        /// </summary>
        public EncryptionHelper(string encryptionKey, string encryptionIV)
        {
            if (string.IsNullOrEmpty(encryptionKey))
                throw new ArgumentNullException(nameof(encryptionKey));
            if (string.IsNullOrEmpty(encryptionIV))
                throw new ArgumentNullException(nameof(encryptionIV));

            _encryptionKey = GetBytes(encryptionKey, 32);
            _encryptionIV = GetBytes(encryptionIV, 16);
        }

        #region Crittografia Stringhe

        /// <summary>
        /// Cripta una stringa usando AES-256
        /// </summary>
        public string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            try
            {
                byte[] encrypted;

                using (Aes aes = Aes.Create())
                {
                    aes.Key = _encryptionKey;
                    aes.IV = _encryptionIV;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(plainText);
                            }
                            encrypted = msEncrypt.ToArray();
                        }
                    }
                }

                return Convert.ToBase64String(encrypted);
            }
            catch (Exception ex)
            {
                throw new CryptographicException($"Errore durante la crittografia: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Decripta una stringa crittografata con AES-256
        /// </summary>
        public string Decrypt(string encryptedText)
        {
            if (string.IsNullOrEmpty(encryptedText))
                return encryptedText;

            try
            {
                byte[] buffer = Convert.FromBase64String(encryptedText);
                string plaintext = null;

                using (Aes aes = Aes.Create())
                {
                    aes.Key = _encryptionKey;
                    aes.IV = _encryptionIV;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream msDecrypt = new MemoryStream(buffer))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }

                return plaintext;
            }
            catch (Exception ex)
            {
                throw new CryptographicException($"Errore durante la decrittografia: {ex.Message}", ex);
            }
        }

        #endregion

        #region Crittografia Byte Array

        /// <summary>
        /// Cripta array di byte usando AES-256
        /// </summary>
        public byte[] Encrypt(byte[] data)
        {
            if (data == null || data.Length == 0)
                return data;

            try
            {
                byte[] encrypted;

                using (Aes aes = Aes.Create())
                {
                    aes.Key = _encryptionKey;
                    aes.IV = _encryptionIV;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            csEncrypt.Write(data, 0, data.Length);
                            csEncrypt.FlushFinalBlock();
                            encrypted = msEncrypt.ToArray();
                        }
                    }
                }

                return encrypted;
            }
            catch (Exception ex)
            {
                throw new CryptographicException($"Errore durante la crittografia dei dati: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Decripta array di byte crittografato con AES-256
        /// </summary>
        public byte[] Decrypt(byte[] encryptedData)
        {
            if (encryptedData == null || encryptedData.Length == 0)
                return encryptedData;

            try
            {
                byte[] decrypted;

                using (Aes aes = Aes.Create())
                {
                    aes.Key = _encryptionKey;
                    aes.IV = _encryptionIV;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream msDecrypt = new MemoryStream(encryptedData))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (MemoryStream msPlain = new MemoryStream())
                            {
                                csDecrypt.CopyTo(msPlain);
                                decrypted = msPlain.ToArray();
                            }
                        }
                    }
                }

                return decrypted;
            }
            catch (Exception ex)
            {
                throw new CryptographicException($"Errore durante la decrittografia dei dati: {ex.Message}", ex);
            }
        }

        #endregion

        #region Password Hashing (BCrypt)

        /// <summary>
        /// Hash password usando BCrypt
        /// </summary>
        public string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));

            try
            {
                // BCrypt con work factor 12 (bilanciamento sicurezza/performance)
                return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
            }
            catch (Exception ex)
            {
                throw new CryptographicException($"Errore durante l'hashing della password: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Verifica password contro hash BCrypt
        /// </summary>
        public bool VerifyPassword(string password, string hash)
        {
            if (string.IsNullOrEmpty(password))
                return false;
            if (string.IsNullOrEmpty(hash))
                return false;

            try
            {
                return BCrypt.Net.BCrypt.Verify(password, hash);
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Genera salt casuale Base64
        /// </summary>
        public string GenerateSalt()
        {
            byte[] saltBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        /// <summary>
        /// Calcola hash SHA256 di una stringa
        /// </summary>
        public string ComputeSha256Hash(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            try
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(input);
                    byte[] hash = sha256.ComputeHash(bytes);

                    StringBuilder builder = new StringBuilder();
                    foreach (byte b in hash)
                    {
                        builder.Append(b.ToString("x2"));
                    }
                    return builder.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new CryptographicException($"Errore durante il calcolo dell'hash SHA256: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Converte stringa in byte array con padding/truncate
        /// </summary>
        private byte[] GetBytes(string input, int length)
        {
            byte[] bytes = new byte[length];
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            int copyLength = Math.Min(inputBytes.Length, length);
            Array.Copy(inputBytes, bytes, copyLength);

            // Se input è più corto, riempie con zero
            // Se input è più lungo, tronca

            return bytes;
        }

        #endregion

        #region File Encryption (Metodi Bonus)

        /// <summary>
        /// Cripta file
        /// </summary>
        public void EncryptFile(string inputFilePath, string outputFilePath)
        {
            if (!File.Exists(inputFilePath))
                throw new FileNotFoundException("File di input non trovato", inputFilePath);

            try
            {
                byte[] fileBytes = File.ReadAllBytes(inputFilePath);
                byte[] encryptedBytes = Encrypt(fileBytes);
                File.WriteAllBytes(outputFilePath, encryptedBytes);
            }
            catch (Exception ex)
            {
                throw new CryptographicException($"Errore durante la crittografia del file: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Decripta file
        /// </summary>
        public void DecryptFile(string inputFilePath, string outputFilePath)
        {
            if (!File.Exists(inputFilePath))
                throw new FileNotFoundException("File di input non trovato", inputFilePath);

            try
            {
                byte[] encryptedBytes = File.ReadAllBytes(inputFilePath);
                byte[] decryptedBytes = Decrypt(encryptedBytes);
                File.WriteAllBytes(outputFilePath, decryptedBytes);
            }
            catch (Exception ex)
            {
                throw new CryptographicException($"Errore durante la decrittografia del file: {ex.Message}", ex);
            }
        }

        #endregion
    }
}
