using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fightasy
{
    // Classe qui gère l'affichage en console.
    class HMICUI
    {
        /** Référence au contrôleur. */
        Controller ctrl;
        /** Symbole du curseur rajouté à la fin des boîtes de choix. */
        string cursor = " <--";

        /** Liste des lignes dessinant les symboles ASCII correspondant aux actions des joueurs. */
        List<string[]> asciiArt = new();

        /** Couleurs associées aux classes dans la console. */
        List<ConsoleColor> classColors = new List<ConsoleColor> {ConsoleColor.Red, ConsoleColor.Cyan, ConsoleColor.White, ConsoleColor.Magenta, ConsoleColor.Blue };

        /** Constructeur de la classe.
         *  <param name="ctrl"> Référence du contrôleur de l'application. </param>
         */
        public HMICUI(Controller ctrl)
        { 
            this.ctrl = ctrl;
            asciiArt.Add(new string[9] { "   .   ",
                                         "  / \\  ",
                                         "  |.|  ", 
                                         "  |.|  ",
                                         "  |:|  ",
                                         "  |:|  ",
                                         "`--8--'",
                                         "   8   ",
                                         "   O   " });
            asciiArt.Add(new string[9] { "|`-._/\\_.-`|",
                                         "|    ||    |",
                                         "|___o()o___|",
                                         "|__((<>))__|",
                                        "\\   o\\/o   /",
                                        " \\   ||   / ", 
                                        "  \\  ||  /  ",
                                         "   '.||.'   ", 
                                         "     ``     " });
            asciiArt.Add(new string[9] { "      ,      ",
                                         "   \\  :  /   ", 
                                         "`. __/ \\__ .'",
                                         "_ _\\     /_ _",
                                         "   /_   _\\   ",
                                         " .'  \\ /  `. ",
                                         "   /  :  \\   ",
                                         "      '       ",
                                         "              " });
        }

        /** Méthode permettant de récupérer le choix du joueur pour le personnage et les actions.
         *  <param name="usage"> Permet de savoir si le choix porte sur les personnages ou les actions. </param>
         *  <returns> La selection de l'utilisateur sous forme d'index. </returns>
         */
        public int ChooseBox(int usage)
        {
            int lineCount = 0;
            // La sélection par défaut est 1.
            int selection = 1;
            // Booléen permettant de savoir si on quitte la méthode lorsque le joueur a choisi.
            bool choice = false;

            // Tant que l'utilisateur n'a pas choisi.
            do
            {
                Console.Clear();
                // La méthode fonctionne pour les actions et les personnages il faut donc savoir dans
                // quel cas elle a été appellée en fonction du booléen "actions" passé en paramètre.
                switch (usage)
                {
                    case 0: DisplayGamemode(selection);
                        lineCount = 2;
                        break;
                    case 1: DisplayDifficulty(selection);
                        lineCount = 3;
                        break;
                    case 2: DisplayCharacters(selection);
                        lineCount = ctrl.playerCharacters.Count;
                        break;
                    case 3: DisplayActions(selection);
                        lineCount = 3;
                        break;
                }

                // Enregistrement de la touche pressée par l'utilisateur.
                ConsoleKeyInfo keyPressed = Console.ReadKey();

                // En fonction de la touche pressée :
                switch (keyPressed.Key)
                {
                    case ConsoleKey.DownArrow: // Descend le curseur.
                        // Si le curseur est en bas il revient en première position.
                        if (selection == lineCount) selection = 1;
                        // Sinon il descend simplement.
                        else selection += 1;

                        break;

                    case ConsoleKey.UpArrow: // Monte le curseur.
                        // Si le curseur est tout en haut il va en dernière position.
                        if (selection == 1) selection = lineCount;
                        // Sinon il monte simplement.
                        else selection -= 1;

                        break;

                    case ConsoleKey.Enter: // Le joueur a choisi.
                        Console.Clear();
                        // On sort de la boucle.
                        choice = true;
                        break;
                }
            }
            while (!choice);

            return selection;
        }

        /** Méthode qui affiche le faux écran d'affichage dans la console. */
        public void DisplayScreen(string actionCode)
        {

            // Affichage du titre de la boîte.
            string sep   = "+----------------------------------------+";
            string title = "|      FIGHTASY : le jeu de combat       |";
            Console.WriteLine(sep + "\n" + title + "\n" + sep);

            // Affichage du côté joueur et du côté l'IA.
            Console.Write("| ");
            WriteInColor(ConsoleColor.Blue,"Joueur");
            Console.Write(new string(' ', (sep.Length - 13)/2) + "VS" + new string(' ', (sep.Length - 13) / 2));
            WriteInColor(ConsoleColor.Yellow, "IA");
            Console.WriteLine(" |");

            // Affichage des points de vie de chaque personnages.
            Console.Write("| ");
            WriteInColor(ConsoleColor.Red, new string('♥',ctrl.player.GetHealth()));
            Console.Write(new string(' ', sep.Length - ctrl.player.GetHealth() - ctrl.computer.GetHealth() - 4));
            WriteInColor(ConsoleColor.Red, new string('♥', ctrl.computer.GetHealth()));
            Console.WriteLine(" |");

            // Affichage du nom des classes de chaque joueurs.
            Console.Write("| ");
            WriteInColor(ctrl.player.GetClassColor(), ctrl.player.GetName());
            Console.Write(new string(' ', sep.Length - ctrl.player.GetName().Length - ctrl.computer.GetName().Length - 4));
            WriteInColor(ctrl.computer.GetClassColor(), ctrl.computer.GetName());
            Console.WriteLine(" |");

            Console.Write("|");
            Console.Write(new string(' ', sep.Length-2));
            Console.WriteLine("|");

            // Affichage des symboles en ASCII Art en fonction des actions des joueurs.
            int firstIndex = 0;
            int lastIndex = 0;

            // Affichage vide quand le joueur choisit son action.
            if (actionCode == "00")
            {
                for (int i = 0; i < asciiArt[0].Length; i++)
                {
                    Console.Write("|");
                    Console.Write(new string(' ', sep.Length - 2));
                    Console.WriteLine("|");
                }
            }
            // Affichage des symboles ASCII correspondant aux actions des joueurs.
            else
            {
                firstIndex = int.Parse("" + actionCode[0]) - 1;
                lastIndex = int.Parse("" + actionCode[1]) - 1;

                for (int i = 0; i < asciiArt[0].Length; i++)
                {
                    Console.Write("| ");
                    int middleLength = sep.Length - 4 - asciiArt[firstIndex][i].Length - asciiArt[lastIndex][i].Length;
                    Console.Write(asciiArt[firstIndex][i] + new string(' ', middleLength) + asciiArt[lastIndex][i]);
                    Console.WriteLine(" |");
                }
            }

            Console.WriteLine(sep);
        }

        void DisplayDifficulty(int selection)
        {
            // Affichage du titre de la boîte.
            string sep = "+----------------------------------------+";
            string title = "|      FIGHTASY : le jeu de combat       |";
            string sep2 = "+---+------------------------------------+";
            Console.WriteLine(sep + "\n" + title + "\n" + sep);

            Console.WriteLine("|   Choisissez la difficulté de l'IA :   |");
            Console.WriteLine(sep2);

            // Affichage de la première ligne.
            Console.Write("| ");
            if (selection == 1) WriteInColor(ConsoleColor.Green, "1");
            else Console.Write("1");
            Console.Write(" | ");

            if (selection == 1) WriteInColor(ConsoleColor.Green, "             Facile               ");
            else Console.Write("             Facile               ");

            Console.Write(" |");

            if (selection == 1) WriteInColor(ConsoleColor.Green, cursor);

            Console.WriteLine("\n" + sep2);

            // Affichage de la deuxième ligne.

            Console.Write("| ");
            if (selection == 2) WriteInColor(ConsoleColor.Green, "2");
            else Console.Write("2");
            Console.Write(" | ");

            if (selection == 2) WriteInColor(ConsoleColor.Green, "             Normal               ");
            else Console.Write("             Normal               ");

            Console.Write(" |");

            if (selection == 2) WriteInColor(ConsoleColor.Green, cursor);

            Console.WriteLine("\n" + sep2);

            // Affichage de la troisième ligne.

            Console.Write("| ");
            if (selection == 3) WriteInColor(ConsoleColor.Green, "3");
            else Console.Write("3");
            Console.Write(" | ");

            if (selection == 3) WriteInColor(ConsoleColor.Green, "            Difficile             ");
            else Console.Write("            Difficile             ");

            Console.Write(" |");

            if (selection == 3) WriteInColor(ConsoleColor.Green, cursor);

            Console.WriteLine("\n" + sep2);

        }

        void DisplayGamemode(int selection)
        {
            // Affichage du titre de la boîte.
            string sep   = "+----------------------------------------+";
            string title = "|      FIGHTASY : le jeu de combat       |";
            string sep2  = "+---+------------------------------------+";
            Console.WriteLine(sep + "\n" + title + "\n" + sep);

            Console.WriteLine("|     Choisissez votre mode de jeu :     |");
            Console.WriteLine(sep2);

            // Affichage de la première ligne.
            Console.Write("| ");
            if (selection == 1) WriteInColor(ConsoleColor.Green, "1");
            else Console.Write("1");
            Console.Write(" | ");

            if (selection == 1) WriteInColor(ConsoleColor.Green, "Fightasy classic (Joueur VS IA)   ");
            else Console.Write("Fightasy classic (Joueur VS IA)   ");

            Console.Write(" |");

            if (selection == 1) WriteInColor(ConsoleColor.Green, cursor);

            Console.WriteLine("\n" + sep2);

            // Affichage de la deuxième ligne.

            Console.Write("| ");
            if (selection == 2) WriteInColor(ConsoleColor.Green, "2");
            else Console.Write("2");
            Console.Write(" | ");

            if (selection == 2) WriteInColor(ConsoleColor.Green, "Fightasy simulator (IA VS IA)     ");
            else Console.Write("Fightasy simulator (IA VS IA)     ");

            Console.Write(" |");

            if (selection == 2) WriteInColor(ConsoleColor.Green, cursor);

            Console.WriteLine("\n" + sep2);

        }

        /** Méthode permettant d'afficher des messages entourés d'une "boîte" faite de +, - et de |.
         * <param name="messages"> Les chaînes de caractères à afficher dans la boîte. </param>
         * <param name="multipleSep"> Permet de savoir si l'on veut mettre des séparateurs entre chaque ligne ou non. </param>
         */
        public void DisplayTextBox(string[] messages, bool multipleSep)
        {

            // Récupération de la longueur maximum parmi les chaînes contenues dans "messages".
            int longestLength = 0;
            foreach (string msg in messages) if (longestLength < msg.Length) longestLength = msg.Length;
            // Instanciation du séparateur en fonction de la longueur max.
            string sep = "+" + new string('-', longestLength + 2) + "+";

            Console.WriteLine(sep);

            // Pour chaque message on l'affiche avec les bordures de la boîte.
            foreach (string msg in messages)
            {
                Console.WriteLine("| " + msg + new string(' ', longestLength - msg.Length) + " |");
                if (multipleSep) Console.WriteLine(sep);
            }

            if (!multipleSep) Console.WriteLine(sep);
            Console.WriteLine();
        }

        /** Méthode permettant d'afficher la boîte de choix des actions.
         * <param name="selection"> Index actuel du curseur. </param>
         */
        void DisplayActions(int selection)
        {
            DisplayScreen("00");
            string title = " Choisis une action ";

            // La longueur maximum de la boîte dépend de si la longueur du nom de la capacité de la classe du joueur
            // est plus longue que le titre de la boîte.
            int longestLength = 22;
            if (longestLength < ctrl.player.GetCapacityName().Length + 4) longestLength = ctrl.player.GetCapacityName().Length + 4;

            // Déclaration de différentes longueurs.
            int titleLength = longestLength + 4 - title.Length;
            int defLength = 9;
            int atkLength = 12;

            string sep = "+---+" + new string('-', longestLength) + "+";

            // Récupération des informations du joueur pour les afficher.
            string capacityName = ctrl.player.GetCapacityName();
            int damage = ctrl.player.GetDamage();

            // Affichage du titre de la boîte de texte.
            Console.WriteLine("+" + new string('-', longestLength + 4) + "+");
            Console.WriteLine("|" + new string(' ', titleLength / 2) + title + new string(' ', titleLength / 2) + "|");

            // Affichage du tableau des actions, affichées en vert si la ligne est acutellement sélectionnée.
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

        /** Méthode permettant d'afficher la boîte de choix des personnages.
         * <param name="selection"> Index actuel du curseur. </param>
         */
        void DisplayCharacters(int selection)
        {
            DisplayTextBox(new string[1] { "FIGHTASY : le jeu de combat" }, true);

            // Affichage du titre de la boîte.
            string sep = "+---+---------+-------+----+";
            Console.WriteLine("+--------------------------+");
            Console.WriteLine("|    Choisis ta classe     |");
            Console.WriteLine(sep);

            // Pour chaque personnage on affiche une ligne avec son nom, sa vie et son attaque.
            for (int i = 1; i < ctrl.playerCharacters.Count + 1; i++)
            {
                // La longueur maximum de la boîte dépend du plus grand nom de personnage.
                int longestLength = 0;
                foreach (Character ch in ctrl.playerCharacters) if (longestLength < ch.GetName().Length) longestLength = ch.GetName().Length;

                // Récupération des informations sur le personnage actuel.
                string name = ctrl.playerCharacters[i - 1].GetName();
                int health = ctrl.playerCharacters[i - 1].GetHealth();
                int damage = ctrl.playerCharacters[i - 1].GetDamage();
                // Affichage de la ligne, en verte si elle est actuellement sélectionnée.
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

        /** Méthode permettant d'écrire en couleur dans la console.
         * <param name="color"> La couleur avec laquelle on souhaite écrire. </param>
         * <param name="msg"> La chaîne de caractère à écrire. </param>
         */
        void WriteInColor(ConsoleColor color, string msg)
        {
            Console.ForegroundColor = color;
            Console.Write(msg);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void ColoredMessage(string[] message, string resultAction, bool textBoxSep = true)
        {
            int coloredTime = 70;
            int msgTime = 2000;
            ConsoleColor firstColor = new ConsoleColor();
            ConsoleColor secondColor = new ConsoleColor();
            switch (resultAction[0])
            {
                case '1': firstColor = ConsoleColor.Red; break;
                case '3': firstColor = ctrl.player.GetClassColor(); break;
                default: break;
            }
            switch (resultAction[1])
            {
                case '1': secondColor = ConsoleColor.Red; break;
                case '3': secondColor = ctrl.computer.GetClassColor(); break;
                default: break;
            }

            if (resultAction[0] != '2' && resultAction[1] != '2')
            {
                Console.Clear();
                Console.BackgroundColor = firstColor;
                DisplayScreen(resultAction);
                Console.BackgroundColor = ConsoleColor.Black;
                DisplayTextBox(message, true);
                System.Threading.Thread.Sleep(coloredTime);

                Console.Clear();
                DisplayScreen(resultAction);
                DisplayTextBox(message, true);
                System.Threading.Thread.Sleep(coloredTime * 2);

                Console.Clear();
                Console.BackgroundColor = secondColor;
                DisplayScreen(resultAction);
                Console.BackgroundColor = ConsoleColor.Black;
                DisplayTextBox(message, true);
                System.Threading.Thread.Sleep(coloredTime);

                Console.Clear();
                DisplayScreen(resultAction);
                DisplayTextBox(message, true);
                System.Threading.Thread.Sleep(msgTime);
            }

            if (resultAction[0] == '2' && resultAction[1] != '2')
            {
                System.Threading.Thread.Sleep(coloredTime);

                Console.Clear();
                Console.BackgroundColor = secondColor;
                DisplayScreen(resultAction);
                Console.BackgroundColor = ConsoleColor.Black;
                DisplayTextBox(message, true);
                System.Threading.Thread.Sleep(coloredTime);

                Console.Clear();
                DisplayScreen(resultAction);
                DisplayTextBox(message, true);
                System.Threading.Thread.Sleep(msgTime);
            }

            if (resultAction[0] != '2' && resultAction[1] == '2')
            {
                Console.Clear();
                Console.BackgroundColor = firstColor;
                DisplayScreen(resultAction);
                Console.BackgroundColor = ConsoleColor.Black;
                DisplayTextBox(message, true);
                System.Threading.Thread.Sleep(coloredTime);


                Console.Clear();
                DisplayScreen(resultAction);
                DisplayTextBox(message, true);
                System.Threading.Thread.Sleep(msgTime);

            }

            if (resultAction[0] == '2' && resultAction[1] == '2')
            {
                System.Threading.Thread.Sleep(msgTime);
            }

        }

        public void DisplayStats(int[,] scores)
        {
            Console.Clear();


            string sep =       "+---------+---------+---------+---------+---------+---------+";
            string sepEmpty =  "|         |         |         |         |         |         |";
            Console.WriteLine($"{sep} \n{sepEmpty}");
            Console.Write("|    X    |");
            WriteInColor(ConsoleColor.DarkRed, " Damager ");
            Console.Write("|");
            WriteInColor(ConsoleColor.Cyan, "  Tank   ");
            Console.Write("|");
            WriteInColor(ConsoleColor.White, "  Healer ");
            Console.Write("|");
            WriteInColor(ConsoleColor.Magenta, " Warlock ");
            Console.Write("|");
            WriteInColor(ConsoleColor.Blue, "  Wizard ");
            Console.WriteLine("|");
            Console.WriteLine(sepEmpty);

            for (int j = 0; j < 5; j++)
            {
                Console.WriteLine(sep);
                Console.WriteLine(sepEmpty);
                switch (j)
                {
                    case 0: Console.Write("|");  WriteInColor(ConsoleColor.DarkRed, " Damager "); break;
                    case 1: Console.Write("|");  WriteInColor(ConsoleColor.Cyan, "  Tank   "); break;
                    case 2: Console.Write("|");  WriteInColor(ConsoleColor.White, "  Healer "); break;
                    case 3: Console.Write("|");  WriteInColor(ConsoleColor.Magenta, " Warlock "); break;
                    case 4: Console.Write("|");  WriteInColor(ConsoleColor.Blue, "  Wizard "); break;
                }
                for (int k = 0; k < 5; k++)
                {

                    if (scores[j, k] <= 500)
                    {
                        if (scores[j, k] == 0)
                        {
                            Console.Write("|    X    ");
                        }
                        else if (scores[j, k].ToString().Length > 2)
                        {
                            Console.Write($"|   ");
                            int score = 100 - (scores[j, k] / 10);
                            WriteInColor(ConsoleColor.Green, score.ToString() + "%");
                            Console.Write("   ");
                        } 
                        else
                        {
                            Console.Write($"|   ");
                            int score = 100 - (scores[j, k] / 10);
                            WriteInColor(ConsoleColor.Green, score.ToString() + "%");
                            Console.Write("    ");
                        }
                            
                    }
                    else
                    {
                        if (scores[j, k] == 0)
                        {
                            Console.Write("|    X    ");
                        }
                        else if (scores[j, k].ToString().Length > 2)
                        {
                            Console.Write($"|   ");
                            int score = 100 -(scores[j, k] / 10);
                            WriteInColor(ConsoleColor.Red, score.ToString() + "%");
                            Console.Write("   ");
                        }
                        else
                        {
                            Console.Write($"|   ");
                            int score = 100 - (scores[j, k] / 10);
                            WriteInColor(ConsoleColor.Red, score.ToString() + "%");
                            Console.Write("    ");
                        }
                    }                    


                }
                Console.WriteLine("|");
                Console.WriteLine(sepEmpty);
            }
            Console.WriteLine(sep);
        }
    }
}