using eCommerce.Models.ProductModels;

namespace eCommerce.Models.ViewModels
{


    public class OrderLineOutputModel
    {
        public OrderLineOutputModel()
        {

        }

        public OrderLineOutputModel(int lineId, string productArticleNumber, string productName, decimal price, int amount)
        {
            LineId = lineId;
            ProductArticleNumber = productArticleNumber;
            ProductName = productName;
            Price = price;
            Amount = amount;
        }

        public int LineId { get; set; }

        public string ProductArticleNumber { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }

    }
}
