using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlaneWar.Properties;

namespace PlaneWar
{
    /// <summary>
    /// 敌人飞机
    /// </summary>
    class PlaneEnemy:PlaneBase
    {
        /*敌人飞机有三种类型*/
        public static Image ImgPlaneEnemy1 = Resources.enemy0;//最小的飞机
        public static Image ImgPlaneEnemy2 = Resources.enemy1;//中间的飞机
        public static Image ImgPlaneEnemy3 = Resources.enemy2;//最大的飞机
        //标志生成哪一种飞机
        public int EnemyType
        {
            get;
            set;
        }
        //根据飞机类型返回对应图片
        public static Image GetImage(int type)
        {
            switch (type)
            {
                case 0:return ImgPlaneEnemy1; break;
                case 1: return ImgPlaneEnemy1; break;
                case 2: return ImgPlaneEnemy1; break;
            }
            return null;
        }
        //根据飞机类型返回对应速度
        public static int GetSpeed(int type)
        {
            switch (type)
            {
                case 0: return 3; break;
                case 1: return 4; break;
                case 2: return 5; break;
            }
            return 0;
        }
      
        //根据飞机类型返回对应生命值
        public static int GetLife(int type)
        {
            switch (type)
            {
                case 0: return 1; break;
                case 1: return 2; break;
                case 2: return 1; break;
            }
            return 0;
        }

        public PlaneEnemy(int x, int y, int type) : base(x, y, GetImage(type), GetSpeed(type), GetLife(type), Direction.Down)
        {
            this.EnemyType = type;
        }
        //重写Draw()
        public override void Draw(Graphics g)
        {
            switch (this.EnemyType)
            {
                case 0: g.DrawImage(ImgPlaneEnemy1, this.x, this.y); break;
                case 1: g.DrawImage(ImgPlaneEnemy2, this.x, this.y); break;
                case 2: g.DrawImage(ImgPlaneEnemy3, this.x, this.y); break;
            }
            Move();
        }
        //重写Move()
        public override void Move()
        {
            //根据游戏对象的方向进行移动
            switch (this.dir)
            {
                case Direction.Up:
                    this.y -= this.speed;
                    break;

                case Direction.Down:
                    this.y += this.speed;
                    break;
                case Direction.Left:
                    this.x -= this.speed;
                    break;
                case Direction.Right:
                    this.x += this.speed;
                    break;
            }

            //移动完成后 判断游戏对象是否超出了窗体
            if (this.x <= 0)
            {
                this.x = 0;
            }
            else if (this.x >= 400)
            {
                this.x = 400;
            }
            if (this.y < 0)
            {
                this.y = 0;
            }else if (this.y >= 700)
            {
                this.y = 1400;//到达窗体底端的时候 让敌人飞机离开窗体
                //同时 当敌人飞机离开窗体的时候 我们应该销毁当前敌人飞机
                SingleObject.GetInstance().RemoveGameObject(this);
            }

            //让飞机超过一定距离左右或者加速飞行
            if (this.y > 200)
            {
                //如果小飞机在窗体左半边,就往右飞
                if (EnemyType == 0)
                {
                    if (this.x > 0 && this.x < 0.5 * Form1.ActiveForm.Width)
                    {
                        this.x += r.Next(0, 100);
                    }
                    else//反之向左飞
                    {
                        this.x -= r.Next(0, 100);
                    }
                }
                else//大飞机加速飞行
                {
                    this.speed += r.Next(0, 3);
                }

            }

            //发射子弹,90%几率
            if (r.Next(0, 100)> 90)
            {
                fire();
            }
        }

        public static Random r = new Random();
        public override void IsOver()
        {
            if (this.life <= 0)
            {
                SingleObject.GetInstance().RemoveGameObject(this);
            }
            //播放敌机爆炸的动画
            SingleObject.GetInstance().AddGameObject(new EnemyBoom(this.x, this.y, this.EnemyType));
            //敌人发生了爆炸 给玩家加分
            //需要根据不同的敌人类型 添加不同的分数
            switch (this.EnemyType)
            {
                case 0:
                    //获得单例中记录玩家分数的属性
                    SingleObject.GetInstance().Score += 100;
                    break;
                case 1:
                    SingleObject.GetInstance().Score += 200;
                    break;
                case 2:
                    SingleObject.GetInstance().Score += 300;
                    break;
            }
        }
        //发射子弹
        public void fire()
        {
            SingleObject.GetInstance().AddGameObject(new BulletEnemy(this, 20, 1));
        }
    }
    
}
