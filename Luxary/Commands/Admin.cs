using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Net.Http;
using Luxary.Services;
using Newtonsoft.Json;
using static Luxary.Services.WeatherDataCurrent;
using Color = Discord.Color;

namespace Luxary
{
    public class Admin : ModuleBase
    {
        [Command("SetGame")]
        [Summary(".setgame **<game>**")]
        [Remarks("Sets the game of the bot(owner only)")]
        [Alias("sg", "game")]
        public async Task setgame([Remainder] string game)
        {
            var GuildUser = await Context.Guild.GetUserAsync(Context.User.Id);
            if (GuildUser.Id != 185402901236154368)
            {
                await Context.Channel.SendMessageAsync("You can't make me play something else");
            }
            else
            {
                await (Context.Client as DiscordSocketClient).SetGameAsync(game);
                await Context.Channel.SendMessageAsync($"Successfully Set the game to *{game}*");
                Console.WriteLine($"{DateTime.Now}: Game was changed to {game}");
            }
        }

        [Command("illuminate")]
        [Summary(".illuminate **<int>**")]
        [Remarks("Deletes messages")]
        [RequireBotPermission(GuildPermission.ManageMessages)]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        [Alias("ilu", "del")]
        public async Task Purge([Remainder] int num = 0)
        {
            if (num <= 100)
            {
                var messagesToDelete = await Context.Channel.GetMessagesAsync(num + 1).Flatten();
                await Context.Channel.DeleteMessagesAsync(messagesToDelete);
                if (num == 1)
                {
                    await Context.Channel.SendMessageAsync(Context.User.Username +
                                                           "  illuminated 1 message.<:luxgasm:304937193028321280>");
                }
                else
                {
                    await Context.Channel.SendMessageAsync(Context.User.Username + "  illuminated " + num +
                                                           " messages.<:luxgasm:304937193028321280>");
                }
            }
            else
            {
                await ReplyAsync("You cannot illuminated more than 100 messages.<:luxgasm:304937193028321280>");
            }
        }

        [Command("Ban")]
        [Summary(".ban **<user>**")]
        [Remarks("Banish a player out the server.")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task BanAsync(SocketGuildUser user = null, [Remainder] string reason = null)
        {
            var GuildUser = await Context.Guild.GetUserAsync(Context.User.Id);
            if (GuildUser.Id != 185402901236154368)
            {
                await Context.Channel.SendMessageAsync("Jij mag lekker niemand bannen");
            }
            else
            {
                if (user == null) throw new ArgumentException("You must mention a user");
                if (string.IsNullOrWhiteSpace(reason)) throw new ArgumentException("You must provide a reason");

                var gld = Context.Guild as SocketGuild;
                var embed = new EmbedBuilder();
                embed.WithColor(new Color(199, 21, 112));
                embed.Title = $"**{user.Username}** was banned"; ///Who was banned///
                embed.Description =
                    $"**Username: **{user.Username}\n**Guild Name: **{user.Guild.Name}\n**Banned by: **{Context.User.Mention}!\n**Reason: **{reason}"; ///Embed values///

                await gld.AddBanAsync(user); ///bans selected user///
                await Context.Channel.SendMessageAsync("", false, embed); ///sends embed///
            }
        }

        [Command("Kick")]
        [Summary(".kick **<user>**")]
        [Remarks("Kicks a player from the server.")]
        [RequireBotPermission(GuildPermission.KickMembers)] ///Needed BotPerms///
        [RequireUserPermission(GuildPermission.KickMembers)] ///Needed User Perms///

        public async Task KickAsync(SocketGuildUser user, [Remainder] string reason)
        {
            var GuildUser = await Context.Guild.GetUserAsync(Context.User.Id);
            if (GuildUser.Id != 185402901236154368)
            {
                await Context.Channel.SendMessageAsync("Jij mag lekker niemand kicken");
            }
            else
            {
                if (user == null) throw new ArgumentException("You must mention a user");
                if (string.IsNullOrWhiteSpace(reason)) throw new ArgumentException("You must provide a reason");

                var gld = Context.Guild as SocketGuild;
                var embed = new EmbedBuilder(); ///starts embed///
                embed.WithColor(new Color(199, 21, 112)); ///hexacode colours ///
                embed.Title = $" {user.Username} has been kicked from {user.Guild.Name}"; ///who was kicked///
                embed.Description =
                    $"**Username: **{user.Username}\n**Guild Name: **{user.Guild.Name}\n**Kicked by: **{Context.User.Mention}!\n**Reason: **{reason}"; ///embed values///

                await user.KickAsync(); ///kicks selected user///
                await Context.Channel.SendMessageAsync("", false, embed); ///sends embed///
            }
        }

        static List<string> users = new List<string>();
        static string live = "offline";
        private static System.Timers.Timer timer1;
        static int amountOne = 0;
        static int amountTwo = 0;
        static string wordOne = null;
        static string wordTwo = null;
        static bool checkVote;
        public async void elapsed(Object source, System.Timers.ElapsedEventArgs e)
        {
            live = null;
            await SendResults();
        }
        [Command("poll")]
        [Summary("Makes a timed poll between two things")]
        [Remarks("/poll <word1> and <word2>")]
        public async Task poll(string word1 = null, string word2 = null)
        {
            if (word1 == word2)
            {
                await ReplyAsync("you cannoTTTT use 2 the same tags");
            }
            else if
                (live == "offline")
            {
                live = "live";
                timer1 = new System.Timers.Timer();
                timer1.Interval = (30000);
                timer1.AutoReset = false;
                timer1.Start();
                timer1.Elapsed += elapsed;
                wordOne = word1;
                wordTwo = word2;
                var embed = new EmbedBuilder();
                embed.Title = $"Started poll with {word1} and {word2}";
                embed.Description = $".vote {word1}\n.vote {word2}";
                await ReplyAsync("", false, embed.Build());
            }
            else
            {
                await ReplyAsync($"Poll is already live!\n.Vote {word1}\n.Vote {word2}");
            }
        }

        [Command("vote")]
        [Summary("vote for the poll, you can use 1 or 2 to vote or type the word.")]
        [Remarks("/vote <yourvote>")]
        public async Task Vote(string voted)
        {
            string[] array = users.ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                checkVote = false;
                if (array[i].Contains(Context.User.Id.ToString()))
                {
                    checkVote = true;
                    await ReplyAsync($"You already voted {Context.User.Mention}");
                }
            }
            if (live == "live" && checkVote == false)
            {
                users.Add(Context.User.Id.ToString());
                if (voted == wordOne)
                {
                    amountOne = amountOne + 1;
                    await ReplyAsync($"{Context.User.Mention} voted for {wordOne}");
                }
                else if (voted == wordTwo)
                {
                    amountTwo = amountTwo + 1;
                    await ReplyAsync($"{Context.User.Mention} voted for {wordTwo}");
                }
                else
                {
                    await ReplyAsync("You didnt vote for either, vote did not count");
                }
            }
        }

