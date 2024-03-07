using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForm
{
    internal class LevelMap
    {
        private static int[,] layoutWall = new int[,]
            {
                {6,13,2},
                {7,13,1},
                {8,13,2}
            };

        private static int[,] layoutSteel = null;
        private static int[] bossPos = { 7, 14 };

        public int[,] get_layoutWall()
        {
            int[,]  t_layoutWall = layoutWall;
            return t_layoutWall;
        }

        public int[,] get_layoutSteel()
        {
            return layoutSteel;
        }

        public int[] get_bossPos()
        {
            return bossPos;
        }


        public LevelMap(int[,] p_layoutWall, int[,] p_layoutSteel) 
        {
            layoutWall = p_layoutWall;
            layoutSteel = p_layoutSteel;
        }

        public LevelMap(int[,] p_layoutWall, int[,] p_layoutSteel, int[] p_bossPos)
        {
            layoutWall = p_layoutWall;
            layoutSteel = p_layoutSteel;
            bossPos = p_bossPos;
        }
    }
    
}
