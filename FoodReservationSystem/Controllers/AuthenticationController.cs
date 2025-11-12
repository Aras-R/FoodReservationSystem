using FoodReservation.Application.Interfaces.FacadePatterns.UserFacade;
using FoodReservation.Application.Interfaces.Users.Commands;
using FoodReservation.Application.Services.Users.Commands;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodReservationSystem.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUsersFacade _usersFacade;

        public AuthenticationController(IUsersFacade usersFacade)
        {
            _usersFacade = usersFacade;
        }

        // 🟢 GET: /Authentication/SignIn
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        // 🟢 POST: /Authentication/SignIn
        [HttpPost]
        public async Task<IActionResult> SignIn(string studentNumber, string password)
        {
            if (string.IsNullOrWhiteSpace(studentNumber) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "لطفاً تمام فیلدها را پر کنید.";
                return View();
            }

            var result = _usersFacade.LoginUserService.Execute(studentNumber, password);

            if (!result.IsSuccess)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            var user = result.Data;

            // 🔐 ساخت کوکی احراز هویت
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
        }

        // 🟡 GET: /Authentication/SignUp
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        // 🟡 POST: /Authentication/SignUp
        [HttpPost]
        public IActionResult SignUp(string fullName, string studentNumber, string password)
        {
            if (string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(studentNumber) ||
                string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "تمام فیلدها الزامی هستند.";
                return View();
            }

            var result = _usersFacade.SignupUserService.Execute(new RequestSignupUserDto
            {
                FullName = fullName,
                StudentNumber = studentNumber,
                Password = password,
                RePassword = password
            });

            if (!result.IsSuccess)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            TempData["Success"] = "ثبت‌نام با موفقیت انجام شد! لطفاً وارد شوید.";
            return RedirectToAction("SignIn");
        }

        // 🚪 خروج از حساب کاربری
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("SignIn");
        }
    }
}
