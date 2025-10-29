using FoodReservation.Application.Services.DailyFoods.Commands;
using FoodReservation.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Application.Interfaces.DailyFoods.Commands
{
    public interface IEditDailyFoodService
    {
        ResultDto Execute(EditDailyFoodDto request);
        EditDailyFoodDto GetById(int id);
    }
}
