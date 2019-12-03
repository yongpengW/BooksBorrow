using System;
using System.Collections.Generic;
using System.Text;

namespace BooksBorrow.Common.Utils
{
    public  class PlainTextEncrypter: IStringEncrypter
    {
        public string OneWayEncrypt(string text)
        {
            return text;
        }

        public string TwoWayEncrypt(string text)
        {
            return text;
        }

        public string TwoWayDecrypt(string text)
        {
            return text;
        }
    }
}
