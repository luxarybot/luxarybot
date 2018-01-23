//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Reflection;
//using System.Threading.Tasks;
//using Microsoft.Extensions.DependencyInjection;
//using Discord;
//using Discord.WebSocket;
//using Discord.Commands;
//using System.IO;
//using System.Runtime.CompilerServices;
//using Luxary;
//using LiteDB;
//using Luxary.Services;

//namespace Luxary
//{
//    [Group("eco")]
//    public class Eco : ModuleBase<ICommandContext>
//    {
//        [Command("register")]
//        [Summary(".register")]
//        [Remarks("kek")]
//        public async Task Register()
//        {
//            Database.GetInstance().GetUserDao().RegUser(Context.User.Id, "false");
//        }

//        [Command("daily")]
//        [Summary(".daily")]
//        [Remarks("Gives your daily loan, richboi")]
//        public async Task Daily()
//        {
//            DateTime foo = DateTime.Now;
//            long unixTime = ((DateTimeOffset)foo).ToUnixTimeSeconds();
//            //var xd = Database.GetInstance().GetUserDao().UserDaily(Context.User.Id);
//            //var Check = Database.GetInstance().GetUserDao().DailyCheck(Context.User.Id);
            
//            if (Database.GetInstance().GetUserDao().DailyCheck(Context.User.Id)=="false")
//            {
//                Database.GetInstance().GetUserDao().InsertUser(Context.User.Id, 100, (int) unixTime, "true");
//                var xd = Database.GetInstance().GetUserDao().DailyCheck(Context.User.Id);
//                await ReplyAsync($"ez 100 bucks\n{xd}");
//            }
//            else
//            {
//                var xd = Database.GetInstance().GetUserDao().DailyCheck(Context.User.Id);
//                await ReplyAsync($"Cx\n\n{xd}");
//            }
//        }
//        [Command("money")]
//        [Summary(".money")]
//        [Remarks("Shows your wallet")]
//        public async Task Money()
//        {
//            var xd =Database.GetInstance().GetUserDao().UserMoney(Context.User.Id);
//            await ReplyAsync(xd.ToString());
//        }
//    }
//}
