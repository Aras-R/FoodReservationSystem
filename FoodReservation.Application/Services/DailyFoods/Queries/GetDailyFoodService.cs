using FoodReservation.Application.Interfaces.Contexts;
using FoodReservation.Application.Interfaces.DailyFoods.Queries;
using FoodReservation.Common.Dto;
using FoodReservation.Domain.Enums.MealType;
using FoodReservation.Domain.Enums.WeekDay;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Application.Services.DailyFoods.Queries
{
    public class GetDailyFoodService: IGetDailyFoodService
    {
        private readonly IDatabaseContext _databaseContext;
        public GetDailyFoodService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext; 
        }

        public ResultDto<List<DailyFoodListDto>> Execute()
        {
            var data = _databaseContext.DailyFoods
                .Include(d => d.Food)
                .AsEnumerable()
                .Select(d => new DailyFoodListDto
                {
                    Id = d.Id,
                    DayOfWeek = d.DayOfWeek.ToString(),
                    Date = d.Date,
                    MealType = d.MealType.ToString(),
                    FoodId = d.FoodId,
                    FoodName = d.Food != null ? d.Food.Name : "-"
                })
                .OrderBy(d => d.Date)
                .ToList();

            return ResultDto<List<DailyFoodListDto>>.Success(data, "لیست برنامه‌های غذایی با موفقیت دریافت شد ✅");
        }
    }

    public class DailyFoodListDto
    {
        public int Id { get; set; }
        public string DayOfWeek { get; set; }
        public DateTime Date { get; set; }
        public string MealType { get; set; }
        public int? FoodId { get; set; }    
        public string FoodName { get; set; }
    }
}
