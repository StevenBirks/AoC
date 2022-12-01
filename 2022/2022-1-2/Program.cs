namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = ReadInput();

            var result = Calculate(input);

            Console.WriteLine($"Result: {result}");
        }

        private static string ReadInput() {
            return System.IO.File.ReadAllText(@"./input.txt");
        }

        private static int Calculate(string input) {
            var currentCount = 0;
            var elfCals = new List<int>();

            foreach (var i in input.Split("\r\n")) {
                if (i == string.Empty) {
                    elfCals.Add(currentCount);
                    currentCount = 0;
                } else {
                    currentCount += int.Parse(i);
                }
            }

            return elfCals.OrderByDescending(o => o).Take(3).Sum();
        }
    }
}