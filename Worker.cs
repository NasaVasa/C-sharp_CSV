using System.Text.Json;

namespace CSV;

/// <summary>
/// Класс Worker для структурирования входных данных
/// </summary>
public class Worker
{
    /// <summary>
    /// Конструктор класса Worker
    /// </summary>
    /// <param name="id">Первое поле из входных данных</param>
    /// <param name="workingYear">Поле "Working_Year" из входных данных</param>
    /// <param name="designation">Поле "Designation" из входных данных</param>
    /// <param name="experience">Поле "Experience" из входных данных</param>
    /// <param name="employmentStatus">Поле "Employment_Status" из входных данных</param>
    /// <param name="salaryInRupees">Поле "Salary_In_Rupees" из входных данных</param>
    /// <param name="employeeLocation">Поле "Employee_Location" из входных данных</param>
    /// <param name="companyLocation">Поле "Company_Location" из входных данных</param>
    /// <param name="companySize">Поле "Company_Size" из входных данных</param>
    /// <param name="remoteWorkingRatio">Поле "Remote_Working_Ratio" из входных данных</param>
    /// <exception cref="ArgumentNullException">Исключение, вызываемое при равенстве NULL и строковых типов данных</exception>
    public Worker(short id, short workingYear, string designation, string experience,
        string employmentStatus, double salaryInRupees, string employeeLocation,
        string companyLocation, string companySize, short remoteWorkingRatio)
    {
        Id = id;
        WorkingYear = workingYear;
        Designation = designation ?? throw new ArgumentNullException(nameof(designation));
        Experience = experience ?? throw new ArgumentNullException(nameof(experience));
        EmploymentStatus = employmentStatus ?? throw new ArgumentNullException(nameof(employmentStatus));
        SalaryInRupees = salaryInRupees;
        EmployeeLocation = employeeLocation ?? throw new ArgumentNullException(nameof(employeeLocation));
        CompanyLocation = companyLocation ?? throw new ArgumentNullException(nameof(companyLocation));
        CompanySize = companySize ?? throw new ArgumentNullException(nameof(companySize));
        RemoteWorkingRatio = remoteWorkingRatio;
    }
    /// <summary>
    /// Переопределённый метод для "красивого" вывода объекта класса Worker в формате JSON
    /// </summary>
    /// <returns>Возвращает значение всех публичных свойств объекта</returns>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

    public short Id { get; }
    public short WorkingYear { get; }
    public string Designation { get; }
    public string Experience { get; }
    public string EmploymentStatus { get; }
    public double SalaryInRupees { get; }
    public string EmployeeLocation { get; }
    public string CompanyLocation { get; }
    public string CompanySize { get; }
    public short RemoteWorkingRatio { get; }
}