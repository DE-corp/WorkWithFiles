using System;
using System.IO;

namespace WorkWithFiles.Task1
{
    class Program
    {
        static void DeleteDir(string Path, int FreeMinutes)
        {
            try
            {
                var dirInfo = new DirectoryInfo(Path);
                if (dirInfo.Exists)
                {
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
                else
                {
                    Console.WriteLine("Директория не существует!");
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
            var FreeMinutes = 30;

            DeleteDir(Path, FreeMinutes);
            Console.WriteLine($"Путь {Path} очищен от файлов и папок, которые не используются {FreeMinutes} минут!");
        }
    }
}
