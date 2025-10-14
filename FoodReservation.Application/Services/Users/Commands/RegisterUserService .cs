using FoodReservation.Application.Interfaces.Contexts;
using FoodReservation.Application.Interfaces.Users.Commands;
using FoodReservation.Common.Dto;
using FoodReservation.Domain.Entities.Users;
using FoodReservation.Domain.Enums.UserRole;
using Store.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Application.Services.Users.Commands
{
    public class RegisterUserService : IRegisterUserService
    {
        private readonly IDatabaseContext _databaseContext;
        public RegisterUserService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto<ResultRegisterUserDto> Execute(RequestRegisterUserDto request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.StudentNumber))
                    return ResultDto<ResultRegisterUserDto>.Fail("شماره دانشجویی الزامی است.");

                if (string.IsNullOrWhiteSpace(request.FullName))
                    return ResultDto<ResultRegisterUserDto>.Fail("نام و نام خانوادگی الزامی است.");

                if (string.IsNullOrWhiteSpace(request.Password))
                    return ResultDto<ResultRegisterUserDto>.Fail("رمز عبور الزامی است.");

                if(request.Password != request.RePassword)
                    return ResultDto<ResultRegisterUserDto>.Fail("رمز عبور و تکرار آن یکسان نیست.");

                if(_databaseContext.Users.Any(u => u.StudentNumber == request.StudentNumber))
                    return ResultDto<ResultRegisterUserDto>.Fail("این شماره دانشجویی قبلاً ثبت شده است.");

                var passwordHasher = new PasswordHasher();
                var hashedPassword = passwordHasher.HashPassword(request.Password);

                var user = new User
                {
                    StudentNumber = request.StudentNumber,
                    FullName = request.FullName,
                    PassWord = request.Password,
                    Role = request.Role
                };

                _databaseContext.Users.Add(user);
                _databaseContext.SaveChanges();

                return ResultDto<ResultRegisterUserDto>.Success(new ResultRegisterUserDto
                {
                    UserId = user.Id
                }, "کاربر با موفقیت ثبت شد ✅");
            }
            catch (Exception ex)
            {
                return ResultDto<ResultRegisterUserDto>.Fail("خطایی رخ داد: " + ex.Message);
            }
        }


        public class RequestRegisterUserDto
        {
            public string StudentNumber { get; set; }
            public string FullName { get; set; }
            public string Password { get; set; }
            public string RePassword { get; set; }
            public UserRole Role { get; set; } = UserRole.Student;
        }

        public class ResultRegisterUserDto
        {
            public int UserId { get; set; }
        }
    }
}
