using FoodReservation.Application.Interfaces.Contexts;
using FoodReservation.Application.Interfaces.Reservations.Commands;
using FoodReservation.Common.Dto;
using FoodReservation.Domain.Entities.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Application.Services.Reservations.Commands
{
    public class RegisterReservationService : IRegisterReservationService
    {
        private readonly IDatabaseContext _databaseContext;
        public RegisterReservationService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public ResultDto Execute(RequestRegisterReservationDto request)
        {
            try
            {
                // بررسی اینکه ورودی معتبر باشد
                if (request.UserId <= 0 || request.DailyFoodId <= 0)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "اطلاعات کاربر یا غذا نامعتبر است."
                    };
                }

                // جلوگیری از رزرو تکراری
                bool alreadyReserved = _databaseContext.Reservations.Any(r =>
                    r.UserId == request.UserId &&
                    r.DailyFoodId == request.DailyFoodId);

                if (alreadyReserved)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "شما قبلاً این غذا را رزرو کرده‌اید!"
                    };
                }

                // ایجاد رزرو جدید
                var reservation = new Reservation
                {
                    UserId = request.UserId,
                    DailyFoodId = request.DailyFoodId,
                    CreatedAt = DateTime.Now,
                    IsPaid = false
                };

                _databaseContext.Reservations.Add(reservation);
                _databaseContext.SaveChanges();

                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "رزرو با موفقیت انجام شد ✅"
                };
            }
            catch (Exception ex)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "خطا در ثبت رزرو: " + ex.Message
                };
            }
        }
    }

    public class RequestRegisterReservationDto
    {
        public int UserId { get; set; }
        public int DailyFoodId { get; set; }
    }
    
}
