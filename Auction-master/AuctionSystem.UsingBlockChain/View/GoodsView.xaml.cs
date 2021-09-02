using System.Windows.Controls;
using AuctionSystem.UsingBlockChain.Core;
using AuctionSystem.UsingBlockChain.ViewModel;

namespace AuctionSystem.UsingBlockChain.View
{
    public partial class GoodsView : UserControl
    {
        public GoodsView()
        {
            InitializeComponent();
            ViewModelLocator.staticGoodsViewModel = new GoodsViewModel();
            DataContext = ViewModelLocator.staticGoodsViewModel;
        }
    }
}
