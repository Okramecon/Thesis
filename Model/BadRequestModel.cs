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
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Errors
        /// </summary>
        public IDictionary<string, IEnumerable<string>> Errors { get; set; }

        /// <summary>
        /// Exception Code
        /// </summary>
        public string ExceptionCode { get; set; }
    }
}