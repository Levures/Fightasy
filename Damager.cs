using System;

namespace Fightasy
{
    class Damager : Character
    {
        bool raging;
        
        public Damager()
        {
            name = "Damager";
            classColor = ConsoleColor.Red;
            health = 3;
            damage = 2;
            capacityName = "Rage";
            raging = false;

        }

        public override void SpecialCapacity() { raging = !raging; }
    }
}
