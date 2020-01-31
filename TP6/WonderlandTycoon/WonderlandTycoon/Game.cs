using System;

namespace WonderlandTycoon
{
    public class Game
    {
        public Game(string name, int nbRound, long initialMoney)
        {
            TycoonIO.GameInit(name, nbRound, initialMoney);
            throw new NotImplementedException("Please fix me");
        }

        public long Launch(Bot bot)
        {
            throw new NotImplementedException("Please fix me");
        }

        public void Update()
        {
            throw new NotImplementedException("Please fix me");
        }

        public bool Build(int i, int j, Building.BuildingType type)
        {
            throw new NotImplementedException("Please fix me");
        }

        public bool Upgrade(int i, int j)
        {
            throw new NotImplementedException("Please fix me");
        }
    }
}
