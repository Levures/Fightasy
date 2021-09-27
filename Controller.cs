using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fightasy
{
    class Controller
    {
        public Character player;
        public int playerAction;

        public Character computer;
        public int computerAction;

        Random rand;

        public List<Character> characters = new List<Character> { new Damager(), new Tank(), new Healer() };

        HMICUI display;
        public Controller()
        {
            display = new HMICUI(this);
            player = characters[display.ChooseBox(false)-1];

            display.DisplayTextBox(new string[1] { "FIGHTASY : le jeu de combat" }, true);

            rand = new();
            computer = characters[rand.Next(3)];
            display.DisplayTextBox(new string[2] { $" Vous avez choisi la classe {player.GetName()}"
                                         , $"L'IA a choisi de prendre un {computer.GetName()} !" }, true);

            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            display.DisplayTextBox(new string[1] { "FIGHTASY : le jeu de combat" }, true);
            display.DisplayTextBox(new string[1] { " Que le combat commence !" }, true);
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
        }

        public void Game()
        {
            while (!player.isDead() && !computer.isDead())
            {
                display.DisplayScreen();
                playerAction = display.ChooseBox(true);
                computerAction = rand.Next(3);

                string[] tmpMessage = new string[2];

               /* switch()

                switch(playerAction)
                {
                    case 1:
                        
                        tmpMessage[0] = " Vous avez choisi d'Attaquer !";
                        switch()
                        DisplayTextBox()
                }*/
            }
        }

        static void Main()
        {
            Controller ctrl = new();
            ctrl.Game();
        }
    }
}
