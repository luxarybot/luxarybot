using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Discord;
using Discord.WebSocket;
using Color = Discord.Color;

namespace Luxary
{
    public class Embed_Messages : Form
    {
        private readonly DiscordSocketClient bot;
        private readonly ColorDialog pickcolor;
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
            InitializeComponent();
            bott = Program._client;
            bot = bott;
            foreach (var guild in bot.Guilds)
                guild_combobox.Items.Add(guild.Name);
            pickcolor = new ColorDialog();
            guild_combobox.SelectedIndexChanged += guild_combobox_SelectedIndexChanged;
        }

        [STAThread]
        private void select_thumbnail_Click(object sender, EventArgs e)
        {
            selector = new Thumbnail_Selects(bot);
            var num = (int) selector.ShowDialog();
        }

        private void select_color_Click(object sender, EventArgs e)
        {
            var num = (int) pickcolor.ShowDialog();
        }

        private void send_embed_Click(object sender, EventArgs e)
        {
            if (guild_combobox.Text == "")
            {
                var num1 = (int) MessageBox.Show("Select a Guild first!", "Error");
            }
            else if (tchannel_combobox.Text == "")
            {
                var num2 = (int) MessageBox.Show("Select a Text Channel first!", "Error");
            }
            else
            {
                var url = selector.url;
                sendMessage("", new EmbedBuilder
                {
                    Title = embed_title.Text,
                    Description = embed_description.Text,
                    ThumbnailUrl = url,
                    Color = new Color(pickcolor.Color.R, pickcolor.Color.G, pickcolor.Color.B)
                });
                var num3 = (int) MessageBox.Show("Successfully sent Message", "Success");
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
            tchannel_combobox.Items.Clear();
            foreach (var guild in bot.Guilds)
                if (guild.Name == guild_combobox.Text)
                    foreach (SocketGuildChannel textChannel in guild.TextChannels)
                        tchannel_combobox.Items.Add(textChannel.Name);
        }

        private void tchannel_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        public void sendMessage(string msg, EmbedBuilder embed = null)
        {
            foreach (var guild in bot.Guilds)
                if (guild.Name == guild_combobox.Text)
                    foreach (var textChannel in guild.TextChannels)
                        if (textChannel.Name == tchannel_combobox.Text)
                            textChannel.SendMessageAsync(msg, false, embed, null);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            select_color = new Button();
            embed_title = new TextBox();
            guild_combobox = new ComboBox();
            tchannel_combobox = new ComboBox();
            send_embed = new Button();
            select_thumbnail = new Button();
            label1 = new Label();
            label2 = new Label();
            embed_description = new RichTextBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            SuspendLayout();
            select_color.Location = new Point(97, 205);
            select_color.Name = "select_color";
            select_color.Size = new Size(389, 26);
            select_color.TabIndex = 0;
            select_color.Text = "Change Window Color";
            select_color.UseVisualStyleBackColor = true;
            select_color.Click += select_color_Click;
            embed_title.Location = new Point(97, 6);
            embed_title.Name = "embed_title";
            embed_title.Size = new Size(389, 22);
            embed_title.TabIndex = 1;
            embed_title.TextChanged += embed_title_TextChanged;
            guild_combobox.FormattingEnabled = true;
            guild_combobox.Location = new Point(97, 237);
            guild_combobox.Name = "guild_combobox";
            guild_combobox.Size = new Size(389, 24);
            guild_combobox.TabIndex = 3;
            guild_combobox.SelectedIndexChanged += guild_combobox_SelectedIndexChanged;
            tchannel_combobox.FormattingEnabled = true;
            tchannel_combobox.Location = new Point(97, 267);
            tchannel_combobox.Name = "tchannel_combobox";
            tchannel_combobox.Size = new Size(389, 24);
            tchannel_combobox.TabIndex = 4;
            tchannel_combobox.SelectedIndexChanged += tchannel_combobox_SelectedIndexChanged;
            send_embed.Location = new Point(97, 297);
            send_embed.Name = "send_embed";
            send_embed.Size = new Size(389, 28);
            send_embed.TabIndex = 5;
            send_embed.Text = "Send Message";
            send_embed.UseVisualStyleBackColor = true;
            send_embed.Click += send_embed_Click;
            select_thumbnail.Location = new Point(97, 173);
            select_thumbnail.Name = "select_thumbnail";
            select_thumbnail.Size = new Size(389, 26);
            select_thumbnail.TabIndex = 6;
            select_thumbnail.Text = "Select Thumbnail";
            select_thumbnail.UseVisualStyleBackColor = true;
            select_thumbnail.Click += select_thumbnail_Click;
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(35, 17);
            label1.TabIndex = 7;
            label1.Text = "Title";
            label2.AutoSize = true;
            label2.Location = new Point(12, 37);
            label2.Name = "label2";
            label2.Size = new Size(0, 17);
            label2.TabIndex = 8;
            embed_description.Location = new Point(97, 34);
            embed_description.Name = "embed_description";
            embed_description.Size = new Size(389, 133);
            embed_description.TabIndex = 2;
            embed_description.Text = "";
            embed_description.TextChanged += embed_description_TextChanged;
            label3.AutoSize = true;
            label3.Location = new Point(12, 37);
            label3.Name = "label3";
            label3.Size = new Size(79, 17);
            label3.TabIndex = 10;
            label3.Text = "Description";
            label4.AutoSize = true;
            label4.Location = new Point(12, 240);
            label4.Name = "label4";
            label4.Size = new Size(41, 17);
            label4.TabIndex = 11;
            label4.Text = "Guild";
            label5.AutoSize = true;
            label5.Location = new Point(12, 270);
            label5.Name = "label5";
            label5.Size = new Size(60, 17);
            label5.TabIndex = 12;
            label5.Text = "Channel";
            AutoScaleDimensions = new SizeF(8f, 16f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(498, 336);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(select_thumbnail);
            Controls.Add(send_embed);
            Controls.Add(tchannel_combobox);
            Controls.Add(guild_combobox);
            Controls.Add(embed_description);
            Controls.Add(embed_title);
            Controls.Add(select_color);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = nameof(Embed_Messages);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Embed Messages";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}