using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForm
{
    internal class Bullet : MoveThing
    {
        public Bullet(int x, int y, int speed,Direction p_dir,Tags p_tag)
        {
            this.IsMoving = true;
            this.X = x;
            this.Y = y;
            this.Speed = speed;

            BitmapUp = Properties.Resources.BulletUp;
            BitmapDown = Properties.Resources.BulletDown;
            BitmapLeft = Properties.Resources.BulletLeft;
            BitmapRight = Properties.Resources.BulletRight;

            this.Dir = p_dir;
            this.Tag = p_tag;


            this.X -= this.Width / 2;
            this.Y -= this.Height / 2;

        }

        protected override void MoveCheck()
        {
            Rectangle rect = GetRectangle();

            rect.X = rect.X + Width / 2 - 2;
            rect.Y = rect.Y + Height / 2 - 2;
            rect.Width = 5;
            rect.Height = 5;

            //窗体范围检测
            if (Dir == Direction.Up)
            {
                //rect.Y = rect.Y + Height / 2 + 2;
                if (rect.Y + rect.Height < 0)
                {
                    GameObjectManager.DestroyBullet(this); return;
                }
            }
            else if (Dir == Direction.Down)
            {
                //rect.Y = rect.Y + Height / 2 - 2;
                if (rect.Y > 450)
                {
                    GameObjectManager.DestroyBullet(this); return;
                }
            }
            else if (Dir == Direction.Left)
            {
                //rect.X = rect.X + Width / 2 - 2;
                if (rect.X + rect.Width < 0)
                {
                    GameObjectManager.DestroyBullet(this); return;

                }
            }
            else if (Dir == Direction.Right)
            {
                //rect.X = rect.X + Width / 2 + 2;
                if (rect.X > 450)
                {
                    GameObjectManager.DestroyBullet(this); return;

                }
            }

            //碰撞检测
            int xExplosion = this.X + this.Width / 2;
            int yExplosion = this.Y + this.Height / 2;


            if (GameObjectManager.IsCollidedWall(rect) != null)
            {
                NotMoveThing wall = GameObjectManager.IsCollidedWall(rect);
                GameObjectManager.DestroyBullet(this);
                GameObjectManager.DestroyWall(wall);
                GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                return;
            }

            if (GameObjectManager.IsCollidedSteel(rect) != null)
            {
                GameObjectManager.DestroyBullet(this);

                SoundsManager.PlayHit();

                return;
            }

            if((this.Tag == Tags.MyTank) && (GameObjectManager.IsCollidedEnemyTank(rect) != null))
            {
                EnemyTank etank = GameObjectManager.IsCollidedEnemyTank(rect);
                GameObjectManager.DestroyBullet(this);
                GameObjectManager.DestroyEnemyTank(etank);
                GameObjectManager.CreateExplosion(xExplosion, yExplosion);

                return;

            }
            if((this.Tag == Tags.EnemyTank) && (GameObjectManager.IsCollidedMyTank(rect) != null))
            {
                MyTank mytank = GameObjectManager.IsCollidedMyTank(rect);
                if (mytank.HealthValue >1)
                {
                    GameObjectManager.DestroyBullet(this);
                    GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                    GameObjectManager.RebirthMyTank();
                }
                else
                {
                    GameObjectManager.GameOver();
                }
            }



            if (GameObjectManager.IsCollidedBoss(rect) != null)
            {
                GameObjectManager.GameOver();
                return;
            }
        }

    }
}
