using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlaneWar
{
    public partial class Form1 : Form
    {
        static Random r = new Random();
        public Form1()
        {
            InitializeComponent();
            InitialGame();
        }
         void InitialGame()
        {
            SingleObject.GetInstance().AddGameObject(new Background(0, -850, 5));
            SingleObject.GetInstance().AddGameObject(new PlaneHero(100, 100, 5, 3, Direction.Up));
            InitialPlaneEnemy();
            
        }
        /*初始化敌人飞机*/
        private void InitialPlaneEnemy()
        {
            for (int i = 0; i < 3; i++)
            {
                SingleObject.GetInstance().AddGameObject(new PlaneEnemy(r.Next(0, this.Width), -400, r.Next(0, 2)));
                /*%80几率出现大飞机*/
                if (r.Next(0, 100) > 80)
                {
                    SingleObject.GetInstance().AddGameObject(new PlaneEnemy(r.Next(0, this.Width), -400, 2));
                }
            }
            
        } 
        /*窗口重绘时事件*/
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            SingleObject.GetInstance().Draw(e.Graphics);
            string score = SingleObject.GetInstance().Score.ToString();
            //绘制玩家的分数
            e.Graphics.DrawString(score, new Font("宋体", 20, FontStyle.Bold), Brushes.Red, new Point(0, 0));
        }

        private void PaintWindowTimer(object sender, EventArgs e)
        {
            this.Invalidate();
            /*当窗口飞机数量小于2架时,重新出现飞机*/
            if (SingleObject.GetInstance().planeEnemyList.Count < 2)
            {
                InitialPlaneEnemy();
            }
            //碰撞检测
            SingleObject.GetInstance().JudgeCrash();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //在窗体加载的时候 解决窗体闪烁问题
            //将图像绘制到缓冲区减少闪烁
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //当鼠标在窗体进行移动的时候 ，让飞机跟着鼠标的移动而移动
            SingleObject.GetInstance().Hero.MouseMove(e);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //玩家按下鼠标左键,即开炮
            SingleObject.GetInstance().Hero.Fire();
        }
    }
}
