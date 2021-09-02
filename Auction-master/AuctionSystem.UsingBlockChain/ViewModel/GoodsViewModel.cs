using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using AuctionSystem.UsingBlockChain.Core;
using AuctionSystem.UsingBlockChain.Model;
using System.Threading.Tasks;

namespace AuctionSystem.UsingBlockChain.ViewModel
{
    public class GoodsViewModel : AuctionSystemBaseViewModel
    {
        #region - 변수 선언 -
        int CurrentIndex = 1;
        int MaxPageIndex = 0;
        int GoodsCnt = 0;
        private ObservableCollection<PageIndexItem> _pageIndexxList;
        public ObservableCollection<PageIndexItem> PageIndexList
        {
            get
            {
                if (_pageIndexxList == null)
                    _pageIndexxList = new ObservableCollection<PageIndexItem>();
                return _pageIndexxList;
            }
            set
            {
                SetProperty<ObservableCollection<PageIndexItem>>(ref _pageIndexxList, value);
            }
        }

        private ObservableCollection<GoodsItem> _goodsList;
        public ObservableCollection<GoodsItem> GoodsList
        {
            get
            {
                if (_goodsList == null)
                    _goodsList = new ObservableCollection<GoodsItem>();
                return _goodsList;
            }
            set
            {
                SetProperty<ObservableCollection<GoodsItem>>(ref _goodsList, value);
            }
        }
        #endregion

        private void GetPageIdexList(int CurrentIndex)
        {
            PageIndexList = null;
            GoodsCnt = Global.Transaction.GetPageCnt();
            MaxPageIndex = GoodsCnt / 8 + 1;

            for (int i = 1; i <= MaxPageIndex; i++)
            {
                PageIndexItem pageindexItem = new PageIndexItem();

                if (CurrentIndex == i) pageindexItem.IsChecked = true;

                pageindexItem.PageSeq = i.ToString();
                PageIndexList.Add(pageindexItem);
            }
        }

        private void ChangePageIndexPage(string obj)
        {
            CurrentIndex = int.Parse(obj);
            GetPageIdexList(CurrentIndex);
            /*

            SearchLog();
            */
            GoodsList = Global.Transaction.GetGoodsList(CurrentIndex, GoodsCnt);
        }

        private DelegateCommand<string> _pagePopupCommand;
        public ICommand PagePopupCommand
        {
            get
            {
                if (_pagePopupCommand == null)
                    _pagePopupCommand = new DelegateCommand<string>(obj =>
                    {
                        ChangePageIndexPage(obj);
                    });
                return _pagePopupCommand;
            }
        }

        private DelegateCommand<GoodsItem> _changeGoodsDetailViewCommand;
        public ICommand ChangeGoodsDetailViewCommand
        {
            get
            {
                if (_changeGoodsDetailViewCommand == null)
                    _changeGoodsDetailViewCommand = new DelegateCommand<GoodsItem>(obj =>
                    {
                        Global.SelectGoods = obj;
                        ViewModelLocator.staticMainViewModel.ChangeView(View.ParameterResource.EachGoods);
                    });
                return _changeGoodsDetailViewCommand;
            }
        }

        private DelegateCommand<GoodsItem> _createAuctionViewCommand;
        public ICommand CreateAuctionViewCommand
        {
            get
            {
                if (_createAuctionViewCommand == null)
                    _createAuctionViewCommand = new DelegateCommand<GoodsItem>(obj =>
                    {
                        Global.SelectGoods = obj;
                        //ViewModelLocator.staticCreateAuctionViewModel.EndTime = DateTime.Now.ToString("yyyyMMdd");
                        ViewModelLocator.staticMainViewModel.ChangeView(View.ParameterResource.CreateAuction);
                    });

                return _createAuctionViewCommand;
            }
        }

        private void GetData()
        {
            GetPageIdexList(CurrentIndex);
            GoodsList = Global.Transaction.GetGoodsList(CurrentIndex, GoodsCnt);
        }

        private void InitData()
        {
            SetErrMsg(StringResource.Resource.Loading);
            try
            {
                GetData();
                SetErrMsg(StringResource.Resource.Normal);
            }
            catch (Exception e)
            {
                LastErrorMessage = e.Message.ToString();
                SetErrMsg(StringResource.Resource.Failed);
            }
        }

        public GoodsViewModel()
        {
            InitData();
            RefreshGoodsItemsCommand = new AsyncCommand(ExecuteRefreshGoodsItemsAsync, CanExecuteRefreshGoodsItems);
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

        public IAsyncCommand RefreshGoodsItemsCommand { get; private set; }

        private async Task ExecuteRefreshGoodsItemsAsync()
        {
            try
            {
                IsBusy = true;

                await Global.Transaction.Initialize(ViewModelLocator.staticLoginViewModel.InputUserItemInfo.UserPwd);

                ViewModelLocator.staticMainViewModel.ChangeView(View.ParameterResource.Goods);
            }
            finally
            {
                IsBusy = false;
            }
            
        }

        private bool CanExecuteRefreshGoodsItems()
        {
            return !IsBusy;
        }
    }
}