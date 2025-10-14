namespace FoodReservation.Common.Dto
{
    public class ResultDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "";

        public static ResultDto Fail(string message) =>
            new ResultDto { IsSuccess = false, Message = message };

        public static ResultDto Success(string message) =>
            new ResultDto { IsSuccess = true, Message = message };
    }

    public class ResultDto<T> : ResultDto
    {
        public T? Data { get; set; }

        public static ResultDto<T> Fail(string message) =>
            new ResultDto<T> { IsSuccess = false, Message = message };

        public static ResultDto<T> Success(T data, string message = "") =>
            new ResultDto<T> { IsSuccess = true, Message = message, Data = data };
    }
}
