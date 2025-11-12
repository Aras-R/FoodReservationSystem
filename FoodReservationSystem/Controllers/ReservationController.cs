using FoodReservation.Application.Interfaces.FacadePatterns.DailyFoodFacade;
using FoodReservation.Application.Interfaces.FacadePatterns.Reservation;
using FoodReservation.Application.Services.Reservations.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodReservationSystem.Controllers
{
    [Authorize] // فقط کاربر لاگین‌کرده اجازه دسترسی دارد
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

        // نمایش لیست غذاهای روزانه برای رزرو
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

        // رزرو غذا برای کاربر لاگین کرده
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reserv(int dailyFoodId)
        {
            try
            {
                // خواندن آیدی کاربر از کوکی لاگین‌شده
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userIdString))
                {
                    // این حالت فقط زمانی رخ می‌دهد که کوکی خراب یا منقضی شده باشد
                    TempData["Error"] = "خطا در شناسایی کاربر! لطفاً مجدداً وارد شوید.";
                    return RedirectToAction("Index", "Home");
                }

                int userId = int.Parse(userIdString);

                // ایجاد رزرو
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
