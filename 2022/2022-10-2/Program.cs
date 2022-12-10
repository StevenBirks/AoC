namespace MyApp
{
    internal class Program
    {
        public static HashSet<int> MasterCycles = new HashSet<int>();
        public static int Register = 1;
        public static List<List<char>> Rows = new List<List<char>>();

        static void Main(string[] args)
        {
            var input = ReadInput();

            var parsedInput = ParseInput(input);

            Calculate(parsedInput);

            Display();
        }

        private static void Display() {
            foreach (var row in Rows) {
                Console.WriteLine(new string(row.ToArray()));
            }
        }

        private static void Calculate(List<Operation> operations) {
            var cycle = 1;
            foreach (var op in operations) {
                Step(cycle);
                cycle++;

                if (op.Type == OpType.addx) {
                    Step(cycle);
                    cycle++;

                    Register += (int)op.Value;
                }
            }
        }

        private static void Step(int cycle) {
            var pos = (cycle - 1) % 40;

            if (pos == 0) {
                Rows.Add(new List<char>());
            }

            var displayChar = '.';

            if (Math.Abs(Register - pos) <= 1) {
                displayChar = '#';
            }

            Rows.Last().Add(displayChar);
        }

        private static List<Operation> ParseInput(string input) {
            var operations = new List<Operation>();

            foreach (var line in input.Split("\r\n")) {
                var newOp = new Operation();

                if (line.Split(" ")[0] == "noop") {
                    newOp.Type = OpType.noop;
                } else {
                    newOp.Type = OpType.addx;
                    newOp.Value = int.Parse(line.Split(" ")[1]);
                }

                operations.Add(newOp);
            }

            return operations;
        }

        private static string ReadInput() {
            return System.IO.File.ReadAllText(@"./input.txt");
        }
    }

    public class Operation {
        public OpType Type { get; set; }
        public int? Value { get; set; }
    }

    public enum OpType  {
        addx,
        noop
    }
}