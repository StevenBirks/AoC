namespace MyApp
{
    internal class Program
    {
        public static List<Location> Locations = new List<Location>();

        static void Main(string[] args)
        {
            var input = ReadInput();

            ParseInput(input);

            var result = Calculate();

            Console.WriteLine($"Result: {result}");
        }

        private static int Calculate() {
            var distance = 1;

            while (Locations.Where(o => o.IsEnd).Select(o => o.Distance).Single() == -1) {
                // Then loop until End is found
                Console.WriteLine($"distance: {distance}");
                CalcNextStep(distance);
                distance++;

            }

            return Locations.Where(o => o.IsEnd).Select(o => o.Distance).Single();
        }

        private static void CalcNextStep(int distance) {
            var locationsToStepFrom = Locations.Where(o => o.CheckNext).ToList();

            if (locationsToStepFrom.Count == 0) {

            }

            Console.WriteLine($"Checking: {locationsToStepFrom.Count}");

            foreach (var loc in locationsToStepFrom) {
                var foundLocations = Locations
                    .Where(o => (Math.Abs(o.X - loc.X) == 1 && o.Y == loc.Y) ||
                                (Math.Abs(o.Y - loc.Y) == 1 && o.X == loc.X))
                    .Where(o => !o.Checked)
                    .Where(o => o.Height == loc.Height + 1 ||
                                o.Height < loc.Height ||
                                o.Height == loc.Height)
                    .ToList();

                foundLocations.ForEach(o => {
                    o.CheckNext = true;
                    o.Distance = distance;
                });

                loc.Checked = true;
                loc.CheckNext = false;
            }

        }

        private static void ParseInput(string input) {
            var yIndex = 0;

            foreach (var row in input.Split("\r\n")) {
                var xIndex = 0;

                foreach (var loc in row.ToCharArray()) {
                    var newLocation = new Location() {
                        Checked = false,
                        CheckNext = false,
                        Distance = -1,
                        Height = 0,
                        X = xIndex,
                        Y = yIndex,
                        IsEnd = false
                    };

                    if (loc == 'S') {
                        newLocation.Height = 0;
                        newLocation.CheckNext = true;
                    } else if (loc == 'E') {
                        newLocation.Height = 25;
                        newLocation.IsEnd = true;
                    } else {
                        newLocation.Height = (int)loc - 97;
                    }

                    Locations.Add(newLocation);

                    xIndex++;
                }

                yIndex++;
            }
        }

        private static string ReadInput() {
            return System.IO.File.ReadAllText(@"./input.txt");
        }
    }

    public class Location {
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public bool CheckNext { get; set; }
        public bool Checked { get; set; }
        public int Distance { get; set; }
        public bool IsStart { get; set; }
        public bool IsEnd { get; set; }
    }
}
