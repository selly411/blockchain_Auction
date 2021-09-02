using System.Windows.Controls;
using AuctionSystem.UsingBlockChain.Core;
using AuctionSystem.UsingBlockChain.ViewModel;

namespace AuctionSystem.UsingBlockChain.View
{
    public partial class BidInputView : UserControl
    {
        public BidInputView()
        {
            InitializeComponent();
            ViewModelLocator.staticBidInputViewModel = new BidInputViewModel();
            DataContext = ViewModelLocator.staticBidInputViewModel;
        }
    }
}
