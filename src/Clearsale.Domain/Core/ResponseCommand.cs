using Clearsale.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clearsale.Domain.Core
{
    public class ResponseCommand
    {
        public ResponseCommand(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
