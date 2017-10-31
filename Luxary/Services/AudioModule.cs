using System;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Audio;
using Discord.WebSocket;
using Discord.Commands;
using Newtonsoft.Json;
using System.Net;
using Luxary.Services;
using System.Web.Script.Serialization;
using System.Web.Helpers;

namespace Luxary
{
    [Group("Music")]
    [Alias("M")]
    public class Audio : ModuleBase<ICommandContext>
    { 
        // Scroll down further for the AudioService.
        private Process CreateStream(string url)
        {
            Process currentsong = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/C youtube-dl.exe -o - {url} | ffmpeg -i pipe:0 -ac 2 -f s16le -ar 48000 pipe:1",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            currentsong.Start();
            return currentsong;
        }
        public class Part : IEquatable<Part>
        {
            public string PartName { get; set; }

            public int PartId { get; set; }

            public string PartTitle { get; set; }

            public override string ToString()
            {
                return PartName;
            }
            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                Part objAsPart = obj as Part;
                if (objAsPart == null) return false;
                else return Equals(objAsPart);
            }
            public override int GetHashCode()
            {
                return PartId;
            }
            public bool Equals(Part other)
            {
                if (other == null) return false;
                return (this.PartId.Equals(other.PartId));
            }
        }

        private static List<Part> parts = new List<Part>();
        static int id = 1;
        static int count = 0;
        static int idd = 0;
        static string started = "false";
        static string done = "false";

        [Command("play", RunMode = RunMode.Async)]
        [Summary(".play **<yt url>**")]
        [Alias("P")]
        [Remarks("Plays youtube music in the music channel you're currently in.")]
        public async Task Play()
        {
            idd = 0;
            started = "true";
            if (started == "true")
            {                
                Part[] myArray = parts.ToArray();
                foreach (Part songs in parts)
                {
                    string xd = myArray[idd].ToString();
                    var user = Context.User;
                    var thumbnailurl = user.GetAvatarUrl();
                    var auth = new EmbedAuthorBuilder()
                    {
                        Name = "YT",
                        IconUrl = thumbnailurl,
                    };
                    var rnd = new Random();
                    int g1 = rnd.Next(1, 255);
                    int g2 = rnd.Next(1, 255);
                    int g3 = rnd.Next(1, 255);
                    var embed = new EmbedBuilder()
                    {
                        Color = new Discord.Color(g1, g2, g3),
                        Author = auth
                    };
                    var us = user as SocketGuildUser;
                    embed.Description =
                        $"<:yt:367195265410662402> Music started playing.";
                    await ReplyAsync("", false, embed.Build());
                    IVoiceChannel channel = (Context.User as IVoiceState).VoiceChannel;
                    IAudioClient client = await channel.ConnectAsync();
                    var output = CreateStream(myArray[idd].ToString()).StandardOutput.BaseStream;
                    var stream = client.CreatePCMStream(AudioApplication.Music, 128 * 1024);
                    await output.CopyToAsync(stream);
                    await stream.FlushAsync().ConfigureAwait(false);
                    idd = idd + 1;
                }
                done = "true";
                started = "false";
                if (done == "true")
                {
                    var user = Context.User;
                    var thumbnailurl = user.GetAvatarUrl();

                    var auth = new EmbedAuthorBuilder()
                    {
                        Name = "YT",
                        IconUrl = thumbnailurl,
                    };
                    var rnd = new Random();
                    int g1 = rnd.Next(1, 255);
                    int g2 = rnd.Next(1, 255);
                    int g3 = rnd.Next(1, 255);
                    var embed = new EmbedBuilder()
                    {
                        Color = new Discord.Color(g1, g2, g3),
                        Author = auth
                    };
                    var us = user as SocketGuildUser;
                    embed.Description =
                        $"<:yt:367195265410662402>```css\nDone with playlist.\n```";
                    await ReplyAsync("", false, embed.Build());
                    IVoiceChannel channel = (Context.User as IVoiceState).VoiceChannel;
                    IAudioClient client = await channel.ConnectAsync();
                    done = "true";
                    started = "false";
                    idd = 0;

                }
                else
                {
                    var user = Context.User;
                    var thumbnailurl = user.GetAvatarUrl();

                    var auth = new EmbedAuthorBuilder()
                    {
                        Name = "YT",
                        IconUrl = thumbnailurl,
                    };
                    var rnd = new Random();
                    int g1 = rnd.Next(1, 255);
                    int g2 = rnd.Next(1, 255);
                    int g3 = rnd.Next(1, 255);
                    var embed = new EmbedBuilder()
                    {
                        Color = new Discord.Color(g1, g2, g3),
                        Author = auth
                    };
                    var us = user as SocketGuildUser;
                    embed.Description =
                        $"<:yt:367195265410662402>```css\nDone with playlist.\n```";
                    await ReplyAsync("", false, embed.Build());
                    IVoiceChannel channel = (Context.User as IVoiceState).VoiceChannel;
                    IAudioClient client = await channel.ConnectAsync();
                    done = "true";
                    started = "false";
                    idd = 0;
                    
                }
            }
            else
            {
                await ReplyAsync("not started");
            }
        }

