//using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.IO;
using Luxary;
using System;

namespace Luxary
{
    public class Gamesss : ModuleBase
    {
        static string SwitchCaseString = "nofight";

        static string player1;
        static string player2;

        static string whosTurn;
        static string whoWaits;
        static string placeHolder;

        static int health1 = 100;
        static int health2 = 100;

        [Command("fight")]
        [Summary(".fight **<user>**")]
        [Remarks("Fights the user with a turnbased fighting game.")]
        [Alias("Fight")]
        public async Task Fight(IUser user)
        {

            if (Context.User.Mention != user.Mention && SwitchCaseString == "nofight")
            {
                SwitchCaseString = "fight_p1";
                player1 = Context.User.Mention;
                player2 = user.Mention;

                string[] whoStarts = new string[]
                {
                    Context.User.Mention,
                    user.Mention

                };

                Random rand = new Random();

                int randomIndex = rand.Next(whoStarts.Length);
                string text = whoStarts[randomIndex];

                whosTurn = text;
                if (text == Context.User.Mention)
                {
                    whoWaits = user.Mention;
                }
                else
                {
                    whoWaits = Context.User.Mention;
                }

                await ReplyAsync("Fight started between " + Context.User.Mention + " and " + user.Mention + "!\n\n" + player1 + " you got " + health1 + " health!\n" + player2 + " you got " + health2 + " health!\n\n" + text + " your turn!");

            }
            else
            {
                await ReplyAsync(Context.User.Mention + " sorry but there is a fight going on right now, or you just tried to fight urself...");
            }

        }


        [Command("giveup")]
        [Alias("GiveUp", "Giveup", "giveUp")]
        [Summary(".giveup")]
        [Remarks("Stops the fight.")]
        public async Task GiveUp()
        {
            if (SwitchCaseString == "fight_p1")
            {
                await ReplyAsync("The fight stopped.");
                SwitchCaseString = "nofight";
                health1 = 100;
                health2 = 100;
            }
            else
            {
                await ReplyAsync("There is no fight to stop.");
            }
        }

        [Command("Slash")]
        [Alias("slash")]
        [Summary(".slash")]
        [Remarks("Slashes your foe with a sword. Good accuracy and medium damage")]
        public async Task Slash()
        {
            if (SwitchCaseString == "fight_p1")
            {
                if (whosTurn == Context.User.Mention)
                {
                    Random rand = new Random();

                    int randomIndex = rand.Next(1, 6);
                    if (randomIndex != 1)
                    {
                        Random rand2 = new Random();

                        int randomIndex2 = rand2.Next(7, 15);

                        if (Context.User.Mention != player1)
                        {
                            health1 = health1 - randomIndex2;
                            if (health1 > 0)
                            {
                                placeHolder = whosTurn;
                                whosTurn = whoWaits;
                                whoWaits = placeHolder;

                                await ReplyAsync(Context.User.Mention + " u hit and did " + randomIndex2 + " damage!\n\n" + player1 + " got " + health1 + " health left!\n" + player2 + " got " + health2 + " health left!\n\n" + whosTurn + " ur turn!");

                            }
                            else
                            {
                                await ReplyAsync(Context.User.Mention + " u hit and did " + randomIndex2 + " damage!\n\n" + player1 + " died. " + player2 + " won!");
                                SwitchCaseString = "nofight";
                                health1 = 100;
                                health2 = 100;
                            }
                        }
                        else if (Context.User.Mention == player1)
                        {
                            health2 = health2 - randomIndex2;
                            if (health2 > 0)
                            {
                                placeHolder = whosTurn;
                                whosTurn = whoWaits;
                                whoWaits = placeHolder;

                                await ReplyAsync(Context.User.Mention + " u hit and did " + randomIndex2 + " damage!\n\n" + player1 + " got " + health1 + " health left!\n" + player2 + " got " + health2 + " health left!\n\n" + whosTurn + " ur turn!");
                            }
                            else
                            {
                                await ReplyAsync(Context.User.Mention + " u hit and did " + randomIndex2 + " damage!\n\n" + player2 + " died. " + player1 + " won!");
                                SwitchCaseString = "nofight";
                                health1 = 100;
                                health2 = 100;
                            }
                        }
                        else
                        {
                            await ReplyAsync("Sorry it seems like something went wrong. Pls type !giveup");
                        }


                    }
                    else
                    {
                        placeHolder = whosTurn;

                        await ReplyAsync(Context.User.Mention + " sorry you missed!\n" + whosTurn + " ur turn!");
                    }
                }
                else
                {
                    await ReplyAsync(Context.User.Mention + " it is not your turn.");
                }
            }
            else
            {
                await ReplyAsync("There is no fight at the moment. Sorry :/");
            }

        }
    }
}

