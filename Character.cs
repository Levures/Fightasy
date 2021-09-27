using System;

namespace Fightasy
{
    // Classe mère des personnages.
    class Character
    {
        protected int health;
        protected int damage;
        protected string name;
        protected string capacityName;

        public int GetDamage()  { return this.damage; }
        public int GetHealth()  { return this.health; }
        public string GetName() { return this.name;   }
        public string GetCapacityName() { return this.capacityName; }


        public bool isDead()
        {
            if (health <= 0) return true;
            return false;
        }

        public virtual void Hit(int dmgDealt) { health -= dmgDealt; }

        public virtual void SpecialCapacity() { }
    }
}