        [Command("add")]
        [Alias("A")]
        public async Task Add(string url)
        {
            if (url.Contains("https://youtu.be"))
            {
                Video video = new Video();
                var user = Context.User;
                var thumbnailurl = user.GetAvatarUrl();
                string ytKey = "AIzaSyA34CTAyV_VFpo2mZhZdsmRsf81YXG9CbE";
                int t = 0;
                string code = "";
                foreach (char x in url)
                {
                    if (x == '/')
                    {
                        t = t + 1;
                    }
                    else if (t == 3) { code += x.ToString(); }
                }
                WebClient myDownloader = new WebClient {Encoding = System.Text.Encoding.UTF8};

                string jsonResponse = myDownloader.DownloadString("https://www.googleapis.com/youtube/v3/videos?id=" + code + "&key=" + ytKey + "&part=snippet");
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    var dynamicObject = Json.Decode(jsonResponse);
                    var item = dynamicObject.items[0].snippet;

                    video.Title = item.title;
                    video.ChannelTitle = item.channelTitle;
                    video.ThumbUrl = item.thumbnails.@default.url;
                    video.BigThumbUrl = item.thumbnails.high.url;
                    video.Description = item.description;
                    video.UploadDate = Convert.ToDateTime(item.publishedAt);

                    jsonResponse = myDownloader.DownloadString("https://www.googleapis.com/youtube/v3/videos?id=" + code + "&key=" + ytKey + "&part=contentDetails");
                    dynamicObject = Json.Decode(jsonResponse);
                    string tmp = dynamicObject.items[0].contentDetails.duration;
                    video.Duration = Convert.ToInt32(System.Xml.XmlConvert.ToTimeSpan(tmp).TotalSeconds);
                    var timespan = TimeSpan.FromSeconds(video.Duration);
              
                    var time = (timespan.ToString(@"hh\:mm\:ss"));

                video.Url = "http://www.youtube.com/watch?v=" + code;

                var auth = new EmbedAuthorBuilder()
                {
                    Name = "Added to playlist",
                    IconUrl = thumbnailurl,
                };
                var rnd = new Random();
                int g1 = rnd.Next(1, 255);
                int g2 = rnd.Next(1, 255);
                int g3 = rnd.Next(1, 255);
                var embed = new EmbedBuilder()
                {
                    Color = new Discord.Color(g1, g2, g3),
                    Author = auth,
                    Description = $"**{video.Title}**",
                    ThumbnailUrl = $"{video.ThumbUrl}",
                };
                embed.AddField(x =>
                {
                    x.Name = "Channel:";
                    x.Value = ($"**{video.ChannelTitle}**");
                    x.IsInline = true;
                });
                if (time.Contains("00:00:00"))
                {
                    embed.AddField(x =>
                    {
                        x.Name = "Duration:";
                        x.Value = ($":red_circle: **LIVE**");
                        x.IsInline = true;
                    });
                }
                else
                {
                    embed.AddField(x =>
                    {
                        x.Name = "Duration:";
                        x.Value = ($"**{time}**");
                        x.IsInline = true;
                    });
                }
                embed.AddField(x =>
                {
                    x.Name = "Position in playlist:";
                    x.Value = ($"**{id}**");
                    x.IsInline = true;
                });
                embed.AddField(x =>
                {
                    x.Name = "Date released:";
                    x.Value = ($"**{video.UploadDate}**");
                    x.IsInline = true;
                });
                await ReplyAsync("", false, embed.Build());
                id = id + 1;
                count = count + video.Duration;
                parts.Add(new Part()
                {
                    PartName = url,
                    PartId = id,
                    PartTitle = video.Title
                });
            }
            else
            {
                var rnd = new Random();
                int g1 = rnd.Next(1, 255);
                int g2 = rnd.Next(1, 255);
                int g3 = rnd.Next(1, 255);
                var embed = new EmbedBuilder()
                {
                    Color = new Discord.Color(g1, g2, g3),
                    Title = "Error",
                    Description = $"```Invalid URL.```"
                };
                await ReplyAsync("", false, embed.Build());
            }
        }

