class Plant
{
    public string Name { get; set; }
    public string Type { get; set; }
    public float Height { get; set; }

    public Plant(string name, string type, float height)
    {
        Name = name;
        Type = type;
        Height = height;
    }

    public virtual void Bloom()
    {
        Console.WriteLine($"Растение {Name} цветёт");
    }

    public void Grow(float growth)
    {
        Height += growth;
        Console.WriteLine($"{Name} выросло на {growth} см. Высота: {Height} см.");
    }
}

class Flower : Plant
{
    public string Color { get; set; }
    public int PetalCount { get; set; }

    public Flower(string name, float height, string color, int petalCount)
        : base(name, "Цветок", height)
    {
        Color = color;
        PetalCount = petalCount;
    }

    public override void Bloom()
    {
        Console.WriteLine($"Цветок {Name} распускается и радует глаз!");
    }

    public void Wilt()
    {
        Console.WriteLine($"Цветок {Name} начинает увядать");
    }
}

class Tree : Plant
{
    public int Age { get; set; }
    public string LeafType { get; set; }

    public Tree(string name, float height, int age, string leafType)
        : base(name, "Дерево", height)
    {
        Age = age;
        LeafType = leafType;
    }

    public override void Bloom()
    {
        Console.WriteLine($"Дерево {Name} покрывается новыми листьями!");
    }

    public void GrowYears(int years)
    {
        Age += years;
        Console.WriteLine($"Дереву {Name} теперь {Age} лет");
    }
}

class Program
{
    static void Main()
    {
        Flower rose = new Flower("Роза", 30, "Красный", 25);
        Tree oak = new Tree("Дуб", 200, 5, "Лиственные");

        Console.WriteLine("ЦВЕТОК:");

        Console.WriteLine($"Название: {rose.Name}");
        Console.WriteLine($"Цвет: {rose.Color}");
        Console.WriteLine($"Количество лепестков: {rose.PetalCount}");

        rose.Bloom();
        rose.Grow(10);
        rose.Wilt();

        Console.WriteLine("\nДЕРЕВО:");

        Console.WriteLine($"Название: {oak.Name}");
        Console.WriteLine($"Тип листьев: {oak.LeafType}");
        Console.WriteLine($"Возраст: {oak.Age} лет");

        oak.Bloom();
        oak.Grow(50);
        oak.GrowYears(3);

        Console.WriteLine("\nПОЛИМОРФИЗМ:");

        Plant[] plants = { rose, oak };

        foreach (Plant plant in plants)
        {
            plant.Bloom();
        }
    }
}
