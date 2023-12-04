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

        Dictionary<int, List<ScratchCard>> AllCards = new();

        Dictionary<int, List<ScratchCard>> OnlyWinningCards = new();

        public class ScratchCard()
        {

            public int CardNumber { get; set; } = 0;

            public int[] Numbers { get; set; } = [];

            public int[] WinningNumbers { get; set; } = [];

            public int Part1Score { get; set; } = 0;

            public int NumberOfMatches { get; set; } = 0;

            public bool HasBeenEvaluated { get; set; } = false;

        }

        //Card   1:  4 16 87 61 11 37 43 25 49 17 | 54 36 14 55 83 58 43 15 87 17 97 11 62 75 37  4 49 80 42 61 20 79 25 24 16

        public override void Part1()
        {
            Functions.PrintResult(CalculateWinningsPart1(AllCards).ToString(), 4, 1);
            //15268 was correct
        }


        private void ReadInAllCards()
        {

            foreach (string line in AllLines)
            {
                ScratchCard card = new();
                var cardNumber = Regex.Match(line, @"Card\s+(\d+):");
                card.CardNumber = int.Parse(cardNumber.Groups[1].Value);

                Match numbers = Regex.Match(line, @"Card\s+\d+:\s+(.*)\s+\|\s+(.*)");
                string numbers1 = Regex.Replace(numbers.Groups[1].Value, "  ", " ");
                string numbers2 = Regex.Replace(numbers.Groups[2].Value, "  ", " ");

                int[] numbers1Array = numbers1.Split(" ").Select(x => int.Parse(x)).ToArray();
                int[] numbers2Array = numbers2.Split(" ").Select(x => int.Parse(x)).ToArray();

                card.Numbers = numbers1Array;
                card.WinningNumbers = numbers2Array;
                AllCards.Add(card.CardNumber, [card]);
            }
        }

        private int CalculateWinningsPart1(Dictionary<int, List<ScratchCard>> InputCheck)
        {
            int TotalWinningsScore = 0;

            foreach (var card in InputCheck)
            {
                foreach (var number in card.Value.First().Numbers)
                {
                    if (card.Value.First().WinningNumbers.Contains(number) == false)
                    {
                        continue;
                    }

                    card.Value.First().NumberOfMatches++;

                    if (card.Value.First().Part1Score == 0)
                    {
                        card.Value.First().Part1Score = 1;
                    }
                    else
                    {
                        card.Value.First().Part1Score *= 2;
                    }
                }

                TotalWinningsScore += card.Value.First().Part1Score;
                Functions.DebugPrint("Card " + card.Key + " has " + card.Value.First().NumberOfMatches + " winning numbers");
            }
            return TotalWinningsScore;
        }



        public override void Part2()
        {
            Functions.DebugPrint("Part 2");
            CopyOverAllWinningCards();
            CreateAllCopysOfCards();

            int TotalCards = 0;
            foreach (KeyValuePair<int, List<ScratchCard>> card in OnlyWinningCards)
            {
                TotalCards += card.Value.Count();
                Functions.DebugPrint("Card " + card.Key + " has " + card.Value.Count() + " winning cards");
            }

            Functions.DebugPrint("Total Cards: " + TotalCards);
            //901 too low
            //1499 too low
            //3017 too low

        }

        private void CopyOverAllWinningCards()
        {
            OnlyWinningCards.Clear();
            foreach (var card in AllCards)
            {
                if (card.Value.First().NumberOfMatches > 0)
                {
                    OnlyWinningCards.Add(card.Key, [card.Value.First()]);
                }
            }
            Functions.DebugPrint("Total Winning Cards: " + OnlyWinningCards.Count);
        }

        private void CreateAllCopysOfCards()
        {
            int TotalDuplicatesCreated = 0;
            int CurrentNumberOfCards = OnlyWinningCards.Count();
            Functions.DebugPrint("Current Number of Cards to loop over: " + CurrentNumberOfCards);

            //we cant use Foreach as we are adding to the dictionary we are looping through 
            //so instead we use a for-i and loop through the number of cards we have and simaltaniously add to the dictionary

            for (int i = 0; i < CurrentNumberOfCards; i++)
            {
                List<ScratchCard> CurrentCard = OnlyWinningCards[i];
                int NumberOfScratches = CurrentCard.Count();
                Functions.DebugPrint("Current Card: " + CurrentCard.First().CardNumber + " has " + NumberOfScratches + " scratches");

                // Each Ticket inside the current card
                for (int X = 0; X < NumberOfScratches; X++)
                {
                    if (CurrentCard.ElementAt(X).HasBeenEvaluated == true)
                    {
                        //next for X
                        continue;
                    }

                    CurrentCard.ElementAt(X).HasBeenEvaluated = true;

                    for (int NewLotts = 1; NewLotts < CurrentCard.ElementAt(X).NumberOfMatches + 1; NewLotts++)
                    {
                        if (OnlyWinningCards.ContainsKey(i + NewLotts) == false)
                        {
                            // Functions.DebugPrint("Reached an end of what we can copy over, at " + i + " and " + NewLotts + " so we are done with this card");
                            continue;
                        }

                        ScratchCard NewCard = OnlyWinningCards[i + NewLotts].First();
                        NewCard.HasBeenEvaluated = false;
                        OnlyWinningCards[NewCard.CardNumber].Add(NewCard);
                        TotalDuplicatesCreated++;
                    }
                }

            }

            // if (TotalDuplicatesCreated > 0)
            // {
            //     CreateAllCopysOfCards();

            // }


        }


    }



}
