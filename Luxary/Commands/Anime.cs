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
using System.Xml;
using Newtonsoft.Json;

namespace Luxary
{
    [Group("mal")]
    public class Anime : ModuleBase<ICommandContext>
    {
        [Command("search")]
        [Alias("s","sh")]
        public async Task search([Remainder]string tag)
        {
            try
            {
                StreamReader sr = new StreamReader("password.txt");
                string password = sr.ReadLine();
                var xd = tag.Replace(" ", "+");
                WebRequest request = WebRequest.Create($"https://myanimelist.net/api/anime/search.xml?q={xd}");
                request.Method = "GET";
                request.Credentials = new NetworkCredential("Luxedo", password);
                request.PreAuthenticate = true;
                request.ContentType = "application/x-www-form-urlencoded";
                WebResponse response = request.GetResponse();
                Stream Answer = response.GetResponseStream();
                StreamReader _Answer = new StreamReader(Answer);
                string content = _Answer.ReadToEnd();

                XmlDocument Doc = new XmlDocument();
                Doc.LoadXml(content);
                XmlNodeList xnList = Doc.SelectNodes("/anime/entry");
                var kanna = new EmbedBuilder();
                kanna.Title = $"{tag}'s search result";
                kanna.Description = $"---";
                foreach (XmlNode xn in xnList)
                {      
                    string title = xn["title"].InnerText;
                    string score = xn["score"].InnerText;
                    string epi = xn["episodes"].InnerText;
                    string status = xn["status"].InnerText;
                    string pic = xn["image"].InnerText;
                    string id = xn["id"].InnerText;
                    kanna.ThumbnailUrl = pic;
                    kanna.AddField(x =>
                    {
                        x.Name = $"{title}";
                        x.Value = $"**Episodes:** {epi} **Score:** {score}\n**Status:** {status}[:information_source:](https://myanimelist.net/anime/{id})";
                    });
                }
                await ReplyAsync("", false, kanna.Build());

                response.Close();              
            }
            catch (Exception ee)
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
                    Description = $"Anime not found.",
                    ThumbnailUrl =
                        $"https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/pic/silly.png",
                };
                await ReplyAsync("", false, builder.Build());
                Console.WriteLine(ee);
            }
        }
    }
}
