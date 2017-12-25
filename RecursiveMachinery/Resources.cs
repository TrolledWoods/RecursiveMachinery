using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RecursiveMachinery
{
    public static class Resources
    {

        public static string[] ResourceTypes;
        public static int[] ResourcePacks;

        public static void LoadAllResources(string resourceFolder)
        {
            // Check if the director exists
            if (Directory.Exists(resourceFolder))
            {
                // Find all the files in the directoy
                string[] files = Directory.GetFiles(resourceFolder);
                // Check if there are any files
                if (files.Length > 0)
                {
                    // Allocate memory for the packs
                    string[][] itemPacks = new string[files.Length][];
                    int[] packIndices = new int[files.Length];

                    // Find all the pack indices
                    int index = 0;
                    for(int i = 0; i < files.Length; i++)
                    {
                        itemPacks[i] = ReadResourceFile(files[i]);
                        index += itemPacks[i].Length;
                        packIndices[i] = index;
                    }

                    // Allocate memory for the resourcetyps array
                    ResourceTypes = new string[index];
                    index = 0;
                    // Populate the resource types
                    for (int i = 0; i < files.Length; i++)
                    {
                        for(int j = 0; j < itemPacks[i].Length; j++)
                        {
                            ResourceTypes[index] = itemPacks[i][j];
                            index++;
                        }
                    }

                    ResourcePacks = packIndices;

                    return;
                }
            }

            // If there are no resource files, call out an error message and then quit

            // Create an error message
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("FATAL ERROR: There are no resource files in " + resourceFolder);
            Console.ReadKey();
            throw new Exception("No resource files");
        }

        public static string[] ReadResourceFile(string filePath)
        {
            string[] contents = File.ReadAllLines(filePath);

            List<string> items = new List<string>();

            for (int i = 0; i < contents.Length; i++)
            {
                if (contents[i].Length > 0 && contents[i][0] != '#') // Quickly check if it's a valid line
                {
                    int j = 0;

                    // Check the name and the properties of the item
                    string name = (StringInterpreter.GetName(contents[i], ref j));

                    items.Add(name);
                }
            }

            return items.ToArray();

        }
    }
}