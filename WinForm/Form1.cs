using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForm.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinForm
{
    public partial class Form1 : Form
    {
        private Thread t_GameMainThread;
        private static Graphics windowG;
        private static Bitmap bufferBmp;
        public Form1()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen; 

            windowG = this.CreateGraphics();

            bufferBmp = new Bitmap(450, 450);
            Graphics bmpG = Graphics.FromImage(bufferBmp);
            GameFramework.g_canvas = bmpG;

            GameFramework.Start();
            t_GameMainThread = new Thread(new ThreadStart(GameMainThread));
            t_GameMainThread.Start();

        }

        private static void GameMainThread()
        {
            //GameFramework.Start();

            int fps = 60;
            int sleepTime = 1000 / fps;

            while (true)
            {
                
                GameFramework.g_canvas.Clear(Color.Black);
                GameFramework.Update();


                windowG.DrawImage(bufferBmp,0,0);

                Thread.Sleep(sleepTime);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            t_GameMainThread.Abort();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            GameFramework.KeyDown(e);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            GameFramework.KeyUp(e);
        }
    }
}
