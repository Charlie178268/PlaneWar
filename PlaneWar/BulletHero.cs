using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlaneWar.Properties;
namespace PlaneWar
{
    class BulletHero:Bullet
    {
        public static Image imgBulletHero = Resources.bullet1;
        public BulletHero(PlaneBase pb, int speed, int power) :
            base(pb, imgBulletHero, speed, power)
        { }
    }
}
