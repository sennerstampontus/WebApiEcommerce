namespace eCommerce.Models.ViewModels
{
    public class OrderOutputModel
    {
        public OrderOutputModel(CustomerOutputModel customer, ICollection<OrderLineOutputModel> line, StatusOutputModel status)
        {
            Customer = customer;
            Status = status;
            Line = line;
        }

        public OrderOutputModel(int orderId, DateTime createdDate, DateTime dueDate, DateTime updatedDate, CustomerOutputModel customer, ICollection<OrderLineOutputModel> line, decimal orderTotal, StatusOutputModel status)
        {
            OrderId = orderId;
            CreatedDate = createdDate;
            DueDate = dueDate;
            UpdatedDate = updatedDate;
            Customer = customer;
            Line = line;
            Status = status;

            OrderTotal = orderTotal;
        }

        public int OrderId { get; set;}
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; } = DateTime.Today.AddDays(7);
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public CustomerOutputModel Customer { get; set; }
        public StatusOutputModel Status { get; set; }

        public ICollection<OrderLineOutputModel> Line { get; set; }

        public decimal OrderTotal { get; set; }
    }
}
