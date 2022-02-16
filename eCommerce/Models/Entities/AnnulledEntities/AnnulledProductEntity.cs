using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.Models.Entities.AnnulledEntities
{
    public class AnnulledProductEntity
    {
        public AnnulledProductEntity()
        {

        }

        public AnnulledProductEntity(string articleNumber, string name, string description, decimal price)
        {
            ArticleNumber = articleNumber;
            Name = name;
            Description = description;
            Price = price;
        }

        [Key]
        public int Id { get; set; }
        public string ArticleNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName = ("money"))]
        public decimal Price { get; set; }
    }
}
