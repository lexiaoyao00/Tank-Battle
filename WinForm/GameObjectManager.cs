using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm
{
    internal class GameObjectManager
    {
        private static List<NotMoveThing> wallList = new List<NotMoveThing>();
        private static List<NotMoveThing> steelList = new List<NotMoveThing>();
        private static NotMoveThing boss;

        private static MyTank myTank;
        private static Point myTankOriginal=new Point(5,14);
        private static int myTankSpeed = 2;

        private static List<EnemyTank> enemyTankList = new List<EnemyTank>();
        private static List<Bullet> bulletList = new List<Bullet>();
        private static List<Explosion> explosionList = new List<Explosion>();

        private static int[,] layoutWall = MapDesign.map_level1.get_layoutWall();

        private static int[,] layoutSteel = MapDesign.map_level1.get_layoutSteel();
        private static int[] bossPos = MapDesign.map_level1.get_bossPos();

        private static int enemyGenerationSpeed = 45;
        private static int enemyGenerationTimer = 60;
        private static Point[] enemyGenerationPoints = new Point[3];

        public static void Start()
        {
            enemyGenerationPoints[0].X = 0; enemyGenerationPoints[0].Y = 0;
            enemyGenerationPoints[1].X = 7*30; enemyGenerationPoints[1].Y = 0;
            enemyGenerationPoints[2].X = 14*30; enemyGenerationPoints[1].Y = 0;

            createMyTank();
            CreateMap();

        }

        public static void Update()
        {
            DrawMap();
            drawMyTank();
            enemyGeneration();
            drawEnemyTanks();
            drawEnemyBullets();
            drawExplosions();
        }


        public static void GameOver()
        {
            GameFramework.gameState = GameState.GameOver;
        }

        #region 爆炸效果

        public static void drawExplosions()
        {

            for (int i = 0; i < explosionList.Count; i++)
            {
                explosionList[i].Update();
            }

        }
        public static void CreateExplosion(int x, int y)
        {
            SoundsManager.PlayBlast();
            Explosion exp = new Explosion(x, y);
            explosionList.Add(exp);
        }

        public static void DestroyExplosion(Explosion p_explosion)
        {
            explosionList.Remove(p_explosion);
        }


        #endregion

        #region 子弹生成
        public static void drawEnemyBullets()
        {

            for (int i = 0; i < bulletList.Count; i++)
            {
                bulletList[i].Update();
            }
            
        }


        public static void CreateBullet(int x,int y,Tags p_tag,Direction p_dir)
        {
            Bullet bullet = new Bullet(x, y, 5, p_dir, p_tag);
            bulletList.Add(bullet);
        }

        public static void DestroyBullet(Bullet p_bullet)
        {
            bulletList.Remove(p_bullet);
        }

        #endregion


        #region 敌人生成
        public static void drawEnemyTanks()
        {
            for (int i = 0; i < enemyTankList.Count; i++)
            {
                enemyTankList[i].Update();
            }

        }

        public static void enemyGeneration()
        {
            if (enemyTankList.Count > 10) return;// 限定敌人数量最大值为10
            enemyGenerationTimer++;
            if (enemyGenerationTimer < enemyGenerationSpeed) return;
            Random rd = new Random();
            int index = rd.Next(0, 3);
            Point position = enemyGenerationPoints[index];

            int enemyType = rd.Next(1, 5);
            switch (enemyType)
            {
                case 1:
                    CreateEnemyTank1(position.X, position.Y); break;
                case 2:
                    CreateEnemyTank2(position.X, position.Y); break;
                case 3:
                    CreateEnemyTank3(position.X, position.Y); break;
                case 4:
                    CreateEnemyTank4(position.X, position.Y); break;
                default:
                    CreateEnemyTank1(position.X, position.Y); break;
            }

            SoundsManager.PlayAdd();

            enemyGenerationTimer = 0;
        }

        private static void CreateEnemyTank1(int x,int y)
        {
            EnemyTank tank = new EnemyTank(x,y,2,
                Properties.Resources.GrayUp,
                Properties.Resources.GrayDown,
                Properties.Resources.GrayLeft,
                Properties.Resources.GrayRight
                );
            enemyTankList.Add( tank );
        }
        private static void CreateEnemyTank2(int x,int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 2,
                Properties.Resources.GreenUp,
                Properties.Resources.GreenDown,
                Properties.Resources.GreenLeft,
                Properties.Resources.GreenRight
                );
            enemyTankList.Add(tank);
        }
        private static void CreateEnemyTank3(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 4,
                Properties.Resources.QuickUp,
                Properties.Resources.QuickDown,
                Properties.Resources.QuickLeft,
                Properties.Resources.QuickRight
                );
            enemyTankList.Add(tank);
        }
        private static void CreateEnemyTank4(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 1,
                Properties.Resources.SlowUp,
                Properties.Resources.SlowDown,
                Properties.Resources.SlowLeft,
                Properties.Resources.SlowRight
                );
            enemyTankList.Add(tank);
        }

        public static void DestroyEnemyTank(EnemyTank p_tank)
        {
            enemyTankList.Remove(p_tank);
        }


        #endregion


        #region 碰撞检测

        public static NotMoveThing IsCollidedWall(Rectangle p_rt)
        {

            for (int i = 0; i < wallList.Count; i++)
            {
                if (wallList[i].GetRectangle().IntersectsWith(p_rt))
                {
                    return wallList[i];
                }
            }

            return null;
        }

        public static NotMoveThing IsCollidedBoss(Rectangle p_rt)
        { 
            if (boss.GetRectangle().IntersectsWith(p_rt))
            {
                return boss;
            }

            return null;
        }

        public static NotMoveThing IsCollidedSteel(Rectangle p_rt)
        {
            for (int i = 0; i < steelList.Count; i++)
            {
                if (steelList[i].GetRectangle().IntersectsWith(p_rt))
                {
                    return steelList[i];
                }
            }

            return null;
        }

        public static EnemyTank IsCollidedEnemyTank(Rectangle p_rt)
        {
            for (int i = 0; i < enemyTankList.Count; i++)
            {
                if (enemyTankList[i].GetRectangle().IntersectsWith(p_rt))
                {
                    return enemyTankList[i];
                }
            }

            return null;
        }

        public static MyTank IsCollidedMyTank(Rectangle p_rt)
        {

            if (myTank.GetRectangle().IntersectsWith(p_rt))
            { 
                return myTank;
            }

            return null;
        }

        #endregion


        #region 按键检测
        public static void KeyDown(KeyEventArgs p_key)
        {
            myTank.Move_KeyDown(p_key);
        }

        public static void KeyUp(KeyEventArgs p_key)
        {
            myTank.Move_KeyUp(p_key);
        }
        #endregion


        #region 创建mytank位置和画在画布上
        public static void drawMyTank()
        {
            myTank.Update();
        }
        public static void createMyTank()
        {
            int xPosition = myTankOriginal.X * 30;
            int yPosition = myTankOriginal.Y * 30;

            myTank = new MyTank(xPosition, yPosition, myTankSpeed);
        }

        public static void RebirthMyTank()
        {
            myTank.HealthValue -= 1;
            myTank.X = 5 * 30;
            myTank.Y = 14 * 30;
        }

        #endregion


        #region 创建地图不动单位体，包括墙体和boss
        public static void DrawMap()
        {
            for (int i = 0;i < wallList.Count; i++)
            {
                wallList[i].Update();
            }
            for (int i = 0; i < steelList.Count; i++)
            {
                steelList[i].Update();
            }
            boss.Update();
            
        }
        public static void CreateMap()
        {
            createWallMap(layoutWall,Properties.Resources.wall, wallList);
            createWallMap(layoutSteel,Properties.Resources.steel,steelList);
            createBoss(bossPos[0], bossPos[1], Properties.Resources.Boss);
        }


        public static void createBoss(int x, int y,Image p_img)
        {
            int xPosition = x * 30;
            int yPosition = y * 30;
            boss = new NotMoveThing(xPosition,yPosition,p_img);
        }

        public static void createWallMap(int[,] layout,Image p_img,List<NotMoveThing> p_wallList)
        {
            int row;
            int rowMax = layout.GetLength(0);
            //int colMax = layoutWall.GetLength(1);
            for (row = 0; row < rowMax; row++)
            {
                int x = layout[row, 0];
                int y = layout[row, 1];
                int count = layout[row, 2];
                CreateWall(x, y, count, p_img, p_wallList);
            }
        }

        public static void CreateWall(int x,int y,int count,Image img, List<NotMoveThing> p_wallList)
        {
            int xPosision = x * 30;
            int yPosision = y * 30;

            for (int i = yPosision; i < yPosision + count*30; i+=15)
            {
                NotMoveThing wallLeft = new NotMoveThing(xPosision, i, img);
                NotMoveThing wallRight = new NotMoveThing(xPosision + 15, i, img);

                p_wallList.Add(wallLeft);
                p_wallList.Add(wallRight);
            }

        }

        public static void DestroyWall(NotMoveThing wall)
        {
            wallList.Remove(wall);
        }

        #endregion
    }
}
