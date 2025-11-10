using FoodReservation.Application.Interfaces.Contexts;
using FoodReservation.Application.Interfaces.DailyFoods.Commands;
using FoodReservation.Application.Interfaces.FacadePatterns.Reservation;
using FoodReservation.Application.Interfaces.Reservations.Commands;
using FoodReservation.Application.Services.DailyFoods.Commands;
using FoodReservation.Application.Services.Reservations.Commands;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Application.Services.Reservations.Facade
{
    public class ReservationsFacade:IReservationsFacade
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly IHostingEnvironment _environment;
        public ReservationsFacade(IDatabaseContext databaseContext, IHostingEnvironment environment)
        {
            _databaseContext = databaseContext;
            _environment = environment;
        }

        private IRegisterReservationService _registerReservationService;
        public IRegisterReservationService RegisterReservationService
        {
            get
            {
                return _registerReservationService = _registerReservationService ?? new RegisterReservationService(_databaseContext);
            }
        }
    }
}
