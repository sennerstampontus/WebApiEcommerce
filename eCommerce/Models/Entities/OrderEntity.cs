using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.Models.Entities
{

    public class OrderEntity
    {
        public OrderEntity()
        {

        }

        public OrderEntity(CustomerEntity customer, StatusEntity status)
        {
            Customer = customer;
            Status = status;
        }


        public OrderEntity(CustomerEntity customer, ICollection<OrderLineEntity> orderLine, decimal orderTotal, StatusEntity status)
        {
            Customer = customer;
            Lines = orderLine;
            Status = status;

            OrderTotal = orderTotal;
        }


        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; } = DateTime.Today.AddDays(7);
        public DateTime UpdatedDate { get; set;} = DateTime.Now;

        [Required]
        public int CustomerId { get; set; }
        public CustomerEntity Customer { get; set; }

        public ICollection<OrderLineEntity> Lines { get; set; }

        [Required, Column(TypeName = ("money"))]
        public decimal OrderTotal { get; set; }

        [Required]
        public int StatusId { get; set; }
        public StatusEntity Status { get; set; }
    }
}
