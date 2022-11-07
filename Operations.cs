using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CSV;
/// <summary>
/// Класс для группировки всех методов, необходимых для решения поставленной задачи
/// </summary>
public static class Operations
{
    /// <summary>
    /// Выводит в консоль текстовое меню, для взаимодействия с программой
    /// </summary>
    public static void ShowMenu()
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("Для перемещения по программе используйте следующие операции \n" +
                      "1 - Ввести название файла\n" +
                      "2 - Вывести на экран информацию о группах по Experience и сохранить в файл workers.csv\n" +
                      "3 - Вывести на экран информацию о группах по году работы и сохранить в файлы вида Workers-(Year).csv\n" +
                      "4 - Вывести на экран выборку работников, зарплата которых находится в диапазоне от 70% до 80% от максимальной и сохранить в файл Salary-workers.csv\n" +
                      "5 - Вывести на экран сводную статистику по данным загруженного файла:\n" +
                      "\ta) Общее количество строк с данными\n" +
                      "\tb) Работников с наибольшей и наименьшей зарплатой\n" +
                      "\tc) Количество Data Engineer работающих из GB\n" +
                      "\td) Количество работников работающих в компаниях из GB, но работающих из иной страны\n"
        );
        Console.ResetColor();
    }

    /// <summary>
    /// Сканирует введённый файл на правильность пути до него и корректность данных в нём
    /// </summary>
    /// <returns>Возвращает список объектов класса Worker из введённого файла</returns>
    /// <exception cref="ArgumentException">Вызывается при неудачном пути, переданному во входных данных</exception>
    /// <exception cref="FormatException">Вызывается при неудачном форматировании сожержимого файла </exception>
    public static List<Worker> ScanFile()
    {
        Console.WriteLine("Введите путь к файлу");
        var filePath = Console.ReadLine();
        if (filePath == null)
        {
            throw new ArgumentException("Путь к файлу не может быть пустым");
        }

        var reader = new StreamReader(filePath);
        var workers = new List<Worker>();
        var currentString = reader.ReadLine();
        while ((currentString = reader.ReadLine()) != null)
        {
            var currentWorker = new List<string>();
            short start = 0;
            bool inString = false;
            for (short i = 0; i < currentString.Length; ++i)
            {
                if (currentString[i] == '"')
                {
                    inString = inString is false;
                }

                if (inString is false && currentString[i] == ',')
                {
                    if (currentString[start..i].Length == 0)
                    {
                        throw new FormatException("Пустое поле");
                    }

                    currentWorker.Add(currentString[start..i]);
                    start = (short)(i + 1);
                }
            }

            currentWorker.Add(currentString[start..currentString.Length]);
            if (currentWorker.Count != 10)
            {
                throw new FormatException("Данные не соответствуют нужному формату");
            }

            currentWorker[5] = new Regex(",").Replace(currentWorker[5][1..^1], "");
            workers.Add(new Worker(
                short.Parse(currentWorker[0]), short.Parse(currentWorker[1]),
                currentWorker[2], currentWorker[3],
                currentWorker[4], double.Parse(currentWorker[5], CultureInfo.InvariantCulture),
                currentWorker[6], currentWorker[7],
                currentWorker[8], short.Parse(currentWorker[9])));
        }

        reader.Close();
        Console.WriteLine("Файл успешно прочитан");
        return workers;
    }

    /// <summary>
    /// Проверяет, был прочитан файл или нет
    /// </summary>
    /// <param name="workers">Список объектов класса Worker</param>
    /// <exception cref="DataException">Вызывается при обнаружении отсутствия прочтения файла </exception>
    public static void CheckFile(List<Worker> workers)
    {
        if (workers.Count == 0)
        {
            throw new DataException("Сначала нужно прочитать файл");
        }
    }

    /// <summary>
    /// Выводит на экран из набора исходных данных информацию о группах работников по Experience.
    /// Сохраняет в файл workers.csv группируя по опыту работы
    /// </summary>
    /// <param name="workers">Список объектов класса Worker</param>
    public static void GroupByExperience(List<Worker> workers)
    {
        CheckFile(workers);
        var groupsOfWorkersByExperience = workers.GroupBy(x => x.Experience);
        var writer = new StreamWriter("workers.csv");
        writer.WriteLine("Grouped by experience, information about employees");
        Console.WriteLine("Сгруппированная по опыту, информация о работниках");
        foreach (var currentGroup in groupsOfWorkersByExperience)
        {
            writer.WriteLine("Group: " + currentGroup.Key);
            Console.WriteLine("Группа: " + currentGroup.Key);
            foreach (var currentWorker in currentGroup)
            {
                writer.WriteLine(currentWorker);
                Console.WriteLine(currentWorker);
            }

            writer.WriteLine();
            Console.WriteLine();
        }

        writer.Close();
    }

    /// <summary>
    /// Выводит на экран список работников, группируя их по году работы, для каждой
    /// группы перед ней указывает самую большую зарплату в группе.
    /// Сохранять перечень работников по годам в файле Workers-N.csv, где N - номер года.
    /// </summary>
    /// <param name="workers">Список объектов класса Worker</param>
    public static void GroupByYear(List<Worker> workers)
    {
        CheckFile(workers);
        var groupsOfWorkersByYear = workers.GroupBy(x => x.WorkingYear);
        Console.WriteLine("Сгруппированная по годам работы, информация о работниках");
        foreach (var currentGroup in groupsOfWorkersByYear)
        {
            var maxGroupSalary = currentGroup.Select(x => x.SalaryInRupees).Max();
            var writer = new StreamWriter($"Workers-{currentGroup.Key}.csv");
            writer.WriteLine($"Group: {currentGroup.Key}\nMax salary: {maxGroupSalary}");
            Console.WriteLine($"Группа: {currentGroup.Key}\nМаксимальная зарплата: {maxGroupSalary}");
            foreach (var currentWorker in currentGroup)
            {
                writer.WriteLine(currentWorker);
                Console.WriteLine(currentWorker);
            }

            Console.WriteLine();
            writer.Close();
        }
    }
    /// <summary>
    /// Выводит на экран выборку работников, зарплата которых находится в диапазоне от 70 до 80% от максимальной.
    /// Сохраняет выборку в файл Salary-workers.csv
    /// </summary>
    /// <param name="workers">Список объектов класса Worker</param>
    public static void SelectBySalary(List<Worker> workers)
    {
        CheckFile(workers);
        var maxSalary = workers.Select(x => x.SalaryInRupees).Max();
        var selectedWorkersBySalary =
            workers.Where(x => maxSalary * 0.7 <= x.SalaryInRupees && x.SalaryInRupees <= maxSalary * 0.8);
        Console.WriteLine("Выборка работников, зарплата которых находится в диапазоне от 70% до 80% от максимальной");
        var writer = new StreamWriter($"Salary-workers.csv");
        foreach (var currentWorker in selectedWorkersBySalary)
        {
            writer.WriteLine(currentWorker);
            Console.WriteLine(currentWorker);
        }

        Console.WriteLine();
        writer.Close();
    }
    /// <summary>
    /// Выводит на экран сводную статистику по данным загруженного файла:
    /// a. Общее количество строк с данными
    /// b. Работников с наибольшей и наименьшей зарплатой.
    /// c. Количество Data Engineer работающих из GB.
    /// d. Количество работников работающих в компаниях из GB, но работающих из
    /// иной страны, перед каждым из них пишет из какой страны он работает и его Id
    /// </summary>
    /// <param name="workers">Список объектов класса Worker</param>
    public static void ShowStatistic(List<Worker> workers)
    {
        CheckFile(workers);
        Console.WriteLine("a) Общее количество строк с данными:");
        Console.WriteLine($"\t{workers.Count}");
        Console.WriteLine("b) Работники с наибольшей и наименьшей зарплатой:");
        var maxSalary = workers.Select(x => x.SalaryInRupees).Max();
        var minSalary = workers.Select(x => x.SalaryInRupees).Min();
        var workersWithMaxSalary = workers.Where(x => x.SalaryInRupees == maxSalary);
        var workersWithMinSalary = workers.Where(x => x.SalaryInRupees == minSalary);
        Console.WriteLine("\tРаботники с наибольшей зарплатой:");
        foreach (var currentWorker in workersWithMaxSalary)
        {
            Console.WriteLine($"\t{currentWorker}");
        }
        Console.WriteLine("\tРаботники с наименьшей зарплатой:");
        foreach (var currentWorker in workersWithMinSalary)
        {
            Console.WriteLine($"\t{currentWorker}");
        }
        Console.WriteLine("c) Количество Data Engineer работающих из GB");
        var countOfDataEngineersFromGb = workers.Count(x =>
            x.Designation == "Data Engineer" && x.EmployeeLocation == "GB");
        Console.WriteLine($"\t{countOfDataEngineersFromGb}");
        Console.WriteLine("d) Количество работников работающих в компаниях из GB, но работающих из иной страны");
        var workersNotFromGb = workers
            .Where(x => x.CompanyLocation == "GB" && x.EmployeeLocation != "GB")
            .Select(x=>(x.EmployeeLocation,x.Id));
        Console.WriteLine($"\t{workersNotFromGb.Count()}");
        foreach (var currentWorker in workersNotFromGb)
        {
            Console.WriteLine($"\t{currentWorker}" );
        }
        /*
         Задание этого пукта составлено некоректно.
         Нужно вывести на экран количество работников работающих в компаниях из GB, но работающих из
         иной страны, перед каждым из них написать из какой страны он работает.
         Если вывести нужно только количество, то невозможно написать о том, из какой страны работает работник, если не
         просят выводить информацию о нём. Поэтому решил вывести из какой страны работает работник и его Id.
        */
    }
}