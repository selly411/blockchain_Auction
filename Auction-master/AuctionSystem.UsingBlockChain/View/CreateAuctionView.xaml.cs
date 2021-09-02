using System.Windows.Controls;
using AuctionSystem.UsingBlockChain.Core;
using AuctionSystem.UsingBlockChain.ViewModel;

namespace AuctionSystem.UsingBlockChain.View
{
    public partial class CreateAuctionView : UserControl
    {
        public CreateAuctionView()
        {
            InitializeComponent();
            ViewModelLocator.staticCreateAuctionViewModel = new CreateAuctionViewModel();
            DataContext = ViewModelLocator.staticCreateAuctionViewModel;
        }
    }
}
