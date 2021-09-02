namespace AuctionSystem.UsingBlockChain.Asset
{
    public static class GoodsType
    {
        public enum GoodsStatusType
        {
            Before = 0,
            Ing,
            Done,
            Deposit,
            Delivery
        };

        public static readonly GoodsStatusType[] GoodsStatusTypes = new GoodsStatusType[]
        {
            GoodsStatusType.Before,
            GoodsStatusType.Ing,
            GoodsStatusType.Done,
            GoodsStatusType.Deposit,
            GoodsStatusType.Delivery
        };

        public enum GoodsJewelryType
        {
            Etc = 0,
            Dia,
            Ruby,
            Amethyst,
            Sapphire,
            Aquamarine,
            Tanzanite,
            Opal,
            Emerald
        };

        public static readonly GoodsJewelryType[] GoodsJewelryTypes = new GoodsJewelryType[]
        {
            GoodsJewelryType.Etc,
            GoodsJewelryType.Dia,
            GoodsJewelryType.Ruby,
            GoodsJewelryType.Amethyst,
            GoodsJewelryType.Sapphire,
            GoodsJewelryType.Aquamarine,
            GoodsJewelryType.Tanzanite,
            GoodsJewelryType.Opal,
            GoodsJewelryType.Emerald
        };

        public enum GoodsRareType
        {
            High = 0,
            Middle,
            Low
        };

        public static readonly GoodsRareType[] GoodsRareTypes = new GoodsRareType[]
        {
            GoodsRareType.High,
            GoodsRareType.Middle,
            GoodsRareType.Low
        };
    }
}
