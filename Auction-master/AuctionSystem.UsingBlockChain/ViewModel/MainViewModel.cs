using System;
using System.Windows.Input;
using AuctionSystem.UsingBlockChain.Core;
using AuctionSystem.UsingBlockChain.Model;
using System.Threading.Tasks;

namespace AuctionSystem.UsingBlockChain.ViewModel
{
    public class MainViewModel : AuctionSystemBaseViewModel
    {
        private void InitData()
        {
            SetErrMsg(StringResource.Resource.Loading);
            try
            {
                SetErrMsg(StringResource.Resource.Normal);
            }
            catch (Exception e)
            {
                LastErrorMessage = e.Message.ToString();
                SetErrMsg(StringResource.Resource.Failed);
            }
        }

        public MainViewModel()
        {
            ChageMainViewCommand = new AsyncCommand<string>(ExecuteChageMainViewCommand, CanExecuteChageMainViewCommand);

            ChangeView(View.ParameterResource.Login);
            InitData();
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

        public IAsyncCommand<string> ChageMainViewCommand { get; private set; }

        private async Task ExecuteChageMainViewCommand(string view)
        {
            IsBusy = true;

            if (view == View.ParameterResource.Login)
            {
                await Global.Transaction.Initialize(ViewModelLocator.staticLoginViewModel.InputUserItemInfo.UserPwd);
                
                if (Global.Transaction.CheckLogin(ViewModelLocator.staticLoginViewModel.InputUserItemInfo) == true)
                {
                    ChangeView(View.ParameterResource.Home);
                }
                else
                {
                    ViewModelLocator.staticLoginViewModel.InputUserItemInfo.UserPwd = string.Empty;
                    ChangeView(view);
                }
            }
            else if (view == View.ParameterResource.LogOut)
            {
                ViewModelLocator.staticLoginViewModel.InputUserItemInfo = null;
                ChangeView(View.ParameterResource.Login);
            }
            else if (view == View.ParameterResource.User)
            {
                await Global.Transaction.GetUserBiddingList();
                await Global.Transaction.GetUserAuctionHistoryList();

                ChangeView(view);
            }
            else
            {
                ChangeView(view);
            }

            IsBusy = false;
        }

        private bool CanExecuteChageMainViewCommand(string view)
        {
            return !IsBusy;
        }
    }
}
