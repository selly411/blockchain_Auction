using System.Windows.Controls;
using AuctionSystem.UsingBlockChain.Core;
using AuctionSystem.UsingBlockChain.ViewModel;

namespace AuctionSystem.UsingBlockChain.View
{
    public partial class PrivateKeyView : UserControl
    {
        public PrivateKeyView()
        {
            InitializeComponent();
            ViewModelLocator.staticPrivateKeyViewModel = new PrivateKeyViewModel();
            DataContext = ViewModelLocator.staticPrivateKeyViewModel;
        }
    }
}
