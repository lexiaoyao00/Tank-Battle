using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace WinForm
{
    internal class NotMoveThing:GameObject
    {
        private Image img;
        public Image Img { get { return img; }
            set { 
                img = value;
                this.Width = Img.Width;
                this.Height = Img.Height;
            }
        }

        protected override Image GetImage()
        {
            
            return Img;
        }


        public NotMoveThing(int x,int y,Image img)
        {
            this.X = x;
            this.Y = y;
            this.Img = img;
        }
    }
}
