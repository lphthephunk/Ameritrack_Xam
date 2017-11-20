using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Ameritrack_Xam.PCL.Services.RailsServeDbModels
{
    public class RailReport
    {
        [JsonProperty("idReport")]
        public int ReportId { get; set; }

        [JsonProperty("EmployeeCreds")]
        public string EmployeeCredentials { get; set; }

        [JsonProperty("ClientName")]
        public string ClientName { get; set; }

        [JsonProperty("ClientContact")]
        public string ClientContact { get; set; }

        [JsonProperty("InspectorFirstName")]
        public string InspectorFirstName { get; set; }

        [JsonProperty("InspectorLastName")]
        public string InspectorLastName { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }
        
        [JsonProperty("DateAndTime")]
        public DateTime DateAndTime { get; set; }
    }
}
