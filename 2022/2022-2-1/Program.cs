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

        private static int Calculate(List<Game> parsedInput) {
            foreach (var round in parsedInput) {
                if (round.Me == round.Elf) // draw
                {
                    round.Score += 3;
                } else if (round.Me == Rps.Rock && round.Elf == Rps.Scissors ||
                           round.Me == Rps.Paper && round.Elf == Rps.Rock ||
                           round.Me == Rps.Scissors && round.Elf == Rps.Paper)
                {
                    round.Score += 6;
                } else {
                    round.Score += 0;
                }

                if (round.Me == Rps.Rock) {
                    round.Score += 1;
                } else if (round.Me == Rps.Paper) {
                    round.Score += 2;
                } else if (round.Me == Rps.Scissors) {
                    round.Score += 3;
                };

            Console.WriteLine(round.Score);
            }
            
            var result = parsedInput.Select(o => o.Score).Sum();

            return result;
        }

        private static string ReadInput() {
            return System.IO.File.ReadAllText(@"./input.txt");
        }

        private static List<Game> ParseInput(string input) {
            var parsedInput = new List<Game>();

            foreach (var i in input.Split("\r\n")) {
                var newVal = new Game();

                var first = i.Split(" ")[0];
                var second = i.Split(" ")[1];


                newVal.Elf = first == "A" 
                    ? Rps.Rock
                    : first == "B"
                        ? Rps.Paper
                        : Rps.Scissors;

                newVal.Me = second == "X" 
                    ? Rps.Rock
                    : second == "Y"
                        ? Rps.Paper
                        : Rps.Scissors;

                parsedInput.Add(newVal);
            }

            return parsedInput;
        }
    }

    class Game {
        public Rps Me { get; set; }
        public Rps Elf { get; set;}
        public int Score { get; set;} = 0;
    }

    enum Rps {
        Rock,
        Paper,
        Scissors
    }
}