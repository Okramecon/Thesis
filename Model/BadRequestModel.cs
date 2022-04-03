using System.Collections.Generic;

namespace Model
{
    public class BadRequestModel
    {
        /// <summary>
        /// Status
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Error
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Exception Code
        /// </summary>
        public string ExceptionCode { get; set; }
    }
}