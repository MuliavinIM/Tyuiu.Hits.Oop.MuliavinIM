class Product
{
    private string name;
    private decimal price;
    private int quantity;
    protected string category;
    internal string description;

    public string Name { get => name; set => name = value; }
    public decimal Price { get => price; set => price = value; }
    public int Quantity { get => quantity; set => quantity = value; }
    public string Category { get => category; set => category = value; }

    public Product(string name, decimal price, int quantity,
                   string category, string description)
    {
        this.name = name;
        this.price = price;
        this.quantity = quantity;
        this.category = category;
        this.description = description;
    }

    public decimal GetTotalPrice()
    {
        return price * quantity;
    }

    public void UpdateQuantity(int amount)
    {
        if (amount < 0)
        {
            return;
        }

        quantity = amount;
    }

    public string GetProductInfo()
    {
        return $"Название: {name}\n" +
               $"Цена: {price} руб.\n" +
               $"Количество: {quantity}\n" +
               $"Категория: {category}\n" +
               $"Описание: {description}\n" +
               $"Общая стоимость: {GetTotalPrice()} руб.";
    }
}

class Seller
{
    private string name;
    private string employeeId;
    protected decimal salary;
    internal string contactInfo;

    private List<Product> products = new List<Product>();

    public string Name { get => name; set => name = value; }
    public string EmployeeId { get => employeeId; set => employeeId = value; }
    public decimal Salary { get => salary; set => salary = value; }

    public List<Product> Products
    {
        get {return products;}
    }

    public Seller(string name, string employeeId,
                  decimal salary, string contactInfo)
    {
        this.name = name;
        this.employeeId = employeeId;
        this.salary = salary;
        this.contactInfo = contactInfo;
    }

    public void AddProduct(Product product)
    {
        if (!products.Contains(product))
        {
            products.Add(product);
            Console.WriteLine($"Товар {product.Name} добавлен продавцу {name}.");
        }
    }

    public void SellProduct(Product product, int quantity)
    {
        if (!products.Contains(product))
        {
            Console.WriteLine("Товар не найден у продавца");
            return;
        }

        if (quantity <= 0)
        {
            Console.WriteLine("Количество должно быть больше нуля");
            return;
        }

        if (product.Quantity >= quantity)
        {
            product.UpdateQuantity(product.Quantity - quantity);
            Console.WriteLine($"Продано {quantity} шт. товара {product.Name}.");
            Console.WriteLine($"Осталось на складе: {product.Quantity} шт.");
        }
        else
        {
            Console.WriteLine("Недостаточно товара на складе");
        }
    }

    public string GetSellerInfo()
    {
        return $"Имя: {name}\n" +
               $"ID сотрудника: {employeeId}\n" +
               $"Зарплата: {salary} руб.\n" +
               $"Контакты: {contactInfo}";
    }
}

class Store
{
    private string storeName;
    private string location;
    public string storeHours;

    private List<Seller> sellers = new List<Seller>();

    public string StoreName { get => storeName; set => storeName = value; }
    public string Location { get => location; set => location = value; }

    public Store(string storeName, string location, string storeHours)
    {
        this.storeName = storeName;
        this.location = location;
        this.storeHours = storeHours;
    }

    public void AddSeller(Seller seller)
    {
        if (!sellers.Contains(seller))
        {
            sellers.Add(seller);
            Console.WriteLine($"Продавец {seller.Name} добавлен в магазин.");
        }
    }

    public void ListProducts()
    {
        Console.WriteLine("\nТОВАРЫ МАГАЗИНА:");

        foreach (Seller seller in sellers)
        {
            foreach (Product product in seller.Products)
            {
                Console.WriteLine(product.GetProductInfo());
            }
        }
    }

    public string GetStoreInfo()
    {
        return $"Название: {storeName}\n" +
               $"Адрес: {location}\n" +
               $"Время работы: {storeHours}\n" +
               $"Количество продавцов: {sellers.Count}";
    }
}

class Program
{
    static void Main()
    {
        // Создание объектов
        Product product = new Product(
            "Видеокарта RTX 3060 Ti",
            35000,
            5,
            "Комплектующие ПК",
            "Видеокарта NVIDIA GeForce RTX 3060 Ti 8 ГБ"
        );

        Seller seller = new Seller(
            "Иван",
            "EMP001",
            45000,
            "+7 999 123-45-67"
        );

        Store store = new Store(
            "ТехноМаркет",
            "г. Тюмень, ул. Республики, 100",
            "10:00 - 21:00"
        );

        Console.WriteLine("ТОВАР:");
        Console.WriteLine(product.GetProductInfo());

        Console.WriteLine("\nПРОДАВЕЦ:");
        Console.WriteLine(seller.GetSellerInfo());

        Console.WriteLine("\nМАГАЗИН:");
        Console.WriteLine(store.GetStoreInfo());

        Console.WriteLine("\nДОБАВЛЕНИЕ:");
        seller.AddProduct(product);

        store.AddSeller(seller);

        Console.WriteLine("\nПРОДАЖА:");
        seller.SellProduct(product, 2);

        store.ListProducts();

        Console.WriteLine("ИТОГ:");
        Console.WriteLine($"Остаток товара: {product.Quantity} шт.");
        Console.WriteLine($"Общая стоимость остатка: {product.GetTotalPrice()} руб.");
    }
}
