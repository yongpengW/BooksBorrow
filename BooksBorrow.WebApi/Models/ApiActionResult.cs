using BooksBorrow.Domain.Core.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksBorrow.WebApi.Models
{
    public class ApiActionResult
    {

        public bool ok { get; set; } = false;

        public List<BusinessError> errors { get; set; } = new List<BusinessError>();

        /// <summary>
        /// Return data, could be any type of data, such as List<object>, string, etc.
        /// </summary>
        public object data { get; set; }

        public ApiActionResult()
        {
        }

        public ApiActionResult(bool result)
        {
            this.ok = result;
        }
    }
}
