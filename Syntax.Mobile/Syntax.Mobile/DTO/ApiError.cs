using System;
using System.Collections.Generic;
using System.Text;

namespace Syntax.Mobile.DTO
{
    public class ApiError
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public Dictionary<string, List<string>> Errors { get; set; }
    }
}
