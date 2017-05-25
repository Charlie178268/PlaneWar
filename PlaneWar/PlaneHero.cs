using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using PlaneWar.Properties;
using System.Windows.Forms;
namespace PlaneWar
{
    class PlaneHero:PlaneBase
    {
        //导入玩家飞机的图片 存储到字段中
        private static Image imgPlane = Resources.hero1;
        //再去调用父类的构造函数
        public PlaneHero(int x, int y, int speed, int life, Direction dir)
            :base(x, y, imgPlane, speed, life, dir)
        { }
        //重写GameObject中抽象函数Draw
        public override void Draw(Graphics g)
        {
            g.DrawImage(imgPlane, this.x, this.y, this.width / 2, this.height / 2);
        }

        //开炮
        public void Fire()
        {
            //初始化我们的玩家子弹到游戏中
            SingleObject.GetInstance().AddGameObject(new BulletHero(this, 15, 1));
        }

        public override void IsOver()
        {
            SingleObject.GetInstance().AddGameObject(new HeroBoom(this.x, this.y));
        }
        //飞机跟随鼠标移动
        public void MouseMove(MouseEventArgs e)
        {
            this.x = (int)(e.X - 0.25*imgPlane.Width);
            this.y = (int)(e.Y - 0.25*imgPlane.Height);
        }
    }
}

