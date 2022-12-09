namespace MyApp
{
    internal class Program
    {
        public static HashSet<(int, int)> Locations = new HashSet<(int,int)>();
        public static List<Location> CurrentPartLocations = new List<Location>();

        static void Main(string[] args)
        {
            var input = ReadInput();

            for( var i = 0; i < 10; i++) {
                CurrentPartLocations.Add(new Location { Item1 = 0, Item2 = 0});
            }

            var result = Calculate(input);

            Console.WriteLine($"Result: {result}");
        }

        private static int Calculate(string input) {
            foreach (var line in input.Split("\r\n")) {
                var op = new Operation {
                    Dir = MapToDirection(line.Split(" ")[0]),
                    Steps = int.Parse(line.Split(" ")[1])
                };
                
                for (var i = 0; i < op.Steps; i++) {
                    MoveHead(op.Dir);

                    for(var j = 1; j < 10; j++) {
                        MovePart(j);
                    }

                    AddTailLocation();
                }
            }

            return Locations.Count;
        }

        public static void MovePart(int index) {
            // just up
            if (CurrentPartLocations[index-1].Item2 - CurrentPartLocations[index].Item2 == 2 &&
                CurrentPartLocations[index-1].Item1 == CurrentPartLocations[index].Item1) {
                CurrentPartLocations[index].Item2++;
            }
            // just down
            else if (CurrentPartLocations[index-1].Item2 - CurrentPartLocations[index].Item2 == -2 &&
                CurrentPartLocations[index-1].Item1 == CurrentPartLocations[index].Item1) {
                CurrentPartLocations[index].Item2--;
            }
            // just left
            else if (CurrentPartLocations[index-1].Item1 - CurrentPartLocations[index].Item1 == -2 &&
                CurrentPartLocations[index-1].Item2 == CurrentPartLocations[index].Item2) {
                CurrentPartLocations[index].Item1--;
            }
            // just right
            else if (CurrentPartLocations[index-1].Item1 - CurrentPartLocations[index].Item1 == 2 &&
                CurrentPartLocations[index-1].Item2 == CurrentPartLocations[index].Item2) {
                CurrentPartLocations[index].Item1++;
            }
            // up and up and left
            else if (CurrentPartLocations[index-1].Item2 - CurrentPartLocations[index].Item2 == 2 &&
                CurrentPartLocations[index-1].Item1 - CurrentPartLocations[index].Item1 == -1) {
                CurrentPartLocations[index].Item2++;
                CurrentPartLocations[index].Item1--;
            }
            // up and left and left
            else if (CurrentPartLocations[index-1].Item2 - CurrentPartLocations[index].Item2 == 1 &&
                CurrentPartLocations[index-1].Item1 - CurrentPartLocations[index].Item1 == -2) {
                CurrentPartLocations[index].Item2++;
                CurrentPartLocations[index].Item1--;
            }
            // up and up and right
            else if (CurrentPartLocations[index-1].Item2 - CurrentPartLocations[index].Item2 == 2 &&
                CurrentPartLocations[index-1].Item1 - CurrentPartLocations[index].Item1 == 1) {
                CurrentPartLocations[index].Item2++;
                CurrentPartLocations[index].Item1++;
            }
            // up and right and right
            else if (CurrentPartLocations[index-1].Item2 - CurrentPartLocations[index].Item2 == 1 &&
                CurrentPartLocations[index-1].Item1 - CurrentPartLocations[index].Item1 == 2) {
                CurrentPartLocations[index].Item2++;
                CurrentPartLocations[index].Item1++;
            }
            // down and down and left
            else if (CurrentPartLocations[index-1].Item2 - CurrentPartLocations[index].Item2 == -2 &&
                CurrentPartLocations[index-1].Item1 - CurrentPartLocations[index].Item1 == -1) {
                CurrentPartLocations[index].Item2--;
                CurrentPartLocations[index].Item1--;
            }
            // down and left and left
            else if (CurrentPartLocations[index-1].Item2 - CurrentPartLocations[index].Item2 == -1 &&
                CurrentPartLocations[index-1].Item1 - CurrentPartLocations[index].Item1 == -2) {
                CurrentPartLocations[index].Item2--;
                CurrentPartLocations[index].Item1--;
            }
            // down and down and right
            else if (CurrentPartLocations[index-1].Item2 - CurrentPartLocations[index].Item2 == -2 &&
                CurrentPartLocations[index-1].Item1 - CurrentPartLocations[index].Item1 == 1) {
                CurrentPartLocations[index].Item2--;
                CurrentPartLocations[index].Item1++;
            }
            // down and right and right
            else if (CurrentPartLocations[index-1].Item2 - CurrentPartLocations[index].Item2 == -1 &&
                CurrentPartLocations[index-1].Item1 - CurrentPartLocations[index].Item1 == 2) {
                CurrentPartLocations[index].Item2--;
                CurrentPartLocations[index].Item1++;
            }
            // up up left left
            else if (CurrentPartLocations[index-1].Item2 - CurrentPartLocations[index].Item2 == 2 &&
                CurrentPartLocations[index-1].Item1 - CurrentPartLocations[index].Item1 == -2) {
                CurrentPartLocations[index].Item2++;
                CurrentPartLocations[index].Item1--;
            }
            // up up right right
            else if (CurrentPartLocations[index-1].Item2 - CurrentPartLocations[index].Item2 == 2 &&
                CurrentPartLocations[index-1].Item1 - CurrentPartLocations[index].Item1 == 2) {
                CurrentPartLocations[index].Item2++;
                CurrentPartLocations[index].Item1++;
            }
            // down down left left
            else if (CurrentPartLocations[index-1].Item2 - CurrentPartLocations[index].Item2 == -2 &&
                CurrentPartLocations[index-1].Item1 - CurrentPartLocations[index].Item1 == -2) {
                CurrentPartLocations[index].Item2--;
                CurrentPartLocations[index].Item1--;
            }
            // down down right right
            else if (CurrentPartLocations[index-1].Item2 - CurrentPartLocations[index].Item2 == -2 &&
                CurrentPartLocations[index-1].Item1 - CurrentPartLocations[index].Item1 == 2) {
                CurrentPartLocations[index].Item2--;
                CurrentPartLocations[index].Item1++;
            }
        }

        public static void MoveHead(Direction dir) {
            switch(dir) {
                case Direction.Up:
                    CurrentPartLocations[0].Item2++;
                    break;
                case Direction.Down:
                    CurrentPartLocations[0].Item2--;
                    break;
                case Direction.Left:
                    CurrentPartLocations[0].Item1--;
                    break;
                case Direction.Right:
                    CurrentPartLocations[0].Item1++;
                    break;
            };
        }

        private static void AddTailLocation() {
            Locations.Add((CurrentPartLocations[9].Item1, CurrentPartLocations[9].Item2));
        }

        private static Direction MapToDirection(string input) {
            if (input == "U") {
                return Direction.Up;
            } else if (input == "D") {
                return Direction.Down;
            } else if (input == "L") {
                return Direction.Left;
            } else {
                return Direction.Right;
            } 
        }

        private static string ReadInput() {
            return System.IO.File.ReadAllText(@"./input.txt");
        }
    }

    public class Operation {
        public Direction Dir { get; set; }
        public int Steps { get; set; }
    }

    public enum Direction {
        Up,
        Down,
        Left,
        Right
    }

    public class Location {
        public int Item1 { get; set; }
        public int Item2 { get; set; }
    }
}