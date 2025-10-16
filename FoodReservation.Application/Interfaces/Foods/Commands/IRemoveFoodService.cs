using FoodReservation.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Application.Interfaces.Foods.Commands
{
    public interface IRemoveFoodService
    {
        ResultDto Execute(int id);
    }
}
