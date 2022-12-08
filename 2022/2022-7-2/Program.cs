namespace MyApp
{
    internal class Program
    {
        public static Dictionary<int, aFolder> AFolderStrut = new Dictionary<int, aFolder>();
        public static int CurrentFolderId;
        public static int FolderId = 1;

        public static int TotalDiskSpace = 70000000;
        public static int RequiredDiskSpace = 30000000;

        static void Main(string[] args)
        {
            var input = ReadInput();

            CreateFolder("/");

            var result = Calculate(input);

            Console.WriteLine($"Result: {result}");
        }

        private static int Calculate(string input) {
            foreach (var line in input.Split("\r\n")) {
                ExecuteCommand(line);
            }

            foreach (var item in AFolderStrut) {
                item.Value.Size = item.Value.aFiles.Select(o => o.Size).Sum();
            }

            var processedKeys = new HashSet<int>();

            while(processedKeys.Count() < AFolderStrut.Count() - 1) {
                foreach (var item in AFolderStrut) {
                    if (!processedKeys.Contains(item.Key)) {
                        var childFolders = AFolderStrut
                            .Where(o => o.Value.ParentFolderId == item.Key)
                            .Where(o => !processedKeys.Contains(o.Key))
                            .Any();

                        if (!childFolders) {
                            var parentFolder = GetFolder(item.Value.ParentFolderId);
                            parentFolder.Size += item.Value.Size;
                            processedKeys.Add(item.Key);
                        }
                    }
                }
            }

            var toBeCleared = RequiredDiskSpace - (TotalDiskSpace - AFolderStrut.Where(o => o.Key == 1).Single().Value.Size);

            return AFolderStrut
                .Where(o => o.Value.Size >= toBeCleared)
                .OrderBy(o => o.Value.Size)
                .First()
                .Value.Size;
        }

        private static void ExecuteCommand(string command) {
            var splitCommand = command.Split(" ");

            if (splitCommand[0] == "$" && splitCommand[1] == "cd") {
                if (splitCommand[2] == "..") {
                    var currentFolder = GetFolder(CurrentFolderId);

                    CurrentFolderId = currentFolder.ParentFolderId;
                } else {
                    var childFolderId = AFolderStrut.Where(o => o.Value.ParentFolderId == CurrentFolderId)
                        .Where(o => o.Value.FolderName == splitCommand[2])
                        .Select(o => o.Value.FolderId)
                        .Single();
                    CurrentFolderId = childFolderId;
                }
            } else if (splitCommand[0] == "dir") {
                CreateFolder(splitCommand[1]);
            } else if (splitCommand[0] == "$" && splitCommand[1] == "ls") {
            } else if (int.TryParse(splitCommand[0], out int result)) {
                var currentFolder = GetFolder(CurrentFolderId);

                AddFile(splitCommand, currentFolder);
            }
        }

        private static void  AddFile(string[] command, aFolder currentFolder) {
            currentFolder.aFiles.Add(new aFile() {
                Name = command[1],
                Size = int.Parse(command[0])
            });
        }

        private static aFolder GetFolder(int folderId) {
            AFolderStrut.TryGetValue(folderId, out var currentFolder);

            if (currentFolder == null) {
                Console.WriteLine($"O dear, missing folder: {folderId}");
            } else {
                return currentFolder;
            }

            return null;
        }

        private static void CreateFolder(string newFolderName) {
            var newFolder = new aFolder() {
                ParentFolderId = CurrentFolderId,
                FolderName = newFolderName,
                FolderId = FolderId
            };

            AFolderStrut.Add(FolderId, newFolder);
            
            FolderId++;
        }

        private static string ReadInput() {
            return System.IO.File.ReadAllText(@"./input.txt");
        }

        public class aFile {
            public string Name { get; set; } = string.Empty;
            public int Size { get; set; }
        }

        public class aFolder {
            public string FolderName { get; set; } = string.Empty;
            public int FolderId { get; set; } = 0;
            public int ParentFolderId { get; set; } = 0;
            public List<aFile> aFiles { get; set;} = new List<aFile>();
            public int Size { get; set; }
        }
    }
}