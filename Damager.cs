using System;

namespace Fightasy
{
    class Damager : Character
    {
        public Damager()
        {
            name = "Damager";
            classColor = ConsoleColor.Red;
            health = 3;
            damage = 2;
            capacityName = "Rage";
        }
    }
}
