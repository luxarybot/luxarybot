using System;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Reflection;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Timer = System.Timers.Timer;

namespace Luxary
{
    public partial class normal : Form
    {
        public normal()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private static Timer aTimer;
        private static int id;
        private static int idd;
        private static int pid;
        private static int idd2;

        private void button1_Click(object sender, EventArgs e)
        {           
            label3.Text = "";
            id = 0;
            idd = 0;
            pid = 0;
            GoingTimer();
        }
        List<string> myList = new List<string>();
        private Bitmap MyImage;
        private static string img;
        public async Task GoingTimer()
        {
            var xd = textBox2.Text.Replace(" ", "_");
            string url = $"https://rule34.xxx/index.php?page=dapi&s=post&q=index&tags={xd}&pid={pid}";
            XmlDocument Doc = new XmlDocument();
            Doc.Load(url);
            XmlNodeList itemList = Doc.DocumentElement.SelectNodes("post");
            foreach (XmlNode currNode in itemList)
            {
                string date = string.Empty;
                date = currNode.Attributes["file_url"].Value;
                myList.Add(date);
            }
            try
            {
                for (int i = 0; i < myList.Count; i++)
                {
                    string input = "abcdefghijklmnopqrstuvwxyz0123456789";
                    char ch;
                    string chars = @"\";
                    string randomString2 = $"{xd}_{idd}_";
                    Random rand = new Random();
                    for (int id = 0; id < 8; id++)
                    {
                        ch = input[rand.Next(0, input.Length)];
                        randomString2 += ch;
                    }
                    if (MyImage != null)
                    {
                        MyImage.Dispose();
                    }
                    string randomString = myList[id++];
                    string localFilename = textBox1.Text + chars + randomString2 + ".jpg";

                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(randomString, localFilename);
                    }
                    try
                    {
                        System.Drawing.Image image = new Bitmap(localFilename);
                        pb1.Image = image;
                        pb1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    catch
                    {
                        
                    }
                    label3.Text = $"Downloaded: {idd} images";
                    idd++;
                }
            }
            catch (Exception ee)
            {
                
            }
            pid++;
            await GoingTimer();
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            aTimer.Stop();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
        private void folderBrowserDialog1_HelpRequest_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btn_LogOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void pb1_Click(object sender, EventArgs e)
        {

        }
    }
}
