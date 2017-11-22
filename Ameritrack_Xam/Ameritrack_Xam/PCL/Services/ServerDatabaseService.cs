using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Ameritrack_Xam.PCL.Helpers;
using Ameritrack_Xam.PCL.Interfaces;
using Ameritrack_Xam.PCL.Models;
using Ameritrack_Xam.PCL.Services;
using Ameritrack_Xam.PCL.Services.RailsServeDbModels;
using Newtonsoft.Json;
using Xamarin.Forms;

[assembly: Dependency(typeof(ServerDatabaseService))]

namespace Ameritrack_Xam.PCL.Services
{
    public class ServerDatabaseService : IServerDatabase
    {
        const string URI = "http://96.43.208.21:8090/APICalls/";

        #region CommonDefects

        public async Task<List<CommonDefects>> GetAllCommonDefectsFromServer()
        {
            string uri = URI+"RetrieveCommonDefect.php";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                using (HttpResponseMessage response = await client.GetAsync(uri))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        var defects = JsonConvert.DeserializeObject<List<RailCommonDefects>>(jsonResponse);

                        return JsonObjectConverter.RailCommonDefectsToCommonDefectsList(defects);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine(response.ReasonPhrase);

                        return null;
                    }
                }
            }
        }
        #endregion


        #region Employees

        public async Task<Employee> GetEmployeeFromServer(string _empId)
        {
            string uri = URI+"EmployeeRouter.php?empid="+_empId;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    using (HttpResponseMessage response = await client.GetAsync(uri))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var jsonResponse = await response.Content.ReadAsStringAsync();
                            var employee = JsonConvert.DeserializeObject<List<RailEmployee>>(jsonResponse);

                            return JsonObjectConverter.RailEmployeeToEmployee(employee.FirstOrDefault());
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine(response.ReasonPhrase);

                            return null;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }

        // Note: Shouldn't be a use-case that requires this
        public Task<List<Employee>> GetAllEmployeesFromServer()
        {
            throw new NotImplementedException();
        }
        #endregion


        #region Faults

        public async Task<List<Fault>> GetAllFaultsFromServer()
        {
            string uri = URI+"FaultRouter.php?retrievalType=all";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                using (HttpResponseMessage response = await client.GetAsync(uri))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        var faults = JsonConvert.DeserializeObject<List<RailFault>>(jsonResponse);

                        return JsonObjectConverter.RailFaultsToFaultList(faults);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine(response.ReasonPhrase);

                        return null;
                    }
                }
            }
        }

        public async Task<List<Fault>> GetAllFaultsByAreaFromServer(string _area)
        {
            string uri = URI+"FaultRouter.php?retrievalType=area&area=" + _area;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    using (HttpResponseMessage response = await client.GetAsync(uri))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var jsonResponse = await response.Content.ReadAsStringAsync();
                            var faults = JsonConvert.DeserializeObject<List<RailFault>>(jsonResponse);

                            return JsonObjectConverter.RailFaultsToFaultList(faults);
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine(response.ReasonPhrase);
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public Task<Fault> GetFaultByCoordinatesFromServer(double _latitude, double _longitude)
        {
            throw new NotImplementedException();
        }

        public Task InsertFaultToServer(Fault _fault)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertFaultListToServer(List<Fault> _faultList)
        {
            string uri = "http://96.43.208.21:8090/APICalls/FaultRouter.php";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                using (HttpContent content = new StringContent(JsonConvert.SerializeObject(JsonObjectConverter.FaultToRailFaultList(_faultList))))
                {
                    using (HttpResponseMessage response = await client.PostAsync(uri, content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            return true;
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine(response.ReasonPhrase);
                            return false;
                        }
                    }                    
                }
            }
        }

        public Task InsertFaultPictureToServer(FaultPicture faultPicture)
        {
            throw new NotImplementedException();
        }

        public Task UpdateFaultAtServer(Fault _fault)
        {
            throw new NotImplementedException();
        }

        public Task<List<FaultPicture>> GetFaultPicturesFromServer(int? faultId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Fault>> GetAllFaultsByEmployeeFromServer(string _employeeId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Fault>> GetAllFaultsByReportFromServer(int? _reportId)
        {
            // not yet done on the server side
            throw new NotImplementedException();

            string uri = "http://96.43.208.21:8090/APICalls/FaultRouter.php?retrievalType=reportId&reportId=" + _reportId;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                using (HttpResponseMessage response = await client.GetAsync(uri))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        var faults = JsonConvert.DeserializeObject<List<RailFault>>(jsonResponse);

                        return JsonObjectConverter.RailFaultsToFaultList(faults);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine(response.ReasonPhrase);
                        return null;
                    }
                }
            }
        }

        public Task DeleteFaultFromServer(Fault _fault)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region Reports

        public async Task<bool> InsertReportDataToServer(Report report)
        {
            string uri = URI+"ReportRouter.php";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonReport = new RailReport
                    {
                        Address = report.Address,
                        DateAndTime = report.DateTime,
                        ClientContact = report.ClientContact,
                        ClientName = report.ClientName,
                        EmployeeCredentials = report.EmployeeCredentials,
                        InspectorFirstName = report.InspectorFirstName,
                        InspectorLastName = report.InspectorLastName,
                    };
                    using (HttpContent content = new StringContent(JsonConvert.SerializeObject(jsonReport)))
                    {
                        HttpResponseMessage response = new HttpResponseMessage();
                        response = await client.PostAsync(uri, content);

                        if (response.IsSuccessStatusCode)
                        {
                            return true;
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine(response.ReasonPhrase);
                            return false;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<List<Report>> GetReportsByEmployee(Employee employee)
        {
            string uri = URI+"ReportRouter.php?empid="+employee.EmployeeCredentials;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    using (HttpResponseMessage response = await client.GetAsync(uri))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var jsonResponse = await response.Content.ReadAsStringAsync();
                            var railReportList = JsonConvert.DeserializeObject<List<RailReport>>(jsonResponse);

                            return JsonObjectConverter.RailReportsToReportList(railReportList);
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine(response.ReasonPhrase);
                            return null;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }

        #endregion
    }
}
