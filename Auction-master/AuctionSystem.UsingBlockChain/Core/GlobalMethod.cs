using System;
using System.IO;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AuctionSystem.UsingBlockChain.Model;

namespace AuctionSystem.UsingBlockChain.Core
{
    public static class Global
    {
        private static string AppRoaming;
        public static string GetAppRoamingPath()
        {
            if (string.IsNullOrEmpty(AppRoaming))
            {
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                AppRoaming = Path.Combine(folder, "Auction");

                // 폴더가 없으면 폴더를 생성한다.
                if (!Directory.Exists(AppRoaming))
                    Directory.CreateDirectory(AppRoaming);
            }
            return AppRoaming;
        }

        //public static IAuctionTransactions Transaction = new BlockChain();
        public static BlockChain Transaction = new BlockChain();
        public static IpfsService Ipfs = new IpfsService();
        public static SQLiteConnection Conn = null;
        public static UserItem CurrentUser;
        public static GoodsItem SelectGoods;
        public static ObservableCollection<UserBiddingItem> UserBiddingItems = new ObservableCollection<UserBiddingItem>();
        public static ObservableCollection<AuctionHistoryItem> AuctionHistoryItems = new ObservableCollection<AuctionHistoryItem>();

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            return new ObservableCollection<T>(source);
        }
    }
}
