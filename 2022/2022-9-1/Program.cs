namespace MyApp
{
    internal class Program
    {
        public static HashSet<(int, int)> Locations = new HashSet<(int,int)>();
        public static (int, int) CurrentHeadLoction = (0,0);
        public static (int, int) CurrentTailLoction = (0,0);

        static void Main(string[] args)
        {
            var input = ReadInput();

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
                    MoveTail();
                    AddTailLocation();
                }
            }

            return Locations.Count;
        }

        public static void MoveTail() {
            // just up
            if (CurrentHeadLoction.Item2 - CurrentTailLoction.Item2 == 2 &&
                CurrentHeadLoction.Item1 == CurrentTailLoction.Item1) {
                CurrentTailLoction.Item2++;
            }
            // just down
            else if (CurrentHeadLoction.Item2 - CurrentTailLoction.Item2 == -2 &&
                CurrentHeadLoction.Item1 == CurrentTailLoction.Item1) {
                CurrentTailLoction.Item2--;
            }
            // just left
            else if (CurrentHeadLoction.Item1 - CurrentTailLoction.Item1 == -2 &&
                CurrentHeadLoction.Item2 == CurrentTailLoction.Item2) {
                CurrentTailLoction.Item1--;
            }
            // just right
            else if (CurrentHeadLoction.Item1 - CurrentTailLoction.Item1 == 2 &&
                CurrentHeadLoction.Item2 == CurrentTailLoction.Item2) {
                CurrentTailLoction.Item1++;
            }
            // up and up and left
            else if (CurrentHeadLoction.Item2 - CurrentTailLoction.Item2 == 2 &&
                CurrentHeadLoction.Item1 - CurrentTailLoction.Item1 == -1) {
                CurrentTailLoction.Item2++;
                CurrentTailLoction.Item1--;
            }
            // up and left and left
            else if (CurrentHeadLoction.Item2 - CurrentTailLoction.Item2 == 1 &&
                CurrentHeadLoction.Item1 - CurrentTailLoction.Item1 == -2) {
                CurrentTailLoction.Item2++;
                CurrentTailLoction.Item1--;
            }
            // up and up and right
            else if (CurrentHeadLoction.Item2 - CurrentTailLoction.Item2 == 2 &&
                CurrentHeadLoction.Item1 - CurrentTailLoction.Item1 == 1) {
                CurrentTailLoction.Item2++;
                CurrentTailLoction.Item1++;
            }
            // up and right and right
            else if (CurrentHeadLoction.Item2 - CurrentTailLoction.Item2 == 1 &&
                CurrentHeadLoction.Item1 - CurrentTailLoction.Item1 == 2) {
                CurrentTailLoction.Item2++;
                CurrentTailLoction.Item1++;
            }
            // down and down and left
            else if (CurrentHeadLoction.Item2 - CurrentTailLoction.Item2 == -2 &&
                CurrentHeadLoction.Item1 - CurrentTailLoction.Item1 == -1) {
                CurrentTailLoction.Item2--;
                CurrentTailLoction.Item1--;
            }
            // down and left and left
            else if (CurrentHeadLoction.Item2 - CurrentTailLoction.Item2 == -1 &&
                CurrentHeadLoction.Item1 - CurrentTailLoction.Item1 == -2) {
                CurrentTailLoction.Item2--;
                CurrentTailLoction.Item1--;
            }
            // down and down and right
            else if (CurrentHeadLoction.Item2 - CurrentTailLoction.Item2 == -2 &&
                CurrentHeadLoction.Item1 - CurrentTailLoction.Item1 == 1) {
                CurrentTailLoction.Item2--;
                CurrentTailLoction.Item1++;
            }
            // down and right and right
            else if (CurrentHeadLoction.Item2 - CurrentTailLoction.Item2 == -1 &&
                CurrentHeadLoction.Item1 - CurrentTailLoction.Item1 == 2) {
                CurrentTailLoction.Item2--;
                CurrentTailLoction.Item1++;
            }
        }

        public static void MoveHead(Direction dir) {
            switch(dir) {
                case Direction.Up:
                    CurrentHeadLoction.Item2++;
                    break;
                case Direction.Down:
                    CurrentHeadLoction.Item2--;
                    break;
                case Direction.Left:
                    CurrentHeadLoction.Item1--;
                    break;
                case Direction.Right:
                    CurrentHeadLoction.Item1++;
                    break;
            };
        }

        private static void AddTailLocation() {
            // Console.WriteLine($"X:{CurrentTailLoction.Item1}, Y:{CurrentTailLoction.Item2}");
            Locations.Add((CurrentTailLoction.Item1, CurrentTailLoction.Item2));
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
}