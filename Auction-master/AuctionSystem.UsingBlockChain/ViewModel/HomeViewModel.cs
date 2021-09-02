using System;
using AuctionSystem.UsingBlockChain.Model;
using AuctionSystem.UsingBlockChain.Core;
using System.Threading.Tasks;

namespace AuctionSystem.UsingBlockChain.ViewModel
{
    public class HomeViewModel : AuctionSystemBaseViewModel
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

        #endregion

        private void GetMainData()
        {
            if (ViewModelLocator.staticLoginViewModel.InputUserItemInfo != null)
            {
                Helpers.AsyncHelper.RunSync(() => Global.Transaction.GetUserInfo(ViewModelLocator.staticLoginViewModel.InputUserItemInfo.UserId));
                CurrentUserInfo = (UserItem)Global.CurrentUser.Clone();
            }
        }

        private void InitData()
        {
            SetErrMsg(StringResource.Resource.Loading);
            try
            {
                GetMainData();
                SetErrMsg(StringResource.Resource.Normal);
            }
            catch (Exception e)
            {
                LastErrorMessage = e.Message.ToString();
                SetErrMsg(StringResource.Resource.Failed);
            }
        }

        public HomeViewModel()
        {
            InitData();

            UserViewCommand = new AsyncCommand(ExecuteUserViewCommandAsync, CanExecuteUserViewCommand);
        }
        
        public IAsyncCommand UserViewCommand { get; private set; }

        private async Task ExecuteUserViewCommandAsync()
        {
            try
            {
                IsBusy = true;
                await Task.Run(() =>ViewModelLocator.staticMainViewModel.ChangeView(View.ParameterResource.User));
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool CanExecuteUserViewCommand()
        {
            return !IsBusy;
        }
    }
}
