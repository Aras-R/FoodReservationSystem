using FoodReservation.Application.Interfaces.Contexts;
using FoodReservation.Application.Interfaces.DailyFoods.Commands;
using FoodReservation.Common.Dto;
using FoodReservation.Domain.Entities.DailyFoods;
using FoodReservation.Domain.Enums.MealType;
using FoodReservation.Domain.Enums.WeekDay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Application.Services.DailyFoods.Commands
{
    public class RegisterDailyFoodService: IRegisterDailyFoodService
    {
        private readonly IDatabaseContext _databaseContext;
        public RegisterDailyFoodService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto Execute(RequestRegisterDailyFoodDto request)
        {
            try
            {
                if (request.FoodId <= 0)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "لطفاً غذا را انتخاب کنید."
                    };
                }

                var existis = _databaseContext.DailyFoods.Any(df =>
                    df.Date.Date == request.Date.Date &&
                    df.MealType == request.MealType);
                if (existis)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "برای این روز و وعده قبلاً غذا ثبت شده است!"
                    };
                }

                var dailyFood = new DailyFood
                {
                    Date = request.Date,
                    DayOfWeek = request.DayOfWeek,
                    MealType = request.MealType,
                    FoodId = request.FoodId,
                };

                _databaseContext.DailyFoods.Add(dailyFood);
                _databaseContext.SaveChanges();

                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "برنامه غذایی با موفقیت ثبت شد ✅"
                };
                
            }
            catch (Exception ex)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "خطا در ثبت برنامه غذایی: " + ex.Message
                };
            }
        }

    }

    public class RequestRegisterDailyFoodDto()
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public WeekDay DayOfWeek { get; set; }
        public MealType MealType { get; set; }
        public int? FoodId { get; set; }

    }
}
