using System.Windows.Forms;

namespace Luxary.Forms
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.username = new System.Windows.Forms.TextBox();
            this.game = new System.Windows.Forms.TextBox();
            this.image_box = new System.Windows.Forms.PictureBox();
            this.apply_changes = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.image_box)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 97);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Select AVA";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Username:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Current game:";
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(90, 22);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(152, 20);
            this.username.TabIndex = 3;
            this.username.TextChanged += new System.EventHandler(this.current_game_TextChanged);
            // 
            // game
            // 
            this.game.Location = new System.Drawing.Point(90, 54);
            this.game.Name = "game";
            this.game.Size = new System.Drawing.Size(152, 20);
            this.game.TabIndex = 4;
            // 
            // image_box
            // 
            this.image_box.Location = new System.Drawing.Point(90, 97);
            this.image_box.Name = "image_box";
            this.image_box.Size = new System.Drawing.Size(152, 152);
            this.image_box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.image_box.TabIndex = 5;
            this.image_box.TabStop = false;
            // 
            // apply_changes
            // 
            this.apply_changes.Location = new System.Drawing.Point(9, 197);
            this.apply_changes.Name = "apply_changes";
            this.apply_changes.Size = new System.Drawing.Size(75, 23);
            this.apply_changes.TabIndex = 6;
            this.apply_changes.Text = "Apply Game";
            this.apply_changes.UseVisualStyleBackColor = true;
            this.apply_changes.Click += new System.EventHandler(this.apply_changes_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(9, 168);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Apply AVA";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(9, 226);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "Apply Name";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 261);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.apply_changes);
            this.Controls.Add(this.image_box);
            this.Controls.Add(this.game);
            this.Controls.Add(this.username);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Settings";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.image_box)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.TextBox game;
        private System.Windows.Forms.PictureBox image_box;
        private System.Windows.Forms.Button apply_changes;
        private System.Windows.Forms.Button button2;
        private Button button3;
    }
}