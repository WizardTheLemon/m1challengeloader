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
            Console.WriteLine("\n Press the corresponding number to view maps in each category");

            for (int i = 0; i < difficultyFolders.Length; i++)
            {
                var folder = new DirectoryInfo(difficultyFolders[i]).Name;
                Console.WriteLine("   " + (i + 1) + ") " + folder);
            }
            
            var diffSelection = Console.ReadLine();
            int checkedInput = CheckInput(diffSelection, difficultyFolders.Length);
            DisplayFiles(checkedInput);
            //Console.WriteLine("Checked Input: " + checkedInput);
            //Console.ReadKey();
        }

        static void DisplayFiles(int dirChoice)
        {
            Console.Clear();
            Console.WriteLine("\n Press the corresponding number to load the challenge map");

            var availableMaps = Directory.GetFiles(difficultyFolders[dirChoice]);
            for (int i = 0; i < availableMaps.Length; i++)
            {
                var map = Path.GetFileNameWithoutExtension(availableMaps[i]);
                for(int j = 0; j < challengeNames.Length; j++)
                {
                    if(map == challengeNames[j,0])
                    {
                        Console.WriteLine("   " + (i + 1) + ") " + challengeNames[j,1]);
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
            Console.WriteLine("\n\nPress any key to restore the original map and return to the main menu");
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

        static int CheckInput(string input, int maxVal)
        {
            bool inputOK = false;
            int inputConv = 0;
            while (!inputOK)
            {
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
            }
            return inputConv-1;
        }
    }
}