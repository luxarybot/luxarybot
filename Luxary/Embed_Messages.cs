// Decompiled with JetBrains decompiler
// Type: DiscordApp.Embed_Messages
// Assembly: DiscordApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1765A790-0CD1-4AD8-85D6-A495D8C0A045
// Assembly location: C:\Users\thijm\Desktop\Release\DiscordApp.exe

using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Luxary
{
    public class Embed_Messages : Form
    {
        private DiscordSocketClient bot;
        private ColorDialog pickcolor;
        private Thumbnail_Selects selector;
        private IContainer components;
        private Button select_color;
        private TextBox embed_title;
        private ComboBox guild_combobox;
        private ComboBox tchannel_combobox;
        private Button send_embed;
        private Button select_thumbnail;
        private Label label1;
        private Label label2;
        private RichTextBox embed_description;
        private Label label3;
        private Label label4;
        private Label label5;

        public Embed_Messages(DiscordSocketClient bott)
        {
            this.InitializeComponent();
            bott = Program._client;
            bot = bott;
            foreach (SocketGuild guild in (IEnumerable<SocketGuild>)bot.Guilds)
                this.guild_combobox.Items.Add((object)guild.Name);
            this.pickcolor = new ColorDialog();
            this.guild_combobox.SelectedIndexChanged += new EventHandler(this.guild_combobox_SelectedIndexChanged);
        }

        [STAThread]
        private void select_thumbnail_Click(object sender, EventArgs e)
        {
            this.selector = new Thumbnail_Selects(this.bot);
            int num = (int)this.selector.ShowDialog();
        }

        private void select_color_Click(object sender, EventArgs e)
        {
            int num = (int)this.pickcolor.ShowDialog();
        }

        private void send_embed_Click(object sender, EventArgs e)
        {
            if (this.guild_combobox.Text == "")
            {
                int num1 = (int)MessageBox.Show("Select a Guild first!", "Error");
            }
            else if (this.tchannel_combobox.Text == "")
            {
                int num2 = (int)MessageBox.Show("Select a Text Channel first!", "Error");
            }
            else
            {
                string url = this.selector.url;
                this.sendMessage("", new EmbedBuilder()
                {
                    Title = this.embed_title.Text,
                    Description = this.embed_description.Text,
                    ThumbnailUrl = url,
                    Color = new Discord.Color?(new Discord.Color(this.pickcolor.Color.R, this.pickcolor.Color.G, this.pickcolor.Color.B))
                });
                int num3 = (int)MessageBox.Show("Successfully sent Message", "Success");
            }
        }

        private void embed_title_TextChanged(object sender, EventArgs e)
        {
        }

        private void embed_description_TextChanged(object sender, EventArgs e)
        {
        }

        private void guild_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tchannel_combobox.Items.Clear();
            foreach (SocketGuild guild in (IEnumerable<SocketGuild>)this.bot.Guilds)
            {
                if (guild.Name == this.guild_combobox.Text)
                {
                    foreach (SocketGuildChannel textChannel in (IEnumerable<SocketTextChannel>)guild.TextChannels)
                        this.tchannel_combobox.Items.Add((object)textChannel.Name);
                }
            }
        }

        private void tchannel_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        public void sendMessage(string msg, EmbedBuilder embed = null)
        {
            foreach (SocketGuild guild in (IEnumerable<SocketGuild>)this.bot.Guilds)
            {
                if (guild.Name == this.guild_combobox.Text)
                {
                    foreach (SocketTextChannel textChannel in (IEnumerable<SocketTextChannel>)guild.TextChannels)
                    {
                        if (textChannel.Name == this.tchannel_combobox.Text)
                            textChannel.SendMessageAsync(msg, false, (Embed)embed, (RequestOptions)null);
                    }
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.select_color = new Button();
            this.embed_title = new TextBox();
            this.guild_combobox = new ComboBox();
            this.tchannel_combobox = new ComboBox();
            this.send_embed = new Button();
            this.select_thumbnail = new Button();
            this.label1 = new Label();
            this.label2 = new Label();
            this.embed_description = new RichTextBox();
            this.label3 = new Label();
            this.label4 = new Label();
            this.label5 = new Label();
            this.SuspendLayout();
            this.select_color.Location = new Point(97, 205);
            this.select_color.Name = "select_color";
            this.select_color.Size = new Size(389, 26);
            this.select_color.TabIndex = 0;
            this.select_color.Text = "Change Window Color";
            this.select_color.UseVisualStyleBackColor = true;
            this.select_color.Click += new EventHandler(this.select_color_Click);
            this.embed_title.Location = new Point(97, 6);
            this.embed_title.Name = "embed_title";
            this.embed_title.Size = new Size(389, 22);
            this.embed_title.TabIndex = 1;
            this.embed_title.TextChanged += new EventHandler(this.embed_title_TextChanged);
            this.guild_combobox.FormattingEnabled = true;
            this.guild_combobox.Location = new Point(97, 237);
            this.guild_combobox.Name = "guild_combobox";
            this.guild_combobox.Size = new Size(389, 24);
            this.guild_combobox.TabIndex = 3;
            this.guild_combobox.SelectedIndexChanged += new EventHandler(this.guild_combobox_SelectedIndexChanged);
            this.tchannel_combobox.FormattingEnabled = true;
            this.tchannel_combobox.Location = new Point(97, 267);
            this.tchannel_combobox.Name = "tchannel_combobox";
            this.tchannel_combobox.Size = new Size(389, 24);
            this.tchannel_combobox.TabIndex = 4;
            this.tchannel_combobox.SelectedIndexChanged += new EventHandler(this.tchannel_combobox_SelectedIndexChanged);
            this.send_embed.Location = new Point(97, 297);
            this.send_embed.Name = "send_embed";
            this.send_embed.Size = new Size(389, 28);
            this.send_embed.TabIndex = 5;
            this.send_embed.Text = "Send Message";
            this.send_embed.UseVisualStyleBackColor = true;
            this.send_embed.Click += new EventHandler(this.send_embed_Click);
            this.select_thumbnail.Location = new Point(97, 173);
            this.select_thumbnail.Name = "select_thumbnail";
            this.select_thumbnail.Size = new Size(389, 26);
            this.select_thumbnail.TabIndex = 6;
            this.select_thumbnail.Text = "Select Thumbnail";
            this.select_thumbnail.UseVisualStyleBackColor = true;
            this.select_thumbnail.Click += new EventHandler(this.select_thumbnail_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(35, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Title";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0, 17);
            this.label2.TabIndex = 8;
            this.embed_description.Location = new Point(97, 34);
            this.embed_description.Name = "embed_description";
            this.embed_description.Size = new Size(389, 133);
            this.embed_description.TabIndex = 2;
            this.embed_description.Text = "";
            this.embed_description.TextChanged += new EventHandler(this.embed_description_TextChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(12, 37);
            this.label3.Name = "label3";
            this.label3.Size = new Size(79, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "Description";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(12, 240);
            this.label4.Name = "label4";
            this.label4.Size = new Size(41, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Guild";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(12, 270);
            this.label5.Name = "label5";
            this.label5.Size = new Size(60, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "Channel";
            this.AutoScaleDimensions = new SizeF(8f, 16f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(498, 336);
            this.Controls.Add((Control)this.label5);
            this.Controls.Add((Control)this.label4);
            this.Controls.Add((Control)this.label3);
            this.Controls.Add((Control)this.label2);
            this.Controls.Add((Control)this.label1);
            this.Controls.Add((Control)this.select_thumbnail);
            this.Controls.Add((Control)this.send_embed);
            this.Controls.Add((Control)this.tchannel_combobox);
            this.Controls.Add((Control)this.guild_combobox);
            this.Controls.Add((Control)this.embed_description);
            this.Controls.Add((Control)this.embed_title);
            this.Controls.Add((Control)this.select_color);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = nameof(Embed_Messages);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Embed Messages";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
