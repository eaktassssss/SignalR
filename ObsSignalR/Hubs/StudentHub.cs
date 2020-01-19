using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using ObsSignalR.Helpers;
using ObsSignalR.Models.Context;

namespace ObsSignalR.Hubs
{
    public class StudentHub : Hub
    {
        public void RefreshStudent()
        {
            using (var context = new ObsContext())
            {
                var list = context.Students.ToList();
                string viewName = @"~/Views/Shared/PartialList.cshtml";
                var studentsScreen = Helper.GetRazorViewAsString(list, viewName);
                Clients.All.reloadStudent(studentsScreen);
            }
        }
    }
}