using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Dtos.AuthDtos
{
    public class RegisterResponseDto
    {
        public RegisterResponseDto(bool success, string? message = null, List<string>? errors = null)
        {
            Success = success;
            Message = message;
            Errors = errors;
        }

        public bool Success { get; set; }
        public string? Message { get; set; } = null;
        public List<string>? Errors { get; set; } = null;
    }
}
