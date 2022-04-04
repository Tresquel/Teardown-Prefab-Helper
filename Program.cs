namespace Teardown_Prefab_Helper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* This program was mostly created by Github Copilot
             * 
             * I wanted to test it out and made this
             * Sorry if the code is bad, I (and Copilot) made it in a few hours.
             * 
            */
            Console.Clear();
            // find the documents folder
            string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            // list all folders in teardown/mods
            string[] folders = Directory.GetDirectories(documentsFolder + "\\teardown\\mods");
            // ask the user to pick a folder
            Console.WriteLine("Pick a mod:");
            for (int i = 0; i < folders.Length; i++)
            {
                //print only the last part of the path
                Console.WriteLine(i + ": " + folders[i].Split('\\')[folders[i].Split('\\').Length - 1]);
            }
            // check if the user entered a valid number
            int modIndex = -1;
            while (modIndex < 0 || modIndex >= folders.Length)
            {
                Console.WriteLine("Enter a number:");
                string input = Console.ReadLine();
                if (int.TryParse(input, out modIndex))
                {
                    if (modIndex < 0 || modIndex >= folders.Length)
                    {
                        Console.WriteLine("Invalid number!");
                        Console.ReadKey(false);
                        Main(args);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid number!");
                    Console.ReadKey(false);
                    Main(args);
                }
            }
            bool done = false;
            while (done != true)
            {
                Console.Clear();
                // list all the .xml files in the folder and subfolder and then put them in an array
                string[] files = Directory.GetFiles(folders[modIndex], "*.xml", SearchOption.AllDirectories);
                // ask the user to pick the files
                Console.WriteLine("Pick the files:");
                for (int n = 0; n < files.Length; n++)
                {
                    //list only the path from the mods folder
                    Console.WriteLine(n + ": " + files[n].Replace(folders[modIndex], ""));
                }
                // get the user's input
                string[] fileIndexes = Console.ReadLine().Split(' ');
                // if null or empty, ask again
                while (fileIndexes.Length == 0)
                {
                    Console.WriteLine("Please enter a number");
                    Console.ReadKey();
                }
                // for each selection ask for the name and category
                for (int iii = 0; iii < fileIndexes.Length; iii++)
                {
                    //ask for a category and a name for each file
                    Console.WriteLine("Enter a category for " + files[int.Parse(fileIndexes[iii])].Replace(folders[modIndex], ""));
                    string category = Console.ReadLine();
                    Console.WriteLine("Enter a name for " + files[int.Parse(fileIndexes[iii])].Replace(folders[modIndex], ""));
                    string name = Console.ReadLine();
                    // create a spawn.txt file if doesnt exist at mod root
                    if (!File.Exists(folders[modIndex] + "\\spawn.txt"))
                    {
                        File.Create(folders[modIndex] + "\\spawn.txt").Close();
                    }
                    //put the path to xml : category/name in a variable
                    string WhatToWrite = files[int.Parse(fileIndexes[iii])].Replace(folders[modIndex], "") + " : " + category + "/" + name + "\n";
                    //replace every backslash with a forward slash and delete the first slash
                    WhatToWrite = WhatToWrite.Replace("\\", "/").Remove(0, 1);
                    //write the variable to the spawn.txt file
                    File.AppendAllText(folders[modIndex] + "\\spawn.txt", WhatToWrite);

                }
                // if done go back to main menu
                Console.WriteLine("Done? (y/n)");
                string input = Console.ReadLine();
                if (input == "y")
                {
                    Environment.Exit(0);
                }
                else
                {
                    Main(args);
                }
            }
        }
    }
}
