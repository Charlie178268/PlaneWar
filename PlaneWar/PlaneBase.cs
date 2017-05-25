using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PlaneWar
{
    /// <summary>
    /// 飞机基类，因为不需要对draw进行重写，所以也是抽象类
    /// </summary>
    abstract class PlaneBase:GameObject
    {
        public Image imgPlane;
        public PlaneBase(int x, int y, Image img,int speed, int life, Direction dir) : base(x, y, img.Width, img.Height, speed, life, dir)
        {
            this.imgPlane = img;
        }
        //飞机的父类不需要 重写父类的Draw函数，因为玩家飞机跟敌人飞机在绘制自己到窗体的时候 方式各不一样

        //提供一个判断是否死亡的抽象函数 具体怎么死亡 又子类自己去决定
        public abstract void IsOver();
    }
}
