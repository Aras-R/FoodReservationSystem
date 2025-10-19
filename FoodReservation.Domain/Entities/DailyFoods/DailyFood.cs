using FoodReservation.Domain.Enums.MealType;
using FoodReservation.Domain.Enums.WeekDay;
using FoodReservation.Domain.Entities.Reservations;
using FoodReservation.Domain.Entities.Foods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Domain.Entities.DailyFoods
{
    public class DailyFood
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public WeekDay DayOfWeek { get; set; }
        public MealType MealType { get; set; }
        public int? FoodId { get; set; }
        public Food Food { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
         
    }
}
