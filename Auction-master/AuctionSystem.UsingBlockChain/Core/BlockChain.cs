using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionSystem.UsingBlockChain.Model;
using Nethereum.Web3;
using Nethereum.HdWallet;
using System.Net;
using Nethereum.Web3.Accounts;
using System.Threading;
using System.Windows;
using AuctionSystem.UsingBlockChain.Asset;
using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Hex.HexTypes;
using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Newtonsoft.Json;
using System.IO;

namespace AuctionSystem.UsingBlockChain.Core
{
    public class BlockChain/* : IAuctionTransactions*/
    {
        private const string words = "again ripple wealth scissors wagon turn kick mammal hire column oak sun offer tomorrow fatal";

        private Wallet wallet = null;
        private Account account = null;
        private Web3 web3 = null;
                
        private string deedAddress = Properties.Settings.Default.DeedAddress;
        private string auctionAddress = Properties.Settings.Default.AuctionAddress;

        private static Contract deedContract = null;
        private static Contract auctionContract = null;

        private HexBigInteger gas = new HexBigInteger(new BigInteger(400000));
        private HexBigInteger value = new HexBigInteger(new BigInteger(0));

        ObservableCollection<GoodsItem> allAuction = new ObservableCollection<GoodsItem>();
        
        public async Task Initialize(string password)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            
            wallet = new Wallet(words, password);

            account = wallet.GetAccount(0);

            web3 = new Web3(account, Properties.Settings.Default.JsonRPCEndpoint);

            deedContract = web3.Eth.GetContract(ABI.Deed, deedAddress);
            auctionContract = web3.Eth.GetContract(ABI.Auction, auctionAddress);

            await generateAuctions();
        }

        public string GetPrivateKey()
        {
            return account.PrivateKey;
        }

        private async Task<GoodsItem> getGoodsItemByAuctionId(int auctionId)
        {
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            var auction = await getAuctionById(auctionId);

            Metadata meta = JsonConvert.DeserializeObject<Metadata>(auction.Metadata);
            
            DateTime time = unixEpoch.AddSeconds((double)auction.BlockDeadline).ToLocalTime();

            var bid = await getCurrentBid(auctionId);

            string imagePath = AppDomain.CurrentDomain.BaseDirectory + meta.ImageId + meta.Ext;

            if (!File.Exists(imagePath))
            {
                await Global.Ipfs.DownLoad(meta.ImageId, imagePath);
            }
            
            var goods = new GoodsItem
            {
                Image = imagePath,
                GoodsName = auction.Name,
                AuctionCode = auctionId,
                Appraisal = true,
                Deadline = !auction.Active,
                Description = meta.Description,
                EndTime = time.ToString("yyyy/MM/dd HH:mm"),
                State = GoodsType.GoodsStatusType.Ing,
                StartPrice = (double)Web3.Convert.FromWei(auction.StartPrice),
                Owner = auction.Owner,
                CurrentPrice = (double)Web3.Convert.FromWei(bid.Amount),
                IsOwner = string.Equals(account.Address, auction.Owner, StringComparison.OrdinalIgnoreCase),
                Finalized = auction.Finalized || (!auction.Active),
            };

            return goods;
        }

        private async Task generateAuctions()
        {
            allAuction.Clear();

            int count = await getCount();

            for (int i = 0; i < count; i++)
            {
                allAuction.Add(await getGoodsItemByAuctionId(i));
            }
        }

        public bool CheckLogin(UserItem UserItem)
        {
            return true;
        }

        public async Task CreateAuction(int tokenId, 
                                        string title,
                                        string metadata,
                                        double startPrice, 
                                        string endTime)
        {
            DateTime time = DateTime.ParseExact(endTime, "yyyy/MM/dd HH:mm", null);
            
            double deadline = time.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
            
            var receipt = await createAuction(tokenId,
                                            title,
                                            metadata,
                                            startPrice,
                                            deadline);

            if (receipt.Status.Value == 0x1)
            {
                int count = await getCount();
                var goods = await getGoodsItemByAuctionId(count - 1);

                allAuction.Add(goods);
            }
        }
        
