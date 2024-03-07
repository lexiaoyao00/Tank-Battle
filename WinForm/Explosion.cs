using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForm
{
    internal class Explosion : GameObject
    {
        private int playSpeed = 2;
        private int playTimer = -1;
        private int bmpIndex = 0;
        private Bitmap[] bmpBlastArry = new Bitmap[]
        {
            Properties.Resources.EXP1,
            Properties.Resources.EXP2,
            Properties.Resources.EXP3,
            Properties.Resources.EXP4,
            Properties.Resources.EXP5
        };

        public Explosion(int x,int y)
        {
            for (int i = 0; i < bmpBlastArry.Length; i++)
            {
                bmpBlastArry[i].MakeTransparent(Color.Black);
            }

            this.X = x - bmpBlastArry[0].Width / 2;
            this.Y = y - bmpBlastArry[0].Height / 2;

        }
        protected override Image GetImage()
        {
            return this.bmpBlastArry[this.bmpIndex];
        }


        public override void Update()
        {

            if (this.bmpIndex <  this.bmpBlastArry.Length - 1)
            {
                this.playTimer++;
                this.bmpIndex = playTimer / playSpeed;
            }
            else
            {
                GameObjectManager.DestroyExplosion(this);
            }

            base.Update();


        }
    }
}
