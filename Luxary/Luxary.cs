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
using Luxary.Service;
using System.Web;


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
                    Title = "Dit zijn de commands: <:luxgasm:304937193028321280>"
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
                    await ReplyAsync($"Sorry, I couldn't find a command like **{command}**.");
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
                        x.Value = $"Usage: {cmd.Summary}\n" +
                                    $"Info: {cmd.Remarks}";
                        x.IsInline = false;
                    });
                }
                await ReplyAsync("", false, builder.Build());
            }

        }

        [Command("ultraspam")]
        [Summary(".ultraspam**")]
        [Remarks("It's the time we show our power from our lord and saviour Teemo and use it against our haters to achieve our goals to")]
        public async Task SendSpam(string spammm)
        {
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
            await Context.Channel.SendFileAsync(
                $"D:/Discord/Luxary/Luxary/bin/Debug/pic/{spammm}.gif");
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
                int getal = rnd.Next(1, 6);
                int g1 = rnd.Next(1, 255);
                int g2 = rnd.Next(1, 255);
                int g3 = rnd.Next(1, 255);
                var embed = new EmbedBuilder()
                {
                    Color = new Color(g1, g2, g3),
                    Author = auth
                };

                var us = user as SocketGuildUser;

                var username = us.Username;
                var discr = us.Discriminator;
                var id = us.Id;
                var dat = date;
                var stat = us.Status;
                var cc = us.JoinedAt;
                var game = us.Game;
                var nick = us.Nickname;
                var Roles = us.Roles;
                embed.Title = $"**{us.Username}** Informatie";
                embed.Description = $"Username: **{username}\n"
                + $"Tag: **{discr}**\n"
                + $"ID: **{id}**\n"
                + $"Nickname: **{nick}**\n"
                + $"Created at: **{date}**\n"
                + $"Status: **{stat}**\n"
                + $"Joined server at: **{cc}**\n"
                + $"Roles: **{Roles}**\n"
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
                await ReplyAsync($"**{echo}**");
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
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
        [Command("time")]
        [Summary(".time")]
        [Remarks("How much time left")]
        public async Task time()
        {
            try
            {
                string currentTime = DateTime.Now.ToString("HH:mm");
                TimeSpan duration = DateTime.Parse("15:50").Subtract(DateTime.Parse(currentTime));
                int x = int.Parse(duration.Minutes.ToString());
                if (x > 0)
                {
                    await Context.Channel.SendMessageAsync($"**{duration}**" + " till your work day ends. <:mad:362497418291314688>");
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"You're free, for now. <:happy:362565108032995329>");
                }
            }
            catch (Exception)
            {
                await Context.Channel.SendMessageAsync($"Wrong usage, .time");
            }
        }
        [Command("hoursleft")]
        [Alias("hl")]
        [Summary(".hoursleft **<hours>**")]
        [Remarks("Shows how much time left to waste time")]
        public async Task left(int input)
        {
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
                int hours = 720 - input;
                int days = input / 8;
                int weeks = days / 5;
                embed.Title = $"**Zoveel dagen nog te gaan** <:mad:362497418291314688>";
                embed.Description = $"**{hours}** hours worked, \n**{days}** days,\n**{weeks}** weeks.";
                await ReplyAsync("", false, embed.Build());

            }
        }
        [Command("poke")]
        [Summary(".poke **<user>**")]
        [Remarks("touches an other user")]
        [Alias("touch", "prik")]
        public async Task PokeSomebody([Summary("user to poke")] IGuildUser user = null)
        {
            if (user == null)
            {
                await ReplyAsync("Please touch me with your command <:luxgasm:304937193028321280> , \nExample: .poke <USER>");
            }
            else
            {
                await ReplyAsync(Context.User.Mention + " Touches " + user.Mention);
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
                "And how am i supposed to know that.",
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
            int randomIndex = rand.Next(predictionsTexts.Length);
            string text = predictionsTexts[randomIndex];
            embed.Title = $"**I say...**";
            embed.Description = text;
            await ReplyAsync("", false, embed.Build());
        }  
        [Command("rate")]
        [Summary(".rate")]
        [Remarks("Gives a rate between 1/10.")]
        [Alias("r8")]
        public async Task rate([Remainder] string input = null)
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
