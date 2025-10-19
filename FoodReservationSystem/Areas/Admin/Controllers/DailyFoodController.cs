using FoodReservation.Application.Interfaces.FacadePatterns.DailyFoodFacade;
using FoodReservation.Application.Interfaces.FacadePatterns.FoodFacade;
using FoodReservation.Application.Services.DailyFoods.Commands;
using FoodReservation.Domain.Enums.MealType;
using FoodReservation.Domain.Enums.WeekDay;
using Microsoft.AspNetCore.Mvc;

namespace FoodReservationSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DailyFoodController : Controller
    {
        private readonly IDailyFoodsFacade _dailyFoodFacade;
        private readonly IFoodsFacade _foodFacade;

        public DailyFoodController(IDailyFoodsFacade dailyFoodFacade, IFoodsFacade foodFacade)
        {
            _dailyFoodFacade = dailyFoodFacade;
            _foodFacade = foodFacade;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            // فارسی کردن روزهای هفته
            ViewBag.Days = Enum.GetValues(typeof(WeekDay))
                .Cast<WeekDay>()
                .Select(d => new
                {
                    Value = (int)d,
                    Text = d switch
                    {
                        WeekDay.Saturday => "شنبه",
                        WeekDay.Sunday => "یک‌شنبه",
                        WeekDay.Monday => "دوشنبه",
                        WeekDay.Tuesday => "سه‌شنبه",
                        WeekDay.Wednesday => "چهارشنبه",
                        WeekDay.Thursday => "پنج‌شنبه",
                        WeekDay.Friday => "جمعه",
                        _ => d.ToString()
                    }
                }).ToList();

            // فارسی کردن نوع وعده غذایی
            ViewBag.MealTypes = Enum.GetValues(typeof(MealType))
                .Cast<MealType>()
                .Select(m => new
                {
                    Value = (int)m,
                    Text = m switch
                    {
                        MealType.Breakfast => "صبحانه",
                        MealType.Lunch => "ناهار",
                        MealType.Dinner => "شام",
                        _ => m.ToString()
                    }
                }).ToList();

            // غذاها
            ViewBag.Foods = _foodFacade.GetFoodService.Execute().Data;

            return View();
        }

        [HttpPost]
        public IActionResult Register(RequestRegisterDailyFoodDto request)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { isSuccess = false, message = "لطفاً تمام فیلدها را به درستی پر کنید." });
            }

            var result = _dailyFoodFacade.RegisterDailyFoodService.Execute(request);

            if (result.IsSuccess)
                return Json(new { isSuccess = true, message = "برنامه غذایی با موفقیت ثبت شد ✅" });

            return Json(new { isSuccess = false, message = result.Message });
        }
    }
}
