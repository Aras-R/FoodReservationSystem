using FoodReservation.Application.Services.DailyFoods.Queries;
using FoodReservation.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Application.Interfaces.DailyFoods.Queries
{
    public interface IGetDailyFoodService
    {
        ResultDto<List<DailyFoodListDto>> Execute();
    }
}
