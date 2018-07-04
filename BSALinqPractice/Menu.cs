using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BSALinqPractice
{
    public class Menu
    {
        private delegate void MethodHandler();

        private static readonly List<String> MenuItems = new List<String>  //Main Menu atributes
        {                                                           //Shown into console to choose
            "Amount of coments under user`s posts (1 required query)",
            "Short coments under all user`s posts (2 required query)",
            "User`s uncompleted todoes (3 required query)",
            "Sorted Users with sorted todoes (4 required query)",
            "User`s info(5 required query)",
            "Post info(6 required query)",
            "Exit"
        };

        private static readonly List<MethodHandler> MenuMethods = new List<MethodHandler>
        {
            PrintCommentsAmount,                                  //Method, which works when menu item choosen
            Exit,
            Exit,
            Exit,
            Exit,
            Exit,
            Exit
        };

        public static void MainMenu()
        {
            MenuMethods[PrintMenu(MenuItems, "Main menu")]();
        }

        private static void PrintCommentsAmount()
        {
            Console.Clear();
            Console.WriteLine("Enter user`s id: ");
            int id = int.Parse(Console.ReadLine());
            var items = Program.CountCommentsUnderPosts(id);
            foreach(var item in items)
            {
                Console.WriteLine($"/tPost title: {item.Item1.Title}, Coments: {item.Item2}");
            }
            Console.ReadKey();
            MainMenu();
        }

        private static void Exit() // Exit app
        {

        }
        private static int PrintMenu(List<String> menuItems, String topInfo) //Method, that allows to select menu strings
                                                                             //First argument - list of menu items to select
        {                                                                    //Second argument - title
            int counter = 0;                                                 //returns index of decision
            ConsoleKeyInfo key;
            do
            {
                Console.Clear();
                Console.WriteLine(topInfo + "\n\n");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 0; i < menuItems.Count; i++)
                {
                    if (counter == i)
                    {
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(menuItems[i]);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                        Console.WriteLine(menuItems[i]);

                }
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow)
                {
                    counter--;
                    if (counter == -1)
                    {
                        counter = menuItems.Count - 1;
                    }
                }
                if (key.Key == ConsoleKey.DownArrow)
                {
                    counter++;
                    if (counter == menuItems.Count)
                    {
                        counter = 0;
                    }
                }
            }
            while (key.Key != ConsoleKey.Enter);
            return counter;
        }
    }
}