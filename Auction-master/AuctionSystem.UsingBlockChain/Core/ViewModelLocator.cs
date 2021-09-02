using AuctionSystem.UsingBlockChain.ViewModel;

namespace AuctionSystem.UsingBlockChain.Core
{
    public class ViewModelLocator
    {
        public static MainViewModel staticMainViewModel;
        public MainViewModel MainViewModel
        {
            get
            {
                if (staticMainViewModel == null)
                {
                    staticMainViewModel = new MainViewModel();
                }
                return staticMainViewModel;
            }
        }

        public static LoginViewModel staticLoginViewModel;
        public LoginViewModel LoginViewModel
        {
            get
            {
                staticLoginViewModel = new LoginViewModel();
                return staticLoginViewModel;
            }
        }

        public static HomeViewModel staticHomeViewModel;
        public HomeViewModel HomeViewModel
        {
            get
            {
                staticHomeViewModel = new HomeViewModel();
                return staticHomeViewModel;
            }
        }

        public static UserViewModel staticUserViewModel;
        public UserViewModel UserViewModel
        {
            get
            {
                staticUserViewModel = new UserViewModel();
                return staticUserViewModel;
            }
        }

        public static GoodsViewModel staticGoodsViewModel;
        public GoodsViewModel GoodsViewModel
        {
            get
            {
                staticGoodsViewModel = new GoodsViewModel();
                return staticGoodsViewModel;
            }
        }

        public static EachGoodsViewModel staticEachGoodsViewModel;
        public EachGoodsViewModel EachGoodsViewModel
        {
            get
            {
                staticEachGoodsViewModel = new EachGoodsViewModel();
                return staticEachGoodsViewModel;
            }
        }

        public static BidInputViewModel staticBidInputViewModel;
        public BidInputViewModel BidInputViewModel
        {
            get
            {
                staticBidInputViewModel = new BidInputViewModel();
                return staticBidInputViewModel;
            }
        }

        public static CreateAuctionViewModel staticCreateAuctionViewModel;
        public CreateAuctionViewModel CreateAuctionViewModel
        {
            get
            {
                staticCreateAuctionViewModel = new CreateAuctionViewModel();
                return staticCreateAuctionViewModel;
            }
        }

        public static PrivateKeyViewModel staticPrivateKeyViewModel;
        public PrivateKeyViewModel PrivateKeyViewModel
        {
            get
            {
                staticPrivateKeyViewModel = new PrivateKeyViewModel();
                return staticPrivateKeyViewModel;
            }
        }
    }
}
