using FoodReservation.Application.Interfaces.Contexts;
using FoodReservation.Application.Interfaces.Users.Commands;
using FoodReservation.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReservation.Application.Services.Users.Commands
{
    public class RemoveUserService: IRemoveUserService
    {
        private readonly IDatabaseContext _databaseContext;
        public RemoveUserService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto Execute(int id)
        {
            var user = _databaseContext.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "کاربر یافت نشد ❌"
                };
            }
            _databaseContext.Users.Remove(user);
            _databaseContext.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message = "کاربر با موفقیت حذف شد ✅"
            };
        }
    }
}
