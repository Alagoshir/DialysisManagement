using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using QRCoder;
using NLog;

namespace DialysisManagement.Utilities
{
    /// <summary>
    /// Helper per generazione QR code
    /// </summary>
    public class QRCodeGenerator : IQRCodeGenerator
    {
        private readonly Logger _logger;
        private readonly string _qrCodesPath;

        public QRCodeGenerator()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _qrCodesPath = Path.Combine(
                ConfigurationHelper.GetAppSetting("AttachmentsPath", ".\\Attachments\\"),
                "QRCodes");

            if (!Directory.Exists(_qrCodesPath))
            {
                Directory.CreateDirectory(_qrCodesPath);
            }
        }

        /// <summary>
        /// Genera un QR code e lo salva su file
        /// </summary>
        public async Task<string> GenerateQRCodeAsync(string data, string fileName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data))
                    throw new ArgumentException("Dati QR code vuoti", nameof(data));

                _logger.Info($"Generazione QR code: {fileName}");

                // Genera immagine QR code
                var qrImage = await GenerateQRCodeImageAsync(data);

                // Salva su file
                var filePath = Path.Combine(_qrCodesPath, $"{fileName}.png");
                qrImage.Save(filePath, ImageFormat.Png);

                _logger.Info($"QR code salvato: {filePath}");

                return filePath;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore durante la generazione del QR code: {fileName}");
                throw;
            }
        }

        /// <summary>
        /// Genera un QR code come immagine Bitmap
        /// </summary>
        public async Task<Bitmap> GenerateQRCodeImageAsync(string data, int size = 300)
        {
            try
            {
                using var qrGenerator = new QRCoder.QRCodeGenerator();
                var qrCodeData = qrGenerator.CreateQrCode(data, QRCoder.QRCodeGenerator.ECCLevel.Q);

                using var qrCode = new QRCoder.QRCode(qrCodeData);
                var qrImage = qrCode.GetGraphic(20, Color.Black, Color.White, true);

                // Ridimensiona se necessario
                if (qrImage.Width != size || qrImage.Height != size)
                {
                    var resizedImage = new Bitmap(qrImage, new Size(size, size));
                    return await Task.FromResult(resizedImage);
                }

                return await Task.FromResult(qrImage);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore durante la generazione dell'immagine QR code");
                throw;
            }
        }

        /// <summary>
        /// Ottiene il path completo di un QR code
        /// </summary>
        public string GetQRCodePath(string fileName)
        {
            return Path.Combine(_qrCodesPath, $"{fileName}.png");
        }
    }
}
