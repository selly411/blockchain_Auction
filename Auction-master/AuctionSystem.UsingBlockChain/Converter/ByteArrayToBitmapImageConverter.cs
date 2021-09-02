using System;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.IO;
using System.Globalization;

namespace AuctionSystem.UsingBlockChain.Converter
{
    public class ByteArrayToBitmapImageConverter : IValueConverter
    {
        public static BitmapImage ConvertByteArrayToBitMapImage(byte[] imageByteArray)
        {
            BitmapImage img = new BitmapImage();
            img.StreamSource = new MemoryStream(imageByteArray);
            return img;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var imageByteArray = value as byte[];
            if (imageByteArray == null) return null;
            return ConvertByteArrayToBitMapImage(imageByteArray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
