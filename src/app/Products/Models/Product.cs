namespace Products.Models
{
    /// <summary>
    /// Describes Product Model
    /// </summary>
    public class Product
    {
        public long Id { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Attribute Attribute { get; set; }
    }
    
    public class Attribute
    {
        public Fantastic Fantastic { get; set; }
        public Rating Rating { get; set; }
    }

    public class Fantastic
    {
        public bool Value { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
    }

    public class Rating
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public double Value { get; set; }
    }
}