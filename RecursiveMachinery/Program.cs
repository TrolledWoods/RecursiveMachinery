using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursiveMachinery
{
    class Program
    {
        static void Main(string[] args)
        {
            Reload();
            Console.ReadKey();
        }

        public static void Reload()
        {
            Console.Clear();
            Console.WriteLine("Loading game...");
            Console.WriteLine("Loading resources");
            // Load all the resources
            Resources.LoadAllResources(System.AppDomain.CurrentDomain.BaseDirectory + "/Data/ResourcePacks/");

            // Display all the resources
            int packIndex = 0;
            for (int i = 0; i < Resources.ResourceTypes.Length; i++)
            {
                if(Resources.ResourcePacks[packIndex] == i)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine();
                    packIndex++;
                    packIndex = packIndex >= Resources.ResourcePacks.Length ? Resources.ResourcePacks.Length - 1 : packIndex;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(Resources.ResourceTypes[i]);
            }
        }
    }
}