        public async Task<string> RegisterDeed(int tokenId, 
                                                string imagePath,
                                                string type,
                                                string rare,
                                                string description)
        {
            string hash = await Global.Ipfs.Upload(imagePath);

            var metadata = new Metadata()
            {
                ImageId = hash,
                Ext = Path.GetExtension(imagePath),
                Type = type,
                Rare = rare,
                Description = description,
            };

            string metaJson = JsonConvert.SerializeObject(metadata);

            await registerDeed(tokenId, metaJson);

            return metaJson;
        }

        public async Task Approve(int tokenId)
        {
            await approve(tokenId);
        }

        public async Task TransferFrom(int tokenId)
        {
            await transferFrom(tokenId);
        }

        public async Task<ObservableCollection<GoodsBidDetailItem>> GetGoodsBidDetailList(int AuctionCode)
        {
            var bids = new ObservableCollection<GoodsBidDetailItem>();

            if (await getBidsCount(AuctionCode) > 0)
            {
                var bid = await getCurrentBid(AuctionCode);

                bids.Add(new GoodsBidDetailItem
                {
                    BidTime = bid.Bider,
                    Price = (double)Web3.Convert.FromWei(bid.Amount),
                });
            }
            
            return bids;
        }

        public ObservableCollection<GoodsItem> GetGoodsList(int CurrentIndex, int GoodsCnt)
        {
            ObservableCollection<GoodsItem> goodsList = new ObservableCollection<GoodsItem>();
            
            int offset = (CurrentIndex - 1) * 8;
            int listCnt = 8;

            if (GoodsCnt - (CurrentIndex * 8) < 0)
            {
                listCnt = GoodsCnt - (CurrentIndex - 1) * 8;
            }

            for (int i = offset; i < offset + listCnt; i++)
            {
                goodsList.Add(allAuction[i]);
            }

            return goodsList;
        }

        public UserBiddingItem GetMaxGoodsPrice()
        {
            throw new NotImplementedException();
        }

        public int GetPageCnt()
        {
            int count = allAuction.Count;

            return count;
        }

        public async Task GetUserAuctionHistoryList()
        {
            Global.AuctionHistoryItems.Clear();

            var auctionIds = await getAuctionsOf(account.Address);

            for (int i = 0; i < auctionIds.Count; i++)
            {
                int auctionId = auctionIds[i];

                int bidCount = await getBidsCount(auctionId);
                Bid bid = new Bid();

                if (bidCount > 0)
                {
                    bid = await getCurrentBid(auctionId);
                }

                Global.AuctionHistoryItems.Add(new AuctionHistoryItem()
                {
                    AuctionCode = auctionId,
                    GoodsName = allAuction[auctionId].GoodsName,
                    EndTime = allAuction[auctionId].EndTime,
                    DeadLineTime = allAuction[auctionId].EndTime,
                    LastBidOrder = bidCount,
                    BidPrice = (double)Web3.Convert.FromWei(bid.Amount),
                    State = allAuction[auctionId].State,
                });
                
            }
        }

        public async Task GetUserBiddingList()
        {
            Global.UserBiddingItems.Clear();

            for (int i = 0; i < allAuction.Count; i++)
            {
                var currentBid = await getCurrentBid(i);
                if (string.Equals(currentBid.Bider, account.Address, StringComparison.OrdinalIgnoreCase))
                {
                    Global.UserBiddingItems.Add(new UserBiddingItem()
                    {
                        AuctionCode = i,
                        IsBid = true,
                        GoodsName = allAuction[i].GoodsName,
                        StartPrice = allAuction[i].StartPrice,
                        BidOrder = 0,
                        BidPrice = (double)Web3.Convert.FromWei(currentBid.Amount),
                        MyPrice = (double)Web3.Convert.FromWei(currentBid.Amount),
                        Dday = DateTime.Parse(allAuction[i].EndTime).Subtract(DateTime.Now).Days,
                    });
                }
            }
        }
        
        public async Task GetUserInfo(string UserId)
        {
            var balance = await web3.Eth.GetBalance.SendRequestAsync(account.Address);

            Global.CurrentUser = new UserItem();
            Global.CurrentUser.UserSeq = 0;
            Global.CurrentUser.UserName = account.Address;
            Global.CurrentUser.UserId = "N/A";
            Global.CurrentUser.UserLevel = UserType.UserLevelType.Normal;
            Global.CurrentUser.Address = Web3.Convert.FromWei(balance.Value).ToString() + " ETH";
        }

