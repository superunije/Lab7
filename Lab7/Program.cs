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

            Console.WriteLine($"\nСодержимое файла {fileName}:");

            Console.WriteLine(File.ReadAllText(fileName));
        }


        public static void PrintFileBinary(string fileName)
        {
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

            while (true)
            {
                Console.WriteLine("\n\n--- Задание 1");
                Console.WriteLine("1. Создать файлы");
                Console.WriteLine("2. Получить новый файл, уменьшив каждый элемент исходного на 1.");
                Console.WriteLine("3. Просмотр содержимого файлов");

                Console.WriteLine("--- Задание 2");
                Console.WriteLine("4. Найти разность первого и максимального элементов в файле.");
                Console.WriteLine("--- Задание 3");
                Console.WriteLine("5. Переписать в другой файл строки, начинающиеся с буквы б");
                Console.WriteLine("--- Задание 4");
                Console.WriteLine("6. Найти разность первого и максимального элементов файла");
                Console.WriteLine("--- Задание 5");
                Console.WriteLine("7. Определить стоимость кукол для детей шести лет");
                Console.WriteLine("--- Задание 6");
                Console.WriteLine("8. Оставить в списке только первые вхождения одинаковых элементов");
                Console.WriteLine("--- Задание 7");
                Console.WriteLine("9. Если у элемента со значением E \"соседи\" не равны, поменять их местами");
                Console.WriteLine("--- Задание 8");
                Console.WriteLine("10. Задание с работниками фирмы");
                Console.WriteLine("--- Задание 9");
                Console.WriteLine("11. Определить с каких букв начинаются слова");
                Console.WriteLine("0. Выход");

                var choice = ReadInt("Выбор: ", true);

                switch (choice)
                {
                    case 1:
                        Worker.FillFileNumbers(NumbersPath, 20);
                        Worker.FillFileText(TextPath, 20);
                        Worker.FillFileBinary(BinaryPath, 20);
                        Worker.FillFileXML(ToysPath);

                        break;
                    case 2:
                        Worker.DecreaseByOne(NumbersPath, ResultPath);

                        break;
                    case 3:
                        try
                        {
                            Console.WriteLine("Просмотр файлов numbers.txt, result.txt: ");

                            PrintFile(NumbersPath);
                            PrintFile(TextPath);
                            PrintFileBinary(BinaryPath);
                            PrintFile(ResultPath);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Ошибка: {e.Message}");
                        }

                        break;
                    case 4:
                        Console.WriteLine(Worker.DifferenceFirstAndMax(NumbersPath));

                        break;
                    case 5:
                        Worker.CopyLinesStartingWithB(TextPath, ResultPath);

                        break;
                    case 6:
                        Worker.BinaryDifferenceFirstAndMax(BinaryPath);

                        break;
                    case 7:
                        decimal cost =
                            Worker.GetDollsCostForSixYears(ToysPath);

                        Console.WriteLine(
                            $"Стоимость кукол для детей 6 лет: {cost} руб.");

                        break;
                    case 8:
                        List<int> list1 = Worker.CreateList(10);

                        Console.WriteLine("Исходный список:");
                        Worker.PrintList(list1);

                        Worker.LeaveFirstOccurrences(list1);

                        Console.WriteLine("После удаления повторов:");
                        Worker.PrintList(list1);

                        break;
                    case 9:
                        LinkedList<int> list2 = Worker.CreateLinkedList(10);

                        Console.WriteLine("Исходный список:");
                        Worker.PrintList(list2);

                        var E = ReadInt("Введите E", true);
                        Worker.SwapNeighborsOfE(list2, E);

                        Console.WriteLine("После обработки:");
                        Worker.PrintList(list2);

                        break;
                    case 10:
                        Worker.PrintHashSetAnswers();

                        break;
                    case 11:
                        string fileName = "students.txt";

                        Worker.GenerateStudentsFile(fileName);

                        List<Student> students =
                            Worker.ReadStudentsFile(fileName);

                        Worker.ProcessSchool50(students);

                        break;
                    case 0:
                        return;
                }
            }
        }
    }
}