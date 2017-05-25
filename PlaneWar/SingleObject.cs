using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PlaneWar
{
    /// <summary>
    /// 单例类,负责创建和返回对象
    /// </summary>
    class SingleObject
    {
        private SingleObject()
        {

        }
        private static SingleObject _singleObj = null;
        public static SingleObject GetInstance()
        {
            if (_singleObj == null)
            {
                _singleObj = new SingleObject();
            }
            return _singleObj;
        }

        //存储游戏中唯一的对象背景
        public Background Bg
        {
            get;
            set;
        }
        //存储玩家飞机
        public PlaneHero Hero
        {
            get;
            set;
        }
        //存储玩家子弹
        public List<BulletHero> bulletHeroList = new List<BulletHero>();
        //存储敌人飞机
        public List<PlaneEnemy> planeEnemyList = new List<PlaneEnemy>();
        //存储爆炸对象
        public List<EnemyBoom> enemyBoomList = new List<EnemyBoom>();
        //存储敌人子弹
        public List<BulletEnemy> bulletEnemyList = new List<BulletEnemy>();
        //存储玩家飞机爆炸
        public List<HeroBoom> heroBoomList = new List<HeroBoom>();
        //记录玩家的分数
        public int Score
        {
            get;
            set;
        }
        public void AddGameObject(GameObject go)
        {
            if (go is Background)
            {
                this.Bg = go as Background;
            }
            else if (go is PlaneHero)
            {
                this.Hero = go as PlaneHero;
            }
            else if (go is BulletHero)
            {
                this.bulletHeroList.Add(go as BulletHero);
            }
            else if (go is PlaneEnemy)
            {
                this.planeEnemyList.Add(go as PlaneEnemy);
            }
            else if(go is EnemyBoom)
            {
                this.enemyBoomList.Add(go as EnemyBoom);
            }else if(go is BulletEnemy)
            {
                this.bulletEnemyList.Add(go as BulletEnemy);
            }else if (go is HeroBoom)
            {
                this.heroBoomList.Add(go as HeroBoom);
            }
        }
        public void Draw(Graphics g)
        {
            this.Bg.Draw(g);//向窗体中绘制的是背景
            this.Hero.Draw(g);//绘制的是玩家飞机
            /*绘制子弹,注意不要用foreach*/
            for (int i = 0; i < bulletHeroList.Count; i++)
            {
                bulletHeroList[i].Draw(g);
            }
            /*绘制敌机*/
            for (int i = 0; i < planeEnemyList.Count; i++)
            {
                planeEnemyList[i].Draw(g);
            }
            /*绘制爆炸图片*/
            for (int i=0; i<enemyBoomList.Count; i++)
            {
                enemyBoomList[i].Draw(g);
            }
            /*绘制敌人子弹图片*/
            for (int i = 0; i < bulletEnemyList.Count; i++)
            {
                bulletEnemyList[i].Draw(g);
            }
            /*绘制玩家飞机爆炸*/
            for (int i=0; i<heroBoomList.Count; i++)
            {
                heroBoomList[i].Draw(g);
            }
        }
        //移除对象
        public void RemoveGameObject(GameObject go)
        {
            if (go is BulletHero)
            {
                bulletHeroList.Remove(go as BulletHero);//移除子弹,但是移除会报错,暂时注释掉
            }
            else if (go is PlaneEnemy)
            {
                planeEnemyList.Remove(go as PlaneEnemy);//移除敌方飞机 
            }
            else if (go is EnemyBoom)
            {
                enemyBoomList.Remove(go as EnemyBoom);
            }
            else if (go is BulletEnemy)
            {
                this.bulletEnemyList.Remove(go as BulletEnemy);
            }
            else if (go is HeroBoom)
            {
                this.heroBoomList.Remove(go as HeroBoom);
            }
        }
        public void JudgeCrash()
        {
            //碰撞检测,检测玩家子弹是否打到敌机
            for (int i = 0; i < bulletHeroList.Count; i++)
            {
                for (int j=0; j<planeEnemyList.Count; j++)
                {
                    if (bulletHeroList[i].GetRectangle().IntersectsWith(planeEnemyList[j].GetRectangle()))
                    {
                        //玩家子弹和敌机碰撞后，敌机生命减少子弹威力大小
                        planeEnemyList[j].life -= bulletHeroList[i].Power;
                        //判断敌机是否死亡
                        planeEnemyList[j].IsOver();
                        //子弹消失
                        bulletHeroList.Remove(bulletHeroList[i]);
                        break;
                    }
                }   
            }
            //检测玩家是否被敌机子弹击落
            for (int i = 0; i < bulletEnemyList.Count; i++)
            {
                if (bulletEnemyList[i].GetRectangle().IntersectsWith(Hero.GetRectangle()))
                {
                    //让玩家爆炸
                    Hero.IsOver();
                    break;
                }
            }
            //检测玩家是否和敌人飞机发生了碰撞
            for (int i=0; i<planeEnemyList.Count; i++)
            {
                if (Hero.GetRectangle().IntersectsWith(planeEnemyList[i].GetRectangle())){
                    //Hero.IsOver();
                    planeEnemyList[i].life = 0;
                    planeEnemyList[i].IsOver();
                }
            }
        }
    }
}