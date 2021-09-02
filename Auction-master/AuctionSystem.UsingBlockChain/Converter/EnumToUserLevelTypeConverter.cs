using System;
using System.Windows.Data;
using System.Globalization;

namespace AuctionSystem.UsingBlockChain.Converter
{
    public class EnumToUserLevelTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return System.Windows.DependencyProperty.UnsetValue;

            string strValue = string.Empty;
            try
            {
                Asset.UserType.UserLevelType enumValue =
                (Asset.UserType.UserLevelType)Enum.Parse(
                typeof(Asset.UserType.UserLevelType), value.ToString());

                if (enumValue == Asset.UserType.UserLevelType.Admin)
                    strValue = StringResource.Resource.Admin;
                else if (enumValue == Asset.UserType.UserLevelType.Normal)
                    strValue = StringResource.Resource.NormalUser;
                else if (enumValue == Asset.UserType.UserLevelType.VIP)
                    strValue = StringResource.Resource.VIP;
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
