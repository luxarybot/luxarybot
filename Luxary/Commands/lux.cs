using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.IO.Compression;
using Luxary.Service;

namespace Luxary
{
        [Group("Emote")]
        [Alias("l", "e")]
        public class LuxEmotes : ModuleBase<ICommandContext>
        {
            [Command("syndra")]
            [Summary(".lux syndra")]
            [Remarks("Shows an syndra emote")]
            public async Task syndra()
            {
                int Delete = 1;
                foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
                {
                    await Item.DeleteAsync();
                }
                await Context.Channel.SendFileAsync("D:/Discord/Luxary/Luxary/bin/Debug/pic/syn.png");
            }
            [Command("silly")]
            [Summary(".lux silly")]
            [Remarks("Shows an lux emote")]
            public async Task silly()
            {
                int Delete = 1;
                foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
                {
                    await Item.DeleteAsync();
                }
                await Context.Channel.SendFileAsync("D:/Discord/Luxary/Luxary/bin/Debug/pic/silly.png");
            }
            [Command("triggered")]
            [Summary(".lux triggered")]
            [Remarks("Shows an lux emote")]
            public async Task trig()
            {
                int Delete = 1;
                foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
                {
                    await Item.DeleteAsync();
                }
                await Context.Channel.SendFileAsync("D:/Discord/Luxary/Luxary/bin/Debug/pic/luxerred.gif");
            }
            [Command("normal")]
            [Summary(".lux normal")]
            [Remarks("Shows an lux emote")]
            public async Task normal()
            {
            int Delete = 1;
            foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
            {
                await Item.DeleteAsync();
            }
            await Context.Channel.SendFileAsync("D:/Discord/Luxary/Luxary/bin/Debug/pic/normal.png");
            }
            [Command("mad")]
            [Summary(".lux mad")]
            [Remarks("Shows an lux emote")]
            public async Task mad()
            {
            int Delete = 1;
            foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
            {
                await Item.DeleteAsync();
            }
            await Context.Channel.SendFileAsync("D:/Discord/Luxary/Luxary/bin/Debug/pic/mad.png");
            }
            [Command("sad")]
            [Summary(".lux sad")]
            [Remarks("Shows an lux emote")]
            public async Task sad()
            {
            int Delete = 1;
            foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
            {
                await Item.DeleteAsync();
            }
            await Context.Channel.SendFileAsync("D:/Discord/Luxary/Luxary/bin/Debug/pic/sad.png");
            }
            [Command("cry")]
            [Summary(".lux cry")]
            [Remarks("Shows an lux emote")]
            public async Task cry()
            {
            int Delete = 1;
            foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
            {
                await Item.DeleteAsync();
            }
            await Context.Channel.SendFileAsync("D:/Discord/Luxary/Luxary/bin/Debug/pic/cry.png");
            }
            [Command("hmm")]
            [Summary(".lux hmm")]
            [Remarks("Shows an lux emote")]
            public async Task hmm()
            {
            int Delete = 1;
            foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
            {
                await Item.DeleteAsync();
            }
            await Context.Channel.SendFileAsync("D:/Discord/Luxary/Luxary/bin/Debug/pic/hmm.png");
            }
            [Command("gasm")]
            [Summary(".lux gasm")]
            [Remarks("Shows an lux emote")]
            public async Task gasm()
            {
            int Delete = 1;
            foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
            {
                await Item.DeleteAsync();
            }
            await Context.Channel.SendFileAsync("D:/Discord/Luxary/Luxary/bin/Debug/pic/gasm.png");
            }
            [Command("lel")]
            [Summary(".lux lel")]
            [Remarks("Shows an lux emote")]
            public async Task lel()
            {
            int Delete = 1;
            foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
            {
                await Item.DeleteAsync();
            }
            await Context.Channel.SendFileAsync("D:/Discord/Luxary/Luxary/bin/Debug/pic/lel2.png");
            }
            [Command("ded")]
            [Summary(".lux ded")]
            [Remarks("Shows an lux emote")]
            public async Task ded()
            {
            int Delete = 1;
            foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
            {
                await Item.DeleteAsync();
            }
            await Context.Channel.SendFileAsync("D:/Discord/Luxary/Luxary/bin/Debug/pic/ded.png");
            }
            [Command("happy")]
            [Summary(".lux happy")]
            [Remarks("Shows an lux emote")]
            public async Task happy()
            {
            int Delete = 1;
            foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
            {
                await Item.DeleteAsync();
            }
            await Context.Channel.SendFileAsync("D:/Discord/Luxary/Luxary/bin/Debug/pic/happy.png");
            }
            [Command("shy")]
            [Summary(".lux shy")]
            [Remarks("Shows an lux emote")]
            public async Task shy()
            {
            int Delete = 1;
            foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
            {
                await Item.DeleteAsync();
            }
            await Context.Channel.SendFileAsync("D:/Discord/Luxary/Luxary/bin/Debug/pic/shy.png");
            }
            [Command("m2s")]
            [Summary(".lux m2s")]
            [Remarks("Fuck overwatch")]
            public async Task m2s()
            {
            int Delete = 1;
            foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
            {
                await Item.DeleteAsync();
            }
            await Context.Channel.SendFileAsync("D:/Discord/Luxary/Luxary/bin/Debug/pic/m2s.png");
            }
            [Command("thonk")]
            [Summary(".lux thonk")]
            [Remarks("Shows THONK boi.")]
            public async Task thonk()
            {
            int Delete = 1;
            foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
            {
                await Item.DeleteAsync();
            }
            await Context.Channel.SendFileAsync("D:/Discord/Luxary/Luxary/bin/Debug/pic/thonk.png");
            }
            [Command("oh")]
            [Summary(".lux oh")]
            [Remarks("Shows an lux emote")]
            public async Task oh()
            {
            int Delete = 1;
            foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
            {
                await Item.DeleteAsync();
            }
            await Context.Channel.SendFileAsync("D:/Discord/Luxary/Luxary/bin/Debug/pic/oh.png");
            }
            [Command("think")]
            [Summary(".lux think")]
            [Remarks("Shows an lux emote")]
            public async Task thinking()
            {
            int Delete = 1;
            foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
            {
                await Item.DeleteAsync();
            }
            await Context.Channel.SendFileAsync("D:/Discord/Luxary/Luxary/bin/Debug/pic/thinking.png");
            }
            [Command("kl")]
            [Summary(".lux kl")]
            [Remarks("Shows an kappa lux emote")]
            public async Task kl()
            {
            int Delete = 1;
            foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
            {
                await Item.DeleteAsync();
            }
            await Context.Channel.SendFileAsync("D:/Discord/Luxary/Luxary/bin/Debug/pic/kl.png");
            }
            [Command("cat")]
            [Summary(".lux cat")]
            [Remarks("Shows an lux emote")]
            public async Task cat()
            {
            int Delete = 1;
            foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
            {
                await Item.DeleteAsync();
            }
            await Context.Channel.SendFileAsync("D:/Discord/Luxary/Luxary/bin/Debug/pic/sg.png");
            }
            [Command("bulb")]
            [Summary(".lux bulb")]
            [Remarks("Shows an lux emote")]
            public async Task bulb()
            {
            int Delete = 1;
            foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
            {
                await Item.DeleteAsync();
            }
            await Context.Channel.SendFileAsync("D:/Discord/Luxary/Luxary/bin/Debug/pic/bulb.png");
            }
            [Command(":3")]
            [Summary(".lux :3")]
            [Remarks("Shows an lux emote")]
            public async Task lell()
            {
                int Delete = 1;
                foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
                {
                    await Item.DeleteAsync();
                }
                await Context.Channel.SendFileAsync("D:/Discord/Luxary/Luxary/bin/Debug/pic/lel.png");
            }
            [Command("luxgasm")]
            [Alias("lg")]
            [Summary(".lux luxgasm")]
            [Remarks("Shows an lux emote")]
            public async Task luxgasm()
            {
            int Delete = 1;
            foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
            {
                await Item.DeleteAsync();
            }
            await Context.Channel.SendFileAsync("D:/Discord/Luxary/Luxary/bin/Debug/pic/luxgasm.png");
            }
    }
}

