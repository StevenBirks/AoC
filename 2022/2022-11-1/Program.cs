using MoreLinq;

namespace MyApp
{
    internal class Program
    {
        public static List<Monkey> Monkeys = new List<Monkey>();

        static void Main(string[] args)
        {
            var input = ReadInput();

             ParseInput(input);

            var result = Calculate();

            Console.WriteLine($"Result: {result}");
        }

        private static int Calculate() {
            for (var round = 0; round < 20; round++) {
                foreach (var monkey in Monkeys) {
                    foreach (var item in monkey.StartingItems) {
                        // inspect
                        var newWorryValue = Inspect(item, monkey);

                        // reduce worry
                        newWorryValue = ReduceWorry(newWorryValue);

                        // check value
                        var newMonkeyIndex = FindWhichMonkeyToThrowTo(newWorryValue, monkey.TestValue, monkey.TrueMonkeyIndex, monkey.FalseMonkeyIndex);

                        // throw
                        ThrowItemToMonkey(newWorryValue, newMonkeyIndex);
                    }

                    monkey.StartingItems = new List<int>();
                }
            }

            var result = 1;

            var activeMonkeys = Monkeys
                .Select(o => o.Inspections)
                .OrderByDescending(o => o)
                .Take(2)
                .ToList();

            foreach(var activeMonkey in activeMonkeys) {
                result *= activeMonkey;
            };

            return result;
        }

        private static void ThrowItemToMonkey(int item, int monkeyIndex) {
            Monkeys[monkeyIndex].StartingItems.Add(item);
        }

        private static int FindWhichMonkeyToThrowTo(int itemWorry, int monkeyDivision, int trueMonkeyIndex, int falseMonkeyIndex) {
            return itemWorry % monkeyDivision == 0 
                ? trueMonkeyIndex
                : falseMonkeyIndex;
        }

        private static int Inspect(int itemWorry, Monkey monkey) {
            var val = monkey.OpValue;
            
            if (val == null) {
                val = itemWorry; 
            }  

            monkey.Inspections++;

            switch (monkey.Op) {
                case Operator.plus:
                    return itemWorry += (int)val;
                case Operator.minus:
                    return itemWorry -= (int)val;
                case Operator.multiply:
                    return itemWorry *= (int)val;
                case Operator.divide:
                    return itemWorry /= (int)val;
                default:
                    throw new NotSupportedException();
            }
        }

        private static void ParseInput(string input) {
            Monkeys = input
                .Split("\r\n")
                .Batch(7)
                .Select(o => CreateMonkey(o.ToList()))
                .ToList();
        }

        private static int ReduceWorry(int itemWorry) {
            return (int)(itemWorry / 3);
        }

        private static Monkey CreateMonkey(List<string> input) {
            var newMonkey = new Monkey();

            newMonkey.StartingItems = input[1]
                .Split(":")[1]
                .Split(",")
                .Select(o => int.Parse(o))
                .ToList();

            newMonkey.Op = input[2]
                .Split("=")[1]
                .Split(" ")[2]
                .Select(o => GetOperator(o))
                .Single();

            int val;

            var res = int.TryParse(input[2]
                .Split("=")[1]
                .Split(" ")[3], out val);

            if (res) {
                newMonkey.OpValue = val;
            }

            newMonkey.TestValue = int.Parse(input[3]
                .Split(":")[1]
                .Split(" ")[3]);

            newMonkey.TrueMonkeyIndex = int.Parse(input[4]
                .Split(":")[1]
                .Split(" ")[4]);

            newMonkey.FalseMonkeyIndex = int.Parse(input[5]
                .Split(":")[1]
                .Split(" ")[4]);           

            return newMonkey;
        }

        private static Operator GetOperator(char input) {
            switch (input) {
                case '+':
                  return Operator.plus;
                case '-':
                  return Operator.minus;
                case '*':
                  return Operator.multiply;
                case '/':
                  return Operator.divide;
                default:
                  throw new NotSupportedException();
            }
        }

        private static string ReadInput() {
            return System.IO.File.ReadAllText(@"./input.txt");
        }
    }

    public class Monkey {
        public List<int> StartingItems { get; set; } = new List<int>();
        public Operator Op { get; set; }
        public int? OpValue { get; set;}
        public int TestValue { get; set; }
        public int TrueMonkeyIndex { get; set; }
        public int FalseMonkeyIndex { get; set; }
        public int Inspections { get; set; } = 0;
    }

    public enum Operator  {
        plus,
        minus,
        multiply,
        divide
    }
}