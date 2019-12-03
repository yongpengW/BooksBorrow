using System;
using System.Collections.Generic;
using System.Text;

namespace BooksBorrow.AppConfiguration
{
    public interface IConfigurationReader
    {
        string ConnectionString
        {
            get;
        }
    }
}
