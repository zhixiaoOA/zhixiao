using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Drawing;
using ThoughtWorks.QRCode.Codec;

namespace ZX.Tools
{
    public class QrCodeHelper
    {
        private static GraphicsRenderer gRender = new GraphicsRenderer(new FixedModuleSize(8, QuietZoneModules.Four));

        //生成二维码图片，不写文件
        public static Image CreateQRCode(string CodeUrl) 
        {
            BarCodeLib.Barcode b = new BarCodeLib.Barcode();
            
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            //设置编码模式  
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //设置编码测量度  
            qrCodeEncoder.QRCodeScale = 15;
            //设置编码版本  
            qrCodeEncoder.QRCodeVersion = 0;
            //设置编码错误纠正  
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            //生成二维码图片  
            return qrCodeEncoder.Encode(CodeUrl); 
        }
        //生成条形码，不写文件
        public static Image CreateBarCode(string CodeUrl) 
        {
            BarCodeLib.Barcode b = new BarCodeLib.Barcode();
            b.RawData = CodeUrl;
            BarCodeLib.TYPE type = BarCodeLib.TYPE.Interleaved2of5;
            b.Encode(type, System.Drawing.Color.Black, System.Drawing.Color.White);
            return BarCodeLib.Barcode.DoEncode(type, b.RawData, System.Drawing.Color.Black, System.Drawing.Color.White, 300, 50);
        }
    }
}
