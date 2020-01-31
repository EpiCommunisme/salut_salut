using System;
using System.CodeDom.Compiler;

namespace WonderlandTycoon
{
    public class Map
    {
        public Tile[,] matrix;

        public Map(string name)
        {
            TycoonIO.ParseMap(name);
        }

        public bool Build(int i, int j, ref long money,
                Building.BuildingType type)
        {
            bool res = false;

            if (matrix[i, j].GetBiome == Tile.Biome.PLAIN)
            {
                if ((type == Building.BuildingType.SHOP) && (money >= Shop.BUILD_COST))
                {
                    money = money - Shop.BUILD_COST;
                    res = true;
                }

                if ((type == Building.BuildingType.HOUSE) && (money >= House.BUILD_COST))
                {
                    money = money - House.BUILD_COST;
                    res = true;
                }

                if ((type == Building.BuildingType.ATTRACTION) && (money >= Attraction.BUILD_COST))
                {
                    money = money - Attraction.BUILD_COST;
                    res = true;
                }
            }
            return res;
        }

        public bool Upgrade(int i, int j, ref long money)
        {
            return matrix[i, j].Upgrade(ref money);
        }

        public long GetAttractiveness()
        {
            long res = 0;
            foreach (Tile t in matrix)
            {
                res = res + t.GetAttractiveness();
            }
            return res;
        }

        public long GetHousing()
        {
            long res = 0;
            foreach (Tile t in matrix)
            {
                res = res + t.GetHousing();
            }
            return res;
        }

        public long GetPopulation()
        {
            long res = GetHousing();
            if (GetHousing() < GetAttractiveness())
                res = GetAttractiveness();
            return res;
        }

        public long GetIncome(long population)
        {
            long res = 0;
            foreach (Tile t in matrix)
            {
                res = res + t.GetIncome(population);
            }
            return res;
        }
    }
}
