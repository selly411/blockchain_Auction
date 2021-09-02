using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using AuctionSystem.UsingBlockChain.Core;
using AuctionSystem.UsingBlockChain.Model;
using System.Threading.Tasks;

namespace AuctionSystem.UsingBlockChain.ViewModel
{
    public class BidInputViewModel : AuctionSystemBaseViewModel
    {
        #region - 변수 선언 -
        private UserBiddingItem _inputBiddingItem;
        public UserBiddingItem InputBiddingItem
        {
            get
            {
                if (_inputBiddingItem == null)
                {
                    _inputBiddingItem = new UserBiddingItem();
                }
                return _inputBiddingItem;
            }
            set
            {
                SetProperty<UserBiddingItem>(ref _inputBiddingItem, value);
            }
        }
        #endregion

        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                SetProperty<bool>(ref _isBusy, value);
            }
        }
        
        private void GetBidInputData()
        {
            //InputBiddingItem = Global.Transaction.GetMaxGoodsPrice();
        }

        private void InitData()
        {
            SetErrMsg(StringResource.Resource.Loading);
            try
            {
                GetBidInputData();
                SetErrMsg(StringResource.Resource.Normal);
            }
            catch (Exception e)
            {
                LastErrorMessage = e.Message.ToString();
                SetErrMsg(StringResource.Resource.Failed);
            }
        }

        public BidInputViewModel()
        {
            BidOnAuctionCommand = new AsyncCommand(ExecuteBidOnAuction, CanExecuteBidOnAuction);
            InitData();
        }

        public IAsyncCommand BidOnAuctionCommand { get; private set; }
        
        private async Task ExecuteBidOnAuction()
        {
            try
            {
                IsBusy = true;
                InputBiddingItem.AuctionCode = ViewModelLocator.staticEachGoodsViewModel.SelectGoodsInfo.AuctionCode;
                await Global.Transaction.SetBidding(InputBiddingItem);
                
            }
            finally
            {
                IsBusy = false;
                ViewModelLocator.staticMainViewModel.ChangeView(View.ParameterResource.EachGoods);
            }
            
        }

        private bool CanExecuteBidOnAuction()
        {
            return !IsBusy;
        }
    }
}
