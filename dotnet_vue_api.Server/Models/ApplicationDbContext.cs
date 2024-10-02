using Microsoft.EntityFrameworkCore;

namespace dotnet_vue_api.Server.Models
{
	public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
	{

		public DbSet<Product> Products { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Product>()
		   .Property(p => p.Price)
		   .HasPrecision(18, 4);
		}
	}
}
