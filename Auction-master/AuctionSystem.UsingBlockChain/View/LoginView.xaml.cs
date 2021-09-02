using System.Windows.Controls;
using AuctionSystem.UsingBlockChain.Core;
using AuctionSystem.UsingBlockChain.ViewModel;

namespace AuctionSystem.UsingBlockChain.View
{
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
            ViewModelLocator.staticLoginViewModel = new LoginViewModel();
            DataContext = ViewModelLocator.staticLoginViewModel;
        }
    }
}
