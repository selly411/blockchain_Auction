using System;
using System.Windows.Data;
using System.Globalization;

namespace AuctionSystem.UsingBlockChain.Converter
{
    public class EnumToGoodsRareTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return System.Windows.DependencyProperty.UnsetValue;

            string strValue = string.Empty;
            try
            {
                Asset.GoodsType.GoodsRareType enumValue =
                (Asset.GoodsType.GoodsRareType)Enum.Parse(
                typeof(Asset.GoodsType.GoodsRareType), value.ToString());

                if (enumValue == Asset.GoodsType.GoodsRareType.High)
                    strValue = StringResource.Resource.High;
                else if (enumValue == Asset.GoodsType.GoodsRareType.Middle)
                    strValue = StringResource.Resource.Middle;
                else if (enumValue == Asset.GoodsType.GoodsRareType.Low)
                    strValue = StringResource.Resource.Low;
            }
            catch
            {
                strValue = StringResource.Resource.Before;
            }

            return strValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Enum.ToObject(targetType, value);
        }
    }
}
