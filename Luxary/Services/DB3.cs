//using System;
//using Luxary.Services;
//using LiteDB;

//namespace Luxary.Services
//{
//    class UserDao : Dao<User>
//    {
//        private static int check;
//        public UserDao(LiteDatabase database, string entityName) : base(database, entityName)
//        {
//        }

//        public void InsertUser(ulong discordId, int Money2, int counter, string xd)
//        {

//            User user = Find(a => a.DailyGot == "true");
//            if (user == null)
//            {
//                User user2 = new User
//                {
//                    DiscordID = discordId,
//                    Money = Money2,
//                    Counter = counter,
//                    DailyGot = xd                 
//                };
//                Save(user2);
//                return;
//            }
            
//            else
//            {
//                user.Money += 100;
//            }
//            //;P
//            Save(user);

//        }
//        public void RegUser(ulong discordId, string xd)
//        {

//            User user = Find(a => a.DiscordID == discordId);
//            if (user == null)
//            {
//                User user2 = new User
//                {
//                    DiscordID = discordId,
//                    DailyGot = xd
//                };
//                Save(user2);
//                return;
//            }
//            //;P
//            Save(user);

//        }
//        public int UserMoney(ulong discordId)
//        {
//            User user = Find(a => a.DiscordID == discordId);
//            if (user != null)
//            {
//                return user.Money;
//            }
//            return 0;
//        }
//        public string DailyCheck(ulong discordId)
//        {
//            User user = Find(a => a.DiscordID == discordId);
//            if (user != null)
//            {
//                return user.DailyGot;
//            }
//            return "";
//        }
//        public int UserDaily(ulong discordId)
//        {
//            User user = Find(a => a.DiscordID == discordId);
//            if (user != null)
//            {
//                return user.Counter;
//            }
//            return 0;
//        }
//    }
//}