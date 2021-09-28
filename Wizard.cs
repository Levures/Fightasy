using System;

namespace Fightasy
{
    class Wizard : Character
    {
        public Wizard()
        {
            name = "Wizard";
            classColor = ConsoleColor.Blue;
            health = 3;
            damage = 1;
            capacityName = "Contre-sort";
        }
    }
}
