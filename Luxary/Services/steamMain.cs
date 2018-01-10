using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using SteamKit2;

namespace Luxary
{
    namespace LuxaryLog
    {
#if DEBUG
        class SendToDebug : IDebugListener
        {
            public void WriteLine(string category, string msg)
            {
                Debug.WriteLine("SteamKit2> {0} : {1}", category, msg);
            }
        }
#endif
        class SendToCon : IDebugListener
        {
            public void WriteLine(string category, string msg)
            {
                Console.WriteLine("SteamKit2> " + category + ": " + msg, Console.ForegroundColor = ConsoleColor.Green);
                Console.ResetColor();
            }
        }
    }

    class SteamConnect
    {
        public static ulong ID = Convert.ToUInt64(File.ReadAllText("id.txt"));
        public SteamID BotOwnerID = ID;
        string strUser, strPassword;
        public bool steamIsRunning { get; protected set; }
        string authCode, twofactor;
        Random rnd = new Random();
        public SteamClient steamClient { get; }
        public CallbackManager callManager { get; }
        public SteamUser steamUser => steamClient.GetHandler<SteamUser>();
        public SteamFriends steamFriends => steamClient.GetHandler<SteamFriends>();
        public SteamTrading steamTrading => steamClient.GetHandler<SteamTrading>();
        public bool authed = false;
        public JobID StandardCallBackJob = new JobID();
        bool decrwSentP = false;
        bool dlogregd = false;
        public string RememberKey;
        public bool RememberMe = false;
        bool ServerMode = false;

        private IDebugListener ConDeLog { get; } = new LuxaryLog.SendToCon();

        public void ToggleDLogToCon()
        {
            if (dlogregd == false)
            {
                DebugLog.AddListener(ConDeLog);
                dlogregd = true;
#if DEBUG
                Console.WriteLine("DebugLog now outputs here!", Console.ForegroundColor = ConsoleColor.Green);
#endif
            }
            if (DebugLog.Enabled == false)
            {
                DebugLog.Enabled = true;
                Console.WriteLine("DebugLog enabled!", Console.ForegroundColor = ConsoleColor.Green);
            }
            else if (dlogregd)
            {
                DebugLog.RemoveListener(ConDeLog);
                dlogregd = false;
#if DEBUG
                Console.WriteLine(
                    "Console no longer logging DebugLog messages!  Check the debug window output instead.",
                    Console.ForegroundColor = ConsoleColor.Green);
#elif !DEBUG
            DebugLog.Enabled = false;
            Console.WriteLine("DebugLog disabled!", Console.ForegroundColor = ConsoleColor.Green);
#endif
            }
        }

        byte[] getstandardkey()
        {
            System.Security.Cryptography.AesCryptoServiceProvider helper =
                new System.Security.Cryptography.AesCryptoServiceProvider();
            helper.GenerateKey();
            if (File.Exists("dispenser.bin") == false)
                File.WriteAllBytes("dispenser.bin", helper.Key);
            byte[] keyend = File.ReadAllBytes("dispenser.bin");
            byte[] sentryFile;
            if (File.Exists("sentry.bin"))
                sentryFile = File.ReadAllBytes("sentry.bin");
            else
            {
                byte[] dummysent = new byte[18];
                rnd.NextBytes(dummysent);
                File.WriteAllBytes("sentry.bin", dummysent);
                sentryFile = dummysent;
            }
            byte[] sentryHash = CryptoHelper.SHAHash(sentryFile);
            byte[] fullkey = new byte[helper.Key.Length];
            int y = 0;
            while (y < sentryHash.Length)
            {
                fullkey[y] = sentryHash[y];
                y = y + 1;
            }
            while (y < fullkey.Length)
            {
                fullkey[y] = keyend[y - 18];
                y = y + 1;
            }
            return fullkey;

        }

