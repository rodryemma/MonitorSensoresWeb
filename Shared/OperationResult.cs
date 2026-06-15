using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class OperationResult<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public static OperationResult<T> Ok(T data, string? message = null)
        {
            return new OperationResult<T> { Success = true, Data = data, Message = message };
        }

        public static OperationResult<T> Fail(string message)
        {
            return new OperationResult<T> { Success = false, Message = message };
        }
    }
}
