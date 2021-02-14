using System;
using System.IO;

namespace Challengeloader
{
    class Program
    {
        static string[] difficultyFolders = Directory.GetDirectories(Path.Combine(Directory.GetCurrentDirectory(), "ChallengeloaderMaps", "Modded"));
        static string vanillaBackup = Path.Combine(Directory.GetCurrentDirectory(), "ChallengeloaderMaps", "Original");
        static string vanillaFolder = Path.Combine(Directory.GetCurrentDirectory(), "Content", "Levels", "Challenges", "Test");

        static string[,] challengeNames = {
            {"chs_end_of_world","End of World"},
            {"chs_fields","Vietnam Survival"},
            {"chs_glade","Glade"},
            {"chs_havindr_arena","Havindr Arena"},
            {"chs_mines","Cavern"},
            {"chs_mirror_crystal_cavern","Mirror Crystal Cavern"},
            {"chs_necro","Elvenhus Arena"},
            {"chs_outsmouth","Outsmouth"},
            {"chs_shrine","Shrine"},
            {"chs_swamp","Swamp"},
            {"chs_volcano_hideout","Volcano Hideout"},
            {"chs_vulcanus","Vlucanus Arena"},
            {"chs_woot_bout_of_madness","Bout of Madness"},
            {"chs_woot_elemental_roulette","Elemental Roulette"},
        };
        public static void Main()
        {
            Console.Title = "Magicka Challengeloader";

            Console.WriteLine(
                "\n Press the corresponding number to view maps in each category." +
                "\n Type \"exit\" if you want to exit the program." +
                "\n Type \"restore\" to restore all original challenge files. \n"
            );

            for (int i = 0; i < difficultyFolders.Length; i++)
            {
                var folder = new DirectoryInfo(difficultyFolders[i]).Name;
                //Console.WriteLine("   " + (i + 1) + ") " + folder);
                Console.WriteLine("   {0}) {1}", i+1 , folder);
            }

            var diffSelection = Console.ReadLine();

            if (diffSelection == "restore") RestoreFiles();

            int checkedInput = CheckInput(diffSelection, difficultyFolders.Length);
            DisplayFiles(checkedInput);
        }

        static void DisplayFiles(int dirChoice)
        {
            Console.Clear();
            Console.WriteLine(
                "\n Press the corresponding number to load the challenge map" +
                "\n Type \"exit\" if you want to exit the program."
            );

            var availableMaps = Directory.GetFiles(difficultyFolders[dirChoice]);
            for (int i = 0; i < availableMaps.Length; i++)
            {
                var map = Path.GetFileNameWithoutExtension(availableMaps[i]);
                for(int j = 0; j < challengeNames.Length; j++)
                {
                    if(map == challengeNames[j,0])
                    {
                        //Console.WriteLine("   " + (i + 1) + ") " + challengeNames[j,1]);
                        Console.WriteLine("   {0}) {1}", i+1, challengeNames[j,1]);
                        break;
                    }
                }
            }

            Console.WriteLine();
            string mapChoice = Console.ReadLine();

            int checkedInput = CheckInput(mapChoice, availableMaps.Length);
            MoveFile(availableMaps[checkedInput], dirChoice);
        }

        static void MoveFile(string mapChoice, int dirChoice)
        {
            Console.Clear();

            File.Copy(mapChoice, Path.Combine(vanillaFolder, Path.GetFileName(mapChoice)), true);

            Console.WriteLine(
                "\nDifficulty: {0}" +
                "\nMap:        {1}" +
                "\n\nYou may now start the challenge in-game.",
                new DirectoryInfo(difficultyFolders[dirChoice]).Name,
                Path.GetFileNameWithoutExtension(mapChoice)
            );
            Console.WriteLine("\n\nPress any key to restore the original map and return to the main menu.");
            Console.ReadKey();
            Console.Clear();

            File.Copy(
                Path.Combine(vanillaBackup, Path.GetFileName(mapChoice)),
                Path.Combine(vanillaFolder, Path.GetFileName(mapChoice)),
                true
            );

            Main();

            /*Console.WriteLine("mapChoice:   " + mapChoice);
            Console.WriteLine("vanillaFolder:   " + vanillaFolder);
            Console.WriteLine("path extract:   " + Path.GetFileName(mapChoice));
            Console.WriteLine("path combine:   " + Path.Combine(vanillaFolder,Path.GetFileName(mapChoice)));
            Console.ReadKey();*/
        }

        static void RestoreFiles()
        {
            Console.Clear();
            string[] vanillaFiles = Directory.GetFiles(vanillaBackup);

            foreach(string mapFile in vanillaFiles)
            {
                File.Copy(
                    mapFile,
                    Path.Combine(vanillaFolder, Path.GetFileName(mapFile)),
                    true
                );
            }

            Console.WriteLine("\n\nAll original challenge files have been restored. Start/Restart the game now.");
            Console.ReadKey();
            Console.Clear();
            Main();
        }

        static int CheckInput(string input, int maxVal)
        {
            bool inputOK = false;
            int inputConv = 0;

            if (input == "exit")
            {
                Environment.Exit(0);
            }

            while (!inputOK)
            {
                if (input.Length < 4) { // required to prevent program from stopping to run if user input too big (eg 998273482374982374)
                    if (int.TryParse(input, out inputConv))
                    {
                        if (inputConv < 1 || inputConv > maxVal)
                        {
                            Console.WriteLine("Invalid input. Write a number from 1 to {0}", maxVal);
                            input = Console.ReadLine();
                        }
                        else
                        {
                            inputOK = true;
                        }
                    }
                } else
                {
                    Console.WriteLine("Invalid input. Write a number from 1 to {0}", maxVal);
                    input = Console.ReadLine();
                }
            }
            return inputConv-1;
        }
    }
}