using Microsoft.AspNetCore.Mvc;

namespace FoodReservationSystem.Controllers
{
    public class AuthenticationController : Controller
    {
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(string StudentNumber, string PassWord) 
        {
            if (StudentNumber == "123" && PassWord == "123")
            {
                return RedirectToAction("Index", "Home");
            }
        ViewBag.Error = "شماره دانشجویی یا رمز اشتباه است!";
        return View();

        }


        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(string StudentNumber, string FullName, string PassWord)
        {
            return RedirectToAction("SignIn");
        }
    }
}
