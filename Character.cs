﻿using System;

namespace Fightasy
{
    // Classe mère des personnages.
    class Character
    {
        protected int health;
        protected int damage;
        protected ConsoleColor classColor;
        protected string name;
        protected string capacityName;

        public int GetDamage()  { return this.damage; }
        public int GetHealth()  { return this.health; }
        public string GetName() { return this.name;   }
        public string GetCapacityName() { return this.capacityName; }
        public ConsoleColor GetClassColor() { return this.classColor; }

        public bool isDead() { return health == 0; }
        public virtual void Hit(int dmgDealt) 
        {
            health -= dmgDealt;
            if (health < 0) health = 0;
        }
        public virtual void SpecialCapacity() { }
    }
}