using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ACA.Common.Enums;

namespace ACA.Common
{
    public class SystemConfig
    {
        #region Properties
        public static string ServiceAPIURL { get { return GetPropertyAPI(SystemConfigurationsKeys.ServiceURL); } }
        public static string ServiceURL { get { return GetProperty(SystemConfigurationsKeys.ServiceURL); } }

        #endregion

        #region Methods

        public static string GetPropertyAPI(SystemConfigurationsKeys _Key)
        {
            string myKey = _Key.ToString();
            //myKey = "http://192.168.0.166:5050/api/";
            myKey = "http://10.3.1.222:9291/api/";
            return myKey;
        }
        public static string GetProperty(SystemConfigurationsKeys _Key)
        {
            string myKey = _Key.ToString();
            //myKey = "http://192.168.0.166:5050/api/survey/";
            myKey = "http://10.3.1.222:9291/api/survey/";
            return myKey;
        }

        #endregion
    }
}
