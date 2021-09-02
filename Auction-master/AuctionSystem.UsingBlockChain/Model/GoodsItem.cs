using Newtonsoft.Json;
using AuctionSystem.UsingBlockChain.Asset;
using AuctionSystem.UsingBlockChain.Converter;
using AuctionSystem.UsingBlockChain.Core;
using System;

namespace AuctionSystem.UsingBlockChain.Model
{
    public class GoodsItem : ObservableObject
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

        private GoodsType.GoodsJewelryType _type;
        [JsonProperty(PropertyName = "Type")]
        public GoodsType.GoodsJewelryType Type
        {
            get { return _type; }
            set { SetProperty(ref _type, value, () => Type); }
        }

        private GoodsType.GoodsStatusType _state;
        [JsonProperty(PropertyName = "State")]
        public GoodsType.GoodsStatusType State
        {
            get
            {   
                if (this.Finalized)
                {
                    _state = GoodsType.GoodsStatusType.Done;
                }

                return _state;
            }
            set { SetProperty(ref _state, value, () => State); }
        }

        private double _startPrice;
        [JsonConverter(typeof(IntToStringJsonConverter))]
        [JsonProperty(PropertyName = "StartPrice")]
        public double StartPrice
        {
            get { return _startPrice; }
            set { SetProperty(ref _startPrice, value, () => StartPrice); }
        }

        private double _currentPrice = 0;
        [JsonConverter(typeof(IntToStringJsonConverter))]
        [JsonProperty(PropertyName = "CurrentPrice")]
        public double CurrentPrice
        {
            get { return _currentPrice; }
            set { SetProperty(ref _currentPrice, value, () => CurrentPrice); }
        }

        private string _owner;
        [JsonProperty(PropertyName = "Owner")]
        public string Owner
        {
            get { return (_owner != null) ? _owner : string.Empty; }
            set { SetProperty(ref _owner, value, () => Owner); }
        }

        private string _endTime;
        [JsonProperty(PropertyName = "EndTime")]
        public string EndTime
        {
            get { return (_endTime != null) ? _endTime : string.Empty; }
            set { SetProperty(ref _endTime, value, () => EndTime); }
        }

        private int _dday = 0;
        [JsonConverter(typeof(IntToStringJsonConverter))]
        public int Dday
        {
            get { return _dday; }
            set { SetProperty(ref _dday, value, () => Dday); }
        }

        private bool _deadline = false;
        [JsonProperty(PropertyName = "Deadline")]
        public bool Deadline
        {
            get { return _deadline; }
            set
            {
                SetProperty(ref _deadline, value, () => Deadline);
            }
        }

        private bool _appraisal;
        [JsonProperty(PropertyName = "Appraisal")]
        public bool Appraisal
        {
            get { return _appraisal; }
            set
            {
                SetProperty(ref _appraisal, value, () => Appraisal);
            }
        }

        private GoodsType.GoodsRareType _rare;
        [JsonProperty(PropertyName = "Rare")]
        public GoodsType.GoodsRareType Rare
        {
            get { return _rare; }
            set { SetProperty(ref _rare, value, () => Rare); }
        }

        private string _description;
        [JsonProperty(PropertyName = "Description")]
        public string Description
        {
            get { return (_description != null) ? _description : string.Empty; }
            set { SetProperty(ref _description, value, () => Description); }
        }

        private string _image;
        [JsonProperty(PropertyName = "Image")]
        public string Image
        {
            get { return (_image != null) ? _image : string.Empty; }
            set { SetProperty(ref _image, value, () => Image); }
        }

        public bool IsOwner { get; set; }

        public bool Finalized { get; set; }

        private bool _active;
        public bool Active
        {
            get
            {
                _active = DateTime.Now.CompareTo(DateTime.Parse(EndTime)) > 0 ?
                    false : true;

                return _active;
            }
        }
    }
}
