using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Discord.Audio;
using Discord.WebSocket;

namespace Luxary
{
    public class Audio_Player : Form
    {
        private readonly DiscordSocketClient bot;
        private IAudioClient client;
        private IContainer components;
        private string currentpath;
        private Process currentsong;
        private ComboBox guild_combobox;
        private Button join_channel;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button play_song;
        private Button select_songs_folder;
        private readonly FolderBrowserDialog selectsongfolder;
        private ComboBox songs_combobox;
        private ComboBox vchannel_combobox;

        public Audio_Player(DiscordSocketClient bott)
        {
            InitializeComponent();
            bott = Program._client;
            bot = bott;
            foreach (var guild in bot.Guilds)
                guild_combobox.Items.Add(guild.Name);
            selectsongfolder = new FolderBrowserDialog();
            guild_combobox.SelectedIndexChanged += guild_combobox_SelectedIndexChanged;
        }

        private void guild_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            vchannel_combobox.Items.Clear();
            foreach (var guild in bot.Guilds)
                if (guild.Name == guild_combobox.Text)
                    foreach (SocketGuildChannel voiceChannel in guild.VoiceChannels)
                        vchannel_combobox.Items.Add(voiceChannel.Name);
        }

        private void vchannel_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void songs_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void join_channel_Click(object sender, EventArgs e)
        {
            if (guild_combobox.Text == "")
            {
                var num1 = (int) MessageBox.Show("Select a Guild first!", "Error");
            }
            else if (vchannel_combobox.Text == "")
            {
                var num2 = (int) MessageBox.Show("Select a Voice Channel first!", "Error");
            }
            else
            {
                joinChannel();
            }
        }

        private void select_songs_folder_Click(object sender, EventArgs e)
        {
            songs_combobox.Items.Clear();
            selectsongfolder.Description = "Select a folder containing songs";
            var num = (int) selectsongfolder.ShowDialog();
            currentpath = selectsongfolder.SelectedPath + "\\";
            if (!(selectsongfolder.SelectedPath != ""))
                return;
            foreach (var file in Directory.GetFiles(selectsongfolder.SelectedPath))
                if (file.EndsWith(".mp3") || file.EndsWith(".wav") || file.EndsWith(".m4a"))
                    songs_combobox.Items.Add(file.Remove(0, selectsongfolder.SelectedPath.Length + 1));
        }

        private void play_song_Click(object sender, EventArgs e)
        {
            if (guild_combobox.Text == "")
            {
                var num1 = (int) MessageBox.Show("Select a Guild first!", "Error");
            }
            else if (vchannel_combobox.Text == "")
            {
                var num2 = (int) MessageBox.Show("Select a Voice Channel first!", "Error");
            }
            else if (songs_combobox.Text == "")
            {
                var num3 = (int) MessageBox.Show("Select a Song first!", "Error");
            }
            else
            {
                var baseStream = CreateStream(songs_combobox.Text).StandardOutput.BaseStream;
                var pcmStream = client.CreatePCMStream(AudioApplication.Music, 131072, 1000);
                var audioOutStream = pcmStream;
                baseStream.CopyToAsync(audioOutStream);
                pcmStream.FlushAsync().ConfigureAwait(false);
            }
        }

        public async void joinChannel()
        {
            var audioPlayer1 = this;
            foreach (var guild in audioPlayer1.bot.Guilds)
            if (guild.Name == audioPlayer1.guild_combobox.Text)
            foreach (var voiceChannel in guild.VoiceChannels)
            if (voiceChannel.Name == audioPlayer1.vchannel_combobox.Text)
            try
            {
                var audioPlayer = audioPlayer1;
                var client = audioPlayer.client;

                var audioClient = await voiceChannel.ConnectAsync(null);
                audioPlayer.client = audioClient;
                audioPlayer = null;
            }
            catch
            {
                            
            }
        }

