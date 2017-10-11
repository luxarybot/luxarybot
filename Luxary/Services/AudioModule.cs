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
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using ImageSharp.Drawing;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.IO.Compression;
using Luxary.Service;
using Discord.Rest;
using Google.GData.YouTube;
using Google.YouTube;

namespace Luxary
{
    public class Audio : ModuleBase<ICommandContext>
    { 
        // Scroll down further for the AudioService.
        private Process CreateStream(string url)
        {
            Process currentsong = new Process();

            currentsong.StartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/C youtube-dl.exe -o - {url} | ffmpeg -i pipe:0 -ac 2 -f s16le -ar 48000 pipe:1",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            currentsong.Start();
            return currentsong;
        }
        [Command("play", RunMode = RunMode.Async)]
        [Summary(".play **<yt url>**")]
        [Remarks("Plays youtube music in the music channel you're currently in.")]
        public async Task Pay(string url)
        {
           
                if (url.Contains("verified=1"))
                {
                    await ReplyAsync("invalid url.");
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
                    embed.Description = $"<:yt:367195265410662402>  Music started playing. ```\n{url}\n```";
                    await ReplyAsync("", false, embed.Build());
                    IVoiceChannel channel = (Context.User as IVoiceState).VoiceChannel;
                    IAudioClient client = await channel.ConnectAsync();
                    var output = CreateStream(url).StandardOutput.BaseStream;
                    var stream = client.CreatePCMStream(AudioApplication.Music, 128 * 1024);
                    await output.CopyToAsync(stream);

                    await stream.FlushAsync().ConfigureAwait(false);
                    var xd = new EmbedBuilder
                    {
                        Color = new Discord.Color(g1, g2, g3),
                        Author = auth,
                        Title = "<:yt:367195265410662402> Music stopped playing."
                    };

                    await ReplyAsync("", false, xd.Build());
                }
            }
        [Command("leave", RunMode = RunMode.Async)]
        [Summary(".leave")]
        [Remarks("Stops the music.")]
        public async Task Stop()
        {
            IVoiceChannel channel = (Context.User as IVoiceState).VoiceChannel;
            IAudioClient client = await channel.ConnectAsync();
            var embed = new EmbedBuilder
            {
                Title = ":exclamation:Music stopped playing.:exclamation: "
            };
            await ReplyAsync("", false, embed.Build());
        }
    }
}
