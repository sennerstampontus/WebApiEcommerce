using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models.Entities
{
    public class StatusEntity
    {
        public StatusEntity(string statusName)
        {
            StatusName = statusName;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string StatusName { get; set; }

        public ICollection<OrderEntity> Orders { get; set; }
    }
}
