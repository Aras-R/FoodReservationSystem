using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodReservation.Domain.Entities.DailyFoods;
using FoodReservation.Domain.Entities.Foods;
using FoodReservation.Domain.Entities.Reservations;
using FoodReservation.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace FoodReservation.Application.Interfaces.Contexts
{
    public interface IDatabaseContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Food> Foods { get; set; }
        DbSet<DailyFood> DailyFoods { get; set; }
        DbSet<Reservation> Reservations { get; set; }

        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());

    }
}
