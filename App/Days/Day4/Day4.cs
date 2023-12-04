using System.ComponentModel;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.App.Days
{
    public class Day4 : EveryDay, DayTest
    {

        public Day4()
        {
            ReadInProblem();
            ReadInAllCards();
        }

        Dictionary<int, ScratchCard> AllCards = new();

        public class ScratchCard()
        {

            public int CardNumber { get; set; } = 0;

            public int[] Numbers { get; set; } = [];

            public int[] WinningNumbers { get; set; } = [];

            public int Part1Score { get; set; } = 0;

        }

        //Card   1:  4 16 87 61 11 37 43 25 49 17 | 54 36 14 55 83 58 43 15 87 17 97 11 62 75 37  4 49 80 42 61 20 79 25 24 16

        public override void Part1()
        {
            Functions.PrintResult(CalculateWinnings(AllCards).ToString(), 4, 1);
            //15268 was correct
        }


        private void ReadInAllCards()
        {

            foreach (string line in AllLines)
            {
                ScratchCard card = new();
                var cardNumber = Regex.Match(line, @"Card\s+(\d+):");
                Functions.DebugPrint("Found Card: " + cardNumber.Groups[1].Value);
                card.CardNumber = int.Parse(cardNumber.Groups[1].Value);

                Match numbers = Regex.Match(line, @"Card\s+\d+:\s+(.*)\s+\|\s+(.*)");
                string numbers1 = Regex.Replace(numbers.Groups[1].Value, "  ", " ");
                string numbers2 = Regex.Replace(numbers.Groups[2].Value, "  ", " ");

                int[] numbers1Array = numbers1.Split(" ").Select(x => int.Parse(x)).ToArray();
                int[] numbers2Array = numbers2.Split(" ").Select(x => int.Parse(x)).ToArray();

                card.Numbers = numbers1Array;
                card.WinningNumbers = numbers2Array;
                AllCards.Add(card.CardNumber, card);
            }
        }

        private int CalculateWinnings(Dictionary<int, ScratchCard> InputCheck)
        {
            int TotalWinningsScore = 0;

            foreach (var card in InputCheck)
            {
                int winnings = 0;
                foreach (var number in card.Value.Numbers)
                {
                    if (card.Value.WinningNumbers.Contains(number))
                    {
                        if (card.Value.Part1Score == 0)
                        {
                            card.Value.Part1Score = 1;
                        }
                        else
                        {
                            card.Value.Part1Score *= 2;
                        }
                    }
                }
                TotalWinningsScore += card.Value.Part1Score;
                Functions.DebugPrint("Card " + card.Key + " has " + winnings + " winning numbers");
            }
            return TotalWinningsScore;


        }


        public override void Part2()
        {
        }

    }

}
