// Decompiled with JetBrains decompiler
// Type: DiscordApp.Bot_Settings
// Assembly: DiscordApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1765A790-0CD1-4AD8-85D6-A495D8C0A045
// Assembly location: C:\Users\thijm\Desktop\DiscordApp.exe

using Discord;
using Discord.WebSocket;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Luxary
{
  public class Bot_Settings : Form
  {
    private DiscordSocketClient bot;
    private IContainer components;
    private TextBox username_box;
    private PictureBox image_box;
    private Label label2;
    private Label label3;
    private Button apply_changes;
    private Label game;
    private TextBox current_game;
    private Button apply_change;

    public Bot_Settings(DiscordSocketClient bott)
    {
          InitializeComponent();
          bott = Program._client;
          bot = bott;
          current_game.Text = bot.CurrentUser.Game.ToString();
          username_box.Text = bot.CurrentUser.Username;
          image_box.ImageLocation = bot.CurrentUser.GetAvatarUrl((ImageFormat) 0, (ushort) 128);
    }

    private void username_box_TextChanged(object sender, EventArgs e)
    {
    }

    private void current_game_TextChanged(object sender, EventArgs e)
    {
    }

    private void image_box_Click(object sender, EventArgs e)
    {
    }

    private void apply_change_Click(object sender, EventArgs e)
    {
        if ((bot.CurrentUser.Username != username_box.Text))
            bot.CurrentUser.ModifyAsync(u => u.Username = username_box.Text);
        
        if ((bot.CurrentUser.Game.ToString() != current_game.Text))
            return;
        bot.SetGameAsync(current_game.Text);
        bot.CurrentUser.ModifyAsync(u => u.Avatar = image_box.Image);
        }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.username_box = new TextBox();
      this.image_box = new PictureBox();
      this.label2 = new Label();
      this.label3 = new Label();
      this.game = new Label();
      this.current_game = new TextBox();
      this.apply_change = new Button();
      ((ISupportInitialize) this.image_box).BeginInit();
      this.SuspendLayout();
      this.username_box.Location = new Point(115, 6);
      this.username_box.Name = "username_box";
      this.username_box.Size = new Size(154, 22);
      this.username_box.TabIndex = 0;
      this.username_box.TextChanged += new EventHandler(this.username_box_TextChanged);
      this.image_box.Location = new Point(141, 114);
      this.image_box.Name = "image_box";
      this.image_box.Size = new Size(256, 256);
      this.image_box.TabIndex = 2;
      this.image_box.TabStop = false;
      this.image_box.Click += new EventHandler(this.image_box_Click);
      this.label2.AutoSize = true;
      this.label2.Location = new Point(12, 9);
      this.label2.Name = "label2";
      this.label2.Size = new Size(73, 17);
      this.label2.TabIndex = 4;
      this.label2.Text = "Username";
      this.label3.BorderStyle = BorderStyle.Fixed3D;
      this.label3.Location = new Point(-2, 100);
      this.label3.Name = "label3";
      this.label3.Size = new Size(284, 2);
      this.label3.TabIndex = 5;
      this.game.AutoSize = true;
      this.game.Location = new Point(12, 37);
      this.game.Name = "game";
      this.game.Size = new Size(97, 17);
      this.game.TabIndex = 7;
      this.game.Text = "Current Game";
      this.current_game.Location = new Point(115, 34);
      this.current_game.Name = "current_game";
      this.current_game.Size = new Size(154, 22);
      this.current_game.TabIndex = 10;
      this.current_game.TextChanged += new EventHandler(this.current_game_TextChanged);
      this.apply_change.Location = new Point(115, 63);
      this.apply_change.Name = "apply_change";
      this.apply_change.Size = new Size(154, 26);
      this.apply_change.TabIndex = 9;
      this.apply_change.Text = "Apply Changes";
      this.apply_change.UseVisualStyleBackColor = true;
      this.apply_change.Click += new EventHandler(this.apply_change_Click);
      this.AutoScaleDimensions = new SizeF(8f, 16f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(281, 254);
      this.Controls.Add((Control) this.apply_change);
      this.Controls.Add((Control) this.current_game);
      this.Controls.Add((Control) this.game);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.image_box);
      this.Controls.Add((Control) this.username_box);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.Name = nameof (Bot_Settings);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Bot Settings";
      ((ISupportInitialize) this.image_box).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
