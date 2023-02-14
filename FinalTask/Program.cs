using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    [Serializable]
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }

    }

    class Program
    {
        static void WriteFile(string path, string text)
        {
            if (!File.Exists(path))
            {
                using (StreamWriter sw = new StreamWriter(new FileStream(path, FileMode.Append)))
                {
                    sw.WriteLine(text);
                }
            }
        }

        static void Main(string[] args)
        {
            string path = Path.Combine(
               Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
               "Students"
            );

            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                    Console.WriteLine("Создана директория Students\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"[Error] {e.Message}");
                }
            }

            BinaryFormatter formatter = new BinaryFormatter();
            using (var fs = new FileStream("Students.dat", FileMode.Open))
            {
                var students = (Student[])formatter.Deserialize(fs);
                foreach (var student in students)
                {
                    string text = $"{student.Name}, {student.DateOfBirth}";
                    string filePath = @$"{path}\{student.Group}.txt"; // Наверное можно сделать покрасивее ))
                    WriteFile(filePath, text);

                    Console.WriteLine($"Имя: {student.Name} --- Группа: {student.Group} --- Дата рождения {student.DateOfBirth}");
                    
                }
                Console.WriteLine("\nСтуденты распределены по группам!");
            }
        }
    }
}
