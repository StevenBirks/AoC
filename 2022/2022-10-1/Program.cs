namespace MyApp
{
    internal class Program
    {
        public static HashSet<int> MasterCycles = new HashSet<int>();
        public static int Register = 1;
        public static HashSet<int> RegisterValues = new HashSet<int>();

        static void Main(string[] args)
        {
            var input = ReadInput();

            var parsedInput = ParseInput(input);

            InitMasterCycles();

            var result = Calculate(parsedInput);

            Console.WriteLine($"Result: {result}");
        }

        private static int Calculate(List<Operation> operations) {
            var cycle = 1;
            foreach (var op in operations) {
                CheckCycle(cycle);
                cycle++;

                if (op.Type == OpType.addx) {
                    CheckCycle(cycle);
                    cycle++;

                    Register += (int)op.Value;
                }
            }

            return RegisterValues.ToList().Sum();
        }

        private static void CheckCycle(int cycle) {
            if (MasterCycles.Contains(cycle)) {
                RegisterValues.Add(Register * cycle);
            }
        }

        private static void InitMasterCycles() {
            MasterCycles.Add(20);
            MasterCycles.Add(60);
            MasterCycles.Add(100);
            MasterCycles.Add(140);
            MasterCycles.Add(180);
            MasterCycles.Add(220);
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