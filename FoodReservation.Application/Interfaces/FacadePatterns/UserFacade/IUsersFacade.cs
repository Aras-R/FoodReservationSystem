using FoodReservation.Application.Interfaces.Users.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Application.Interfaces.FacadePatterns.UserFacade
{
    public interface IUsersFacade
    {
        IRegisterUserService RegisterUserService { get; } 
    }
}
