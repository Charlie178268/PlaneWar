using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneWar
{
    class Bullet:GameObject
    {
        private Image imgBullet;//存储子弹图片
        //记录一下子弹的威力
        public int Power
        {
            get;
            set;
        }
        public Bullet(PlaneBase pb, Image img, int speed, int power)
            : base(pb.x + pb.width / 2 - 30, pb.y + pb.height / 2 - 50, img.Width, img.Height, speed, 0, pb.dir)
        {
            this.imgBullet = img;
            this.Power = power;
        }


        //重写GameObject的抽象成员
        public override void Draw(Graphics g)
        {
            this.Move();
            Console.WriteLine("这是Draw方法");
            g.DrawImage(imgBullet, this.x, this.y, this.width / 2, this.height / 2);
        }

        public override void Move()
        {
            switch (this.dir)
            {
                case Direction.Up:
                    this.y -= this.speed;
                    break;
                case Direction.Down:
                    this.y += this.speed;
                    break;
            }
            //子弹发出后 控制一下子弹的坐标
            if (this.y <= 0)
            {
                this.y = -100;
                //在游戏中移除子弹对象
                SingleObject.GetInstance().RemoveGameObject(this);
            }
            if (this.y >= 780)
            {
                this.y = 1000;
                //在游戏中移除子弹对象
                SingleObject.GetInstance().RemoveGameObject(this);
            }
        }
    }
}
