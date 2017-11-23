using Discord.Audio;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Luxary
{
    public class Audio_Player : Form
    {
        private IAudioClient client;
        private string currentpath;
        private DiscordSocketClient bot;
        private Process currentsong;
        private FolderBrowserDialog selectsongfolder;
        private IContainer components;
        private ComboBox guild_combobox;
        private ComboBox vchannel_combobox;
        private Button join_channel;
        private Button select_songs_folder;
        private ComboBox songs_combobox;
        private Button play_song;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;

        public Audio_Player(DiscordSocketClient bott)
        {
            this.InitializeComponent();
            bott = Program._client;
            bot = bott;
            foreach (SocketGuild guild in (IEnumerable<SocketGuild>)bot.Guilds)
                this.guild_combobox.Items.Add((object)guild.Name);
            this.selectsongfolder = new FolderBrowserDialog();
            this.guild_combobox.SelectedIndexChanged += new EventHandler(this.guild_combobox_SelectedIndexChanged);
        }

        private void guild_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.vchannel_combobox.Items.Clear();
            foreach (SocketGuild guild in (IEnumerable<SocketGuild>)this.bot.Guilds)
            {
                if (guild.Name == this.guild_combobox.Text)
                {
                    foreach (SocketGuildChannel voiceChannel in (IEnumerable<SocketVoiceChannel>)guild.VoiceChannels)
                        this.vchannel_combobox.Items.Add((object)voiceChannel.Name);
                }
            }
        }

        private void vchannel_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void songs_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void join_channel_Click(object sender, EventArgs e)
        {
            if (this.guild_combobox.Text == "")
            {
                int num1 = (int)MessageBox.Show("Select a Guild first!", "Error");
            }
            else if (this.vchannel_combobox.Text == "")
            {
                int num2 = (int)MessageBox.Show("Select a Voice Channel first!", "Error");
            }
            else
                this.joinChannel();
        }

        private void select_songs_folder_Click(object sender, EventArgs e)
        {
            this.songs_combobox.Items.Clear();
            this.selectsongfolder.Description = "Select a folder containing songs";
            int num = (int)this.selectsongfolder.ShowDialog();
            this.currentpath = this.selectsongfolder.SelectedPath + "\\";
            if (!(this.selectsongfolder.SelectedPath != ""))
                return;
            foreach (string file in Directory.GetFiles(this.selectsongfolder.SelectedPath))
            {
                if (file.EndsWith(".mp3") || file.EndsWith(".wav") || file.EndsWith(".m4a"))
                    this.songs_combobox.Items.Add((object)file.Remove(0, this.selectsongfolder.SelectedPath.Length + 1));
            }
        }

        private void play_song_Click(object sender, EventArgs e)
        {
            if (this.guild_combobox.Text == "")
            {
                int num1 = (int)MessageBox.Show("Select a Guild first!", "Error");
            }
            else if (this.vchannel_combobox.Text == "")
            {
                int num2 = (int)MessageBox.Show("Select a Voice Channel first!", "Error");
            }
            else if (this.songs_combobox.Text == "")
            {
                int num3 = (int)MessageBox.Show("Select a Song first!", "Error");
            }
            else
            {
                Stream baseStream = this.CreateStream(this.songs_combobox.Text).StandardOutput.BaseStream;
                AudioOutStream pcmStream = this.client.CreatePCMStream(AudioApplication.Music, new int?(131072), 1000);
                AudioOutStream audioOutStream = pcmStream;
                baseStream.CopyToAsync((Stream)audioOutStream);
                pcmStream.FlushAsync().ConfigureAwait(false);
            }
        }

        public async void joinChannel()
        {
            Audio_Player audioPlayer1 = this;
            foreach (SocketGuild guild in (IEnumerable<SocketGuild>)audioPlayer1.bot.Guilds)
            {
                if (guild.Name == audioPlayer1.guild_combobox.Text)
                {
                    foreach (SocketVoiceChannel voiceChannel in (IEnumerable<SocketVoiceChannel>)guild.VoiceChannels)
                    {
                        if (voiceChannel.Name == audioPlayer1.vchannel_combobox.Text)
                        {
                            try
                            {
                                Audio_Player audioPlayer = audioPlayer1;
                                IAudioClient client = audioPlayer.client;
                                IAudioClient audioClient = await voiceChannel.ConnectAsync((Action<IAudioClient>)null);
                                audioPlayer.client = audioClient;
                                audioPlayer = (Audio_Player)null;
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
        }

        private Process CreateStream(string path)
        {
            this.currentsong = new Process();
            Process currentsong = this.currentsong;
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = "ffmpeg.exe";
            string str = string.Format("-hide_banner -loglevel panic -i \"{0}\" -ac 2 -f s16le -ar 48000 pipe:1", (object)(this.currentpath + path));
            processStartInfo.Arguments = str;
            int num1 = 0;
            processStartInfo.UseShellExecute = num1 != 0;
            int num2 = 1;
            processStartInfo.RedirectStandardOutput = num2 != 0;
            int num3 = 1;
            processStartInfo.CreateNoWindow = num3 != 0;
            currentsong.StartInfo = processStartInfo;
            this.currentsong.Start();
            return this.currentsong;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.guild_combobox = new ComboBox();
            this.vchannel_combobox = new ComboBox();
            this.join_channel = new Button();
            this.select_songs_folder = new Button();
            this.songs_combobox = new ComboBox();
            this.play_song = new Button();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.SuspendLayout();
            this.guild_combobox.FormattingEnabled = true;
            this.guild_combobox.Location = new Point(78, 12);
            this.guild_combobox.Name = "guild_combobox";
            this.guild_combobox.Size = new Size(236, 24);
            this.guild_combobox.TabIndex = 0;
            this.guild_combobox.SelectedIndexChanged += new EventHandler(this.guild_combobox_SelectedIndexChanged);
            this.vchannel_combobox.FormattingEnabled = true;
            this.vchannel_combobox.Location = new Point(78, 39);
            this.vchannel_combobox.Name = "vchannel_combobox";
            this.vchannel_combobox.Size = new Size(236, 24);
            this.vchannel_combobox.TabIndex = 1;
            this.vchannel_combobox.SelectedIndexChanged += new EventHandler(this.vchannel_combobox_SelectedIndexChanged);
            this.join_channel.Location = new Point(78, 69);
            this.join_channel.Name = "join_channel";
            this.join_channel.Size = new Size(236, 26);
            this.join_channel.TabIndex = 2;
            this.join_channel.Text = "Join Channel";
            this.join_channel.UseVisualStyleBackColor = true;
            this.join_channel.Click += new EventHandler(this.join_channel_Click);
            this.select_songs_folder.Location = new Point(78, 121);
            this.select_songs_folder.Name = "select_songs_folder";
            this.select_songs_folder.Size = new Size(236, 35);
            this.select_songs_folder.TabIndex = 3;
            this.select_songs_folder.Text = "Select Songs Folder";
            this.select_songs_folder.UseVisualStyleBackColor = true;
            this.select_songs_folder.Click += new EventHandler(this.select_songs_folder_Click);
            this.songs_combobox.FormattingEnabled = true;
            this.songs_combobox.Location = new Point(78, 162);
            this.songs_combobox.Name = "songs_combobox";
            this.songs_combobox.Size = new Size(236, 24);
            this.songs_combobox.TabIndex = 4;
            this.songs_combobox.SelectedIndexChanged += new EventHandler(this.songs_combobox_SelectedIndexChanged);
            this.play_song.Location = new Point(78, 192);
            this.play_song.Name = "play_song";
            this.play_song.Size = new Size(236, 49);
            this.play_song.TabIndex = 5;
            this.play_song.Text = "Play Song";
            this.play_song.UseVisualStyleBackColor = true;
            this.play_song.Click += new EventHandler(this.play_song_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(41, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Guild";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new Size(60, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Channel";
            this.label3.BorderStyle = BorderStyle.Fixed3D;
            this.label3.Location = new Point(0, 107);
            this.label3.Name = "label3";
            this.label3.Size = new Size(327, 2);
            this.label3.TabIndex = 8;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(12, 165);
            this.label4.Name = "label4";
            this.label4.Size = new Size(41, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Song";
            this.AutoScaleDimensions = new SizeF(8f, 16f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(326, 253);
            this.Controls.Add((Control)this.label4);
            this.Controls.Add((Control)this.label3);
            this.Controls.Add((Control)this.label2);
            this.Controls.Add((Control)this.label1);
            this.Controls.Add((Control)this.play_song);
            this.Controls.Add((Control)this.songs_combobox);
            this.Controls.Add((Control)this.select_songs_folder);
            this.Controls.Add((Control)this.join_channel);
            this.Controls.Add((Control)this.vchannel_combobox);
            this.Controls.Add((Control)this.guild_combobox);
            this.MinimizeBox = false;
            this.Name = nameof(Audio_Player);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Audio Player";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
