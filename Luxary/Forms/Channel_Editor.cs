using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Luxary
{
    public class Channel_Editor : Form
    {
        private DiscordSocketClient bot;
        private IContainer components;
        private Label guild_label;
        private ComboBox guild_combobox;
        private Label label1;
        private Label label2;
        private ComboBox vchannel_combobox;
        private ComboBox tchannel_combobox;
        private Label label3;
        private Label label4;
        private TextBox vchannel_name;
        private Label label5;
        private TextBox tchannel_name;
        private Label label6;
        private Button apply_changes;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private TextBox vchannel_position;
        private TextBox tchannel_position;
        private Button create_channel;
        private TextBox channel_position;
        private Label label11;
        private Label label12;
        private TextBox channel_name;
        private RadioButton rvchannel;
        private RadioButton rtchannel;

        public Channel_Editor(DiscordSocketClient bott)
        {           
            this.InitializeComponent();
            bott = Program._client;
            bot = bott;
            foreach (SocketGuild guild in (IEnumerable<SocketGuild>)bott.Guilds)
                this.guild_combobox.Items.Add((object)guild.Name);
            this.guild_combobox.SelectedIndexChanged += new EventHandler(this.guild_combobox_SelectedIndexChanged);
            this.vchannel_combobox.SelectedIndexChanged += new EventHandler(this.vchannel_combobox_SelectedIndexChanged);
            this.tchannel_combobox.SelectedIndexChanged += new EventHandler(this.tchannel_combobox_SelectedIndexChanged);
        }

        private void guild_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tchannel_combobox.Items.Clear();
            this.vchannel_combobox.Items.Clear();
            this.tchannel_name.Text = "";
            this.vchannel_name.Text = "";
            foreach (SocketGuild guild in (IEnumerable<SocketGuild>)this.bot.Guilds)
            {
                if (guild.Name == this.guild_combobox.Text)
                {
                    foreach (SocketGuildChannel textChannel in (IEnumerable<SocketTextChannel>)guild.TextChannels)
                        this.tchannel_combobox.Items.Add((object)textChannel.Name);
                    foreach (SocketGuildChannel voiceChannel in (IEnumerable<SocketVoiceChannel>)guild.VoiceChannels)
                        this.vchannel_combobox.Items.Add((object)voiceChannel.Name);
                }
            }
        }

        private void vchannel_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.vchannel_name.Text = this.getVChannel().Name;
            this.vchannel_position.Text = this.getVChannel().Position.ToString();
        }

        private void tchannel_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tchannel_name.Text = this.getTChannel().Name;
            this.tchannel_position.Text = this.getTChannel().Position.ToString();
        }

        private void apply_changes_Click(object sender, EventArgs e)
        {
            if (this.guild_combobox.Text == "")
            {
                int num1 = (int)MessageBox.Show("Select a Guild first!", "Error");
            }
            else if (this.vchannel_combobox.Text == "" && this.tchannel_combobox.Text == "")
            {
                int num2 = (int)MessageBox.Show("Select a Channel first!", "Error");
            }
            else
            {
                short result;
                if (this.vchannel_combobox.Text != "")
                {
                    if (!short.TryParse(this.vchannel_position.Text, out result))
                    {
                        int num3 = (int)MessageBox.Show("Error while reading Channel Position", "Error");
                        return;
                    }
                    this.getVChannel().ModifyAsync((Action<VoiceChannelProperties>)(u => u.Name = (Optional<string>)this.vchannel_name.Text), (RequestOptions)null);
                    this.getVChannel().ModifyAsync((Action<VoiceChannelProperties>)(u => u.Position = (Optional<int>)((int)short.Parse(this.vchannel_position.Text))), (RequestOptions)null);
                }
                else if (this.tchannel_combobox.Text != "")
                {
                    if (!short.TryParse(this.tchannel_position.Text, out result))
                    {
                        int num3 = (int)MessageBox.Show("Error while reading Channel Position", "Error");
                        return;
                    }
                    if (!this.tchannel_name.Text.Contains(" "))
                    {
                        this.getTChannel().ModifyAsync((Action<TextChannelProperties>)(u => u.Name = (Optional<string>)this.tchannel_name.Text), (RequestOptions)null);
                        this.getTChannel().ModifyAsync((Action<TextChannelProperties>)(u => u.Position = (Optional<int>)((int)short.Parse(this.tchannel_position.Text))), (RequestOptions)null);
                    }
                    else
                    {
                        int num3 = (int)MessageBox.Show("Text Channel Names must be alphanumeric with dashes or underscores!", "Error");
                        return;
                    }
                }
                this.vchannel_combobox.Items.Clear();
                this.tchannel_combobox.Items.Clear();
                int num4 = (int)MessageBox.Show("Succesfully edited Channel", "Success");
                foreach (SocketGuild guild in (IEnumerable<SocketGuild>)this.bot.Guilds)
                {
                    if (guild.Name == this.guild_combobox.Text)
                    {
                        foreach (SocketGuildChannel textChannel in (IEnumerable<SocketTextChannel>)guild.TextChannels)
                            this.tchannel_combobox.Items.Add((object)textChannel.Name);
                        foreach (SocketGuildChannel voiceChannel in (IEnumerable<SocketVoiceChannel>)guild.VoiceChannels)
                            this.vchannel_combobox.Items.Add((object)voiceChannel.Name);
                    }
                }
                this.vchannel_combobox.Refresh();
                this.tchannel_combobox.Refresh();
            }
        }

        private void create_channel_Click(object sender, EventArgs e)
        {
            if (this.guild_combobox.Text == "")
            {
                int num1 = (int)MessageBox.Show("Select a Guild first!", "Error");
            }
            else
            {
                short result;
                if (!short.TryParse(this.channel_position.Text, out result))
                {
                    int num2 = (int)MessageBox.Show("Error while reading Channel Position", "Error");
                }
                else if (!this.rvchannel.Checked && !this.rtchannel.Checked)
                {
                    int num3 = (int)MessageBox.Show("Please select a Channel Type first!", "Error");
                }
                else if (this.rvchannel.Checked)
                {
                    this.getGuild().CreateVoiceChannelAsync(this.channel_name.Text, (RequestOptions)null).Result.ModifyAsync((Action<VoiceChannelProperties>)(u => u.Position = (Optional<int>)((int)short.Parse(this.channel_position.Text) - 3)), (RequestOptions)null);
                    int num4 = (int)MessageBox.Show("Succesfully created Channel", "Success");
                }
                else if (!this.channel_name.Text.Contains(" "))
                {
                    this.getGuild().CreateTextChannelAsync(this.channel_name.Text, (RequestOptions)null).Result.ModifyAsync((Action<TextChannelProperties>)(u => u.Position = (Optional<int>)((int)short.Parse(this.channel_position.Text) - 4)), (RequestOptions)null);
                    int num4 = (int)MessageBox.Show("Succesfully created Channel", "Success");
                }
                else
                {
                    int num5 = (int)MessageBox.Show("Text Channel Names must be alphanumeric with dashes or underscores!", "Error");
                }
            }
        }

        private void vchannel_name_TextChanged(object sender, EventArgs e)
        {
        }

        private void tchannel_name_TextChanged(object sender, EventArgs e)
        {
        }

        private void channel_name_TextChanged(object sender, EventArgs e)
        {
        }

        private void channel_position_TextChanged(object sender, EventArgs e)
        {
        }

        private void vchannel_position_SelectedItemChanged(object sender, EventArgs e)
        {
        }

        private void tchannel_position_SelectedItemChanged(object sender, EventArgs e)
        {
        }

        private void rvchannel_CheckedChanged(object sender, EventArgs e)
        {
            this.rvchannel.IsAccessible = true;
            this.rtchannel.IsAccessible = false;
        }

        private void rtchannel_CheckedChanged(object sender, EventArgs e)
        {
            this.rvchannel.IsAccessible = false;
            this.rtchannel.IsAccessible = true;
        }

        private SocketTextChannel getTChannel()
        {
            foreach (SocketGuild guild in (IEnumerable<SocketGuild>)this.bot.Guilds)
            {
                if (guild.Name == this.guild_combobox.Text)
                {
                    foreach (SocketTextChannel textChannel in (IEnumerable<SocketTextChannel>)guild.TextChannels)
                    {
                        if (textChannel.Name == this.tchannel_combobox.Text)
                            return textChannel;
                    }
                }
            }
            return (SocketTextChannel)null;
        }

        private SocketVoiceChannel getVChannel()
        {
            foreach (SocketGuild guild in (IEnumerable<SocketGuild>)this.bot.Guilds)
            {
                if (guild.Name == this.guild_combobox.Text)
                {
                    foreach (SocketVoiceChannel voiceChannel in (IEnumerable<SocketVoiceChannel>)guild.VoiceChannels)
                    {
                        if (voiceChannel.Name == this.vchannel_combobox.Text)
                            return voiceChannel;
                    }
                }
            }
            return (SocketVoiceChannel)null;
        }

        private SocketGuild getGuild()
        {
            foreach (SocketGuild guild in (IEnumerable<SocketGuild>)this.bot.Guilds)
            {
                if (guild.Name == this.guild_combobox.Text)
                    return guild;
            }
            return (SocketGuild)null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.guild_label = new Label();
            this.guild_combobox = new ComboBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.vchannel_combobox = new ComboBox();
            this.tchannel_combobox = new ComboBox();
            this.label3 = new Label();
            this.label4 = new Label();
            this.vchannel_name = new TextBox();
            this.label5 = new Label();
            this.tchannel_name = new TextBox();
            this.label6 = new Label();
            this.apply_changes = new Button();
            this.label7 = new Label();
            this.label8 = new Label();
            this.label9 = new Label();
            this.label10 = new Label();
            this.vchannel_position = new TextBox();
            this.tchannel_position = new TextBox();
            this.create_channel = new Button();
            this.channel_position = new TextBox();
            this.label11 = new Label();
            this.label12 = new Label();
            this.channel_name = new TextBox();
            this.rvchannel = new RadioButton();
            this.rtchannel = new RadioButton();
            this.SuspendLayout();
            this.guild_label.AutoSize = true;
            this.guild_label.Location = new Point(12, 15);
            this.guild_label.Name = "guild_label";
            this.guild_label.Size = new Size(41, 17);
            this.guild_label.TabIndex = 0;
            this.guild_label.Text = "Guild";
            this.guild_combobox.FormattingEnabled = true;
            this.guild_combobox.Location = new Point(59, 12);
            this.guild_combobox.Name = "guild_combobox";
            this.guild_combobox.Size = new Size(252, 24);
            this.guild_combobox.TabIndex = 1;
            this.guild_combobox.SelectedIndexChanged += new EventHandler(this.guild_combobox_SelectedIndexChanged);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 55);
            this.label1.Name = "label1";
            this.label1.Size = new Size(99, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Voice Channel";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(163, 55);
            this.label2.Name = "label2";
            this.label2.Size = new Size(91, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Text Channel";
            this.vchannel_combobox.FormattingEnabled = true;
            this.vchannel_combobox.Location = new Point(15, 76);
            this.vchannel_combobox.Name = "vchannel_combobox";
            this.vchannel_combobox.Size = new Size(145, 24);
            this.vchannel_combobox.TabIndex = 4;
            this.vchannel_combobox.SelectedIndexChanged += new EventHandler(this.vchannel_combobox_SelectedIndexChanged);
            this.tchannel_combobox.FormattingEnabled = true;
            this.tchannel_combobox.Location = new Point(166, 76);
            this.tchannel_combobox.Name = "tchannel_combobox";
            this.tchannel_combobox.Size = new Size(145, 24);
            this.tchannel_combobox.TabIndex = 5;
            this.tchannel_combobox.SelectedIndexChanged += new EventHandler(this.tchannel_combobox_SelectedIndexChanged);
            this.label3.BorderStyle = BorderStyle.Fixed3D;
            this.label3.Location = new Point(0, 110);
            this.label3.Name = "label3";
            this.label3.Size = new Size(333, 2);
            this.label3.TabIndex = 6;
            this.label4.BorderStyle = BorderStyle.Fixed3D;
            this.label4.Location = new Point(0, 46);
            this.label4.Name = "label4";
            this.label4.Size = new Size(333, 2);
            this.label4.TabIndex = 7;
            this.vchannel_name.Location = new Point(15, 142);
            this.vchannel_name.Name = "vchannel_name";
            this.vchannel_name.Size = new Size(145, 22);
            this.vchannel_name.TabIndex = 8;
            this.vchannel_name.TextChanged += new EventHandler(this.vchannel_name_TextChanged);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(13, 122);
            this.label5.Name = "label5";
            this.label5.Size = new Size(45, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Name";
            this.tchannel_name.Location = new Point(166, 142);
            this.tchannel_name.Name = "tchannel_name";
            this.tchannel_name.Size = new Size(145, 22);
            this.tchannel_name.TabIndex = 10;
            this.tchannel_name.TextChanged += new EventHandler(this.tchannel_name_TextChanged);
            this.label6.AutoSize = true;
            this.label6.Location = new Point(163, 122);
            this.label6.Name = "label6";
            this.label6.Size = new Size(45, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "Name";
            this.apply_changes.Location = new Point(15, 235);
            this.apply_changes.Name = "apply_changes";
            this.apply_changes.Size = new Size(296, 33);
            this.apply_changes.TabIndex = 12;
            this.apply_changes.Text = "Apply Changes";
            this.apply_changes.UseVisualStyleBackColor = true;
            this.apply_changes.Click += new EventHandler(this.apply_changes_Click);
            this.label7.BorderStyle = BorderStyle.Fixed3D;
            this.label7.Location = new Point(0, 223);
            this.label7.Name = "label7";
            this.label7.Size = new Size(333, 2);
            this.label7.TabIndex = 13;
            this.label8.AutoSize = true;
            this.label8.Location = new Point(12, 167);
            this.label8.Name = "label8";
            this.label8.Size = new Size(58, 17);
            this.label8.TabIndex = 15;
            this.label8.Text = "Position";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(163, 167);
            this.label9.Name = "label9";
            this.label9.Size = new Size(58, 17);
            this.label9.TabIndex = 17;
            this.label9.Text = "Position";
            this.label10.BorderStyle = BorderStyle.Fixed3D;
            this.label10.Location = new Point(0, 280);
            this.label10.Name = "label10";
            this.label10.Size = new Size(333, 2);
            this.label10.TabIndex = 18;
            this.vchannel_position.Location = new Point(15, 187);
            this.vchannel_position.Name = "vchannel_position";
            this.vchannel_position.Size = new Size(145, 22);
            this.vchannel_position.TabIndex = 19;
            this.tchannel_position.Location = new Point(166, 187);
            this.tchannel_position.Name = "tchannel_position";
            this.tchannel_position.Size = new Size(145, 22);
            this.tchannel_position.TabIndex = 20;
            this.create_channel.Location = new Point(15, 384);
            this.create_channel.Name = "create_channel";
            this.create_channel.Size = new Size(296, 33);
            this.create_channel.TabIndex = 21;
            this.create_channel.Text = "Create Channel";
            this.create_channel.UseVisualStyleBackColor = true;
            this.create_channel.Click += new EventHandler(this.create_channel_Click);
            this.channel_position.Location = new Point(16, 355);
            this.channel_position.Name = "channel_position";
            this.channel_position.Size = new Size(145, 22);
            this.channel_position.TabIndex = 25;
            this.channel_position.TextChanged += new EventHandler(this.channel_position_TextChanged);
            this.label11.AutoSize = true;
            this.label11.Location = new Point(13, 335);
            this.label11.Name = "label11";
            this.label11.Size = new Size(58, 17);
            this.label11.TabIndex = 24;
            this.label11.Text = "Position";
            this.label12.AutoSize = true;
            this.label12.Location = new Point(14, 290);
            this.label12.Name = "label12";
            this.label12.Size = new Size(45, 17);
            this.label12.TabIndex = 23;
            this.label12.Text = "Name";
            this.channel_name.Location = new Point(16, 310);
            this.channel_name.Name = "channel_name";
            this.channel_name.Size = new Size(145, 22);
            this.channel_name.TabIndex = 22;
            this.channel_name.TextChanged += new EventHandler(this.channel_name_TextChanged);
            this.rvchannel.AutoSize = true;
            this.rvchannel.Location = new Point(201, 310);
            this.rvchannel.Name = "rvchannel";
            this.rvchannel.Size = new Size(120, 21);
            this.rvchannel.TabIndex = 26;
            this.rvchannel.TabStop = true;
            this.rvchannel.Text = "Voice Channel";
            this.rvchannel.UseVisualStyleBackColor = true;
            this.rvchannel.CheckedChanged += new EventHandler(this.rvchannel_CheckedChanged);
            this.rtchannel.AutoSize = true;
            this.rtchannel.Location = new Point(201, 333);
            this.rtchannel.Name = "rtchannel";
            this.rtchannel.Size = new Size(112, 21);
            this.rtchannel.TabIndex = 27;
            this.rtchannel.TabStop = true;
            this.rtchannel.Text = "Text Channel";
            this.rtchannel.UseVisualStyleBackColor = true;
            this.rtchannel.CheckedChanged += new EventHandler(this.rtchannel_CheckedChanged);
            this.AutoScaleDimensions = new SizeF(8f, 16f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(332, 429);
            this.Controls.Add((Control)this.rtchannel);
            this.Controls.Add((Control)this.rvchannel);
            this.Controls.Add((Control)this.channel_position);
            this.Controls.Add((Control)this.label11);
            this.Controls.Add((Control)this.label12);
            this.Controls.Add((Control)this.channel_name);
            this.Controls.Add((Control)this.create_channel);
            this.Controls.Add((Control)this.tchannel_position);
            this.Controls.Add((Control)this.vchannel_position);
            this.Controls.Add((Control)this.label10);
            this.Controls.Add((Control)this.label9);
            this.Controls.Add((Control)this.label8);
            this.Controls.Add((Control)this.label7);
            this.Controls.Add((Control)this.apply_changes);
            this.Controls.Add((Control)this.label6);
            this.Controls.Add((Control)this.tchannel_name);
            this.Controls.Add((Control)this.label5);
            this.Controls.Add((Control)this.vchannel_name);
            this.Controls.Add((Control)this.label4);
            this.Controls.Add((Control)this.label3);
            this.Controls.Add((Control)this.tchannel_combobox);
            this.Controls.Add((Control)this.vchannel_combobox);
            this.Controls.Add((Control)this.label2);
            this.Controls.Add((Control)this.label1);
            this.Controls.Add((Control)this.guild_combobox);
            this.Controls.Add((Control)this.guild_label);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = nameof(Channel_Editor);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Channel Editor";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
