using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.Audio;
using Discord.WebSocket;
using Discord.Commands;
using System.IO;
using System.Net.Http;
using ImageSharp;
using ImageSharp.Drawing;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Timers;
using Luxary.Service;
using System.Web;
using Newtonsoft.Json;


namespace Luxary
{
    public class Luxary : ModuleBase<ICommandContext>
    {

        private CommandService _service;

        public Luxary(CommandService service)
        {
            _service = service;
        }

        [Command("element")]
        [Summary(".element")]
        [Alias("ele")]
        [Remarks("Shows a random element.")]
        public async Task element()
        {
            string[] elements = new string[]
            {
                "Fire - Air = Lightning",
                "Air - Fire = Lightning",
                "Fire - Nature = Magma",
                "Nature - Fire = Magma",
                "Water - Nature =  Mystic",
                "Nature - Water =  Mystic",
                "Water - Fire = Dark",
                "Nature - Air = Dark",
                "Air - Water = Ice",
                "Water - Air = Ice"
             };
            var rnd = new Random();
            int randomIndex = rnd.Next(elements.Length);
            string text = elements[randomIndex];
            var embed = new EmbedBuilder();
            if (text.Contains("Lightning"))
            {
                {
                    embed.Color = new Color(255, 215, 0);
                    embed.Title = $"**The element you're going to play**";
                    embed.Description = text;
                }
            }
            else if (text.Contains("Dark"))
            {
                {
                    embed.Color = new Color(128, 0, 128);
                    embed.Title = $"**The element you're going to play**";
                    embed.Description = text;
                }
            }
            else if (text.Contains("Magma"))
            {
                {
                    embed.Color = new Color(165, 42, 42);
                    embed.Title = $"**The element you're going to play**";
                    embed.Description = text;
                }
            }
            else if (text.Contains("Ice"))
            {
                {
                    embed.Color = new Color(0, 206, 209);
                    embed.Title = $"**The element you're going to play**";
                    embed.Description = text;
                }
            }
            else if(text.Contains("Mystic"))
            {
                {
                    embed.Color = new Color(186, 85, 211);
                    embed.Title = $"**The element you're going to play**";
                    embed.Description = text;
                }
            }
            await ReplyAsync("", false, embed.Build());
        }
        [Command("commands")]
        [Summary(".commands")]
        [Remarks("Shows the commandlist.")]
        [Alias("cmds","help")]
        public async Task HelpAsync([Remainder, Summary("cmds")] string command = null)
        {
            string prefix = ".";

            if (command == null)
            {
                var rnd = new Random();
                int g1 = rnd.Next(1, 255);
                int g2 = rnd.Next(1, 255);
                int g3 = rnd.Next(1, 255);
                var builder = new EmbedBuilder()
                {
                    Color = new Color(g1, g2, g3),
                    Title = "My commands: <:luxgasm:304937193028321280>"
                };

                foreach (var module in _service.Modules)
                {
                    string description = null;
                    foreach (var cmd in module.Commands)
                    {
                        var result = await cmd.CheckPreconditionsAsync(Context);
                        if (result.IsSuccess)
                            description += $"__{prefix}{cmd.Aliases.First()}__ ``|`` ";
                    }

                    if (!string.IsNullOrWhiteSpace(description))
                    {
                        builder.AddField(x =>
                        {
                            x.Name = $"{module.Name}";
                            x.Value = description;
                            x.IsInline = false;
                        });
                    }
                }

                var dmchannel = await Context.User.GetOrCreateDMChannelAsync();
                await dmchannel.SendMessageAsync("", false, builder.Build());
                await ReplyAsync("", false, builder.Build());
            }
            else
            {
                var result = _service.Search(Context, command);

                var dmChannel = await Context.User.GetOrCreateDMChannelAsync();
                if (!result.IsSuccess)
                {
                    var rnd1 = new Random();
                    int g11 = rnd1.Next(1, 255);
                    int g12 = rnd1.Next(1, 255);
                    int g13 = rnd1.Next(1, 255);
                    var builder1 = new EmbedBuilder()
                    {

                        Title = "Error",
                        Color = new Color(g11, g12, g13),
                        Description = $"the command: **{command}** doesn't exorcist.\n.cmds for all commands."
                    };
                    await ReplyAsync("", false, builder1.Build());
                    return;
                }
                var rnd = new Random();
                int g1 = rnd.Next(1, 255);
                int g2 = rnd.Next(1, 255);
                int g3 = rnd.Next(1, 255);
                var builder = new EmbedBuilder()

                {
                    Color = new Color(g1, g2, g3),
                    Description = $"Help about: **{command}** <:happy:362565108032995329>"
                };

                foreach (var match in result.Commands)
                {
                    var cmd = match.Command;

                    builder.AddField(x =>
                    {
                        x.Name = $"Command: " + string.Join(", ", cmd.Aliases);
                        x.Value = $"**Usage:** {cmd.Summary}\n" +
                                    $"**Info:** {cmd.Remarks}";
                        x.IsInline = false;
                    });
                }
                await ReplyAsync("", false, builder.Build());
            }

        }

