using System;

namespace WonderlandTycoon
{
    public class House : Building
    {
        public const long BUILD_COST = 250;
        public static readonly long[] UPGRADE_COST = {750, 3000, 10000};
        public static readonly long[] HOUSING = {300, 500, 650, 750};
        private int lvl;

        public int Lvl
        {
            get { return lvl; }
        }

        public House()
        {
            lvl = 0;
            type = BuildingType.HOUSE;
        }

        public long Housing()
        {
            long res = HOUSING[lvl];
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
