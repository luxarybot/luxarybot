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
using System.Runtime.InteropServices;
using Luxary.Service;
using System.Web;
using Newtonsoft.Json;
using SteamKit2.GC.Dota.Internal;
using SteamKit2.Unified.Internal;

namespace Luxary
{
    public class Games : ModuleBase<ICommandContext>
    {
        public class Part22 : IEquatable<Part22>
        {
            public string PartName { get; set; }

            public string PartId { get; set; }

            public string PartMas { get; set; }

            public int PartID { get; set; }

            public string PartTitle { get; set; }

            public override string ToString()
            {
                return PartName;
            }

            public override int GetHashCode()
            {
                return PartID;
            }

            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                Part22 objAsPart = obj as Part22;
                if (objAsPart == null) return false;
                else return Equals(objAsPart);
            }

            public bool Equals(Part22 other)
            {
                if (other == null) return false;
                return (this.PartId.Equals(other.PartId));
            }
        }

        private static List<Part22> parts22 = new List<Part22>();

        [Command("pubg stats")]
        public async Task Pubg(string name, [Optional]string mode)
        {
            int gi = 0;
            if (mode == "duo")
            {
                gi = 0;
            }
            if (mode == "squad")
            {
                gi = 1;
            }
            if (mode == "duo-fpp")
            {
                gi = 2;
            }
            if (mode != null)
            {
                HttpWebRequest httpWebRequest =
                    WebRequest.Create($"https://api.pubgtracker.com/v2/profile/pc/{name}") as HttpWebRequest;
                httpWebRequest.Method = "GET";
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add("TRN-Api-Key",
                    "5ad9927e-15f1-4116-8159-6e74daf459fd");
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
                    var nname = container["nickname"];
                    var turl = container["avatar"];
                    var embed = new EmbedBuilder
                    {
                        Title = $"{nname}'s PUBG stats",
                        ThumbnailUrl = turl.ToString()
                    };
                    var count = container["stats"][gi]["stats"].Count();
                    //for (int i = 0; i < 2; i++)
                    //{
                    for (int ie = 0; ie < count; ie++)
                    {
                        var des = container["stats"][gi]["stats"][ie]["displayValue"].ToString();
                        var lbl = container["stats"][gi]["stats"][ie]["label"].ToString();
                        embed.Description += $"**{lbl}**: {des}\n";
                    }
                    //}
                    await ReplyAsync("", false, embed.Build());


                }
            }
            else
            {
                await ReplyAsync("choose gamemode ples, duo, squad, dua-fpp");
            }
        }
        [Command("osu user")]
        [Alias("osu us","osu u")]
        public async Task Ossu([Remainder] string name)
        {
            try
            {
                var request =
                    WebRequest.Create(
                            $"https://osu.ppy.sh/api/get_user?k=00b5c6aaae0d1a09091f08fc294836c893c591de&u={name}") as
                        HttpWebRequest;
                if (request == null) return;
                request.Method = "GET";
                request.ContentType = "application/json";

                var myWebResponse = (HttpWebResponse) request.GetResponse();

                var encoding = Encoding.ASCII;
                using (var reader =
                    new StreamReader(myWebResponse.GetResponseStream() ?? throw new InvalidOperationException(),
                        encoding))
                {
                    var result = reader.ReadToEnd();
                    var container = (JContainer) JsonConvert.DeserializeObject(result);
                    var userid = container[0]["user_id"].ToString();
                    double ppr = Convert.ToDouble(container[0]["pp_raw"]);
                    var pprank = container[0]["pp_rank"].ToString();
                    var username = container[0]["username"].ToString();
                    var pcount = container[0]["playcount"].ToString();
                    double pacc = Convert.ToDouble(container[0]["accuracy"]);
                    var country = container[0]["country"].ToString();
                    double lvl = Convert.ToDouble(container[0]["level"]);
                    var quickmaffs = Math.Truncate(lvl);
                    var quickmaffs2 = Math.Round(ppr, 0);
                    var quickmaffs3 = Math.Round(pacc, 1);
                    EmbedBuilder xd = new EmbedBuilder
                    {
                        Title = $"{username}'s Osu! profile",
                        Description =
                            $"**Level: {quickmaffs}**\n**Playcount:** {pcount}\n**Accuracy:** {quickmaffs3}%\n**Country:** {country}\n**Rank:** {pprank}\n**PP:** {quickmaffs2}",
                    };
                    string local = @"D:\Discord\Luxary\Luxary\bin\Debug\xd\"+userid+".jpg";
                    string imgurl = $"http://s.ppy.sh/a/{userid}";
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(imgurl, local);
                    }
                    xd.ThumbnailUrl = imgurl;
                    await ReplyAsync("", false, xd.Build());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await Erroruser();
            }
        }
        System.Text.StringBuilder mods = new System.Text.StringBuilder();
        [Command("osu userrecent")]
        [Alias("osu ur")]
        public async Task usebest([Remainder] string name)
        {
            try
            {
                EmbedBuilder xd = new EmbedBuilder();
                var request =
                    WebRequest.Create(
                            $"https://osu.ppy.sh/api/get_user?k=00b5c6aaae0d1a09091f08fc294836c893c591de&u={name}")
                        as
                        HttpWebRequest;
                if (request == null) return;
                request.Method = "GET";
                request.ContentType = "application/json";
                
                var myWebResponse = (HttpWebResponse) request.GetResponse();
                var encoding = Encoding.ASCII;
                using (var reader =
                    new StreamReader(myWebResponse.GetResponseStream() ?? throw new InvalidOperationException(),
                        encoding))
                {
                    var result = reader.ReadToEnd();
                    var container = (JContainer) JsonConvert.DeserializeObject(result);
                    var userid = container[0]["user_id"].ToString();
                    var username = container[0]["username"].ToString();
                    
                    try
                    {
                        
                        var request2 =
                            WebRequest.Create(
                                    $"https://osu.ppy.sh/api/get_user_recent?k=00b5c6aaae0d1a09091f08fc294836c893c591de&u={userid}&type=id")
                                as
                                HttpWebRequest;
                        if (request2 == null) return;
                        request2.Method = "GET";
                        request2.ContentType = "application/json";

                        var myWebResponse2 = (HttpWebResponse) request2.GetResponse();
                        var encoding2 = Encoding.ASCII;
                        using (var reader2 =
                            new StreamReader(
                                myWebResponse2.GetResponseStream() ?? throw new InvalidOperationException(),
                                encoding2))
                        {
                            var result2 = reader2.ReadToEnd();

                            JArray container2 = (JArray) JsonConvert.DeserializeObject(result2);
                            var length = container2.Count();
                            if (length > 0)
                            {
                                for (int xdd = 0; xdd < length; xdd++)
                                {
                                    var date = container2[xdd]["maxcombo"].ToString();
                                    var bmap = container2[xdd]["beatmap_id"].ToString();
                                    var rank = container2[xdd]["rank"].ToString();
                                    var c50 = Convert.ToDouble(container2[xdd]["count50"]);
                                    var c100 = Convert.ToDouble(container2[xdd]["count100"]);
                                    var c300 = Convert.ToDouble(container2[xdd]["count300"]);
                                    var mod = container2[xdd]["enabled_mods"].ToString();
                                    
                                    var cmiss = Convert.ToDouble(container2[xdd]["countmiss"]);
                                    var acc2 = 100.0 * (6 * c300 + 2 * c100 + c50) / (6 * (c50 + c100 + c300 + cmiss));
                                    var acc = Math.Round(acc2, 2);
                                    if (rank.Contains("A"))
                                    {
                                        rank = "<:rankingAsmall:400920344593956867>";
                                    }
                                    if (rank.Contains("B"))
                                    {
                                        rank = "<:rankingBsmall:400920320925761538>";
                                    }
                                    if (rank.Contains("C"))
                                    {
                                        rank = "<:rankingCsmall:400920375053254656>";
                                    }
                                    if (rank.Contains("D"))
                                    {
                                        rank = "<:rankingDsmall:400920408884510721>";
                                    }
                                    if (rank.Contains("S"))
                                    {
                                        rank = "<:rankingSsmall:400920431344746517>";
                                    }
                                    if (rank.Contains("F"))
                                    {
                                        rank = "<:sectionfail:400920942408368128>";
                                    }
                                    if (rank.Contains("X"))
                                    {
                                        rank = "<:rankingXsmall:400920039479574532>";
                                    }
                                    if (mod==("1"))
                                    {
                                        mods.AppendLine("NF");
                                    }
                                    if (mod==("2"))
                                    {
                                        mods.AppendLine("EZ");
                                    }
                                    if (mod==("3"))
                                    {
                                        mods.AppendLine("EZNF");
                                    }
                                    if (mod==("8"))
                                    {
                                        mods.AppendLine("HD");
                                    }
                                    if (mod==("9"))
                                    {
                                        mods.AppendLine("NFHD");
                                    }
                                    if (mod==("10"))
                                    {
                                        mods.AppendLine("EZHD");
                                    }
                                    if (mod==("11"))
                                    {
                                        mods.AppendLine("NFEZHD");
                                    }
                                    if (mod==("16"))
                                    {
                                        mods.AppendLine("HR");
                                    }
                                    if (mod==("17"))
                                    {
                                        mods.AppendLine("NFHR");
                                    }
                                    if (mod==("24"))
                                    {
                                        mods.AppendLine("HDHR");
                                    }
                                    if (mod==("32"))
                                    {
                                        mods.AppendLine("SD");
                                    }
                                    if (mod==("34"))
                                    {
                                        mods.AppendLine("EZSD");
                                    }
                                    if (mod==("40"))
                                    {
                                        mods.AppendLine("HDSD");
                                    }
                                    if (mod==("64"))
                                    {
                                        mods.AppendLine("DT");
                                    }
                                    if (mod==("66"))
                                    {
                                        mods.AppendLine("EZDT");
                                    }
                                    if (mod==("72"))
                                    {
                                        mods.AppendLine("HDDT");
                                    }
                                    if (mod==("73"))
                                    {
                                        mods.AppendLine("NFHDDT");
                                    }
                                    if (mod==("74"))
                                    {
                                        mods.AppendLine("EZHDDT");
                                    }
                                    if (mod==("75"))
                                    {
                                        mods.AppendLine("NFEZHDDT");
                                    }
                                    if (mod==("88"))
                                    {
                                        mods.AppendLine("HDHRDT");
                                    }
                                    if (mod==("89"))
                                    {
                                        mods.AppendLine("NFHDHRDT");
                                    }
                                    if (mod==("128"))
                                    {
                                        mods.AppendLine("RL PLEB");
                                    }
                                    if (mod==("16418"))
                                    {
                                        mods.AppendLine("EZPF");
                                    }
                                    //------------------------------------------------
                                    var request3 =
                                        WebRequest.Create(
                                                $"https://osu.ppy.sh/api/get_beatmaps?k=00b5c6aaae0d1a09091f08fc294836c893c591de&b={bmap}")
                                            as
                                            HttpWebRequest;
                                    if (request3 == null) return;

                                    request3.Method = "GET";
                                    request3.ContentType = "application/json";
                                    var myWebResponse3 = (HttpWebResponse) request3.GetResponse();
                                    var encoding3 = Encoding.ASCII;
                                    using (var reader3 =
                                        new StreamReader(
                                            myWebResponse3.GetResponseStream() ?? throw new InvalidOperationException(),
                                            encoding3))
                                    {
                                        var result3 = reader3.ReadToEnd();
                                        var container3 = (JContainer) JsonConvert.DeserializeObject(result3);

                                        var title = container3[0]["title"].ToString();
                                        var diff = container3[0]["version"].ToString();
                                        var url = container3[0]["beatmapset_id"].ToString();
                                        var mc = container3[0]["max_combo"].ToString();
                                        var ar = container3[0]["artist"].ToString();

                                        xd.Title = username;
                                        xd.Description = $"{length} Recent plays.\n----";
                                        xd.ThumbnailUrl = $"http://s.ppy.sh/a/{userid}";
                                        if (mod == "0")
                                        {
                                            xd.AddField(x =>
                                            {
                                                x.Name = $"{ar} - {title} ({diff})";
                                                x.Value =
                                                    $"**Rank:** {rank} **Combo**: {date}({mc}) **Accuracy:** {acc}[:arrow_down:](https://osu.ppy.sh/d/{url}) [:information_source:](https://osu.ppy.sh/s/{url})";
                                            });
                                        }                                    
                                        else
                                        {
                                            xd.AddField(x =>
                                            {
                                                x.Name = $"{ar} - {title} ({diff}) +{mods}";
                                                x.Value =
                                                    $"**Rank:** {rank} **Combo**: {date}({mc}) **Accuracy:** {acc}[:arrow_down:](https://osu.ppy.sh/d/{url}) [:information_source:](https://osu.ppy.sh/s/{url})";
                                            });
                                            mods.Clear();
                                        }
                                    }
                                }
                                await ReplyAsync("", false, xd.Build());
                            }
                            else
                            {
                                await Errorrecent();
                            }
                        }
                    }
                    catch (Exception x)
                    {
                        await Errorrecent();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await Erroruser();
            }
        }

        public async Task Errorrecent()
        {
            var auth = new EmbedAuthorBuilder()
            {
                Name = $"Error",
            };
            var rnd = new Random();
            int g1 = rnd.Next(1, 255);
            int g2 = rnd.Next(1, 255);
            int g3 = rnd.Next(1, 255);
            var builder = new EmbedBuilder()
            {
                Color = new Discord.Color(g1, g2, g3),
                Author = auth,
            };
            builder.Description = $"No recent plays.";
            builder.ThumbnailUrl =
            $"https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/pic/oh.png";
            await ReplyAsync("", false, builder.Build());
            return;
        }
        public async Task Erroruser()
        {
            var auth = new EmbedAuthorBuilder()
            {
                Name = $"Error",
            };
            var rnd = new Random();
            int g1 = rnd.Next(1, 255);
            int g2 = rnd.Next(1, 255);
            int g3 = rnd.Next(1, 255);
            var builder = new EmbedBuilder()
            {
                Color = new Discord.Color(g1, g2, g3),
                Author = auth,
            };
            builder.Description = $"User not found.";
            builder.ThumbnailUrl =
                $"https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/pic/oh.png";
            await ReplyAsync("", false, builder.Build());
            return;
        }


        [Command("osu userbest")]
        [Alias("osu ub")]
        public async Task Osssu([Remainder] string name)
        {
            //https://osu.ppy.sh/api/get_beatmaps?k=00b5c6aaae0d1a09091f08fc294836c893c591de&b=275265"
            try
            {
                var request =
                    WebRequest.Create(
                            $"https://osu.ppy.sh/api/get_user?k=00b5c6aaae0d1a09091f08fc294836c893c591de&u={name}") as
                        HttpWebRequest;
                if (request == null) return;
                request.Method = "GET";
                request.ContentType = "application/json";

                var myWebResponse = (HttpWebResponse) request.GetResponse();
                var encoding = Encoding.ASCII;
                using (var reader =
                    new StreamReader(myWebResponse.GetResponseStream() ?? throw new InvalidOperationException(),
                        encoding))
                {
                    var result = reader.ReadToEnd();
                    var container = (JContainer) JsonConvert.DeserializeObject(result);
                    var userid = container[0]["user_id"].ToString();
                    var username = container[0]["username"].ToString();
                    EmbedBuilder xd = new EmbedBuilder
                    {
                        Title = username,
                        Description = "Top 10 plays.\n----",
                        ThumbnailUrl = $"http://s.ppy.sh/a/{userid}"
                    };
                    for (int xdd = 0; xdd < 10; xdd++)
                    {
                        var request2 =
                            WebRequest.Create(
                                    $"https://osu.ppy.sh/api/get_user_best?k=00b5c6aaae0d1a09091f08fc294836c893c591de&u={userid}")
                                as
                                HttpWebRequest;
                        if (request2 == null) return;
                        request2.Method = "GET";
                        request2.ContentType = "application/json";

                        var myWebResponse2 = (HttpWebResponse) request2.GetResponse();
                        var encoding2 = Encoding.ASCII;
                        using (var reader2 =
                            new StreamReader(
                                myWebResponse2.GetResponseStream() ?? throw new InvalidOperationException(),
                                encoding2))
                        {
                            var result2 = reader2.ReadToEnd();

                            var container2 = (JContainer) JsonConvert.DeserializeObject(result2);
                            double ppr2 = Convert.ToDouble(container2[xdd]["pp"]);
                            var date = container2[xdd]["maxcombo"].ToString();
                            var bmap = container2[xdd]["beatmap_id"].ToString();
                            var rank = container2[xdd]["rank"].ToString();
                            var mod = container2[xdd]["enabled_mods"].ToString();
                            var c50 = Convert.ToDouble(container2[xdd]["count50"]);
                            var c100 = Convert.ToDouble(container2[xdd]["count100"]);
                            var c300 = Convert.ToDouble(container2[xdd]["count300"]);
                            
                            var cmiss = Convert.ToDouble(container2[xdd]["countmiss"]);
                            var acc2 = 100.0 * (6 * c300 + 2 * c100 + c50) / (6 * (c50 + c100 + c300 + cmiss));
                            var acc = Math.Round(acc2, 2);
                            var quickmaffs = Math.Round(ppr2, 0);
                            //------------------------------------------------
                            var request3 =
                                WebRequest.Create(
                                        $"https://osu.ppy.sh/api/get_beatmaps?k=00b5c6aaae0d1a09091f08fc294836c893c591de&b={bmap}")
                                    as
                                    HttpWebRequest;
                            if (request3 == null) return;
                            if (rank.Contains("A"))
                            {
                                rank = "<:rankingAsmall:400920344593956867>";
                            }
                            if (rank.Contains("B"))
                            {
                                rank = "<:rankingBsmall:400920320925761538>";
                            }
                            if (rank.Contains("C"))
                            {
                                rank = "<:rankingCsmall:400920375053254656>";
                            }
                            if (rank.Contains("D"))
                            {
                                rank = "<:rankingDsmall:400920408884510721>";
                            }
                            if (rank.Contains("S"))
                            {
                                rank = "<:rankingSsmall:400920431344746517>";
                            }
                            if (rank.Contains("F"))
                            {
                                rank = "<:sectionfail:400920942408368128>";
                            }
                            if (rank.Contains("X"))
                            {
                                rank = "<:rankingXsmall:400920039479574532>";
                            }
                            if (mod==("1"))
                            {
                                mods.AppendLine("NF");
                            }
                            if (mod=="2")
                            {
                                mods.AppendLine("EZ");
                            }
                            if (mod==("3"))
                            {
                                mods.AppendLine("EZNF");
                            }
                            if (mod==("8"))
                            {
                                mods.AppendLine("HD");
                            }
                            if (mod==("9"))
                            {
                                mods.AppendLine("NFHD");
                            }
                            if (mod==("10"))
                            {
                                mods.AppendLine("EZHD");
                            }
                            if (mod==("11"))
                            {
                                mods.AppendLine("NFEZHD");
                            }
                            if (mod==("16"))
                            {
                                mods.AppendLine("HR");
                            }
                            if (mod==("17"))
                            {
                                mods.AppendLine("NFHR");
                            }
                            if (mod==("24"))
                            {
                                mods.AppendLine("HDHR");
                            }
                            if (mod==("32"))
                            {
                                mods.AppendLine("SD");
                            }
                            if (mod==("34"))
                            {
                                mods.AppendLine("EZSD");
                            }
                            if (mod==("40"))
                            {
                                mods.AppendLine("HDSD");
                            }
                            if (mod==("64"))
                            {
                                mods.AppendLine("DT");
                            }
                            if (mod==("66"))
                            {
                                mods.AppendLine("EZDT");
                            }
                            if (mod==("72"))
                            {
                                mods.AppendLine("HDDT");
                            }
                            if (mod==("73"))
                            {
                                mods.AppendLine("NFHDDT");
                            }
                            if (mod==("74"))
                            {
                                mods.AppendLine("EZHDDT");
                            }
                            if (mod==("75"))
                            {
                                mods.AppendLine("NFEZHDDT");
                            }
                            if (mod==("88"))
                            {
                                mods.AppendLine("HDHRDT");
                            }
                            if (mod==("89"))
                            {
                                mods.AppendLine("NFHDHRDT");
                            }
                            if (mod==("128"))
                            {
                                mods.AppendLine("RL PLEB");
                            }
                            if (mod==("16418"))
                            {
                                mods.AppendLine("EZPF");
                            }

                            request3.Method = "GET";
                            request3.ContentType = "application/json";
                            var myWebResponse3 = (HttpWebResponse) request3.GetResponse();
                            var encoding3 = Encoding.ASCII;
                            using (var reader3 =
                                new StreamReader(
                                    myWebResponse3.GetResponseStream() ?? throw new InvalidOperationException(),
                                    encoding3))
                            {
                                var result3 = reader3.ReadToEnd();
                                var container3 = (JContainer) JsonConvert.DeserializeObject(result3);

                                var title = container3[0]["title"].ToString();
                                var diff = container3[0]["version"].ToString();
                                var url = container3[0]["beatmapset_id"].ToString();
                                var mc = container3[0]["max_combo"].ToString();
                                var ar = container3[0]["artist"].ToString();

                                if (mod == "0")
                                {
                                    xd.AddField(x =>
                                    {
                                        x.Name = $"{ar} - {title} ({diff})";
                                        x.Value =
                                            $"**PP**: {quickmaffs} **Rank:** {rank} **Combo**: {date}({mc})**Accuracy:** {acc}[:arrow_down:](https://osu.ppy.sh/d/{url}) [:information_source:](https://osu.ppy.sh/s/{url})";

                                    });                                    
                                }
                                else if (mod == "592")
                                {
                                    xd.AddField(x =>
                                    {
                                        x.Name = $"{ar} - {title} ({diff})";
                                        x.Value =
                                            $"**PP**: {quickmaffs} **Rank:** {rank} **Combo**: {date}({mc})**Accuracy:** {acc}[:arrow_down:](https://osu.ppy.sh/d/{url}) [:information_source:](https://osu.ppy.sh/s/{url})";

                                    });
                                }
                                else
                                {
                                    xd.AddField(x =>
                                    {
                                        x.Name = $"{ar} - {title} ({diff}) +{mods}";
                                        x.Value =
                                            $"**PP**: {quickmaffs} **Rank:** {rank} **Combo**: {date}({mc})**Accuracy:** {acc}[:arrow_down:](https://osu.ppy.sh/d/{url}) [:information_source:](https://osu.ppy.sh/s/{url})";

                                    });
                                    mods.Clear();
                                }
                            }
                        }
                    }
                    await ReplyAsync("", false, xd.Build());
                }
            }



            catch (Exception e)
            {
                await Erroruser();
            }
        }

        [Command("osu beatmap")]
        [Alias("osu bmap")]
        public async Task BM(string url)
        {
            try
            {
                //int t = 0;
                //string code = "";
                //foreach (char x in url)
                //{
                //    if (x == '/')
                //    {
                //        t = t + 1;
                //    }
                //    else if (t == 4)
                //    {
                //        code += x.ToString();
                //    }
                //}
                //string img = "";
                //foreach (char x in url)
                //{
                //    if (x == '/')
                //    {
                //        t = t + 1;
                //    }
                //    else if (t == 4)
                //    {
                //        code += x.ToString();
                //    }
                //}
                var request3 =
                    WebRequest.Create(
                            $"https://osu.ppy.sh/api/get_beatmaps?k=00b5c6aaae0d1a09091f08fc294836c893c591de&s={url}")
                        as
                        HttpWebRequest;
                if (request3 == null) return;
                EmbedBuilder xdd = new EmbedBuilder
                {
                    Description = "---"
                };
                request3.Method = "GET";
                request3.ContentType = "application/json";
                var myWebResponse3 = (HttpWebResponse) request3.GetResponse();
                var encoding3 = Encoding.ASCII;
                using (var reader3 =
                    new StreamReader(
                        myWebResponse3.GetResponseStream() ?? throw new InvalidOperationException(),
                        encoding3))
                {
                    var result3 = reader3.ReadToEnd();
                    var haitai = (JArray) JsonConvert.DeserializeObject(result3);
                    var kekistan = haitai.Count;
                    
                    for(int i=0; i < haitai.Count; i++)
                    { 
                        var title = haitai[i]["title"].ToString();
                        var bpm = haitai[i]["bpm"].ToString();
                        var version = haitai[i]["version"].ToString();
                        var creator = haitai[i]["creator"].ToString();
                        var max_combo = haitai[i]["max_combo"].ToString();
                        double difficultyrating = Convert.ToDouble(haitai[i]["difficultyrating"]);
                        var approved_date = haitai[i]["approved_date"].ToString();
                        var diff_approach = haitai[i]["diff_approach"].ToString();
                        var approved = haitai[i]["approved"].ToString();
                        var mlg = haitai[i]["beatmapset_id"].ToString();
                        var mlg2 = haitai[i]["beatmap_id"].ToString();
                        var shee = Math.Round(difficultyrating, 2);
                        if (approved.Contains("0"))
                        {
                            xdd.AddField(x =>
                            {
                                x.Name = $"[{title}({version})](https://osu.ppy.sh/api/get_beatmaps?k=00b5c6aaae0d1a09091f08fc294836c893c591de&s={url})";
                                x.Value =
                                    $"**Type:** Pending **Creator:** {creator} **BPM** {bpm} **Max combo:** {max_combo}\n**Difficulty:** {shee} **AR:** {diff_approach} **Approve date:** {approved_date}";
                            });
                        }
                        else if (approved.Contains("-1"))
                        {
                            xdd.AddField(x =>
                            {
                                x.Name = $"[{title}({version})](https://osu.ppy.sh/api/get_beatmaps?k=00b5c6aaae0d1a09091f08fc294836c893c591de&s={url})";
                                x.Value =
                                    $"**Type:** WIP **Creator:** {creator}**BPM** {bpm}**Max combo:** {max_combo}\n**Difficulty:** {shee} **AR:** {diff_approach}";
                            });
                        }
                        else if (approved.Contains("-2"))
                        {
                            xdd.AddField(x =>
                            {
                                x.Name = $"[{title}({version})](https://osu.ppy.sh/api/get_beatmaps?k=00b5c6aaae0d1a09091f08fc294836c893c591de&s={url})";
                                x.Value =
                                    $"**Type:** In the graveyard ;( **Creator:** {creator} **BPM** {bpm} **Max combo:** {max_combo}\n**Difficulty:** {shee} **AR:** {diff_approach}";
                            });
                        }
                        else if (approved.Contains("1"))
                        {
                            xdd.AddField(x =>
                            {
                                x.Name = $"[{title}({version})]";
                                x.Value =
                                    $"**Type:** Ranked **Creator:** {creator} **BPM** {bpm} **Max combo:** {max_combo}\n**Difficulty:** {shee} **AR:** {diff_approach} **Approve date:** {approved_date}[:arrow_down:](https://osu.ppy.sh/d/{url}) [:information_source:](https://osu.ppy.sh/s/{url})";
                            });
                        }
                        else if (approved.Contains("2"))
                        {
                            xdd.AddField(x =>
                            {
                                x.Name = $"[{title}({version})]";
                                x.Value =
                                    $"**Type:** Approved **Creator:** {creator} **BPM** {bpm} **Max combo:** {max_combo}\n**Difficulty:** {shee} **AR:** {diff_approach} **Approve date:** {approved_date}";
                            });
                        }
                        else if (approved.Contains("3"))
                        {
                            xdd.AddField(x =>
                            {
                                x.Name = $"[{title}({version})](https://osu.ppy.sh/api/get_beatmaps?k=00b5c6aaae0d1a09091f08fc294836c893c591de&s={url})";
                                x.Value =
                                    $"**Type:** qualified **Creator:** {creator} **BPM** {bpm} **Max combo:** {max_combo}\n**Difficulty:** {shee} **AR:** {diff_approach} **Approve date:** {approved_date}";
                            });
                        }
                        else if (approved.Contains("4"))
                        {
                            xdd.AddField(x =>
                            {
                                x.Name = $"[{title}({version})](https://osu.ppy.sh/api/get_beatmaps?k=00b5c6aaae0d1a09091f08fc294836c893c591de&s={url})";
                                x.Value =
                                    $"**Type:** LOVED ❤ **Creator:** {creator} **BPM** {bpm} **Max combo:** {max_combo}\n**Difficulty:** {shee} **AR:** {diff_approach} **Approve date:** {approved_date}";
                            });
                        }
                        
                        else
                        {
                            var auth = new EmbedAuthorBuilder()
                            {
                                Name = $"Error",
                            };
                            var rnd = new Random();
                            int g1 = rnd.Next(1, 255);
                            int g2 = rnd.Next(1, 255);
                            int g3 = rnd.Next(1, 255);
                            var builder = new EmbedBuilder()
                            {
                                Color = new Discord.Color(g1, g2, g3),
                                Author = auth,
                            };
                            builder.Description = $"No data yet....\nosu ples";
                            builder.ThumbnailUrl =
                                $"https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/pic/oh.png";
                            await ReplyAsync("", false, builder.Build());
                        }
                        xdd.Title = title;
                    }
                    await ReplyAsync("", false, xdd.Build());
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
                var builder = new EmbedBuilder()
                {
                    Color = new Discord.Color(g1, g2, g3),
                    Author = auth,
                };
                builder.Description = $"No data yet....\nosu ples";
                builder.ThumbnailUrl =
                    $"https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/pic/oh.png";
                await ReplyAsync("", false, builder.Build());
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
