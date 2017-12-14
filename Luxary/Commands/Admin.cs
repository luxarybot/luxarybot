using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Net.Http;
using System.Text;
using Luxary.Services;
using Newtonsoft.Json;
using static Luxary.Services.WeatherDataCurrent;
using Color = Discord.Color;

namespace Luxary
{
    public class Admin : ModuleBase
    {
        public static char prefgl = '.';
        [Command("prefix")]
        [Alias("sinfo", "servinfo")]
        [Summary(".serverinfo")]
        [Remarks("Info about the server you're currently in")]
        public async Task Prefix(char pref)
        {
            try
            {
                await (Context.Client as DiscordSocketClient).SetGameAsync($"{pref}cmds for my commands");
                var xd = new EmbedBuilder
                {
                    Title = "Did it :D",
                    Description = $"Changed the prefix to: **{pref}**"
                };
                await ReplyAsync("", false, xd.Build());
                prefgl = pref;
                
            }
            catch (Exception e)
            {
                var xd = new EmbedBuilder
                {
                    Title = "Error",
                    Description = "You can only use **1** character."
                };
                await ReplyAsync("", false, xd.Build());
                Console.WriteLine(e);
                await ReplyAsync($"Changed the prefix to: **{pref}**");
            }
        }

        
        [Command("stats")]
        public async Task tehee()
        {
            PerformanceCounter ramCounter = new PerformanceCounter("Process", "Working Set - Private", Process.GetCurrentProcess().ProcessName);
            PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            string getCurrentCpuUsage()
            {
                return cpuCounter.NextValue() + "%";
            }
                int memsize = Convert.ToInt32((ramCounter.NextValue() / (int)(1024)) / 1024);

            await ReplyAsync($"CPU Usage: **{getCurrentCpuUsage()}**\nRAM Usage: **{memsize}MB**");
        }

        [Command("ServerInfo")]
        [Alias("sinfo", "servinfo")]
        [Summary(".serverinfo")]
        [Remarks("Info about the server you're currently in")]
        public async Task GuildInfo()
        {
            var embedBuilder = new EmbedBuilder();
            embedBuilder.WithColor(new Color(0, 71, 171));

            var gld = Context.Guild as SocketGuild;
            var client = Context.Client as DiscordSocketClient;


            if (!string.IsNullOrWhiteSpace(gld.IconUrl))
                embedBuilder.ThumbnailUrl = gld.IconUrl;
            var O = gld.Owner.Username;

            var V = gld.VoiceRegionId;
            var C = gld.CreatedAt;
            var CHV = gld.VoiceChannels.Count;
            var CHT = gld.TextChannels.Count;
            var N = gld.DefaultMessageNotifications;
            var VL = gld.VerificationLevel;
            var XD = gld.Roles.Count;
            var X = gld.MemberCount;
            var Z = client.ConnectionState;

            embedBuilder.Title = $"{gld.Name} Server Information";
            embedBuilder.Description =
                $"Server Owner: **{O}\n**Voice Region: **{V}\n**Created At: **{C}\n**MsgNtfc: **{N}\n**Verification: **{VL}\n**Role Count: **{XD}\n**Members: **{X}\n**Connection state:** {Z}\n**Text Channels:** {CHT}\n**Voice Channels:** {CHV}**";
            await ReplyAsync("", false, embedBuilder);

        }

