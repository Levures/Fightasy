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

        public List<Character> playerCharacters = new List<Character> { new Damager(), new Tank(), new Healer() };
        public List<Character> iaCharacters = new List<Character> { new Damager(), new Tank(), new Healer() };

        HMICUI display;
        public Controller()
        {
            display = new HMICUI(this);
            player = playerCharacters[display.ChooseBox(false)-1];

            display.DisplayTextBox(new string[1] { "FIGHTASY : le jeu de combat" }, true);

            rand = new();
            computer = iaCharacters[rand.Next(3)];
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
                Console.Clear();
                display.DisplayScreen();
                playerAction = display.ChooseBox(true);
                computerAction = rand.Next(1, 4);
                

                string[] tmpMessage1 = new string[1];
                string[] tmpMessage2 = new string[2];
                string resultAction = String.Concat(playerAction.ToString(), computerAction.ToString());

                Console.WriteLine(resultAction + "   resultaction");
                System.Threading.Thread.Sleep(2000);

                switch (resultAction)
                {
                    case "11": // Joueur : Attaque |-| IA : Attaque                   
                        computer.Hit(player.GetDamage());
                        player.Hit(computer.GetDamage());

                        tmpMessage2[0] = $"Vous infligez {player.GetDamage()} points de dégâts à votre adversaire ! " +
                                $"Il lui reste {computer.GetHealth()} points de vie";

                        tmpMessage2[1] = $"Votre adversaire vous inflige {computer.GetDamage()} points de dégâts, " +
                                    $"il vous reste {player.GetHealth()} points de vie";

                        display.DisplayTextBox(tmpMessage2, true);

                        System.Threading.Thread.Sleep(5000);
                        break;

                    case "22": //Joueur : Défend |-| IA : Defend 

                        break;

                    case "12": //Joueur : Attaque |-| IA : Défend

                        break;

                    case "21": //Joueur : Défend |-| IA : Attaque

                        break;

                    case "33": //Joueur : Spécial |-| IA : Spécial 

                        break;

                    case "13": //Joueur : Attaque |-| IA : Spécial

                        break; 

                    case "31": //Joueur : Spécial |-| IA : Attaque

                        break;

                    case "23": //Joueur : Défend |-| IA : Spécial

                        break;

                    case "32": //Joueur : Spécial |-| IA : Défend

                        break;

                    default:
                        Console.WriteLine("WIP");
                        break;

                }

            }
        }

        static void Main()
        {
            Controller ctrl = new();
            ctrl.Game();
        }
    }
}
