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

        private static int Calculate(string input) {
            for (var i = 3; i < input.Length; i++) {
                var count = input.Substring(i-3, 4).ToList().Distinct().Count();

                if (count == 4) {
                    return i+1;
                }
            }

            return 0; // fail
        }

        private static string ReadInput() {
            return System.IO.File.ReadAllText(@"./input.txt");
        }
    }
}