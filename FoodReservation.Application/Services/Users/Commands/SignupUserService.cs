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
    public class SignupUserService : ISignupUserService
    {
        private readonly IDatabaseContext _databaseContext;

        public SignupUserService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto<ResultSignupUserDto> Execute(RequestSignupUserDto request)
        {
            try
            {
                // اعتبارسنجی
                if (string.IsNullOrWhiteSpace(request.StudentNumber))
                    return ResultDto<ResultSignupUserDto>.Fail("شماره دانشجویی الزامی است.");

                if (string.IsNullOrWhiteSpace(request.FullName))
                    return ResultDto<ResultSignupUserDto>.Fail("نام و نام خانوادگی الزامی است.");

                if (string.IsNullOrWhiteSpace(request.Password))
                    return ResultDto<ResultSignupUserDto>.Fail("رمز عبور الزامی است.");

                if (request.Password != request.RePassword)
                    return ResultDto<ResultSignupUserDto>.Fail("رمز عبور و تکرار آن مطابقت ندارند.");

                if (_databaseContext.Users.Any(u => u.StudentNumber == request.StudentNumber))
                    return ResultDto<ResultSignupUserDto>.Fail("این شماره دانشجویی قبلاً ثبت شده است.");

                // هش کردن رمز
                var passwordHasher = new PasswordHasher();
                var hashedPassword = passwordHasher.HashPassword(request.Password);

                // ساخت یوزر جدید
                var user = new User
                {
                    StudentNumber = request.StudentNumber,
                    FullName = request.FullName,
                    PassWord = hashedPassword,
                    Role = UserRole.Student // 🔹 همیشه دانشجو
                };

                _databaseContext.Users.Add(user);
                _databaseContext.SaveChanges();

                return ResultDto<ResultSignupUserDto>.Success(new ResultSignupUserDto
                {
                    UserId = user.Id
                }, "ثبت‌نام با موفقیت انجام شد ✅");
            }
            catch (Exception ex)
            {
                return ResultDto<ResultSignupUserDto>.Fail("خطایی در ثبت‌نام رخ داد: " + ex.Message);
            }
        }
    }

    // 🧾 DTO‌ها
    public class RequestSignupUserDto
    {
        public string StudentNumber { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
    }

    public class ResultSignupUserDto
    {
        public int UserId { get; set; }
    }
}
