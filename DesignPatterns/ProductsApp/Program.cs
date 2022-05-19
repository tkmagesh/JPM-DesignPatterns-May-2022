// See https://aka.ms/new-console-template for more information
var products = new Products();
products.Add(new Product { Id = 100, Name = "Pen", Cost = 10, Category = "Stationary" });
products.Add(new Product { Id = 104, Name = "Printer", Cost = 5000, Category = "Electronics" });
products.Add(new Product { Id = 102, Name = "Pencil", Cost = 5, Category = "Stationary" });
products.Add(new Product { Id = 103, Name = "Mouse", Cost = 500, Category = "Electronics" });

products.Print();

//sort the products by id (default)
/*
 * Add provisions for sorting the products by other attributes
 * NOTE : DO NOT use the builtin sort functionality.. write your own
 */

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Cost { get; set; }
    public string Category { get; set; }

    public override string ToString()
    {
        return $"Id={Id}, Name={Name}, Cost={Cost}, Category={Category}";
    }
}

public class Products
{
    private IList<Product> list = new List<Product>();

    public IList<Product> GetAll
    {
        get => list;
    }

    public void Sort()
    {

    }

    public void Add(Product product)
    {
        list.Add(product);
    }

    public void Print()
    {
        foreach (var p in list)
        {
            Console.WriteLine(p);
        }
    }

}