        string GetUserName()
        {
            if (File.Exists("UserName.txt"))
            {
                return File.ReadAllText("UserName.txt");
            }
            else
            {
                Console.WriteLine(
                    "No UserName.txt detected.  Please create one in the root application directory or enter your username in the Input Box.");
                Console.Write("Username> ");
                string username = Console.ReadLine();
                Thread.Sleep(10);
                Console.WriteLine("Type \"yes\" to save this username in a txt file.");
                Console.Write("Store?> ");
                string output = Console.ReadLine();
                if (output == "yes")
                {
                    File.WriteAllText("UserName.txt", username);
                }
                return username;
            }
        }

        public SteamConnect()
        {
            steamClient = new SteamClient();
            callManager = new CallbackManager(steamClient);
        }

        public void BeginClient(string[] args)
        {
            if (BotOwnerID == null)
            {
                System.Media.SystemSounds.Exclamation.Play();
                Debug.Assert(BotOwnerID != null,
                    "SteamID BotOwnerID is not set to a value.  Please assign a value to it in SteamMain.cs on ~line 60.");
                Console.WriteLine(
                    "SteamID BotOwnerID is not set to a value.  Please assign a value to it in SteamMain.cs.\nPress any key to exit.");
                Console.WriteLine(Console.ReadKey());
                Environment.Exit(0);
            }

            DebugLog.AddListener(new LuxaryLog.SendToDebug());
            DebugLog.Enabled = true;
            try
            {
                if (args[0] == "servermode")
                {
                    Console.WriteLine("Running as game server!");
                    ServerMode = true;
                }
            }
            catch
            {
            }
            Console.WriteLine("Ctrl+C Quits the Program", Console.BackgroundColor = ConsoleColor.White);
            Console.WriteLine(
                "Be aware of what the input line says at the beginning.  If you see \"Command>\" at the beginning of the line, even if it gets ammended with something else, be warned that you will have to press enter before anything but commands will be accepted.  Oh, and if nothing is displayed on the typing line, then your first keypress will be ignored unless you see something appear on the line.  Be aware of this.");
            Console.ResetColor();
            strUser = GetUserName();
            StreamReader sr = new StreamReader("pw.txt");
            strPassword = sr.ReadLine();

            RunSteam();

        }

        public void RunSteam()
        {
            callManager.Subscribe<SteamClient.ConnectedCallback>(StandardCallBackJob, OnConnected);
            callManager.Subscribe<SteamUser.LoggedOnCallback>(StandardCallBackJob, OnLoggedOn);
            callManager.Subscribe<SteamClient.DisconnectedCallback>(StandardCallBackJob, OnDisconnected);
            callManager.Subscribe<SteamFriends.FriendMsgCallback>(StandardCallBackJob, OnChatMessage);
            callManager.Subscribe<SteamUser.AccountInfoCallback>(StandardCallBackJob, OnAccountInfo);
            callManager.Subscribe<SteamFriends.ChatInviteCallback>(OnChatInvite);
            callManager.Subscribe<SteamFriends.FriendsListCallback>(OnFriendRequest);
            callManager.Subscribe<SteamTrading.TradeProposedCallback>(StandardCallBackJob, Trade);
            steamIsRunning = true;
            Console.WriteLine("Opening Connection....", Console.ForegroundColor = ConsoleColor.Green,
                Console.BackgroundColor == ConsoleColor.Black);
            steamClient.Connect();
            const int callbacklimit = 100;
            while (steamIsRunning)
            {
                int counterup = 0;
                while (steamClient.GetCallback() != null && counterup < callbacklimit)
                {
                    callManager.RunCallbacks();
                    counterup++;
                }
                if (counterup > callbacklimit)
                {
                    //idk yet
                }
                steamClient.WaitForCallback();
            }
        }

