using Microsoft.EntityFrameworkCore;
using ShopSphere_API.Entities;

namespace ShopSphere_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> users { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Cart> cart { get; set; }
        public DbSet<CartItem> cartItems { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }
        public DbSet<Payment> payments { get; set; }
    }
}
