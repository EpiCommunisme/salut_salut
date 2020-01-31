using System;

namespace WonderlandTycoon
{
    public class Tile
    {
        public enum Biome
        {
            SEA, MOUNTAIN, PLAIN
        }
        private Biome biome;
        private Building building;

        public Biome GetBiome
        {
            get { return biome; } 
        }

        public Tile(Biome b)
        {
            biome = b;
            building = null;
        }

        public bool Build(ref long money, Building.BuildingType type)
        {
            bool res = false;
            if ((biome == Biome.PLAIN) && (building == null))
            {
                if (type == Building.BuildingType.ATTRACTION)
                {
                    if (money >= Attraction.BUILD_COST)
                    {
                        res = true;
                        money = money - Attraction.BUILD_COST;
                        building = new Attraction();
                    }

                    if (money >= Shop.BUILD_COST)
                    {
                        res = true;
                        money = money - Shop.BUILD_COST;
                        building = new Shop();
                    }

                    if (money >= House.BUILD_COST)
                    {
                        res = true;
                        money = money - House.BUILD_COST;
                        building = new House();
                    }
                }
            }
            return res;
        }

        public bool Upgrade(ref long money)
        {
            return building.Upgrade(ref money);
        }

        public long GetHousing()
        {
            long res = 0;
            if ((building != null) && (building.Type ==Building.BuildingType.HOUSE))
            {
                res = res + (building as House).Housing();
            }
            return res;
        }

        public long GetAttractiveness()
        {
            long res = 0;
            if ((building != null) && (building.Type == Building.BuildingType.ATTRACTION))
            {
                res = res + (building as Attraction).Attractiveness();
            }
            return res;
        }

        public long GetIncome(long population)
        {
            long res = 0;
            if ((building != null) && (building.Type == Building.BuildingType.SHOP))
                res = res + (building as Shop).Income(population);
            return res;
        }
    }
}
