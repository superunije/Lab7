using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace Lab7
{
    internal class Program
    {
        static int ReadInt(string message, bool allowZero = false)
        {
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out int value) && (allowZero ? value >= 0 : value > 0))
                {
                    return value;
                }

                Console.WriteLine("Ошибка ввода. Введите положительное целое число.");
            }
        }

        static void PrintFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                Console.WriteLine("Файл не найден.");
                return;
            }

            Console.WriteLine($"Содержимое файла {fileName}:");

            Console.WriteLine(File.ReadAllText(fileName));
        }

        public static void PrintFileXmlToys(string fileName)
        {
            Console.WriteLine($"Содержимое файла {fileName}:");

            XmlSerializer serializer =
                new XmlSerializer(typeof(List<Toy>));

            using (FileStream fileStream = new FileStream(
                fileName,
                FileMode.Open,
                FileAccess.Read))
            {
                List<Toy>? toys =
                    serializer.Deserialize(fileStream)
                    as List<Toy>;

                if (toys is null)
                {
                    return;
                }

                foreach (Toy toy in toys)
                {
                    Console.WriteLine(
                        $"Название: {toy.Name}, " +
                        $"Цена: {toy.Price} руб., " +
                        $"Возраст: от {toy.MinAge} до {toy.MaxAge} лет");
                }
            }
        }

        public static void PrintFileStudents(string fileName)
        {
            if (!File.Exists(fileName))
            {
                Console.WriteLine("Файл не найден.");
                return;
            }

            Console.WriteLine($"Содержимое файла {fileName}:");

            foreach (string line in File.ReadLines(fileName))
            {
                string[] parts = line.Split(' ');

                Student student = new Student(
                    parts[0],
                    parts[1],
                    int.Parse(parts[2]),
                    int.Parse(parts[3]));

                Console.WriteLine(
                    $"{student.LastName} " +
                    $"{student.FirstName}, " +
                    $"школа №{student.School}, " +
                    $"балл: {student.Score}");
            }
        }

        public static void PrintFileBinary(string fileName)
        {
            Console.WriteLine($"Содержимое файла {fileName}:");

            using (FileStream fileStream = new FileStream(
                fileName,
                FileMode.Open,
                FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    while (fileStream.Position < fileStream.Length)
                    {
                        Console.WriteLine(reader.ReadInt32());
                    }
                }
            }
        }

        static void Main(string[] _)
        {
            const string NumbersPath = "numbers.txt";
            const string TextPath = "text.txt";
            const string BinaryPath = "binary.dat";
            const string ResultPath = "result.txt";
            const string ToysPath = "toys.xml";
            const string StudentsPath = "students.txt";

            while (true)
            {
                Console.WriteLine("\n\n--- Задание 1");
                Console.WriteLine("1. Получить новый файл, уменьшив каждый элемент исходного на 1.");
                Console.WriteLine("--- Задание 2");
                Console.WriteLine("2. Найти разность первого и максимального элементов в файле.");
                Console.WriteLine("--- Задание 3");
                Console.WriteLine("3. Переписать в другой файл строки, начинающиеся с буквы б");
                Console.WriteLine("--- Задание 4");
                Console.WriteLine("4. Найти разность первого и максимального элементов файла");
                Console.WriteLine("--- Задание 5");
                Console.WriteLine("5. Определить стоимость кукол для детей шести лет");
                Console.WriteLine("--- Задание 6");
                Console.WriteLine("6. Оставить в списке только первые вхождения одинаковых элементов");
                Console.WriteLine("--- Задание 7");
                Console.WriteLine("7. Если у элемента со значением E \"соседи\" не равны, поменять их местами");
                Console.WriteLine("--- Задание 8");
                Console.WriteLine("8. Задание с работниками фирмы");
                Console.WriteLine("--- Задание 10");
                Console.WriteLine("10. Задание со студентами");
                Console.WriteLine("0. Выход");

                var choice = ReadInt("Выбор: ", true);

                switch (choice)
                {
                    case 1:
                        Worker.FillFileNumbers(NumbersPath, 20);

                        Worker.DecreaseByOne(NumbersPath, ResultPath);

                        Console.WriteLine("\nИзначальный файл");
                        PrintFile(NumbersPath);
                        Console.WriteLine("\nИзменённый файл");
                        PrintFile(ResultPath);

                        break;
                    case 2:
                        Worker.FillFileNumbers(NumbersPath, 20);
                        PrintFile(NumbersPath);

                        Console.WriteLine("Разность первого и максимального числа равна:");
                        Console.WriteLine(Worker.DifferenceFirstAndMax(NumbersPath));

                        break;
                    case 3:
                        Worker.FillFileText(TextPath, 20);

                        Worker.CopyLinesStartingWithB(TextPath, ResultPath);

                        PrintFile(TextPath);
                        PrintFile(ResultPath);

                        break;
                    case 4:
                        Worker.FillFileBinary(BinaryPath, 20);
                        PrintFileBinary(BinaryPath);

                        Console.WriteLine("Разность первого и максимального числа равна:");
                        Console.WriteLine(Worker.BinaryDifferenceFirstAndMax(BinaryPath));

                        break;
                    case 5:
                        Worker.FillFileXML(ToysPath);
                        PrintFileXmlToys(ToysPath);

                        Console.WriteLine(
                            $"\nСтоимость кукол для детей 6 лет: " +
                            $"{Worker.GetDollsCostForSixYears(ToysPath)} руб.");

                        break;
                    case 6:
                        List<int> list1 = Worker.CreateList(10);

                        Console.WriteLine("Исходный список:");
                        Worker.PrintList(list1);

                        Worker.LeaveFirstOccurrences(list1);

                        Console.WriteLine("После удаления повторов:");
                        Worker.PrintList(list1);

                        break;
                    case 7:
                        LinkedList<int> list2 = Worker.CreateLinkedList(10);

                        Console.WriteLine("Исходный список:");
                        Worker.PrintList(list2);

                        var E = ReadInt("Введите E\n", true);
                        Worker.SwapNeighborsOfE(list2, E);

                        Console.WriteLine("После обработки:");
                        Worker.PrintList(list2);

                        break;
                    case 8:
                        Worker.PrintHashSetAnswers();

                        break;
                    case 10:
                        Worker.GenerateStudentsFile(StudentsPath);
                        PrintFileStudents(StudentsPath);

                        List<Student> students =
                            Worker.ReadStudentsFile(StudentsPath);

                        Console.WriteLine("\nСтуденты с высшим баллом:");
                        Worker.ProcessSchool50(students);

                        break;
                    case 0:
                        return;
                }
            }
        }
    }
}