using System.Text.Json.Serialization;

namespace Services.Scoping.API
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }

        public bool Succeeded { get; set; }

        public string ErrorMessage { get; set; }

        public string SuccessMessage { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool IsNameExist { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool IsCodeExist { get; set; }

        public static ApiResponse<T> Fail(string errorMessage)
        {
            return new ApiResponse<T>
            {
                Succeeded = false,
                ErrorMessage = errorMessage
            };
        }

        public static ApiResponse<T> Success(T data)
        {
            return new ApiResponse<T>
            {
                Succeeded = true,
                Data = data
            };
        }

        public static ApiResponse<T> Success(string SuccessMessage)
        {
            return new ApiResponse<T>
            {
                Succeeded = true,
                SuccessMessage = SuccessMessage
            };
        }

        public static ApiResponse<T> Success(string successMessage, T data)
        {
            return new ApiResponse<T>
            {
                Succeeded = true,
                Data = data,
                SuccessMessage = successMessage
            };
        }

        public static ApiResponse<T> DuplicateCheck(bool isNameExist, string errorMessage)
        {
            return new ApiResponse<T>
            {
                Succeeded = false,
                ErrorMessage = errorMessage,
                IsNameExist = isNameExist
            };
        }
    }
}

