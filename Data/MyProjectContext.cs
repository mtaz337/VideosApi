using Microsoft.EntityFrameworkCore;
using VideoApi.Data.Entities;

namespace VideoApi.Data
{
    public class MyProjectContext : DbContext
    {
        public MyProjectContext(DbContextOptions<MyProjectContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<VideoFile> VideoFiles { get; set; }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<VideoFile>()
        .HasOne(vf => vf.Product)
        .WithMany(p => p.VideoFiles)
        .HasForeignKey(vf => vf.ProductId)
        .OnDelete(DeleteBehavior.Cascade); // Optional: Set the desired delete behavior
}
    }
}
