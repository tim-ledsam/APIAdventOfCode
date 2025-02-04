﻿using AdventOfCode.Puzzles._2021.Challenges;
using AdventOfCode.Puzzles._2022.Challenges;
using APIAdventOfCode.Models;

namespace APIAdventOfCode.Puzzles
{
    public interface IAdventofCodeService
    {
        AOCResponse GetAOCAnswer(int year, int day);
    }

    public class AdventOfCodeService : IAdventofCodeService
    {
        public AOCResponse GetAOCAnswer(int year, int day)
        {
            return new AOCResponse(FindAnswer(year, day));
        }

        private string FindAnswer(int year, int day)
        {
            switch (year)
            {
                case 2021:
                    switch (day)
                    {
                        case 1:
                            return ChallengeOne2021.GetIncreases();
                        case 2:
                            return ChallengeTwo2021.GetDepthXHorizontal();
                        default:
                            throw new Exception("Invalid day");

                    }
                case 2022:
                    switch (day)
                    {
                        case 1:
                            return ChallengeOne2022.GetCalories();
                        case 2:
                            return ChallengeTwo2022.GetScore();
                        case 3:
                            return ChallengeThree2022.GetTotalPriority();
                        case 4:
                            return ChallengeFour2022.GetMatchingIDs();
                        case 5:
                            return ChallengeFive2022.GetCraneMovements();
                        case 6:
                            return ChallengeSix2022.GetPacket();
                        case 7:
                            return ChallengeSeven2022.GetDirectorySizes();
                        default:
                            throw new Exception("Invalid day");
                    }
                default:
                    throw new Exception("Invalid year");
            }

        }
    }
}
