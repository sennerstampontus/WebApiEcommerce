namespace eCommerce.Models.UpdateModels
{
    public class ProductUpdateModel
    {
        public ProductUpdateModel(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
