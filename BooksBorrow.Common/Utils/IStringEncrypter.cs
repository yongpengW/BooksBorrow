using System;
using System.Collections.Generic;
using System.Text;

namespace BooksBorrow.Common.Utils
{
    public interface IStringEncrypter
    {
        string OneWayEncrypt(string text);

        string TwoWayEncrypt(string text);

        string TwoWayDecrypt(string text);
    }
}
