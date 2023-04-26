using System;
using System.Collections.Generic;
using System.Text;

namespace Syntax.Mobile.DTO
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public ApiError Error { get; set; }

        public ApiResponse(T data)
        {
            Success = true;
            Data = data;
        }

        public ApiResponse(ApiError error)
        {
            Success = false;
            Error = error;
        }
    }
}
