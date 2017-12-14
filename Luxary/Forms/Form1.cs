using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Net.Http;
using Luxary.Services;
using Newtonsoft.Json;
using System.Windows.Forms.VisualStyles;
using System.Threading;
using SixLabors.Shapes;

namespace Luxary
{
    public partial class FormConsole : Form
    {
        TextWriter _writer = null;
        private static string xd = "hello";
        public FormConsole()
        {
            InitializeComponent();
        }

        private void FormConsole_Load(object sender, EventArgs e)
        {
            _writer = new TextBoxStreamWriter(txtConsole);
            Console.SetOut(_writer);
            Timer();
        }
        static System.Timers.Timer xxd;
        public void txtSayHello_Click(object sender, EventArgs e)
        {
            FormConsole form1 = this;
            if (xd == "hi")
            {
                txtConsole.Clear();
                Program.Start();
                xd = "hello";
                
            }
        }
        private void Timer()
        {
            xxd = new System.Timers.Timer();
            xxd.Start();
            xxd.Interval = 1000;
            xxd.AutoReset = true;
            xxd.Elapsed += StartBoi;
        }
        public async void StartBoi(Object source, System.Timers.ElapsedEventArgs e)
        {
            await Stats();
        }
        private void sleep_Click(object sender, EventArgs e)
        {
            if (xd == "hello")
            {
                txtConsole.Clear();
                Program.Stop();
                xd = "hi";
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (xd == "hello")
            {
                Embed_Messages open = new Embed_Messages(new DiscordSocketClient());
                open.Show();
            }
            else
            {
                txtConsole.Text = "Bot is not online.";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (xd == "hello")
            {
                Channel_Editor open = new Channel_Editor(new DiscordSocketClient());
                open.Show();
            }
            else
            {
                txtConsole.Text = "Bot is not online.";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (xd == "hello")
            {
                Audio_Player open = new Audio_Player(new DiscordSocketClient());
                open.Show();
            }
            else
            {
                txtConsole.Text="Bot is not online.";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (xd == "hello")
            {
                normal open = new normal();
                open.Show();
            }
            else
            {
                txtConsole.Text = "Bot is not online.";
            }
        }

        private static bool che = true;
        private void button5_Click(object sender, EventArgs e)
        {
            if (che == true)
                {
                button4.Show();
                    che = false;
                }
            else if (che == false)
            {
                button4.Hide();
                che = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        public async Task Stats()
        {
            PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            PerformanceCounter ramCounter = new PerformanceCounter("Process", "Working Set - Private", Process.GetCurrentProcess().ProcessName);
            using (var process = Process.GetCurrentProcess())
            {
                var time = DateTime.Now - process.StartTime; /* Subtracts current time and start time to get Uptime*/
                var sb = new StringBuilder();
                if (time.Days > 0)
                {
                    sb.Append($"{time.Days}d ");
                }
                if (time.Hours > 0)
                {
                    sb.Append($"{time.Hours}h ");
                }
                if (time.Minutes > 0)
                {
                    sb.Append($"{time.Minutes}m ");
                }
                sb.Append($"{time.Seconds}s ");
                TRNG.Text = sb.ToString();
            }

            string getCurrentCpuUsage()
            {
                return cpuCounter.NextValue() + "%";
            }
            int memsize = Convert.ToInt32((ramCounter.NextValue() / (int)(1024)) / 1024);
            CPU.Text = getCurrentCpuUsage();
            RAM.Text = memsize+"MB";

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Settings_Click(object sender, EventArgs e)
        {
            if (xd == "hello")
            {
                Bot_Settings open = new Bot_Settings(new DiscordSocketClient());
                open.Show();
            }
            else
            {
                txtConsole.Text = "Bot is not online.";
            }
        }
    }
}