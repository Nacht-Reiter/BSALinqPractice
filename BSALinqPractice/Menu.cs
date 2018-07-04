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
            "User`s completed todoes (3 required query)",
            "Sorted Users with sorted todoes (4 required query)",
            "User`s info(5 required query)",
            "Post info(6 required query)",
            "Exit"
        };

        private static readonly List<MethodHandler> MenuMethods = new List<MethodHandler>
        {
            PrintCommentsAmount,                      //Methods, which works when menu item choosen
            PrintShortComents,
            PrintCompletedTodoes,
            PrintSortedUsers,
            PrintUsersInfo,
            PrintPostInfo,
            Exit
        };

        public static void MainMenu()
        {
            try
            {
                MenuMethods[PrintMenu(MenuItems, "Main menu")]();
            }
            catch (ArgumentException e)
            {
                PrintError(e.Message);
            }
            catch (FormatException e)
            {
                PrintError("Wrong input format, try again");
            }
            catch (Exception e)
            {
                PrintError("Unknown error");
            }
        }

        private static void PrintCommentsAmount()
        {
            Console.Clear();
            Console.WriteLine("Enter user`s id: ");
            int id = int.Parse(Console.ReadLine());
            Console.Clear();
            var items = Program.CountCommentsUnderPosts(id);
            foreach(var item in items)
            {
                Console.WriteLine(item.Item1.ToString());
                Console.WriteLine($"\tComments Amount: {item.Item2}\n\n");
            }
            Console.ReadKey();
            MainMenu();
        }

        private static void PrintShortComents()
        {
            Console.Clear();
            Console.WriteLine("Enter user`s id: ");
            int id = int.Parse(Console.ReadLine());
            Console.Clear();
            var items = Program.GetShortCommentsUnderPosts(id);
            foreach(var i in items)
            {
                Console.WriteLine(i.ToString());
            }
            Console.ReadKey();
            MainMenu();
        }

        private static void PrintCompletedTodoes()
        {
            Console.Clear();
            Console.WriteLine("Enter user`s id: ");
            int id = int.Parse(Console.ReadLine());
            Console.Clear();
            var items = Program.GetCompleteTodoes(id);
            foreach (var i in items)
            {
                Console.WriteLine($"Id: {i.Item1}  Name:{i.Item2}");
            }
            Console.ReadKey();
            MainMenu();
        }

        private static void PrintSortedUsers()
        {
            Console.Clear();
            Console.WriteLine();
            foreach(var i in Program.GetSortedUsers())
            {
                Console.WriteLine(i.ToString());
            }
            Console.ReadKey();
            MainMenu();
        }

        private static void PrintUsersInfo()
        {
            Console.Clear();
            Console.WriteLine("Enter user`s id: ");
            int id = int.Parse(Console.ReadLine());
            Console.Clear();
            var i = Program.GetUserInfo(id);
            Console.WriteLine($"\n{i.Item1?.ToString()}\n");
            Console.WriteLine($"Last Post: \n{i.Item2?.ToString()}\n");
            Console.WriteLine($"Last post`s comments amount: {i.Item3}\n");
            Console.WriteLine($"Uncomplete todoes amount: {i.Item4}\n");
            Console.WriteLine($"Popularest post(Comments): \n{i.Item5?.ToString()}\n");
            Console.WriteLine($"Popularest post(Likes): \n{i.Item6?.ToString()}\n");
            Console.ReadKey();
            MainMenu();
        }

        private static void PrintPostInfo()
        {
            Console.Clear();
            Console.WriteLine("Enter post id: ");
            int id = int.Parse(Console.ReadLine());
            Console.Clear();
            var i = Program.GetPostInfo(id);
            Console.WriteLine($"\n{i.Item1?.ToString()}\n");
            Console.WriteLine($"\tLongest comment: \n{i.Item2?.ToString()}\n");
            Console.WriteLine($"\tLiked comment: {i.Item3?.ToString()}\n");
            Console.WriteLine($"\tTrash comment`s amount: {i.Item4}\n");

            Console.ReadKey();
            MainMenu();
        }

        private static void Exit()
        {

        }

        private static void PrintError(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            Console.ReadKey();
            MainMenu();
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