        public async Task SendResults()
        {
            {
                if (amountOne > amountTwo)
                {
                    var embed = new EmbedBuilder();
                    embed.Title = $"Poll ended! {wordOne} won!";
                    embed.Description =
                        $"```dsconfig\n{wordOne}: {amountOne} Points\n{wordTwo}: {amountTwo} Points\n```";
                    await ReplyAsync("", false, embed.Build());
                    live = "offline";
                }
                else if (amountOne < amountTwo)
                {
                    var embed = new EmbedBuilder();
                    embed.Title = $"Poll ended! {wordTwo} won!";
                    embed.Description =
                        $"```dsconfig\n{wordOne}: {amountOne} Points\n{wordTwo}: {amountTwo} Points\n```";
                    await ReplyAsync("", false, embed.Build());
                    live = "offline";
                }
                else
                {
                    var embed = new EmbedBuilder();
                    embed.Title = $"Poll ended! draw!";
                    embed.Description =
                        $"```dsconfig\n{wordOne}: {amountOne} Points\n{wordTwo}: {amountTwo} Points\n```";
                    await ReplyAsync("", false, embed.Build());
                    live = "offline";
                }
                checkVote = false;
            }
        }
        public async Task<string> GetWeatherAsync(string city)
        {
            var httpClient = new HttpClient();
            string URL = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid=b3fffd8d87abbe92c0437f5dc669502d";/*URL for the JSON file.  city is pass from our command*/
            var response = await httpClient.GetAsync(URL);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
       
        [Command("weather")]
        public async Task WeatherAsync([Remainder] string city = null)
        {
            try
            {
                WeatherReportCurrent weather;
                weather = JsonConvert.DeserializeObject<WeatherReportCurrent>(GetWeatherAsync(city)
                    .Result); /*We give the DeserializeObject the type of object to put the data into, and use the GetWeatherAsync to get the JSON from the URL.*/
                double longi = weather.Coord.Lon; /*Get the longitude data*/
                double lati = weather.Coord.Lat;
                double speed = weather.Wind.Speed;
                double deg = weather.Wind.Deg;
                double temp = weather.Main.Temp;
                double pres = weather.Main.Pressure;
                double humi = weather.Main.Humidity;
                double tmin = weather.Main.TempMin;
                double tmax = weather.Main.TempMax;
                string country = weather.Sys.Country;
                string naam = weather.Name;

                int clouds = weather.Clouds.All;
                var tmin1 = tmin - 274;
                var tmin2 = tmax - 274;
                var temp2 = temp - 274;
                var embed = new EmbedBuilder()
                {
                    Title = $"{naam}'s weather information",
                    
                };
                embed.AddField(x =>
                {
                    x.Name = "Coordinates";
                    x.Value = ($"Longitude: **{longi}**\nLatitude: **{lati}**");
                    x.IsInline = false;
                });
                embed.AddField(b =>
                {
                    b.Name = $"Wind";
                    b.Value = ($"Wind speed: **{speed}**\nWind direction: **{deg}**");
                    b.IsInline = false;
                });
                embed.AddField(d =>
                {
                    d.Name = $"Conditions";
                    d.Value =
                    ($"Current Temp: **{temp2}℃**\nHigh Temp: **{tmin2}℃**\nLow Temp: **{tmin1}℃**\nHumidity: **{humi}**\nBarometric Pressure: **{pres}**\nClouds: **{clouds}%**"
                    );
                    d.IsInline = false;
                });
                embed.AddField(c =>
                {
                    c.Name = $"Country information";
                    c.Value = ($"State: **{naam}**\nCountry: **{country}**");
                    c.IsInline = false;
                });
                await ReplyAsync("", false, embed.Build());
            }
            catch
            {
                var embed = new EmbedBuilder()
                {
                    Title = $"Error.",
                    ThumbnailUrl = $"https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/pic/oh.png",
                    Description = "Not a valid state."                  
                };
                await ReplyAsync("", false, embed.Build());
            }
            
        }
    }
}

    
