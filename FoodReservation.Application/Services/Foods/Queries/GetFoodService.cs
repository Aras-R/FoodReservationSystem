using FoodReservation.Application.Interfaces.Contexts;
using FoodReservation.Application.Interfaces.Foods.Queries;
using FoodReservation.Common.Dto;
using FoodReservation.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Application.Services.Foods.Queries
{
    public class GetFoodService: IGetFoodService
    {
        private readonly IDatabaseContext _databaseContext;
        public GetFoodService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto<List<GetFoodDto>> Execute()
        {
            var foods = _databaseContext.Foods
                .Select(x => new GetFoodDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Description = x.Description,
                    Image = x.Image,
                })
                .OrderBy(u =>u.Id)
                .ToList();

            return ResultDto<List<GetFoodDto>>.Success(foods, "لیست کاربران با موفقیت دریافت شد ✅");
        }

    }

    public class GetFoodDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string? Description { get; set; }
        public string? Image {  get; set; }
    }
}
