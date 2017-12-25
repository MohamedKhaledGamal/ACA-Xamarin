using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACA.Views.Home
{

    public class ACAMasterPageMenuItem
    {
        public ACAMasterPageMenuItem()
        {
            TargetType = typeof(ACAMasterPageDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}