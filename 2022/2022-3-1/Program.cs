namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = ReadInput();

            var parsedInput = ParseInput(input);

            var result = Calculate(parsedInput);

            Console.WriteLine($"Result: {result}");
        }

        private static int Calculate(List<Rucksack> parsedInput) {
            foreach (var sack in parsedInput) {
                foreach (var item in sack.Comp1) {
                    if (sack.Comp2.Contains(item)) {
                        sack.Match = item;

                        sack.Score = GetScoreFromMatch(sack.Match);

                        continue;
                    }
                }
            }

            var result = parsedInput.Select(o => o.Score).Sum();

            return result;
        }

        private static int GetScoreFromMatch(char match) {
            var score = 0;
            
            if ((int)match <= 90) {
                score = (int)match - 38;
            } else {
                score = (int)match - 96;
            }

            return score;
        }

        private static string ReadInput() {
            return System.IO.File.ReadAllText(@"./input.txt");
        }

        private static List<Rucksack> ParseInput(string input) {
            var parsedInput = new List<Rucksack>();

            foreach (var i in input.Split("\r\n")) {
                var newVal = new Rucksack();

                newVal.Comp1 = i.Substring(0,i.Length/2);
                newVal.Comp2 =  i.Substring(i.Length/2);

                parsedInput.Add(newVal);
            }

            return parsedInput;
        }
    }

    class Rucksack {
        public string Comp1 { get; set; } = string.Empty;
        public string Comp2  { get; set; } = string.Empty;
        public char Match { get; set;}
        public int Score { get; set; } = 0;
    }
}