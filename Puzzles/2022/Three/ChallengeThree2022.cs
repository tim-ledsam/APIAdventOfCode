using AdventOfCode.Common;
using APIAdventOfCode.Puzzles._2022.Three;

namespace AdventOfCode._2022.Three;

public class ChallengeThree2022
{
    public static string GetTotalPriority()
    {
        //IEnumerable<Rucksack> rucksacks = ReadCheatSheet();
        //return rucksacks.Select(x => x.GetRucksackPriority()).Sum().ToString();

        IEnumerable<ThreeElves> threeElves = ReadElfGroups();
        return threeElves.Select(x => x.GetBadgePriority())
            .Sum().ToString();
    }
    
    private static IEnumerable<Rucksack> ReadCheatSheet()
    {
        string inputFilePath = "2022\\Three\\ThreeInput.txt";
        return FileService.ReadStringInput(inputFilePath)
            .Select(x => new Rucksack(x));
    }

    private static IEnumerable<ThreeElves> ReadElfGroups()
    {
        string inputFilePath = "2022\\Three\\ThreeInput.txt";
        return FileService.ReadStringInput(inputFilePath)
            .ToFormattedList(3, args => new ThreeElves(args[0], args[1], args[2]));

    }
}