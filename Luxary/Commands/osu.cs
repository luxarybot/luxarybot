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
using Newtonsoft.Json;

namespace Luxary
{
    [Group("osu")]
    public class Osu : ModuleBase<ICommandContext>
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

        [Command("user")]
        [Alias("us")]
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
        [Command("userrecent")]
        [Alias("ur", "urecent")]
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
                                    var cmiss = Convert.ToDouble(container2[xdd]["countmiss"]);
                                    var acc2 = 100.0 * (6 * c300 + 2 * c100 + c50) / (6 * (c50 + c100 + c300 + cmiss));
                                    var acc = Math.Round(acc2, 2);
                                    if (rank.Contains("F"))
                                    {
                                        rank = "Forfeit";
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

                                        xd.Title = username;
                                        xd.Description = $"{length} Recent plays.\n----";
                                        xd.ThumbnailUrl = $"http://s.ppy.sh/a/{userid}";

                                        xd.AddField(x =>
                                        {
                                            x.Name = $"{title} ({diff})";
                                            x.Value = $"**Rank:** {rank} **Combo**: {date} **Accuracy:** {acc}\n";
                                        });
                                    }
                                    ;
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

            
        [Command("userbest")]
        [Alias("ub","ubest")]
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
                                xd.AddField(x =>
                                {
                                    x.Name = $"{title} ({diff})";
                                    x.Value = $"**PP**: {quickmaffs} **Rank:** {rank} **Combo**: {date}**Accuracy:** {acc}\n";
                                });
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

        [Command("beatmap")]
        [Alias("bmap","bm")]
        public async Task BM(string url)
        {
            try
            {
                int t = 0;
                string code = "";
                foreach (char x in url)
                {
                    if (x == '/')
                    {
                        t = t + 1;
                    }
                    else if (t == 4)
                    {
                        code += x.ToString();
                    }
                }
                string img = "";
                foreach (char x in url)
                {
                    if (x == '/')
                    {
                        t = t + 1;
                    }
                    else if (t == 4)
                    {
                        code += x.ToString();
                    }
                }
                var request3 =
                    WebRequest.Create(
                            $"https://osu.ppy.sh/api/get_beatmaps?k=00b5c6aaae0d1a09091f08fc294836c893c591de&b={code}")
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
                    var bpm= container3[0]["bpm"].ToString();
                    var version = container3[0]["version"].ToString();
                    var creator = container3[0]["creator"].ToString();
                    var max_combo = container3[0]["max_combo"].ToString();
                    double difficultyrating = Convert.ToDouble(container3[0]["difficultyrating"]);
                    var approved_date = container3[0]["approved_date"].ToString();
                    var diff_approach = container3[0]["diff_approach"].ToString();
                    var approved = container3[0]["approved"].ToString();
                    var mlg = container3[0]["beatmapset_id"].ToString();
                    var mlg2 = container3[0]["beatmap_id"].ToString();
                    var shee = Math.Round(difficultyrating, 2);
                    if (approved.Contains("0"))
                    {
                        EmbedBuilder xd = new EmbedBuilder
                        {
                            Title = title + $"({version})",
                            Description =
                                $"**Type:** Pending\n**Creator:** {creator}\n**BPM** {bpm}\n**Max combo:** {max_combo}\n**Difficulty:** {shee}\n**AR:** {diff_approach}\n**Approve date:** {approved_date}",
                           ImageUrl = $"https://b.ppy.sh/thumb/{mlg}l.jpg"
                        };
                        await ReplyAsync($"**Download:** " + $"https://osu.ppy.sh/d/{mlg}", false, xd.Build());
                    }
                    else if (approved.Contains("-1"))
                    {
                        EmbedBuilder xd = new EmbedBuilder
                        {
                            Title = title + $"({version})",
                            Description =
                                $"**Type:** WIP\n**Creator:** {creator}\n**BPM** {bpm}\n**Max combo:** {max_combo}\n**Difficulty:** {shee}\n**AR:** {diff_approach}",
                           ImageUrl = $"https://b.ppy.sh/thumb/{mlg}l.jpg"
                           
                        };
                        await ReplyAsync($"**Download:** " + $"https://osu.ppy.sh/d/{mlg}", false, xd.Build());
                    }
                    else if (approved.Contains("-2"))
                    {
                        EmbedBuilder xd = new EmbedBuilder
                        {
                            Title = title + $"({version})",
                            Description =
                                $"**Type:** In the graveyard ;(\n**Creator:** {creator}\n**BPM** {bpm}\n**Max combo:** {max_combo}\n**Difficulty:** {shee}\n**AR:** {diff_approach}",
                           ImageUrl = $"https://b.ppy.sh/thumb/{mlg}l.jpg"
                        };
                        await ReplyAsync($"**Download:** " + $"https://osu.ppy.sh/d/{mlg}", false, xd.Build());
                    }
                    else if (approved.Contains("1"))
                    {
                        EmbedBuilder xd = new EmbedBuilder
                        {
                            Title = title + $"({version})",
                            Description =
                                $"**Type:** Ranked\n**Creator:** {creator}\n**BPM** {bpm}\n**Max combo:** {max_combo}\n**Difficulty:** {shee}\n**AR:** {diff_approach}\n**Approve date:** {approved_date}",
                           ImageUrl = $"https://b.ppy.sh/thumb/{mlg}l.jpg"
                        };
                        await ReplyAsync($"**Download:** " + $"https://osu.ppy.sh/d/{mlg}", false, xd.Build());
                    }
                    else if (approved.Contains("2"))
                    {
                        EmbedBuilder xd = new EmbedBuilder
                        {
                            Title = title + $"({version})",
                            Description =
                                $"**Type:** Approved\n**Creator:** {creator}\n**BPM** {bpm}\n**Max combo:** {max_combo}\n**Difficulty:** {shee}\n**AR:** {diff_approach}\n**Approve date:** {approved_date}",
                           ImageUrl = $"https://b.ppy.sh/thumb/{mlg}l.jpg"
                        };
                        await ReplyAsync($"**Download:** " + $"https://osu.ppy.sh/d/{mlg}", false, xd.Build());
                    }
                    else if (approved.Contains("3"))
                    {
                        EmbedBuilder xd = new EmbedBuilder
                        {
                            Title = title + $"({version})",
                            Description =
                                $"**Type:** qualified\n**Creator:** {creator}\n**BPM** {bpm}\n**Max combo:** {max_combo}\n**Difficulty:** {shee}\n**AR:** {diff_approach}\n**Approve date:** {approved_date}",
                           ImageUrl = $"https://b.ppy.sh/thumb/{mlg}l.jpg"
                        };
                        await ReplyAsync($"**Download:** " + $"https://osu.ppy.sh/d/{mlg}", false, xd.Build());
                    }
                    else if (approved.Contains("4"))
                    {
                        EmbedBuilder xd = new EmbedBuilder
                        {
                            Title = title + $"({version})",
                            Description =
                                $"**Type:** LOVED ❤\n**Creator:** {creator}\n**BPM** {bpm}\n**Max combo:** {max_combo}\n**Difficulty:** {shee}\n**AR:** {diff_approach}\n**Approve date:** {approved_date}",
                           ImageUrl = $"https://b.ppy.sh/thumb/{mlg}l.jpg"
                        };
                        await ReplyAsync($"**Download:** " + $"https://osu.ppy.sh/d/{mlg}", false, xd.Build());
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
