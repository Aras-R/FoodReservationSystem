using FoodReservation.Application.Interfaces.Reservations.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Application.Interfaces.FacadePatterns.Reservation
{
    public interface IReservationsFacade
    {
        IRegisterReservationService RegisterReservationService { get; }
    }
}
