namespace MyApp
{
    internal class Program
    {
        public static Dictionary<string, int> ScenicScores = new Dictionary<string, int>();
        static void Main(string[] args)
        {
            var input = ReadInput();

            var parsedInput = ParseInput(input);

            var result = Calculate(parsedInput);

            Console.WriteLine($"Result: {result}");
        }

        private static int Calculate(List<List<int>> input) {
            for (var i = 1; i < input.Count() -1; i++) { // loop over rows
                for (var j = 1; j < input[i].Count() -1; j++) { // loop over columns
                    // consider this tree;
                    var tree = input[i][j];
                    var upScore = 0;
                    var downScore = 0;
                    var leftScore = 0;
                    var rightScore = 0;


                    // look up
                    var y = i;

                    while (y > 0) {
                        upScore++;

                        if (input[y-1][j] >= tree) {
                            break;
                        }

                        y--;
                    }

                    // look down
                    y = i;

                    while (y < input.Count() - 1) {
                        downScore++;

                        if (input[y+1][j] >= tree) {
                            break;
                        }

                        y++;
                    }

                    // look left
                    var x = j;

                    while (x > 0) {
                        leftScore++;

                        if (input[i][x-1] >= tree) {
                            break;
                        }

                        x--;
                    }

                    // look right
                    x = j;

                    while (x < input[i].Count() - 1) {
                        rightScore++;
                        
                        if (input[i][x+1] >= tree) {
                            break;
                        }

                        x++;
                    }

                    ScenicScores.Add($"{j},{i}", upScore * downScore * leftScore * rightScore);
                }

            }

            return ScenicScores.OrderByDescending(o => o.Value).First().Value;
        }

        private static string ReadInput() {
            return System.IO.File.ReadAllText(@"./input.txt");
        }

        private static List<List<int>> ParseInput(string input) {
            var matrix = new List<List<int>>();

            foreach (var row in input.Split("\r\n")) {
                var newRow = new List<int>();

                foreach (var item in row.ToCharArray()) {
                    newRow.Add(int.Parse(item.ToString()));
                }

                matrix.Add(newRow);
            }

            return matrix;
        }
    }
}