        [Command("botinfo")]
        [Alias("binfo")]
        [Summary(".botinfo")]
        [Remarks("Shows all Bot Info.")]
        public async Task Info()
        {
            using (var process = Process.GetCurrentProcess())
            {
                var xd = Program._client;
                /*this is required for up time*/
                var embed = new EmbedBuilder();
                var application = await Context.Client.GetApplicationInfoAsync(); /*for lib version*/
                embed.ThumbnailUrl = xd.CurrentUser.GetAvatarUrl();/*pulls bot Avatar. Not needed can be removed*/
                embed.WithColor(new Color(0x4900ff)) /*Hexacode colours*/

                .AddField(y =>
                {
            /*new embed field*/
                    y.Name = "My daddy:";  /*Field name here*/
                    y.Value = application.Owner.Username; application.Owner.Id.ToString(); /*Code here. If INT convert to string*/
                    y.IsInline = true;
                })
                .AddField(y =>  /* add new field, rinse and repeat*/
                {
                    y.Name = "Bot uptime:";
                    var time = DateTime.Now - process.StartTime; /* Subtracts current time and start time to get Uptime*/
                    var sb = new StringBuilder();
                    if (time.Days > 0)
                    {
                        sb.Append($"{time.Days}d ");
                    }
                    if (time.Hours > 0)
                    {
                        sb.Append($"{time.Hours}h ");
                    }
                    if (time.Minutes > 0)
                    {
                        sb.Append($"{time.Minutes}m ");
                    }
                    sb.Append($"{time.Seconds}s ");
                    y.Value = sb.ToString();
                    y.IsInline = true;
                })
                .AddField(y =>
                {
                    y.Name = "Discord.net version:"; /*pulls discord lib version*/
                    y.Value = DiscordConfig.Version;
                    y.IsInline = true;
                }).AddField(y =>
                {
                    y.Name = "Servers:";
                    y.Value = (Context.Client as DiscordSocketClient).Guilds.Count.ToString() + " connected servers."; /*Numbers of servers the bot is in*/
                    y.IsInline = true;
                }).AddField(y =>
                {
                    y.Name = "Number Of Users:";
                    y.Value = (Context.Client as DiscordSocketClient).Guilds.Sum(g => g.Users.Count).ToString(); /*Counts users*/
                    y.IsInline = true;
                })
                .AddField(y =>
                {
                    y.Name = "Channels:";
                    y.Value = (Context.Client as DiscordSocketClient).Guilds.Sum(g => g.Channels.Count).ToString() + " connected channels.";
                    y.IsInline = true;
                });
                await ReplyAsync("",false, embed.Build());
            }
        }
        [Command("SetGame")]
        [Summary(".setgame **<game>**")]
        [Remarks("Sets the game of the bot(owner only)")]
        [Alias("sg", "game")]
        public async Task setgame([Remainder] string game)
        {
            var GuildUser = await Context.Guild.GetUserAsync(Context.User.Id);
            if (GuildUser.Id != 185402901236154368)
            {
                await Context.Channel.SendMessageAsync("Only my daddy can do this.");
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

        [Command("role")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task Role(IGuildUser user, string roles)
        {
            try
            {
                var role = user.Guild.Roles.Where(has => has.Name.ToUpper() == roles.ToUpper());
                await user.AddRolesAsync(role);
                var embed = new EmbedBuilder
                {
                    Title = "Admin",
                    Description = $"Gave **{user}** the role **{roles}**"
                };
                await ReplyAsync("", false, embed.Build());
            }
            catch (Exception)
            {
                var embed = new EmbedBuilder
                {
                    Title = "Error",
                    Description = $"idk no rights?"
                };
                await ReplyAsync("", false, embed.Build());
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
                timer1 = new System.Timers.Timer
                {
                    Interval = (30000),
                    AutoReset = false
                };
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
                var tmaxr = Math.Round(tmax, 0);
                var tminr = Math.Round(tmin, 0);
                var tempr = Math.Round(temp, 0);
                int clouds = weather.Clouds.All;
                var tmin1 = tminr - 274;
                var tmax1 = tmaxr - 274;
                var temp2 = tempr - 274;
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
                    ($"Current Temp: **{temp2}℃**\nHighest Temp: **{tmax1}℃**\nLowest Temp: **{tmin1}℃**\nHumidity: **{humi}**\nBarometric Pressure: **{pres}**\nClouds: **{clouds}%**"
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

    
