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

namespace Ameritrack_Xam.PCL.Services
{
    public class ServerDatabaseService : IDatabaseServices
    {
        #region CommonDefects

        public Task InsertCommonDefects(CommonDefects defects)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CommonDefects>> GetAllCommonDefects()
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

        public Task InsertEmployee(Employee _employee)
        {
            throw new NotImplementedException();
        }

        public Task UpdateEmployee(Employee _employee)
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> GetEmployee(string _empId)
        {
            string uri = string.Empty; // TODO: get the actual uri

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = new HttpResponseMessage();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var employee = JsonConvert.DeserializeObject<RailEmployee>(jsonResponse);

                    return JsonObjectConverter.RailEmployeeToEmployee(employee);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(response.ReasonPhrase);

                    return null;
                }
            }
        }

        public List<Employee> GetAllEmployees()
        {
            throw new NotImplementedException();
        }
        #endregion


        #region Faults

        public async Task<List<Fault>> GetAllFaults()
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

        public Task<Fault> GetFaultByCoordinates(double _latitude, double _longitude)
        {
            throw new NotImplementedException();
        }

        public Task InsertFault(Fault _fault)
        {
            throw new NotImplementedException();
        }

        public Task InsertFaultPicture(FaultPicture faultPicture)
        {
            throw new NotImplementedException();
        }

        public Task UpdateFault(Fault _fault)
        {
            throw new NotImplementedException();
        }

        public Task<List<FaultPicture>> GetFaultPictures(int? faultId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Fault>> GetAllFaultsByArea(string _area)
        {
            throw new NotImplementedException();
        }

        public Task<List<Fault>> GetAllFaultsByEmployee(string _employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Fault>> GetAllFaultsByReport(int? _reportId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFault(Fault _fault)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region Reports

        public Task InsertReportData(Report report)
        {
            throw new NotImplementedException();
        }

        public Task<Report> GetReportData(Report report)
        {
            throw new NotImplementedException();
        }
        #endregion






        #region Ignore
        public Task CreateAllTables()
        {
            throw new NotImplementedException();
        }

        public Task InitDatabase()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
