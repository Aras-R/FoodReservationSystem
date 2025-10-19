using FoodReservation.Application.Services.DailyFoods.Commands;
using FoodReservation.Application.Services.Users.Commands;
using FoodReservation.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Application.Interfaces.DailyFoods.Commands
{
    public interface IRegisterDailyFoodService
    {
        ResultDto Execute(RequestRegisterDailyFoodDto request);
    }
}
