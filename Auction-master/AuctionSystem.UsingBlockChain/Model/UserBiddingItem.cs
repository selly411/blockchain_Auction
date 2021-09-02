using Newtonsoft.Json;
using AuctionSystem.UsingBlockChain.Converter;
using AuctionSystem.UsingBlockChain.Core;

namespace AuctionSystem.UsingBlockChain.Model
{
    public class UserBiddingItem : ObservableObject
    {
        private bool _isBid = false;
        [JsonProperty(PropertyName = "IsBid")]
        public bool IsBid
        {
            get { return _isBid; }
            set
            {
                SetProperty(ref _isBid, value, () => IsBid);
            }
        }

        private int _auctionCode;
        [JsonConverter(typeof(IntToStringJsonConverter))]
        [JsonProperty(PropertyName = "AuctionCode")]
        public int AuctionCode
        {
            get { return _auctionCode; }
            set { SetProperty(ref _auctionCode, value, () => AuctionCode); }
        }

        private string _goodsName;
        [JsonProperty(PropertyName = "GoodsName")]
        public string GoodsName
        {
            get { return (_goodsName != null) ? _goodsName : string.Empty; }
            set { SetProperty(ref _goodsName, value, () => GoodsName); }
        }

        private double _startPrice;
        [JsonConverter(typeof(IntToStringJsonConverter))]
        [JsonProperty(PropertyName = "StartPrice")]
        public double StartPrice
        {
            get { return _startPrice; }
            set { SetProperty(ref _startPrice, value, () => StartPrice); }
        }

        private int _bidOrder = 0;
        [JsonConverter(typeof(IntToStringJsonConverter))]
        [JsonProperty(PropertyName = "BidOrder")]
        public int BidOrder
        {
            get { return _bidOrder; }
            set { SetProperty(ref _bidOrder, value, () => BidOrder); }
        }

        private double _bidPrice = 0;
        [JsonConverter(typeof(IntToStringJsonConverter))]
        [JsonProperty(PropertyName = "BidPrice")]
        public double BidPrice
        {
            get { return _bidPrice; }
            set { SetProperty(ref _bidPrice, value, () => BidPrice); }
        }

        private double _myPrice = 0;
        [JsonConverter(typeof(IntToStringJsonConverter))]
        [JsonProperty(PropertyName = "MyPrice")]
        public double MyPrice
        {
            get { return _myPrice; }
            set { SetProperty(ref _myPrice, value, () => MyPrice); }
        }

        private int _dday = 0;
        [JsonConverter(typeof(IntToStringJsonConverter))]
        public int Dday
        {
            get { return _dday; }
            set { SetProperty(ref _dday, value, () => Dday); }
        }
    }
}
