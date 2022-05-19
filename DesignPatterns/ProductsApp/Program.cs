// See https://aka.ms/new-console-template for more information
var products = new Products();
products.Add(new Product { Id = 100, Name = "Pen", Cost = 10, Category = "Stationary" });
products.Add(new Product { Id = 104, Name = "Printer", Cost = 5000, Category = "Electronics" });
products.Add(new Product { Id = 102, Name = "Pencil", Cost = 5, Category = "Stationary" });
products.Add(new Product { Id = 103, Name = "Mouse", Cost = 500, Category = "Electronics" });

products.Print();

Console.WriteLine("Sort (name)");
products.Sort(new ProductComparerByName());
products.Print();

Console.WriteLine("Sort (Cost)");
products.Sort(new ProductComparerByCost());
products.Print();

Console.WriteLine("Sort (Category)");
products.Sort(new ProductComparerByCategory());
products.Print();

Console.WriteLine("Sort (id)");
products.Sort((p1, p2) =>
{
    return p1.Id > p2.Id ? 1 : p1.Id < p2.Id ? -1 : 0;

});
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

public class ProductComparerByName : IProductComparer
{
    public int Compare(Product p1, Product p2)
    {
        return p1.Name.CompareTo(p2.Name);
    }
}

public class ProductComparerByCost : IProductComparer
{
    public int Compare(Product p1, Product p2)
    {
        if (p1.Cost < p2.Cost)
        {
            return -1;
        }
        if (p1.Cost > p2.Cost)
        {
            return 1;
        }
        return 0;
    }
}

public class ProductComparerByCategory : IProductComparer
{
    public int Compare(Product p1, Product p2)
    {
        return p1.Category.CompareTo(p2.Category);
    }
}

public class Products
{
    private IList<Product> list = new List<Product>();

    public IList<Product> GetAll
    {
        get => list;
    }

    public void Sort(IProductComparer comparer)
    {
        for (var i=0; i < list.Count-1; i++)
        {
            for (var j=i+1; j < list.Count; j++)
            {
                if (comparer.Compare(list[i], list[j]) > 0)
                {
                    var temp = list[i];
                    list[i] = list[j];
                    list[j] = temp;
                }
            }
        }
    }

    public void Sort(string attrName)
    {
        for (var i = 0; i < list.Count - 1; i++)
        {
            for (var j = i + 1; j < list.Count; j++)
            {
               if (attrName == "Name")
                {
                    if (list[i].Name.CompareTo(list[j].Name) > 0)
                    {
                        var temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }
               if (attrName == "Cost")
                {
                    if (list[i].Cost > list[j].Cost)
                    {
                        var temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }
               if (attrName == "Category")
                {
                    if (list[i].Category.CompareTo(list[j].Category) > 0)
                    {
                        var temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }
                
            }
        }
    }

    public void Sort(Func<Product, Product, int> compare)
    {
        for (var i = 0; i < list.Count - 1; i++)
        {
            for (var j = i + 1; j < list.Count; j++)
            {
                if (compare(list[i], list[j]) > 0)
                {
                    var temp = list[i];
                    list[i] = list[j];
                    list[j] = temp;
                }
            }
        }
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


