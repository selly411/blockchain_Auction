using Newtonsoft.Json;
using AuctionSystem.UsingBlockChain.Converter;
using AuctionSystem.UsingBlockChain.Core;

namespace AuctionSystem.UsingBlockChain.Model
{
    public class GoodsBidDetailItem : ObservableObject
    {
        private int _auctionCode;
        [JsonConverter(typeof(IntToStringJsonConverter))]
        [JsonProperty(PropertyName = "AuctionCode")]
        public int AuctionCode
        {
            get { return _auctionCode; }
            set { SetProperty(ref _auctionCode, value, () => AuctionCode); }
        }

        private int _bidOrder;
        [JsonConverter(typeof(IntToStringJsonConverter))]
        [JsonProperty(PropertyName = "BidOrder")]
        public int BidOrder
        {
            get { return _bidOrder; }
            set { SetProperty(ref _bidOrder, value, () => BidOrder); }
        }

        private string _bidTime;
        [JsonProperty(PropertyName = "BidTime")]
        public string BidTime
        {
            get { return (_bidTime != null) ? _bidTime : string.Empty; }
            set { SetProperty(ref _bidTime, value, () => BidTime); }
        }

        private double _price;
        [JsonConverter(typeof(IntToStringJsonConverter))]
        [JsonProperty(PropertyName = "Price")]
        public double Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value, () => Price); }
        }

        private int _userSeq;
        [JsonConverter(typeof(IntToStringJsonConverter))]
        [JsonProperty(PropertyName = "UserSeq")]
        public int UserSeq
        {
            get { return _userSeq; }
            set { SetProperty(ref _userSeq, value, () => UserSeq); }
        }

        private bool _myBid = false;
        [JsonProperty(PropertyName = "MyBid")]
        public bool MyBid
        {
            get { return _myBid; }
            set
            {
                SetProperty(ref _myBid, value, () => MyBid);
            }
        }
    }
}
