using FoodReservation.Application.Interfaces.FacadePatterns.DailyFoodFacade;
using Microsoft.AspNetCore.Mvc;

namespace FoodReservationSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DailyFoodController : Controller
    {
        private readonly IDailyFoodFacade _dailyFoodFacade;


        public IActionResult Index()
        {
            return View();
        }
    }
}
