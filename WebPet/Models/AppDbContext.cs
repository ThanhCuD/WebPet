using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPet.Models
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {
        }
        public DbSet<Custommer> Custommers { get; set; }
        public DbSet<Animal> Animals { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // configures one-to-many relationship
            modelBuilder.Entity<Animal>()
                .HasOne(_ => _.Owner)
                .WithMany(_ => _.Animals)
                .HasForeignKey(_=>_.OwnerID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