//        static string text = null;
//        static string player = null;
//        static int punten = 0;
//        static string game = "offline";
//        static string[] quote =
//        {
//            "That tasted Purple",
//            "You belong in a museum",
//            "Hasagi",
//            "Let's light it up",
//            "Time to troll!",
//            "So much untapped power!",
//            "Shurima! Your emperor has returned!",
//                "test"

//        };
//        //static Random rande = new Random();
//        //static int randomIndex = rande.Next(quote.Length);
//        //static string next = null;
//        static int i = 0;
//        static int q = 1;
//        [Command("start")]
//        [Summary(".start")]
//        [Remarks("Starts a quote minigame.")]
//        public async Task Start()
//        {
//            player = Context.User.Mention;
//            if (game == "live")
//            {
//                var ember = new EmbedBuilder();
//                ember.Title = ("League Quotes");
//                ember.Description = ("Game already started! \n .guess **<quote>** to guess a champion.\n .stop to stop the game.");
//                await ReplyAsync("", false, ember.Build());
//            }
//            else
//            {
//                punten = 0;
//                game = "live";
//                text = quote[i];
//                var ember = new EmbedBuilder();
//                ember.Color = new Color(0, 255, 0);
//                ember.Title = ("League Quotes");
//                ember.Description = ($"Started the quote game \n\n .guess **<quote>** to make your guess!\n **{quote[i]}**");
//                await ReplyAsync("", false, ember.Build());

//            }
//        }
//        [Command("guess")]
//        [Alias("g")]
//        [Summary(".guess **<champion>**")]
//        [Remarks("To guess a champion quote")]
//        public async Task Guess([Remainder]string answer)
//        {
//            if (game == "live")
//            {
//                if (Context.User.Mention == player)
//                {
//                    if (i == 0)
//                    {
//                        if (answer.Contains("lulu"))
//                        {
//                            punten += 1;
//                            var ember = new EmbedBuilder();
//                            ember.Title = ("League Quotes");
//                            ember.Description = ($"Correct! \n **+1** \n Your new quote is: \n**{quote[q]}**");
//                            ember.Color = new Color(0, 255, 0);
//                            await ReplyAsync("", false, ember.Build());
//                        }
//                        else
//                        {
//                            var ember = new EmbedBuilder();
//                            ember.Title = ("League Quotes");
//                            ember.Description = ($"Incorrect! \n Your new quote is: \n**{quote[q]}**");
//                            ember.Color = new Color(139, 0, 0);
//                            await ReplyAsync("", false, ember.Build());
//                        }
//                        i = i + 1;
//                        q = q + 1;
//                    }
//                    else if (i == 1)
//                    {
//                        if (answer.Contains("ezreal"))
//                        {
//                            punten += 1;
//                            var ember = new EmbedBuilder();
//                            ember.Title = ("League Quotes");
//                            ember.Description = ($"Correct! \n **+1** \n Your new quote is: \n**{quote[q]}**");
//                            ember.Color = new Color(0, 255, 0);
//                            await ReplyAsync("", false, ember.Build());
//                        }
//                        else
//                        {
//                            var ember = new EmbedBuilder();
//                            ember.Title = ("League Quotes");
//                            ember.Description = ($"Incorrect! \n Your new quote is: \n**{quote[q]}**");
//                            ember.Color = new Color(139, 0, 0);
//                            await ReplyAsync("", false, ember.Build());
//                        }
//                        i = i + 1;
//                        q = q + 1;
//                    }
//                    else if (i == 2)
//                    {
//                        if (answer.Contains("yasuo"))
//                        {
//                            punten += 1;
//                            var ember = new EmbedBuilder();
//                            ember.Title = ("League Quotes");
//                            ember.Description = ($"Correct! \n **+1** \n Your new quote is: \n**{quote[q]}**");
//                            ember.Color = new Color(0, 255, 0);
//                            await ReplyAsync("", false, ember.Build());
//                        }
//                        else
//                        {
//                            var ember = new EmbedBuilder();
//                            ember.Title = ("League Quotes");
//                            ember.Description = ($"Incorrect! \n Your new quote is: \n**{quote[q]}**");
//                            ember.Color = new Color(139, 0, 0);
//                            await ReplyAsync("", false, ember.Build());
//                        }
//                        i = i + 1;
//                        q = q + 1;
//                    }
//                    else if (i == 3)
//                    {
//                        if (answer.Contains("lux"))
//                        {
//                            punten += 1;
//                            var ember = new EmbedBuilder();
//                            ember.Title = ("League Quotes");
//                            ember.Description = ($"Correct! \n **+1** \n Your new quote is: \n**{quote[q]}**");
//                            ember.Color = new Color(0, 255, 0);
//                            await ReplyAsync("", false, ember.Build());
//                        }
//                        else
//                        {
//                            var ember = new EmbedBuilder();
//                            ember.Title = ("League Quotes");
//                            ember.Description = ($"Incorrect! \n Your new quote is: \n**{quote[q]}**");
//                            ember.Color = new Color(139, 0, 0);
//                            await ReplyAsync("", false, ember.Build());
//                        }
//                        i = i + 1;
//                        q = q + 1;
//                    }
//                    else if (i == 4)
//                    {
//                        if (answer.Contains("trundle"))
//                        {

