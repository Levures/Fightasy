using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fightasy
{
    class Controller
    {
        Character player;
        int playerAction;

        Character computer;
        int computerAction;

        Random rand;

        List<Character> characters = new List<Character> { new Damager(), new Tank(), new Healer() };

        string cursor = " <--";

        public Controller()
        {
            player = characters[ChooseBox(false)-1];

            DisplayTextBox(new string[1] { "FIGHTASY : le jeu de combat" }, true);

            rand = new();
            computer = characters[rand.Next(3)];
            DisplayTextBox(new string[2] { $" Vous avez choisi la classe {player.GetName()}"
                                         , $"L'IA a choisi de prendre un {computer.GetName()} !" }, true);

            System.Threading.Thread.Sleep(4000);
            Console.Clear();
            DisplayTextBox(new string[1] { "FIGHTASY : le jeu de combat" }, true);
            DisplayTextBox(new string[1] { " Que le combat commence !" }, true);
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
        }

        public void Game()
        {
            while (!player.isDead() && !computer.isDead())
            {
                playerAction = ChooseBox(true);
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

        public int ChooseBox(bool actions)
        {
            int selection = 1;
            bool choice = false;

            if (actions) DisplayActions(selection);
            else DisplayCharacters(selection);

            do
            {
                ConsoleKeyInfo keyPressed = Console.ReadKey();
                switch (keyPressed.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (selection == characters.Count)
                        {
                            selection = 1;
                            Console.Clear();
                            if (actions) DisplayActions(selection);
                            else DisplayCharacters(selection);
                        }
                        else
                        {
                            selection += 1;
                            Console.Clear();
                            if (actions) DisplayActions(selection);
                            else DisplayCharacters(selection);
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        if (selection == 1)
                        {
                            selection = 3;
                            Console.Clear();
                            if (actions) DisplayActions(selection);
                            else DisplayCharacters(selection); ;
                        }
                        else
                        {
                            selection -= 1;
                            Console.Clear();
                            if (actions) DisplayActions(selection);
                            else DisplayCharacters(selection);
                        }
                        break;

                    case ConsoleKey.Enter:
                        Console.Clear();
                        choice = true;
                        break;

                    default:
                        Console.Clear();
                        if (actions) DisplayActions(selection);
                        else DisplayCharacters(selection);
                        break;
                }
            }
            while (!choice);
            Console.WriteLine(selection);
            return selection;
        }

        public void DisplayCharacters(int selection)
        {
            DisplayTextBox(new string[1] { "FIGHTASY : le jeu de combat" }, true);

            
            string sep = "+---+---------+-------+----+";
            Console.WriteLine("+--------------------------+");
            Console.WriteLine("|    Choisis ta classe     |");
            Console.WriteLine("+--------------------------+");

            for (int i = 1; i < characters.Count + 1; i++)
            {
                int longestLength = 0;
                foreach (Character ch in characters) if (longestLength < ch.GetName().Length) longestLength = ch.GetName().Length;

                string name = characters[i - 1].GetName();
                int health = characters[i - 1].GetHealth();
                int damage = characters[i - 1].GetDamage();
                Console.Write($"| {i} | " + name + new string(' ', longestLength - name.Length) + " |");
                Console.Write(" " + new string('♥', health) + new string(' ', 5 - health) + " |");
                Console.Write($" " + new string('♦', damage) + new string(' ', 2 - damage) + " |");
                if (selection == i)
                    Console.Write(cursor);
                Console.WriteLine("\n" + sep);
            }
        }

        public void DisplayTextBox(string[] messages, bool multipleSep)
        {
            int longestLength = 0;
            foreach (string msg in messages) if (longestLength < msg.Length) longestLength = msg.Length;
            string sep = "+" + new string('-', longestLength + 2) + "+";

            Console.WriteLine(sep);
            foreach (string msg in messages)
            {
                Console.WriteLine("| " + msg + new string(' ',longestLength - msg.Length)+" |");
                if(multipleSep) Console.WriteLine(sep);
            }

            if (!multipleSep) Console.WriteLine(sep);
            Console.WriteLine();
        }

        public void DisplayActions(int selection)
        {
            DisplayTextBox(new string[1] { "FIGHTASY : le jeu de combat" }, true);

            string title = " Choisis une action ";
            int longestLength = 22;
            if (longestLength < player.GetCapacityName().Length+4) longestLength = player.GetCapacityName().Length+4;
            int titleLength = longestLength + 4 - title.Length;
            int defLength = 9;
            int atkLength = 12;

            string sep = "+---+" + new string('-', longestLength) + "+";

            string capacityName = player.GetCapacityName();
            int damage = player.GetDamage();

            // Affichage du titre de la boîte de texte.
            Console.WriteLine("+" + new string('-', longestLength+4) + "+");
            Console.WriteLine("|" + new string(' ', titleLength / 2) + title + new string(' ', titleLength / 2) + "|");

            // Affichage du tableau des actions.
            Console.WriteLine(sep);
            Console.Write("| 1 | Attaquer (" + new string('♦', damage) + ")" + new string(' ', longestLength - damage - atkLength) + "|");
            if (selection == 1) Console.Write(cursor);
            Console.WriteLine();
            Console.WriteLine(sep);
            Console.Write("| 2 | Défendre" + new string(' ', longestLength - defLength) + "|");
            if (selection == 2) Console.Write(cursor);
            Console.WriteLine();
            Console.WriteLine(sep);
            Console.Write($"| 3 | {capacityName}" + new string(' ', longestLength - capacityName.Length-1) + "|");
            if (selection == 3) Console.Write(cursor);
            Console.WriteLine();
            Console.WriteLine(sep);
        }

        static void Main()
        {
            Controller ctrl = new();
            ctrl.Game();
        }
    }
}
