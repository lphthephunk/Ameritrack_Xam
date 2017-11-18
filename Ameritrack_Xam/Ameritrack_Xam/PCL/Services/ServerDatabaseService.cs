using Ameritrack_Xam.PCL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ameritrack_Xam.PCL.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Ameritrack_Xam.PCL.Services.RailsServeDbModels;
using Ameritrack_Xam.PCL.Helpers;
using Xamarin.Forms;
using Ameritrack_Xam.PCL.Services;

[assembly: Dependency(typeof(ServerDatabaseService))]

namespace Ameritrack_Xam.PCL.Services
{
    public class ServerDatabaseService : IServerDatabase
    {
        #region CommonDefects

        public async Task<List<CommonDefects>> GetAllCommonDefectsFromServer()
        {
            string uri = string.Empty; // TODO: get actual uri

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = new HttpResponseMessage();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                response = await client.GetAsync(uri);
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
        #endregion


        #region Employees

        public async Task<Employee> GetEmployeeFromServer(string _empId)
        {
            string uri = "http://96.43.208.21:8090/APICalls/EmployeeRouter.php?empid="+_empId;
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

        public Task<List<Employee>> GetAllEmployeesFromServer()
        {
            throw new NotImplementedException();
        }
        #endregion


        #region Faults

        public async Task<List<Fault>> GetAllFaultsFromServer()
        {
            string uri = "http://96.43.208.21:8090/APICalls/FaultRouter.php?retrievalType=all";

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
            string uri = "http://96.43.208.21:8090/APICalls/FaultRouter.php?retrievalType=area&area=" + _area ;

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

        public Task InsertFaultFromServer(Fault _fault)
        {
            throw new NotImplementedException();
        }

        public Task InsertFaultPictureFromServer(FaultPicture faultPicture)
        {
            throw new NotImplementedException();
        }

        public Task UpdateFaultFromServer(Fault _fault)
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

        public Task<List<Fault>> GetAllFaultsByReportFromServer(int? _reportId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFaultFromServer(Fault _fault)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region Reports

        public Task InsertReportDataFromServer(Report report)
        {
            throw new NotImplementedException();
        }

        public Task<Report> GetReportDataFromServer(Report report)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
