using Newtonsoft.Json;
using AuctionSystem.UsingBlockChain.Asset;
using AuctionSystem.UsingBlockChain.Converter;
using AuctionSystem.UsingBlockChain.Core;

namespace AuctionSystem.UsingBlockChain.Model
{
    public class AuctionHistoryItem : ObservableObject
    {
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

        private string _endTime;
        [JsonProperty(PropertyName = "EndTime")]
        public string EndTime
        {
            get { return (_endTime != null) ? _endTime : string.Empty; }
            set { SetProperty(ref _endTime, value, () => EndTime); }
        }

        private string _deadLineTime;
        [JsonProperty(PropertyName = "DeadLineTime")]
        public string DeadLineTime
        {
            get { return (_deadLineTime != null) ? _deadLineTime : string.Empty; }
            set { SetProperty(ref _deadLineTime, value, () => DeadLineTime); }
        }

        private int _lastBidOrder;
        [JsonConverter(typeof(IntToStringJsonConverter))]
        [JsonProperty(PropertyName = "LastBidOrder")]
        public int LastBidOrder
        {
            get { return _lastBidOrder; }
            set { SetProperty(ref _lastBidOrder, value, () => LastBidOrder); }
        }

        private double _bidPrice;
        [JsonConverter(typeof(IntToStringJsonConverter))]
        [JsonProperty(PropertyName = "BidPrice")]
        public double BidPrice
        {
            get { return _bidPrice; }
            set { SetProperty(ref _bidPrice, value, () => BidPrice); }
        }

        private GoodsType.GoodsStatusType _state;
        [JsonProperty(PropertyName = "State")]
        public GoodsType.GoodsStatusType State
        {
            get { return _state; }
            set { SetProperty(ref _state, value, () => State); }
        }
    }
}
