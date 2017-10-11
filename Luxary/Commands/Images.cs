using System;
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
using ImageSharp;
using System.IO;

namespace Luxary
{
    public class Image : ModuleBase<ICommandContext>
    {
        private CommandService _service;
        [Command("image")]
        [Alias("img")]
        public async Task image(string xd = null)
        {
            {
               
            }
        }
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
                var builder = new EmbedBuilder()
                {
                    Color = new Discord.Color(g1, g2, g3),
                };
                builder.Title = ("Lil doggo's xd");
                builder.ImageUrl = $"{DogImage}";
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
            ImageSharp.Image<Rgba32> champ1 = await core.StartStreamAsync(path: "D:/Discord/Luxary/Luxary/bin/Debug/league/" + ex1 + ".png");
            ImageSharp.Image<Rgba32> champ2 = await core.StartStreamAsync(path: "D:/Discord/Luxary/Luxary/bin/Debug/league/" + ex2 + ".png");
            ImageSharp.Image<Rgba32> champ3 = await core.StartStreamAsync(path: "D:/Discord/Luxary/Luxary/bin/Debug/league/" + ex3 + ".png");
            ImageSharp.Image<Rgba32> champ4 = await core.StartStreamAsync(path: "D:/Discord/Luxary/Luxary/bin/Debug/league/" + ex4 + ".png");
            ImageSharp.Image<Rgba32> champ5 = await core.StartStreamAsync(path: "D:/Discord/Luxary/Luxary/bin/Debug/league/" + ex5 + ".png");
            ImageSharp.Image<Rgba32> l1 = await core.StartStreamAsync(path: "D:/Discord/Luxary/Luxary/bin/Debug/pic/top.png");
            ImageSharp.Image<Rgba32> l2 = await core.StartStreamAsync(path: "D:/Discord/Luxary/Luxary/bin/Debug/pic/jng.png");
            ImageSharp.Image<Rgba32> l3 = await core.StartStreamAsync(path: "D:/Discord/Luxary/Luxary/bin/Debug/pic/mid.png");
            ImageSharp.Image<Rgba32> l4 = await core.StartStreamAsync(path: "D:/Discord/Luxary/Luxary/bin/Debug/pic/bot.png");
            ImageSharp.Image<Rgba32> l5 = await core.StartStreamAsync(path: "D:/Discord/Luxary/Luxary/bin/Debug/pic/sup.png");
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
                        await core.StartStreamAsync(path: "D:/Discord/Luxary/Luxary/bin/Debug/pic/xdddd.png");
                    ImageSharp.Image<Rgba32> user =
                        await core.StartStreamAsync(
                            path: $"D:/Discord/Luxary/Luxary/bin/Debug/images/{randomString}.png");
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
                        await core.StartStreamAsync(path: "D:/Discord/Luxary/Luxary/bin/Debug/pic/xdddd.png");
                    ImageSharp.Image<Rgba32> user =
                        await core.StartStreamAsync(
                            path: $"D:/Discord/Luxary/Luxary/bin/Debug/images/{randomString}.png");
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
                    await core.StartStreamAsync(path: "D:/Discord/Luxary/Luxary/bin/Debug/pic/xdddd.png");
                ImageSharp.Image<Rgba32> user =
                    await core.StartStreamAsync(
                        path: $"D:/Discord/Luxary/Luxary/bin/Debug/pic/boii.png");
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
        [Command("nudes")]
        [Alias("boobs", "rack")]
        [Summary(".slav")]
        [Remarks("Fetches some sexy titties")]
        public async Task BoobsAsync([Remainder]string tag)
        {
            int Delete = 1;
            foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
            {
                await Item.DeleteAsync();
            }
            var GuildUser = await Context.Guild.GetUserAsync(Context.User.Id);
            if (!(GuildUser.Id == 185402901236154368))
            {
                await Context.Channel.SendMessageAsync("lel no");
            }
            else
            {
                Console.WriteLine("Making API Call...");
                XmlDocument doc1 = new XmlDocument();
                doc1.Load($"https://rule34.xxx/index.php?page=dapi&s=post&q=index&tags={tag}");
                XmlElement root = doc1.DocumentElement;
                XmlNodeList nodes = root.SelectNodes("/posts/post");

                foreach (XmlNode node in nodes)
                {
                    string tempf = node["@[file_url]"].InnerText;
                    var rnd = new Random();
                    int g1 = rnd.Next(1, 255);
                    int g2 = rnd.Next(1, 255);
                    int g3 = rnd.Next(1, 255);
                    var builder = new EmbedBuilder()
                    {
                        Color = new Discord.Color(g1, g2, g3),
                    };
                    builder.Title = ("Lil doggo's xd");
                    builder.ImageUrl = $"{tempf}";
                    await ReplyAsync("", false, builder.Build());
                }
            }
        }  
        [Command("summoner")]
        [Alias("sum")]
        [Summary(".summoner **<name>**")]
        [Remarks("Shows your summoner information")]
        public async Task Summoner([Remainder]string tag)
        {
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                try
                {
                    string Summonerid = $"https://euw1.api.riotgames.com/lol/summoner/v3/summoners/by-name/{tag}?api_key=RGAPI-9f5b8ac8-b4fe-4c20-94b7-233807f05c23";
                    client.BaseAddress = new Uri(Summonerid);
                    HttpResponseMessage response = client.GetAsync("").Result;
                    response.EnsureSuccessStatusCode();

                    string result = await response.Content.ReadAsStringAsync();
                    var jsonn = JObject.Parse(result);
                    string id = jsonn["id"].ToString();
                    string Name = jsonn["name"].ToString();
                    string lvl = jsonn["summonerLevel"].ToString();
                    string icon = jsonn["profileIconId"].ToString();

                    var ke = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });
                    try
                    {
                        string Elo = $"https://euw1.api.riotgames.com/lol/league/v3/positions/by-summoner/{id}?api_key=RGAPI-9f5b8ac8-b4fe-4c20-94b7-233807f05c23";
                        ke.BaseAddress = new Uri(Elo);
                        HttpResponseMessage kee = ke.GetAsync("").Result;
                        kee.EnsureSuccessStatusCode();
                        string result2 = await kee.Content.ReadAsStringAsync();
                        var json = JArray.Parse(result2);
                        string tier = json[0]["tier"].ToString();
                        string tier2 = json[0]["rank"].ToString();
                        string check = json[0]["playerOrTeamName"].ToString();
                        string wins = json[0]["wins"].ToString();
                        string Losses = json[0]["losses"].ToString();
                        int resultt = int.Parse(wins);
                        int resultt2 = int.Parse(Losses);

                        int TotalFilesToProcess = resultt + resultt2;
                        int winrate = resultt * 100 / TotalFilesToProcess;
                        var auth = new EmbedAuthorBuilder()
                        {
                            Name = $"{Name}'s profile",
                        };
                        string version = "7.19.1";
                        if (tier == "BRONZE")
                        {
                            var builder = new EmbedBuilder()
                            {
                                Color = new Discord.Color(139, 69, 19),
                                Author = auth,
                            };
                            builder.Description = $"Level: {lvl}\nRank: {tier} {tier2}\nWins: {wins}\nLosses: {Losses}\nWinrate: {winrate}%";
                            builder.ThumbnailUrl = $"http://ddragon.leagueoflegends.com/cdn/{version}/img/profileicon/{icon}.png";
                            await ReplyAsync("", false, builder.Build());
                        }
                        else if (tier == "SILVER")
                        {
                            var builder = new EmbedBuilder()
                            {
                                Color = new Discord.Color(112, 128, 144),
                                Author = auth,
                            };
                            builder.Description = $"Level: {lvl}\nRank: {tier} {tier2}\nWins: {wins}\nLosses: {Losses}\nWinrate: {winrate}%";
                            builder.ThumbnailUrl = $"http://ddragon.leagueoflegends.com/cdn/{version}/img/profileicon/{icon}.png";
                            await ReplyAsync("", false, builder.Build());
                        }
                        else if (tier == "GOLD")
                        {
                            var builder = new EmbedBuilder()
                            {
                                Color = new Discord.Color(218, 165, 32),
                                Author = auth,
                            };
                            builder.Description = $"Level: {lvl}\nRank: {tier} {tier2}\nWins: {wins}\nLosses: {Losses}\nWinrate: {winrate}%";
                            builder.ThumbnailUrl = $"http://ddragon.leagueoflegends.com/cdn/{version}/img/profileicon/{icon}.png";
                            await ReplyAsync("", false, builder.Build());
                        }
                        else if (tier == "PLATINUM")
                        {
                            var builder = new EmbedBuilder()
                            {
                                Color = new Discord.Color(175, 238, 238),
                                Author = auth,
                            };
                            builder.Description = $"Level: {lvl}\nRank: {tier} {tier2}\nWins: {wins}\nLosses: {Losses}\nWinrate: {winrate}%";
                            builder.ThumbnailUrl = $"http://ddragon.leagueoflegends.com/cdn/{version}/img/profileicon/{icon}.png";
                            await ReplyAsync("", false, builder.Build());
                        }
                        else if (tier == "DIAMOND")
                        {
                            var builder = new EmbedBuilder()
                            {
                                Color = new Discord.Color(30, 144, 255),
                                Author = auth,
                            };
                            builder.Description = $"Level: {lvl}\nRank: {tier} {tier2}\nWins: {wins}\nLosses: {Losses}\nWinrate: {winrate}%";
                            builder.ThumbnailUrl = $"http://ddragon.leagueoflegends.com/cdn/{version}/img/profileicon/{icon}.png";
                            await ReplyAsync("", false, builder.Build());
                        }
                        else if (tier == "MASTERS")
                        {
                            var builder = new EmbedBuilder()
                            {
                                Color = new Discord.Color(178, 34, 34),
                                Author = auth,
                            };
                            builder.Description = $"Level: {lvl}\nRank: {tier} {tier2}\nWins: {wins}\nLosses: {Losses}\nWinrate: {winrate}%";
                            builder.ThumbnailUrl = $"http://ddragon.leagueoflegends.com/cdn/{version}/img/profileicon/{icon}.png";
                            await ReplyAsync("", false, builder.Build());
                        }
                        else if (tier == "CHALLENGER")
                        {
                            var builder = new EmbedBuilder()
                            {
                                Color = new Discord.Color(220, 20, 60),
                                Author = auth,
                            };
                            builder.Description = $"Level: {lvl}\nRank: {tier} {tier2}\nWins: {wins}\nLosses: {Losses}\nWinrate: {winrate}%";
                            builder.ThumbnailUrl = $"http://ddragon.leagueoflegends.com/cdn/{version}/img/profileicon/{icon}.png";
                            await ReplyAsync("", false, builder.Build());
                        }
                    }
                    catch
                    {
                        var auth = new EmbedAuthorBuilder()
                        {
                            Name = $"{Name}'s profile",
                        };
                        string version = "7.19.1";
                        var rnd = new Random();
                        int g1 = rnd.Next(1, 255);
                        int g2 = rnd.Next(1, 255);
                        int g3 = rnd.Next(1, 255);
                        var builder = new EmbedBuilder()
                        {
                            Color = new Discord.Color(g1, g2, g3),
                            Author = auth,
                        };
                        builder.Description = $"Level: {lvl}\nNo ranked data.";
                        builder.ThumbnailUrl = $"http://ddragon.leagueoflegends.com/cdn/{version}/img/profileicon/{icon}.png";
                        await ReplyAsync("", false, builder.Build());
                        return;
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
                    var builder = new EmbedBuilder()
                    {
                        Color = new Discord.Color(g1, g2, g3),
                        Author = auth,
                    };
                    builder.Description = $"Username not found";
                    builder.ThumbnailUrl = $"https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/pic/oh.png";
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
            embed.ImageUrl = $"https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/pic/lol.jpg";
            await ReplyAsync("", false, embed.Build());
        }
        [Command("cover")]
        [Alias("gyb", "c")]
        [Summary(".cover")]
        [Remarks("Covers some content.")]
        public async Task cover()
        {
            int Delete = 1;
            foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
            {
                await Item.DeleteAsync();
            }
            await Context.Channel.SendFileAsync("D:/Discord/Luxary/Luxary/bin/Debug/pic/blank.png");
            await Context.Channel.SendFileAsync("D:/Discord/Luxary/Luxary/bin/Debug/pic/blank.png");
            await Context.Channel.SendFileAsync("D:/Discord/Luxary/Luxary/bin/Debug/pic/blank.png");
            await Context.Channel.SendFileAsync("D:/Discord/Luxary/Luxary/bin/Debug/pic/blank.png");
            await ReplyAsync("Covered some weird shit for " + Context.User.Mention + " your welcome");
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
           
            var embed = new EmbedBuilder();
            embed.Title = "This wil be your champ";
            embed.ImageUrl = $"https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/league/{ex}.png";
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
            if (!(thumbnailurl == null))
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
                int Delete = 1;
                foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
                {
                    await Item.DeleteAsync();
                }
                Console.WriteLine("Making API Call...");
                using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
                {
                    string websiteurl = $"https://api.tenor.co/v1/search?tag={tag}&key=0V4RTXJOKKRQ";
                    client.BaseAddress = new Uri(websiteurl);
                    HttpResponseMessage response = client.GetAsync("").Result;
                    response.EnsureSuccessStatusCode();
                    string result = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(result);
                    string Joke = json["results"][0]["url"].ToString();

                    await ReplyAsync(Joke);
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
                builder.ThumbnailUrl = $"https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/pic/oh.png";
                await ReplyAsync("", false, builder.Build());
            }
        }
        [Command("giphy")]
        [Summary(".giphy **<message>**")]
        [Remarks("Shows a gif.")]
        [Alias("gp")]
        public async Task giphy([Remainder] string tag = null)
        {
            try
            {
                int Delete = 1;
                foreach (var Item in await Context.Channel.GetMessagesAsync(Delete).Flatten())
                {
                    await Item.DeleteAsync();
                }
                Console.WriteLine("Making API Call...");
                using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
                {
                    string websiteurl = $"https://api.giphy.com/v1/gifs/random?api_key=BJLJx6TDsXhXKdAIo5qw0ItokQHku6rr&tag={tag}&rating=G";
                    client.BaseAddress = new Uri(websiteurl);
                    HttpResponseMessage response = client.GetAsync("").Result;
                    response.EnsureSuccessStatusCode();
                    string result = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(result);
                    string Joke = json["data"]["url"].ToString();
                    await ReplyAsync(Joke);
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
                builder.ThumbnailUrl = $"https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/pic/oh.png";
                await ReplyAsync("", false, builder.Build());
            }
        }
        [Command("weeb")]
        [Summary(".weeb")]
        [Remarks("Sends a weeb gif")]
        public async Task weeb()
        {
            Console.WriteLine("Sending weebstuff >_< desu desu..");
            var build = new EmbedBuilder();
            build.Title = "desu desu";
            build.ImageUrl = "https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/pic/weeb.gif";
            await ReplyAsync("", false, build.Build());
        }
    }
}
