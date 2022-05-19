// See https://aka.ms/new-console-template for more information
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

}

