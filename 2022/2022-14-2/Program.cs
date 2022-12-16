using System.Linq;

namespace MyApp
{
    internal class Program
    {
        public static List<List<char>> Locations = new List<List<char>>();
        public static int VoidLevel = 0;

        static void Main(string[] args)
        {
            for (var i = 0; i < 500; i++) {
                Locations.Add(Enumerable.Repeat('.', 700).ToList());
            }

            var input = ReadInput();

            ParseInput(input);

            for (var i = 0; i < Locations.Count; i++) {
                if (Locations[i].Contains('#')) {
                    VoidLevel = i;
                }
            }

            VoidLevel += 2;

            Locations[VoidLevel] = Enumerable.Repeat('#', 700).ToList();

            Console.WriteLine(VoidLevel);

            var result = Calculate();

            foreach (var row in Locations) {
                Console.WriteLine(string.Join("", row).Substring(400));
            }

            Console.WriteLine($"Result: {result}");
        }

        private static int Calculate() {
            var unitsOfSand = 0;

            var canAddSand = true;

            while (canAddSand) {
                unitsOfSand++;
                canAddSand = TryAddSand();
            }

            return unitsOfSand;
        }

        private static bool TryAddSand() {
            var oldSandPosition = (500,0);

            var newSandPosition = GetNewLocation(oldSandPosition);

            if (newSandPosition.Item2 == oldSandPosition.Item2 && newSandPosition.Item1 == oldSandPosition.Item1){
                Locations[newSandPosition.Item2][newSandPosition.Item1] = 'o';

                return false;
            }

            while (true) {
                if (newSandPosition.Item2 == oldSandPosition.Item2 && newSandPosition.Item1 == oldSandPosition.Item1){
                    Locations[newSandPosition.Item2][newSandPosition.Item1] = 'o';
                    return true;
                } else {
                    oldSandPosition.Item1 = newSandPosition.Item1;
                    oldSandPosition.Item2 = newSandPosition.Item2;

                    newSandPosition = GetNewLocation(oldSandPosition);
                }
            }
        }

        private static (int, int) GetNewLocation((int, int) oldSandPosition) {
            (int, int) newSandPosition;

            newSandPosition.Item1 = oldSandPosition.Item1;
            newSandPosition.Item2 = oldSandPosition.Item2;

            if (Locations[oldSandPosition.Item2 + 1][oldSandPosition.Item1] == '.') {
                newSandPosition.Item2++;

                return newSandPosition;
            } else if (Locations[oldSandPosition.Item2 + 1][oldSandPosition.Item1 - 1] == '.') {
                newSandPosition.Item2++;
                newSandPosition.Item1--;

                return newSandPosition;
            } else if (Locations[oldSandPosition.Item2 + 1][oldSandPosition.Item1 + 1] == '.') {
                newSandPosition.Item2++;
                newSandPosition.Item1++;

                return newSandPosition;
            } else {
                return oldSandPosition;
            }
        }

        

        private static void ParseInput(string input) {
            foreach (var row in input.Split("\r\n")) {
                var prevX = -1;
                var prevY = -1;

                foreach (var point in row.Split(" -> ")) {
                    var newX = int.Parse(point.Split(",")[0]);
                    var newY = int.Parse(point.Split(",")[1]);


                    if (prevX == -1 && prevY == -1) {
                        prevY = newY;
                        prevX = newX;
                    } else {
                        if (prevY == newY) {
                            var from = prevX < newX ? prevX : newX;
                            var end = prevX > newX ? prevX : newX;
                            
                            for (var start = from; start <= end; start++) {
                                Locations[prevY][start] = '#';
                            }
                        } else {
                            var from = prevY < newY ? prevY : newY;
                            var end = prevY > newY ? prevY : newY;
                            
                            for (var start = from; start <= end; start++) {
                                Locations[start][prevX] = '#';
                            }
                        }
                    }

                    prevY = newY;
                    prevX = newX;
                }

                prevX = -1;
                prevY = -1;
            }
        }

        private static string ReadInput() {
            return System.IO.File.ReadAllText(@"./input.txt");
        }
    }
}