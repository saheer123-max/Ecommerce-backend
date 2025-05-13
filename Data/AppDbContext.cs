using Microsoft.EntityFrameworkCore;
using WeekFive.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Users> Users { get; set; } // ✅ Corrected: singular name changed to plural
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Wishlist> Wishlists { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Order and Payment relationship (One-to-One) ✅ Fixed cascade issue
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Payments)
            .WithOne(p => p.Order)
            .HasForeignKey<Payment>(p => p.OrderId)
            .OnDelete(DeleteBehavior.Restrict); // ✅ Prevents cascade conflict

        // Order and OrderDetails relationship (One-to-Many)
        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderDetails)
            .WithOne(od => od.Order)
            .HasForeignKey(od => od.OrderId)
            .OnDelete(DeleteBehavior.Cascade); // ✅ OK: when order deletes, details also delete

        // Product price precision
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);

        // Users Email uniqueness
        modelBuilder.Entity<Users>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // Payment status conversion to string (e.g., Enum → string)
        modelBuilder.Entity<Payment>()
            .Property(p => p.Status)
            .HasConversion<string>();

        // Order and User relationship
        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany()
            .HasForeignKey(o => o.UserId);

        // OrderDetail and Product relationship
        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Product)
            .WithMany()
            .HasForeignKey(od => od.ProductId);
    }
}
