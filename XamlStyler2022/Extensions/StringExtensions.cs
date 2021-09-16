using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamlStyler2022.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string self)
        {
            return String.IsNullOrEmpty(self);
        }

        public static bool IsNullOrWhiteSpace(this string self)
        {
            return String.IsNullOrWhiteSpace(self);
        }
    }
}
