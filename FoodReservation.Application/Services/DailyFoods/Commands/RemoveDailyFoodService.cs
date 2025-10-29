using FoodReservation.Application.Interfaces.Contexts;
using FoodReservation.Application.Interfaces.DailyFoods.Commands;
using FoodReservation.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Application.Services.DailyFoods.Commands
{
    public class RemoveDailyFoodService: IRemoveDailyFoodService
    {
        private readonly IDatabaseContext _databaseContext;
        public RemoveDailyFoodService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto Execute(int id)
        {
            var dailyFood = _databaseContext.DailyFoods.FirstOrDefault(x => x.Id == id);
            if(dailyFood == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "برنامه غذایی مورد نظر یافت نشد ❌"
                };
            }
            _databaseContext.DailyFoods.Remove(dailyFood);
            _databaseContext.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message = "برنامه غذایی با موفقیت حذف شد ✅"
            };
        }
    }

    public class RemoveDailyFoodDto
    {
        public int Id { get; set; }
    }
}
