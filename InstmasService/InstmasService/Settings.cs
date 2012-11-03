using System;
using System.Web.Configuration;

namespace InstmasService
{
    public class Settings
    {
        public static DateTime StartDate
        {
            get
            {
                var dateString = WebConfigurationManager.AppSettings["StartDate"].Split('.');
                return new DateTime(int.Parse(dateString[2]), int.Parse(dateString[1]), int.Parse(dateString[0]));
            }
        }

        public static string HashTag { get { return WebConfigurationManager.AppSettings["HashTag"]; } }

        public static string ClientId { get { return WebConfigurationManager.AppSettings["ClientId"]; } }
    }
}