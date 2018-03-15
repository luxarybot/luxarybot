using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.IO;
using System.Runtime.CompilerServices;
using Luxary;
using LiteDB;
using Luxary.Services;

namespace Luxary
{
    [Group("eco")]
    public class Kekistan: ModuleBase
    {
       
        [Command("roll")]
        [Summary(".daily")]
        [Remarks("Gives your daily loan, richboi")]
        public async Task Rollerino(int amount)
        {
            var xd = Database.GetInstance().GetUserDao().UserMoney(Context.User.Id);
            if (!Context.User.IsBot)
            {
                if (amount <= xd)
                {
                    Database.GetInstance().GetUserDao().GrabMoney(Context.User.Id, amount);
                    var random = new Random();
                    var rnd = random.Next(1, 8);
                    double calc = 0;
                    string arrow = ":large_blue_diamond:";
                    if (rnd == 1)
                    {
                        arrow = ":arrow_upper_left:";
                        calc = amount * 1.5;
                    }
                    if (rnd == 2)
                    {
                        arrow = ":arrow_up:";
                        calc = amount * 1.7;
                    }
                    if (rnd == 3)
                    {
                        arrow = ":arrow_upper_right:";
                        calc = amount * 2.4;
                    }
                    if (rnd == 4)
                    {
                        arrow = ":arrow_right:";
                        calc = amount * 1.2;
                    }
                    if (rnd == 5)
                    {
                        arrow = ":arrow_lower_right:";
                        calc = amount * 0.5;
                    }
                    if (rnd == 6)
                    {
                        arrow = ":arrow_down:";
                        calc = amount * 0.3;
                    }
                    if (rnd == 7)
                    {
                        arrow = ":arrow_lower_left: ";
                        calc = amount * 0.1;
                    }
                    if (rnd == 8)
                    {
                        arrow = ":arrow_left:";
                        calc = amount * 0.2;
                    }

                    Database.GetInstance().GetUserDao().InsertMoney(Context.User.Id, calc);
                    var xd2 = Database.GetInstance().GetUserDao().UserMoney(Context.User.Id);
                    var embed = new EmbedBuilder
                    {
                        Title = $"Rolled {calc}",
                        Description =
                            $"『1.5』   『1.7』   『2.4』\r\n\r\n『0.2』     {arrow}       『1.2』\r\n\r\n『0.1』  『0.3』   『0.5』\r\n\nYou now have **{xd2}** coins"
                    };
                    if (calc < amount)
                    {
                        embed.Color = new Color(255, 0, 0);
                    }
                    else
                    {
                        embed.Color = new Color(0, 255, 0);
                    }
                    await ReplyAsync("", false, embed.Build());
                }
                else
                {
                    await ReplyAsync("ur poor bastard");
                }
            }
            else
            {
                await ReplyAsync("cheater");
            }
        }

        [Command("test")]
        public async Task xd([Remainder]string kek)
        {
            await ReplyAsync(kek);
        }
        [Command("shop")]
        [Summary(".daily")]
        [Remarks("Gives your daily loan, richboi")]
        public async Task Shop()
        {
            var shop = new EmbedBuilder
            {
                Title = "Shop",
                Description = "----"
            };
            shop.AddField(y =>
            {
                y.Name = "Weeb rank";
                y.Value = $"1337 coins";
                y.IsInline = true;
            });
            shop.AddField(y =>
            {
                y.Name = "Kappa rank";
                y.Value = $"Kappa coins";
                y.IsInline = true;
            });
            shop.AddField(y =>
            {
                y.Name = "Kappa rank";
                y.Value = $"Kappa coins";
                y.IsInline = true;
            });
            shop.AddField(y =>
            {
                y.Name = "Kappa rank";
                y.Value = $"Kappa coins";
                y.IsInline = true;
            });
            shop.AddField(y =>
            {
                y.Name = "Kappa rank";
                y.Value = $"Kappa coins";
                y.IsInline = true;
            });
            shop.AddField(y =>
            {
                y.Name = "Kappa rank";
                y.Value = $"Kappa coins";
                y.IsInline = true;
            });
            await ReplyAsync("", false, shop.Build());
        }

        [Command("daily")]
        [Summary(".daily")]
        [Remarks("Gives your daily loan, richboi")]
        public async Task Daily()
        {
            var foo = DateTime.Now;
            var unixTime = ((DateTimeOffset)foo).ToUnixTimeSeconds();
            var checktime = Database.GetInstance().GetUserDao().UnixCheck(Context.User.Id);
            if (unixTime > checktime)
            {
                Database.GetInstance().GetUserDao().InsertUser(Context.User.Id, 100, (int)(unixTime + 86400), Context.User.Username);
                await ReplyAsync($"ez 100 bucks");
            }
            else
            {
                var calc = checktime - unixTime;
                TimeSpan t = TimeSpan.FromSeconds(calc);

                string answer = string.Format("**{0:D2}:{1:D2}:{2:D2}**",
                    t.Hours,
                    t.Minutes,
                    t.Seconds);
                var xd = new EmbedBuilder
                {
                    Title = "Tick tock",
                    Description = ($"You need to wait: {answer} for your next daily.")
                };
                await ReplyAsync("", false, xd.Build());
            }
        }

        [Command("money")]
        [Summary(".money")]
        [Remarks("Shows your wallet")]
        public async Task Money()
        {
            var xd = Database.GetInstance().GetUserDao().UserMoney(Context.User.Id);
            await ReplyAsync(xd.ToString());
        }
    }
}
