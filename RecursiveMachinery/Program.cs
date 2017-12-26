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
            // Load the resources
            Load();

            // Start the game
            Inventory i = new Inventory();

            Console.ReadKey();
        }

        public static void Load()
        {
            Console.Clear();
            Program.LogLoading("game");
            // Load all the resources
            Resources.LoadAllResources(System.AppDomain.CurrentDomain.BaseDirectory + "/Data/ResourcePacks/");
            Program.LogFinish();

            // Press any key to continue message
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("Press any key to continue..");
            Console.ReadKey();
        }

        public static string StandardIndent = " | ";
        static Stack<string> LoadingStack = null;

        static string GetIndent(int indent)
        {
            string indentString = "";
            for (int i = 0; i < indent; i++) indentString += StandardIndent;
            return indentString;
        }

        public static void LogFinish()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(GetIndent(LoadingStack.Count - 1));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Finished loading " + LoadingStack.Pop());
        }

        public static void LogStatus(string statusMessage)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(GetIndent(LoadingStack.Count - 1));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(statusMessage);
        }

        public static void LogLoading(string whatYoureLoading)
        {
            if (LoadingStack == null)
                LoadingStack = new Stack<string>();

            LoadingStack.Push(whatYoureLoading);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(GetIndent(LoadingStack.Count - 1));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Loading " + whatYoureLoading);
        }
    }
}
