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

        // Foods List
        public IActionResult Index()
        {
            var result = _foodsFacade.GetFoodService.Execute();
            return View(result.Data);
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

        //Foods Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var result = _foodsFacade.EditFoodService.GetById(id);
            if(result == null)
            {
                return Json(new { error = "❌ غذا یافت نشد" });
            }
            return Json(result);
        }
        [HttpPost]
        public IActionResult Edit([FromBody] EditFoodDto request)
        {
            var result = _foodsFacade.EditFoodService.Execute(request);
            return Json(result);
        }

        // Foods Remove
        [HttpPost]
        public IActionResult Remove([FromBody] RemoveFoodDto request)
        {
            var result = _foodsFacade.RemoveFoodService.Execute(request.Id);
            return Json(result);
        }
    }
}
