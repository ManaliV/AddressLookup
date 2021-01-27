using System;
using System.Collections.Generic;
using System.Data;

using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Hosting;


namespace AddressLookup.Models
{
    public static class Utility
    {

        public static string GetCodeFromCurrentAddress(string address, string zipCode)
        {
            string pathToExcelFile = HostingEnvironment.MapPath("~/App_Data/Sample_Address.xlsx");
            ConnexionExcel ConxObject = new ConnexionExcel(pathToExcelFile);

            try
            {
                var query = (from p in ConxObject.UrlConnexion.Worksheet<CurrentAddress>("Sheet1")
                             where p.Address.Equals(address) && p.ZipCode.Equals(zipCode)
                             select new
                             {
                                 p.Code
                             }).FirstOrDefault();


                if (query != null)
                    return query.Code.ToString();
                else
                    return string.Empty;
            }
            catch
            {
                return string.Empty;
            }

        }
    }
}