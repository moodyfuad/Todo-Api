using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Responses
{
    /// <summary>
    /// Generic API response wrapper used for all successful/failed responses.
    /// </summary>
    public class ApiResponse<T>
    {
        public bool Successed { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
        public object? Errors { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public ApiResponse() { }

        public ApiResponse(T? data, bool success = true, string? message = null, object? errors = null)
        {
            Successed = success;
            Data = data;
            Message = message;
            Errors = errors;
            Timestamp = DateTime.UtcNow;
        }

        public static ApiResponse<T> Success(T? data = default, string? message = null)
            => new ApiResponse<T>(data, success: true, message: message);

        public static ApiResponse<T> Fail(string? message = null, object? errors = null)
            => new ApiResponse<T>(default, success: false, message: message, errors: errors);
    }
}
