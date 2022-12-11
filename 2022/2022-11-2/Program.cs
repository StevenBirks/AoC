using System.Numerics;
using MoreLinq;

namespace MyApp
{
    internal class Program
    {
        public static List<Monkey> Monkeys = new List<Monkey>();

        public static long MaxTestValue = 1;

        static void Main(string[] args)
        {
            var input = ReadInput();

            ParseInput(input);

            var result = Calculate();

            Console.WriteLine($"Result: {result}");
        }

        private static long Calculate() {
            Monkeys.ForEach(o => {
                    MaxTestValue *= o.TestValue;
            });

            for (var round = 0; round < 10000; round++) {
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

                    monkey.StartingItems = new List<long>();
                }

                if ((round + 1) % 1000 == 0) {
                    Console.WriteLine($"Round: {round + 1}");

                    foreach (var monkey in Monkeys) {
                        Console.WriteLine(monkey.Inspections);
                    }
                }
            }

            long result = 1;

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

        private static long ReduceWorry(long itemWorry) {
            itemWorry %= MaxTestValue;

            return itemWorry;
        }

        private static void ThrowItemToMonkey(long item, int monkeyIndex) {
            Monkeys[monkeyIndex].StartingItems.Add(item);
        }

        private static int FindWhichMonkeyToThrowTo(long itemWorry, int monkeyDivision, int trueMonkeyIndex, int falseMonkeyIndex) {
            return itemWorry % monkeyDivision == 0 
                ? trueMonkeyIndex
                : falseMonkeyIndex;
        }

        private static long Inspect(long itemWorry, Monkey monkey) {
            var val = (long?)monkey.OpValue;
            
            if (val == null) {
                val = itemWorry; 
            }  

            monkey.Inspections++;

            switch (monkey.Op) {
                case Operator.plus:
                    return itemWorry += (long)val;
                case Operator.multiply:
                    return itemWorry *= (long)val;
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

        private static Monkey CreateMonkey(List<string> input) {
            var newMonkey = new Monkey();

            newMonkey.StartingItems = input[1]
                .Split(":")[1]
                .Split(",")
                .Select(o => long.Parse(o))
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
                case '*':
                  return Operator.multiply;
                default:
                  throw new NotSupportedException();
            }
        }

        private static string ReadInput() {
            return System.IO.File.ReadAllText(@"./input.txt");
        }
    }

    public class Monkey {
        public List<long> StartingItems { get; set; } = new List<long>();
        public Operator Op { get; set; }
        public int? OpValue { get; set;}
        public int TestValue { get; set; }
        public int TrueMonkeyIndex { get; set; }
        public int FalseMonkeyIndex { get; set; }
        public int Inspections { get; set; } = 0;
    }

    public enum Operator  {
        plus,
        multiply,
    }
}