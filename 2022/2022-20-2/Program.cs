namespace MyApp
{
    internal class Program
    {
        public static List<ListItem> EncryptedFile = new List<ListItem>();

        static void Main(string[] args)
        {
            var input = ReadInput();

            ParseInput(input);

            var result = Calculate();

            Console.WriteLine($"Result: {result}");
        }

        private static long Calculate() {
            for (var j = 0; j < 10; j++) {
                Console.WriteLine(j);
                for (var i = 0; i < EncryptedFile.Count; i++) {
                    MoveItem(i);
                }
            }

            var indexOfZero = EncryptedFile.FindIndex(0,EncryptedFile.Count, o => o.Val == 0);

            Console.WriteLine(EncryptedFile[(1000 + indexOfZero) % (EncryptedFile.Count)].Val);
            Console.WriteLine(EncryptedFile[(2000 + indexOfZero) % (EncryptedFile.Count)].Val);
            Console.WriteLine(EncryptedFile[(3000 + indexOfZero) % (EncryptedFile.Count)].Val);

            return EncryptedFile[(1000 + indexOfZero) % EncryptedFile.Count].Val + 
                   EncryptedFile[(2000 + indexOfZero) % EncryptedFile.Count].Val + 
                   EncryptedFile[(3000 + indexOfZero) % EncryptedFile.Count].Val;
        }

        private static void MoveItem(int itemIndex) {
            var item = EncryptedFile.Where(o => o.InitialIndex == itemIndex).Single();
            var currentIndex = EncryptedFile.IndexOf(item);

            var newIndex = (currentIndex + item.Val) % ((long)EncryptedFile.Count - 1);

            if (newIndex < 0) {
                newIndex = EncryptedFile.Count - 1 + newIndex;
            }

            EncryptedFile.Remove(item);
            EncryptedFile.Insert((int)newIndex,item);

            //Console.WriteLine($"{string.Join(string.Empty, EncryptedFile.Select(o => o.Val).ToArray())}");
        }

        private static string ReadInput() {
            return System.IO.File.ReadAllText(@"./input.txt");
        }

        private static void ParseInput(string input) {
            var index = 0;
            var decryptionKey = 811589153;

            foreach (var row in input.Split("\r\n")) {
                EncryptedFile.Add(new ListItem() {
                    InitialIndex = index,
                    Val = long.Parse(row) * decryptionKey
                });

                index++;
            }
        }

        public class ListItem {
            public long Val { get; set; }
            public int InitialIndex { get; set; }
        }
    }
}