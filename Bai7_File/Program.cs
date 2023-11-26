namespace FileExample
{
    class Program
    {
        //Phuong thuc Liet ke tat ca cac file, thu muc trong thu muc path
        static void ListFileDirectory(string path)
        {
            String[] directories = System.IO.Directory.GetDirectories(path);
            String[] files = System.IO.Directory.GetFiles(path);
            foreach (var file in files)
            {
                Console.WriteLine(file);
            }
            foreach (var directory in directories)
            {
                Console.WriteLine(directory);
                ListFileDirectory(directory); // Đệ quy
            }
        }
        static void Main(string[] args)
        {
            //1.Doc thong tin o dia - DriveInfo
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (var drive in drives)
            {
                Console.WriteLine($"Drive: {drive.Name}");
                Console.WriteLine($"Type: {drive.DriveType}");
                Console.WriteLine($"Lable: {drive.VolumeLabel}");
                Console.WriteLine($"Format: {drive.DriveFormat}");
                Console.WriteLine($"Size: {drive.TotalSize}");
                Console.WriteLine($"Free: {drive.TotalFreeSpace}");
                Console.WriteLine("---------------------------------");
            }

            //2. Lam viec voi thu muc System.IO.Directory
            string path = "abc";
            Directory.CreateDirectory(path);
        
            if (Directory.Exists(path))
            {
                Console.WriteLine($"Thu muc {path} ton tai");
            }
            else 
            {
                Console.WriteLine($"Thu muc {path} khong ton tai");
            }

            ListFileDirectory("abc");
            Console.WriteLine("---------------------------------");

            //3. Lop Path ho tro lam viec voi duong dan
            Console.WriteLine(Path.DirectorySeparatorChar);

            var path1 = Path.Combine("Desktop", "Abc", "akaka.txt"); // Desktop\Abc\akaka.txt
            Console.WriteLine(path1);

            Console.WriteLine(Path.ChangeExtension(path1, "md"));    // Desktop\Abc\akaka.md
            
            Console.WriteLine(Path.GetDirectoryName(path1));         // Desktop\Abc
            
            Console.WriteLine(Path.GetExtension(path1));             // .txt
            
            Console.WriteLine(Path.GetFileName(path1));              // akaka.txt

            Console.WriteLine(Path.GetFileNameWithoutExtension(path1));   //akaka

            Console.WriteLine(Path.GetFullPath(path1));                // Lay duong dan day du - tu duong dan tuong doi

            Console.WriteLine(Path.GetPathRoot(path1));                //Lay goc duong dan

            Console.WriteLine(Path.GetRandomFileName());               // Tao file ngau nhien
            Console.WriteLine("----------------------------------");

            //4.Lam viec voi file
            string fileName = "Test.txt";
            string content = "Day la file Test.txt";

            File.WriteAllText(fileName, content);

            File.AppendAllText(fileName, "Them noi dung");

            string s = File.ReadAllText(fileName);
            Console.WriteLine(s);
        }
    }
    
}