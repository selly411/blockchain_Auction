using System;
using System.Collections.ObjectModel;
using AuctionSystem.UsingBlockChain.Core;
using AuctionSystem.UsingBlockChain.Model;
using System.Threading.Tasks;

namespace AuctionSystem.UsingBlockChain.ViewModel
{
    public class UserViewModel : AuctionSystemBaseViewModel
    {
        #region - 변수 선언 -
        private UserItem _currentUserInfo;
        public UserItem CurrentUserInfo
        {
            get
            {
                if (_currentUserInfo == null)
                {
                    _currentUserInfo = new UserItem();
                }
                return _currentUserInfo;
            }
            set
            {
                SetProperty<UserItem>(ref _currentUserInfo, value);
            }
        }

        private ObservableCollection<UserBiddingItem> _userBiddingList;
        public ObservableCollection<UserBiddingItem> UserBiddingList
        {
            get
            {
                if (_userBiddingList == null)
                    _userBiddingList = new ObservableCollection<UserBiddingItem>();
                return _userBiddingList;
            }
            set
            {
                SetProperty<ObservableCollection<UserBiddingItem>>(ref _userBiddingList, value);
            }
        }

        private ObservableCollection<AuctionHistoryItem> _auctionHistoryItem;
        public ObservableCollection<AuctionHistoryItem> AuctionHistoryList
        {
            get
            {
                if (_auctionHistoryItem == null)
                    _auctionHistoryItem = new ObservableCollection<AuctionHistoryItem>();
                return _auctionHistoryItem;
            }
            set
            {
                SetProperty<ObservableCollection<AuctionHistoryItem>>(ref _auctionHistoryItem, value);
            }
        }
        #endregion
        
        private void GetUserData()
        {
            CurrentUserInfo = (UserItem)Global.CurrentUser.Clone();

            //Task.Run(() =>
            //{
            //    UserBiddingList = Global.Transaction.GetUserBiddingList().Result;
            //    AuctionHistoryList = Global.Transaction.GetUserAuctionHistoryList().Result;
            //});
            
            UserBiddingList = Global.UserBiddingItems;
            AuctionHistoryList = Global.AuctionHistoryItems;
        }

        private void InitData()
        {
            SetErrMsg(StringResource.Resource.Loading);
            try
            {
                GetUserData();
                SetErrMsg(StringResource.Resource.Normal);
            }
            catch (Exception e)
            {
                LastErrorMessage = e.Message.ToString();
                SetErrMsg(StringResource.Resource.Failed);
            }
        }

        public UserViewModel()
        {
            InitData();
        }
    }
}
