using FoodReservation.Application.Interfaces.Contexts;
using FoodReservation.Application.Interfaces.DailyFoods.Commands;
using FoodReservation.Application.Interfaces.FacadePatterns.DailyFoodFacade;
using FoodReservation.Application.Services.DailyFoods.Commands;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Application.Services.DailyFoods.Facade
{
    public class DailyFoodFacade: IDailyFoodsFacade
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly IHostingEnvironment _environment;
        public DailyFoodFacade(IDatabaseContext databaseContext, IHostingEnvironment environment)
        {
            _databaseContext = databaseContext;
            _environment = environment;
        }

        private IRegisterDailyFoodService _registerDailyFoodService;
        public IRegisterDailyFoodService RegisterDailyFoodService
        {
            get
            {
                return _registerDailyFoodService = _registerDailyFoodService ?? new RegisterDailyFoodService(_databaseContext);
            }
        }
    }
}
