using System;
using System.Collections.Generic;
using System.Text;

namespace BooksBorrow.Common.Constants
{
    public class ConfigKey
    {
        public const string PageSize = "PageSize";

        public const int DefaultPageSize = 10;

        public const string RepositoryName = "NETCoreRepository";

        /// <summary>
        /// One day
        /// </summary>
        public const int LoginTokenActiveMinutes = 60 * 24;

        /// <summary>
        /// xxx.temp
        /// </summary>
        public static string Document_Temp_Exname = "temp";

        /// <summary>
        /// xxx.mpg
        /// </summary>
        public static string Document_Exname = "mpg";
    }
}
