using System;
using System.Xml.Schema;

namespace WonderlandTycoon
{
    public class Game
    {
        private long score;
        private long money;
        private int nbRound;
        private int round;
        private Map map;

        public long Score
        {
            get { return score; }
        }

        public long Money
        {
            get { return money; }
        }

        public int Nbround
        {
            get { return nbRound; }
        }

        public int Round
        {
            get { return round; }
        }

        public Map Map
        {
            get { return map; }
        }

        public Game(string name, int nbRound, long initialMoney)
        {
            TycoonIO.GameInit(name, nbRound, initialMoney);
            this.money = initialMoney;
            this.round = 1;
            this.nbRound = nbRound;
            for (int i = round; i <= nbRound; i++)
            {
                Console.WriteLine(i);
            }
        }

        public long Launch(Bot bot)
        {
            bot.Start( this);
            while (round < nbRound)
            {
                bot.Update(this);
                round++;
            }
            bot.End(this);
            return score;
        }
        

        public void Update()
        {
            throw new NotImplementedException("Please fix me");
        }

        public bool Build(int i, int j, Building.BuildingType type)
        {
            bool res = map.Build(i, j, ref money, type);
            if (res)
            {
                TycoonIO.GameBuild(i,j,type);
            }
            return res;
        }

        public bool Upgrade(int i, int j)
        {
            bool res = map.Upgrade(i, j, ref money);
            if (res)
            {
                TycoonIO.GameUpgrade(i,j);
            }

            return res;
        }
    }
}
