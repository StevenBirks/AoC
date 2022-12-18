using System.Numerics;

namespace MyApp
{
    internal class Program
    {
        public static List<List<char>> Column = new List<List<char>>();
        public static List<Rock> Rocks = new List<Rock>();
        public static Rock CurrentRock = new Rock();
        public static string Jet = string.Empty;
        public static int Height = 0;

        static void Main(string[] args)
        {
            ReadInput();
            Console.WriteLine($"JetLength: {Jet.Length}");
            InitColumn();
            InitRocks();
            
            //// Stuff related to the example but modified
            // var initialRoundOffset = 7; 
            // var numberOfRounds = (1000000000000) / Jet.Length;
            // var repeat = 1; // TODO
            // var numberOfRepeatRounds = numberOfRounds / repeat; 
            // var numberOfSevenRoundsRemainder = numberOfRounds % repeat;
            // var repeatValues = 59 + 59 + 59 + 64 + 60 + 61 + 62;
            // var roundsByHeightGrowth = (numberOfRepeatRounds - 1) * repeatValues;
            // var result = initialRoundOffset + roundsByHeightGrowth + Height;

            var heightOffset = 393; // height after 257 rounds
            var rocksOffset = 257;

            var totalCompleteRounds = (1000000000000 - rocksOffset) / 1740; // 1740 is repeating pattern rocks;
            var repeatHeight = 2681; // rocks added per repeating round;

            var remainingRounds = (1000000000000 - rocksOffset) % 1740; // rounds after offset and repetitiveness

            Calculate(rocksOffset + remainingRounds);

            var totalHeight = (totalCompleteRounds * repeatHeight) + Height; // + Height contains offset + remainder;

            Console.WriteLine($"result: {totalHeight}");
        }

        private static void Calculate(long rockCountLimit) {
            var currentRockIndex = -1;
            int rockCount = 0;
            //var prevHeight = 0;

            while(rockCount < rockCountLimit) {
                rockCount++;
                currentRockIndex = (currentRockIndex + 1) % 5;
                
                InitiateRock(currentRockIndex);
                LowerRock();
                FixRock(rockCount, currentRockIndex);
                AddToColumn();


                // if (rockCount % Jet.Length == 0) {
                //     Console.WriteLine($"Growth = {Height - prevHeight}");
                //     prevHeight = Height;
                // }
            }

            Console.WriteLine($"rockcount processed: {rockCount}");
        }

        private static void FixRock(int rockCount, int rockIndex) {
            CurrentRock.Locations.ForEach((o => {
                Column[o.Y][o.X] = '#';
                if(new string(Column[o.Y].ToArray()) == "|#######|") {
                    Console.WriteLine($"Height: {Height}, rockCount: {rockCount},rockIndex: {rockIndex}");
                }
            }));


            var CurrRockHigh = CurrentRock.Locations[0].Y;

            Height = CurrRockHigh > Height ? CurrRockHigh : Height;
        }

        private static void LowerRock() {
            // push
            var blocked = false;
            var left = Jet[0] == '<';

            foreach (var rockPiece in CurrentRock.Locations) {
                if ((left && Column[rockPiece.Y][rockPiece.X - 1] != '.') ||
                    (!left && Column[rockPiece.Y][rockPiece.X + 1] != '.')) {
                        blocked = true;
                        continue;
                }
            }

            if (!blocked) {
                CurrentRock.Locations.ForEach(o => {
                    o.X = left ? o.X-1 : o.X+1;
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
            Column.Add("|.......|".ToCharArray().ToList());
            Column.Add("|.......|".ToCharArray().ToList());
            Column.Add("|.......|".ToCharArray().ToList());
        }

        private static void InitiateRock(int rockIndex) {
            CurrentRock.Locations = new List<Location>();

            foreach(var rockPart in Rocks[rockIndex].Locations) {
                CurrentRock.Locations.Add(new Location { Y = rockPart.Y + Height, X = rockPart.X });
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
            Rock1.Locations.Add(new Location { X = 4, Y = 6 });
            Rock1.Locations.Add(new Location { X = 3, Y = 5 });
            Rock1.Locations.Add(new Location { X = 4, Y = 5 });
            Rock1.Locations.Add(new Location { X = 5, Y = 5 });
            Rock1.Locations.Add(new Location { X = 4, Y = 4 });
            Rocks.Add(Rock1);

            // Rock 2
            var Rock2 = new Rock();
            Rock2.Locations.Add(new Location { X = 5, Y = 6 });
            Rock2.Locations.Add(new Location { X = 3, Y = 4 });
            Rock2.Locations.Add(new Location { X = 4, Y = 4 });
            Rock2.Locations.Add(new Location { X = 5, Y = 4 });
            Rock2.Locations.Add(new Location { X = 5, Y = 5 });
            Rocks.Add(Rock2);

            // Rock 3
            var Rock3 = new Rock();
            Rock3.Locations.Add(new Location { X = 3, Y = 7 });
            Rock3.Locations.Add(new Location { X = 3, Y = 4 });
            Rock3.Locations.Add(new Location { X = 3, Y = 5 });
            Rock3.Locations.Add(new Location { X = 3, Y = 6 });
            Rocks.Add(Rock3);

            // Rock 4
            var Rock4 = new Rock();
            Rock4.Locations.Add(new Location { X = 3, Y = 5 });
            Rock4.Locations.Add(new Location { X = 3, Y = 4 });
            Rock4.Locations.Add(new Location { X = 4, Y = 4 });
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