using FoodReservation.Domain.Entities.Users;
using FoodReservation.Domain.Entities.DailyFoods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Domain.Entities.Reservations
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsPaid { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int DailyFoodId { get; set; }
        public DailyFood  DailyFood { get; set; }
    }
}
