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
        List<ConsoleColor> classColors = new List<ConsoleColor> {ConsoleColor.Red, ConsoleColor.Blue, ConsoleColor.White };

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
                                         "|    ||    | ",
                                         "|___o()o___|",
                                         "|__((<>))__|",
                                         "\\   o\\/o   /",
                                         " \\   ||   /", 
                                         "  \\  ||  /",
                                         "   '.||.'", 
                                         "     ``   " });
            asciiArt.Add(new string[9] { "      ,      ",
                                         "   \\  :  /   ", 
                                         "`. __/ \\__ .'",
                                         "_ _\\     /_ _",
                                         "   /_   _\\   ",
                                         " .'  \\ /  `.",
                                         "   /  :  \\   ",
                                         "      '      ",
                                         "             " });
        }

        /** Méthode permettant de récupérer le choix du joueur pour le personnage et les actions.
         *  <param name="actions"> Permet de savoir si le choix porte sur les personnages ou les actions. </param>
         *  <returns> La selection de l'utilisateur sous forme d'index. </returns>
         */
        public int ChooseBox(bool actions)
        {
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
                if (actions) DisplayActions(selection);
                else DisplayCharacters(selection);

                // Enregistrement de la touche pressée par l'utilisateur.
                ConsoleKeyInfo keyPressed = Console.ReadKey();

                // En fonction de la touche pressée :
                switch (keyPressed.Key)
                {
                    case ConsoleKey.DownArrow: // Descend le curseur.
                        // Si le curseur est en bas il revient en première position.
                        if (selection == ctrl.playerCharacters.Count) selection = 1;
                        // Sinon il descend simplement.
                        else selection += 1;

                        break;

                    case ConsoleKey.UpArrow: // Monte le curseur.
                        // Si le curseur est tout en haut il va en dernière position.
                        if (selection == 1) selection = ctrl.playerCharacters.Count;
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

            bool empty = false;
            int firstIndex = 0;
            int lastIndex = 0;

            // Affichage des symboles en ASCII Art en fonction des actions des joueurs.
            if (actionCode == "00")
            {
                for (int i = 0; i < asciiArt[0].Length; i++)
                {
                    Console.Write("|");
                    Console.Write(new string(' ', sep.Length - 2));
                    Console.WriteLine("|");
                }
            }
            else
            {
                firstIndex = int.Parse("" + actionCode[0]) - 1;
                firstIndex = int.Parse("" + actionCode[1]) - 1;

                for (int i = 0; i < asciiArt[0].Length; i++)
                {
                    Console.Write("| ");
                    int middleLength = sep.Length - 4 - asciiArt[firstIndex][i].Length - asciiArt[lastIndex][i].Length;
                    Console.Write(asciiArt[firstIndex][i] + new string(' ', middleLength) + asciiArt[lastIndex][i]);
                    Console.WriteLine(" |");
                }
            }
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
    }
}
