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
        public static Scope[] baseResourcePacks;

        public static void LoadAllResources(string resourceFolder)
        {
            if (Directory.Exists(resourceFolder))
            {
                Program.LogLoading("resources");
                string[] files = Directory.GetFiles(resourceFolder);
                for(int i = 0; i < files.Length; i++)
                {
                    if (StringInterpreter.GetFileExtension(files[i]) == "pack")
                    {
                        LoadScope(resourceFolder, StringInterpreter.GetFileName(files[i]), new int[] { i });
                    }
                }
                Program.LogFinish();

                return;
            }

            // If there are no resource files, call out an error message and then quit
            // Create an error message
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("FATAL ERROR: There are no resource files in " + resourceFolder);
            Console.ReadKey();
            throw new Exception("No resource files");
        }

        static Scope LoadScope(string path, string name, int[] id)
        {
            if (File.Exists(path + name + ".pack"))
            {
                Program.LogLoading("pack named " + name);

                // Find the filename of the resourcedirectory of the pack
                string resourceDirPath = path + name + '/';

                // If that directory doesn't exist, create it
                if (!Directory.Exists(resourceDirPath))
                    Directory.CreateDirectory(resourceDirPath);

                // Read the file into a string array
                string[] contents = File.ReadAllLines(path + name + ".pack");

                // First, find all the lines that start with a '-', because those lines
                // are trying to change the mode
                List<int> LinesOfImportance = new List<int>();
                for(int i = 0; i < contents.Length; i++)
                {
                    if (contents[i].Length > 0 && contents[i][0] == '-')
                        LinesOfImportance.Add(i);
                }

                // Initialize all the variables remembering stuff
                List<string> Items = new List<string>();

                // Go to all the lines of importance that change the mode to
                // item mode, and then add all of those items to the list of items in the
                // pack
                Program.LogLoading("items from pack " + name);
                for(int i = 0; i < LinesOfImportance.Count; i++)
                {
                    // Check if it is the ItemMode
                    if(contents[LinesOfImportance[i]] == "-ItemMode")
                    {
                        Program.LogLoading("itemMode section on line " + LinesOfImportance[i]);
                        // From here, read all the contents and add them as items
                        LoadItems(Items, contents, LinesOfImportance[i], i < LinesOfImportance.Count-1 ? LinesOfImportance[i + 1] : contents.Length);
                        Program.LogFinish();
                    }
                }
                Program.LogFinish(); // Finished the items


                // Go though the lines of importance again and find the recipe things
                Program.LogLoading("recipes from pack " + name);
                for (int i = 0; i < LinesOfImportance.Count; i++)
                {
                    // Check if it is the ItemMode
                    if (contents[LinesOfImportance[i]] == "-RecipeMode")
                    {
                        Program.LogLoading("recipeMode section on line " + LinesOfImportance[i]);
                        Program.LogFinish();
                    }
                }
                Program.LogFinish(); // Finish the recipes

                // Initialize the scope
                Scope packScope = new Scope(name, id, Items.ToArray());

                // Find all the subScopes
                LoadSubScopes(packScope, path + name + '/');
                
                Program.LogFinish(); // Finish the pack

                // Return a new scope with a name, an id, and all the other values as well
                return packScope;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR: A resourcefile that doesn't exist was trying to load");
            Console.ReadKey();
            throw new Exception("File doesn't exist");
        }

        static void LoadSubScopes(Scope parentScope, string path)
        {
            // See if the path exists
            if (Directory.Exists(path))
            {
                // Load all the files in the directory
                string[] files = Directory.GetFiles(path);

                // Load the id from the parentScope. If the parentscope is null, just create an empty int array instead
                int[] id = parentScope == null ? new int[0] : parentScope.scopeID;

                // Make a list of all the subscopes
                List<Scope> subScopes = new List<Scope>();

                Program.LogLoading("subpacks in pack " + parentScope.Name);
                // Search through all the files to find all the pack files
                for (int i = 0; i < files.Length; i++)
                {
                    if (StringInterpreter.GetFileExtension(files[i]) == "pack")
                    {
                        subScopes.Add(LoadScope(path, StringInterpreter.GetFileName(files[i]), new int[] { i }));
                    }
                }
                Program.LogFinish();

                parentScope.SubScopes = subScopes.ToArray();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: A subscope folder that doesn't exist was trying to be loaded");
                Console.WriteLine("Subscope folder: " + path);
                Console.ReadKey();
                throw new Exception("Subscope folder does not exist");
            }
        }
        
        static void LoadItems(List<string> ItemList, string[] lines, int beginLine, int endLine)
        {
            Console.ForegroundColor = ConsoleColor.White;
            for(int i = beginLine; i < endLine; i++)
            {
                if(lines[i].Length > 0 && lines[i][0] != '#' && lines[i][0] != '-')
                {
                    // Find the name
                    int index = 0;
                    string itemName = StringInterpreter.GetName(lines[i], ref index);
                    Program.LogLoading("item " + itemName);
                    ItemList.Add(itemName);
                    Program.LogFinish();
                }
            }
        }
    }

    public class Scope
    {
        public string Name;
        public string[] Resources;
        public Scope[] SubScopes;
        public Scope ParentScope;

        public int[] scopeID;
        public int scopeSize; // How many items exist in the scope
        public int localScopeIndex; // Where in the local scope space the scope lies
                                    // The local scope space is basically all the items in the parentScopes children.
                                    // So, where the scope lies is really jus the sum of the scopeSizes of all the
                                    // previous scope children in the scope parent
        public int globalScopeIndex; // This value is the parentScopes globalScopeIndex + parentScopes scopeSize, and if there is no
                                     // parentscope, the value is simply scopeSize
        
        public Scope(string name, int[] scopeID, string[] resources)
        {
            this.Name = name;
            this.scopeID = scopeID;

            this.Resources = resources;
        }
    }
}