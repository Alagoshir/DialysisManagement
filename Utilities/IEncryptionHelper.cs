using System;

namespace DialysisManagement.Utilities
{
    /// <summary>
    /// Interfaccia per servizi di crittografia
    /// </summary>
    public interface IEncryptionHelper
    {
        /// <summary>
        /// Cripta una stringa
        /// </summary>
        string Encrypt(string plainText);

        /// <summary>
        /// Decripta una stringa
        /// </summary>
        string Decrypt(string encryptedText);

        /// <summary>
        /// Cripta array di byte
        /// </summary>
        byte[] Encrypt(byte[] data);

        /// <summary>
        /// Decripta array di byte
        /// </summary>
        byte[] Decrypt(byte[] encryptedData);

        /// <summary>
        /// Hash password con BCrypt
        /// </summary>
        string HashPassword(string password);

        /// <summary>
        /// Verifica password con hash BCrypt
        /// </summary>
        bool VerifyPassword(string password, string hash);

        /// <summary>
        /// Genera salt casuale
        /// </summary>
        string GenerateSalt();

        /// <summary>
        /// Calcola hash SHA256
        /// </summary>
        string ComputeSha256Hash(string input);
    }
}
