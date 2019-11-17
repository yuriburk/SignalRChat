using SignalRChat.Domain.Base.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalRChat.Domain.Results
{
    public class Error
    {
        public ErrorCodes ErrorCode { get; set; }
        public string Message { get; set; }
    }
}
