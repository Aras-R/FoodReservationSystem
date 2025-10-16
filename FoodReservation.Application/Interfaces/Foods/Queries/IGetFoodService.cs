using FoodReservation.Application.Services.Foods.Queries;
using FoodReservation.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Application.Interfaces.Foods.Queries
{
    public interface IGetFoodService
    {
        ResultDto<List<GetFoodDto>> Execute();
    }
}
