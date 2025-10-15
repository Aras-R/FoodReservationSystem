﻿using FoodReservation.Application.Services.Users.Commands;
using FoodReservation.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Application.Interfaces.Users.Commands
{
    public interface IEditUserService
    {
        ResultDto Execute(EditUserDto request);
        EditUserDto GetById(int id);
    }
}
