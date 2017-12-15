using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Discord.WebSocket;
using Discord;

namespace Luxary.Forms
{
    public partial class Settings : Form
    {
        private DiscordSocketClient bot;
        private static string path;
        public Settings(DiscordSocketClient bott)
        {
            InitializeComponent();
            bott = Program._client;
            bot = bott;
            game.Text = bot.CurrentUser.Game.ToString();
            username.Text = bot.CurrentUser.Username;
            image_box.ImageLocation = bot.CurrentUser.GetAvatarUrl((ImageFormat)0, (ushort)128);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.InitialDirectory + openFileDialog1.FileName;
                image_box.Image = System.Drawing.Image.FromFile(path);
            }
        }

        private void current_game_TextChanged(object sender, EventArgs e)
        {

        }

        private void apply_changes_Click(object sender, EventArgs e)
        {;
            bot.SetGameAsync(game.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var hehe = new Discord.Image(path);
            bot.CurrentUser.ModifyAsync(u => u.Avatar = hehe);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bot.CurrentUser.ModifyAsync(u => u.Username = username.Text);
        }
    }
}
