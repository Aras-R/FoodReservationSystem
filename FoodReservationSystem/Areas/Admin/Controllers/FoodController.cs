using Azure.Core;
using FoodReservation.Application.Interfaces.FacadePatterns.FoodFacade;
using FoodReservation.Application.Interfaces.Foods.Commands;
using FoodReservation.Application.Services.Foods.Commands;
using Microsoft.AspNetCore.Mvc;

namespace FoodReservationSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FoodController : Controller
    {
        private readonly IFoodsFacade _foodsFacade;
        public FoodController(IFoodsFacade foodsFacade)
        {
            _foodsFacade = foodsFacade;
        }


        public IActionResult Index()
        {
            return View();
        }

        // Foods Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RequestRegisterFoodDto request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "لطفاً اطلاعات را به درستی وارد کنید ❗";
                return View(request);
            }

            var result = _foodsFacade.RegisterFoodService.Execute(request);
            ViewBag.Massage = result.Message;
            if (result.IsSuccess)
            {
                ModelState.Clear();
            }

            return View();
        }
    }
}
