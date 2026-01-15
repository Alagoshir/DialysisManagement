using System.Drawing;
using System.Threading.Tasks;

namespace DialysisManagement.Utilities
{
    public interface IQRCodeGenerator
    {
        Task<string> GenerateQRCodeAsync(string data, string fileName);
        Task<Bitmap> GenerateQRCodeImageAsync(string data, int size = 300);
        string GetQRCodePath(string fileName);
    }
}
