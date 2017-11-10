using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ameritrack_Xam.PCL.Services.RailsServeDbModels
{
    public class RailCommonDefects
    {
        [JsonProperty("DefectName")]
        public string DefectName { get; set; }
    }
}
