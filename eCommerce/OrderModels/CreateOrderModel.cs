

namespace eCommerce.OrderModels
{
    public class CreateOrderModel
    {
        private int customerId;
        private List<CreateOrderLineModel> line;

        public int CustomerId
        {
            get { return customerId; }
            set { customerId = value; }
        }
        public List<CreateOrderLineModel> Line
        {
            get
            { 
                return line; 
            } 
            set 
            { 
                var _line = new List<CreateOrderLineModel>();
                foreach(var i in value)
                {
                    _line.Add(new CreateOrderLineModel() { ProductArticleNumber = i.ProductArticleNumber, Amount = i.Amount}); 
                };

                line = _line;
            } 
        }
        public int StatusId { get; private set; } = 1;

        public DateTime CreatedDate { get; private set; } = DateTime.Now;
        public DateTime DueDate { get; set; } = DateTime.Today.AddDays(7);
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

       
    }
}
