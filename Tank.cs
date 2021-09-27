using System;

namespace Fightasy
{
    class Tank : Character
    {
        public Tank()
        {
            name = "Tank";
            classColor = ConsoleColor.Cyan;
            health = 5;
            damage = 1;
            capacityName = "Attaque puissante";
        }
    
        public override void SpecialCapacity()
        {
            --health;
            ++damage;
        }
    }
}
