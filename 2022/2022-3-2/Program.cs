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

        private static int Calculate(List<Group> parsedInput) {
            foreach (var group in parsedInput) {
                foreach (var item in group.Sack1) {
                    if (group.Sack2.Contains(item) && group.Sack3.Contains(item)) {
                        group.Match = item;

                        group.Score = GetScoreFromMatch(group.Match);

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

        private static List<Group> ParseInput(string input) {
            var parsedInput = new List<Group>();
            var count = 1;
            var newVal = new Group();

            foreach (var i in input.Split("\r\n")) {
                if (count == 1) {
                    newVal = new Group();
                    newVal.Sack1 = i;
                    count++;
                } else if (count == 2) {
                    newVal.Sack2 = i;
                    count++;
                } else {
                    newVal.Sack3 = i;
                    count = 1;
                    parsedInput.Add(newVal);
                }
            }

            return parsedInput;
        }
    }

    class Group {
        public string Sack1 { get; set; } = string.Empty;
        public string Sack2  { get; set; } = string.Empty;
        public string Sack3  { get; set; } = string.Empty;
        public char Match { get; set;}
        public int Score { get; set; } = 0;
    }
}