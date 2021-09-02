using System;
using System.IO;
using System.Data.SQLite;
using AuctionSystem.UsingBlockChain.Model;
using System.Collections.ObjectModel;
using AuctionSystem.UsingBlockChain.Asset;
using System.Threading.Tasks;

namespace AuctionSystem.UsingBlockChain.Core
{
    public class LocalDB// : IAuctionTransactions
    {
        public void Initialize(string password)
        {
            throw new NotImplementedException();
        }

        public Task Create(int tokenId, string title, double startPrice, string endTime, Metadata metadata)
        {
            throw new NotImplementedException();
        }
        public bool Finalize(int auctionId)
        {
            throw new NotImplementedException();
        }

        public int GetPageCnt()
        {
            int cnt = 0;
            try
            {
                // db 연결
                CreateConnection();

                // 총 상품 갯수
                string checkLoginSql = "select count(1) as cnt from Goods";
                SQLiteCommand command = new SQLiteCommand(checkLoginSql, Global.Conn);
                SQLiteDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    cnt = int.Parse(rdr["cnt"].ToString());
                }

                rdr.Close();

                // db 연결 종료
                CloseConnection();

                return cnt;
            }
            catch (Exception ex)
            {
                string errorMsg = "DB Select Fail : " + ex.ToString();
                CloseConnection();
                return cnt;
            }
        }

