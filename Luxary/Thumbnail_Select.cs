using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Luxary
{
    public class Thumbnail_Selects : Form
    {
        private DiscordSocketClient bot;
        public string url;
        private IContainer components;
        private CheckBox url_check;
        private TextBox url_box;
        private ComboBox guild_combobox;
        private ComboBox user_combobox;
        private PictureBox image_box;
        private Label label1;
        private Label label2;
        private Button select_picture;
        private Button button1;
        private Button button2;
        private Button select_from_url;
        private Label label3;
        private Button use_picture;
        private Label label4;
        private Label label5;

        public Thumbnail_Selects(DiscordSocketClient bott)
        {
            this.InitializeComponent();
            bott = Program._client;
            bot = bott;
            this.url_box.Enabled = false;
            this.select_from_url.Enabled = false;
            foreach (SocketGuild guild in (IEnumerable<SocketGuild>)bot.Guilds)
                this.guild_combobox.Items.Add((object)guild.Name);
            this.guild_combobox.SelectedIndexChanged += new EventHandler(this.guild_combobox_SelectedIndexChanged);
            this.url_check.CheckedChanged += new EventHandler(this.Url_check_CheckedChanged);
        }

        private void Url_check_CheckedChanged(object sender, EventArgs e)
        {
            if (this.url_check.Checked)
            {
                this.url_box.Enabled = true;
                this.select_from_url.Enabled = true;
                this.guild_combobox.Enabled = false;
                this.user_combobox.Enabled = false;
                this.select_picture.Enabled = false;
            }
            else
            {
                this.url_box.Enabled = false;
                this.select_from_url.Enabled = false;
                this.guild_combobox.Enabled = true;
                this.user_combobox.Enabled = true;
                this.select_picture.Enabled = true;
            }
        }

        private void guild_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.user_combobox.Items.Clear();
            foreach (SocketGuild guild in (IEnumerable<SocketGuild>)this.bot.Guilds)
            {
                if (guild.Name == this.guild_combobox.Text)
                {
                    foreach (SocketUser user in (IEnumerable<SocketGuildUser>)guild.Users)
                        this.user_combobox.Items.Add((object)user.Username);
                }
            }
        }

        private void user_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void url_box_TextChanged(object sender, EventArgs e)
        {
        }

        private void image_box_Click(object sender, EventArgs e)
        {
        }

        private void url_check_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void select_picture_Click(object sender, EventArgs e)
        {
            if (this.guild_combobox.Text == "")
            {
                int num1 = (int)MessageBox.Show("Select a Guild first!", "Error");
            }
            else
            {
                if (this.user_combobox.Text == "")
                {
                    int num2 = (int)MessageBox.Show("Select a User first!", "Error");
                }
                foreach (SocketGuild guild in (IEnumerable<SocketGuild>)this.bot.Guilds)
                {
                    if (guild.Name == this.guild_combobox.Text)
                    {
                        foreach (SocketGuildUser user in (IEnumerable<SocketGuildUser>)guild.Users)
                        {
                            if (user.Username == this.user_combobox.Text)
                            {
                                this.url = user.GetAvatarUrl(ImageFormat.Auto, (ushort)128);
                                this.image_box.ImageLocation = this.url;
                                this.select_picture.Enabled = true;
                            }
                        }
                    }
                }
            }
        }

        private void select_from_url_Click(object sender, EventArgs e)
        {
            this.url = this.url_box.Text;
            this.image_box.ImageLocation = this.url;
            this.select_picture.Enabled = true;
        }

        private void use_picture_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.url_check = new CheckBox();
            this.url_box = new TextBox();
            this.guild_combobox = new ComboBox();
            this.user_combobox = new ComboBox();
            this.image_box = new PictureBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.select_picture = new Button();
            this.button1 = new Button();
            this.button2 = new Button();
            this.select_from_url = new Button();
            this.label3 = new Label();
            this.use_picture = new Button();
            this.label4 = new Label();
            this.label5 = new Label();
            ((ISupportInitialize)this.image_box).BeginInit();
            this.SuspendLayout();
            this.url_check.AutoSize = true;
            this.url_check.Location = new Point(12, 123);
            this.url_check.Name = "url_check";
            this.url_check.Size = new Size(94, 21);
            this.url_check.TabIndex = 0;
            this.url_check.Text = "From URL";
            this.url_check.UseVisualStyleBackColor = true;
            this.url_check.CheckedChanged += new EventHandler(this.url_check_CheckedChanged);
            this.url_box.Location = new Point(57, 150);
            this.url_box.Name = "url_box";
            this.url_box.Size = new Size(179, 22);
            this.url_box.TabIndex = 1;
            this.url_box.TextChanged += new EventHandler(this.url_box_TextChanged);
            this.guild_combobox.FormattingEnabled = true;
            this.guild_combobox.Location = new Point(57, 12);
            this.guild_combobox.Name = "guild_combobox";
            this.guild_combobox.Size = new Size(179, 24);
            this.guild_combobox.TabIndex = 2;
            this.guild_combobox.SelectedIndexChanged += new EventHandler(this.guild_combobox_SelectedIndexChanged);
            this.user_combobox.FormattingEnabled = true;
            this.user_combobox.Location = new Point(57, 42);
            this.user_combobox.Name = "user_combobox";
            this.user_combobox.Size = new Size(179, 24);
            this.user_combobox.TabIndex = 3;
            this.user_combobox.SelectedIndexChanged += new EventHandler(this.user_combobox_SelectedIndexChanged);
            this.image_box.BorderStyle = BorderStyle.FixedSingle;
            this.image_box.Location = new Point(12, 286);
            this.image_box.Name = "image_box";
            this.image_box.Size = new Size(223, 223);
            this.image_box.TabIndex = 4;
            this.image_box.TabStop = false;
            this.image_box.Click += new EventHandler(this.image_box_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(9, 14);
            this.label1.Name = "label1";
            this.label1.Size = new Size(41, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Guild";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(9, 45);
            this.label2.Name = "label2";
            this.label2.Size = new Size(38, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "User";
            this.select_picture.Location = new Point(12, 72);
            this.select_picture.Name = "select_picture";
            this.select_picture.Size = new Size(224, 26);
            this.select_picture.TabIndex = 7;
            this.select_picture.Text = "Select User Profile Picture";
            this.select_picture.UseVisualStyleBackColor = true;
            this.select_picture.Click += new EventHandler(this.select_picture_Click);
            this.button1.Location = new Point(12, 159);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0, 0);
            this.button1.TabIndex = 8;
            this.button1.Text = "Select Picture from URL";
            this.button1.UseVisualStyleBackColor = true;
            this.button2.Location = new Point(12, 159);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0, 0);
            this.button2.TabIndex = 9;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.select_from_url.Location = new Point(12, 178);
            this.select_from_url.Name = "select_from_url";
            this.select_from_url.Size = new Size(223, 26);
            this.select_from_url.TabIndex = 10;
            this.select_from_url.Text = "Select Picture from URL";
            this.select_from_url.UseVisualStyleBackColor = true;
            this.select_from_url.Click += new EventHandler(this.select_from_url_Click);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(9, 153);
            this.label3.Name = "label3";
            this.label3.Size = new Size(36, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "URL";
            this.use_picture.Location = new Point(12, 242);
            this.use_picture.Name = "use_picture";
            this.use_picture.Size = new Size(223, 26);
            this.use_picture.TabIndex = 12;
            this.use_picture.Text = "Use Picture";
            this.use_picture.UseVisualStyleBackColor = true;
            this.use_picture.Click += new EventHandler(this.use_picture_Click);
            this.label4.BorderStyle = BorderStyle.Fixed3D;
            this.label4.Location = new Point(0, 220);
            this.label4.Name = "label4";
            this.label4.Size = new Size(250, 2);
            this.label4.TabIndex = 13;
            this.label5.BorderStyle = BorderStyle.Fixed3D;
            this.label5.Location = new Point(0, 110);
            this.label5.Name = "label5";
            this.label5.Size = new Size(250, 2);
            this.label5.TabIndex = 14;
            this.AutoScaleDimensions = new SizeF(8f, 16f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(248, 521);
            this.Controls.Add((Control)this.label5);
            this.Controls.Add((Control)this.label4);
            this.Controls.Add((Control)this.use_picture);
            this.Controls.Add((Control)this.label3);
            this.Controls.Add((Control)this.select_from_url);
            this.Controls.Add((Control)this.button2);
            this.Controls.Add((Control)this.button1);
            this.Controls.Add((Control)this.select_picture);
            this.Controls.Add((Control)this.label2);
            this.Controls.Add((Control)this.label1);
            this.Controls.Add((Control)this.image_box);
            this.Controls.Add((Control)this.user_combobox);
            this.Controls.Add((Control)this.guild_combobox);
            this.Controls.Add((Control)this.url_box);
            this.Controls.Add((Control)this.url_check);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = nameof(Thumbnail_Selects);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Thumbnail Selector";
            ((ISupportInitialize)this.image_box).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
