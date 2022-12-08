namespace MyApp
{
    internal class Program
    {
        public static HashSet<string> Locations = new HashSet<string>();
        static void Main(string[] args)
        {
            var input = ReadInput();

            var parsedInput = ParseInput(input);

            var result = Calculate(parsedInput);

            Console.WriteLine($"Result: {result}");
        }

        private static int Calculate(List<List<int>> input) {
            // From the Top
            for (var j = 1; j < input[0].Count() - 1; j++) { // for each column [j]
                var colMax =  input[0][j];

                for (var i = 1; i < input.Count() - 2; i++) { // loop over row values [i]
                    if (input[i][j] > colMax) {
                        Locations.Add($"{j},{i}");
                        colMax = input[i][j];
                    }
                }
            }

            // From the Bottom
            for (var j = 1; j < input[0].Count() - 1; j++) {
                var colMax = input[input.Count()-1][j];

                for (var i = input.Count() - 2; i > 0; i--) {
                    if (input[i][j] > colMax) {
                        Locations.Add($"{j},{i}");
                        colMax = input[i][j];
                    }
                }
            }

            // From the Left
            for (var i = 1; i < input.Count() - 1; i++) { // for each row [i]
                var rowMax = input[i][0];

                for (var j = 1; j < input[i].Count() - 2; j++) { // loop over column values [j]
                    if (input[i][j] > rowMax) {
                        Locations.Add($"{j},{i}");
                        rowMax = input[i][j];
                    }
                }
            }

            // From the Right
            for (var i = 1; i < input.Count() - 1; i++) {
                var rowMax = input[i][input[0].Count()-1];

                for (var j = input[i].Count() - 2; j > 0 ; j--) {
                    if (input[i][j] > rowMax) {
                        Locations.Add($"{j},{i}");
                        rowMax = input[i][j];
                    }
                }
            }

            var total = Locations.Count() // middle ones
             + (input[0].Count() * 2) // top and bottom rows
             + ((input.Count() - 2) * 2); // two sides minus top and bottom 

            return total;
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