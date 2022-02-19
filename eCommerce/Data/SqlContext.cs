using eCommerce.Models.Entities;
using eCommerce.Models.Entities.AnnulledEntities;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Data
{
    public class SqlContext : DbContext
    {
        public SqlContext()
        {

        }

        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<OrderLineEntity>()
        //        .HasKey(e => new { e.Id, e.OrderId }).IsClustered(true);
        //}

        public DbSet<AdminEntity> Admins { get; set; }
        public DbSet<AddressEntity> Addresses { get; set; }
        public DbSet<ContactEntity> Contacts { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderLineEntity> OrdersLine { get; set;}
        public DbSet<StatusEntity> Statuses { get; set; }
        public DbSet<AnnulledProductEntity> AnnulledProducts { get; set;}
        public DbSet<AnnulledOrderEntity> AnnulledOrders { get; set;}

    }
}
