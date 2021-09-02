using System.Windows.Controls;
using AuctionSystem.UsingBlockChain.Core;
using AuctionSystem.UsingBlockChain.ViewModel;

namespace AuctionSystem.UsingBlockChain.View
{
    public partial class UserView : UserControl
    {
        public UserView()
        {
            InitializeComponent();
            ViewModelLocator.staticUserViewModel = new UserViewModel();
            DataContext = ViewModelLocator.staticUserViewModel;
        }
    }
}
