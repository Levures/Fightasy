using System;

namespace Fightasy
{
    class Damager : Character
    {
        bool raging;
        
        public Damager()
        {
            name = "Damager";
            health = 3;
            damage = 2;
            capacityName = "Rage";
            raging = false;
        }

        public override void SpecialCapacity() { raging = true; }
    }
}
