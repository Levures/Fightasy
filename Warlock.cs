using System;

namespace Fightasy
{
    class Warlock : Character
    {
        public Warlock()
        {
            name = "Warlock";
            health = 3;
            damage = 1;
            capacityName = "Don de sang";
        }

        public override void SpecialCapacity()
        {
            Random rand = new();
            int prob = rand.Next(10);
            if (prob == 1) health = 0;
            else if (prob <= 5) --health;
            else if (prob <= 9) ++damage;
            else if (prob == 10) damage += 2;

        }
    }
}
