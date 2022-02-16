namespace eCommerce.Models.ProductModels
{
    public class CreateProductModel
    {
        private string productName;
        private string productDescription;
        private decimal price;
        private string categoryName;
        private DateTime createdDate;
        private DateTime updatedDate;


        public string ProductName { get { return productName; } set { productName = value.Trim(); } }
        public string ProductDescription { get { return productDescription; } set { productDescription = value; } }
        public decimal Price { get { return price; } set { price = value; } }
        public string CategoryName { get { return categoryName; } set { categoryName = value.Trim(); } }
        public DateTime CreatedDate { get { return createdDate; } private set { createdDate = DateTime.Now; } }
        public DateTime UpdatedDate { get { return updatedDate;} private set { updatedDate = DateTime.Now; } }
    }
}
