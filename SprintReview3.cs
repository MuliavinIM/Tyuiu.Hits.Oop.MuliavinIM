interface IInstallable
{
    void Install();
}

abstract class Component
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    protected Component(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public virtual void GetInfo()
    {
        Console.WriteLine($"{Name}, цена: {Price} руб.");
    }

    public override string ToString()
    {
        return $"{Name} — {Price} руб.";
    }
}

class CPU : Component, IInstallable
{
    public int Cores { get; set; }
    public double FrequencyGHz { get; set; }

    public CPU(string name, decimal price, int cores, double frequency)
        : base(name, price)
    {
        Cores = cores;
        FrequencyGHz = frequency;
    }

    public override void GetInfo()
    {
        base.GetInfo();
        Console.WriteLine($"Ядер: {Cores}, частота: {FrequencyGHz} ГГц");
    }

    public void Install()
    {
        Console.WriteLine($"Процессор {Name} установлен.");
    }

    public override bool Equals(object obj)
    {
        return obj is CPU other &&
               Name == other.Name &&
               Price == other.Price &&
               Cores == other.Cores &&
               FrequencyGHz == other.FrequencyGHz;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Price, Cores, FrequencyGHz);
    }

    public static bool operator ==(CPU a, CPU b) => object.Equals(a, b);
    public static bool operator !=(CPU a, CPU b) => !object.Equals(a, b);
}

class RAM : Component, IInstallable
{
    public int CapacityGB { get; set; }
    public int FrequencyMHz { get; set; }

    public RAM(string name, decimal price, int capacity, int frequency)
        : base(name, price)
    {
        CapacityGB = capacity;
        FrequencyMHz = frequency;
    }

    public override void GetInfo()
    {
        base.GetInfo();
        Console.WriteLine($"Объём: {CapacityGB} ГБ, частота: {FrequencyMHz} МГц");
    }

    public void Install()
    {
        Console.WriteLine($"Оперативная память {Name} установлена.");
    }

    public override bool Equals(object obj)
    {
        return obj is RAM other &&
               Name == other.Name &&
               Price == other.Price &&
               CapacityGB == other.CapacityGB &&
               FrequencyMHz == other.FrequencyMHz;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Price, CapacityGB, FrequencyMHz);
    }

    public static bool operator ==(RAM a, RAM b) => object.Equals(a, b);
    public static bool operator !=(RAM a, RAM b) => !object.Equals(a, b);
}

class GPU : Component, IInstallable
{
    public int MemoryGB { get; set; }

    public GPU(string name, decimal price, int memory)
        : base(name, price)
    {
        MemoryGB = memory;
    }

    public override void GetInfo()
    {
        base.GetInfo();
        Console.WriteLine($"Видеопамять: {MemoryGB} ГБ");
    }

    public void Install()
    {
        Console.WriteLine($"Видеокарта {Name} установлена.");
    }

    public override bool Equals(object obj)
    {
        return obj is GPU other &&
               Name == other.Name &&
               Price == other.Price &&
               MemoryGB == other.MemoryGB;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Price, MemoryGB);
    }

    public static bool operator ==(GPU a, GPU b) => object.Equals(a, b);
    public static bool operator !=(GPU a, GPU b) => !object.Equals(a, b);
}

class HDD : Component, IInstallable
{
    public int CapacityGB { get; set; }
    public int RotationSpeed { get; set; }

    public HDD(string name, decimal price, int capacity, int speed)
        : base(name, price)
    {
        CapacityGB = capacity;
        RotationSpeed = speed;
    }

    public override void GetInfo()
    {
        base.GetInfo();
        Console.WriteLine($"Объём: {CapacityGB} ГБ, скорость: {RotationSpeed} об/мин");
    }

    public void Install()
    {
        Console.WriteLine($"Жёсткий диск {Name} установлен.");
    }

    public override bool Equals(object obj)
    {
        return obj is HDD other &&
               Name == other.Name &&
               Price == other.Price &&
               CapacityGB == other.CapacityGB &&
               RotationSpeed == other.RotationSpeed;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Price, CapacityGB, RotationSpeed);
    }

    public static bool operator ==(HDD a, HDD b) => object.Equals(a, b);
    public static bool operator !=(HDD a, HDD b) => !object.Equals(a, b);
}

