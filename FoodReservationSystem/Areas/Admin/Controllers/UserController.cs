using FoodReservation.Application.Interfaces.FacadePatterns.UserFacade;
using FoodReservation.Application.Services.Users.Commands;
using Microsoft.AspNetCore.Mvc;

namespace FoodReservationSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUsersFacade _usersFacade;

        public UserController(IUsersFacade usersFacade)
        {
            _usersFacade = usersFacade;
        }

        public IActionResult Index()
        {
            return View();
        }

        // ✅ نمایش فرم ثبت کاربر
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // ✅ ثبت کاربر جدید
        [HttpPost]
        public IActionResult Register(RequestRegisterUserDto request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "لطفاً تمام فیلدها را کامل کنید.";
                return View(request);
            }

            var result = _usersFacade.RegisterUserService.Execute(request);

            ViewBag.Message = result.Message;

            if (result.IsSuccess)
            {
                ModelState.Clear();
            }

            return View();
        }
    }
}
