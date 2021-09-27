using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fightasy
{
    class HMICUI
    {
        Controller ctrl;

        string cursor = " <--";

        List<ConsoleColor> classColors = new List<ConsoleColor> {ConsoleColor.Red, ConsoleColor.Blue, ConsoleColor.White };

        public HMICUI(Controller ctrl)
        {
            this.ctrl = ctrl;
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
                        if (selection == ctrl.characters.Count)
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

        public void DisplayScreen()
        {
            string sep   = "+----------------------------------------+";
            string title = "|      FIGHTASY : le jeu de combat       |";

            Console.WriteLine(sep + "\n" + title + "\n" + sep);

            Console.Write("| ");
            WriteInColor(ConsoleColor.Blue,"Joueur");
            Console.Write(new string(' ', (sep.Length - 13)/2) + "VS" + new string(' ', (sep.Length - 13) / 2));
            WriteInColor(ConsoleColor.Yellow, "IA");
            Console.WriteLine(" |");

            Console.Write("| ");
            WriteInColor(ConsoleColor.Red, new string('♥',ctrl.player.GetHealth()));
            Console.Write(new string(' ', sep.Length - ctrl.player.GetHealth() - ctrl.computer.GetHealth() - 4));
            WriteInColor(ConsoleColor.Red, new string('♥', ctrl.computer.GetHealth()));
            Console.WriteLine(" |");

            Console.Write("| ");
            WriteInColor(ctrl.player.GetClassColor(), ctrl.player.GetName());
            Console.Write(new string(' ', sep.Length - ctrl.player.GetName().Length - ctrl.computer.GetName().Length - 4));
            WriteInColor(ctrl.computer.GetClassColor(), ctrl.computer.GetName());
            Console.WriteLine(" |");
        }

        public void DisplayTextBox(string[] messages, bool multipleSep)
        {
            int longestLength = 0;
            foreach (string msg in messages) if (longestLength < msg.Length) longestLength = msg.Length;
            string sep = "+" + new string('-', longestLength + 2) + "+";

            Console.WriteLine(sep);
            foreach (string msg in messages)
            {
                Console.WriteLine("| " + msg + new string(' ', longestLength - msg.Length) + " |");
                if (multipleSep) Console.WriteLine(sep);
            }

            if (!multipleSep) Console.WriteLine(sep);
            Console.WriteLine();
        }

        void DisplayActions(int selection)
        {
            string title = " Choisis une action ";

            int longestLength = 22;
            if (longestLength < ctrl.player.GetCapacityName().Length + 4) longestLength = ctrl.player.GetCapacityName().Length + 4;

            int titleLength = longestLength + 4 - title.Length;
            int defLength = 9;
            int atkLength = 12;

            string sep = "+---+" + new string('-', longestLength) + "+";

            string capacityName = ctrl.player.GetCapacityName();
            int damage = ctrl.player.GetDamage();

            // Affichage du titre de la boîte de texte.
            Console.WriteLine("+" + new string('-', longestLength + 4) + "+");
            Console.WriteLine("|" + new string(' ', titleLength / 2) + title + new string(' ', titleLength / 2) + "|");

            // Affichage du tableau des actions.
            Console.WriteLine(sep);
            Console.Write("| ");

            if (selection == 1) WriteInColor(ConsoleColor.Green, "1");
            else Console.Write("1");

            Console.Write(" | ");

            if (selection == 1) WriteInColor(ConsoleColor.Green, "Attaquer");
            else Console.Write("Attaquer");

            Console.Write(" (");
            WriteInColor(ConsoleColor.White, new string('♦', damage));

            Console.Write(")" + new string(' ', longestLength - damage - atkLength) + "|");

            if (selection == 1) WriteInColor(ConsoleColor.Green, cursor);

            Console.WriteLine();
            Console.WriteLine(sep);

            Console.Write("| ");

            if (selection == 2) WriteInColor(ConsoleColor.Green, "2");
            else Console.Write("2");

            Console.Write(" | ");

            if (selection == 2) WriteInColor(ConsoleColor.Green, "Défendre");
            else Console.Write("Défendre");

            Console.Write( new string(' ', longestLength - defLength) + "|");

            if (selection == 2) WriteInColor(ConsoleColor.Green, cursor);

            Console.WriteLine();
            Console.WriteLine(sep);

            Console.Write("| ");

            if (selection == 3) WriteInColor(ConsoleColor.Green,"3");
            else Console.Write("3");

            Console.Write(" | ");

            if (selection == 3) WriteInColor(ConsoleColor.Green, capacityName);
            else Console.Write(capacityName);

            Console.Write(new string(' ', longestLength - capacityName.Length - 1) + "|");

            if (selection == 3) WriteInColor(ConsoleColor.Green, cursor);

            Console.WriteLine();
            Console.WriteLine(sep);
        }

        void DisplayCharacters(int selection)
        {
            DisplayTextBox(new string[1] { "FIGHTASY : le jeu de combat" }, true);


            string sep = "+---+---------+-------+----+";
            Console.WriteLine("+--------------------------+");
            Console.WriteLine("|    Choisis ta classe     |");
            Console.WriteLine(sep);

            for (int i = 1; i < ctrl.characters.Count + 1; i++)
            {

                int longestLength = 0;
                foreach (Character ch in ctrl.characters) if (longestLength < ch.GetName().Length) longestLength = ch.GetName().Length;

                string name = ctrl.characters[i - 1].GetName();
                int health = ctrl.characters[i - 1].GetHealth();
                int damage = ctrl.characters[i - 1].GetDamage();

                Console.Write("|");
                if (selection == i) WriteInColor(ConsoleColor.Green, $" {i} ");
                else Console.Write($" {i} ");

                Console.Write("| ");
                if (selection == i) WriteInColor(ConsoleColor.Green, name);
                else WriteInColor(classColors[i-1], name);

                Console.Write(new string(' ', longestLength - name.Length) + " |");

                WriteInColor(ConsoleColor.Red, " " + new string('♥', health));
                Console.Write(new string(' ', 5 - health) + " |");

                WriteInColor(ConsoleColor.White, " " + new string('♦', damage));

                Console.Write(new string(' ', 2 - damage) + " |");

                if (selection == i) WriteInColor(ConsoleColor.Green, cursor);

                Console.WriteLine("\n" + sep);
            }
        }

        void WriteInColor(ConsoleColor color, string msg)
        {
            Console.ForegroundColor = color;
            Console.Write(msg);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
