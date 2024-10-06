using QRCoder;

namespace KBZLifeInsuranceCodeTest.Shared.Services.QRServices
{
    public class QRService
    {
        public byte[] GenerateQRCodeByte(string inputText)
        {
            try
            {
                using MemoryStream memoryStream = new();
                QRCodeGenerator qrGenerator = new();

                if (
                    Uri.TryCreate(
                        Uri.UnescapeDataString(inputText),
                        new UriCreationOptions(),
                        out Uri? result
                    )
                )
                {
                    inputText = result.AbsoluteUri;
                }

                QRCodeData qrCodeData = qrGenerator.CreateQrCode(inputText, QRCodeGenerator.ECCLevel.Q);
                PngByteQRCode imageByte = new(qrCodeData);

                return imageByte.GetGraphic(50);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
