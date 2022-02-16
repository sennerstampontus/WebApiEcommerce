using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.Models.Entities.AnnulledEntities
{
    public class AnnulledOrderEntity
    {
        public AnnulledOrderEntity()
        {

        }

        public AnnulledOrderEntity(int orderId, int customerId, DateTime createdDate, DateTime annuledDate, decimal orderTotal)
        {
            OrderId = orderId;
            CustomerId = customerId;
            CreatedDate = createdDate;
            AnnuledDate = annuledDate;
            OrderTotal = orderTotal;
        }

        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime AnnuledDate { get; set; } = DateTime.Now;
        [Column(TypeName = ("money"))]
        public decimal OrderTotal { get; set; }
    }
}
