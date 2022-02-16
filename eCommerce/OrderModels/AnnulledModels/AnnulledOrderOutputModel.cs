namespace eCommerce.OrderModels
{
    public class AnnulledOrderOutputModel
    {
        public AnnulledOrderOutputModel()
        {

        }

        public AnnulledOrderOutputModel(int orderId, int customerId, DateTime createdDate, DateTime annuledDate, decimal orderTotal)
        {
            OrderId = orderId;
            CustomerId = customerId;
            CreatedDate = createdDate;
            AnnuledDate = annuledDate;
            OrderTotal = orderTotal;
        }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime AnnuledDate { get; set; } = DateTime.Now;
        public decimal OrderTotal { get; set; }
    }
}
