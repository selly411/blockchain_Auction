using System;
using System.Windows.Input;
using System.Collections.ObjectModel;
using AuctionSystem.UsingBlockChain.Core;
using AuctionSystem.UsingBlockChain.Model;
using System.Threading.Tasks;

namespace AuctionSystem.UsingBlockChain.ViewModel
{
    public class PrivateKeyViewModel : AuctionSystemBaseViewModel
    {
        #region - 변수 선언 -

        private string _privateKey;
        public string PrivateKey
        {
            get
            {
                return (_privateKey != null) ? _privateKey : string.Empty;
            }
            private set
            {
                SetProperty<string>(ref _privateKey, value);
            }
        }

        #endregion
        public PrivateKeyViewModel()
        {
            PrivateKey = Global.Transaction.GetPrivateKey();
        }
    }
}
