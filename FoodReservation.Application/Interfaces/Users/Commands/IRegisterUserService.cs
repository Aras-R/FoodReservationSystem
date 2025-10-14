using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodReservation.Application.Services.Users.Commands;
using FoodReservation.Common.Dto;
using static FoodReservation.Application.Services.Users.Commands.RegisterUserService;

namespace FoodReservation.Application.Interfaces.Users.Commands
{
    public interface IRegisterUserService
    {
        ResultDto<ResultRegisterUserDto> Execute(RequestRegisterUserDto request);
    }
}
