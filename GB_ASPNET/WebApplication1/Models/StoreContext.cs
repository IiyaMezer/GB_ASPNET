using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public class StoreContext : DbContext
{
    public DbSet<Storage> ProductStorage { get; set;}
    public DbSet<Product> Products { get; set;}
    public DbSet<Group> ProductGroups { get; set;}

    public StoreContext(DbContextOptions<StoreContext> dbContext) : base(dbContext)
    {

    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.; Database=GB;Integrated Security=False;TrustServerCertificate=True; Trusted_Connection=True;").UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");

            entity.HasKey(x => x.Id).HasName("ProductId");
            entity.HasIndex(x => x.Name).IsUnique();

            entity.Property(e=>e.Name).
            HasColumnName("ProdName").
            HasMaxLength(255).
            IsRequired();

            entity.Property(e=> e.Description).
            HasColumnName("Description").
            HasMaxLength(255).
            IsRequired();

            entity.Property(e => e.Price).
            HasColumnName("Cost").
            IsRequired();

            entity.HasOne(x => x.Group).
            WithMany(c=> c.Products).
            HasForeignKey(x=>x.Id).
            HasConstraintName("GroupToProduct");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.ToTable("ProductGroups");

            entity.HasKey(x => x.Id).HasName("GroupId");
            entity.HasIndex(x => x.Name).IsUnique();

            entity.Property(e => e.Name).
            HasColumnName("ProdName").
            HasMaxLength(255).
            IsRequired();

        });


        modelBuilder.Entity<Storage>(entity =>
        {
            entity.ToTable("Storage");

            entity.HasKey(x => x.Id).HasName("StoreId");            

            entity.Property(e => e.Name).
            HasColumnName("StorageName");

            entity.Property(e => e.Count).
            HasColumnName("ProductCount");

            entity.HasMany(x => x.Products)
                .WithMany(b => b.Storages)
                .UsingEntity(j => j.ToTable("StorageProduct"));


        });
    }
}
