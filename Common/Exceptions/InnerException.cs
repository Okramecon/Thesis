using System;

namespace Common.Exceptions
{
    public class InnerException : Exception
    {
        public string PropertyName { get; set; }

        //guid
        public string ExceptionCode { get; set; }

        public InnerException(string message, string exceptionCode) : base(message)
        {
            ExceptionCode = exceptionCode;
        }
    }
}