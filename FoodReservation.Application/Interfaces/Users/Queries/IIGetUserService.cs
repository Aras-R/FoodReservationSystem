using FoodReservation.Application.Services.Users.Queries;
using FoodReservation.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Application.Interfaces.Users.Queries
{
    public interface IGetUserService
    {
        ResultDto<List<GetUserDto>> Execute();
    }
}
