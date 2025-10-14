using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodReservation.Application.Services.Users.Commands;
using FoodReservation.Common.Dto;

namespace FoodReservation.Application.Interfaces.Users.Commands
{
    public interface IRegisterUserService
    {
        ResultDto<ResultRegisterUserDto> Execute(RequestRegisterUserDto request);
    }
}
