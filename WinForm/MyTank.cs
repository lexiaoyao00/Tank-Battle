using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    internal class MyTank:MoveThing
    {
        public int HealthValue {  get; set; }
        public MyTank(int x, int y,int speed)
        {
            this.IsMoving = false;
            this.X = x; 
            this.Y = y; 
            this.Speed = speed;

            BitmapUp = Properties.Resources.MyTankUp;
            BitmapDown = Properties.Resources.MyTankDown;
            BitmapLeft = Properties.Resources.MyTankLeft;
            BitmapRight = Properties.Resources.MyTankRight;


            this.Dir = Direction.Up;
            this.HealthValue = 3;
            this.Tag = Tags.MyTank;
        }


        public void Move_KeyDown(KeyEventArgs p_key)
        {
            switch (p_key.KeyCode)
            {
                case Keys.W:
                    this.Dir = Direction.Up;
                    IsMoving = true;
                    break;
                case Keys.S:
                    this.Dir = Direction.Down;
                    IsMoving = true;
                    break;
                case Keys.D:
                    this.Dir = Direction.Right;
                    IsMoving = true;
                    break;
                case Keys.A:
                    this.Dir = Direction.Left;
                    IsMoving = true;
                    break;
                case Keys.Space:
                    Fire();
                    break;
            }
        }


        public void Fire()
        {
            SoundsManager.PlayFire();
            int x = this.X;
            int y = this.Y;

            switch (this.Dir)
            {
                case Direction.Up:
                    x = x + (this.Width / 2);
                    break;
                case Direction.Down:
                    x = x + (this.Width / 2);
                    y += this.Height;
                    break;
                case Direction.Left:
                    y = y + (this.Height / 2);
                    break;
                case Direction.Right:
                    x += this.Width;
                    y = y + (this.Height / 2);
                    break;
            }


            GameObjectManager.CreateBullet(x, y, Tags.MyTank, this.Dir);
        }

        public void Move_KeyUp(KeyEventArgs p_key)
        {
            switch (p_key.KeyCode)
            {
                case Keys.W:
                    IsMoving = false;
                    break;
                case Keys.S:
                    IsMoving = false;
                    break;
                case Keys.D:
                    IsMoving = false;
                    break;
                case Keys.A:
                    IsMoving = false;
                    break;
            }
        }


    }
}
