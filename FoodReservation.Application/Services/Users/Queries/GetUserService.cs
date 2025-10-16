using FoodReservation.Application.Interfaces.Contexts;
using FoodReservation.Application.Interfaces.Users.Queries;
using FoodReservation.Common.Dto;
using System.Collections.Generic;
using System.Linq;

namespace FoodReservation.Application.Services.Users.Queries
{
    public class GetUserService : IGetUserService
    {
        private readonly IDatabaseContext _databaseContext;
        public GetUserService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ResultDto<List<GetUserDto>> Execute()
        {
            var users = _databaseContext.Users
                .Select(u => new GetUserDto
                {
                    Id = u.Id,
                    StudentNumber = u.StudentNumber,
                    FullName = u.FullName,
                    Role = u.Role.ToString()
                })
                .OrderBy(u => u.Id)
                .ToList();

            return ResultDto<List<GetUserDto>>.Success(users, "لیست کاربران با موفقیت دریافت شد ✅");
        }
    }
    public class GetUserDto
    {
        public int Id { get; set; }
        public string StudentNumber { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
    }
}
