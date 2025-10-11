using FoodReservation.Application.Interfaces.Contexts;
using FoodReservation.Domain.Entities.DailyFoods;
using FoodReservation.Domain.Entities.Foods;
using FoodReservation.Domain.Entities.Reservations;
using FoodReservation.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Persistence.Context
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<DailyFood> DailyFoods { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Reservations)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Food>()
                .HasMany(f => f.DailyFoods)
                .WithOne(df =>df.Food)
                .HasForeignKey(df => df.FoodId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DailyFood>()
                .HasMany(df => df.Reservations)
                .WithOne(r => r.DailyFood)
                .HasForeignKey(r =>r.DailyFoodId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reservation>()
                .Property(r => r.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");





        }

    }
}
