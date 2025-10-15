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

        //List of users
        public IActionResult Index()
        {
            var result = _usersFacade.GetUserService.Execute();
            return View(result.Data);
        }


        //Users register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

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


        //Users Edit
        [HttpPost]
        public IActionResult Edit([FromBody] EditUserDto request) 
        {
            var result = _usersFacade.EditUserService.Execute(request);
            return Json(result);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _usersFacade.EditUserService.GetById(id);
            if (user == null)
                return Json(new { error = "❌ کاربر یافت نشد" });

            return Json(user);
        }


        //Users Remove
        [HttpPost]
        public IActionResult Remove([FromBody] RemoveUserDto request)
        {
            var result = _usersFacade.RemoveUserService.Execute(request.Id);
            return Json(result);
        }
    }

}

