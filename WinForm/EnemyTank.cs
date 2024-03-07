using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForm
{
    internal class EnemyTank : MoveThing
    {
        private int fireSpeed = 60;
        private int fireTimer = 0;
        private Random rand = new Random();
        private int turnTimer = 0;
        private int turnSpeed = 100;
        public EnemyTank(int x, int y, int speed,
            Bitmap p_BitmapUp,
            Bitmap p_BitmapDown,
            Bitmap p_BitmapLeft,
            Bitmap p_BitmapRight)
        {
            this.IsMoving = true;
            this.X = x;
            this.Y = y;
            this.Speed = speed;

            this.BitmapUp = p_BitmapUp;
            this.BitmapDown = p_BitmapDown;
            this.BitmapLeft = p_BitmapLeft;
            this.BitmapRight = p_BitmapRight;

            this.Dir = Direction.Down;
            this.Tag = Tags.EnemyTank;
        }


        public override void Update()
        {
            this.turnTimer++;
            if (this.turnTimer > this.turnSpeed)
            {
                this.IsMoving = false;
                this.turnTimer = 0;
            }
            ChangeDirection();
            FireCheck();
            base.Update();
        }

        private void ChangeDirection()
        {

            while (!this.IsMoving)
            {

                Direction t_dir = (Direction)rand.Next(0, 4);

                if (t_dir == this.Dir)
                {
                    continue;
                }
                else
                {
                    this.Dir = t_dir;
                    this.IsMoving = true;
                    break;
                }
            }

        }



        public void FireCheck()
        {

            this.fireTimer = ++this.fireTimer % this.fireSpeed;
            if (this.fireTimer == 0)
            {
                this.Fire();
            }
        }

        public void Fire()
        {
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

            GameObjectManager.CreateBullet(x, y, Tags.EnemyTank, this.Dir);
        }
    }
}
