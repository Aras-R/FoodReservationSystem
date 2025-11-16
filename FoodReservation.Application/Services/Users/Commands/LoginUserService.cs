using FoodReservation.Application.Interfaces.Contexts;
using FoodReservation.Application.Interfaces.Users.Commands;
using FoodReservation.Common.Dto;
using Store.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Application.Services.Users.Commands
{
    public class LoginUserService: ILoginUserService
    {
        private readonly IDatabaseContext _databaseContext;
        public LoginUserService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto<UserLoginResultDto> Execute(string studentNumber, string password)
        {
            var user = _databaseContext.Users.FirstOrDefault(u => u.StudentNumber == studentNumber);

            if (user == null)
            {
                return ResultDto<UserLoginResultDto>.Fail("شماره دانشجویی یا رمز عبور اشتباه است.");
            }

            var passwordHasher = new PasswordHasher();

            // 🔐 بررسی درست بودن رمز
            bool passwordIsValid = passwordHasher.VerifyPassword(user.PassWord, password);

            if (!passwordIsValid)
            {
                return ResultDto<UserLoginResultDto>.Fail("شماره دانشجویی یا رمز عبور اشتباه است.");
            }

            return ResultDto<UserLoginResultDto>.Success(new UserLoginResultDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Role = user.Role.ToString()
            }, "ورود موفقیت‌آمیز بود.");
        }
    }

    public class UserLoginResultDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
    }
}

