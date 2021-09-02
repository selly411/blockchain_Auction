using Newtonsoft.Json;
using AuctionSystem.UsingBlockChain.Asset;
using AuctionSystem.UsingBlockChain.Converter;
using AuctionSystem.UsingBlockChain.Core;
using System.Collections.Generic;

namespace AuctionSystem.UsingBlockChain.Model
{
    public class UserItem : ObservableObject
    {
        private int _userSeq;
        [JsonConverter(typeof(IntToStringJsonConverter))]
        [JsonProperty(PropertyName = "UserSeq")]
        public int UserSeq
        {
            get { return _userSeq; }
            set { SetProperty(ref _userSeq, value, () => UserSeq); }
        }

        private string _userName;
        [JsonProperty(PropertyName = "UserName")]
        public string UserName
        {
            get { return (_userName != null) ? _userName : string.Empty; }
            set { SetProperty(ref _userName, value, () => UserName); }
        }

        private string _userId;
        [JsonProperty(PropertyName = "UserId")]
        public string UserId
        {
            get { return (_userId != null) ? _userId : string.Empty; }
            set { SetProperty(ref _userId, value, () => UserId); }
        }

        private string _userPwd;
        [JsonProperty(PropertyName = "UserPwd")]
        public string UserPwd
        {
            get { return (_userPwd != null) ? _userPwd : string.Empty; }
            set { SetProperty(ref _userPwd, value, () => UserPwd); }
        }

        private UserType.UserLevelType _userLevel;
        [JsonProperty(PropertyName = "UserLevel")]
        public UserType.UserLevelType UserLevel
        {
            get { return _userLevel; }
            set { SetProperty(ref _userLevel, value, () => UserLevel); }
        }

        private string _address;
        [JsonProperty(PropertyName = "Address")]
        public string Address
        {
            get { return (_address != null) ? _address : string.Empty; }
            set { SetProperty(ref _address, value, () => Address); }
        }

        private List<Deed> _deed;

        public List<Deed> Deed
        {
            get
            {
                if (_deed == null)
                {
                    _deed = new List<Model.Deed>();
                }

                return _deed;
            }

            set
            {
                SetProperty(ref _deed, value, () => Deed);
            }
        }
    }
}
