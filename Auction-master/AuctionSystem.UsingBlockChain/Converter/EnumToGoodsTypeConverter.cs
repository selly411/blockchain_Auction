using System;
using System.Windows.Data;
using System.Globalization;

namespace AuctionSystem.UsingBlockChain.Converter
{
    public class EnumToGoodsTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return System.Windows.DependencyProperty.UnsetValue;

            string strValue = string.Empty;
            try
            {
                Asset.GoodsType.GoodsStatusType enumValue =
                (Asset.GoodsType.GoodsStatusType)Enum.Parse(
                typeof(Asset.GoodsType.GoodsStatusType), value.ToString());

                if (enumValue == Asset.GoodsType.GoodsStatusType.Before)
                    strValue = StringResource.Resource.Before;
                else if (enumValue == Asset.GoodsType.GoodsStatusType.Ing)
                    strValue = StringResource.Resource.Ing;
                else if (enumValue == Asset.GoodsType.GoodsStatusType.Done)
                    strValue = StringResource.Resource.Done;
                else if (enumValue == Asset.GoodsType.GoodsStatusType.Deposit)
                    strValue = StringResource.Resource.Deposit;
                else if (enumValue == Asset.GoodsType.GoodsStatusType.Delivery)
                    strValue = StringResource.Resource.Delivery;
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
