using System.Windows.Controls;
using AuctionSystem.UsingBlockChain.Core;
using AuctionSystem.UsingBlockChain.ViewModel;

namespace AuctionSystem.UsingBlockChain.View
{
    public partial class EachGoodsView : UserControl
    {
        public EachGoodsView()
        {
            InitializeComponent();
            ViewModelLocator.staticEachGoodsViewModel = new EachGoodsViewModel();
            DataContext = ViewModelLocator.staticEachGoodsViewModel;
        }
    }
}
