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

Console.WriteLine("Filter");
Console.WriteLine("Stationary Products");
var stationaryProducts = products.FilterStationaryProducts();
stationaryProducts.Print();

Console.WriteLine("Costly Products");
var costlyProducts = products.FilterCostlyProducts();
costlyProducts.Print();

//sort the products by id (default)
/*
 * Add provisions for sorting the products by other attributes
 * NOTE : DO NOT use the builtin sort functionality.. write your own
 */


/*
Func<Product, bool> costlyProductPredicate = p => p.Cost > 1000;
Func<Product, bool> stationaryProductPredicate = p => p.Category == "Stationary";
*/
Console.WriteLine("Filter using delegates and delegate composition");
Predicate<Product> costlyProductPredicate = p => p.Cost > 1000;
Predicate<Product> stationaryProductPredicate = p => p.Category == "Stationary";
var cps = products.Filter(costlyProductPredicate);
Console.WriteLine("Costly Products");
cps.Print();

var sps = products.Filter(stationaryProductPredicate);
Console.WriteLine("Stationary Products");
sps.Print();

Predicate<Product> costlyStationaryProductPredicate = new Or(costlyProductPredicate, stationaryProductPredicate).Predicate;
var costlyStationaryProducts = products.Filter(costlyStationaryProductPredicate);
Console.WriteLine("Costly Stationary Products");
costlyStationaryProducts.Print();

Func<Predicate<Product>, Predicate<Product>, Predicate<Product>> OR = (left, right) => (product) => left(product) || right(product);
//public static delegate Func<Predicate<T>, Predicate<T>, Predicate<T>> OR<T> = (T left , T right) => (T item) => left(item) || right(item);

var csps = products.Filter(OR(costlyProductPredicate, stationaryProductPredicate));
Console.WriteLine("Costly Stationary Products [functions]");
csps.Print();

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

    public Products FilterStationaryProducts()
    {
        var result = new Products();
        foreach (var product in list)
        {
            if (product.Category == "Stationary")
            {
                result.Add(product);
            }
        }
        return result;
    }

    public Products FilterCostlyProducts()
    {
        var result = new Products();
        foreach (var product in list)
        {
            if (product.Cost >1000)
            {
                result.Add(product);
            }
        }
        return result;
    }

    public Products Filter(Predicate<Product> predicate)
    {
        var result = new Products();
        foreach (var product in list)
        {
            if (predicate(product))
            {
                result.Add(product);
            }
        }
        return result;
    }
}

public class Or
{
    private readonly Predicate<Product> left;
    private readonly Predicate<Product> right;

    public Predicate<Product> Predicate {
        get => (product) => left(product) || right(product);
    }
    public Or(Predicate<Product> left, Predicate<Product> right)
    {
        this.left = left;
        this.right = right;
    }
}


