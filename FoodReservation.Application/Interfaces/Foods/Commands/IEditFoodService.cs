using FoodReservation.Application.Services.Foods.Commands;
using FoodReservation.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Application.Interfaces.Foods.Commands
{
    public interface IEditFoodService
    {
        ResultDto Execute(EditFoodDto request);
        EditFoodDto GetById(int id);
    }
}
