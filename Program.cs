/*
 * Grachev Vasiliy
 * PI 229
 *
 * 05.11.2022 - 07.11.2022
 */


using CSV;

var workers = new List<Worker>();
while (true)
{
    try
    {
        Operations.ShowMenu();
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        switch (Console.ReadLine())
        {
            case "1":
                workers = Operations.ScanFile();
                break;
            case "2":
                Operations.GroupByExperience(workers);
                break;
            case "3":
                Operations.GroupByYear(workers);
                break;
            case "4":
                Operations.SelectBySalary(workers);
                break;
            case "5":
                Operations.ShowStatistic(workers);
                break;
            default:
                throw new ArgumentException("Неправильная команда");
        }
    }
    catch (ArgumentException e)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Неверные введённые данные: " + e.Message);
    }
    catch (FormatException)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("В файле неверные входные данные");
    }
    catch (FileNotFoundException)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Файл не был найден");
    }
    catch (Exception e)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(e.Message);
    }
}