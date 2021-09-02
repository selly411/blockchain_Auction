using System;
using AuctionSystem.UsingBlockChain.Model;

namespace AuctionSystem.UsingBlockChain.ViewModel
{
    public class LoginViewModel : AuctionSystemBaseViewModel
    {
        #region - 변수 선언 -
        private UserItem _inputUserItemInfo;
        public UserItem InputUserItemInfo
        {
            get
            {
                if (_inputUserItemInfo == null)
                {
                    _inputUserItemInfo = new UserItem();
                }
                return _inputUserItemInfo;
            }
            set
            {
                SetProperty<UserItem>(ref _inputUserItemInfo, value);
            }
        }
        #endregion

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

        public LoginViewModel()
        {
            InitData();
        }
    }
}