        void OnConnected(SteamKit2.SteamClient.ConnectedCallback callback)
        {
            if (callback.Result == EResult.NoConnection)
            {
                Console.WriteLine("Not connected to the internet.  Gonna take a little longer than usual.",
                    Console.ForegroundColor = ConsoleColor.Green);
                Thread.Sleep(25000);
            }
            if (callback.Result != EResult.OK)
            {
                Console.WriteLine("OH NO! I CAN'T BELIEVE IT!  Steam won't talk to me." + callback.Result,
                    Console.ForegroundColor = ConsoleColor.Green);
                return;
            }

            Console.WriteLine("Connected to Steam.");
            Console.WriteLine("Logging in, reversing the polarity of the neutron flow!",
                Console.ForegroundColor = ConsoleColor.Green);

            byte[] sentryHash = null;
            if (File.Exists("sentry.bin"))
            {
                byte[] sentryFile = File.ReadAllBytes("sentry.bin");

                sentryHash = CryptoHelper.SHAHash(sentryFile);
                if (RememberMe && File.Exists("UserKey.bin"))
                {
                    byte[] keyhashed = File.ReadAllBytes("UserKey.bin");
                    RememberKey =
                        Encoding.Unicode.GetString(CryptoHelper.SymmetricDecrypt(keyhashed, getstandardkey()));
                }

                if (decrwSentP == true)
                {
                    byte[] phashed = File.ReadAllBytes("UserPass.bin");
                    strPassword = Encoding.Unicode.GetString(CryptoHelper.SymmetricDecrypt(phashed, getstandardkey()));
                }
            }

            steamUser.LogOn(new SteamUser.LogOnDetails
            {
                Username = strUser,
                Password = strPassword,

                AuthCode = authCode,

                TwoFactorCode = twofactor,

                LoginKey = RememberKey,

                ShouldRememberPassword = RememberMe,

                SentryFileHash = sentryHash,
            });
        }

        bool failonce = false;

        void OnLoggedOn(SteamUser.LoggedOnCallback callback)
        {
            if (callback.Result == EResult.AccountLogonDenied)
            {
                Console.WriteLine("Steam Guard denied your a(cce)ss!", Console.ForegroundColor = ConsoleColor.Green);
                Console.WriteLine("Please enter code that you got emailed!",
                    Console.ForegroundColor = ConsoleColor.Green);
                Console.ResetColor();
                Console.Write("AuthCode> ");
                authCode = Console.ReadLine();
                Console.WriteLine("Your Authcode is: " + authCode);
                return;
            }

            if (callback.Result == EResult.AccountLoginDeniedNeedTwoFactor)
            {
                Console.WriteLine("Steam Guard denied your a(cce)ss!", Console.ForegroundColor = ConsoleColor.Green);
                Console.WriteLine("Please enter code from two-factor authentication (ie. from your mobile app)!: ",
                    Console.ForegroundColor = ConsoleColor.Green);
                Console.ResetColor();
                Console.Write("Two-Factor Code> ");
                twofactor = Console.ReadLine();
                return;
            }

            if (callback.Result == EResult.LogonSessionReplaced || callback.Result == EResult.LoggedInElsewhere ||
                callback.Result == EResult.AlreadyLoggedInElsewhere)
            {
                Console.WriteLine("I'm already logged in elsewhere!  Shut down the other me first!",
                    Console.ForegroundColor = ConsoleColor.Green);
                Console.ResetColor();
                steamClient.Disconnect();
                return;
            }

            if (callback.Result != EResult.OK)
            {
                Console.WriteLine("Login failed! Reason: " + callback.Result,
                    Console.ForegroundColor = ConsoleColor.Green);
                return;
            }
            Console.WriteLine("I'm logged in and ready to go!", Console.ForegroundColor = ConsoleColor.Red);
            Console.Write("My SteamID: ");
            Console.WriteLine(steamClient.SteamID.ToString(), Console.ForegroundColor = ConsoleColor.Yellow);
            Console.Write("Owner SteamID (I'm assuming this is you.):", Console.ForegroundColor = ConsoleColor.Red);
            Console.WriteLine(BotOwnerID.ToString(), Console.ForegroundColor = ConsoleColor.Yellow);
            Console.ResetColor();
            authed = true;
        }

