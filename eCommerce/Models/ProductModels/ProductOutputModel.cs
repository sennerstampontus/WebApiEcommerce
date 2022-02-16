using eCommerce.Models.CategoryModels;

namespace eCommerce.Models.ProductModels
{
    public class ProductOutputModel
    {
        public ProductOutputModel()
        {

        }

        public ProductOutputModel(string articleNumber, string productName, string productDescription, decimal price)
        {
            ArticleNumber = articleNumber;
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
        }

        public ProductOutputModel(string articleNumber, string productName, string productDescription, decimal price, CategoryModel categoryName)
        {
            ArticleNumber = articleNumber;
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
            CategoryName = categoryName;
        }

        public string ArticleNumber { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public CategoryModel CategoryName { get; set; }
       
    }
}
