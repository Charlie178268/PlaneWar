using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneWar
{
    /// <summary>
    /// 游戏元素的基类，坐标x,y,高度和宽度用于碰撞检测,生命值,速度,方向
    /// </summary>
    ///
   
    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    abstract class GameObject
    {
       // const int planeWidth = 50;//加载到窗口上的飞机的宽度
        //const int planeHeight = 65;//加载到窗口上的飞机的高度
        public int x
        {
            get;
            set;
        }

        public int y
        {
            get;
            set;
        }

        public int width
        {
            get;
            set;
        }

        public int height
        {
            get;
            set;
        }

        public int speed
        {
            get;
            set;
        }

        public int life
        {
            get;
            set;
        }

        public Direction dir
        {
            get;
            set;
        }
        public GameObject(int x, int y, int width, int height, int speed, int life, Direction dir)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.speed = speed;
            this.life = life;
            this.dir = dir;
        }
        //每个游戏对象在使用GDI+对象绘制自己到窗体的时候，绘制的方式都各不一样、。
        //所以需要在父类中提供一个绘制对象的抽象函数
        public abstract void Draw(Graphics g);

        //在提供一个用于碰撞检测的函数 返回当前游戏对象的矩形

        public Rectangle GetRectangle()
        {
            return new Rectangle(this.x, this.y, this.width, this.height);
        }

        public GameObject(int x, int y)
        {
            this.x = x;
            this.y = y;
        }


        /// <summary>
        /// 移动的虚方法，每个子类如果有不一样的地方，则重写
        /// </summary>
        public virtual void Move()
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
            }else if(this.x >= 400){
                this.x = 400;
            }
            if (this.y < 0)
            {
                this.y = 0;
            }else if (this.y >= 700){
                this.y = 700;
            }
        }
    }
}
