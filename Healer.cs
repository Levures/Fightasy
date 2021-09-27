using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fightasy
{
    class Healer : Character
    {
        public Healer()
        {
            name = "Healer";
            classColor = ConsoleColor.White;
            health = 4;
            damage = 1;
            capacityName = "Soins";
        }

        public override void SpecialCapacity() { ++health; }
    }
}
