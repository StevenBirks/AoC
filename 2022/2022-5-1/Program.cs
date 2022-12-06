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
                for (var i = 0; i < op.CratesToMove; i++) {
                    MoveCrate(op.FromIndex, op.ToIndex, stacks);
                }
            }

            var result = "";
            
            stacks.ForEach(o => {
                result = $"{result}{o[o.Length -1]}";
            });

            return result;
        }

        private static void MoveCrate(int fromIndex, int toIndex, List<string> stacks) {
            stacks[toIndex] = stacks[toIndex] + (stacks[fromIndex].Substring(stacks[fromIndex].Length - 1));
            stacks[fromIndex] = stacks[fromIndex].Remove(stacks[fromIndex].Length - 1, 1);
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