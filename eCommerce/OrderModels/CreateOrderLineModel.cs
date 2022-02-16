using eCommerce.Models.ProductModels;

namespace eCommerce.OrderModels
{
    public class CreateOrderLineModel
    {
        public CreateOrderLineModel()
        {

        }

        private string productArticleNumber;
        private int amount;


        
        public string ProductArticleNumber
        {
            get { return productArticleNumber; }
            set { productArticleNumber = value; }
        }
        public int Amount 
        { 
            get { return amount; } 
            set { amount = value; } 
        }

    }
}
