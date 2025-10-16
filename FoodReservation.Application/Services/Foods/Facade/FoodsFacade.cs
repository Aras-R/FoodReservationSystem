using FoodReservation.Application.Interfaces.Contexts;
using FoodReservation.Application.Interfaces.FacadePatterns.FoodFacade;
using FoodReservation.Application.Interfaces.Foods.Commands;
using FoodReservation.Application.Interfaces.Foods.Queries;
using FoodReservation.Application.Services.Foods.Commands;
using FoodReservation.Application.Services.Foods.Queries;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace FoodReservation.Application.Services.Foods.Facade
{
    public class FoodsFacade: IFoodsFacade
    {
        private readonly IDatabaseContext _databaseContext;
        private readonly IHostingEnvironment _environment;
        public FoodsFacade(IDatabaseContext context, IHostingEnvironment hostingEnvironment)
        {
            _databaseContext = context;
            _environment = hostingEnvironment;
        }


        private IRegisterFoodService _registerFoodService;
        public IRegisterFoodService RegisterFoodService
        {
            get
            {
                return _registerFoodService = _registerFoodService ?? new RegisterFoodService(_databaseContext);
            }
        }

        private IGetFoodService _getFoodService;
        public IGetFoodService GetFoodService
        {
            get
            {
                return _getFoodService = _getFoodService ?? new GetFoodService(_databaseContext);
            }
        }

        private IEditFoodService _editFoodService;
        public IEditFoodService EditFoodService
        {
            get
            {
                return _editFoodService = _editFoodService ?? new EditFoodService(_databaseContext);
            }
        }
    }
}
