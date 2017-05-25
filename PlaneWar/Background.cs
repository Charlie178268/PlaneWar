using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlaneWar.Properties;
using System.Drawing;
namespace PlaneWar
{
    class Background:GameObject
    {
        private static Image imgBg = Resources.background;
        public Background(int x, int y, int speed):base(x, y, imgBg.Width, imgBg.Height, speed, 0, Direction.Down)
        {}
        /*实现背景图片滚动*/
        public override void Draw(Graphics g)
        {
            this.y += this.speed;
            if (this.y == 0)
            {
                this.y = -850;
            }
            //坐标改变完成后 ，将背景图像不停的绘制到我们窗体中
            g.DrawImage(imgBg, this.x, this.y);
        }
    }
}
