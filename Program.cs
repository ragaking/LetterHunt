using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace LetterHunt
{
    class Program
    {
        public static Random rnd = new Random();//this public static members will be useful...
        public static int randomrow, randomcolumn;
        public static string[,] scoreboard = new string[2, 5];//to storage scoreboard
        public static ConsoleColor[] colors = { ConsoleColor.White, ConsoleColor.White, ConsoleColor.White, ConsoleColor.White, ConsoleColor.White, ConsoleColor.Black };//to storage colors which select by user
        public static bool[] options = { false, false, false };//to storage options which select by user
        public static string[] char_set = { "%", "%", "%", "%", "+", "#", "!", "#", "#", "#", "#", "#", "#" };//to storage charsets which select by user
        static void find_empty_field(char[, ,] board)
        {
            randomrow = rnd.Next(0, 25);
            randomcolumn = rnd.Next(0, 60);
            if (board[randomrow, randomcolumn, 0] != '\0')//when selected area not empty,method call itself to try a new coordinates to find a new one...
            {
                find_empty_field(board);
            }
        }//to find an empty field on board
        struct coordinates
        {
            public int row;
            public int column;
        }//to storage snake's coordinates
        static void Main(string[] args)
        {
            //Some customization stuffs
            Console.CursorVisible = false;
            Console.Title = "LETTER HUNT";
            Console.WindowHeight = 35;
            Console.WindowWidth = 80;
            //--------
            Console.ForegroundColor = colors[2];
            read_scoreboard();//to read scoreboard or create a new one

            string menu_select;
            do
            {
                open_effect();//some menu effects...
                menu_select = main_menu();//get an a value to 
                if (menu_select == "P")
                {
                    game();//to play the game
                }
                else if (menu_select == "O")
                {
                    option();//to optimize the settings
                }
                else if (menu_select == "S")
                {
                    skorboard();//to show scoreboard
                }

            } while (menu_select != "E");

        }
        static void open_effect()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.Clear();
                Console.SetCursorPosition(0, 9 - i);
                Console.WriteLine("    ___       ___       ___       ___       ___       ___   ");
                Console.WriteLine("   /\\__\\     /\\  \\     /\\  \\     /\\  \\     /\\  \\     /\\  \\  ");
                Console.WriteLine("  /:/  /    /::\\  \\    \\:\\  \\    \\:\\  \\   /::\\  \\   /::\\  \\ ");
                Console.WriteLine(" /:/__/    /::\\:\\__\\   /::\\__\\   /::\\__\\ /::\\:\\__\\ /::\\:\\__\\");
                Console.WriteLine(" \\:\\  \\    \\:\\:\\/  /  /:/\\/__/  /:/\\/__/ \\:\\:\\/  / \\;:::/  /");
                Console.WriteLine("  \\:\\__\\    \\:\\/  /   \\/__/     \\/__/     \\:\\/  /   |:\\/__/ ");
                Console.WriteLine("   \\/__/     \\/__/                         \\/__/     \\|__|  ");
                Console.WriteLine("");
                Console.WriteLine("            ___       ___       ___       ___   ");
                Console.WriteLine("           /\\__\\     /\\__\\     /\\__\\     /\\  \\  ");
                Console.WriteLine("          /:/__/_   /:/ _/_   /:| _|_    \\:\\  \\ ");
                Console.WriteLine("         /::\\/\\__\\ /:/_/\\__\\ /::|/\\__\\   /::\\__\\");
                Console.WriteLine("         \\/\\::/  / \\:\\/:/  / \\/|::/  /  /:/\\/__/");
                Console.WriteLine("           /:/  /   \\::/  /    |:/  /   \\/__/   ");
                Console.WriteLine("           \\/__/     \\/__/     \\/__/            ");
                Thread.Sleep(100);
            }
        }//a bit effect to menu...
        static string main_menu()
        {
            ConsoleKeyInfo cki;

            string menu = null;

            int cursor = 0;

            Console.Clear();
            Console.WriteLine("    ___       ___       ___       ___       ___       ___   ");
            Console.WriteLine("   /\\__\\     /\\  \\     /\\  \\     /\\  \\     /\\  \\     /\\  \\  ");
            Console.WriteLine("  /:/  /    /::\\  \\    \\:\\  \\    \\:\\  \\   /::\\  \\   /::\\  \\ ");
            Console.WriteLine(" /:/__/    /::\\:\\__\\   /::\\__\\   /::\\__\\ /::\\:\\__\\ /::\\:\\__\\");
            Console.WriteLine(" \\:\\  \\    \\:\\:\\/  /  /:/\\/__/  /:/\\/__/ \\:\\:\\/  / \\;:::/  /");
            Console.WriteLine("  \\:\\__\\    \\:\\/  /   \\/__/     \\/__/     \\:\\/  /   |:\\/__/ ");
            Console.WriteLine("   \\/__/     \\/__/                         \\/__/     \\|__|  ");
            Console.WriteLine("");
            Console.WriteLine("            ___       ___       ___       ___   ");
            Console.WriteLine("           /\\__\\     /\\__\\     /\\__\\     /\\  \\  ");
            Console.WriteLine("          /:/__/_   /:/ _/_   /:| _|_    \\:\\  \\ ");
            Console.WriteLine("         /::\\/\\__\\ /:/_/\\__\\ /::|/\\__\\   /::\\__\\");
            Console.WriteLine("         \\/\\::/  / \\:\\/:/  / \\/|::/  /  /:/\\/__/");
            Console.WriteLine("           /:/  /   \\::/  /    |:/  /   \\/__/   ");
            Console.WriteLine("           \\/__/     \\/__/     \\/__/            ");
            Console.WriteLine();
            Console.WriteLine("                          Play");
            Console.WriteLine();
            Console.WriteLine("                         Option");
            Console.WriteLine();
            Console.WriteLine("                       Scoreboard");
            Console.WriteLine();
            Console.WriteLine("                          Exit");
            while (true)
            {
                Console.SetCursorPosition(21, 16 + cursor * 2);
                Console.Write(">");
                Console.SetCursorPosition(34, 16 + cursor * 2);
                Console.Write("<");

                cki = Console.ReadKey();

                Console.SetCursorPosition(21, 16 + cursor * 2);
                Console.Write("  ");
                Console.SetCursorPosition(33, 16 + cursor * 2);
                Console.Write("    ");

                if (cki.Key == ConsoleKey.UpArrow)
                {
                    cursor--;
                }
                else if (cki.Key == ConsoleKey.DownArrow)
                {
                    cursor++;
                }
                else if (cki.Key == ConsoleKey.Enter)
                {
                    if (cursor == 0)
                    {
                        menu = "P";
                    }
                    else if (cursor == 1)
                    {
                        menu = "O";
                    }
                    else if (cursor == 2)
                    {
                        menu = "S";
                    }
                    else
                    {
                        menu = "E";
                    }
                    break;
                }
                if (cursor < 0)
                {
                    cursor = 3;
                }
                else if (cursor > 3)
                {
                    cursor = 0;
                }
            }

            return menu;
        }//this function returns a string which you want..exp:play game or options
        static void option()
        {
            int menu_select = 0;
            ConsoleKeyInfo cki;
            open_effect();


            while (true)
            {
                Console.SetCursorPosition(0, 15);
                Console.WriteLine();
                Console.WriteLine("                       Game Play");
                Console.WriteLine();
                Console.WriteLine("                        Colors");
                Console.WriteLine();
                Console.WriteLine("                       Char-Sets");

                Console.SetCursorPosition(21, 16 + menu_select * 2);
                Console.Write(">");
                Console.SetCursorPosition(33, 16 + menu_select * 2);
                Console.Write("<");

                cki = Console.ReadKey();

                Console.SetCursorPosition(21, 16 + menu_select * 2);
                Console.Write(" ");
                Console.SetCursorPosition(33, 16 + menu_select * 2);
                Console.Write("  ");

                if (cki.Key == ConsoleKey.UpArrow)
                {
                    menu_select--;
                }
                else if (cki.Key == ConsoleKey.DownArrow)
                {
                    menu_select++;
                }
                else if (cki.Key == ConsoleKey.Enter)
                {
                    if (menu_select == 0)
                    {
                        game_play();
                    }
                    else if (menu_select == 1)
                    {
                        customize_colors();
                    }
                    else if (menu_select == 2)
                    {
                        customize_chars();
                    }
                    open_effect();
                }
                else if (cki.Key == ConsoleKey.Escape)
                {
                    break;
                }
                if (menu_select < 0)
                {
                    menu_select = 2;
                }
                else if (menu_select > 2)
                {
                    menu_select = 0;
                }
            }



        }//option
        static void game_play()
        {
            int menu_select = 0;
            ConsoleKeyInfo cki;

            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("                            Customize Gameplay");
                Console.WriteLine();
                Console.WriteLine("                       Pass From Grid        : " + options[0]);
                Console.WriteLine();
                Console.WriteLine("                       Fog of War            : " + options[1]);
                Console.WriteLine();
                Console.WriteLine("                       Only Around Sneak(FOG): " + options[2]);

                Console.SetCursorPosition(21, menu_select * 2 + 3);
                Console.Write(">");
                Console.SetCursorPosition(53, menu_select * 2 + 3);
                Console.Write("<");

                cki = Console.ReadKey();

                Console.SetCursorPosition(21, menu_select * 2 + 3);
                Console.Write(" ");
                Console.SetCursorPosition(53, menu_select * 2 + 3);
                Console.Write(" ");

                if (cki.Key == ConsoleKey.UpArrow)
                {
                    menu_select--;
                }
                else if (cki.Key == ConsoleKey.DownArrow)
                {
                    menu_select++;
                }
                else if (cki.Key == ConsoleKey.Enter)
                {
                    if (options[menu_select])
                    {
                        options[menu_select] = false;
                    }
                    else
                    {
                        options[menu_select] = true;
                    }
                }
                else if (cki.Key == ConsoleKey.Escape)
                {
                    break;
                }
                if (menu_select < 0)
                {
                    menu_select = 2;
                }
                else if (menu_select > 2)
                {
                    menu_select = 0;
                }
            }//gameplay
        }//to customize options
        static void customize_chars()
        {
            int i = 0, j = 0;
            ConsoleKeyInfo cki;
            //this is string storage 4 char-sets which can be setup by user...
            string[,] storage_chars = { { "%", "%", "%", "%", "+", "#", "!", "#", "#", "#", "#", "#", "#", }, { "☺", "☺", "☺", "☺", "☻", "■", "☺", "/", "\\", "\\", "/", "|", "-", }, { "►", "◄", "▼", "▲", "☼", "♦", "░", "╔", "╗", "╚", "╝", "║", "═" }, { "→", "←", "↓", "↑", "§", "♫", "?", "░", "░", "░", "░", "░", "░" } };

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("                            Customize Char-Sets");
            Console.WriteLine();
            Console.WriteLine("                     Snake  Tail   Wall   FOG    Grid ");
            Console.WriteLine("                     -----  -----  -----  -----  -----");
            Console.WriteLine();
            Console.WriteLine("                     Set1   Set1   Set1   Set1   Set1  ");
            Console.WriteLine();
            Console.WriteLine("                     Set2   Set2   Set2   Set2   Set2  ");
            Console.WriteLine();
            Console.WriteLine("                     Set3   Set3   Set3   Set3   Set3  ");
            Console.WriteLine();
            Console.WriteLine("                     Set4   Set4   Set4   Set4   Set4  ");
            while (true)
            {
                Console.SetCursorPosition(0, 13);
                preview();

                Console.SetCursorPosition(20 + j * 7, 6 + i * 2);
                Console.Write(">");
                Console.SetCursorPosition(25 + j * 7, 6 + i * 2);
                Console.Write("<");

                cki = Console.ReadKey();

                Console.SetCursorPosition(20 + j * 7, 6 + i * 2);
                Console.Write(" ");
                Console.SetCursorPosition(25 + j * 7, 6 + i * 2);
                Console.Write("  ");

                if (cki.Key == ConsoleKey.UpArrow)
                {
                    i--;
                }
                else if (cki.Key == ConsoleKey.DownArrow)
                {
                    i++;
                }
                else if (cki.Key == ConsoleKey.LeftArrow)
                {
                    j--;
                }
                else if (cki.Key == ConsoleKey.RightArrow)
                {
                    j++;
                }
                else if (cki.Key == ConsoleKey.Enter)
                {
                    if (j == 0)
                    {
                        for (int u = 0; u < 4; u++)
                        {
                            char_set[u] = storage_chars[i, u];
                        }
                    }
                    else if (j == 1)
                    {
                        char_set[4] = storage_chars[i, 4];
                    }
                    else if (j == 2)
                    {
                        char_set[5] = storage_chars[i, 5];
                    }
                    else if (j == 3)
                    {
                        char_set[6] = storage_chars[i, 6];
                    }
                    else
                    {
                        for (int u = 0; u < 6; u++)
                        {
                            char_set[u + 7] = storage_chars[i, u + 7];
                        }
                    }
                }
                else if (cki.Key == ConsoleKey.Escape)
                {
                    break;
                }
                if (i < 0)
                {
                    i = 3;
                }
                else if (i > 3)
                {
                    i = 0;
                }
                if (j < 0)
                {
                    j = 4;
                }
                else if (j > 4)
                {
                    j = 0;
                }



            }


        }//to customize char-set
        static void skorboard()
        {
            Console.Clear();
            Console.WriteLine("    ___       ___       ___       ___       ___       ___   ");
            Console.WriteLine("   /\\__\\     /\\  \\     /\\  \\     /\\  \\     /\\  \\     /\\  \\  ");
            Console.WriteLine("  /:/  /    /::\\  \\    \\:\\  \\    \\:\\  \\   /::\\  \\   /::\\  \\ ");
            Console.WriteLine(" /:/__/    /::\\:\\__\\   /::\\__\\   /::\\__\\ /::\\:\\__\\ /::\\:\\__\\");
            Console.WriteLine(" \\:\\  \\    \\:\\:\\/  /  /:/\\/__/  /:/\\/__/ \\:\\:\\/  / \\;:::/  /");
            Console.WriteLine("  \\:\\__\\    \\:\\/  /   \\/__/     \\/__/     \\:\\/  /   |:\\/__/ ");
            Console.WriteLine("   \\/__/     \\/__/                         \\/__/     \\|__|  ");
            Console.WriteLine("");
            Console.WriteLine("            ___       ___       ___       ___   ");
            Console.WriteLine("           /\\__\\     /\\__\\     /\\__\\     /\\  \\  ");
            Console.WriteLine("          /:/__/_   /:/ _/_   /:| _|_    \\:\\  \\ ");
            Console.WriteLine("         /::\\/\\__\\ /:/_/\\__\\ /::|/\\__\\   /::\\__\\");
            Console.WriteLine("         \\/\\::/  / \\:\\/:/  / \\/|::/  /  /:/\\/__/");
            Console.WriteLine("           /:/  /   \\::/  /    |:/  /   \\/__/   ");
            Console.WriteLine("           \\/__/     \\/__/     \\/__/            ");

            Console.SetCursorPosition(21, 16);
            Console.Write("   SCORE BOARD");
            Console.SetCursorPosition(18, 18);
            Console.Write(char_set[7]);
            for (int i = 0; i < 22; i++)
            {
                Console.Write(char_set[12]);
            }
            Console.Write(char_set[8]);

            for (int i = 0; i < scoreboard.GetLength(1); i++)
            {
                Console.SetCursorPosition(18, 19 + i * 2);
                Console.Write(char_set[11]);
                Console.SetCursorPosition(41, 19 + i * 2);
                Console.Write(char_set[11]);

                Console.SetCursorPosition(18, 20 + i * 2);
                Console.Write(char_set[11] + " " + (i + 1) + "- " + scoreboard[0, i]);
                Console.SetCursorPosition(36, 20 + i * 2);
                Console.Write(scoreboard[1, i] + " " + char_set[11]);
            }

            Console.SetCursorPosition(18, 29);
            Console.Write(char_set[11]);
            Console.SetCursorPosition(41, 29);
            Console.Write(char_set[11]);

            Console.SetCursorPosition(18, 30);
            Console.Write(char_set[9]);
            for (int i = 0; i < 22; i++)
            {
                Console.Write(char_set[12]);
            }
            Console.Write(char_set[10]);



            Console.SetCursorPosition(18, 32);
            Console.Write("Press any key to main menu...");
            Console.ReadKey();

        }//to see the score board
        static void customize_colors()
        {
            int i = 0, j = 0;
            ConsoleKeyInfo cki;
            //to storage all consolecolors
            ConsoleColor[] storagecolors = { ConsoleColor.Black, ConsoleColor.DarkBlue, ConsoleColor.DarkGreen, ConsoleColor.DarkCyan, ConsoleColor.DarkRed, ConsoleColor.DarkMagenta, ConsoleColor.DarkYellow, ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.Blue, ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Red, ConsoleColor.Magenta, ConsoleColor.Yellow, ConsoleColor.White };

            while (true)
            {
                Console.ForegroundColor = colors[2];
                Console.BackgroundColor = colors[5];
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("                            Customize Color");
                Console.WriteLine();
                Console.WriteLine("   Letter       Wall     Grid-Font      Snake    Fog of War  Backgruond");
                Console.WriteLine(" ----------  ----------  ----------  ----------  ----------  ----------");
                Console.WriteLine("    Black       Black       Black       Black       Black       Black   ");
                Console.WriteLine("    DBlue       DBlue       DBlue       DBlue       DBlue       DBlue   ");
                Console.WriteLine("   DGreen      DGreen      DGreen      DGreen      DGreen      DGreen   ");
                Console.WriteLine("    DCyan       DCyan       DCyan       DCyan       DCyan       DCyan   ");
                Console.WriteLine("    DRed        DRed        DRed        DRed        DRed        DRed    ");
                Console.WriteLine("  DMagenta    DMagenta    DMagenta    DMagenta    DMagenta    DMagenta  ");
                Console.WriteLine("   DYellow     DYellow     DYellow     DYellow     DYellow     DYellow  ");
                Console.WriteLine("    Gray        Gray        Gray        Gray        Gray        Gray    ");
                Console.WriteLine("    DGray       DGray       DGray       DGray       DGray       DGray   ");
                Console.WriteLine("    Blue        Blue        Blue        Blue        Blue        Blue    ");
                Console.WriteLine("    Green       Green       Green       Green       Green       Green   ");
                Console.WriteLine("    Cyan        Cyan        Cyan        Cyan        Cyan        Cyan    ");
                Console.WriteLine("    Red         Red         Red         Red         Red         Red     ");
                Console.WriteLine("   Magenta     Magenta     Magenta     Magenta     Magenta     Magenta  ");
                Console.WriteLine("   Yellow      Yellow      Yellow      Yellow      Yellow      Yellow   ");
                Console.WriteLine("    White       White       White       White       White       White   ");

                Console.WriteLine();
                preview();

                Console.ForegroundColor = colors[2];

                Console.SetCursorPosition(i * 12, 5 + j);
                Console.Write(">");
                Console.SetCursorPosition(11 + i * 12, 5 + j);
                Console.Write("<");

                cki = Console.ReadKey();

                Console.SetCursorPosition(i * 12, 5 + j);
                Console.Write(" ");
                Console.SetCursorPosition(11 + i * 12, 5 + j);
                Console.Write(" ");

                if (cki.Key == ConsoleKey.UpArrow)
                {
                    j--;
                }
                else if (cki.Key == ConsoleKey.DownArrow)
                {
                    j++;
                }
                else if (cki.Key == ConsoleKey.LeftArrow)
                {
                    i--;
                }
                else if (cki.Key == ConsoleKey.RightArrow)
                {
                    i++;
                }
                else if (cki.Key == ConsoleKey.Enter)
                {
                    //set color which selected by user to []colors
                    colors[i] = storagecolors[j];
                }
                else if (cki.Key == ConsoleKey.Escape)
                {
                    break;
                }
                if (i < 0)
                {
                    i = 5;
                }
                else if (i > 5)
                {
                    i = 0;
                }
                if (j < 0)
                {
                    j = 15;
                }
                else if (j > 15)
                {
                    j = 0;
                }
            }

        }//to customize colors
        static void preview()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("                                 Preview");
            Console.WriteLine();
            Console.Write("                            ");
            Console.ForegroundColor = colors[2];
            Console.Write(char_set[7]);
            for (int i = 0; i < 9; i++)
            {
                Console.Write(char_set[12]);
            }
            Console.WriteLine();
            Console.Write("                            ");
            Console.Write(char_set[11]);
            Console.ForegroundColor = colors[1];
            for (int i = 0; i < 7; i++)
            {
                Console.Write(char_set[5]);
            }
            Console.WriteLine();
            Console.Write("                            ");
            Console.ForegroundColor = colors[2];
            Console.Write(char_set[11]);
            Console.ForegroundColor = colors[3];
            Console.WriteLine("     " + char_set[3]);
            Console.Write("                            ");
            Console.ForegroundColor = colors[2];
            Console.Write(char_set[11]);
            Console.ForegroundColor = colors[3];
            Console.WriteLine("C" + char_set[4] + char_set[0] + "  A" + char_set[4]);
            Console.Write("                            ");
            Console.ForegroundColor = colors[2];
            Console.WriteLine(char_set[11]);
            Console.Write("                            ");
            Console.Write(char_set[11]);
            Console.ForegroundColor = colors[0];
            Console.WriteLine(" J Y E H");
            Console.Write("                            ");
            Console.ForegroundColor = colors[2];
            Console.WriteLine(char_set[11]);
            Console.Write("                            ");
            Console.Write(char_set[11]);
            Console.ForegroundColor = colors[4];
            for (int i = 0; i < 8; i++)
            {
                Console.Write(char_set[6]);
            }
            Console.ForegroundColor = colors[2];


        }//to preview char-sets and colors in customize char-sets or colors
        static void game()
        {
            //when game over escape to loops...
            bool game = true;
            //an key variable...
            ConsoleKeyInfo cki;
            //three dimensional to storage al board data
            char[, ,] board;
            //to storage snake's direction
            int direction;
            //to storage snake's coordinates
            coordinates[] snake;
            //to storage snake's last character's coordinates
            coordinates temp;
            //to determinate next correct letter
            int correct_letter;
            //to storage statement
            string statement = null;
            //to storage snake's characters
            string str_snake;
            //to dont get same idiom or proverb
            int select_statement, temp_select;
            //to calculate move and totalmove
            int move = 0, total_move = 0;


            for (int level = 1; level < 5; level++)
            {
                temp_select = -1;
                for (int stage = 1; stage < 3; stage++)
                {
                    //when second statement same first one get a new one....
                    do
                    {
                        select_statement = rnd.Next((level - 1) * 5, (level - 1) * 5 + 5);
                    } while (temp_select == select_statement);
                    temp_select = select_statement;
                    //--------
                    //to build snake with char-sets
                    str_snake = char_set[0] + char_set[4];
                    //to reset all board
                    board = new char[25, 60, 3];
                    //to set direction none
                    direction = 4;
                    //reset coordinate for snake's tail and head
                    snake = new coordinates[2];
                    //to reset next correct letter's counter
                    correct_letter = 0;
                    //to reset move level by level
                    move = 0;

                    //to prepare all game in same method...
                    prepare_game(ref board, level, select_statement, ref statement, ref snake);

                    //to write some informations.....
                    Console.SetCursorPosition(20, 29);
                    Console.Write("Level:" + level);
                    Console.SetCursorPosition(20, 31);
                    Console.Write("Stage:" + stage);
                    Console.SetCursorPosition(2, 31);
                    Console.Write("Total Move:    \b\b\b\b" + total_move);

                    //when palyer dont select fog of war only around the snake set all board to fog...
                    if (!options[2] && options[1])
                    {
                        for (int i = 0; i < 25; i++)
                        {
                            for (int j = 0; j < 60; j++)
                            {
                                board[i, j, 2] = '1';
                            }
                        }
                    }

                    while (game)//loop where game is played....
                    {
                        //to set all stuff on board and write board...
                        set_board(board, str_snake, snake, move);

                        //storage last snake's coordinates
                        temp = snake[0];

                        //to slide snake's coodinates by one by....
                        for (int i = 0; i < snake.Length - 1; i++)
                        {
                            snake[snake.Length - 1 - i] = snake[snake.Length - 2 - i];
                        }

                        Thread.Sleep(150);

                        //to work only if user press an button
                        if (Console.KeyAvailable)
                        {
                            //set the direction by pressed key
                            cki = Console.ReadKey();
                            
                            if (direction == 0 || direction == 2 || direction == 4)
                            {
                                if (cki.Key == ConsoleKey.UpArrow)
                                {
                                    direction = 1;
                                }
                                else if (cki.Key == ConsoleKey.DownArrow)
                                {
                                    direction = 3;
                                }
                            }
                            if (direction == 1 || direction == 3 || direction == 4)
                            {
                                if (cki.Key == ConsoleKey.LeftArrow)
                                {
                                    direction = 2;
                                }
                                else if (cki.Key == ConsoleKey.RightArrow)
                                {
                                    direction = 0;
                                }
                            }
                        }

                        //set snake and snake's head coordinates according to direction
                        if (direction == 0)
                        {
                            snake[0].column++;
                            str_snake = char_set[0] + str_snake.Substring(1);
                            move++;
                        }
                        else if (direction == 1)
                        {
                            snake[0].row--;
                            str_snake = char_set[3] + str_snake.Substring(1);
                            move++;
                        }
                        else if (direction == 2)
                        {
                            snake[0].column--;
                            str_snake = char_set[1] + str_snake.Substring(1);
                            move++;
                        }
                        else if (direction == 3)
                        {
                            snake[0].row++;
                            str_snake = char_set[2] + str_snake.Substring(1);
                            move++;
                        }

                        //to pass grid to grid if player activate first option
                        if (options[0])
                        {
                            if (snake[0].row < 0)
                            {
                                snake[0].row = 24;
                            }
                            else if (snake[0].row > 24)
                            {
                                snake[0].row = 0;
                            }
                            if (snake[0].column < 0)
                            {
                                snake[0].column = 59;
                            }
                            else if (snake[0].column > 59)
                            {
                                snake[0].column = 0;
                            }
                        }

                        //when player dont activate first option,to finish the game if snake get out board...
                        if (snake[0].column > 59 || snake[0].column < 0 || snake[0].row < 0 || snake[0].row > 24)
                        {
                            game_over(level, stage, total_move);
                            game = false;
                            break;
                        }
                        //when user hit a wall ,game over...
                        if (board[snake[0].row, snake[0].column, 0] == Convert.ToChar(char_set[5]))
                        {
                            game_over(level, stage, total_move);
                            game = false;
                        }
                        else if (board[snake[0].row, snake[0].column, 0] != '\0')//if snake eats an letter...
                        {
                            //expand the array for new character...
                            Array.Resize(ref snake, snake.Length + 1);
                            if (board[snake[0].row, snake[0].column, 0] == statement[correct_letter]) //eat correct letter...
                            {
                                //add the correct letter to snake in order(correct_letter+1)...
                                str_snake = str_snake.Insert(correct_letter + 1, statement[correct_letter].ToString());
                                correct_letter++;
                            }
                            else//eat wrong letter...
                            {
                                //add wrong letter to snake
                                str_snake += board[snake[0].row, snake[0].column, 0];
                                //find an empty field for wrong eaten letter...
                                find_empty_field(board);
                                //place wrong eaten letter to new place
                                board[randomrow, randomcolumn, 0] = board[snake[0].row, snake[0].column, 0];
                                //add 40 point to move to punishment the player...
                                move += 40;
                            }
                            //set temp-coordinates to snake's last character...
                            snake[snake.Length - 1] = temp;
                            //reset zone which eaten letter on
                            board[snake[0].row, snake[0].column, 0] = '\0';
                        }

                        //to pass next level when user collect all letters in order
                        if (correct_letter == statement.Length)
                        {
                            break;
                        }
                        //to check that does snake hit its own body...
                        for (int i = 4; i < snake.Length; i++)
                        {
                            if (snake[0].row == snake[i].row && snake[0].column == snake[i].column)
                            {
                                game_over(level, stage, total_move);
                                game = false;
                            }
                        }
                        //add move to totalmove...

                    }//game loop...

                    //to dont get extra move while escaping loop...
                    total_move += move;

                    if (!game)
                    {
                        break;
                    }
                    Console.Clear();

                }//stage loop...
                if (!game)
                {
                    break;
                }
            }//level loop...
            if (game)
            {
                //to record player's totalmove if user pass all level succsefully
                record_scoreboard(total_move);
            }
        }//an method where play game on
        static void prepare_game(ref char[, ,] board, int level, int select_statement, ref string statement, ref coordinates[] snake)
        {
            //an bool to check that is there a one space between wall... 
            bool check = true;
            for (int u = 0; u < level * 2; u++)
            {
                if (rnd.Next(0, 2) == 0)
                {
                    do
                    {
                        check = false;
                        randomrow = rnd.Next(0, 25);
                        randomcolumn = rnd.Next(0, 60);
                        for (int i = randomrow - 1; i < randomrow + 2; i++)
                        {
                            for (int j = randomcolumn - 5; j < randomcolumn + 7; j++)
                            {
                                if (i > -1 && i < 25 && j > -1 && j < 60)
                                {
                                    if (board[i, j, 0] != '\0')
                                    {
                                        check = true;
                                    }
                                }
                            }
                        }

                    } while (check);
                    for (int i = randomcolumn - 4; i < randomcolumn + 6; i++)
                    {
                        if (i > -1 && i < 60)
                        {
                            board[randomrow, i, 0] = Convert.ToChar(char_set[5]);
                        }
                    }

                }
                else
                {
                    do
                    {
                        check = false;
                        randomrow = rnd.Next(0, 25);
                        randomcolumn = rnd.Next(0, 60);
                        for (int i = randomrow - 5; i < randomcolumn + 7; i++)
                        {
                            for (int j = randomcolumn - 1; j < randomcolumn + 2; j++)
                            {
                                if (i > -1 && i < 25 && j > -1 && j < 60)
                                {
                                    if (board[i, j, 0] != '\0')
                                    {
                                        check = true;
                                    }
                                }
                            }
                        }

                    } while (check);
                    for (int i = randomrow - 4; i < randomrow + 6; i++)
                    {
                        if (i > -1 && i < 25)
                        {
                            board[i, randomcolumn, 0] = Convert.ToChar(char_set[5]);
                        }
                    }
                }
            }//wall_assingment

            //read statement
            StreamReader reader = File.OpenText("..\\..\\statements.txt");

            //read to no avail for reach wanted idiom or proverb...
            for (int i = 0; i < select_statement; i++)
            {
                reader.ReadLine();
            }

            for (int i = 0; i < 2; i++)
            {
                reader.Read();
            }

            string temp_statement = reader.ReadLine().ToUpper();

            //to write statement and its information
            Console.SetCursorPosition(30, 29);
            Console.Write(temp_statement);
            Console.SetCursorPosition(30, 31);
            if (level == 1)
            {
                Console.Write("TWO-WORDED IDIOM");
            }
            else if (level == 2)
            {
                Console.Write("THREE-WORDED IDIOM");
            }
            else if (level == 3)
            {
                Console.Write("FOUR-WORDED IDIOM");
            }
            else
            {
                Console.Write("PROVERBS");
            }

            //to dont set empty value to board...
            statement = temp_statement.Replace(" ", "");

            //aplhabet assingment
            string alphabet = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ";

            for (int i = 0; i < alphabet.Length; i++)
            {
                find_empty_field(board);
                board[randomrow, randomcolumn, 0] = alphabet[i];
            }

            //statement assingment
            for (int i = 0; i < statement.Length; i++)
            {
                find_empty_field(board);
                board[randomrow, randomcolumn, 0] = statement[i];
            }

            //find a empty square for snake...
            do
            {
                check = false;
                find_empty_field(board);
                for (int i = randomrow - 1; i < randomrow + 2; i++)
                {
                    for (int j = randomcolumn - 1; j < randomcolumn + 2; j++)
                    {
                        if (i > -1 && i < 25 && j > -1 && j < 60)
                        {
                            if (board[i, j, 0] != '\0')
                            {
                                check = true;
                            }
                        }
                    }
                }
            } while (check);

            //set selected zone to snake...
            for (int i = 0; i < 2; i++)
            {
                snake[i].row = randomrow;
                snake[i].column = randomcolumn;
            }

        }//all prepare stuff..ex:read statement,assingment wall and etc...
        static void set_board(char[, ,] board, string str_snake, coordinates[] snake, int move)
        {
            //if player activate fog of war...
            if (options[1])
            {
                //an integer to get an matrix araound snake...
                int x = 0;

                //if user activate only around the snake
                if (options[2])
                {
                    for (int i = 0; i < 25; i++)
                    {
                        for (int j = 0; j < 60; j++)
                        {
                            board[i, j, 2] = '2';
                        }
                    }
                }

                //-------set values around the snake
                for (int i = snake[0].row - 7; i < snake[0].row - 2; i++)
                {
                    for (int j = snake[0].column - 2 - x; j < snake[0].column + 3 + x; j++)
                    {
                        if (i > -1 && i < 25 && j > -1 && j < 60)
                        {
                            if (options[2])
                            {
                                if (i == snake[0].row - 7 || j == snake[0].column - 2 - x || j == snake[0].column + 2 + x)
                                {
                                    board[i, j, 2] = '1';
                                }
                                else
                                {
                                    board[i, j, 2] = '\0';
                                }
                            }
                            else
                            {
                                board[i, j, 2] = '\0';
                            }
                        }
                    }
                    x++;
                }

                for (int i = snake[0].row - 2; i < snake[0].row + 3; i++)
                {
                    for (int j = snake[0].column - 7; j < snake[0].column + 8; j++)
                    {
                        if (i > -1 && i < 25 && j > -1 && j < 60)
                        {
                            if (options[2])
                            {
                                if (j == snake[0].column - 7 || j == snake[0].column + 7)
                                {
                                    board[i, j, 2] = '1';
                                }
                                else
                                {
                                    board[i, j, 2] = '\0';
                                }
                            }
                            else
                            {
                                board[i, j, 2] = '\0';
                            }

                        }
                    }
                }
                x--;

                for (int i = snake[0].row + 3; i < snake[0].row + 8; i++)
                {
                    for (int j = snake[0].column - 2 - x; j < snake[0].column + 3 + x; j++)
                    {
                        if (i > -1 && i < 25 && j > -1 && j < 60)
                        {
                            if (options[2])
                            {
                                if (i == snake[0].row + 7 || j == snake[0].column - 2 - x || j == snake[0].column + 2 + x)
                                {
                                    board[i, j, 2] = '1';
                                }
                                else
                                {
                                    board[i, j, 2] = '\0';
                                }
                            }
                            else
                            {
                                board[i, j, 2] = '\0';
                            }
                        }
                    }
                    x--;
                }
                //---------------------
            }//fog of war....

            //set snake to board....
            for (int i = 0; i < snake.Length; i++)
            {
                board[snake[i].row, snake[i].column, 0] = str_snake[i];
                board[snake[i].row, snake[i].column, 1] = '3';
            }


            //setting values for coloring
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 60; j++)
                {
                    if (board[i, j, 2] == '\0')
                    {
                        if (board[i, j, 0] == Convert.ToChar(char_set[5]))
                        {
                            board[i, j, 1] = '1';
                        }
                        else if (board[i, j, 1] != '3')
                        {
                            board[i, j, 1] = '0';
                        }
                    }
                    else if (!options[2] && board[i, j, 2] == '1')
                    {
                        if (board[i, j, 0] == Convert.ToChar(char_set[5]))
                        {
                            board[i, j, 1] = '1';
                        }
                        else if (board[i, j, 1] != '3')
                        {
                            board[i, j, 1] = '0';
                        }
                    }
                    else
                    {
                        board[i, j, 1] = '4';
                    }
                }
            }

            //time write board....
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = colors[2];
            Console.Write(char_set[7]);
            for (int i = 0; i < board.GetLength(1); i++)
            {
                Console.Write(char_set[12]);
            }
            Console.WriteLine(char_set[8]);

            for (int i = 0; i < board.GetLength(0); i++)
            {
                Console.Write(char_set[11]);
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    //setting color...
                    Console.ForegroundColor = colors[Convert.ToInt16(Convert.ToString(board[i, j, 1]))];
                    if (!options[2])
                    {
                        if (board[i, j, 2] == '1')
                        {
                            Console.ForegroundColor = colors[4];
                        }
                    }
                    //if-else statement for fog of war...
                    if (board[i, j, 2] == '\0')
                    {
                        Console.Write(board[i, j, 0]);
                    }
                    else if (board[i, j, 2] == '2')
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write(char_set[6]);
                    }

                }
                Console.ForegroundColor = colors[2];
                Console.WriteLine(char_set[11]);
            }
            Console.Write(char_set[9]);
            for (int i = 0; i < board.GetLength(1); i++)
            {
                Console.Write(char_set[12]);
            }
            Console.WriteLine(char_set[10]);
            //wirte move...
            Console.SetCursorPosition(2, 29);
            Console.Write("Move:    \b\b\b\b" + move);





            //remove snake from game board....
            for (int i = 0; i < snake.Length; i++)
            {
                board[snake[i].row, snake[i].column, 0] = '\0';
            }


        }//to write board...
        static void game_over(int level, int stage, int total_move)
        {
            //to use all console colors.
            ConsoleColor[] storagecolors = { ConsoleColor.Black, ConsoleColor.DarkBlue, ConsoleColor.DarkGreen, ConsoleColor.DarkCyan, ConsoleColor.DarkRed, ConsoleColor.DarkMagenta, ConsoleColor.DarkYellow, ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.Blue, ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Red, ConsoleColor.Magenta, ConsoleColor.Yellow, ConsoleColor.White };

            Console.Clear();
            int[] row = new int[150];
            int[] column = new int[150];
            int i = 0;
            bool flag = true;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(13, 4);
            Console.WriteLine("                 *             )            (    ");
            Console.SetCursorPosition(13, 5);
            Console.WriteLine("         (     (  `         ( /(            )\\ ) ");
            Console.SetCursorPosition(13, 6);
            Console.WriteLine("    )    )\\    )\\))(  (     )\\())(   (  (  (()/( ");
            Console.SetCursorPosition(13, 7);
            Console.WriteLine("   /( ((((_)( ((_)()\\ )\\   ((_)\\ )\\  )\\ )\\  /(_)) ");
            Console.SetCursorPosition(13, 8);
            Console.WriteLine("  (_))_)\\ _ )\\(_()((_|(_)    ((_|(_)((_|(_)(_)) ");
            Console.ForegroundColor = colors[2];
            Console.SetCursorPosition(13, 9);
            Console.WriteLine("   ) __(_)_\\(_)  \\/  | __|  / _ \\ \\ / /| __| _ \\ ");
            Console.SetCursorPosition(13, 10);
            Console.WriteLine("  | (_ |/ _ \\ | |\\/| | _|  | (_) \\ V / | _||   / ");
            Console.SetCursorPosition(13, 11);
            Console.WriteLine("   \\___/_/ \\_\\|_|  |_|___|  \\___/ \\_/  |___|_|_\\ ");
            Console.SetCursorPosition(24, 13);
            Console.Write("Total Move:" + total_move + " Level:" + level + " Stage:" + stage);

            do
            {
                Thread.Sleep(20);
                do
                {
                    flag = true;
                    row[i] = rnd.Next(2, 16);

                    if (row[i] != 2 && row[i] != 15)
                    {
                        int a = rnd.Next(0, 2);
                        if (a == 0)
                        {
                            column[i] = 7;
                        }
                        else
                            column[i] = 69;
                    }
                    else
                        column[i] = rnd.Next(7, 70);
                    if (i != 0)
                    {

                        for (int m = i - 1; m >= 0; m--)
                        {
                            if (row[i] == row[m] && column[i] == column[m])
                            {
                                flag = false;
                                break;
                            }
                        }
                    }
                } while (flag == false);

                Console.ForegroundColor = storagecolors[rnd.Next(0, storagecolors.Length - 1)];

                Console.SetCursorPosition(column[i], row[i]);
                Console.Write("▒");

                i++;
            } while (i != 150);

            Console.ForegroundColor = colors[2];

            Console.SetCursorPosition(24, 18);
            Console.Write("Press any key to back main menu...");
            Console.ReadKey();

        }//to show total move ,level and stage to user an effective screen when game over
        static void read_scoreboard()
        {
            //if there is a text,to read it...
            if (File.Exists("scoreboard.txt"))
            {
                StreamReader scorereader = File.OpenText("scoreboard.txt");

                for (int i = 0; i < scoreboard.GetLength(1); i++)
                {
                    scoreboard[0, i] = scorereader.ReadLine();
                    scoreboard[1, i] = scorereader.ReadLine();
                }

                scorereader.Close();
            }
            else//or create a new one
            {
                //bizim skor listemiz
                string[,] storage_scoreboard = { { "Abdullah", "Dilara", "İpek", "Evren", "Muhittin" }, { "3619", "4129", "4213", "4385", "4779" } };
                scoreboard = storage_scoreboard;
            }
        }//first of all read scoreboard from an text or create a new one...
        static void record_scoreboard(int totalmove)
        {
            //compare player's score with scoreboard...
            for (int i = 0; i < scoreboard.GetLength(1); i++)
            {
                //when player ger an place in the scoreboard take his name an create a folder to storage player score...
                if (Convert.ToInt16(scoreboard[1, i]) > totalmove)
                {
                    for (int j = scoreboard.GetLength(1) - 1; j > i; j--)
                    {
                        scoreboard[0, j] = scoreboard[0, j - 1];
                        scoreboard[1, j] = scoreboard[1, j - 1];
                    }

                    scoreboard[1, i] = Convert.ToString(totalmove);

                    string name = null;

                    do
                    {
                        Console.Clear();
                        Console.SetCursorPosition(30, 15);
                        Console.Write("Your score :" + totalmove);
                        Console.SetCursorPosition(30, 17);
                        Console.Write("Enter name :");
                        name = Console.ReadLine();
                    } while (name == null || name.Length > 12 || name.Length < 2);

                    scoreboard[0, i] = name;

                    StreamWriter record = File.CreateText("scoreboard.txt");

                    for (int j = 0; j < scoreboard.GetLength(1); j++)
                    {
                        record.WriteLine(scoreboard[0, j]);
                        record.WriteLine(scoreboard[1, j]);
                    }

                    record.Close();

                    skorboard();

                    break;
                }
            }
            //Eğer skorborda yerleşemezse
            Console.Clear();
            Console.SetCursorPosition(30, 15);
            Console.Write("Your score :" + totalmove);

        }//record score when player succesfully pass the level...

    }
}
