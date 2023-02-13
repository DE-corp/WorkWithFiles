using System;
using System.IO;

namespace WorkWithFiles.Task2
{
    class Program
    {
        static long FullLength;
        static void DirInfo(string DirUrl)
        {
            try
            {
                if (Directory.Exists(DirUrl))
                {
                    var dirInfo = new DirectoryInfo(DirUrl);
                    var Files = dirInfo.GetFiles();
                    var Dirs = dirInfo.GetDirectories();

                    foreach (var file in Files)
                    {
                        FullLength += file.Length;
                    }

                    foreach (var dir in Dirs)
                    {
                        DirInfo(dir.FullName);
                    }
                }
                else
                {
                    Console.WriteLine("Директория не существует!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] {ex.Message}");
            }
        }

        static void Main(string[] args)
        {
            Console.Write("Введите URL директории: ");
            var DirUrl = Console.ReadLine();
            DirInfo(DirUrl);
            Console.WriteLine($"Общий размер всех файлов в директории: {FullLength} байт.");
        }
    }
}
