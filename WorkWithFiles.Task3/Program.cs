using System;
using System.IO;

namespace WorkWithFiles.Task3
{
    class Program
    {
        static long FullLength;

        static void GetPathSize(string path)
        {
            try
            {
                var dirInfo = new DirectoryInfo(path);
                var Files = dirInfo.GetFiles();
                var Dirs = dirInfo.GetDirectories();

                foreach (var file in Files)
                {
                    FullLength += file.Length;
                }

                foreach (var dir in Dirs)
                {
                    GetPathSize(dir.FullName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Error] " + ex.Message);
            }
        }


        static void DeleteDir(string Path, int FreeMinutes)
        {
            try
            {
                var dirInfo = new DirectoryInfo(Path);
                var Files = dirInfo.GetFiles();
                var Dirs = dirInfo.GetDirectories();

                foreach (var file in Files)
                {
                    if (file.LastAccessTime.CompareTo(DateTime.Now - TimeSpan.FromMinutes(FreeMinutes)) < 0)
                    {
                        file.Delete();
                    }
                }
                foreach (var dir in Dirs)
                {
                    // Используем рекурсию
                    DeleteDir(dir.FullName, FreeMinutes);

                    if (dir.LastAccessTime.CompareTo(DateTime.Now - TimeSpan.FromMinutes(FreeMinutes)) < 0)
                    {
                        dir.Delete(true);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Error] " + ex.Message);
            }
        }

        static void Main(string[] args)
        {
            Console.Write("Введите путь до папки: ");
            var Path = Console.ReadLine();
            if (Directory.Exists(Path))
            {
                var FreeMinutes = 1;

                GetPathSize(Path);
                Console.WriteLine($"\nИсходный размер папки: {FullLength} байт\n");

                DeleteDir(Path, FreeMinutes);
                Console.WriteLine($"Путь {Path} очищен от файлов и папок, которые не используются {FreeMinutes} минут!\n");

                long tempFullLength = FullLength;
                FullLength = 0;
                GetPathSize(Path);

                long CleanedLength = tempFullLength - FullLength;
                Console.WriteLine($"Освобождено: {CleanedLength} байт");
                Console.WriteLine($"Текущий размер папки: {FullLength} байт");
            }
            else
            {
                Console.WriteLine("Директория не существует!");
            }

        }
    }
}
