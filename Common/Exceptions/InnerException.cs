using System;

namespace Common.Exceptions
{
    public class InnerException : Exception
    {
        //guid
        public string ExceptionCode { get; set; }

        public InnerException(string message, string exceptionCode) : base(message)
        {
            ExceptionCode = exceptionCode;
        }
    }
}