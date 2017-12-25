using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursiveMachinery
{
    public static class StringInterpreter
    {

        public static string GetName(string s, ref int index)
        {
            MoveToPointOfInterest(s, ref index);

            string name = "";
            // Add the name with - as the name termiator
            for (; index < s.Length; index++)
            {
                if (s[index] == '-') break;
                name += s[index];
            }

            // Remove any eccess spaces from the name
            int i = name.Length - 1;
            for (; i >= 0 && (name[i] == ' ' || name[i] == '-'); i--) { }
            name.Remove(i, name.Length - 1 - i);

            return name;
        }

        public static string GetFileName(string filePath)
        {
            // Backtrace until you hit a dot, then add all the characters until you hit a forwardslash
            string name = "";
            bool startWritingName = false;
            for(int i = filePath.Length-1; i >= 0 && filePath[i] != '/'; i--)
            {
                if(startWritingName)
                {
                    name = filePath[i] + name;
                }

                if(filePath[i] == '.')
                {
                    startWritingName = true;
                }
            }

            return name;
        }

        public static string GetFileExtension(string filePath)
        {
            string extension = "";
            for (int i = filePath.Length - 1; i >= 0 && filePath[i] != '.'; i--)
            {
                extension = filePath[i] + extension;
            }

            return extension;
        }

        /// <summary>
        /// Moves the index forward until it hits a letter that is not a space
        /// </summary>
        public static void MoveToPointOfInterest(string s, ref int index)
        {
            for(; index < s.Length && s[index] == ' '; index++) { }
        }

    }
}