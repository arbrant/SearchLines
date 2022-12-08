using System;
using SearchText;
using System.Collections.Generic;

namespace Main
{
    class MainClass
    {
        public static void ShowList(List<string> input)
        {
            foreach (string str in input)
            {
                Console.WriteLine(str);
            }
        }

        public static void Main(string[] args)
        {
            var search = new SearchInFile("..\\..\\..\\txt\\input.txt");
            Console.Write("Search: ");
            List<string> searched = search.Search(Console.ReadLine());
            Console.WriteLine();
            if (searched.Count == 0)
            {
                Console.WriteLine("Is NOT found!");
            }
            else
            {
                ShowList(searched);
            }
            Console.WriteLine("\nThe end!");
            Console.ReadKey();
        }
    }
}