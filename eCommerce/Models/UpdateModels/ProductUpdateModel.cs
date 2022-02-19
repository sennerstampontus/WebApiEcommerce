namespace eCommerce.Models.UpdateModels
{
    public class ProductUpdateModel
    {
        private string name;
        private string description;
        private decimal price;


        public string Name { get { return name; } set { name = value; } }
        public string Description { get { return description; } set { description = value; } }
        public decimal Price { get { return price; } set { price = value; } }
    }
}
