using System.Windows.Controls;
using AuctionSystem.UsingBlockChain.Core;
using AuctionSystem.UsingBlockChain.ViewModel;

namespace AuctionSystem.UsingBlockChain.View
{
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
            ViewModelLocator.staticHomeViewModel = new HomeViewModel();
            DataContext = ViewModelLocator.staticHomeViewModel;
        }
    }
}
