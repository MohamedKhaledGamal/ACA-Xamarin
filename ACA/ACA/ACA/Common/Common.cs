using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACA.Common
{
    public class Common
    {
        public static string FormatDate(string DateValue)
        {
            if (DateValue != string.Empty)
            {
                string year = DateValue.Substring(0, 4);
                string month = DateValue.Substring(4, 2);
                string day = DateValue.Substring(6);

                return year + "/" + month + "/" + day;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
