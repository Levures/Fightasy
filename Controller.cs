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

        public List<Character> playerCharacters = new List<Character> { new Damager(), new Tank(), new Healer(), new Warlock(), new Wizard() };
        public List<Character> iaCharacters = new List<Character> { new Damager(), new Tank(), new Healer(), new Warlock(), new Wizard() };

        HMICUI display;

        int gamemode;
        int difficulty;

        int[,] scores = new int[5,5]; // Pour la simulation

        public Controller()
        {
            display = new HMICUI(this);
            rand = new();

            gamemode = display.ChooseBox(0)-1;
            
            if (gamemode == 0)
            {
                difficulty = display.ChooseBox(1) - 1;

                player = playerCharacters[display.ChooseBox(2) - 1];
                computer = iaCharacters[rand.Next(iaCharacters.Count-1)];

                display.DisplayTextBox(new string[1] { "FIGHTASY : le jeu de combat" }, true);

                
                display.DisplayTextBox(new string[2] { $" Vous avez choisi la classe {player.GetName()}"
                                         , $"L'IA a choisi de prendre un {computer.GetName()} !" }, true);

                System.Threading.Thread.Sleep(2000);
                Console.Clear();
                display.DisplayTextBox(new string[1] { "FIGHTASY : le jeu de combat" }, true);
                display.DisplayTextBox(new string[1] { " Que le combat commence !" }, true);
                System.Threading.Thread.Sleep(1000);
                Console.Clear();
            }
            else // Simulation
            {
                difficulty = 3;
                

                display.DisplayTextBox(new string[1] { "La simulation commence" }, true);
            }
        }

        public void Game()
        {
            if(gamemode == 0)
            {
                while (!player.isDead() && !computer.isDead())
                {
                    Console.Clear();
                    playerAction = display.ChooseBox(3);

                    computerAction = IAChoices();

                    string resultAction = String.Concat(playerAction.ToString(), computerAction.ToString());
                    display.DisplayScreen(resultAction);

                    string[] tmpMessage1 = new string[1];
                    string[] tmpMessage2 = new string[2];

                    switch (resultAction)
                    {
                        case "11": // Joueur : Attaque |-| IA : Attaque                   
                            computer.Hit(player.GetDamage());
                            player.Hit(computer.GetDamage());

                            tmpMessage2[0] = $"Vous infligez {player.GetDamage()} points de dégâts à votre adversaire ! " +
                                    $"Il lui reste {computer.GetHealth()} points de vie";

                            tmpMessage2[1] = $"Votre adversaire vous inflige {computer.GetDamage()} points de dégâts, " +
                                        $"il vous reste {player.GetHealth()} points de vie";

                            display.ColoredMessage(tmpMessage2, resultAction);
                            break;

                        case "22": //Joueur : Défend |-| IA : Défend 
                            tmpMessage1[0] = "Les deux joueurs se défendent";
                            display.ColoredMessage(tmpMessage1, resultAction);
                            break;

                        case "12": //Joueur : Attaque |-| IA : Défend
                            tmpMessage1[0] = "Vous attaquez, mais votre adversaire se défend";
                            display.ColoredMessage(tmpMessage1, resultAction);
                            break;

                        case "21": //Joueur : Défend |-| IA : Attaque
                            tmpMessage1[0] = "Vous vous défendez face à l'attaque ennemie";
                            display.ColoredMessage(tmpMessage1, resultAction);
                            break;

                        case "33": //Joueur : Spécial |-| IA : Spécial 
                            tmpMessage1[0] = $"Vous utilisez {player.GetCapacityName()} contre {computer.GetCapacityName()}";
                            display.ColoredMessage(tmpMessage1, resultAction);

                            player.SpecialCapacity();
                            computer.SpecialCapacity();

                            if (player.GetName() == "Tank")
                                computer.Hit(player.GetDamage());

                            if (computer.GetName() == "Tank")
                                player.Hit(computer.GetDamage());


                            player.SpecialCapacity();
                            computer.SpecialCapacity();
                            break;

                        case "13": //Joueur : Attaque |-| IA : Spécial
                            tmpMessage1[0] = $"Vous attaquez contre {computer.GetCapacityName()}";
                            display.ColoredMessage(tmpMessage1, resultAction);

                            computer.SpecialCapacity();

                            computer.Hit(player.GetDamage());

                            if (computer.GetName() == "Tank")
                                player.Hit(computer.GetDamage());

                            if (computer.GetName() == "Damager")
                                player.Hit(player.GetDamage());

                            computer.SpecialCapacity();
                            break;

                        case "31": //Joueur : Spécial |-| IA : Attaque
                            tmpMessage1[0] = $"Vous utilisez {player.GetCapacityName()} contre l'attaque ennemie";
                            display.ColoredMessage(tmpMessage1, resultAction);

                            player.SpecialCapacity();

                            player.Hit(computer.GetDamage());

                            if (player.GetName() == "Tank")
                                computer.Hit(player.GetDamage());

                            if (player.GetName() == "Damager")
                                computer.Hit(computer.GetDamage());

                            player.SpecialCapacity();
                            break;

                        case "23": //Joueur : Défend |-| IA : Spécial
                            tmpMessage1[0] = $"Vous essayez de vous défendre contre {computer.GetCapacityName()}";
                            display.ColoredMessage(tmpMessage1, resultAction);

                            computer.SpecialCapacity();

                            if (computer.GetName() == "Tank")
                                player.Hit(computer.GetDamage()-1);

                            computer.SpecialCapacity();
                            break;

                        case "32": //Joueur : Spécial |-| IA : Défend
                            tmpMessage1[0] = $"Vous utilisez {player.GetCapacityName()} contre la défense ennemie";
                            display.ColoredMessage(tmpMessage1, resultAction);

                            player.SpecialCapacity();

                            if (player.GetName() == "Tank")
                                computer.Hit(player.GetDamage()-1);

                            player.SpecialCapacity();
                            break;

                        default:
                            Console.WriteLine("WIP");
                            break;


                    }
                    System.Threading.Thread.Sleep(1000);

                }
            }
            else
            {
                List<Character> playerCharacters = new List<Character> { new Damager(), new Tank(), new Healer(), new Warlock()};
                List<Character> iaCharacters = new List<Character> { new Damager(), new Tank(), new Healer(), new Warlock(), new Wizard() };

                for (int j = 0; j < playerCharacters.Count; j++) for (int k = j; k < 5; k++) scores[k, j] = 1000;
                    

                for (int a = 0; a < 100; a++)
                {
                    for (int j = 0; j < playerCharacters.Count; j++)
                    {
                        for (int k = j; k < 5; k++)
                        {
                            Console.WriteLine("j : " + j + "  k : " + k);
                            if (j != k)
                            {
                                player = playerCharacters[j];
                                computer = iaCharacters[k];
                                int nbTurn = 0;
                                while (!player.isDead() && !computer.isDead() && nbTurn < 50)
                                {

                                    computerAction = rand.Next(1, 4);
                                    playerAction = rand.Next(1, 4);

                                    string resultAction = String.Concat(playerAction.ToString(), computerAction.ToString());

                                    switch (resultAction)
                                    {
                                        case "11": // Joueur : Attaque |-| IA : Attaque                   
                                            computer.Hit(player.GetDamage());
                                            player.Hit(computer.GetDamage());
                                            break;

                                        case "33": //Joueur : Spécial |-| IA : Spécial 
                                            player.SpecialCapacity();
                                            computer.SpecialCapacity();

                                            if (player.GetName() == "Tank")
                                                computer.Hit(player.GetDamage());

                                            if (computer.GetName() == "Tank")
                                                player.Hit(computer.GetDamage());

                                            player.SpecialCapacity();
                                            computer.SpecialCapacity();
                                            break;

                                        case "13": //Joueur : Attaque |-| IA : Spécial
                                            computer.SpecialCapacity();

                                            computer.Hit(player.GetDamage());

                                            if (computer.GetName() == "Tank")
                                                player.Hit(computer.GetDamage());

                                            if (computer.GetName() == "Damager")
                                                player.Hit(player.GetDamage());

                                            computer.SpecialCapacity();
                                            break;

                                        case "31": //Joueur : Spécial |-| IA : Attaque
                                            player.SpecialCapacity();

                                            player.Hit(computer.GetDamage());

                                            if (player.GetName() == "Tank")
                                                computer.Hit(player.GetDamage());

                                            if (player.GetName() == "Damager")
                                                computer.Hit(computer.GetDamage());

                                            player.SpecialCapacity();
                                            break;

                                        case "23": //Joueur : Défend |-| IA : Spécial
                                            computer.SpecialCapacity();

                                            if (computer.GetName() == "Tank")
                                                player.Hit(computer.GetDamage() - 1);

                                            computer.SpecialCapacity();
                                            break;

                                        case "32": //Joueur : Spécial |-| IA : Défend
                                            player.SpecialCapacity();

                                            if (player.GetName() == "Tank")
                                                computer.Hit(player.GetDamage() - 1);

                                            player.SpecialCapacity();
                                            break;

                                        default:
                                            break;
                                    }
                                    display.DisplayTextBox(new string[2] { $"Joueur : {player.GetName()}, {player.GetHealth()}", $"IA : {computer.GetName()}, {computer.GetHealth()}" }, true);
                                    nbTurn += 1;
                                }

                                if (player.GetHealth() <= 0 && computer.GetHealth() <= 0)
                                {
                                    scores[j, k] += 10;
                                    scores[k, j] -= 10;

                                }
                                else if (player.GetHealth() <= 0 && computer.GetHealth() > 0)
                                {
                                    scores[j, k] += 5;
                                    scores[k, j] -= 5;
                                }

                                playerCharacters = new List<Character> { new Damager(), new Tank(), new Healer(), new Warlock(), new Wizard() };
                                iaCharacters = new List<Character> { new Damager(), new Tank(), new Healer(), new Warlock(), new Wizard() };
                            }
                            
                        }

                    }                     
                }

                display.DisplayStats(scores);
                Console.ReadLine();
            }
        }

        int IAChoices()
        {
            // Choix de l'action de l'IA.
            if (difficulty == 2) return rand.Next(1, 4);
            else
            {
                switch (playerAction)
                {
                    // Le joueur attaque.
                    case 1:
                        if (difficulty == 3)
                        {
                            if (computer.GetName() == "Tank")
                            {
                                if (computer.GetHealth() - 1 > player.GetDamage()) return rand.Next(3) <= 1 ? 3 : rand.Next(1, 4);
                                else if (computer.GetHealth() - 1 > player.GetDamage()) return rand.Next(3) <= 1 ? 1 : rand.Next(1, 4);
                                else return rand.Next(3) <= 1 ? 2 : rand.Next(1, 4);
                            }
                            else
                            {
                                if (computer.GetHealth() > player.GetDamage()) return rand.Next(3) <= 1 ? 1 : rand.Next(1, 4);
                                else return rand.Next(3) <= 1 ? 2 : rand.Next(1, 4);
                            }
                        }
                        else return rand.Next(3) <= 1 ? 1 : rand.Next(1, 4);
                        break;

                    case 2:
                        if (difficulty == 3)
                        {
                            if (computer.GetName() == "Damager" || computer.GetName() == "Wizard") return rand.Next(1, 4);
                            else return rand.Next(3) <= 1 ? 3 : rand.Next(1, 4);
                        }
                        else return rand.Next(3) <= 1 ? 2 : rand.Next(1, 4);
                        break;

                    case 3:
                        if (difficulty == 3)
                        {
                            if (player.GetName() == "Warlock" || player.GetName() == "Healer") return rand.Next(3) <= 1 ? 1 : rand.Next(1, 4);
                            else return rand.Next(3) <= 1 ? 2 : rand.Next(1, 4);
                        }
                        else return rand.Next(3) <= 1 ? 2 : rand.Next(1, 4);
                        break;
                }
                return -1;
            }
            
        }

        static void Main()
        {
            Controller ctrl = new();
            ctrl.Game();
        }
    }
}
