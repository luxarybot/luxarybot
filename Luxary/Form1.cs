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
            txtConsole.Clear();
            Program.Start();
        }

        private void txtConsole_TextChanged(object sender, EventArgs e)
        {

        }

        private void sleep_Click(object sender, EventArgs e)
        {
            txtConsole.Clear();
            Program.Stop();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MSG open = new MSG();
            open.Show();
        }
    }
}