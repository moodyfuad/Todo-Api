using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.OperationResults
{
    public class OperationResult
    {
        public OperationResult() { }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }

        public static OperationResult Success()
        {
            return new OperationResult { IsSuccess = true};
        }
        public static OperationResult Failure(string? errorMessage = default)
        {
            return new OperationResult { IsSuccess = false, ErrorMessage = errorMessage };
        }

    }
}