        private Process CreateStream(string path)
        {
            this.currentsong = new Process();
            var currentsong = this.currentsong;
            var processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = "ffmpeg.exe";
            var str = string.Format("-hide_banner -loglevel panic -i \"{0}\" -ac 2 -f s16le -ar 48000 pipe:1",
                currentpath + path);
            processStartInfo.Arguments = str;
            var num1 = 0;
            processStartInfo.UseShellExecute = num1 != 0;
            var num2 = 1;
            processStartInfo.RedirectStandardOutput = num2 != 0;
            var num3 = 1;
            processStartInfo.CreateNoWindow = num3 != 0;
            currentsong.StartInfo = processStartInfo;
            this.currentsong.Start();
            return this.currentsong;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            guild_combobox = new ComboBox();
            vchannel_combobox = new ComboBox();
            join_channel = new Button();
            select_songs_folder = new Button();
            songs_combobox = new ComboBox();
            play_song = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            guild_combobox.FormattingEnabled = true;
            guild_combobox.Location = new Point(78, 12);
            guild_combobox.Name = "guild_combobox";
            guild_combobox.Size = new Size(236, 24);
            guild_combobox.TabIndex = 0;
            guild_combobox.SelectedIndexChanged += guild_combobox_SelectedIndexChanged;
            vchannel_combobox.FormattingEnabled = true;
            vchannel_combobox.Location = new Point(78, 39);
            vchannel_combobox.Name = "vchannel_combobox";
            vchannel_combobox.Size = new Size(236, 24);
            vchannel_combobox.TabIndex = 1;
            vchannel_combobox.SelectedIndexChanged += vchannel_combobox_SelectedIndexChanged;
            join_channel.Location = new Point(78, 69);
            join_channel.Name = "join_channel";
            join_channel.Size = new Size(236, 26);
            join_channel.TabIndex = 2;
            join_channel.Text = "Join Channel";
            join_channel.UseVisualStyleBackColor = true;
            join_channel.Click += join_channel_Click;
            select_songs_folder.Location = new Point(78, 121);
            select_songs_folder.Name = "select_songs_folder";
            select_songs_folder.Size = new Size(236, 35);
            select_songs_folder.TabIndex = 3;
            select_songs_folder.Text = "Select Songs Folder";
            select_songs_folder.UseVisualStyleBackColor = true;
            select_songs_folder.Click += select_songs_folder_Click;
            songs_combobox.FormattingEnabled = true;
            songs_combobox.Location = new Point(78, 162);
            songs_combobox.Name = "songs_combobox";
            songs_combobox.Size = new Size(236, 24);
            songs_combobox.TabIndex = 4;
            songs_combobox.SelectedIndexChanged += songs_combobox_SelectedIndexChanged;
            play_song.Location = new Point(78, 192);
            play_song.Name = "play_song";
            play_song.Size = new Size(236, 49);
            play_song.TabIndex = 5;
            play_song.Text = "Play Song";
            play_song.UseVisualStyleBackColor = true;
            play_song.Click += play_song_Click;
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(41, 17);
            label1.TabIndex = 6;
            label1.Text = "Guild";
            label2.AutoSize = true;
            label2.Location = new Point(12, 42);
            label2.Name = "label2";
            label2.Size = new Size(60, 17);
            label2.TabIndex = 7;
            label2.Text = "Channel";
            label3.BorderStyle = BorderStyle.Fixed3D;
            label3.Location = new Point(0, 107);
            label3.Name = "label3";
            label3.Size = new Size(327, 2);
            label3.TabIndex = 8;
            label4.AutoSize = true;
            label4.Location = new Point(12, 165);
            label4.Name = "label4";
            label4.Size = new Size(41, 17);
            label4.TabIndex = 9;
            label4.Text = "Song";
            AutoScaleDimensions = new SizeF(8f, 16f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(326, 253);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(play_song);
            Controls.Add(songs_combobox);
            Controls.Add(select_songs_folder);
            Controls.Add(join_channel);
            Controls.Add(vchannel_combobox);
            Controls.Add(guild_combobox);
            MinimizeBox = false;
            Name = nameof(Audio_Player);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Audio Player";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}