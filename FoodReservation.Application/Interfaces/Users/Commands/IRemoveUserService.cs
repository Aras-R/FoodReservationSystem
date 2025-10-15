using FoodReservation.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Application.Interfaces.Users.Commands
{
    public interface IRemoveUserService
    {
        ResultDto Execute(int id);
    }
}