        void OnDisconnected(SteamKit2.SteamClient.DisconnectedCallback callback)
        {
            if (callback.UserInitiated)
            {
                Console.WriteLine("Disconnect successful.  I'll be napping over here until you reconnect.",
                    Console.ForegroundColor = ConsoleColor.Green);
                steamIsRunning = false;
                return;
            }
            Console.WriteLine("Disconnected, reconnect in 5 sec.", Console.ForegroundColor = ConsoleColor.Green);
            Thread.Sleep(5000);
            steamClient.Connect();
        }

        void OnAccountInfo(SteamUser.AccountInfoCallback callback)
        {
            steamFriends.SetPersonaState(EPersonaState.Online);
        }

        public void OnChatInvite(SteamFriends.ChatInviteCallback callback)
        {
            steamFriends.JoinChat(callback.ChatRoomID);
        }

        public void OnFriendRequest(SteamFriends.FriendsListCallback callback)
        {
            foreach (var friend in callback.FriendList)
            {
                if (friend.Relationship == EFriendRelationship.RequestRecipient)
                {
                    steamFriends.AddFriend(friend.SteamID);
                }
            }
        }

        public void Trade(SteamTrading.TradeProposedCallback callback)
        {
            steamTrading.RespondToTrade(callback.TradeID, true);
        }

        public void OnChatMessage(SteamFriends.FriendMsgCallback callback)
        {
            if (callback.Message == ".trade")
            {

            }
            if (callback.Message == ".sleep")
            {
                steamFriends.SendChatMessage(callback.Sender, EChatEntryType.ChatMsg, "hehe xd bye");
                steamClient.Disconnect();
                Environment.Exit(0);
            }
            if (callback.Message == ".ping")
            {
                steamFriends.SendChatMessage(callback.Sender, EChatEntryType.ChatMsg, "pong!:chocola3:");
            }
            if (callback.Message == ".help")
            {
                steamFriends.SendChatMessage(callback.Sender, EChatEntryType.ChatMsg, "\n.ping:chocola3:\n.sleep:chocola3:\n.say (text):chocola3:");
            }
            if (callback.Message.Contains(".say"))
            {
                string xd = callback.Message.Replace(".say ", "");
                steamFriends.SendChatMessage(callback.Sender, EChatEntryType.ChatMsg, xd);
            }
            UInt64 IDD = 76561198058269957;
            if (callback.Message != String.Empty)
            {
                steamFriends.SendChatMessage(callback.Sender, EChatEntryType.ChatMsg, "am at work, so afking nekopara");
            }
        }
        public void OnBChatMessage(SteamFriends.ChatMsgCallback callback)
        {

            if (callback.Message == ".sleep")
            {
                steamFriends.SendChatMessage(callback.ChatterID, EChatEntryType.ChatMsg, "hehe xd bye");
                steamClient.Disconnect();
                Environment.Exit(0);
            }
            if (callback.Message == ".ping")
            {
                steamFriends.SendChatMessage(callback.ChatterID, EChatEntryType.ChatMsg, "pong!:chocola3:");
            }
            if (callback.Message == ".help")
            {
                steamFriends.SendChatMessage(callback.ChatterID, EChatEntryType.ChatMsg, "\n.ping:chocola3:\n.sleep:chocola3:\n.say (text):chocola3:");
            }
            if (callback.Message.Contains(".say"))
            {
                string xd = callback.Message.Replace(".say ", "");
                steamFriends.SendChatMessage(callback.ChatterID, EChatEntryType.ChatMsg, xd);
            }
            UInt64 IDD = 76561198044693012;
            if (callback.ChatterID == IDD)
            {
                steamFriends.SendChatMessage(callback.ChatterID, EChatEntryType.ChatMsg, "test");
            }
        }
    }
}
