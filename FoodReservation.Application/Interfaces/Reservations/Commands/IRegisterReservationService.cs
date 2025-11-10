using FoodReservation.Application.Services.Reservations.Commands;
using FoodReservation.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Application.Interfaces.Reservations.Commands
{
    public interface IRegisterReservationService
    {
        ResultDto Execute(RequestRegisterReservationDto request);

    }
}
