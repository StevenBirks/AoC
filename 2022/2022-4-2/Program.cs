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

        private static int Calculate(string[] input) {
            var result = input
            .Select(o => new {
                Range1Low = int.Parse(o.Split(",")[0].Split("-")[0]),
                Range1High = int.Parse(o.Split(",")[0].Split("-")[1]),
                Range2Low = int.Parse(o.Split(",")[1].Split("-")[0]),
                Range2High = int.Parse(o.Split(",")[1].Split("-")[1]),
            })
            .Where(o => (o.Range2Low >= o.Range1Low && o.Range2Low <= o.Range1High) ||
                        (o.Range2High >= o.Range1Low && o.Range2High <= o.Range1High) || 
                        (o.Range1Low >= o.Range2Low && o.Range1Low <= o.Range2High) ||
                        (o.Range1High >= o.Range2Low && o.Range1High <= o.Range2High))
            .Count();

            return result;
        }

        private static string[] ReadInput() {
            return System.IO.File.ReadAllLines(@"./input.txt");
        }
    }
}