        public async Task<bool> SetBidding(UserBiddingItem userBiddingItem)
        {
            var receipt = await bidOnAuction(userBiddingItem.AuctionCode,
                                            userBiddingItem.MyPrice);
            if (receipt.Status.Value == 1)
            {
                allAuction[userBiddingItem.AuctionCode].CurrentPrice = userBiddingItem.MyPrice;
                return true;
            }

            return false;
        }

        public async Task<bool> Finalize(int auctionId)
        {
            var receipt = await finalizeAuction((uint)auctionId);
            if (receipt.Status.Value == 1)
            {
                return true;
            }

            return false;
        }

        #region Contract functions

        private async Task<TransactionReceipt> registerDeed(int tokenId, string uri)
        {
            var registerDeed = deedContract.GetFunction("registerDeed");

            return await registerDeed.SendTransactionAndWaitForReceiptAsync(account.Address, gas, value, null, tokenId, uri);
        }

        private async Task<TransactionReceipt> approve(int tokenId)
        {
            var approve = deedContract.GetFunction("approve");

            return await approve.SendTransactionAndWaitForReceiptAsync(account.Address, gas, value, null, auctionAddress, tokenId);
        }

        private async Task<TransactionReceipt> transferFrom(int tokenId)
        {
            var transferFrom = deedContract.GetFunction("transferFrom");

            return await transferFrom.SendTransactionAndWaitForReceiptAsync(account.Address, gas, value, null, account.Address, auctionAddress, tokenId);
        }

        private async Task<List<int>> tokensOf(string owner)
        {
            var tokensOf = auctionContract.GetFunction("tokensOf");

            var tokens = await tokensOf.CallAsync<List<int>>(owner);

            return tokens;
        }

        /**
        * @dev Gets the length of auctions
        * @return uint representing the auction count
        */
        private async Task<int> getCount()
        {
            var getCount = auctionContract.GetFunction("getCount");

            int count = await getCount.CallAsync<int>();

            return count;
        }

        /**
        * @dev Gets the bid counts of a given auction
        * @param _auctionId uint ID of the auction
        */
        private async Task<int> getBidsCount(int auctionId)
        {
            var getBidsCount = auctionContract.GetFunction("getBidsCount");

            return await getBidsCount.CallAsync<int>(auctionId);
        }


        /**
        * @dev Gets an array of owned auctions
        * @param _owner address of the auction owner
        */
        private async Task<List<int>> getAuctionsOf(string owner)
        {
            var getAuctionsOf = auctionContract.GetFunction("getAuctionsOf");

            var auctions = await getAuctionsOf.CallAsync<List<int>>(owner);

            return auctions;
        }

        /**
        * @dev Gets an array of owned auctions
        * @param _auctionId uint of the auction owner
        * @return amount uint256, address of last bidder
        */
        private async Task<Bid> getCurrentBid(int auctionId)
        {
            var getCurrentBid = auctionContract.GetFunction("getCurrentBid");

            var bid = await getCurrentBid.CallDeserializingToObjectAsync<Bid>(auctionId);

            return bid;
        }

        /**
        * @dev Gets the total number of auctions owned by an address
        * @param _owner address of the owner
        * @return uint total number of auctions
        */
        private async Task<uint> getAuctionsCountOfOwner(string owner)
        {
            var getAuctionsCountOfOwner = auctionContract.GetFunction("getAuctionsCountOfOwner");

            return await getAuctionsCountOfOwner.CallAsync<uint>(owner);
        }

        /**
        * @dev Gets the info of a given auction which are stored within a struct
        * @param _auctionId uint ID of the auction
        * @return string name of the auction
        * @return uint256 timestamp of the auction in which it expires
        * @return uint256 starting price of the auction
        * @return string representing the metadata of the auction
        * @return uint256 ID of the deed registered in DeedRepository
        * @return address Address of the DeedRepository
        * @return address owner of the auction
        * @return bool whether the auction is active
        * @return bool whether the auction is finalized
        */
        private async Task<Auction> getAuctionById(int auctionId)
        {
            var getAuctionById = auctionContract.GetFunction("getAuctionById");

            return await getAuctionById.CallDeserializingToObjectAsync<Auction>(auctionId);
        }

