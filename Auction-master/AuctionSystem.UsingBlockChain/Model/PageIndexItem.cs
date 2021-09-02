using Newtonsoft.Json;
using AuctionSystem.UsingBlockChain.Core;

namespace AuctionSystem.UsingBlockChain.Model
{
    public class PageIndexItem : ObservableObject
    {
        private bool _isChecked = false;
        [JsonIgnore]
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                SetProperty(ref _isChecked, value, () => IsChecked);
            }
        }

        private string _pageSeq;
        [JsonProperty(PropertyName = "PageSeq")]
        public string PageSeq
        {
            get { return (_pageSeq != null) ? _pageSeq : string.Empty; }
            set { SetProperty(ref _pageSeq, value, () => PageSeq); }
        }
    }
}
