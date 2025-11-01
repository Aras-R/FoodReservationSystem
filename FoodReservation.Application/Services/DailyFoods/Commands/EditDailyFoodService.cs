using FoodReservation.Application.Interfaces.Contexts;
using FoodReservation.Application.Interfaces.DailyFoods.Commands;
using FoodReservation.Common.Dto;
using FoodReservation.Domain.Entities.Foods;
using FoodReservation.Domain.Enums.MealType;
using FoodReservation.Domain.Enums.WeekDay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Application.Services.DailyFoods.Commands
{
    public class EditDailyFoodService: IEditDailyFoodService
    {
        private readonly IDatabaseContext _databaseContext;
        public EditDailyFoodService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public EditDailyFoodDto GetById(int Id)
        {
            var dailyFood = _databaseContext.DailyFoods.FirstOrDefault(d => d.Id == Id);
            if (dailyFood == null)
            {
                return null;
            }
            var food = _databaseContext.Foods.FirstOrDefault(f => f.Id == dailyFood.FoodId);
            return new EditDailyFoodDto
            {
                Id = dailyFood.Id,
                DayOfWeek = dailyFood.DayOfWeek,
                Date = dailyFood.Date,
                MealType = dailyFood.MealType,
                FoodId = dailyFood.FoodId,
                FoodName = food?.Name ?? "نامشخص"
            };
        }


        public ResultDto Execute(EditDailyFoodDto request)
        {
            try
            {
                var dailyFood = _databaseContext.DailyFoods.FirstOrDefault(d => d.Id == request.Id);
                if (dailyFood == null)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "برنامه‌ی غذایی مورد نظر یافت نشد ❌"
                    };
                }

                dailyFood.DayOfWeek = request.DayOfWeek;
                dailyFood.Date = request.Date;
                dailyFood.MealType = request.MealType;
                dailyFood.FoodId = request.FoodId;

                _databaseContext.SaveChanges();

                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "ویرایش وعده غذایی با موفقیت انجام شد ✅"
                };
            }
            catch (Exception ex)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = $"خطا در ویرایش برنامه‌ی غذایی: {ex.Message}"
                };
            }
        }
    }
    
    

    public class EditDailyFoodDto
    {
        public int Id { get; set; }
        public WeekDay DayOfWeek { get; set; } 
        public DateTime Date { get; set; } 
        public MealType MealType { get; set; } 
        public int? FoodId { get; set; }
        public string FoodName { get; set; }
    }
}
