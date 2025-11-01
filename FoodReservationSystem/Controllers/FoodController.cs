using FoodReservation.Application.Interfaces.FacadePatterns.FoodFacade;
using FoodReservation.Application.Interfaces.Foods.Queries;
using FoodReservation.Application.Services.Foods.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FoodReservationSystem.Controllers
{
    public class FoodController : Controller
    {

        //[Area("User")]
        private readonly IFoodsFacade _foodsFacade;
        public FoodController(IFoodsFacade foodsFacade)
        {
            _foodsFacade = foodsFacade;
        }
        public IActionResult List()
        {
            var result = _foodsFacade.GetFoodService.Execute();
            if (!result.IsSuccess)
                TempData["Error"] = result.Message;

            return View(result.Data);
        }
    }
}
