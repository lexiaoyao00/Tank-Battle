using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForm
{
    internal abstract class GameObject
    {
        public int X {  get; set; }
        public int Y { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }


        protected abstract Image GetImage();
        public virtual void DrwaSelf()
        {
            Graphics g = GameFramework.g_canvas;

            g.DrawImage(GetImage(),X,Y);
        }

        public virtual void Update()
        {
            DrwaSelf();
        }


        public Rectangle GetRectangle()
        {
            Rectangle rectangle = new Rectangle(X,Y,Width,Height);
            return rectangle;
        }
    }
}
