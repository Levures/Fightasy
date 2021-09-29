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
            description = "Sacrifie un de ses poitns de vie pour infliger un dégât de plus à l'adversaire";
        }
    
        public override void SpecialCapacity()
        {            
            if (damage > 1)
                damage = 1;
            else
            {
                ++damage;
                --health;
            }
                
        }
    }
}
