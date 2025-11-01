using FoodReservation.Application.Interfaces.Contexts;
using FoodReservation.Application.Interfaces.Foods.Commands;
using FoodReservation.Common.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Application.Services.Foods.Commands
{
    public class EditFoodService: IEditFoodService
    {
        private readonly IDatabaseContext _databaseContext; 
        public EditFoodService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public EditFoodDto GetById(int id)
        {
            var food = _databaseContext.Foods.FirstOrDefault(f => f.Id == id);
            if(food == null)
            {
                return null;
            }
            return new EditFoodDto
            {
                Id = food.Id,
                Name = food.Name,
                Price = food.Price,
                Description = food.Description,
            };
        }

        public ResultDto Execute(EditFoodDto request)
        {
            var food = _databaseContext.Foods.FirstOrDefault(x => x.Id == request.Id);
            if (food == null)
                return new ResultDto { IsSuccess = false, Message = "غذا یافت نشد ❌" };

            food.Name = request.Name;
            food.Price = request.Price;
            food.Description = request.Description;

            if (request.ImageFile != null && request.ImageFile.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(request.ImageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/foods", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    request.ImageFile.CopyTo(stream);
                }

                food.Image = "/uploads/foods/" + fileName;
            }

            _databaseContext.SaveChanges();

            return new ResultDto { IsSuccess = true, Message = "ویرایش غذا با موفقیت انجام شد ✅" };
        }

    }

    public class EditFoodDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? CurrentImageUrl { get; set; }
    }
}
