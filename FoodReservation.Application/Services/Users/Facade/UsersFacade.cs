using FoodReservation.Application.Interfaces.Contexts;
using FoodReservation.Application.Interfaces.FacadePatterns.UserFacade;
using FoodReservation.Application.Interfaces.Users.Commands;
using FoodReservation.Application.Interfaces.Users.Queries;
using FoodReservation.Application.Services.Users.Commands;
using FoodReservation.Application.Services.Users.Queries;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Application.Services.Users.Facade
{
    public class UsersFacade: IUsersFacade
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly IHostingEnvironment _environment;
        public UsersFacade(IDatabaseContext context, IHostingEnvironment hostingEnvironment)
        {
            _databaseContext = context;
            _environment = hostingEnvironment;
        }

        private IRegisterUserService _registerUserService;
        public IRegisterUserService RegisterUserService
        {
            get
            {
                return _registerUserService = _registerUserService ??  new RegisterUserService(_databaseContext);
            }
        }

        private IGetUserService _getUserService;
        public IGetUserService GetUserService
        {
            get
            {
                return _getUserService = _getUserService ?? new GetUserService(_databaseContext);
            }
        }

        private IEditUserService _editUserService;
        public IEditUserService EditUserService
        {
            get
            {
                return _editUserService = _editUserService ?? new EditUserService(_databaseContext);
            }
        }
    }
}