        /**
        * @dev Creates an auction with the given informatin
        * @param _deedRepositoryAddress address of the DeedRepository contract
        * @param _deedId uint256 of the deed registered in DeedRepository
        * @param _auctionTitle string containing auction title
        * @param _metadata string containing auction metadata 
        * @param _startPrice uint256 starting price of the auction
        * @param _blockDeadline uint is the timestamp in which the auction expires
        * @return bool whether the auction is created
        */
        private async Task<TransactionReceipt> createAuction(int deedId, string auctionTitle, string metadata, double startPrice, double deadline)
        {
            var createAuction = auctionContract.GetFunction("createAuction");
            
            return await createAuction.SendTransactionAndWaitForReceiptAsync(account.Address,
                                                                            gas, value, null,
                                                                            deedAddress,
                                                                            deedId,
                                                                            auctionTitle,
                                                                            metadata,
                                                                            Web3.Convert.ToWei(startPrice),
                                                                            new BigInteger(deadline));
        }

        /**
        * @dev Cancels an ongoing auction by the owner
        * @dev Deed is transfered back to the auction owner
        * @dev Bidder is refunded with the initial amount
        * @param _auctionId uint ID of the created auction
        */
        private async Task<TransactionReceipt> cancelAuction(uint auctionId)
        {
            var cancelAuction = auctionContract.GetFunction("cancelAuction");

            return await cancelAuction.SendTransactionAndWaitForReceiptAsync(account.Address,
                                                                            gas, value, null,
                                                                            auctionId);
        }


        /**
        * @dev Bidder sends bid on an auction
        * @dev Auction should be active and not ended
        * @dev Refund previous bidder if a new bid is valid and placed.
        * @param _auctionId uint ID of the created auction
        */
        private async Task<TransactionReceipt> bidOnAuction(int auctionId, double value)
        {
            var bidOnAuction = auctionContract.GetFunction("bidOnAuction");

            return await bidOnAuction.SendTransactionAndWaitForReceiptAsync(account.Address,
                                                                            gas,
                                                                            new HexBigInteger(Web3.Convert.ToWei(value)),
                                                                            null,
                                                                            auctionId);
        }

        /**
        * @dev Finalized an ended auction
        * @dev The auction should be ended, and there should be at least one bid
        * @dev On success Deed is transfered to bidder and auction owner gets the amount
        * @param _auctionId uint ID of the created auction
        */
        private async Task<TransactionReceipt> finalizeAuction(uint auctionId)
        {
            var finalizeAuction = auctionContract.GetFunction("finalizeAuction");

            return await finalizeAuction.SendTransactionAndWaitForReceiptAsync(account.Address,
                                                                            gas, value, null,
                                                                            auctionId);
        }
        #endregion
    }

    [FunctionOutput]
    public class Auction
    {
        [Parameter("string", "name", 1)]
        public string Name { get; set; }

        [Parameter("uint256", "blockDeadline", 2)]
        public BigInteger BlockDeadline { get; set; }

        [Parameter("uint256", "startPrice", 3)]
        public BigInteger StartPrice { get; set; }

        [Parameter("string", "metadata", 4)]
        public string Metadata { get; set; }

        [Parameter("uint256", "deedId", 5)]
        public BigInteger DeedId { get; set; }

        [Parameter("address", "deedRepositoryAddress", 6)]
        public string DeedRepositoryAddress { get; set; }

        [Parameter("address", "owner", 7)]
        public string Owner { get; set; }

        [Parameter("bool", "active", 8)]
        public bool Active { get; set; }

        [Parameter("bool", "finalized", 9)]
        public bool Finalized { get; set; }
    }

    [FunctionOutput]
    public class Bid
    {
        [Parameter("uint256", 1)]
        public BigInteger Amount { get; set; }

        [Parameter("address", 2)]
        public string Bider { get; set; }
    }
}
