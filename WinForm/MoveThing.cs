using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    enum Direction
    {
        Up=0,
        Down,
        Left,
        Right
    }
    enum Tags
    {
        MyTank,
        EnemyTank
    }
    internal class MoveThing:GameObject
    {
        public Tags Tag { get; set; }

        private Object _lock = new Object();

        public Bitmap BitmapUp {  get; set; }
        public Bitmap BitmapDown { get; set; }
        public Bitmap BitmapLeft { get; set; }
        public Bitmap BitmapRight { get; set; }
        public bool IsMoving { get; set; }
        public int Speed { get; set; }

        private Direction dir;
        public Direction Dir { get { return dir; }
            set
            {
                dir = value;
                Bitmap t_bmp = null;
                switch(dir)
                {
                    case Direction.Up:
                        t_bmp=this.BitmapUp;
                        break;
                    case Direction.Down:
                        t_bmp=this.BitmapDown;
                        break;
                    case Direction.Left:
                        t_bmp= this.BitmapLeft;
                        break;
                    case Direction.Right:
                        t_bmp=this.BitmapRight;
                        break;
                    default:
                        t_bmp=this.BitmapUp;
                        break;
                }

                lock (_lock)
                {
                    this.Width = t_bmp.Width;
                    this.Height = t_bmp.Height;
                }
                
            }
        }

        protected override Image GetImage()
        {
            Bitmap bitmap = null;
            switch (Dir)
            {
                case Direction.Up:
                    bitmap = BitmapUp;
                    break;
                case Direction.Down:
                    bitmap = BitmapDown;
                    break;
                case Direction.Left:
                    bitmap = BitmapLeft;
                    break;
                case Direction.Right:
                    bitmap = BitmapRight;
                    break;
                default:
                    bitmap = BitmapUp;
                    break;
            }

            bitmap.MakeTransparent(Color.Black);

            return bitmap;
        }

        public override void DrwaSelf()
        {
            lock (_lock)
            {
                base.DrwaSelf();
            }
        }



        public override void Update()
        {
            MoveCheck();
            Move();

            base.Update();
        }


        protected virtual void Move()
        {
            if (IsMoving == false) return;

            switch (Dir)
            {
                case Direction.Up:
                    Y -= Speed;
                    break;
                case Direction.Down:
                    Y += Speed;
                    break;
                case Direction.Left:
                    X -= Speed;
                    break;
                case Direction.Right:
                    X += Speed;
                    break;
            }
        }

        protected virtual void MoveCheck()
        {

            Rectangle rect = GetRectangle();

            //窗体范围检测
            if (Dir == Direction.Up)
            {
                rect.Y -= Speed;
                if (rect.Y < 0)
                {
                    IsMoving = false; return;
                }
            }
            else if (Dir == Direction.Down)
            {
                rect.Y += Speed;
                if (rect.Y + Height > 450)
                {
                    IsMoving = false; return;
                }
            }
            else if (Dir == Direction.Left)
            {
                rect.X -= Speed;
                if (rect.X - Speed < 0)
                {
                    IsMoving = false; return;
                }
            }
            else if (Dir == Direction.Right)
            {
                rect.X += Speed;
                if (rect.X + Width > 450)
                {
                    IsMoving = false; return;
                }
            }

            //碰撞检测
            if (GameObjectManager.IsCollidedWall(rect) != null)
            {
                IsMoving = false; return;
            }

            if (GameObjectManager.IsCollidedSteel(rect) != null)
            {
                IsMoving = false; return;
            }

            if (GameObjectManager.IsCollidedBoss(rect) != null)
            {
                IsMoving = false; return;
            }

            if ((this.Tag == Tags.EnemyTank) && (GameObjectManager.IsCollidedMyTank(rect) != null))
            {
                int xExplosion = this.X + this.Width / 2;
                int yExplosion = this.Y + this.Height / 2;
                MyTank mytank = GameObjectManager.IsCollidedMyTank(rect);
                if (mytank.HealthValue > 1)
                {
                    GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                    GameObjectManager.RebirthMyTank();
                }
                else
                {
                    GameObjectManager.GameOver();
                }
            }


        }




    }
}
