
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AmiloBot.util
{
    class StringUtil
    {

        public static bool IsEmpty(string input)
        {
            return input == null || input.Length == 0;
        }

        public static string GetNumericValue(string stringWithNumber)
        {
            return new string(stringWithNumber.Where(c => char.IsDigit(c)).ToArray());
        }
    }
}
