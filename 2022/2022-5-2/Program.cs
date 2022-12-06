namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var stacks = new List<string>();
            AddInput(stacks);
            var input = ReadInput();

            var parsedInput = ParseInput(input);

            var result = Calculate(parsedInput, stacks);

            Console.WriteLine($"Result: {result}");
        }

        private static void AddTest(List<String> stacks) {
            stacks.Add("ZN");
            stacks.Add("MCD");
            stacks.Add("P");
        }

        private static void AddInput(List<string> stacks) {
            stacks.Add("GDVZJSB");
            stacks.Add("ZSMGVP");
            stacks.Add("CLBSWTQF");
            stacks.Add("HJGWMRVQ");
            stacks.Add("CLSNFMD");
            stacks.Add("RGCD");
            stacks.Add("HGTRJDSQ");
            stacks.Add("PFV");
            stacks.Add("DRSTJ");
        }

        private static string Calculate(List<Move> parsedInput, List<string> stacks)
        {
            foreach (var op in parsedInput) {
                MoveCrates(op, stacks);
            }

            var result = "";
            
            stacks.ForEach(o => {
                result = $"{result}{o[o.Length -1]}";
            });

            return result;
        }

        private static void MoveCrates(Move operation, List<string> stacks) {
            stacks[operation.ToIndex] = stacks[operation.ToIndex] + (stacks[operation.FromIndex].Substring(stacks[operation.FromIndex].Length - operation.CratesToMove));
            stacks[operation.FromIndex] = stacks[operation.FromIndex].Remove(stacks[operation.FromIndex].Length - operation.CratesToMove);
        }

        private static string ReadInput()
        {
            return System.IO.File.ReadAllText(@"./input.txt");
        }

        private static List<Move> ParseInput(string input)
        {
            var parsedInput = new List<Move>();

            foreach (var i in input.Split("\r\n"))
            {
                var newVal = new Move();
                newVal.CratesToMove = int.Parse(i.Split(" ")[1]);
                newVal.FromIndex = int.Parse(i.Split(" ")[3]) - 1;
                newVal.ToIndex = int.Parse(i.Split(" ")[5]) - 1;

                parsedInput.Add(newVal);
            }

            return parsedInput;
        }
    }

    class Move
    {
        public int CratesToMove { get; set; } = 0;
        public int FromIndex { get; set; }
        public int ToIndex { get; set; }
    }
}