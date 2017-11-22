using Ameritrack_Xam.PCL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ameritrack_Xam.PCL.Services.RailsServeDbModels
{
    public class RailFault
    {
        [JsonProperty("EmployeeCreds")]
        public string ServerEmployeeCredentials { get; set; }

        [JsonProperty("ReportId")]
        public int? ServerReportId { get; set; }

        [JsonProperty("TrackName")]
        public string ServerTrackname { get; set; }

        [JsonProperty("AreaAddress")]
        public string ServerAreaAddress { get; set; }

        [JsonProperty("FaultComments")]
        public string ServerFaultComments { get; set; }

        [JsonProperty("FaultType")]
        public string ServerFaultType { get; set; }

        [JsonProperty("IsUrgent")]
        public int IsUrgent { get; set; }

        [JsonProperty("Latitude")]
        public double ServerLatitude { get; set; }

        [JsonProperty("Longitude")]
        public double ServerLongitude { get; set; }
    }
}
