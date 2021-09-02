using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using AuctionSystem.UsingBlockChain.Core;
using AuctionSystem.UsingBlockChain.Model;
using System.Windows;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IO;

namespace AuctionSystem.UsingBlockChain.ViewModel
{
    public class CreateAuctionViewModel : AuctionSystemBaseViewModel
    {
        private int _tokenId = 0;
        public int TokenId
        {
            get
            {
                if (_tokenId == 0)
                {
                    _tokenId = (new Random()).Next();
                }
                return _tokenId;
            }
            set
            {
                SetProperty<int>(ref _tokenId, value);
            }
        }
        
        private string _auctionTitle = "";
        public string AuctionTitle
        {
            get
            {
                return _auctionTitle;
            }
            set
            {
                _auctionTitle = value;
            }
        }

        private double _startPrice = 0;
        public double StartPrice
        {
            get
            {
                return _startPrice;
            }
            set
            {
                _startPrice = value;
            }
        }
        
        private string _endTime = "";
        public string EndTime
        {
            get
            {
                if (_endTime == "")
                {
                    _endTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                }

                return _endTime;
            }
            set
            {
                _endTime = value;
            }
        }

        private string _imagePath;
        public string ImagePath
        {
            get
            {
                if (_imagePath == null)
                {
                    _imagePath = @"C:\AuctionImage\1.png";
                }
                return _imagePath;
            }
            set
            {
                SetProperty<string>(ref _imagePath, value);
            }
        }

        private string _type;
        public string Type
        {
            get
            {
                if (_type == null)
                {
                    _type = "다이아몬드";
                }
                return _type;
            }
            set
            {
                SetProperty<string>(ref _type, value);
            }
        }

        private string _rare;
        public string Rare
        {
            get
            {
                if (_rare == null)
                {
                    _rare = "상";
                }
                return _rare;
            }
            set
            {
                SetProperty<string>(ref _rare, value);
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                if (_description == null)
                {
                    _description = "설명";
                }
                return _description;
            }
            set
            {
                SetProperty<string>(ref _description, value);
            }
        }
        
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

        public CreateAuctionViewModel()
        {
            InitData();
            CreateAuction = new AsyncCommand(ExecuteCreateAuctionAsync, CanExecuteCreateAuction);
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

        private string _busyContent;
        public string BusyContent
        {
            get
            {
                return (_busyContent != null) ? _busyContent : string.Empty;
            }
            set
            {
                SetProperty<string>(ref _busyContent, value);
            }
        }

        public IAsyncCommand CreateAuction { get; private set; }

        private async Task ExecuteCreateAuctionAsync()
        {
            try
            {
                IsBusy = true;

                BusyContent = "Register deed...";
                string metadata = await Global.Transaction.RegisterDeed(_tokenId,
                                                                    _imagePath,
                                                                    _type,
                                                                    _rare,
                                                                    _description);
                BusyContent = "Approve token...";
                await Global.Transaction.Approve(_tokenId);

                BusyContent = "Transfer ownership...";
                await Global.Transaction.TransferFrom(_tokenId);

                BusyContent = "Create auction...";
                await Global.Transaction.CreateAuction(_tokenId, 
                                                _auctionTitle, 
                                                metadata,
                                                _startPrice, 
                                                _endTime);
            }
            finally
            {
                IsBusy = false;
                ViewModelLocator.staticMainViewModel.ChangeView(View.ParameterResource.Goods);
            }
        }

        private bool CanExecuteCreateAuction()
        {
            return !IsBusy;
        }
    }
}
