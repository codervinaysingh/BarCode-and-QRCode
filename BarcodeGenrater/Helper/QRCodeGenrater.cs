
using BarcodeLib;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;



namespace BarcodeGenrater.Helper
{

    public class QRCodeGenrater
    {
        public string CreateQRCode(string inputValue)
        {
            QRCodeGenerator QrGenerator = new QRCodeGenerator();
            QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(inputValue, QRCodeGenerator.ECCLevel.Q);
           QRCode qRCode=new QRCode(QrCodeInfo);
            Bitmap QrBitmap = qRCode.GetGraphic(60);
            byte[] BitmapArray = QrBitmap.BitmapToByteArray();
            string QrUri = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray));
            return QrUri;
        }
        public string CreateBarCode(string inputValue)
        {
            MemoryStream memoryStream;
            using (memoryStream = new MemoryStream())
            {
                Barcode barcode = new Barcode();
                Image img = barcode.Encode(TYPE.CODE39, inputValue, Color.Black, Color.White, 250, 100);
                img.Save(memoryStream, ImageFormat.Png);
                string data= "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
                return data;
            }
        }
    }
    //Extension method to convert Bitmap to Byte Array
    public static class BitmapExtension
    {
        public static byte[] BitmapToByteArray(this Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}
