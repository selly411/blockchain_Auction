using System;
using System.Windows.Data;
using System.Globalization;

namespace AuctionSystem.UsingBlockChain.Converter
{
    public class EnumToGoodsJewelryTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return System.Windows.DependencyProperty.UnsetValue;

            string strValue = string.Empty;
            try
            {
                Asset.GoodsType.GoodsJewelryType enumValue =
                (Asset.GoodsType.GoodsJewelryType)Enum.Parse(
                typeof(Asset.GoodsType.GoodsJewelryType), value.ToString());

                if (enumValue == Asset.GoodsType.GoodsJewelryType.Etc)
                    strValue = StringResource.Resource.Etc;
                else if (enumValue == Asset.GoodsType.GoodsJewelryType.Dia)
                    strValue = StringResource.Resource.Dia;
                else if (enumValue == Asset.GoodsType.GoodsJewelryType.Ruby)
                    strValue = StringResource.Resource.Ruby;
                else if (enumValue == Asset.GoodsType.GoodsJewelryType.Amethyst)
                    strValue = StringResource.Resource.Amethyst;
                else if (enumValue == Asset.GoodsType.GoodsJewelryType.Sapphire)
                    strValue = StringResource.Resource.Sapphire;
                else if (enumValue == Asset.GoodsType.GoodsJewelryType.Aquamarine)
                    strValue = StringResource.Resource.Aquamarine;
                else if (enumValue == Asset.GoodsType.GoodsJewelryType.Tanzanite)
                    strValue = StringResource.Resource.Tanzanite;
                else if (enumValue == Asset.GoodsType.GoodsJewelryType.Opal)
                    strValue = StringResource.Resource.Opal;
                else if (enumValue == Asset.GoodsType.GoodsJewelryType.Emerald)
                    strValue = StringResource.Resource.Emerald;
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
