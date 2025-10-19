using FoodReservation.Application.Interfaces.DailyFoods.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Application.Interfaces.FacadePatterns.DailyFoodFacade
{
    public interface IDailyFoodFacade
    {
        IRegisterDailyFoodService RegisterDailyFoodService { get; }
    }
}
