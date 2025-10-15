﻿using FoodReservation.Application.Interfaces.Contexts;
using FoodReservation.Application.Interfaces.Foods.Commands;
using FoodReservation.Common.Dto;
using FoodReservation.Domain.Entities.Foods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Application.Services.Foods.Commands
{
    public class RegisterFoodService: IRegisterFoodService
    {
        private readonly IDatabaseContext _databaseContext;
        public RegisterFoodService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto Execute(RequestRegisterFoodDto request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "نام غذا الزامی است ❌"
                    };
                }
                if (request.Price <= 0)
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "قیمت باید بیشتر از صفر باشد ❌"
                    };
                }

                var food = new Food
                {
                    Name = request.Name.Trim(),
                    Price = request.Price,
                    Description = request.Description,
                };

                _databaseContext.Foods.Add(food);
                _databaseContext.SaveChanges();

                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "غذا با موفقیت ثبت شد ✅"
                };
            }
            catch (Exception ex) 
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = $"خطا در ثبت غذا: {ex.Message}"
                };
            }
        }


    }

    public class RequestRegisterFoodDto
    {
        public string Name { get; set; }
        public int Price { get; set; }   
        public string? Description { get; set; }  

    }

}