//                            punten += 1;
//                            var ember = new EmbedBuilder();
//                            ember.Title = ("League Quotes");
//                            ember.Description = ($"Correct! \n **+1** \n Your new quote is: \n**{quote[q]}**");
//                            ember.Color = new Color(0, 255, 0);
//                            await ReplyAsync("", false, ember.Build());
//                        }
//                        else
//                        {

//                            var ember = new EmbedBuilder();
//                            ember.Title = ("League Quotes");
//                            ember.Description = ($"Incorrect! \n Your new quote is: \n**{quote[q]}**");
//                            ember.Color = new Color(139, 0, 0);
//                            await ReplyAsync("", false, ember.Build());
//                        }
//                        i = i + 1;
//                        q = q + 1;
//                    }
//                    else if (i == 5)
//                    {
//                        if (answer.Contains("syndra"))
//                        {

//                            punten += 1;
//                            var ember = new EmbedBuilder();
//                            ember.Title = ("League Quotes");
//                            ember.Description = ($"Correct! \n **+1** \n Your new quote is: \n**{quote[q]}**");
//                            ember.Color = new Color(0, 255, 0);
//                            await ReplyAsync("", false, ember.Build());
//                        }
//                        else
//                        {

//                            var ember = new EmbedBuilder();
//                            ember.Title = ("League Quotes");
//                            ember.Description = ($"Incorrect! \n Your new quote is: \n**{quote[q]}**");
//                            ember.Color = new Color(139, 0, 0);
//                            await ReplyAsync("", false, ember.Build());
//                        }
//                        i = i + 1;
//                        q = q + 1;
//                    }
//                    else if (i == 6)
//                    {
//                        if (answer.Contains("azir"))
//                        {
//                            punten += 1;
//                            var ember = new EmbedBuilder();
//                            ember.Title = ("League Quotes");
//                            ember.Description = $"Correct! \n \nYou finished the game! \nYou scored **{punten}** points";
//                            ember.Color = new Color(0, 255, 0);
//                            await ReplyAsync("", false, ember.Build());
//                        }
//                        else
//                        {
//                            var ember = new EmbedBuilder();
//                            ember.Title = ("League Quotes");
//                            ember.Description = $"Incorrect! \n \nYou finished the game! \nYou scored **{punten}** points";
//                            ember.Color = new Color(139, 0, 0);
//                            await ReplyAsync("", false, ember.Build());
//                        }
//                        game = "offline";
//                        i = 0;
//                    }
//                }
//                else
//                {
//                    await ReplyAsync("not your game");
//                }
//            }
//            else
//            {
//                await ReplyAsync("game is not live");
//            }
//        }
//        [Command("stop")]
//        [Summary(".stop")]
//        [Remarks("Stops the quote minigame.")]
//        public async Task StopGame()
//        {
//            if (game == "live")
//            {
//                if (Context.User.Mention == player)
//                {
//                    var ember = new EmbedBuilder();
//                    ember.Title = ("League Quotes");
//                    ember.Color = new Color(255, 215, 0);
//                    ember.Description = $"You stopped the game! \nYou scored {punten} points";
//                    await ReplyAsync("", false, ember.Build());
//                    game = "offline";
//                    i = 0;
//                }
//                else
//                {
//                    var ember = new EmbedBuilder();
//                    ember.Title = ("League Quotes");
//                    ember.Color = new Color(178, 34, 34);
//                    ember.Description = $"You cannot stop another players game.";
//                    await ReplyAsync("", false, ember.Build());
//                }
//            }
//            else
//            {
//                var ember = new EmbedBuilder();
//                ember.Title = ("League Quotes");
//                ember.Color = new Color(153,50,204);
//                ember.Description = $"Game has not started yet.";
//                await ReplyAsync("", false, ember.Build());
//            }
//        }
//        [Command("test")]
//        public async Task test()
//        {
//            await ReplyAsync($"{game}i={i}, Punten: {punten}");
//        }
//    }
//}
