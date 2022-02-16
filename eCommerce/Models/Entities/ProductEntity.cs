using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.Models.Entities
{
    public class ProductEntity
    {
        public ProductEntity(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;

            ArticleNumber = $"{Name.Substring(0,3).Trim()}{Guid.NewGuid().ToString().Replace("-", "").Substring(0, 15)}";
        }

        public ProductEntity(string name, string description, decimal price, CategoryEntity category)
        {
            Name = name;
            Description = description;
            Price = price;
            Category = category;

            ArticleNumber = $"{Name.Substring(0, 3).Trim()} {Guid.NewGuid().ToString().Replace("-", "").Substring(0,15)}";
        }

        public ProductEntity(string name, string description, decimal price, int categoryId)
        {
            Name = name;
            Description = description;
            Price = price;
            CategoryId = categoryId;

            ArticleNumber = $"{Name.Substring(0, 3).Trim()} {Guid.NewGuid().ToString().Replace("-", "").Substring(0, 15)}";
        }

        [Key]
        public string ArticleNumber { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public CategoryEntity Category { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set;} = DateTime.Now;
    }
}
