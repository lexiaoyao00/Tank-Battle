using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    enum GameState
    {
        Running,
        GameOver
    }

    internal class GameFramework
    {
        public static Graphics g_canvas;
        public static GameState gameState = GameState.Running;
        public static void Start()
        {
            SoundsManager.InitSound();
            GameObjectManager.Start();
            //GameObjectManager.CreateMap();
            //GameObjectManager.createMyTank();
            SoundsManager.PlayStart();

        }

        public static void Update()
        {
            if (gameState == GameState.Running) 
            { 
                GameObjectManager.Update();
            }
            else
            {
                GameOver();
            }


        }

        private static void GameOver()
        {
            Bitmap bmpGG = Properties.Resources.GameOver;
            int x = 450 / 2 - bmpGG.Width / 2;
            int y = 450 / 2 - bmpGG.Height / 2;
            g_canvas.DrawImage(bmpGG, x, y);
        }

        public static void KeyDown(KeyEventArgs p_key)
        {
            GameObjectManager.KeyDown(p_key);
        }

        public static void KeyUp(KeyEventArgs p_key)
        {
            GameObjectManager.KeyUp(p_key);
        }

    }
}
