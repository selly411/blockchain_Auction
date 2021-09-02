using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using AuctionSystem.UsingBlockChain.Core;
using AuctionSystem.UsingBlockChain.Model;
using System.Windows;
using System.Threading.Tasks;

namespace AuctionSystem.UsingBlockChain.ViewModel
{
    public class EachGoodsViewModel : AuctionSystemBaseViewModel
    {
        #region - 변수 선언 -
        private GoodsItem _selectGoodsInfo;
        public GoodsItem SelectGoodsInfo
        {
            get
            {
                if (_selectGoodsInfo == null)
                {
                    _selectGoodsInfo = new GoodsItem();
                }
                return _selectGoodsInfo;
            }
            set
            {
                SetProperty<GoodsItem>(ref _selectGoodsInfo, value);
            }
        }

        private ObservableCollection<GoodsBidDetailItem> _goodsBidDetailList;
        public ObservableCollection<GoodsBidDetailItem> GoodsBidDetailList
        {
            get
            {
                if (_goodsBidDetailList == null)
                    _goodsBidDetailList = new ObservableCollection<GoodsBidDetailItem>();
                return _goodsBidDetailList;
            }
            set
            {
                SetProperty<ObservableCollection<GoodsBidDetailItem>>(ref _goodsBidDetailList, value);
            }
        }

        private bool _checkConfirmStatus;
        public bool CheckConfirmStatus
        {
            get { return _checkConfirmStatus; }
            set { SetProperty<bool>(ref _checkConfirmStatus, value); }
        }
        #endregion
        
        private DelegateCommand _bidOnAuctionCommand;
        public ICommand BidOnAuctionCommand
        {
            get
            {
                if (_bidOnAuctionCommand == null)
                    _bidOnAuctionCommand = new DelegateCommand(() =>
                    {
                        ViewModelLocator.staticMainViewModel.ChangeView(View.ParameterResource.BidInput);
                    });

                return _bidOnAuctionCommand;
            }
        }


        private void GetGoodsDetailData()
        {
            SelectGoodsInfo = Global.SelectGoods;//.Clone();
            GoodsBidDetailList = Helpers.AsyncHelper.RunSync(() => 
                            Global.Transaction.GetGoodsBidDetailList(SelectGoodsInfo.AuctionCode));
        }

        private void InitData()
        {
            SetErrMsg(StringResource.Resource.Loading);
            try
            {
                CheckConfirmStatus = false;
                GetGoodsDetailData();
                SetErrMsg(StringResource.Resource.Normal);
            }
            catch (Exception e)
            {
                LastErrorMessage = e.Message.ToString();
                SetErrMsg(StringResource.Resource.Failed);
            }
        }

        public EachGoodsViewModel()
        {
            InitData();

            FinalizeAuction = new AsyncCommand(ExecuteFinalizeAuctionAsync, CanExecuteFinalizeAuction);
        }

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
        

        public IAsyncCommand FinalizeAuction { get; private set; }

        private async Task ExecuteFinalizeAuctionAsync()
        {
            try
            {
                IsBusy = true;
                var status = await Global.Transaction.Finalize(SelectGoodsInfo.AuctionCode);
                if (status)
                {
                    SelectGoodsInfo.Finalized = true;
                }
            }
            finally
            {
                IsBusy = false;
                ViewModelLocator.staticMainViewModel.ChangeView(View.ParameterResource.EachGoods);
            }
        }

        private bool CanExecuteFinalizeAuction()
        {
            return !IsBusy;
        }
    }
}
