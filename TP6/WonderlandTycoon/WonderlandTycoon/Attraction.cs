using System;

namespace WonderlandTycoon
{
    public class Attraction : Building
    {
        public const long BUILD_COST = 10000;
        public static readonly long[] UPGRADE_COST = {5000, 10000, 45000};
        public static readonly long[] ATTRACTIVENESS = {500, 1000, 1300, 1500};
        private int lvl;

        public int Lvl
        {
            get { return lvl;}
        }
        
        public Attraction()
        {
            lvl = 0;
            type = BuildingType.ATTRACTION;
        }

        public long Attractiveness()
        {
            long res = ATTRACTIVENESS[lvl];
            return res;
        }

        public override bool Upgrade(ref long money)
        {
            bool res = false;
            if ((lvl < 3) && (money >= UPGRADE_COST[lvl - 1]));
            {
                res = true;
                lvl = lvl + 1;
                money = money - UPGRADE_COST[lvl];
            }
            return res;
        }
    }
}
