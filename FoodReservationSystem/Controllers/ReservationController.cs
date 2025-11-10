using FoodReservation.Application.Interfaces.FacadePatterns.DailyFoodFacade;
using FoodReservation.Application.Interfaces.FacadePatterns.Reservation;
using FoodReservation.Application.Services.Reservations.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodReservationSystem.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationsFacade _reservationsFacade;
        private readonly IDailyFoodsFacade _dailyFoodsFacade;

        public ReservationController(
            IReservationsFacade reservationsFacade,
            IDailyFoodsFacade dailyFoodsFacade)
        {
            _reservationsFacade = reservationsFacade;
            _dailyFoodsFacade = dailyFoodsFacade;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Reserv()
        {
            var dailyFoods = _dailyFoodsFacade.GetDailyFoodService.Execute();

            if (!dailyFoods.IsSuccess)
            {
                TempData["Error"] = dailyFoods.Message;
                return View(new List<FoodReservation.Application.Services.DailyFoods.Queries.DailyFoodListDto>());
            }

            return View(dailyFoods.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reserv(int dailyFoodId)
        {
            try
            {
                // گرفتن آیدی کاربر لاگین شده از سیستم احراز هویت
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userIdString))
                {
                    TempData["Error"] = "لطفاً ابتدا وارد حساب کاربری خود شوید.";
                    return RedirectToAction("Login", "Account");
                }

                int userId = int.Parse(userIdString);

                var result = _reservationsFacade.RegisterReservationService.Execute(
                    new RequestRegisterReservationDto
                    {
                        UserId = userId,
                        DailyFoodId = dailyFoodId
                    });

                if (result.IsSuccess)
                {
                    TempData["Message"] = result.Message;
                }
                else
                {
                    TempData["Error"] = result.Message;
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "خطا در رزرو غذا: " + ex.Message;
            }

            return RedirectToAction(nameof(Reserv));
        }
    }
}
