using System;
using Luxary.Services;
using LiteDB;

namespace Luxary.Services
{
    class UserDao : Dao<User>
    {
        public UserDao(LiteDatabase database, string entityName) : base(database, entityName)
        {
        }

        public void InsertUser(ulong discordId, int Money2, int counter, string xd, int pp)
        {

            User user = Find(a => a.DiscordID == discordId);
            if (user == null)
            {
                User user2 = new User
                {
                    DiscordID = discordId,
                    Money = Money2,
                    Counter = counter,
                    Name = xd,
                    PP = pp
                };
                Save(user2);
                return;
            }

            else
            {
                user.Money += 100;
                user.Counter = counter;
            }
            //;P
            Save(user);

        }
        public void InsertMoney(ulong discordId, double Money2)
        {
            User user = Find(a => a.DiscordID == discordId);          
            user.Money += Money2;
            Save(user);
        }
        public void InsertPP(ulong discordId, int pp)
        {

            User user = Find(a => a.DiscordID == discordId);
            if (user == null)
            {
                User user2 = new User
                {
                    DiscordID = discordId,
                    PP = pp
                };
                Save(user2);
                return;
            }

            else
            {
                user.PP += pp;
            }
            //;P
            Save(user);

        }
        public void GrabMoney(ulong discordId, double Money2)
        {
            User user = Find(a => a.DiscordID == discordId);
            user.Money -= Money2;
            Save(user);
        }
       
        public int UserMoney(ulong discordId)
        {
            User user = Find(a => a.DiscordID == discordId);
            if (user != null)
            {
                return (int) user.Money;
            }
            return 0;
        }

        public int UserPP(ulong discordId)
        {
            User user = Find(a => a.DiscordID == discordId);
            if (user != null)
            {
                return user.PP;
            }
            return 0;
        }
        public int UnixCheck(ulong discordId)
        {
            User user = Find(a => a.DiscordID == discordId);
            if (user != null)
            {
                return user.Counter;
            }
            return 0;
        }
        public int UserDaily(ulong discordId)
        {
            User user = Find(a => a.DiscordID == discordId);
            if (user != null)
            {
                return user.Counter;
            }
            return 0;
        }
    }
}