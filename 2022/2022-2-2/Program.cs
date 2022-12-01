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

        private static int Calculate(List<Pwordandrule> parsedInput) {
            var result = 0;

            foreach (var item in parsedInput) {
                var count = item.Pword
                    .Where((o,i) => o == item.Character && (i == (item.Min-1) || i == (item.Max-1)))
                    .Count();

                if (count == 1) {
                    result++;
                }
            }

            return result;
        }

        private static string ReadInput() {
            return System.IO.File.ReadAllText(@"./input.txt");
        }

        private static List<Pwordandrule> ParseInput(string input) {
            var parsedInput = new List<Pwordandrule>();

            foreach (var i in input.Split("\r\n")) {
                var newVal = new Pwordandrule();

                newVal.Min = int.Parse(i.Split(" ")[0].Split("-")[0]);
                newVal.Max = int.Parse(i.Split(" ")[0].Split("-")[1]);
                newVal.Character = char.Parse(i.Split(" ")[1].Remove(1));
                newVal.Pword = i.Split(" ")[2];
                parsedInput.Add(newVal);
            }

            return parsedInput;
        }
    }

    class Pwordandrule {
        public int Min { get; set; }
        public int Max { get; set; }
        public char Character { get; set; }
        public string Pword { get; set; } = string.Empty;
    }
}