using FoodReservation.Application.Services.Users.Commands;
using FoodReservation.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Application.Interfaces.Users.Commands
{
    public interface ILoginUserService
    {
        ResultDto<UserLoginResultDto> Execute(string studentNumber, string password);
    }
}
