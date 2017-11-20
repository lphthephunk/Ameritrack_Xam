using Ameritrack_Xam.PCL.Models;
using Ameritrack_Xam.PCL.Services.RailsServeDbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ameritrack_Xam.PCL.Helpers
{
    public static class JsonObjectConverter
    {
        public static List<Fault> RailFaultsToFaultList(List<RailFault> railFaults)
        {
            List<Fault> convertedFaultList = new List<Fault>();

            foreach (var railFault in railFaults)
            {
                var fault = new Fault
                {
                    Employee = railFault.ServerEmployeeCredentials,
                    AreaAddress = railFault.ServerAreaAddress,
                    FaultComments = railFault.ServerFaultComments,
                    FaultType = railFault.ServerFaultType,
                    Latitude = railFault.ServerLatitude,
                    Longitude = railFault.ServerLongitude,
                    TrackName = railFault.ServerTrackname,
                    // TODO: add report ID
                };
                if (railFault.IsUrgent == 1)
                {
                    fault.IsUrgent = true;
                }
                else
                {
                    fault.IsUrgent = false;
                }
                convertedFaultList.Add(fault);
            }

            return convertedFaultList;
        }

        public static Employee RailEmployeeToEmployee(RailEmployee railEmployee)
        {
            var employee = new Employee
            {
                EmployeeFirstName = railEmployee.FirstName,
                EmployeeLastName = railEmployee.LastName,
                EmployeeCredentials = railEmployee.Credentials
            };

            return employee;
        }

        public static List<CommonDefects> RailCommonDefectsToCommonDefectsList(List<RailCommonDefects> railCommonDefects)
        {
            List<CommonDefects> convertedCommonDefects = new List<CommonDefects>();

            foreach (var railDefect in railCommonDefects)
            {
                var defect = new CommonDefects
                {
                    DefectName = railDefect.DefectName
                };
                convertedCommonDefects.Add(defect);
            }

            return convertedCommonDefects;
        }

        public static List<Report> RailReportsToReportList(List<RailReport> railReports)
        {
            List<Report> reportList = new List<Report>();

            foreach (var railReport in railReports)
            {
                var rep = new Report
                {
                    Address = railReport.Address,
                    ClientContact = railReport.ClientContact,
                    ClientName = railReport.ClientName,
                    InspectorFirstName = railReport.InspectorFirstName,
                    InspectorLastName = railReport.InspectorLastName,
                    DateTime = railReport.DateAndTime,
                    EmployeeCredentials = railReport.EmployeeCredentials
                };

                reportList.Add(rep);
            }

            return reportList;
        }
    }
}