class Computer
{
    private List<Component> components = new List<Component>();
    public string Name { get; set; }

    public Computer(string name)
    {
        Name = name;
    }

    public void AddComponent<T>(T component) where T : Component, IInstallable
    {
        components.Add(component);
        component.Install();
    }

    public void RemoveComponent<T>(T component) where T : Component
    {
        components.Remove(component);
    }

    public decimal GetTotalPrice()
    {
        decimal total = 0;

        foreach (Component component in components)
            total += component.Price;

        return total;
    }

    public decimal GetTotalPrice(decimal discount)
    {
        return GetTotalPrice() * (1 - discount / 100);
    }

    public void PrintInfo()
    {
        Console.WriteLine($"\nКомпьютер: {Name}");

        foreach (Component component in components)
            component.GetInfo();

        Console.WriteLine($"Общая стоимость: {GetTotalPrice()} руб.");
    }
}

static class ComputerFactory
{
    public static Computer CreateComputer<TCpu, TRam, TGpu, THdd>(
        string name, TCpu cpu, TRam ram, TGpu gpu, THdd hdd)
        where TCpu : CPU
        where TRam : RAM
        where TGpu : GPU
        where THdd : HDD
    {
        Computer computer = new Computer(name);

        computer.AddComponent(cpu);
        computer.AddComponent(ram);
        computer.AddComponent(gpu);
        computer.AddComponent(hdd);

        return computer;
    }

    public static Computer CreateOfficeComputer()
    {
        return CreateComputer(
            "Офисный компьютер",
            new CPU("Intel Core i3-12100F", 8000, 4, 3.3),
            new RAM("Kingston Fury", 2500, 8, 3200),
            new GPU("GeForce GT 1030", 7000, 2),
            new HDD("WD Blue", 4500, 1000, 7200));
    }

    public static Computer CreateGamingComputer()
    {
        return CreateComputer(
            "Игровой компьютер",
            new CPU("AMD Ryzen 5 5600", 12000, 6, 3.5),
            new RAM("Kingston Fury", 5000, 16, 3200),
            new GPU("RTX 3060 Ti", 35000, 8),
            new HDD("Seagate Barracuda", 6500, 2000, 7200));
    }
}

class Program
{
    static void Main()
    {
        CPU cpu = new CPU("AMD Ryzen 5 5600", 12000, 6, 3.5);
        RAM ram = new RAM("Kingston Fury", 5000, 16, 3200);
        GPU gpu = new GPU("RTX 3060 Ti", 35000, 8);
        HDD hdd = new HDD("Seagate Barracuda", 6500, 2000, 7200);

        Computer computer = new Computer("Мой компьютер");

        computer.AddComponent(cpu);
        computer.AddComponent(ram);
        computer.AddComponent(gpu);
        computer.AddComponent(hdd);

        computer.PrintInfo();

        Console.WriteLine("\nToString:");
        Console.WriteLine(cpu.ToString());

        Console.WriteLine("\nСравнение компонентов:");
        Console.WriteLine(cpu == new CPU("AMD Ryzen 5 5600", 12000, 6, 3.5));
        Console.WriteLine(ram != new RAM("Kingston", 2500, 8, 3200));
        Console.WriteLine(gpu == new GPU("RTX 3060 Ti", 35000, 8));
        Console.WriteLine(hdd != new HDD("WD Blue", 4500, 1000, 7200));

        Console.WriteLine("\nПерегрузка метода:");
        Console.WriteLine($"Без скидки: {computer.GetTotalPrice()} руб.");
        Console.WriteLine($"Со скидкой 10%: {computer.GetTotalPrice(10)} руб.");

        computer.RemoveComponent(hdd);
        computer.PrintInfo();

        Computer office = ComputerFactory.CreateOfficeComputer();
        Computer gaming = ComputerFactory.CreateGamingComputer();

        office.PrintInfo();
        gaming.PrintInfo();
    }
}