using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RazorWorkouts.Model
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<WorkoutSets> WorkoutSets { get; set; }
        public DbSet<Workout> Workout { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<WorkoutSets>()
               .HasMany(dc => dc.Workout)
               .WithOne(d => d.WorkoutSets);
            //modelBuilder.Entity<DocumentCategory>()
            //   .Property(dc => dc.Documents).HasColumnName("DocumentID");

            modelBuilder.Entity<Workout>()
                .HasOne(d => d.WorkoutSets)
                .WithMany(dc => dc.Workout).IsRequired();
        }
    }
}
