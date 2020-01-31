using System;

namespace WonderlandTycoon
{
    public class Shop : Building
    {
        public const long BUILD_COST = 300;
        public static readonly long[] UPGRADE_COST = {2500, 10000, 50000};
        public static readonly long[] INCOME = {7, 8, 9, 10};
        private int lvl;

        public int Lvl
        {
            get { return lvl; }
        }

        public Shop()
        {
            lvl = 0;
            type = BuildingType.SHOP;
        }

        public long Income(long population)
        {
            long res = UPGRADE_COST[lvl] * population / 100;
            return res;
        }

        public override bool Upgrade(ref long money)
        {
            bool res = false;
            if ((lvl < 3) & (money >= UPGRADE_COST[lvl - 1]));
            {
                res = true;
                lvl = lvl + 1;
                money = money - UPGRADE_COST[lvl];
            }
            return res;
        }
    }
}
