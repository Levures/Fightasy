using System;

namespace Fightasy
{
    class Warlock : Character
    {
        bool alreadyGave = false;
        public Warlock()
        {
            name = "Warlock";
            classColor = ConsoleColor.Magenta;
            health = 5;
            damage = 1;
            capacityName = "Don de sang";
        }

        public override void SpecialCapacity()
        {
            if (!alreadyGave)
            {
                Random rand = new();
                int prob = rand.Next(10);
                if (prob == 1) health = 0;
                else if (prob <= 5) --health;
                else if (prob <= 9) ++damage;
                else if (prob == 10) damage += 2;
            }
            else alreadyGave = true;
        }
    }
}
