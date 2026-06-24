using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab7
{
    public struct Toy
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
    }

    public record class Student(string LastName, string FirstName, int School, int Score);

    internal class Worker
    {
        public static void FillFileNumbers(string fileName, int count)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                for (int i = 0; i < count; i++)
                {
                    writer.WriteLine(Random.Shared.Next(-100, 101)); // числа от -100 до 100
                }
            }
            Console.WriteLine("Файл создан.");
        }

        public static void FillFileText(string fileName, int count)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                for (int i = 0; i < count; i++)
                {
                    int length = Random.Shared.Next(5, 11);
                    char[] chars = new char[length];

                    for (int j = 0; j < length; j++)
                    {
                        chars[j] = (char)Random.Shared.Next('А', 'Я' + 1);
                    }

                    writer.WriteLine(new string(chars));
                }
            }
        }

        public static void FillFileBinary(string fileName, int count)
        {
            using (FileStream fileStream = new FileStream(
                fileName,
                FileMode.Create,
                FileAccess.Write))
            {
                using (BinaryWriter writer = new BinaryWriter(fileStream))
                {
                    for (int i = 0; i < count; i++)
                    {
                        writer.Write(Random.Shared.Next(-100, 101));
                    }
                }
            }
        }

        public static void DecreaseByOne(string sourceFile, string resultFile)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(resultFile))
                {
                    foreach (string line in File.ReadLines(sourceFile))
                    {
                        if (int.TryParse(line, out int number))
                        {
                            writer.WriteLine(number - 1);
                        }
                    }
                }
                Console.WriteLine("Новый файл создан.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
        }

        public static int DifferenceFirstAndMax(string fileName)
        {
            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    var line = reader.ReadLine()!;

                    int first = int.Parse(line);
                    int max = first;

                    while ((line = reader.ReadLine()) != null)
                    {
                        int number = int.Parse(line);

                        if (number > max)
                        {
                            max = number;
                        }
                    }

                    return first - max;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
                return 0;
            }
        }

        public static void CopyLinesStartingWithB(string sourceFileName, string resultFileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(resultFileName))
                {
                    foreach (string line in File.ReadLines(sourceFileName))
                    {
                        if (!string.IsNullOrWhiteSpace(line) &&
                            char.ToLower(line[0]) == 'б')
                        {
                            writer.WriteLine(line);
                        }
                    }
                }
                Console.WriteLine("Новый файл создан.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
        }

        public static int BinaryDifferenceFirstAndMax(string fileName)
        {
            try
            {
                using (FileStream fileStream = new FileStream(
                fileName,
                FileMode.Open,
                FileAccess.Read))
                {
                    using (BinaryReader reader = new BinaryReader(fileStream))
                    {
                        int firstNumber = reader.ReadInt32();
                        int maxNumber = firstNumber;

                        while (fileStream.Position < fileStream.Length)
                        {
                            int currentNumber = reader.ReadInt32();

                            if (currentNumber > maxNumber)
                            {
                                maxNumber = currentNumber;
                            }
                        }

                        return firstNumber - maxNumber;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
                return 0;
            }
        }

        #region XML
        public static void FillFileXML(string fileName)
        {
            List<Toy> toys =
            [
                new Toy
                {
                    Name = "Кукла",
                    Price = 1500,
                    MinAge = 3,
                    MaxAge = 7
                },
                new Toy
                {
                    Name = "Машинка",
                    Price = 900,
                    MinAge = 5,
                    MaxAge = 10
                },
                new Toy
                {
                    Name = "Кукла",
                    Price = 2000,
                    MinAge = 6,
                    MaxAge = 8
                },
                new Toy
                {
                    Name = "Конструктор",
                    Price = 2500,
                    MinAge = 6,
                    MaxAge = 12
                }
            ];

            XmlSerializer serializer =
                new XmlSerializer(typeof(List<Toy>));

            using (FileStream stream = new FileStream(
                fileName,
                FileMode.Create))
            {
                serializer.Serialize(stream, toys);
            }
        }

        public static decimal GetDollsCostForSixYears(string fileName)
        {
            try
            {
                XmlSerializer serializer =
                new XmlSerializer(typeof(List<Toy>));

                using (FileStream stream = new FileStream(
                    fileName,
                    FileMode.Open))
                {
                    List<Toy> toys =
                        (List<Toy>)serializer.Deserialize(stream)!;

                    decimal totalCost = 0;

                    foreach (Toy toy in toys)
                    {
                        if (toy.Name == "Кукла" &&
                            toy.MinAge <= 6 &&
                            toy.MaxAge >= 6)
                        {
                            totalCost += toy.Price;
                        }
                    }

                    return totalCost;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
                return 0;
            }
            
        }
        #endregion

        #region List
        public static List<int> CreateList(int count)
        {
            List<int> numbers = new List<int>();

            for (int i = 0; i < count; i++)
            {
                numbers.Add(Random.Shared.Next(1, 11));
            }

            return numbers;
        }

        public static void PrintList(ICollection<int> numbers)
        {
            foreach (int number in numbers)
            {
                Console.Write($"{number} ");
            }

            Console.WriteLine();
        }

        public static void LeaveFirstOccurrences(List<int> numbers)
        {
            HashSet<int> uniqueNumbers = new HashSet<int>();

            for (int i = 0; i < numbers.Count; i++)
            {
                if (!uniqueNumbers.Add(numbers[i]))
                {
                    numbers.RemoveAt(i);
                    i--;
                }
            }
        }
        #endregion

        #region LinkedList
        public static LinkedList<int> CreateLinkedList(int count)
        {
            Random random = new Random();

            LinkedList<int> numbers = new LinkedList<int>();

            for (int i = 0; i < count; i++)
            {
                numbers.AddLast(random.Next(1, 11));
            }

            return numbers;
        }

        public static void SwapNeighborsOfE(LinkedList<int> numbers, int e)
        {
            LinkedListNode<int>? current = numbers.Find(e);

            if (current is null ||
                current.Previous is null ||
                current.Next is null)
            {
                return;
            }

            if (current.Previous.Value != current.Next.Value)
            {
                int temp = current.Previous.Value;
                current.Previous.Value = current.Next.Value;
                current.Next.Value = temp;
            }
        }
        #endregion

        #region HashSet
        static HashSet<string> _allLanguages =
        [
            "Английский",
            "Немецкий",
            "Французский",
            "Испанский",
            "Китайский"
        ];

        static HashSet<string> _employee1 =
        [
            "Английский",
            "Немецкий",
            "Французский"
        ];

        static HashSet<string> _employee2 =
        [
            "Английский",
            "Испанский"
        ];

        static HashSet<string> _employee3 =
        [
            "Английский",
            "Немецкий"
        ];

        public static HashSet<string> GetKnownByEveryone(
            HashSet<string> employee1,
            HashSet<string> employee2,
            HashSet<string> employee3)
        {
            HashSet<string> result = new HashSet<string>(employee1);

            result.IntersectWith(employee2);
            result.IntersectWith(employee3);

            return result;
        }

        public static HashSet<string> GetKnownByAtLeastOne(
            HashSet<string> employee1,
            HashSet<string> employee2,
            HashSet<string> employee3)
        {
            HashSet<string> result = new HashSet<string>(employee1);

            result.UnionWith(employee2);
            result.UnionWith(employee3);

            return result;
        }
        public static HashSet<string> GetKnownByNobody(
            HashSet<string> allLanguages,
            HashSet<string> knownByAtLeastOne)
        {
            HashSet<string> result =
                new HashSet<string>(allLanguages);

            result.ExceptWith(knownByAtLeastOne);

            return result;
        }

        public static void PrintLanguages(
            string title,
            HashSet<string> languages)
        {
            Console.WriteLine(title);

            foreach (string language in languages)
            {
                Console.WriteLine(language);
            }

            Console.WriteLine();
        }

        public static void PrintHashSetAnswers()
        {
            HashSet<string> knownByEveryone =
                GetKnownByEveryone(
                    _employee1,
                    _employee2,
                    _employee3);

            HashSet<string> knownByAtLeastOne =
                GetKnownByAtLeastOne(
                    _employee1,
                    _employee2,
                    _employee3);

            HashSet<string> knownByNobody =
                GetKnownByNobody(
                    _allLanguages,
                    knownByAtLeastOne);

            PrintLanguages(
                "Знают все работники:",
                knownByEveryone);

            PrintLanguages(
                "Знает хотя бы один работник:",
                knownByAtLeastOne);

            PrintLanguages(
                "Не знает никто:",
                knownByNobody);
        }
        #endregion

        #region Student
        public static void ProcessSchool50(List<Student> students)
        {
            var school50 = new List<Student>();
            for (int i = 0; i < students.Count; i++)
            {
                if (students[i].School == 50)
                {
                    school50.Add(students[i]);
                }
            }

            var maxScore = -1;
            for (int i = 0; i < school50.Count; i++)
            {
                if (school50[i].Score > maxScore)
                {
                    maxScore = school50[i].Score;
                }
            }

            var best = new List<Student>();
            for (int i = 0; i < school50.Count; i++)
            {
                if (school50[i].Score == maxScore)
                {
                    best.Add(school50[i]);
                }
            }

            if (best.Count > 2)
            {
                Console.WriteLine(best.Count);
                return;
            }

            if (best.Count == 2)
            {
                Console.WriteLine($"{best[0].LastName} {best[0].FirstName}");
                Console.WriteLine($"{best[1].LastName} {best[1].FirstName}");
                return;
            }

            var secondScore = -1;
            for (int i = 0; i < school50.Count; i++)
            {
                if (school50[i].Score < maxScore && school50[i].Score > secondScore)
                {
                    secondScore = school50[i].Score;
                }
            }

            var secondCount = 0;
            for (int i = 0; i < school50.Count; i++)
            {
                if (school50[i].Score == secondScore)
                {
                    secondCount++;
                }
            }

            if (best.Count > 0)
            {
                Console.WriteLine($"{best[0].LastName} {best[0].FirstName}");
            }
        }

        public static void GenerateStudentsFile(string fileName)
        {
            List<Student> students =
            [
                new("Иванов", "Иван", 15, 82),
                new("Петров", "Петр", 12, 91),
                new("Сидоров", "Алексей", 15, 77),
                new("Кузнецов", "Максим", 8, 95),
                new("Смирнова", "Анна", 12, 88),
                new("Смирнова", "Анна", 50, 88),
                new("Иванов", "Сергей", 50, 100),
                new("Петров", "Иван", 50, 95),
                new("Сидоров", "Алексей", 50, 90),
                new("Кузнецов", "Павел", 50, 80),
                new("Смирнов", "Дмитрий", 50, 70),
                new("Орлов", "Илья", 12, 100)
            ];

            using StreamWriter writer = new StreamWriter(fileName);

            foreach (Student student in students)
            {
                writer.WriteLine(
                    $"{student.LastName};" +
                    $"{student.FirstName};" +
                    $"{student.School};" +
                    $"{student.Score}");
            }
        }
        public static List<Student> ReadStudentsFile(string fileName)
        {
            List<Student> students = new List<Student>();

            foreach (string line in File.ReadLines(fileName))
            {
                string[] parts = line.Split(';');

                students.Add(
                    new Student(
                        parts[0],
                        parts[1],
                        int.Parse(parts[2]),
                        int.Parse(parts[3])));
            }

            return students;
        }
        #endregion
    }
}