        // =========================================================================
        // 경매 리스트 가져오기
        // =========================================================================
        public ObservableCollection<GoodsItem> GetGoodsList(int CurrentIndex, int GoodsCnt)
        {
            ObservableCollection<GoodsItem> GoodsList = new ObservableCollection<GoodsItem>();
            try
            {
                // db 연결
                CreateConnection();

                // 상품 정보 가져오기
                int offset = (CurrentIndex - 1) * 8;
                int ListCnt = 8;
                if (GoodsCnt - (CurrentIndex * 8) < 0) ListCnt = GoodsCnt - (CurrentIndex - 1) * 8;

                string checkLoginSql = "select a.AuctionCode, a.GoodsName, a.Type, a.StartPrice, a.StartTime, a.EndTime, a.Appraisal, a.Rare, a.Description, a.Image, b.CurrentPrice ";
                checkLoginSql += "from (select AuctionCode, GoodsName, Type, StartPrice, StartTime, EndTime, Appraisal, Rare, Description, Image ";
                checkLoginSql += "from Goods limit " + ListCnt + " offset " + offset + ") a ";
                checkLoginSql += "left outer join (select AuctionCode, MAX(Price) as CurrentPrice from GoodsBidDetail group by AuctionCode) b ";
                checkLoginSql += "on a.AuctionCode = b.AuctionCode;";
                SQLiteCommand command = new SQLiteCommand(checkLoginSql, Global.Conn);
                SQLiteDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    GoodsItem goodsItem = new GoodsItem();
                    goodsItem.AuctionCode = int.Parse(rdr["AuctionCode"].ToString());
                    goodsItem.GoodsName = rdr["GoodsName"].ToString();
                    goodsItem.Type = (GoodsType.GoodsJewelryType)int.Parse(rdr["Type"].ToString());
                    goodsItem.StartPrice = int.Parse(rdr["StartPrice"].ToString());
                    if (rdr["CurrentPrice"].ToString() == "") goodsItem.CurrentPrice = 0;
                    else goodsItem.CurrentPrice = int.Parse(rdr["CurrentPrice"].ToString());
                    goodsItem.Owner = DateTime.Parse(rdr["StartTime"].ToString()).ToString("yyyy-MM-dd");
                    goodsItem.EndTime = DateTime.Parse(rdr["EndTime"].ToString()).ToString("yyyy-MM-dd");

                    if ((DateTime.Now - DateTime.Parse(rdr["StartTime"].ToString())).Days < 0)
                    {
                        goodsItem.State = GoodsType.GoodsStatusType.Before;
                    }
                    else
                    {
                        goodsItem.Dday = (DateTime.Parse(rdr["EndTime"].ToString()) - DateTime.Now).Days;
                        if (goodsItem.Dday >= 0)
                        {
                            goodsItem.State = GoodsType.GoodsStatusType.Ing;
                            if (goodsItem.Dday < 7) goodsItem.Deadline = true;
                        }
                        else
                        {
                            goodsItem.State = GoodsType.GoodsStatusType.Done;
                        }
                    }

                    goodsItem.Appraisal = Convert.ToBoolean(int.Parse(rdr["Appraisal"].ToString()));
                    goodsItem.Rare = (GoodsType.GoodsRareType)int.Parse(rdr["Rare"].ToString());
                    goodsItem.Description = rdr["Description"].ToString();
                    goodsItem.Image = rdr["Image"].ToString();

                    GoodsList.Add(goodsItem);
                }

                rdr.Close();

                // db 연결 종료
                CloseConnection();

                return GoodsList;
            }
            catch (Exception ex)
            {
                string errorMsg = "DB Select Fail : " + ex.ToString();
                CloseConnection();
                return GoodsList;
            }
        }

        // =========================================================================
        // 경매 물품 상세 내역 가져오기
        // =========================================================================
        public ObservableCollection<GoodsBidDetailItem> GetGoodsBidDetailList(int AuctionCode)
        {
            ObservableCollection<GoodsBidDetailItem> GoodsBidDetailList = new ObservableCollection<GoodsBidDetailItem>();
            try
            {
                // db 연결
                CreateConnection();

                // 상품 정보 가져오기
                string sql = "select BidOrder, UserSeq, BidTime, Price from GoodsBidDetail where AuctionCode = " + AuctionCode + " order by BidOrder Desc";
                SQLiteCommand command = new SQLiteCommand(sql, Global.Conn);
                SQLiteDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    GoodsBidDetailItem goodsBidDetailItem = new GoodsBidDetailItem();
                    goodsBidDetailItem.BidOrder = int.Parse(rdr["BidOrder"].ToString());
                    goodsBidDetailItem.UserSeq = int.Parse(rdr["UserSeq"].ToString());
                    goodsBidDetailItem.BidTime = DateTime.Parse(rdr["BidTime"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    goodsBidDetailItem.Price = int.Parse(rdr["Price"].ToString());
                    if (goodsBidDetailItem.UserSeq == Global.CurrentUser.UserSeq) goodsBidDetailItem.MyBid = true;

                    GoodsBidDetailList.Add(goodsBidDetailItem);
                }

                rdr.Close();

                // db 연결 종료
                CloseConnection();

                return GoodsBidDetailList;
            }
            catch (Exception ex)
            {
                string errorMsg = "DB Select Fail : " + ex.ToString();
                CloseConnection();
                return GoodsBidDetailList;
            }
        }

        // =========================================================================
        // 사용자가 현재 진행 중인 경매 리스트 가져오기
        // =========================================================================
        public ObservableCollection<UserBiddingItem> GetUserBiddingList()
        {
            ObservableCollection<UserBiddingItem> UserBiddingList = new ObservableCollection<UserBiddingItem>();
            try
            {
                // db 연결
                CreateConnection();

                // 상품 정보 가져오기
                string sql = "select a.AuctionCode, a.BidPrice, b.MyPrice, c.GoodsName, c.StartPrice, c.StartTime, c.EndTime ";
                sql += "from (select AuctionCode, Max(Price) as BidPrice from GoodsBidDetail group by AuctionCode) a ";
                sql += "INNER JOIN(select AuctionCode, Max(Price) as MyPrice from GoodsBidDetail where UserSeq = " + Global.CurrentUser.UserSeq + " group by AuctionCode) b ";
                sql += "ON a.AuctionCode = b.AuctionCode ";
                sql += "INNER JOIN (select AuctionCode, GoodsName, StartPrice, StartTime, EndTime from Goods) c ";
                sql += "ON b.AuctionCode = c.AuctionCode";
                SQLiteCommand command = new SQLiteCommand(sql, Global.Conn);
                SQLiteDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    UserBiddingItem userBiddingItem = new UserBiddingItem();
                    userBiddingItem.AuctionCode = int.Parse(rdr["AuctionCode"].ToString());
                    userBiddingItem.BidPrice = int.Parse(rdr["BidPrice"].ToString());
                    userBiddingItem.MyPrice = int.Parse(rdr["MyPrice"].ToString());
                    userBiddingItem.GoodsName = rdr["GoodsName"].ToString();
                    userBiddingItem.StartPrice = int.Parse(rdr["StartPrice"].ToString());
                    userBiddingItem.Dday = (DateTime.Parse(rdr["EndTime"].ToString()) - DateTime.Parse(rdr["StartTime"].ToString())).Days;
                    if (userBiddingItem.BidPrice == userBiddingItem.MyPrice) userBiddingItem.IsBid = true;

                    UserBiddingList.Add(userBiddingItem);
                }

                rdr.Close();

                // db 연결 종료
                CloseConnection();

                return UserBiddingList;
            }
            catch (Exception ex)
            {
                string errorMsg = "DB Select Fail : " + ex.ToString();
                CloseConnection();
                return UserBiddingList;
            }
        }

        // =========================================================================
        // 최대 입찰가 가져오기
        // =========================================================================
        public UserBiddingItem GetMaxGoodsPrice()
        {
            UserBiddingItem userBiddingItem = new UserBiddingItem();
            try
            {
                // db 연결
                CreateConnection();

                // 상품 정보 가져오기
                string sql = "select AuctionCode, BidOrder, Price from GoodsBidDetail where AuctionCode = " + Global.SelectGoods.AuctionCode + " order by BidOrder desc limit 1";
                SQLiteCommand command = new SQLiteCommand(sql, Global.Conn);
                SQLiteDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    userBiddingItem.AuctionCode = int.Parse(rdr["AuctionCode"].ToString());
                    userBiddingItem.BidOrder = int.Parse(rdr["BidOrder"].ToString());
                    userBiddingItem.BidPrice = int.Parse(rdr["Price"].ToString());
                    userBiddingItem.MyPrice = int.Parse(rdr["Price"].ToString());
                }

                rdr.Close();

                // db 연결 종료
                CloseConnection();

                return userBiddingItem;
            }
            catch (Exception ex)
            {
                string errorMsg = "DB Select Fail : " + ex.ToString();
                CloseConnection();
                return userBiddingItem;
            }
        }

        // =========================================================================
        // 입찰가 셋팅하기
        // =========================================================================
        public bool SetBidding(UserBiddingItem userBiddingItem)
        {
            try
            {
                // db 연결
                CreateConnection();

                // 상품 정보 가져오기
                int currentBidOrder = userBiddingItem.BidOrder + 1;
                string sql = "insert into GoodsBidDetail (AuctionCode, BidOrder, UserSeq, BidTime, Price) values (";
                sql += userBiddingItem.AuctionCode + ", " + currentBidOrder + ", " + Global.CurrentUser.UserSeq;
                sql += ", '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " + userBiddingItem.MyPrice + ")";

                SQLiteCommand command = new SQLiteCommand(sql, Global.Conn);
                if (command.ExecuteNonQuery() == 1)
                {
                    // db 연결 종료
                    CloseConnection();
                    return true;
                }
                else
                {
                    // db 연결 종료
                    CloseConnection();
                    return false;
                }
            }
            catch (Exception ex)
            {
                string errorMsg = "DB Select Fail : " + ex.ToString();
                CloseConnection();
                return false;
            }
        }

        // =========================================================================
        // 유저별 경매 히스토리 가져오기
        // =========================================================================
        public ObservableCollection<AuctionHistoryItem> GetUserAuctionHistoryList()
        {
            ObservableCollection<AuctionHistoryItem> AuctionHistoryList = new ObservableCollection<AuctionHistoryItem>();
            try
            {
                // db 연결
                CreateConnection();

                // 상품 정보 가져오기
                string sql = "select AuctionCode, GoodsName, EndTime, LastBidOrder, BidPrice, State from AuctionHistory where UserSeq = " + Global.CurrentUser.UserSeq;
                sql += " order by EndTime desc";
                SQLiteCommand command = new SQLiteCommand(sql, Global.Conn);
                SQLiteDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    AuctionHistoryItem auctionHistoryItem = new AuctionHistoryItem();
                    auctionHistoryItem.State = (GoodsType.GoodsStatusType)int.Parse(rdr["State"].ToString());
                    auctionHistoryItem.AuctionCode = int.Parse(rdr["AuctionCode"].ToString());
                    auctionHistoryItem.GoodsName = rdr["GoodsName"].ToString();
                    auctionHistoryItem.EndTime = DateTime.Parse(rdr["EndTime"].ToString()).ToString("yyyy-MM-dd");
                    auctionHistoryItem.DeadLineTime = DateTime.Parse(rdr["EndTime"].ToString()).AddDays(7).ToString("yyyy-MM-dd");
                    auctionHistoryItem.BidPrice = int.Parse(rdr["BidPrice"].ToString());
                    auctionHistoryItem.LastBidOrder = int.Parse(rdr["LastBidOrder"].ToString());

                    AuctionHistoryList.Add(auctionHistoryItem);
                }

                rdr.Close();

                // db 연결 종료
                CloseConnection();

                return AuctionHistoryList;
            }
            catch (Exception ex)
            {
                string errorMsg = "DB Select Fail : " + ex.ToString();
                CloseConnection();
                return AuctionHistoryList;
            }
        }

        // =========================================================================
        // 유저정보 가져오기
        // =========================================================================
        public void GetUserInfo(string UserId)
        {
            try
            {
                // db 연결
                CreateConnection();

                // 유저 정보 가져 옴
                string sql = "select UserSeq, UserName, UserId, UserLevel, Address from UserInfo where UserId = '" + UserId + "'";
                SQLiteCommand command = new SQLiteCommand(sql, Global.Conn);
                SQLiteDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    Global.CurrentUser = new UserItem();
                    Global.CurrentUser.UserSeq = int.Parse(rdr["UserSeq"].ToString());
                    Global.CurrentUser.UserName = rdr["UserName"].ToString();
                    Global.CurrentUser.UserId = rdr["UserId"].ToString();
                    Global.CurrentUser.UserLevel = (UserType.UserLevelType)int.Parse(rdr["UserLevel"].ToString());
                    Global.CurrentUser.Address = rdr["Address"].ToString();
                }

                rdr.Close();

                // db 연결 종료
                CloseConnection();
            }
            catch (Exception ex)
            {
                string errorMsg = "DB Select Fail : " + ex.ToString();
                CloseConnection();
            }
        }

        // =========================================================================
        // 로그인 체크하기
        // =========================================================================
        public bool CheckLogin(UserItem UserItem)
        {
            try
            {
                // db 연결
                CreateConnection();

                // 로그인 가능한지 체크
                string checkLoginSql = "select count(1) as cnt from UserInfo where UserId = '" + UserItem.UserId + "' and UserPwd = '" + UserItem.UserPwd + "'";
                SQLiteCommand command = new SQLiteCommand(checkLoginSql, Global.Conn);
                SQLiteDataReader rdr = command.ExecuteReader();

                int cnt = 0;
                while (rdr.Read())
                {
                    cnt = int.Parse(rdr["cnt"].ToString());
                }

                rdr.Close();

                // db 연결 종료
                CloseConnection();

                if (cnt == 1) return true;
                else return false;
            }
            catch (Exception ex)
            {
                string errorMsg = "DB Select Fail : " + ex.ToString();
                CloseConnection();
                return false;
            }
        }

        public void CreateSsampleData()
        {
            // table 생성
            string createUserTableSql = "create table UserInfo (UserSeq integer PRIMARY KEY AUTOINCREMENT, UserName varchar(45), UserId varchar(45), UserPwd varchar(45), UserLevel int, Address varchar(500))";
            string createGoodsTableSql = "create table Goods (AuctionCode integer PRIMARY KEY AUTOINCREMENT, GoodsName varchar(45), Type int, StartPrice int, StartTime DATETIME, EndTime DATETIME, Appraisal int, Rare int, Description varchar(1024), Image varchar(100))";
            string createGoodsBidDetailTableSql = "create table GoodsBidDetail (AuctionCode int, BidOrder int, UserSeq int, BidTime DATETIME, Price int)";
            string createAuctionTableSql = "create table AuctionHistory (AuctionCode int, GoodsName varchar(45), EndTime DATETIME, LastBidOrder int, BidPrice int, UserSeq int, State int)";

            SQLiteCommand commandUser = new SQLiteCommand(createUserTableSql, Global.Conn);
            int userResult = commandUser.ExecuteNonQuery();

            SQLiteCommand commandGoods = new SQLiteCommand(createGoodsTableSql, Global.Conn);
            int goodsResult = commandGoods.ExecuteNonQuery();

            SQLiteCommand commandGoodsBidDetail = new SQLiteCommand(createGoodsBidDetailTableSql, Global.Conn);
            int goodsBidDetailResult = commandGoodsBidDetail.ExecuteNonQuery();

            SQLiteCommand commandAuction = new SQLiteCommand(createAuctionTableSql, Global.Conn);
            int auctionResult = commandAuction.ExecuteNonQuery();

            // sample user 생성
            string insertUserSql = "insert into UserInfo (UserName, UserId, UserPwd, UserLevel, Address) values ('관리자', 'Admin', '1234', 0, '서울특별시 영등포구 여의도동 국제금융로 10');";
            insertUserSql += "insert into UserInfo (UserName, UserId, UserPwd, UserLevel, Address) values ('김태훈', 'kth', '123', 1, '서울특별시 양천구 신정동 313-1');";
            insertUserSql += "insert into UserInfo (UserName, UserId, UserPwd, UserLevel, Address) values ('엄재웅', 'ejw', '123', 2, '서울특별시 관악구 신림동 1707');";
            insertUserSql += "insert into UserInfo (UserName, UserId, UserPwd, UserLevel, Address) values ('최지혜', 'cjh', '123', 2, '서울특별시 중구 세종대로 110 서울특별시청');";
            insertUserSql += "insert into UserInfo (UserName, UserId, UserPwd, UserLevel, Address) values ('김주엽', 'kjy', '123', 1, '인천광역시 부평구 부개동 499-1');";
            insertUserSql += "insert into UserInfo (UserName, UserId, UserPwd, UserLevel, Address) values ('고아라', 'kar', '123', 2, '서울특별시 강남구 역삼로 451');";
            insertUserSql += "insert into UserInfo (UserName, UserId, UserPwd, UserLevel, Address) values ('정태완', 'jtw', '123', 2, '서울특별시 서초구 서초동 1650');";

            SQLiteCommand commandUserinsert = new SQLiteCommand(insertUserSql, Global.Conn);
            int retUser = commandUserinsert.ExecuteNonQuery();

            // sample goods 생성
            string insertGoodsSql = "insert into Goods (GoodsName, Type, StartPrice, StartTime, EndTime, Appraisal, Rare, Description, Image) values ('내추럴 탄자나이트 반지', 1, 2490000, '2019-10-10', '2019-10-25', 1, 0, '샤넬 백중의 백 25호입니다. 리미티드 한정판으로 내 손에 넣기 도전해 보시죠!!', '/ImageResource/1.png');";
            insertGoodsSql += "insert into Goods (GoodsName, Type, StartPrice, StartTime, EndTime, Appraisal, Rare, Description, Image) values ('보석 안에 담긴 바다 한조각', 0, 1900000000, '2019-10-15', '2019-10-17', 0, 1, '신사임당 가문에 대대로 전해 내려오던 백자입니다. 백자 감정서 및 신사임당 친필 보증서 첨부 되어 있습니다.', '/ImageResource/2.png');";
            insertGoodsSql += "insert into Goods (GoodsName, Type, StartPrice, StartTime, EndTime, Appraisal, Rare, Description, Image) values ('빅 트릴리언 아쿠아마린 반지', 5, 120000000, '2019-10-05', '2019-10-23', 1, 2, '박남규 감정사 인증서를 동반한 고려 청자입니다.', '/ImageResource/3.png');";
            insertGoodsSql += "insert into Goods (GoodsName, Type, StartPrice, StartTime, EndTime, Appraisal, Rare, Description, Image) values ('마퀴즈 사파이어 목걸이', 4, 169000, '2019-10-09', '2022-10-20', 1, 0, '유모차계에도 명품은 존재합니다! 헥스 유모차! 영국 황실의 안락함을 느껴 보세요', '/ImageResource/4.png');";
            insertGoodsSql += "insert into Goods (GoodsName, Type, StartPrice, StartTime, EndTime, Appraisal, Rare, Description, Image) values ('가든 자수정 목걸이', 3, 319000, '2019-10-21', '2022-10-20', 1, 0, '1캐럿 요미우리 광산 채집, 미야노 세공 더 말해 무엇합니까?', '/ImageResource/5.png');";
            insertGoodsSql += "insert into Goods (GoodsName, Type, StartPrice, StartTime, EndTime, Appraisal, Rare, Description, Image) values ('루비 물방울 귀걸이', 2, 309000, '2019-11-01', '2022-10-20', 1, 0, '호로로 광산에서 채집 된 3등급 원석입니다. 세공만 잘하면 더욱 빛을 발할 준비가 되어 있다죠?', '/ImageResource/6.png');";
            insertGoodsSql += "insert into Goods (GoodsName, Type, StartPrice, StartTime, EndTime, Appraisal, Rare, Description, Image) values ('루비반지', 2, 669000, '2019-11-05', '2022-10-20', 1, 0, '40년만에 한번만 나온다는 나무에서 나온 최상급의 두리안! 40년전 1억에 낙찰되었던 경력이 있습니다. 천상의 맛을 즐겨 보시죠', '/ImageResource/7.png');";

            insertGoodsSql += "insert into Goods (GoodsName, Type, StartPrice, StartTime, EndTime, Appraisal, Rare, Description, Image) values ('꽃잎서브 에메랄드 반지', 8, 559000, '2019-11-05', '2022-10-20', 1, 0, '희귀 잉어 사육 해 보시렵니까?', '/ImageResource/8.png');";
            insertGoodsSql += "insert into Goods (GoodsName, Type, StartPrice, StartTime, EndTime, Appraisal, Rare, Description, Image) values ('문스톤 다이아몬드 목걸이', 1, 1160000, '2019-11-05', '2022-10-20', 1, 0, '일본 제품 헐값에 팝니다.', '/ImageResource/9.png');";
            insertGoodsSql += "insert into Goods (GoodsName, Type, StartPrice, StartTime, EndTime, Appraisal, Rare, Description, Image) values ('크라운볼 자수정 팬던트', 3, 379000, '2019-11-05', '2022-10-20', 1, 0, '그 이름도 유명하다는 샴송 노트북 산스! SSD의 매력에 빠져 보시협니까?', '/ImageResource/10.png');";
            insertGoodsSql += "insert into Goods (GoodsName, Type, StartPrice, StartTime, EndTime, Appraisal, Rare, Description, Image) values ('트라이앵글 탄자나이트 팬던트', 6, 1490000, '2019-11-05', '2022-10-20', 1, 0, '인현왕후 민씨는 기억 하십니까? 그녀가 대대손손 물려줬다는 그 비녀, TV쇼 진품명품 134화 감상해 보시죠', '/ImageResource/11.png');";
            insertGoodsSql += "insert into Goods (GoodsName, Type, StartPrice, StartTime, EndTime, Appraisal, Rare, Description, Image) values ('패싯오팔 반지', 0, 469000, '2019-11-05', '2022-10-20', 1, 0, '아이돈 크라~이 아이돈 크라~이 루비반지!', '/ImageResource/12.png');";
            insertGoodsSql += "insert into Goods (GoodsName, Type, StartPrice, StartTime, EndTime, Appraisal, Rare, Description, Image) values ('비너스의 금발 귀걸이', 0, 649000, '2019-11-05', '2022-10-20', 1, 0, '세상 단 하나뿐인 직접 수제로 만든 그 프로포즈 반지!', '/ImageResource/13.png');";

            SQLiteCommand commandGoodsinsert = new SQLiteCommand(insertGoodsSql, Global.Conn);
            int retGoods = commandGoodsinsert.ExecuteNonQuery();

            // sample GoodsBidDetail 생성
            string insertGoodsBidDetailSql = "insert into GoodsBidDetail (AuctionCode, BidOrder, UserSeq, BidTime, Price) values (1, 1, 1, '2019-10-16 2:13:11', 3490000);";
            insertGoodsBidDetailSql += "insert into GoodsBidDetail (AuctionCode, BidOrder, UserSeq, BidTime, Price) values (1, 2, 2, '2019-10-17 2:13:11', 4490000);";
            insertGoodsBidDetailSql += "insert into GoodsBidDetail (AuctionCode, BidOrder, UserSeq, BidTime, Price) values (1, 3, 3, '2019-10-17 6:13:11', 5490000);";
            insertGoodsBidDetailSql += "insert into GoodsBidDetail (AuctionCode, BidOrder, UserSeq, BidTime, Price) values (1, 4, 1, '2019-10-18 2:13:11', 8490000);";

            insertGoodsBidDetailSql += "insert into GoodsBidDetail (AuctionCode, BidOrder, UserSeq, BidTime, Price) values (2, 1, 1, '2019-10-15 16:13:11', 1900000001);";
            insertGoodsBidDetailSql += "insert into GoodsBidDetail (AuctionCode, BidOrder, UserSeq, BidTime, Price) values (2, 2, 2, '2019-10-16 22:53:41', 1900000010);";

            SQLiteCommand commandGoodsBidDetailinsert = new SQLiteCommand(insertGoodsBidDetailSql, Global.Conn);
            int retGoodsBidDetail = commandGoodsBidDetailinsert.ExecuteNonQuery();

            // sample AuctionHistory 생성
            string insertAuctionHisSql = "insert into AuctionHistory (AuctionCode, GoodsName, EndTime, LastBidOrder, BidPrice, UserSeq, State) values (1, '내추럴 탄자나이트 반지', '2019-10-25', 4, 8490000, 1, 2);";
            insertAuctionHisSql += "insert into AuctionHistory (AuctionCode, GoodsName, EndTime, LastBidOrder, BidPrice, UserSeq, State) values (2, '보석 안에 담긴 바다 한조각', '2019-10-17', 2, 1900000010, 2, 3);";

            SQLiteCommand commandAuctionHisinsert = new SQLiteCommand(insertAuctionHisSql, Global.Conn);
            int retAuctionHis = commandAuctionHisinsert.ExecuteNonQuery();
        }

        public bool CreateConnection()
        {
            try
            {
                bool createmode = false;
                string dbFilePath = Global.GetAppRoamingPath() + "\\Auction.db";
                if (!File.Exists(dbFilePath))
                {
                    SQLiteConnection.CreateFile(dbFilePath);
                    createmode = true;
                }
                else
                {
                    FileInfo info = new FileInfo(dbFilePath);
                }

                string strConnection = "Data Source=" + dbFilePath + ";Version=3;datetimeformat=CurrentCulture;";
                Global.Conn = new SQLiteConnection(strConnection);
                Global.Conn.Open();

                if (createmode)
                {
                    CreateSsampleData();
                }
                return true;
            }
            catch (Exception ex)
            {
                CloseConnection();
                string errorMsg = "DB Connection Fail : " + ex.ToString();
                return false;
            }
        }
        public void CloseConnection()
        {
            if (Global.Conn != null)
            {
                Global.Conn.Close();
                Global.Conn = null;
            }
        }
    }
}
