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
using System.Threading;

namespace Luxary
{
    public partial class FormConsole : Form
    {
        TextWriter _writer = null;
        private static string xd = "hi";
        public FormConsole()
        {
            InitializeComponent();
        }

        private void FormConsole_Load(object sender, EventArgs e)
        {
            _writer = new TextBoxStreamWriter(txtConsole);
            Console.SetOut(_writer);
        }
        private void txtSayHello_Click(object sender, EventArgs e)
        {
            FormConsole form1 = this;
            if (xd == "hi")
            {
                txtConsole.Clear();
                Program.Start();
                xd = "hello";
            }
        }
        private void txtConsole_TextChanged(object sender, EventArgs e)
        {

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
    }
}