        [Command("clear")]
        [Alias("c","cl")]
        public async Task Clear()
        {
            parts.Clear();
            id = 1;
            count = 0;
            var user = Context.User;

            var thumbnailurl = user.GetAvatarUrl();

            var auth = new EmbedAuthorBuilder()
            {
                Name = "YT",
                IconUrl = thumbnailurl,
            };
            var rnd = new Random();
            int g1 = rnd.Next(1, 255);
            int g2 = rnd.Next(1, 255);
            int g3 = rnd.Next(1, 255);
            var krakak = new EmbedBuilder
            {
                Color = new Discord.Color(g1, g2, g3),
                Author = auth,
                Title = "Clearing...",
                Description = Context.User.Username + $" cleared the playlist."
            };
            await ReplyAsync("", false, krakak.Build());
            idd = 0;
        }
        [Command("playlist")]
        [Alias("pl")]
        public async Task List()
        {
            bool isEmpty = !parts.Any();
            if (!(isEmpty))
            {
                var krakak = new EmbedBuilder();
                foreach (var aPart in parts)
                {
                    var timespan = TimeSpan.FromSeconds(count);
                    var time = (timespan.ToString(@"hh\:mm\:ss"));
                    int fin = aPart.PartId - 1;
                    krakak.Title = $"Playlist length: **{time}**";
                    krakak.Description += $"```css\n{fin} - {aPart.PartTitle}\n```";
                }                
                await ReplyAsync("", false, krakak.Build());
            }
            else
            {
                var krakak = new EmbedBuilder
                {
                    Title = "Playlist",
                    Description = "Playlist is empty."
                };

                await ReplyAsync("", false, krakak.Build());
            }
        }
        [Command("earrape", RunMode = RunMode.Async)]
        [Summary(".earrape")]
        [Alias("er")]
        [Remarks("Plays youtube music in the music channel you're currently in.")]
        public async Task Rep()
        {
            var user = Context.User;

                var thumbnailurl = user.GetAvatarUrl();

                var auth = new EmbedAuthorBuilder()
                {
                    Name = "YT",
                    IconUrl = thumbnailurl,
                };
                var rnd = new Random();
                int g1 = rnd.Next(1, 255);
                int g2 = rnd.Next(1, 255);
                int g3 = rnd.Next(1, 255);
                var embed = new EmbedBuilder()
                {
                    Color = new Discord.Color(g1, g2, g3),
                    Author = auth
                };

                string[] elements = new string[]
                {
                    "https://www.youtube.com/watch?v=MOvDrom0rjc",
                    "https://www.youtube.com/watch?v=2lu8z8E3kpY",
                    "https://www.youtube.com/watch?v=6Joyj0dmkug",
                    "https://www.youtube.com/watch?v=BThyEkAWxbc",
                    "https://youtu.be/qclDfyIdMDk",
                    "https://www.youtube.com/watch?v=8U9WwTQ0L6A",
                    "https://www.youtube.com/watch?v=TX-6qPppbjY",
                    "https://www.youtube.com/watch?v=GW--4j1vkEw",
                    "https://www.youtube.com/watch?v=-w9PukG97oQ",
                    "https://www.youtube.com/watch?v=Pja-pCBVqUY",
                    "https://www.youtube.com/watch?v=9a-zHRBTVlY",
                    "https://www.youtube.com/watch?v=stlZEKoJg10",
                    "https://youtu.be/HGHUPVJtr2s",
                    "https://www.youtube.com/watch?v=aUmrW9hzRms",
                    "https://www.youtube.com/watch?v=Q27UzUZuMMk"
                };
                var rnde = new Random();
                int randomIndex = rnde.Next(elements.Length);
                string text = elements[randomIndex];
                embed.Description = $"<:yt:367195265410662402>  EARRAPE BOI";
                await ReplyAsync("", false, embed.Build());
                IVoiceChannel channel = (Context.User as IVoiceState).VoiceChannel;
                IAudioClient client = await channel.ConnectAsync();
                var output = CreateStream(text).StandardOutput.BaseStream;
                var stream = client.CreatePCMStream(AudioApplication.Music, 128 * 1024);
                await output.CopyToAsync(stream);

                await stream.FlushAsync().ConfigureAwait(false);
            }
        [Command("leave", RunMode = RunMode.Async)]
        [Summary(".leave")]
        [Alias("l")]
        [Remarks("Stops the music.")]
        public async Task Stop()
        {
            started = "false";
            IVoiceChannel channel = (Context.User as IVoiceState).VoiceChannel;
            IAudioClient client = await channel.ConnectAsync();
            await client.StopAsync();
        }

