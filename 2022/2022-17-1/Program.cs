namespace MyApp
{
    internal class Program
    {
        public static List<List<char>> Column = new List<List<char>>();
        public static List<Rock> Rocks = new List<Rock>();
        public static Rock CurrentRock = new Rock();
        public static string Jet = string.Empty;

        static void Main(string[] args)
        {
            ReadInput();
            InitColumn();
            InitRocks();

            var result = Calculate();

            Console.WriteLine($"Result: {result}");
        }

        private static int Calculate() {
            var currentRockIndex = -1;
            var rockCount = 0;

            while(rockCount < 2022) {
                rockCount++;
                currentRockIndex = (currentRockIndex + 1) % 5;
                
                InitiateRock(currentRockIndex);
                LowerRock();
                FixRock();
                AddToColumn();

                // for (var i = Column.Count - 1; i >= 0; i--) {
                //     Console.WriteLine(new string(Column[i].ToArray()));
                // };
                //Console.WriteLine("          ");
            }

            return Column.Where(o => o.Contains('#')).ToList().Count;
        }

        private static void FixRock() {
            CurrentRock.Locations.ForEach((o => {
                Column[o.Y][o.X] = '#';
            }));
        }

        private static void LowerRock() {
            // push
            var blocked = false;

            foreach (var rockPiece in CurrentRock.Locations) {
                if ((Jet[0] == '<' && Column[rockPiece.Y][rockPiece.X - 1] != '.') ||
                    (Jet[0] == '>' && Column[rockPiece.Y][rockPiece.X + 1] != '.')) {
                        blocked = true;
                }
            }

            if (!blocked) {
                CurrentRock.Locations.ForEach(o => {
                    o.X = Jet[0] == '<' ? o.X-1 : o.X+1;
                });
            }

            IterateJet();

            // lower
            foreach (var rockPiece in CurrentRock.Locations) {
                if (Column[rockPiece.Y - 1][rockPiece.X] != '.') {
                    return;
                }
            }

            CurrentRock.Locations.ForEach(o => {
                o.Y--;
            });

            LowerRock();    
        }

        private static void IterateJet() {
            Jet = $"{Jet.Substring(1)}{Jet[0]}";
        }

        private static void AddToColumn() {
            Column.Add("|.......|".ToCharArray().ToList());
            Column.Add("|.......|".ToCharArray().ToList());
            Column.Add("|.......|".ToCharArray().ToList());
        }

        private static void InitColumn() {
            Column.Add("+-------+".ToCharArray().ToList());
            Column.Add("|.......|".ToCharArray().ToList());
            Column.Add("|.......|".ToCharArray().ToList());
            Column.Add("|.......|".ToCharArray().ToList());
            Column.Add("|.......|".ToCharArray().ToList());
            Column.Add("|.......|".ToCharArray().ToList());
            Column.Add("|.......|".ToCharArray().ToList());
            Column.Add("|.......|".ToCharArray().ToList());
        }

        private static void InitiateRock(int rockIndex) {
            var maxIndex = Column
                .Select((o, index) => new { o, index })
                .Where(g => g.o.Contains('#') || g.o.Contains('-'))
                .Max(o => o.index);

            var rock = Rocks[rockIndex];

            CurrentRock.Locations = new List<Location>();

            foreach(var rockPart in rock.Locations) {
                CurrentRock.Locations.Add(new Location { Y = rockPart.Y + maxIndex, X = rockPart.X });
            }
        }

        private static void InitRocks() {
            // Rock 0
            var Rock0 = new Rock();
            Rock0.Locations.Add(new Location { X = 3, Y = 4 });
            Rock0.Locations.Add(new Location { X = 4, Y = 4 });
            Rock0.Locations.Add(new Location { X = 5, Y = 4 });
            Rock0.Locations.Add(new Location { X = 6, Y = 4 });
            Rocks.Add(Rock0);

            // Rock 1
            var Rock1 = new Rock();
            Rock1.Locations.Add(new Location { X = 3, Y = 5 });
            Rock1.Locations.Add(new Location { X = 4, Y = 5 });
            Rock1.Locations.Add(new Location { X = 5, Y = 5 });
            Rock1.Locations.Add(new Location { X = 4, Y = 6 });
            Rock1.Locations.Add(new Location { X = 4, Y = 4 });
            Rocks.Add(Rock1);

            // Rock 2
            var Rock2 = new Rock();
            Rock2.Locations.Add(new Location { X = 3, Y = 4 });
            Rock2.Locations.Add(new Location { X = 4, Y = 4 });
            Rock2.Locations.Add(new Location { X = 5, Y = 4 });
            Rock2.Locations.Add(new Location { X = 5, Y = 5 });
            Rock2.Locations.Add(new Location { X = 5, Y = 6 });
            Rocks.Add(Rock2);

            // Rock 3
            var Rock3 = new Rock();
            Rock3.Locations.Add(new Location { X = 3, Y = 4 });
            Rock3.Locations.Add(new Location { X = 3, Y = 5 });
            Rock3.Locations.Add(new Location { X = 3, Y = 6 });
            Rock3.Locations.Add(new Location { X = 3, Y = 7 });
            Rocks.Add(Rock3);

            // Rock 4
            var Rock4 = new Rock();
            Rock4.Locations.Add(new Location { X = 3, Y = 4 });
            Rock4.Locations.Add(new Location { X = 4, Y = 4 });
            Rock4.Locations.Add(new Location { X = 3, Y = 5 });
            Rock4.Locations.Add(new Location { X = 4, Y = 5 });
            Rocks.Add(Rock4);
        }

        private static void ReadInput() {
            Jet = System.IO.File.ReadAllText(@"./input.txt");
        }

        public class Rock {
            public List<Location> Locations { get; set; } = new List<Location>();
        }

        public class Location {
            public int X { get; set; }
            public int Y { get; set; }
        }
    }
}