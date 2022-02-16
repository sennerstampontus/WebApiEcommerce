using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.Models.Entities
{
    public class CategoryEntity
    {
        public CategoryEntity(string name)
        {
            Name = name;
        }

        [Key]
        public int Id { get; set; }

        [Required, Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        
        public ICollection<ProductEntity> Products { get; set; }
    }
}
