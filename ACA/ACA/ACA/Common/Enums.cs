using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACA.Common
{
    public class Enums
    {
        public enum SystemConfigurationsKeys
        {
            ServiceURL
        }

        public enum ServiceNames
        {
            Login,
            ChangePassword,
            Lookup,
            sync,
            syncresults

        }

        public enum OrderParams
        {
            asc,
            desc
        }

        public enum LanguageParams
        {
            ar,
            en
        }

        public enum LookupType : int
        {
            SurveyStatus = 1,
            EntityTypes = 2
        }

    }
}
