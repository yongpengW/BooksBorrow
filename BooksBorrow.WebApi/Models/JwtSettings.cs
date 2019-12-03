using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksBorrow.WebApi.Models
{
    public class JwtSettings
    {
        /// <summary>
        /// Who issuer this token
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Which platform can use this token
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Secret key seed
        /// </summary>
        public string SecretKey { get; set; }
    }
}
