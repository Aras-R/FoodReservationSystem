using FoodReservation.Application.Interfaces.Contexts;
using FoodReservation.Application.Interfaces.Users.Commands;
using FoodReservation.Common.Dto;
using FoodReservation.Domain.Enums.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Application.Services.Users.Commands
{
    public class EditUserService: IEditUserService
    {
        private readonly IDatabaseContext _databaseContext;
        public EditUserService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public EditUserDto GetById(int id) 
        {
            var user = _databaseContext.Users.FirstOrDefault(x => x.Id == id);
            if(user == null)
            {
                return null;
            }
            return new EditUserDto
            {
                Id = user.Id,
                StudentNumber = user.StudentNumber,
                FullName = user.FullName,
                Role = user.Role,
            };
        }

        public ResultDto Execute(EditUserDto request)
        {
            var user = _databaseContext.Users.FirstOrDefault(x => x.Id == request.Id);
            if(user == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد ❌"
                };
            }

            user.StudentNumber = request.StudentNumber;
            user.FullName = request.FullName;
            user.Role = request.Role;

            _databaseContext.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message = "ویرایش کاربر با موفقیت انجام شد ✅"
            };
        }
    }

    public class EditUserDto
    {
        public int Id { get; set; }
        public string StudentNumber { get; set; }
        public string FullName { get; set; }
        public UserRole Role { get; set; }
    }
}