        [Command("skip", RunMode = RunMode.Async)]
        [Alias("s")]
        public async Task Test()
        {
            if (started == "true")
            {

                var user1 = Context.User;
                var thumbnailurl1 = user1.GetAvatarUrl();
                var auth1 = new EmbedAuthorBuilder()
                {
                    Name = "YT",
                    IconUrl = thumbnailurl1,
                };
                var rnd1 = new Random();
                int g11 = rnd1.Next(1, 255);
                int g12 = rnd1.Next(1, 255);
                int g13 = rnd1.Next(1, 255);
                var embed1 = new EmbedBuilder()
                {
                    Color = new Discord.Color(g11, g12, g13),
                    Author = auth1,
                    Description = "<:yt:367195265410662402> Skipping to next song..."
                };
                await ReplyAsync("", false, embed1.Build());

                Part[] myArray = parts.ToArray();
                string xd = myArray[idd].ToString();
                idd = idd + 1;
                if (idd != myArray.Length)
                {
                    foreach (Part songs in parts)
                    {
                       
                        if (idd < myArray.Length)
                        {
                            IVoiceChannel channel = (Context.User as IVoiceState).VoiceChannel;
                            IAudioClient client = await channel.ConnectAsync();
                            var output = CreateStream(myArray[idd].ToString()).StandardOutput.BaseStream;
                            var stream = client.CreatePCMStream(AudioApplication.Music, 128 * 1024);
                            await output.CopyToAsync(stream);
                            await stream.FlushAsync().ConfigureAwait(false);
                            idd = idd + 1;
                            xd = xd + 1;
                        }
                        else if (idd == myArray.Length)
                        {
                            idd = 0;
                        }
                        else
                        {
                            await ReplyAsync("ERROR");
                        }
                    }
                    done = "true";
                    started = "false";
                    if (done == "true")
                    {
                        var user = Context.User;
                        var thumbnailurl = user.GetAvatarUrl();

                        var auth = new EmbedAuthorBuilder()
                        {
                            Name = "YT",
                            IconUrl = thumbnailurl,
                        };
                        var rnd = new Random();
                        int g1 = rnd.Next(1, 255);
                        int g2 = rnd.Next(1, 255);
                        int g3 = rnd.Next(1, 255);
                        var embed = new EmbedBuilder()
                        {
                            Color = new Discord.Color(g1, g2, g3),
                            Author = auth
                        };
                        var us = user as SocketGuildUser;
                        embed.Description =
                            $"<:yt:367195265410662402>```css\nDone with playlist.\n```";
                        await ReplyAsync("", false, embed.Build());
                        IVoiceChannel channel = (Context.User as IVoiceState).VoiceChannel;
                        IAudioClient client = await channel.ConnectAsync();
                        idd = 0;
                        done = "true";
                        started = "false";
                    }
                    else
                    {
                        await ReplyAsync("done is false skip");
                        IVoiceChannel channel = (Context.User as IVoiceState).VoiceChannel;
                        IAudioClient client = await channel.ConnectAsync();
                    }
                }
                else
                {
                    var user = Context.User;
                    var thumbnailurl = user.GetAvatarUrl();

                    var auth = new EmbedAuthorBuilder()
                    {
                        Name = "YT",
                        IconUrl = thumbnailurl,
                    };
                    var rnd = new Random();
                    int g1 = rnd.Next(1, 255);
                    int g2 = rnd.Next(1, 255);
                    int g3 = rnd.Next(1, 255);
                    var embed = new EmbedBuilder()
                    {
                        Color = new Discord.Color(g1, g2, g3),
                        Author = auth
                    };
                    var us = user as SocketGuildUser;
                    embed.Description =
                        $"<:yt:367195265410662402>```css\nDone with playlist.\n```";
                    await ReplyAsync("", false, embed.Build());
                    IVoiceChannel channel = (Context.User as IVoiceState).VoiceChannel;
                    await channel.ConnectAsync();
                    done = "true";
                    started = "false";
                    idd = 0;
                }
            }
            else
            {
                var user = Context.User;
                var thumbnailurl = user.GetAvatarUrl();

                var auth = new EmbedAuthorBuilder()
                {
                    Name = "YT",
                    IconUrl = thumbnailurl,
                };
                var rnd = new Random();
                int g1 = rnd.Next(1, 255);
                int g2 = rnd.Next(1, 255);
                int g3 = rnd.Next(1, 255);
                var embed = new EmbedBuilder()
                {
                    Color = new Discord.Color(g1, g2, g3),
                    Author = auth,
                    Description = $"<:yt:367195265410662402>```css\nNo playlist started.\n```"
                };
                var us = user as SocketGuildUser;
                await ReplyAsync("", false, embed.Build());
                idd = 0;
                IVoiceChannel channel = (Context.User as IVoiceState).VoiceChannel;
                IAudioClient client = await channel.ConnectAsync();
            }
            idd = 0;
            done = "true";
            started = "false";
        }
    }
}
