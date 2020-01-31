using System;

namespace WonderlandTycoon
{
    public abstract class Building
    {
        public enum BuildingType
        {
            NONE, ATTRACTION, HOUSE, SHOP
        }

        protected BuildingType type;

        public virtual bool Upgrade(ref long money)
        {
            return false;
        }
        
        public BuildingType Type
        {
            get { return type; }
        }
    }
}
