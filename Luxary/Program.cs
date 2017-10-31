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
        private CommandService _commands;
        private DiscordSocketClient _client;
        private IServiceProvider _services;

        string _token = "";

        public static void Main(string[] args)
        {
            new Program().Start().GetAwaiter().GetResult();
        }

        public async Task Start()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose
            });
            _commands = new CommandService();
            _services = new ServiceCollection().BuildServiceProvider();

            await InstallCommands();

            try
            {
                Console.WriteLine("Hi master, token found :3");
                StreamReader sr = new StreamReader("token.txt");
                _token = sr.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            await _client.LoginAsync(TokenType.Bot, _token);
            await _client.StartAsync();

            _client.Log += Log;
            _client.UserJoined += UserJoined;
            _client.UserLeft += UserLeft;
            _client.MessageReceived += Message;
            _client.Ready += Game;

        await Task.Delay(-1);
        }

        public async Task Game()
        {
            await _client.SetGameAsync(".cmds for commands");
        }
        public async Task UserJoined(SocketGuildUser user)
        {
            var channel = user.Guild.DefaultChannel;
            await channel.SendMessageAsync("Welcome to the server " + user.Mention + "! <:happy:362565108032995329>");
        }
        public async Task UserLeft(SocketGuildUser user)
        {
            var channel = user.Guild.DefaultChannel;
            await channel.SendMessageAsync(user.Mention + " Left the channel <:mad:362497418291314688>");
        }
        public async Task InstallCommands()
        {
            _client.MessageReceived += HandleCommand;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
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

                                var embeds = new EmbedBuilder
                                {
                                    Color = new Color(199, 21, 112),
                                    ThumbnailUrl = userMentioned.GetAvatarUrl(),
                                    Title = $" {awayUser.User} is away",
                                    Description =
                                        $"{awayUser.User} is away since **{awayUser.AwayTime} for {awayDuration}** \n \n he/she left a **__Message__**: \n \n {awayUser.Message} \n \n \n refrain from pinging him/her unnecessarily"
                                };
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

            if (!(msg.HasCharPrefix(prefix, ref argPos) ||
                  msg.HasMentionPrefix(_client.CurrentUser, ref argPos))) return;

            var context = new CommandContext(_client, msg);

            var result = await _commands.ExecuteAsync(context, argPos, _services);
            if (!result.IsSuccess)
            {
                //    var builder = new EmbedBuilder
                //    {
                //        Title = "Oh no..",
                //        Color = new Discord.Color(178, 34, 34),
                //        Description = result.ErrorReason,
                //        ThumbnailUrl = $"https://raw.githubusercontent.com/ThijmenHogenkamp/Bot/master/Luxary/bin/Debug/pic/oh.png",
                //    };
                //    await context.Channel.SendMessageAsync("", false, builder.Build());
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
            //COPYRIGHT THIJMEN HOGENKAMP
        }
    }
}
