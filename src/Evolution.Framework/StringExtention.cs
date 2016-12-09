using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evolution.Framework
{
    public static class StringExtention
    {
        public static byte[] ToBytes(this string s)
        {
            return System.Text.Encoding.Unicode.GetBytes(s);
        }
    }
}
