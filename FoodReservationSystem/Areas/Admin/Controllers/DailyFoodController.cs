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


        //List DailyFoods
        public IActionResult Index()
        {
            var result = _dailyFoodFacade.GetDailyFoodService.Execute();

            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return View(new List<FoodReservation.Application.Services.DailyFoods.Queries.DailyFoodListDto>());
            }

            ViewBag.Foods = _foodFacade.GetFoodService.Execute().Data;

            foreach (var item in result.Data)
            {
                item.DayOfWeek = item.DayOfWeek switch
                {
                    nameof(WeekDay.Saturday) => "شنبه",
                    nameof(WeekDay.Sunday) => "یک‌شنبه",
                    nameof(WeekDay.Monday) => "دوشنبه",
                    nameof(WeekDay.Tuesday) => "سه‌شنبه",
                    nameof(WeekDay.Wednesday) => "چهارشنبه",
                    nameof(WeekDay.Thursday) => "پنج‌شنبه",
                    nameof(WeekDay.Friday) => "جمعه",
                    _ => item.DayOfWeek
                };

                item.MealType = item.MealType switch
                {
                    nameof(MealType.Breakfast) => "صبحانه",
                    nameof(MealType.Lunch) => "ناهار",
                    nameof(MealType.Dinner) => "شام",
                    _ => item.MealType
                };
            }

            return View(result.Data);
        }

        //Register DailyFoods
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

        //Remove DailyFoods
        [HttpPost]
        public IActionResult Remove([FromBody] RemoveDailyFoodDto request)
        {
            var result = _dailyFoodFacade.RemoveDailyFoodService.Execute(request.Id);
            return Json(result);
        }

        // Edit DailyFoods
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = _dailyFoodFacade.EditDailyFoodService.GetById(id);
            if (data == null)
            {
                return Json(new { isSuccess = false, message = "برنامه‌ی غذایی یافت نشد ❌" });
            }

            return Json(new { isSuccess = true, data });
        }


        [HttpPost]
        public IActionResult Edit([FromBody] EditDailyFoodDto request)
        {
            if (request == null || request.Id <= 0)
                return Json(new { isSuccess = false, message = "درخواست نامعتبر است." });

            var result = _dailyFoodFacade.EditDailyFoodService.Execute(request);
            return Json(result);
        }

    }
}