        [Command("afk")]
        [Alias("away")]
        [Summary(".afk **<name>** **<reason>**")]
        [Remarks("Sets you afk")]
        public async Task away([Remainder]string message)
        {
            var sb = new StringBuilder();
            try
            {
                var data = new awaydata();
                var away = new Away();
                var attempt = data.getAwayUser(Context.User.Username);
                if (!string.IsNullOrEmpty(attempt.User))
                {
                    away.Status = attempt.Status;
                    away.Message = attempt.Message;
                    away.User = attempt.User;
                }
                else
                {
                    away.User = Context.User.Username;
                }

                if (string.IsNullOrEmpty(message.ToString()))
                {
                    message = "No message set!";
                }
                if (Context.User.Username == away.User)
                {
                    if (away.Status)
                    {
                        sb.AppendLine($"You're already away, **{Context.User.Mention}**!");
                    }
                    else
                    {
                        sb.AppendLine($"Marking you as away, {Context.User.Mention}, with the message: {message.ToString()}");
                        away.ToggleAway();
                        away.SetMessage(message.ToString());
                        away.User = Context.User.Username;
                        var awayData = new awaydata();
                        awayData.setAwayUser(away);
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Away command error {ex.Message}");
                sb.AppendLine($"Sorry {Context.User.Mention}, something went wrong with the away command :(");
            }
            finally
            {
                await Context.Channel.SendMessageAsync(sb.ToString());
            }
        }
        [Command("back")]
        [Alias("set back")]
        [Summary(".back")]
        [Remarks("Sets you back from afk.")]
        public async Task back()
        {
            var sb = new StringBuilder();
            try
            {
                var data = new awaydata();
                var attempt = data.getAwayUser(Context.User.Username);
                var away = new Away();
                if (!string.IsNullOrEmpty(attempt.User))
                {
                    away.Status = attempt.Status;
                    away.Message = attempt.Message;
                    away.User = attempt.User;
                }
                else
                {
                    away.User = Context.User.Username;
                }
                if (Context.User.Username == away.User)
                {
                    if (away.Status)
                    {
                        sb.AppendLine($"you're now set as back{Context.User.Mention}");
                        away.ToggleAway();
                        away.SetMessage(string.Empty);
                        var awaydata = new awaydata();
                        awaydata.setAwayUser(away);
                    }
                    else
                    {
                        sb.AppendLine($"you are not even away yet {Context.User.Mention}");
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"away commannd error {ex.Message}");
                sb.AppendLine($"sorry {Context.User.Mention} something went wrong with back command");
            }
            finally
            {
                await Context.Channel.SendMessageAsync(sb.ToString());
            }

        }
        [Command("userinfo")]
        [Summary(".userinfo **<name>**")]
        [Remarks("Returns info about the current user, or the user parameter, if one passed.")]
        [Alias("user", "whois")]
        public async Task UserInfo([Summary("The (optional) user to get info for")]IGuildUser user = null)
        {
            if (user == null)
            {
                await ReplyAsync("Please gimme a name daddy");
            }
            else
            {
                var application = await Context.Client.GetApplicationInfoAsync();
                var thumbnailurl = user.GetAvatarUrl();
                var date = $"{user.CreatedAt.Month}/{user.CreatedAt.Day}/{user.CreatedAt.Year}";
                var auth = new EmbedAuthorBuilder()
                {
                    Name = user.Username,
                    IconUrl = thumbnailurl,
                };
                var rnd = new Random();
                int g1 = rnd.Next(1, 255);
                int g2 = rnd.Next(1, 255);
                int g3 = rnd.Next(1, 255);
                var embed = new EmbedBuilder
                {
                    Color = new Color(g1, g2, g3),
                    Author = auth
                };
                var us = user as SocketGuildUser;
                var username = us.Username;
                var discr = us.Discriminator;
                var id = us.Id;
                var stat = us.Status;
                var cc = us.JoinedAt;
                var game = us.Game;
                var nick = us.Nickname;
                var muted = us.IsMuted;
                embed.Title = $"**{us.Username}** Informatie";
                embed.Description = 
                  $"Username: **{username}**\n"
                + $"Tag: **{discr}**\n"
                + $"ID: **{id}**\n"
                + $"Nickname: **{nick}**\n"
                + $"Created at: **{date}**\n"
                + $"Status: **{stat}**\n"
                + $"Joined server at: **{cc}**\n"
                + $"Muted: **{muted}**\n"
                + $"Playing: **{game}**\n";
                await ReplyAsync("", false, embed.Build());
            }
        }
        [Command("say")]
        [Summary(".say **<message>**")]
        [Remarks("Bot will echo something back")]
        public async Task Say([Remainder, Summary("The text to echo")] string echo)
        {
            int Delete = 1;
            foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
            {
                await Item.DeleteAsync();
            }
            {
                await ReplyAsync($"**{echo}**", true);
            }
        }
        [Command("Delano")]
        [Alias("Db", "delbronzo")]
        [Summary(".delano")]
        [Remarks("Show a hardstuck bronze")]
        public async Task delbronzo()
        {
            string[] delano = new string[]
            {
                        "<:bronze:358952145640226816>Boosted<:bronze:358952145640226816>",
                        "<:bronze:358952145640226816>Debronzo<:bronze:358952145640226816>",
                        "<:bronze:358952145640226816>Boosted<:bronze:358952145640226816>",
                        "<:bronze:358952145640226816>Debronzo<:bronze:358952145640226816>",
                        "<:bronze:358952145640226816>Boosted<:bronze:358952145640226816>",
                        "<:bronze:358952145640226816>Debronzo<:bronze:358952145640226816>",
                        "<:bronze:358952145640226816><:bronze:358952145640226816><:bronze:358952145640226816>",
                        "<:bronze:358952145640226816>Boosted<:bronze:358952145640226816>",
                        "<:bronze:358952145640226816>Boosted<:bronze:358952145640226816>",
            };

            Random rande = new Random();
            int randomIndex = rande.Next(delano.Length);
            string text = delano[randomIndex];
            await ReplyAsync(Context.User.Mention + ", " + text);

        }
        [Command("Ping")]
        [Alias("ping", "pong")]
        [Summary(".ping")]
        [Remarks("Returns a pong")]
        public async Task Say()
        {
            var rnd = new Random();
            int g1 = rnd.Next(1, 255);
            int g2 = rnd.Next(1, 255);
            int g3 = rnd.Next(1, 255);
            var embed = new EmbedBuilder()
            {
                Color = new Color(g1, g2, g3),
            };
            embed.Description = $"Me like pong";
            await Context.Channel.SendMessageAsync($"{embed.Description}", true, embed.Build());
        }
        private static IUserMessage message;
        private static int i = 0;
        private static string hi = "offline";
        private static string hi2 = "offline";
        private static System.Timers.Timer timer1;

        [Command("time")]
        [Summary(".time")]
        [Remarks("How much time left")]
        public async Task UpdateTimer()
        {
            timer1 = new System.Timers.Timer();
            if (hi == "online")
            {
                timer1.Stop();
                timer1.Enabled = false;
                hi = "offline";
                await message.DeleteAsync();
                await UpdateTimer();
                i = 0;
            }
            else
            {
                if (hi == "offline" && timer1.Enabled == false)
                {
                    await GoingTimer();
                    i = 1;
                    timer1.Start();
                    timer1.Enabled = true;
                    timer1.Interval = 5000;
                    timer1.Elapsed += StartBoi;
                    timer1.AutoReset = true;                   
                    hi = "online";
                }
                else
                {
                    timer1.Elapsed += StartBoi;
                    timer1.AutoReset = true;
                }
            }
        }

        [Command("twitch")]
        [Summary(".twitch **<username>**")]
        [Remarks("Searchs stuff")]
        public async Task Twitch(string name)
        {
            try
            {
                HttpWebRequest httpWebRequest = WebRequest.Create($"https://api.twitch.tv/helix/users?login={name}")as HttpWebRequest;
                httpWebRequest.Method = "GET";
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add("Client-ID",
                    "dwcn7987e3v8rftt8dbhg0ecneaxkr");
                HttpWebResponse response = (HttpWebResponse) httpWebRequest.GetResponse();

                Encoding ascii = Encoding.ASCII;
                Stream responseStream = response.GetResponseStream();
                if (responseStream == null)
                    throw new InvalidOperationException();
                Encoding encoding = ascii;
                using (StreamReader streamReader = new StreamReader(responseStream, encoding))
                {
                    string end = streamReader.ReadToEnd();
                    var container = (JObject) JsonConvert.DeserializeObject(end);
                    var des = container["data"][0]["description"].ToString();
                    var view = container["data"][0]["view_count"].ToString();
                    var dn = container["data"][0]["display_name"].ToString();
                    var link = container["data"][0]["login"].ToString();
                    var url = container["data"][0]["profile_image_url"].ToString();
                    var Rem = new EmbedBuilder
                    {
                        Title = $"{dn}'s twitch profile",
                        Description =
                            $"**Name:** {dn}\n**Description:** {des}\n**Views:** {view}\n**[Go to profile](https://www.twitch.tv/{link})**",
                        ThumbnailUrl = url
                    };
                    await ReplyAsync("", false, Rem.Build());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        [Command("twitch play")]
        [Summary(".twitch play **<username>**")]
        [Remarks("Searchs stuff")]
        public async Task Twitchw(string name)
        {
            await ReplyAsync($"https://www.twitch.tv/{name}");
        }

        [Command("twitch video")]
        [Summary(".twitch **<username>**")]
        [Remarks("Searchs stuff")]
        public async Task Twitchv(string name)
        {
            try
            {
                HttpWebRequest httpWebRequest =
                    WebRequest.Create($"https://api.twitch.tv/kraken/channels/{name}/videos?client_id=g8vz0dctlo417o6dz3gvol1ssws1ih") as HttpWebRequest;
                httpWebRequest.Method = "GET";
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add("Client-ID",
                    "dwcn7987e3v8rftt8dbhg0ecneaxkr");
                HttpWebResponse response = (HttpWebResponse) httpWebRequest.GetResponse();

                Encoding ascii = Encoding.ASCII;
                Stream responseStream = response.GetResponseStream();
                if (responseStream == null)
                    throw new InvalidOperationException();
                Encoding encoding = ascii;
                using (StreamReader streamReader = new StreamReader(responseStream, encoding))
                {
                    string end = streamReader.ReadToEnd();
                    var container = JObject.Parse(end);
                    var xd = new Random();
                    var count = container["videos"].Count();
                    var next = xd.Next(1, count);
                    var des = container["videos"][next]["url"];
                    await ReplyAsync(des.ToString());
                }
            }
            catch (Exception e)
            {
                var auth = new EmbedAuthorBuilder()
                {
                    Name = $"Error",
                };
                var rnd = new Random();
                int g1 = rnd.Next(1, 255);
                int g2 = rnd.Next(1, 255);
                int g3 = rnd.Next(1, 255);
                var builder = new EmbedBuilder
                {
                    Color = new Discord.Color(g1, g2, g3),
                    Author = auth,
                    Description = $"No video's found.",
                    ThumbnailUrl =
                        $"https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/pic/silly.png",
                };
                await ReplyAsync("", false, builder.Build());
                Console.WriteLine(e.ToString());
            }
        }

        [Command("timestop")]
        [Summary(".timestop")]
        [Remarks("Stops the timer")]
        public async Task StopTimer()
        {
            timer1.Stop();
            timer1.Enabled = false;
        }

        public async void StartBoi(Object source, System.Timers.ElapsedEventArgs e)
        {
            await GoingTimer();
        }

        public async Task GoingTimer()
        {
            try
            {
                string currentTime = DateTime.Now.ToString("H:mm:ss");
                TimeSpan duration = DateTime.Parse("15:50:00").Subtract(DateTime.Parse(currentTime));
                string final = duration.ToString();
                int y = int.Parse(duration.Hours.ToString());
                int x = int.Parse(duration.Minutes.ToString());
                int z = int.Parse(duration.Seconds.ToString());
                if (!final.Contains("-"))
                {
                    if (i == 0)
                    {
                        message =
                            await ReplyAsync($"**{final}**" + " till your work day ends. <:mad:362497418291314688>");
                        i = 1;
                    }
                    else if (i > 0)
                    {
                        await message.ModifyAsync(msg =>
                            msg.Content =$"**{final}**" + " till your work day ends. <:mad:362497418291314688>");
                    }
                }
                else
                {
                    await DoneTimer();
                    timer1.Stop();
                    timer1.Enabled = false;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());

            }
        }

        public async Task DoneTimer()
        {
            await Context.Channel.SendMessageAsync(
                $"**You're free, for now.** <:happy:362565108032995329>");
            timer1.Stop();
        }

        [Command("hoursleft")]
        [Alias("hl")]
        [Summary(".hoursleft **<hours>**")]
        [Remarks("Shows how much time left to waste time")]
        public async Task left(string input)
        {
            if (input != null)
            {
                var rnd = new Random();
                int getal = rnd.Next(1, 6);
                int g1 = rnd.Next(1, 255);
                int g2 = rnd.Next(1, 255);
                int g3 = rnd.Next(1, 255);
                var embed = new EmbedBuilder()
                {
                    Color = new Color(g1, g2, g3),
                };
                var boi = Convert.ToInt32(input);
                int hours = 720 - boi;
                int days = boi / 8;
                int weeks = days / 5;
                embed.Title = $"**Zoveel dagen nog te gaan** <:mad:362497418291314688>";
                embed.Description = $"**{hours}** hours worked, \n**{days}** days,\n**{weeks}** weeks.";
                await ReplyAsync("", false, embed.Build());

            }
            else
            {
                var embedd = new EmbedBuilder()
                {
                    Title = $"Error",
                    Description = $".hoursleft **<hours>**"
                };
                await ReplyAsync("", false, embedd.Build());
            }
        }
        [Command("pat")]
        [Summary(".pat **<user>**")]
        [Remarks("pats an other user")]
        [Alias("touch")]
        public async Task PokeSomebody([Summary("user to poke")] IGuildUser user = null)
        {
            if (user == null)
            {
                await ReplyAsync("Please pat me with your command <:luxgasm:304937193028321280> , \nExample: .poke <USER>");
            }
            else
            {
                var rnd = new Random();
                int g = rnd.Next(1, 4);
                EmbedBuilder embed = new EmbedBuilder
                {
                    Description = Context.User.Mention + " pats " + user.Mention,
                    ImageUrl = $"https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/pic/xd{g}.gif"
                };
                await ReplyAsync("", false, embed.Build());
            }
        }

        [Command("spam")]
        public async Task spam([Remainder]string tag)
        {
            for (;;)
            {
                await ReplyAsync(tag);
            }
        }

        [Command("Ask")]
        [Summary(".ask **<question>**")]
        [Remarks("Will answer questions")]
        [Alias("askme", "8ball")]
        public async Task EightBall([Remainder] string input)
        {

            string[] predictionsTexts = new string[]
            {
                "I don't think so.",
                "Idk.",
                "Yes.",
                "Idk.",
                "How bout go fuck yourself.",
                "And how am i supposed to know that."
            };

            Random rand = new Random();

            var rnd = new Random();
            int g1 = rnd.Next(1, 255);
            int g2 = rnd.Next(1, 255);
            int g3 = rnd.Next(1, 255);
            var embed = new EmbedBuilder()
            {
                Color = new Color(g1, g2, g3),
            };
            int randomIndex = rand.Next(predictionsTexts.Length);
            string text = predictionsTexts[randomIndex];
            embed.Title = $"**I say...**";
            embed.Description = text;
            await ReplyAsync("", false, embed.Build());
        }

        [Command("dm")]
        public async Task sendmsgtoowner(IGuildUser boi, [Remainder] string text)
        {
            if (text != null)
            {
                var embed = new EmbedBuilder()
                {
                    Color = new Color(231, 31, 31)
                };
                var application = await Context.Client.GetApplicationInfoAsync();
                var user = boi.GetOrCreateDMChannelAsync();
                var z = await boi.GetOrCreateDMChannelAsync();
                embed.Description =
                    $"`{Context.User.Username}` **from** `{Context.Guild.Name}` **send you a message!**\n\n{text}";
                await z.SendMessageAsync("", false, embed);
            }
            else
            {
                await ReplyAsync("xd");
            }
        }

        [Command("rate")]
        [Summary(".rate")]
        [Remarks("Gives a rate between 1/10.")]
        [Alias("r8")]
        public async Task rate([Remainder] string input = null)
        {
            if (input != null)
            {
                string[] predictionssTexts = new string[]
                {
                    $"I rate __{input}__ a **0/10** lol.",
                    $"I rate __{input}__ a **1/10**",
                    $"I rate __{input}__ a **2/10**",
                    $"I rate __{input}__ a **3/10**",
                    $"I rate __{input}__ a **4/10**",
                    $"I rate __{input}__ a **5/10**",
                    $"I rate __{input}__ a **6/10**",
                    $"I rate __{input}__ a **7/10**",
                    $"I rate __{input}__ a **8/10**",
                    $"I rate __{input}__ a **9/10**",
                    $"I rate __{input}__ a **10/10**"
                };


                    Random rand = new Random();

                    var rnd = new Random();
                    int getal = rnd.Next(1, 6);
                    int g1 = rnd.Next(1, 255);
                    int g2 = rnd.Next(1, 255);
                    int g3 = rnd.Next(1, 255);
                    var embed = new EmbedBuilder()
                    {
                    Color = new Color(g1, g2, g3),
                    };
                    int randomIndex = rand.Next(predictionssTexts.Length);
                    string text = predictionssTexts[randomIndex];
                    embed.Title = $"**I say...**";
                    embed.Description = text;
                await ReplyAsync("", false, embed.Build());
            }
            else
            {
            string[] predictionssTexts = new string[]
            {
                $"I rate this a **0/10** lol.",
                $"I rate this a **1/10**",
                $"I rate this a **2/10**",
                $"I rate this a **3/10**",
                $"I rate this a **4/10**",
                $"I rate this a solid **5/7**",
                $"I rate this a **6/10**",
                $"I rate this a **7/10**",
                $"I rate this a **8/10**",
                $"I rate this a **9/10**",
                $"I rate this a **10/10** yeys"
            };

                    Random rand = new Random();

                    var rnd = new Random();
                    int getal = rnd.Next(1, 6);
                    int g1 = rnd.Next(1, 255);
                    int g2 = rnd.Next(1, 255);
                    int g3 = rnd.Next(1, 255);
                    var embed = new EmbedBuilder()
                    {
                    Color = new Color(g1, g2, g3),
                    };
                    int randomIndex = rand.Next(predictionssTexts.Length);
                    string text = predictionssTexts[randomIndex];
                    embed.Title = $"**I say...**";
                    embed.Description = text;
                    await ReplyAsync("", false, embed.Build());
                }
            }
        [Command("roll")]
        [Summary(".roll **<number>**")]
        [Remarks("Rick rolling")]
        public async Task Roll([Optional]string kek)
        {
            if (kek == null)
            {
                kek = "100";
            };
            var rnd = new Random();
            int getal = rnd.Next(1, Convert.ToInt32(kek));
            await ReplyAsync(getal.ToString());
        }
        [Command("choose")]
        [Summary(".choose **<option1>** or **<option2>**")]
        [Remarks("Chooses between option1 and option2")]
        [Alias("ch")]
        public async Task nobrain(string o1 = null, string or = null, string o2 = null)
        {
            if (or.Contains("or"))
            {
                var rd = new Random();
                int r = rd.Next(1, 3);
                if(r == 1)
                {
                    var rnd = new Random();
                    int getal = rnd.Next(1, 6);
                    int g1 = rnd.Next(1, 255);
                    int g2 = rnd.Next(1, 255);
                    int g3 = rnd.Next(1, 255);
                    var embed = new EmbedBuilder()
                    {
                        Color = new Color(g1, g2, g3),
                    };
                    embed.Title = ("I chose...");
                    embed.Description = $"**{o1}**";
                    await ReplyAsync("", false, embed.Build());
                }
                else if (r == 2)
                {
                    var rnd = new Random();
                    int getal = rnd.Next(1, 6);
                    int g1 = rnd.Next(1, 255);
                    int g2 = rnd.Next(1, 255);
                    int g3 = rnd.Next(1, 255);
                    var embed = new EmbedBuilder()
                    {
                        Color = new Color(g1, g2, g3),
                    };
                    embed.Title = ("I chose...");
                    embed.Description = $"**{o2}**";
                    await ReplyAsync("", false, embed.Build());
                }
                else
                {
                    var rnd = new Random();
                    int getal = rnd.Next(1, 6);
                    int g1 = rnd.Next(1, 255);
                    int g2 = rnd.Next(1, 255);
                    int g3 = rnd.Next(1, 255);
                    var embed = new EmbedBuilder()
                    {
                        Color = new Color(g1, g2, g3),
                    };
                    embed.Title = ("I chose...");
                    embed.Description = $"**Nothing :D**";
                    await ReplyAsync("", false, embed.Build());
                }
            }
            else
            {
                await ReplyAsync("Please don't forget to use **or** between the options.");
            }
        }
    }
}
