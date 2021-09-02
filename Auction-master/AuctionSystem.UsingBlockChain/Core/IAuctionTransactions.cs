using System;
using System.Data.SQLite;
using System.Collections.ObjectModel;
using AuctionSystem.UsingBlockChain.Asset;
using AuctionSystem.UsingBlockChain.Model;
using Nethereum.RPC.Eth.DTOs;
using System.Threading.Tasks;

namespace AuctionSystem.UsingBlockChain.Core
{
    public interface IAuctionTransactions
    {
        Task Initialize(string password);
        int GetPageCnt();
        ObservableCollection<GoodsItem> GetGoodsList(int CurrentIndex, int GoodsCnt);
        Task<ObservableCollection<GoodsBidDetailItem>> GetGoodsBidDetailList(int AuctionCode);
        Task<ObservableCollection<UserBiddingItem>> GetUserBiddingList();
        UserBiddingItem GetMaxGoodsPrice();
        Task<bool> SetBidding(UserBiddingItem userBiddingItem);
        Task<ObservableCollection<AuctionHistoryItem>> GetUserAuctionHistoryList();
        Task GetUserInfo(string UserId);
        bool CheckLogin(UserItem UserItem);

        Task Create(int tokenId, string title, double startPrice, string endTime, Metadata metadata);
        Task<bool> Finalize(int AuctionCode);
    }
}
