namespace eCommerce.Models.UpdateModels
{
    public class OrderUpdateModel
    {
        public OrderUpdateModel(int statusId, decimal orderTotal)
        {
            StatusId = statusId;
            OrderTotal = orderTotal;
        }

        public int StatusId { get; set; }
        public decimal OrderTotal { get; set; }
    }
}
