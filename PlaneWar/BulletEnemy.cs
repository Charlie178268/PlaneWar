using PlaneWar.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaneWar
{
    class BulletEnemy:Bullet
    {
        private static Image imgEnemy = Resources.bullet11;
        public BulletEnemy(PlaneBase pf, int speed, int power)
            : base(pf, imgEnemy, speed, power)
        { }
    }
}
