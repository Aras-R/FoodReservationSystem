using FoodReservation.Application.Interfaces.Contexts;
using FoodReservation.Application.Interfaces.Users.Commands;
using FoodReservation.Common.Dto;
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

            if (user == null || user.PassWord != password)
            {
                return new ResultDto<UserLoginResultDto>
                {
                    IsSuccess = false,
                    Message = "شماره دانشجویی یا رمز عبور اشتباه است."
                };
            }

            return new ResultDto<UserLoginResultDto>
            {
                IsSuccess = true,
                Message = "ورود موفقیت‌آمیز بود.",
                Data = new UserLoginResultDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Role = user.Role.ToString()
                }
            };
        }
    }

    public class UserLoginResultDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
    }
}

