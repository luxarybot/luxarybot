using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Xml;
using System.IO.Compression;
using Luxary.Service;
using Luxary.Services;
using ImageSharp;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Point = ImageSharp.Point;
using Size = System.Drawing.Size;

namespace Luxary
{
    public class Image : ModuleBase<ICommandContext>
    {
        [Command("Dog")]
        [Summary(".dog")]
        [Remarks("Shows a random dog.")]
        [Alias("doggo", "doge")]
        public async Task Dog()
        {
            Console.WriteLine("Making API Call...");
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                string websiteurl = "https://dog.ceo/api/breeds/image/random";
                client.BaseAddress = new Uri(websiteurl);
                HttpResponseMessage response = client.GetAsync("").Result;
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(result);
                var rnd = new Random();
                string DogImage = json["message"].ToString();
                int g1 = rnd.Next(1, 255);
                int g2 = rnd.Next(1, 255);
                int g3 = rnd.Next(1, 255);
                var builder = new EmbedBuilder
                {
                    Color = new Discord.Color(g1, g2, g3),
                    Title = ("Lil doggo's xd"),
                    ImageUrl = $"{DogImage}",
                };
                await ReplyAsync("", false, builder.Build());
            }
        }
        [Command("Bunny")]
        [Summary(".bunny")]
        [Remarks("Shows a random bunny.")]
        [Alias("buns", "booni")]
        public async Task Bunny()
        {
            var rand = new Random();
            int getal = rand.Next(1, 200);
            int Delete = 1;
            foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
            {
                await Item.DeleteAsync();
            }

            var rnd = new Random();
            int g1 = rnd.Next(1, 255);
            int g2 = rnd.Next(1, 255);
            int g3 = rnd.Next(1, 255);
            var builder = new EmbedBuilder()
            {
                Color = new Discord.Color(g1, g2, g3),
            };
            string rimage;
            rimage = $"https://bunnies.media/poster/{getal}.png";
            builder.WithImageUrl(rimage);
            await ReplyAsync("", false, builder.Build());
        }
        [Command("team")]
        [Summary(".team")]
        [Remarks("Shows a nice league team comp.")]
        public async Task team()
        {
            ImageCore core = new ImageCore();

            var champ = new Random();
            int ex1 = champ.Next(1, 127);
            int ex2 = champ.Next(1, 127);
            int ex3 = champ.Next(1, 127);
            int ex4 = champ.Next(1, 127);
            int ex5 = champ.Next(1, 127);
            ImageSharp.Image<Rgba32> champ1 = await core.StartStreamAsync(path: "league/" + ex1 + ".png");
            ImageSharp.Image<Rgba32> champ2 = await core.StartStreamAsync(path: "league/" + ex2 + ".png");
            ImageSharp.Image<Rgba32> champ3 = await core.StartStreamAsync(path: "league/" + ex3 + ".png");
            ImageSharp.Image<Rgba32> champ4 = await core.StartStreamAsync(path: "league/" + ex4 + ".png");
            ImageSharp.Image<Rgba32> champ5 = await core.StartStreamAsync(path: "league/" + ex5 + ".png");
            ImageSharp.Image<Rgba32> l1 = await core.StartStreamAsync(path: "pic/top.png");
            ImageSharp.Image<Rgba32> l2 = await core.StartStreamAsync(path: "pic/jng.png");
            ImageSharp.Image<Rgba32> l3 = await core.StartStreamAsync(path: "pic/mid.png");
            ImageSharp.Image<Rgba32> l4 = await core.StartStreamAsync(path: "pic/bot.png");
            ImageSharp.Image<Rgba32> l5 = await core.StartStreamAsync(path: "pic/sup.png");
            //totale foto groot VVVVV
            ImageSharp.Image<Rgba32> finalImage = new ImageSharp.Image<Rgba32>(500, 1250);
            //size per image VVVVV
            ImageSharp.Size size250 = new ImageSharp.Size(250, 250);

            champ1.Resize(size250);
            champ2.Resize(size250);
            champ3.Resize(size250);
            champ4.Resize(size250);
            champ5.Resize(size250);
            l1.Resize(size250);
            l2.Resize(size250);
            l3.Resize(size250);
            l4.Resize(size250);
            l5.Resize(size250);

            finalImage.DrawImage(champ1, 1f, size250, new Point(0, 0));
            finalImage.DrawImage(champ2, 1f, size250, new Point(0, 250));
            finalImage.DrawImage(champ3, 1f, size250, new Point(0, 500));
            finalImage.DrawImage(champ4, 1f, size250, new Point(0, 750));
            finalImage.DrawImage(champ5, 1f, size250, new Point(0, 1000));
            finalImage.DrawImage(l1, 1f, size250, new Point(250, 0));
            finalImage.DrawImage(l2, 1f, size250, new Point(250, 250));
            finalImage.DrawImage(l3, 1f, size250, new Point(250, 500));
            finalImage.DrawImage(l4, 1f, size250, new Point(250, 750));
            finalImage.DrawImage(l5, 1f, size250, new Point(250, 1000));

            await core.StopStreamAsync(Context.Message, finalImage);
        }
        [Command("rip")]
        [Summary(".rip")]
        [Remarks("Shows a nice league team comp.")]
        public async Task rip(IGuildUser userr = null)
        {
            try
            {
                if (!(userr == null))
                {
                    ImageCore core = new ImageCore();
                    ImageSharp.Image<Rgba32> image = null;
                    HttpClient httpCleint = new HttpClient();
                    HttpResponseMessage response = await httpCleint.GetAsync(userr.GetAvatarUrl());
                    Stream inputStream = await response.Content.ReadAsStreamAsync();
                    image = ImageSharp.Image.Load(inputStream);

                    Stream outStream = new MemoryStream();
                    image.SaveAsPng(outStream);
                    outStream.Position = 0;
                    string input = "abcdefghijklmnopqrstuvwxyz0123456789";
                    char ch;
                    string randomString = "";
                    Random rand = new Random();
                    for (int i = 0; i < 8; i++)
                    {
                        ch = input[rand.Next(0, input.Length)];
                        randomString += ch;
                    }
                    var file = File.Create($"images/{randomString}.png");
                    await outStream.CopyToAsync(file);
                    file.Dispose();

                    ImageSharp.Image<Rgba32> champ1 =
                        await core.StartStreamAsync(path: "pic/xdddd.png");
                    ImageSharp.Image<Rgba32> user =
                        await core.StartStreamAsync(
                            path: $"images/{randomString}.png");
                    //totale foto groot VVVVV
                    ImageSharp.Image<Rgba32> finalImage = new ImageSharp.Image<Rgba32>(277, 340);
                    //size per image VVVVV
                    ImageSharp.Size size250 = new ImageSharp.Size(277, 340);
                    ImageSharp.Size size125 = new ImageSharp.Size(125, 125);

                    champ1.Resize(size250);
                    user.Resize(size125);


                    finalImage.DrawImage(champ1, 1f, size250, new Point(0, 0));
                    finalImage.DrawImage(user, 1f, size125, new Point(67, 160));

                    await core.StopStreamAsync(Context.Message, finalImage);
                    File.Delete($"images/{randomString}.png");
                }
                else
                {
                    ImageCore core = new ImageCore();
                    ImageSharp.Image<Rgba32> image = null;
                    HttpClient httpCleint = new HttpClient();
                    HttpResponseMessage response = await httpCleint.GetAsync(Context.User.GetAvatarUrl());
                    Stream inputStream = await response.Content.ReadAsStreamAsync();
                    image = ImageSharp.Image.Load(inputStream);

                    Stream outStream = new MemoryStream();
                    image.SaveAsPng(outStream);
                    outStream.Position = 0;
                    string input = "abcdefghijklmnopqrstuvwxyz0123456789";
                    char ch;
                    string randomString = "";
                    Random rand = new Random();
                    for (int i = 0; i < 8; i++)
                    {
                        ch = input[rand.Next(0, input.Length)];
                        randomString += ch;
                    }
                    var file = File.Create($"images/{randomString}.png");
                    await outStream.CopyToAsync(file);
                    file.Dispose();

                    ImageSharp.Image<Rgba32> champ1 =
                        await core.StartStreamAsync(path: "pic/xdddd.png");
                    ImageSharp.Image<Rgba32> user =
                        await core.StartStreamAsync(
                            path: $"images/{randomString}.png");
                    //totale foto groot VVVVV
                    ImageSharp.Image<Rgba32> finalImage = new ImageSharp.Image<Rgba32>(277, 340);
                    //size per image VVVVV
                    ImageSharp.Size size250 = new ImageSharp.Size(277, 340);
                    ImageSharp.Size size125 = new ImageSharp.Size(125, 125);

                    champ1.Resize(size250);
                    user.Resize(size125);


                    finalImage.DrawImage(champ1, 1f, size250, new Point(0, 0));
                    finalImage.DrawImage(user, 1f, size125, new Point(67, 160));

                    await core.StopStreamAsync(Context.Message, finalImage);
                    File.Delete($"images/{randomString}.png");
                }
            }
            catch
            {
                ImageCore core = new ImageCore();
                ImageSharp.Image<Rgba32> image = null;
                HttpClient httpCleint = new HttpClient();
                HttpResponseMessage response = await httpCleint.GetAsync(Context.User.GetAvatarUrl());
                Stream inputStream = await response.Content.ReadAsStreamAsync();
                image = ImageSharp.Image.Load(inputStream);

                Stream outStream = new MemoryStream();
                image.SaveAsPng(outStream);
                outStream.Position = 0;
                string input = "abcdefghijklmnopqrstuvwxyz0123456789";
                char ch;
                string randomString = "";
                Random rand = new Random();
                for (int i = 0; i < 8; i++)
                {
                    ch = input[rand.Next(0, input.Length)];
                    randomString += ch;
                }
                var file = File.Create($"images/{randomString}.png");
                await outStream.CopyToAsync(file);
                file.Dispose();

                ImageSharp.Image<Rgba32> champ1 =
                    await core.StartStreamAsync(path: "pic/xdddd.png");
                ImageSharp.Image<Rgba32> user =
                    await core.StartStreamAsync(
                        path: $"pic/boii.png");
                //totale foto groot VVVVV
                ImageSharp.Image<Rgba32> finalImage = new ImageSharp.Image<Rgba32>(277, 340);
                //size per image VVVVV
                ImageSharp.Size size250 = new ImageSharp.Size(277, 340);
                ImageSharp.Size size125 = new ImageSharp.Size(125, 125);

                champ1.Resize(size250);
                user.Resize(size125);


                finalImage.DrawImage(champ1, 1f, size250, new Point(0, 0));
                finalImage.DrawImage(user, 1f, size125, new Point(67, 160));

                await core.StopStreamAsync(Context.Message, finalImage);
                File.Delete($"images/{randomString}.png");
            }
        }
        [Command("flip")]
        [Summary(".flip **<rotation>**")]
        [Remarks("Flips your avatar.")]
        public async Task flip([Remainder] int x)
        {
            ImageSharp.Image<Rgba32> image = null;
            HttpClient httpCleint = new HttpClient();
            HttpResponseMessage response = await httpCleint.GetAsync(Context.User.GetAvatarUrl());
            Stream inputStream = await response.Content.ReadAsStreamAsync();
            image = ImageSharp.Image.Load(inputStream);
            image.Rotate(x);

            Stream outStream = new MemoryStream();
            image.SaveAsPng(outStream);
            outStream.Position = 0;
            string input = "abcdefghijklmnopqrstuvwxyz0123456789";
            char ch;
            string randomString = "";
            Random rand = new Random();
            for (int i = 0; i < 8; i++)
            {
                ch = input[rand.Next(0, input.Length)];
                randomString += ch;
            }
            var file = File.Create($"Images/{randomString}.png");
            await outStream.CopyToAsync(file);
            file.Dispose();
            await Context.Channel.SendFileAsync($"Images/{randomString}.png");
            File.Delete($"Images/{randomString}.png");
        }
        [Command("Cat")]
        [Summary(".cat")]
        [Remarks("Shows a random cat.")]
        public async Task Cat()
        {
            Console.WriteLine("Making API Call...");
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate })) //This acts like a webbrowser
            {
                string websiteurl = "http://random.cat/meow";                           //The API site
                client.BaseAddress = new Uri(websiteurl);                                   //This redirects the code to the API?
                HttpResponseMessage response = client.GetAsync("").Result;   //Then it gets the information
                response.EnsureSuccessStatusCode();                                       //Makes sure that its successfull
                string result = await response.Content.ReadAsStringAsync();     //Gets the full website
                var json = JObject.Parse(result);                                                //Reads the json from the html (?)
                var rnd = new Random();
                string CatImage = json["file"].ToString();                                   //Saves the url to CatImage string
                int g1 = rnd.Next(1, 255);
                int g2 = rnd.Next(1, 255);
                int g3 = rnd.Next(1, 255);
                var builder = new EmbedBuilder()
                {
                    Color = new Discord.Color(g1, g2, g3),
                };
                builder.Title = ("Lil cats xd");
                builder.ImageUrl = $"{CatImage}";
                await ReplyAsync("", false, builder.Build());
            }
        }
        public class Part3 : IEquatable<Part3>
        {
            public string PartName { get; set; }

            public int PartId { get; set; }

            public string PartTitle { get; set; }

            public override string ToString()
            {
                return PartName;
            }
            public override int GetHashCode()
            {
                return PartId;
            }
            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                Part3 objAsPart = obj as Part3;
                if (objAsPart == null) return false;
                else return Equals(objAsPart);
            }
            public bool Equals(Part3 other)
            {
                if (other == null) return false;
                return (this.PartId.Equals(other.PartId));
            }
        }
        
        private static IUserMessage message;
        private int idd = 0;
        //[Command("xdd")]
        //[Alias("xdd")]
        //[Summary(".xdd **<tag>**")]
        //[Remarks("( ͡° ͜ʖ ͡°)")]
        //public async Task BoobsAsyncd([Remainder] string tag)
        //{
        //    for (; ; )
        //    {
        //        var xd = tag.Replace(" ", "_");
        //        string url = $"https://rule34.xxx/index.php?page=dapi&s=post&q=index&tags={xd}";
        //        XmlDocument Doc = new XmlDocument();
        //        Doc.Load(url);
        //        XmlNodeList itemList = Doc.DocumentElement.SelectNodes("post");
        //        List<string> myList = new List<string>();
        //        try
        //        {
        //            foreach (XmlNode currNode in itemList)
        //            {
        //                string date = string.Empty;
        //                date = currNode.Attributes["file_url"].Value;
        //                string modifiedString = date.Replace("//", "http://");
        //                myList.Add(modifiedString);
        //            }
        //        }
        //        finally
        //        {
        //            string input = "abcdefghijklmnopqrstuvwxyz0123456789";
        //            char ch;
        //            string randomString2 = "";
        //            Random rand = new Random();
        //            for (int i = 0; i < 8; i++)
        //            {
        //                ch = input[rand.Next(0, input.Length)];
        //                randomString2 += ch;
        //            }
        //            Random r = new Random();
        //            int index = r.Next(myList.Count);
        //            string randomString = myList[index];
        //            string localFilename = @"D:\Discord\Luxary\Luxary\bin\Debug\xd\" + randomString2 + ".jpg";
        //            using (WebClient client = new WebClient())
        //            {
        //                client.DownloadFile(randomString, localFilename);
        //            }
        //            if (idd == 0)
        //            {
        //                message =
        //                    await ReplyAsync($"**Downloaded 1 image**");
        //                idd++;
        //            }
        //            else if (idd > 0)
        //            {
        //                await message.ModifyAsync(msg =>
        //                    msg.Content =
        //                        ($"**Downloaded: {idd} images**"));
        //                idd++;
        //            }
        //        }
        //    }
        //}
        static int i= 0;
        [Command("rule34")]
        [Alias("r34")]
        [Summary(".r34 **<tag>**")]
        [Remarks("( ͡° ͜ʖ ͡°)")]
        public async Task BoobsAsync([Remainder] string tag)
        {
            {
                var nsfw = Context.Channel.IsNsfw;
                if (!nsfw)
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
                        Description = $"Not a NSFW channel.",
                        ThumbnailUrl =
                            $"https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/pic/shy.png",
                    };
                    await ReplyAsync("", false, builder.Build());
                }
                else
                {
                    try
                    {
                        var xd = tag.Replace(" ", "_");
                        string url = $"https://rule34.xxx/index.php?page=dapi&s=post&q=index&tags={xd}";
                        XmlDocument Doc = new XmlDocument();
                        Doc.Load(url);
                        XmlNodeList itemList = Doc.DocumentElement.SelectNodes("post");
                        List<string> myList = new List<string>();
                        try
                        {
                            foreach (XmlNode currNode in itemList)
                            {
                                string date = string.Empty;
                                date = currNode.Attributes["file_url"].Value;
                                string modifiedString = date.Replace("//", "http://");
                                myList.Add(modifiedString);
                            }
                        }
                        finally
                        {
                            string input = "abcdefghijklmnopqrstuvwxyz0123456789";
                            char ch;
                            string randomString2 = "";
                            Random rand = new Random();
                            for (int i = 0; i < 8; i++)
                            {
                                ch = input[rand.Next(0, input.Length)];
                                randomString2 += ch;
                            }
                            Random r = new Random();
                            int index = r.Next(myList.Count);
                            string randomString = myList[index];
                            string localFilename = @"D:\Discord\Luxary\Luxary\bin\Debug\xd\"+randomString2+".jpg";
                            using (WebClient client = new WebClient())
                            {
                                client.DownloadFile(randomString, localFilename);
                            }
                            await Context.Channel.SendFileAsync(localFilename);
                            myList.Clear();
                        }
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
                            Description = $"Tag not found.",
                            ThumbnailUrl =
                                $"https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/pic/shy.png",
                        };
                        await ReplyAsync("", false, builder.Build());
                        Console.WriteLine(ee);
                    }
                }
            }
        }
        static int i2 = 0;
        [Command("img")]
        [Alias("img")]
        [Summary(".img **<tag>**")]
        [Remarks("( ͡° ͜ʖ ͡°)")]
        public async Task weebsAsync([Remainder] string tag)
        {
            var nsfw = Context.Channel.IsNsfw;
            if (!nsfw)
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
                    Description = $"Not a NSFW channel.",
                    ThumbnailUrl =
                        $"https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/pic/shy.png",
                };
                await ReplyAsync("", false, builder.Build());
            }
            else
            try
            {
                var xd = tag.Replace(" ", "_");
                string url = $"https://konachan.com/post.xml?tags={xd}&limit=100";
                XmlDocument Doc = new XmlDocument();
                Doc.Load(url);
                XmlNodeList itemList = Doc.DocumentElement.SelectNodes("post");
                List<string> myList = new List<string>();
                try
                {
                    foreach (XmlNode currNode in itemList)
                    {
                        string date = string.Empty;
                        date = currNode.Attributes["jpeg_url"].Value;
                        myList.Add(date);
                    }
                }
                finally
                {                   
                    string input = "abcdefghijklmnopqrstuvwxyz0123456789";
                    char ch;
                    string randomString2 = "";
                    Random rand = new Random();
                    for (int i2 = 0; i2 < 8; i2++)
                    {
                        ch = input[rand.Next(0, input.Length)];
                        randomString2 += ch;
                    }
                    Random r = new Random();
                    int index = r.Next(myList.Count);
                    string randomString = myList[index];
                    
                    string localFilename = @"D:\Discord\Luxary\Luxary\bin\Debug\xd\" + randomString2 + ".jpg";
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(randomString, localFilename);
                        await Context.Channel.SendFileAsync(localFilename);
                        myList.Clear();
                        client.Dispose();                       
                    }                   
                }
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
                    Description = $"Tag not found.",
                    ThumbnailUrl =
                        $"https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/pic/shy.png",
                };
                await ReplyAsync("", false, builder.Build());
                Console.WriteLine(ee);
            }
        }

        public class Part2 : IEquatable<Part2>
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
                Part2 objAsPart = obj as Part2;
                if (objAsPart == null) return false;
                else return Equals(objAsPart);
            }
            public bool Equals(Part2 other)
            {
                if (other == null) return false;
                return (this.PartId.Equals(other.PartId));
            }
        }

        public class Item
        {
            public int millis;
            public string stamp;
            public DateTime datetime;
            public string light;
            public float temp;
            public float vcc;
        }
        private static List<Part2> parts2 = new List<Part2>();
        [Command("mastery")]
        [Alias("mas")]
        [Summary(".mastery **<name>**")]
        [Remarks("Shows your summoner mastery information")]
        public async Task Mastery([Remainder] string tag)
        {
            parts2.Clear();
            using (var client = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            }))
            {
                try
                {
                    StreamReader sr = new StreamReader("riotkey.txt");
                    string key = sr.ReadLine();
                    HttpWebRequest httpWebRequest =
                    WebRequest.Create($"https://euw1.api.riotgames.com/lol/summoner/v3/summoners/by-name/{tag}?api_key={key}") as HttpWebRequest;
                    httpWebRequest.Method = "GET";
                    httpWebRequest.ContentType = "application/json";                  
                    HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
                    Encoding ascii = Encoding.ASCII;
                    Stream responseStream = response.GetResponseStream();
                    Encoding encoding = ascii;
                    using (StreamReader streamReader = new StreamReader(responseStream, encoding))
                    {
                        string all = "";
                        string end = streamReader.ReadToEnd();
                        var jsonn = (JObject) JsonConvert.DeserializeObject(end);
                        var id = jsonn["id"].ToString();
                        var Name = jsonn["name"].ToString();
                        var icon = jsonn["profileIconId"].ToString();
                        try
                        {
                            var auth = new EmbedAuthorBuilder()
                            {
                                Name = $"{Name}'s mastery",
                            };
                            StreamReader vr = new StreamReader("version.txt");
                            string version = vr.ReadLine();


                            var master = new EmbedBuilder
                            {
                                Author = auth,
                                ThumbnailUrl =
                                    $"http://ddragon.leagueoflegends.com/cdn/{version}/img/profileicon/{icon}.png"
                            };
                            using (var keee = new HttpClient(new HttpClientHandler
                            {
                                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                            }))
                            {

                                string Mastery =
                                    $"https://euw1.api.riotgames.com/lol/champion-mastery/v3/champion-masteries/by-summoner/{id}?api_key={key}";
                                keee.BaseAddress = new Uri(Mastery);
                                HttpResponseMessage yeboi = keee.GetAsync("").Result;
                                string result3 = await yeboi.Content.ReadAsStringAsync();
                                var json3 = JArray.Parse(result3);
                                for (int xd = 0; xd <= 9; xd++)
                                {
                                    using (var keee1 = new HttpClient(new HttpClientHandler
                                    {
                                        AutomaticDecompression =
                                            DecompressionMethods.GZip | DecompressionMethods.Deflate
                                    }))
                                    {
                                        var CID = json3[xd]["championId"].ToString();
                                        var CPT = json3[xd]["championPoints"].ToString();
                                        var CLVL = json3[xd]["championLevel"].ToString();
                                        using (FileStream fs = new FileStream(@"D:\Discord\Luxary\Luxary\bin\Debug\champs.txt", FileMode.Open,
                                            FileAccess.Read))
                                        using (StreamReader srr = new StreamReader(fs))
                                        using (JsonTextReader reader = new JsonTextReader(srr))
                                        {
                                            while (reader.Read())
                                            {
                                                if (reader.TokenType == JsonToken.StartObject)
                                                {
                                                    JObject json31 = JObject.Load(reader);
                                                    var name = json31["data"][CID]["name"].ToString();
                                                    if (CLVL.Contains("7"))
                                                    {
                                                        CLVL = "<:m7:375555291108081664>";
                                                    }
                                                    else if (CLVL.Contains("6"))
                                                    {
                                                        CLVL = "<:m6:375555126880108545>";
                                                    }
                                                    else if (CLVL.Contains("5"))
                                                    {
                                                        CLVL = "<:m5:375555589310513153>";
                                                    }
                                                    else if (CLVL.Contains("4"))
                                                    {
                                                        CLVL = "<:m4:375623810105344020>";
                                                    }
                                                    else if (CLVL.Contains("3"))
                                                    {
                                                        CLVL = "<:m3:375623860462157825>";
                                                    }
                                                    else if (CLVL.Contains("2"))
                                                    {
                                                        CLVL = "<:m2:375624600626790400>";
                                                    }
                                                    else
                                                    {
                                                        CLVL = "<:m1:375624581114888193>";
                                                    }
                                                    parts2.Add(new Part2()
                                                    {
                                                        PartName = name,
                                                        PartTitle = CPT,
                                                        PartMas = CLVL
                                                    });
                                                }
                                            }


                                        }
                                    }
                                }
                                foreach (Part2 songs in parts2)
                                {
                                    master.Description +=
                                        $"{songs.PartMas}. **{songs.PartName}** ``-`` {songs.PartTitle}\n";
                                }
                                await ReplyAsync("", false, master.Build());
                            }
                        }
                        catch (Exception c)
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
                                Description = $"Data not available\nError 429",
                                ThumbnailUrl =
                                    $"https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/pic/mad.png",
                            };
                            await ReplyAsync("", false, builder.Build());
                            Console.WriteLine(c);
                        }
                    }
                }
                catch (Exception e)
                {
                    var auth = new EmbedAuthorBuilder()
                    {
                        Name = $"Not found"
                    };
                    var rnd = new Random();
                    int g1 = rnd.Next(1, 255);
                    int g2 = rnd.Next(1, 255);
                    int g3 = rnd.Next(1, 255);
                    var builder = new EmbedBuilder
                    {
                        Color = new Discord.Color(g1, g2, g3),
                        Author = auth,
                        Description = $"Username not found",
                        ThumbnailUrl =
                            $"https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/pic/oh.png",
                    };
                    await ReplyAsync("", false, builder.Build());
                    Console.WriteLine(e);
                }
            }
        }
        [Command("summoner")]
        [Alias("sum")]
        [Summary(".summoner **<name>**")]
        [Remarks("Shows your summoner information")]
        public async Task Summoner([Remainder]string tag)
        {
            using (var client = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            }))
            {
                try
                {
                    StreamReader sr = new StreamReader("riotkey.txt");
                    string key = sr.ReadLine();
                    string Summonerid =
                        $"https://euw1.api.riotgames.com/lol/summoner/v3/summoners/by-name/{tag}?api_key={key}";
                    client.BaseAddress = new Uri(Summonerid);
                    HttpResponseMessage response = client.GetAsync("").Result;
                    string result = await response.Content.ReadAsStringAsync();
                    var jsonn = JObject.Parse(result);
                    string id = jsonn["id"].ToString();
                    string Name = jsonn["name"].ToString();
                    string lvl = jsonn["summonerLevel"].ToString();
                    string icon = jsonn["profileIconId"].ToString();
                    using (var ke = new HttpClient(new HttpClientHandler
                    {
                        AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                    }))
                    {
                        try
                        {
                            string Elo =
                                $"https://euw1.api.riotgames.com/lol/league/v3/positions/by-summoner/{id}?api_key={key}";
                            ke.BaseAddress = new Uri(Elo);
                            HttpResponseMessage kee = ke.GetAsync("").Result;
                            string result2 = await kee.Content.ReadAsStringAsync();
                            var json = JArray.Parse(result2);
                            using (var keee = new HttpClient(new HttpClientHandler
                            {
                                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                            }))
                            {
                                string Mastery =
                                    $"https://euw1.api.riotgames.com/lol/champion-mastery/v3/champion-masteries/by-summoner/{id}?api_key={key}";
                                keee.BaseAddress = new Uri(Mastery);
                                HttpResponseMessage yeboi = keee.GetAsync("").Result;
                                string result3 = await yeboi.Content.ReadAsStringAsync();
                                var json3 = JArray.Parse(result3);
                                var CID = json3[0]["championId"].ToString();
                                var CLVL = json3[0]["championLevel"].ToString();
                                var CPT = json3[0]["championPoints"].ToString();
                                using (FileStream fs = new FileStream(@"D:\Discord\Luxary\Luxary\bin\Debug\champs.txt", FileMode.Open,
                                    FileAccess.Read))
                                using (StreamReader srr = new StreamReader(fs))
                                using (JsonTextReader reader = new JsonTextReader(srr))
                                {
                                    while (reader.Read())
                                    {
                                        if (reader.TokenType == JsonToken.StartObject)
                                        {
                                            JObject json31 = JObject.Load(reader);
                                            var name = json31["data"][CID]["name"].ToString();
                                            var title = json31["data"][CID]["title"].ToString();
                                            var auth = new EmbedAuthorBuilder()
                                            {
                                                Name = $"{Name}'s profile",
                                            };
                                            StreamReader vr = new StreamReader("version.txt");
                                            string version = vr.ReadLine();
                                            if (CLVL.Contains("7"))
                                            {
                                                CLVL = "<:m7:375555291108081664>";
                                            }
                                            else if (CLVL.Contains("6"))
                                            {
                                                CLVL = "<:m6:375555126880108545>";
                                            }
                                            else if (CLVL.Contains("5"))
                                            {
                                                CLVL = "<:m5:375555589310513153>";
                                            }
                                            else if (CLVL.Contains("4"))
                                            {
                                                CLVL = "<:m4:375623810105344020>";
                                            }
                                            else if (CLVL.Contains("3"))
                                            {
                                                CLVL = "<:m3:375623860462157825>";
                                            }
                                            else if (CLVL.Contains("2"))
                                            {
                                                CLVL = "<:m2:375624600626790400>";
                                            }
                                            else
                                            {
                                                CLVL = "<:m1:375624581114888193>";
                                            }
                                            var builder = new EmbedBuilder
                                            {
                                                Color = new Discord.Color(218, 165, 32),
                                                Author = auth,
                                                Description = $"Level: {lvl}"
                                            };
                                            builder.AddField(x =>
                                            {
                                                x.Name = "Main";
                                                x.Value =
                                                    $"Champion: **{name}, {title}**\nMastery: **{CLVL}** **{CPT}**";
                                                x.IsInline = false;
                                            });
                                            builder.ThumbnailUrl =
                                                $"http://ddragon.leagueoflegends.com/cdn/{version}/img/profileicon/{icon}.png";
                                            int i = 0;
                                            builder.AddField(x =>
                                            {
                                                x.Name = "Ranked Stats";
                                                foreach (var lh in json)
                                                {

                                                    string tier = json[i]["tier"].ToString();
                                                    string tier2 = json[i]["rank"].ToString();
                                                    string check = json[i]["playerOrTeamName"].ToString();
                                                    string wins = json[i]["wins"].ToString();
                                                    string lp = json[i]["leaguePoints"].ToString();
                                                    string league = json[i]["leagueName"].ToString();
                                                    string queueType = json[i]["queueType"].ToString();
                                                    string Losses = json[i]["losses"].ToString();
                                                    int resultt = int.Parse(wins);
                                                    int resultt2 = int.Parse(Losses);
                                                    int totalFilesToProcess = resultt + resultt2;
                                                    int winrate = resultt * 100 / totalFilesToProcess;
                                                    if (queueType == "RANKED_SOLO_5x5")
                                                    {
                                                        queueType = "Solo/Duo";
                                                    }

                                                    x.Value +=
                                                        $"**{queueType}:** {tier} {tier2} - **{lp}LP** {wins}W {Losses}L / Winrate: {winrate}%";
                                                    i = i + 1;
                                                    x.IsInline = true;
                                                }
                                            });
                                            await ReplyAsync("", false, builder.Build());
                                        }
                                    }                                   
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            using (var keee = new HttpClient(new HttpClientHandler
                            {
                                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                            }))
                            {
                                string Mastery =
                                    $"https://euw1.api.riotgames.com/lol/champion-mastery/v3/champion-masteries/by-summoner/{id}?api_key={key}";
                                keee.BaseAddress = new Uri(Mastery);
                                HttpResponseMessage yeboi = keee.GetAsync("").Result;
                                string result3 = await yeboi.Content.ReadAsStringAsync();
                                var json3 = JArray.Parse(result3);
                                var CID = json3[0]["championId"].ToString();
                                var CLVL = json3[0]["championLevel"].ToString();
                                var CPT = json3[0]["championPoints"].ToString();
                                using (FileStream fs = new FileStream(@"D:\Discord\Luxary\Luxary\bin\Debug\champs.txt", FileMode.Open,
                                    FileAccess.Read))
                                using (StreamReader srr = new StreamReader(fs))
                                using (JsonTextReader reader = new JsonTextReader(srr))
                                {
                                    while (reader.Read())
                                    {
                                        if (reader.TokenType == JsonToken.StartObject)
                                        {
                                            JObject json31 = JObject.Load(reader);
                                            var name = json31["data"][CID]["name"].ToString();
                                            var title = json31["data"][CID]["title"].ToString();
                                            var auth = new EmbedAuthorBuilder()
                                            {
                                                Name = $"{Name}'s profile",
                                            };
                                            StreamReader vr = new StreamReader("version.txt");
                                            string version = vr.ReadLine();
                                            if (CLVL.Contains("7"))
                                            {
                                                CLVL = "<:m7:375555291108081664>";
                                            }
                                            else if (CLVL.Contains("6"))
                                            {
                                                CLVL = "<:m6:375555126880108545>";
                                            }
                                            else if (CLVL.Contains("5"))
                                            {
                                                CLVL = "<:m5:375555589310513153>";
                                            }
                                            else if (CLVL.Contains("4"))
                                            {
                                                CLVL = "<:m4:375623810105344020>";
                                            }
                                            else if (CLVL.Contains("3"))
                                            {
                                                CLVL = "<:m3:375623860462157825>";
                                            }
                                            else if (CLVL.Contains("2"))
                                            {
                                                CLVL = "<:m2:375624600626790400>";
                                            }
                                            else
                                            {
                                                CLVL = "<:m1:375624581114888193>";
                                            }
                                            var builder = new EmbedBuilder
                                            {
                                                Color = new Discord.Color(218, 165, 32),
                                                Author = auth,
                                                Description = $"Level: {lvl}"
                                            };
                                            builder.AddField(x =>
                                            {
                                                x.Name = "Main";
                                                x.Value =
                                                    $"Champion: **{name}, {title}**\nMastery: **{CLVL}** **{CPT}**";
                                                x.IsInline = true;


                                            });
                                            builder.ThumbnailUrl =
                                                $"http://ddragon.leagueoflegends.com/cdn/{version}/img/profileicon/{icon}.png";
                                            int i = 0;
                                            builder.AddField(x =>
                                            {
                                                x.Name = "Ranked Stats";
                                                x.Value +=
                                                    $"**Unranked**";
                                                x.IsInline = true;
                            
                                            });
                                            
                                            await ReplyAsync("", false, builder.Build());
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                    var auth = new EmbedAuthorBuilder()
                    {
                        Name = $"Not found",
                    };
                    var rnd = new Random();
                    int g1 = rnd.Next(1, 255);
                    int g2 = rnd.Next(1, 255);
                    int g3 = rnd.Next(1, 255);
                    var builder = new EmbedBuilder
                    {
                        Color = new Discord.Color(g1, g2, g3),
                        Author = auth,
                        Description = $"Username not found",
                        ThumbnailUrl =
                            $"https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/pic/oh.png",
                    };
                    await ReplyAsync("", false, builder.Build());
                }
            }
        }
        [Command("NickG")]
        [Alias("nick", "n")]
        [Summary(".NickG")]
        [Remarks("By far the biggest fgt I know.")]
        public async Task nick()
        {
            int Delete = 1;
            foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
            {
                await Item.DeleteAsync();
            }
            var rnd = new Random();
            int g1 = rnd.Next(1, 255);
            int g2 = rnd.Next(1, 255);
            int g3 = rnd.Next(1, 255);
            var embed = new EmbedBuilder()
            {
                Color = new Discord.Color(g1, g2, g3),
            };
            embed.Title = "Nick choking on that sweet banana.";
            embed.ImageUrl = $"https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/pic/monkey.jpg";
            await ReplyAsync("", false, embed.Build());
        }
        static int xd = 1;
        [Command("lol")]
        [Summary(".lol")]
        [Remarks("ECKSDEEEE")]
        public async Task lol()
        {
            
            int Delete = 1;
            foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
            {
                await Item.DeleteAsync();
            }
            var rnd = new Random();
            int g1 = rnd.Next(1, 255);
            int g2 = rnd.Next(1, 255);
            int g3 = rnd.Next(1, 255);
            var embed = new EmbedBuilder()
            {
                Color = new Discord.Color(g1, g2, g3),
            };
            embed.Title = "Ayylmao";
            if(xd == 1)
            {
                embed.ImageUrl =
                    $"https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/pic/lol.jpg";
                xd = 0;
            }
            else
            {
                embed.ImageUrl =
                    $"https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/pic/lol2.jpg";
                xd = 1;
            }
            await ReplyAsync("", false, embed.Build());
        }
        static string live = "offline";
        private static System.Timers.Timer timer2;
        [Command("cover")]
        [Alias("gyb", "thistakessolongtotypesoyoubetterbefastattyping")]
        [Summary(".cover")]
        [Remarks("Covers some content.")]
        [RequireBotPermission(GuildPermission.ManageMessages)]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        public async Task Cover()
        {
            if (live == "online")
            {
                await ReplyAsync($"This command can be used every 10 sec");
            }
            else
            {
                live = "online";
                await CoverUp();
                var seconds = 10000;
                timer2 = new System.Timers.Timer
                {
                    Enabled = true,
                    Interval = (seconds),
                    AutoReset = false
                };
                timer2.Start();
                timer2.Elapsed += Elapsed2;
            }
        }

        public async void Elapsed2(Object source, System.Timers.ElapsedEventArgs e)
        {
            live = "offline";
            await ReplyAsync($".gyb is back online");
        }
        public async Task CoverUp()
        {
            await Context.Channel.SendFileAsync("pic/blank.png");
            await Context.Channel.SendFileAsync("pic/blank.png");
            await Context.Channel.SendFileAsync("pic/blank.png");
            await Context.Channel.SendFileAsync("pic/blank.png");
            await ReplyAsync("Covered some weird shit for " + Context.User.Mention + " your welcome");
        }

        static string width2;
        static string height2;
        [Command("wallpaper")]
        [Alias("background", "wp", "bg")]
        [Summary(".wallpaper **<tag>**")]
        [Remarks("Shows a wallpaper")]
        public async Task Wallpaper([Remainder]string tag)
        {
            try
            {
                List<string> myList = new List<string>();
                using (var client = new HttpClient(new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                }))
                {
                    var websiteurl = $"https://wall.alphacoders.com/api2.0/get.php?auth=140c472dacc84cf043a7384eebf5282d&method=search&term={tag}";
                    client.BaseAddress = new Uri(websiteurl);
                    var response = client.GetAsync("").Result;
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(result);
                    var build = new EmbedBuilder();
                    
                    JArray items = (JArray)json["wallpapers"];
                    for (int i = 0; i < items.Count; i++)
                    {
                        var width = json["wallpapers"][i]["width"].ToString();
                        var height = json["wallpapers"][i]["height"].ToString();
                        var joke = json["wallpapers"][i]["url_image"].ToString();
                        myList.Add(joke);
                        build.Title = $"{tag} wallpaper: {width}x{height}";
                    }
                    Random r = new Random();
                    int index = r.Next(myList.Count);
                    string randomString = myList[index];      
                    build.ImageUrl = randomString;
                    build.WithImageUrl(randomString);
                    await ReplyAsync("", false, build.Build());
                    myList.Clear();
                }
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
                var builder = new EmbedBuilder()
                {
                    Color = new Discord.Color(g1, g2, g3),
                    Author = auth,
                };
                builder.Description = $"Wallpaper not found.";
                builder.ThumbnailUrl =
                    $"https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/pic/oh.png";
                await ReplyAsync("", false, builder.Build());
                Console.WriteLine(ee);
            }
        }

        [Command("slav")]
        [Alias("cyka", "blyat")]
        [Summary(".slav")]
        [Remarks("Shows a slav.")]
        public async Task slav()
        {
            int Delete = 1;
            foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
            {
                await Item.DeleteAsync();
            }
            var rnd = new Random();
            int g1 = rnd.Next(1, 255);
            int g2 = rnd.Next(1, 255);
            int g3 = rnd.Next(1, 255);
            int id = rnd.Next(1, 4);
            var embed = new EmbedBuilder()
            {
                Color = new Discord.Color(g1, g2, g3),
            };
            embed.Title = "przen. przenośnie wulg. wulgarnie pogard. pogardliwie człowiek postępujący w sposób podły lub niemoralny, łajdak, nikczemnik, sprzedający się";
            embed.ImageUrl = $"https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/pic/slav{id}.jpg";
            await ReplyAsync("", false, embed.Build());
        }
        [Command("bored")]
        [Alias("kms")]
        [Summary(".bored")]
        [Remarks("Shows a bored gif.")]
        public async Task bored()
        {
            int Delete = 1;
            foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
            {
                await Item.DeleteAsync();
            }
            var rnd = new Random();
            int g1 = rnd.Next(1, 255);
            int g2 = rnd.Next(1, 255);
            int g3 = rnd.Next(1, 255);
            var embed = new EmbedBuilder()
            {
                Color = new Discord.Color(g1, g2, g3),
            };
            embed.Title = "...";
            embed.ImageUrl = $"https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/pic/bored.gif";
            await ReplyAsync("", false, embed.Build());
        }
        [Command("champion")]
        [Summary(".champ")]
        [Alias("champ")]
        [Remarks("Shows a random champion.")]
        public async Task champ()
        {
            var champ = new Random();
            int ex = champ.Next(1, 127);

            var embed = new EmbedBuilder
            {
                Title = "This wil be your champ",
                ImageUrl =
                    $"https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/league/{ex}.png"
            };
            await ReplyAsync("", false, embed.Build());
        }
        [Command("kys")]
        [Summary(".kys")]
        [Remarks("Enough said.")]
        public async Task kys()
        {
            int Delete = 1;
            foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
            {
                await Item.DeleteAsync();
            }
            var rnd = new Random();
            int g1 = rnd.Next(1, 255);
            int g2 = rnd.Next(1, 255);
            int g3 = rnd.Next(1, 255);
            var embed = new EmbedBuilder()
            {
                Color = new Discord.Color(g1, g2, g3),
            };
            embed.Title = "kys";
            embed.ImageUrl = $"https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/pic/kys.png";
            await ReplyAsync("", false, embed.Build());
        }
        [Command("avatar")]
        [Alias("ava")]
        [Summary(".avatar **<user>**")]
        [Remarks("Steals an avatar from a user.")]
        public async Task ava(IGuildUser user = null)
        {
            var application = await Context.Client.GetApplicationInfoAsync();
            var thumbnailurl = user.GetAvatarUrl();
            var auth = new EmbedAuthorBuilder()
            {
                Name = user.Username,
                IconUrl = thumbnailurl,
            };
            var rnd = new Random();
            int g1 = rnd.Next(1, 255);
            int g2 = rnd.Next(1, 255);
            int g3 = rnd.Next(1, 255);
            if (thumbnailurl != null)
            {
                var embed = new EmbedBuilder()
                {
                    Color = new Discord.Color(g1, g2, g3),
                    Title = $"Stealing {auth.Name}'s avatar....",
                    ImageUrl = $"{auth.IconUrl}"
                };
                await ReplyAsync("", false, embed.Build());
            }
            else
            {
                await ReplyAsync($"{auth.Name} has no avatar.");
            }
        }
        [Command("joke")]
        [Summary("joke")]
        [Remarks("Shows a random shit joke.")]
        public async Task Joke()
        {
            Console.WriteLine("Generating shit joke....");
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                string websiteurl = "http://api.icndb.com/jokes/random/joke";
                client.BaseAddress = new Uri(websiteurl);
                HttpResponseMessage response = client.GetAsync("joke").Result;
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(result);
                var rnd = new Random();
                int g1 = rnd.Next(1, 255);
                int g2 = rnd.Next(1, 255);
                int g3 = rnd.Next(1, 255);
                string Joke = json["value"]["joke"].ToString();
                var builder = new EmbedBuilder()
                {
                    Color = new Discord.Color(g1, g2, g3),
                    Title = $"{Joke}"
                };

                await ReplyAsync("", false, builder.Build());
            }
        }
        [Command("gif")]
        [Summary(".gif **<message>**")]
        [Remarks("Shows a gif.")]
        public async Task gif([Remainder] string tag = null)
        {
            try
            {
                var xd = tag.Replace(" ", "_");
                List<string> myList = new List<string>();
                int Delete = 1;
                foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
                {
                    await Item.DeleteAsync();
                }
                Console.WriteLine("Making API Call...");
                using (var client = new HttpClient(new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                }))
                {
                    var websiteurl = $"https://api.tenor.co/v1/search?tag={tag}&key=0V4RTXJOKKRQ";
                    client.BaseAddress = new Uri(websiteurl);
                    var response = client.GetAsync("").Result;
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(result);
                    JArray items = (JArray) json["results"];
                    for (int i = 0; i < items.Count; i++)
                    {
                        var joke = json["results"][i]["media"][0]["gif"]["url"].ToString();
                        myList.Add(joke);
                    }
                    Random r = new Random();
                    int index = r.Next(myList.Count);
                    string randomString = myList[index];
                    var build = new EmbedBuilder
                    {
                        Title = xd,
                        ImageUrl = randomString
                    };
                    await ReplyAsync("", false, build.Build());
                }
            }
            catch
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
                builder.Description = $"Gif not found.";
                builder.ThumbnailUrl =
                    $"https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/pic/oh.png";
                await ReplyAsync("", false, builder.Build());
            }
        }
        [Command("ps")]
        [Summary(".giphy **<message>**")]
        [Remarks("Shows a gif.")]
        public async Task Ps([Remainder] string url)
        {
            try
            {
                HttpWebRequest request =
                WebRequest.Create($"http://images.shrinktheweb.com/xino.php?stwembed=1&stwaccesskeyid=11628f627e26928&stwurl={url}") as HttpWebRequest;
                using (Stream stream = request.GetResponse().GetResponseStream())
                {
                    System.Drawing.Image img =
                    System.Drawing.Image.FromStream(stream ?? throw new InvalidOperationException());
                    img.Save("pic/myImage.Jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    await Context.Channel.SendFileAsync("pic/myImage.Jpeg");
                    img.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }          
        }
        [Command("weeb")]
        [Summary(".weeb")]
        [Remarks("Sends a weeb gif")]
        public async Task weeb()
        {
            Console.WriteLine("Sending weebstuff >_< desu desu..");
            var build = new EmbedBuilder
            {
                Title = "desu desu",
                ImageUrl = "https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/pic/weeb.gif"
            };
            await ReplyAsync("", false, build.Build());
        }
    }
}
