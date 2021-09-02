using System.Collections.Generic;
using System.Windows.Input;
using AuctionSystem.UsingBlockChain.Core;

namespace AuctionSystem.UsingBlockChain.ViewModel
{
    public class AuctionSystemBaseViewModel : ViewModelBase
    {
        #region - Popup -
        public string SideOnPopup = "OnPopup";
        public string OffPoupup = "NonPopup";

        private bool _sidePopupVisible = false;
        public bool SidePopupVisible
        {
            get
            {
                return _sidePopupVisible;
            }
            set
            {
                _sidePopupVisible = value;
                OnPropertyChanged("SidePopupVisible");
            }
        }

        private bool _popupVisible = false;
        public bool PopupVisible
        {
            get
            {
                return _popupVisible;
            }
            set
            {
                _popupVisible = value;
                OnPropertyChanged("PopupVisible");
            }
        }

        private string _sidePopupStatus;
        public string SidePopupStatus
        {
            get
            {
                if (_sidePopupStatus == null)
                {
                    _sidePopupStatus = OffPoupup;
                }
                return _sidePopupStatus;
            }
            set
            {
                SetProperty<string>(ref _sidePopupStatus, value);
                SidePopupVisible = true;
            }
        }

        private string _popupStatus;
        public string PopupStatus
        {
            get
            {
                if (_popupStatus == null)
                {
                    _popupStatus = "NonPopup";
                }
                return _popupStatus;
            }
            set
            {
                SetProperty<string>(ref _popupStatus, value);
                PopupVisible = true;
            }
        }

        public void OpenPopup(string par)
        {
            PopupStatus = par;
        }

        private DelegateCommand _openSidePopupCommand;
        public ICommand OpenSidePopupCommand
        {
            get
            {
                if (_openSidePopupCommand == null)
                    _openSidePopupCommand = new DelegateCommand(() =>
                    {
                        OpenSidePopup(SideOnPopup);
                    });
                return _openSidePopupCommand;
            }
        }
        public void OpenSidePopup(string par)
        {
            if (SidePopupStatus == SideOnPopup)
                par = OffPoupup;
            SidePopupStatus = par;
        }

        private DelegateCommand _nonPopupCommand;
        public ICommand NonPopupCommand
        {
            get
            {
                if (_nonPopupCommand == null)
                    _nonPopupCommand = new DelegateCommand(() =>
                    {
                        ClosePopup();
                    });
                return _nonPopupCommand;
            }
        }

        public void ClosePopup()
        {
            PopupVisible = false;
        }

        private string _viewStatus;
        public string ViewStatus
        {
            get
            {
                if (_viewStatus == null)
                {
                    _viewStatus = "";
                }
                return _viewStatus;
            }
            set
            {
                SetProperty<string>(ref _viewStatus, value);
            }
        }
        private DelegateCommand<string> _changeViewCommand;
        public ICommand ChangeViewCommand
        {
            get
            {
                if (_changeViewCommand == null)
                    _changeViewCommand = new DelegateCommand<string>(obj =>
                    {
                        ChangeView(obj);
                    });
                return _changeViewCommand;
            }
        }

        public void ChangeView(string obj)
        {
            ViewStatus = obj;
        }
        #endregion

        #region - ErrorMsg -
        private string _lastErrorMessage;
        public string LastErrorMessage
        {
            get
            {
                if (_lastErrorMessage == null)
                    _lastErrorMessage = string.Empty;
                return _lastErrorMessage;
            }
            set
            {
                SetProperty<string>(ref _lastErrorMessage, value);
            }
        }

        public string _errStatus = StringResource.Resource.Loading;
        private string _errMsg;
        public string ErrMsg
        {
            get
            {
                return _errMsg;
            }
            set
            {
                SetProperty<string>(ref _errMsg, value);
            }
        }
        public void SetErrMsg(string par)
        {
            if (par == StringResource.Resource.Failed && _errStatus == StringResource.Resource.Failed)
            {
                SetErrMsg(StringResource.Resource.Loading);
                SetErrMsg(StringResource.Resource.Failed);
            }
            else
            {
                _errStatus = par;
                ErrMsg = StringResource.Resource.ResourceManager.GetString(_errStatus);
            }
        }

        private List<string> _errList;
        public List<string> ErrList
        {
            get
            {
                if (_errList == null)
                    _errList = new List<string>();
                return _errList;
            }
            set
            {
                _errList = value;
                OnPropertyChanged("ErrList");
            }
        }
        #endregion
    }
}
