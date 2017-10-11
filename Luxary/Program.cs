using System;
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
using Luxary.Service;

namespace Luxary
{
    class Program
    {
        private CommandService commands;
        private DiscordSocketClient client;
        private IServiceProvider services;

        string token = "";

        static void Main(string[] args) =>
            new Program().Start().GetAwaiter().GetResult();

        public async Task Start()
        {
            client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose
            });
            commands = new CommandService();
            services = new ServiceCollection().BuildServiceProvider();

            await InstallCommands();

            try
            {
                StreamReader sr = new StreamReader("token.txt");
                token = sr.ReadLine();
            }
            catch (Exception e)
            {
                Console.Write("Exception: " + e.Message);
            }
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            client.Log += Log;
            client.UserJoined += UserJoined;
            client.UserLeft += UserLeft;
            client.MessageReceived += Message;
            client.Ready += Game;

        await Task.Delay(-1);
        }
        public async Task Game()
        {
            await client.SetGameAsync(".weeb for memes");
        }
        public async Task UserJoined(SocketGuildUser user)
        {
            var channel = user.Guild.DefaultChannel;
            await channel.SendMessageAsync("Welcome to the server " + user.Mention + "!");
        }
        public async Task UserLeft(SocketGuildUser user)
        {
            var channel = user.Guild.DefaultChannel;
            await channel.SendMessageAsync(user.Mention + " Left the channel :(");
        }
        public async Task InstallCommands()
        {
            client.MessageReceived += HandleCommand;
            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }
        public async Task Message(SocketMessage message)
        {
            if (!message.Author.IsBot)
            {
                var userMentioned = message.MentionedUsers.FirstOrDefault();
                if (userMentioned != null)
                {
                    var awayData = new awaydata();
                    var awayUser = awayData.getAwayUser(userMentioned.Username);
                    if (awayUser != null)
                    {
                        string awayDuration = string.Empty;
                        if (awayUser.AwayTime.HasValue)
                        {
                            var awayTime = DateTime.Now - awayUser.AwayTime;
                            if (awayTime.HasValue)
                            {
                                awayDuration = $"**{awayTime.Value.Hours}** hours, **{awayTime.Value.Minutes}** minutes, and **{awayTime.Value.Seconds}** seconds";
                            }
                        }

                        Console.WriteLine($"Mentioned user {userMentioned.Username} -> {awayUser.User} -> {awayUser.Status}");
                        if ((bool)awayUser.Status)
                        {
                            if (userMentioned.Username == (awayUser.User))
                            {

                                var embeds = new EmbedBuilder();
                                embeds.Color = new Color(199, 21, 112);
                                embeds.ThumbnailUrl = userMentioned.GetAvatarUrl();
                                embeds.Title = $" {awayUser.User} is away";
                                embeds.Description = $"{awayUser.User} is away since **{awayUser.AwayTime} for {awayDuration}** \n \n he/she left a **__Message__**: \n \n {awayUser.Message} \n \n \n refrain from pinging him/her unnecessarily";
                                await message.Channel.SendMessageAsync("", false, embeds);
                            }
                        }
                    }
                }
            }
        }

        public async Task HandleCommand(SocketMessage msgParam)
        {
            var msg = msgParam as SocketUserMessage;
            char prefix = '.';
            if (msg == null) return;

            int argPos = 0;

            if (!(msg.HasCharPrefix(prefix, ref argPos) || msg.HasMentionPrefix(client.CurrentUser, ref argPos))) return;

            var context = new CommandContext(client, msg);

            var result = await commands.ExecuteAsync(context, argPos, services);
            if (!result.IsSuccess)
            {
                var builder = new EmbedBuilder
                {
                    Title = "Oh no..",
                    Color = new Discord.Color(178, 34, 34),
                    Description = result.ErrorReason,
                    ThumbnailUrl = $"https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/pic/oh.png",
                };
                await context.Channel.SendMessageAsync("", false, builder.Build());
            }
        }

        private Task Log(LogMessage msg)
        {
            var c = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            switch (msg.Severity)
            {
                case LogSeverity.Critical:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case LogSeverity.Error:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
                case LogSeverity.Warning:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case LogSeverity.Info:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogSeverity.Verbose:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case LogSeverity.Debug:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
            }
            Console.WriteLine($"{DateTime.Now,-19} [{msg.Severity,8}] {msg.Source}: {msg.Message}");
            Console.ForegroundColor = c;

            return Task.CompletedTask;
        }
    }
}
