using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForm
{
    internal class MapDesign
    {
        private static int[,] l1_layoutWall = new int[,] {
            //x,y,count
            {6,13,2},
            {7,13,1},
            {8,13,2},

            {1,1,5},
            {3,1,5},
            {5,1,4},
            {7,1,3},
            {9,1,4},
            {11,1,5},
            {13,1,5},

            {2,7,1},
            {3,7,1},
            {4,7,1},
            {6,7,1},
            {7,6,2},
            {8,7,1},
            {10,7,1},
            {11,7,1},
            {12,7,1},

            {1,9,5},
            {3,9,5},
            {3,9,5},
            {5,9,3},

            {6,10,1},
            {7,10,2},
            {8,10,1},

            {9,9,3},
            {11,9,5},
            {13,9,5}
            };
        private static int[,] l1_layoutSteel = new int[,]
            {
            //x,y,count
            {7,5,1},
            {0,7,1},
            {14,7,1}

            };

        public static LevelMap map_level1 = new LevelMap(l1_layoutWall,l1_layoutSteel);

    }
}
