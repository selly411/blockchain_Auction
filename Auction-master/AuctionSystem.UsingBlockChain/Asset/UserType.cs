namespace AuctionSystem.UsingBlockChain.Asset
{
    public static class UserType
    {
        public enum UserLevelType
        {
            Admin = 0,
            Normal,
            VIP
        };

        public static readonly UserLevelType[] UserLevelTypes = new UserLevelType[]
        {
            UserLevelType.Admin,
            UserLevelType.Normal,
            UserLevelType.VIP
        };
    }
}
