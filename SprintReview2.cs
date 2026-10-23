interface IReportable
{
    string GenerateReport();
}

interface ITeamLeader
{
    string[] GetTeamMembers();
}

class Employee
{
    public string Name { get; set; }
    public string Position { get; set; }
    public double Salary { get; set; }

    public Employee(string name, string position, double salary)
    {
        Name = name;
        Position = position;
        Salary = salary;
    }

    public virtual void PrintInfo()
    {
        Console.WriteLine($"Имя: {Name}, должность: {Position}, зарплата: {Salary} руб.");
    }
}

class Manager : Employee
{
    public string Department { get; set; }

    public Manager(string name, string position, double salary, string department)
        : base(name, position, salary)
    {
        Department = department;
    }

    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"Отдел: {Department}");
    }
}

class ProjectManager : Manager, IReportable, ITeamLeader
{
    public string ProjectName { get; set; }
    private string[] teamMembers;

    public ProjectManager(string name, double salary, string department,
                          string projectName, string[] teamMembers)
        : base(name, "Проектный менеджер", salary, department)
    {
        ProjectName = projectName;
        this.teamMembers = teamMembers;
    }

    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine($"Проект: {ProjectName}");
    }

    public string GenerateReport()
    {
        return $"Проект: {ProjectName}, руководитель: {Name}, " +
               $"команда: {string.Join(", ", teamMembers)}";
    }

    public string[] GetTeamMembers()
    {
        return teamMembers;
    }
}

class Program
{
    static void Main()
    {
        Employee employee = new Employee("Анна", "Бухгалтер", 50000);
        Manager manager = new Manager("Сергей", "Менеджер", 75000, "Продажи");
        ProjectManager projectManager = new ProjectManager(
            "Иван", 90000, "Разработка", "Интернет-магазин",
            new string[] { "Мария", "Алексей", "Дмитрий" });

        employee.PrintInfo();
        Console.WriteLine();

        manager.PrintInfo();
        Console.WriteLine();

        projectManager.PrintInfo();

        IReportable reportable = projectManager;
        ITeamLeader teamLeader = projectManager;

        Console.WriteLine("\n" + reportable.GenerateReport());
        Console.WriteLine("Команда: " +
            string.Join(", ", teamLeader.GetTeamMembers()));
    }
}