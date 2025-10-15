using FoodReservation.Application.Interfaces.Foods.Commands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Application.Interfaces.FacadePatterns.FoodFacade
{
    public interface IFoodsFacade
    {
        IRegisterFoodService RegisterFoodService { get; }
    }
}
