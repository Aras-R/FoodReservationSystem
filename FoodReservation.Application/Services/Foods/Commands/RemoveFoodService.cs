using FoodReservation.Application.Interfaces.Contexts;
using FoodReservation.Application.Interfaces.Foods.Commands;
using FoodReservation.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Application.Services.Foods.Commands
{
    public class RemoveFoodService: IRemoveFoodService
    {
        private readonly IDatabaseContext _databaseContext;
        public RemoveFoodService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto Execute(int id)
        {
            var food = _databaseContext.Foods.FirstOrDefault(f => f.Id == id);
            if (food == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "غذا یافت نشد ❌"
                };
            }
            _databaseContext.Foods.Remove(food);
            _databaseContext.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message = "غذا با موفقیت حذف شد ✅"
            };
        }
    }

    public class RemoveFoodDto
    {
        public int Id { get; set; }
    }
}
