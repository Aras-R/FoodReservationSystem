using FoodReservation.Domain.Enums.UserRole;
using FoodReservation.Domain.Entities.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Domain.Entities.Users
{
    public class User
    {
        public int Id { get; set; }
        public string StudentNumber { get; set; }
        public string FullName { get; set; }
        public string PassWord { get; set; }
        public UserRole Role { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
