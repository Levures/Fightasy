using System;

namespace Fightasy
{
    class Wizard : Character
    {
        bool countering;

        public Wizard()
        {
            name = "Wizard";
            classColor = ConsoleColor.Blue;
            health = 3;
            damage = 1;
            capacityName = "Contre-sort";
            countering = false;
        }

        public override void SpecialCapacity(){ countering = true; }
    }
}
