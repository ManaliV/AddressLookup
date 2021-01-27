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
            string code = string.Empty;

            string pathToExcelFile = HostingEnvironment.MapPath("~/App_Data/Sample_Address.xlsx");
            ConnexionExcel ConxObject = new ConnexionExcel(pathToExcelFile);
            
            var query = (from p in ConxObject.UrlConnexion.Worksheet<CurrentAddress>("Sheet1")
                         where p.Address.Equals(address) && p.ZipCode.Equals(zipCode)
                         select new
                         {
                             p.Code                             
                         }).FirstOrDefault();
            

            
            return query.Code.ToString();

            //string code = string.Empty;
            //string pathCurrentAddress = HostingEnvironment.MapPath("~/App_Data/Sample_Address.xlsx");

            //WorkBook wb = WorkBook.Load(pathCurrentAddress);
            //DataSet dataSet = wb.ToDataSet();
            //DataTable currentTable = dataSet.Tables[0];

            //DataRow drow = currentTable.AsEnumerable().Where(p => p.Field<string>(2) == address && p.Field<string>(3)==zipCode).FirstOrDefault();
            
            //return code;

            //Microsoft.ACE.OLEDB.15.0

            //string path = HostingEnvironment.MapPath("~/App_Data/Sample_Address.xlsx");
            //string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0;";

             //// string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + HostingEnvironment.MapPath("~/App_Data/Sample_Address.xlsx") + ";Extended Properties='Excel 12.0;HDR=NO';"; //for above excel 2007 
            //// string connString = "Provider=Microsoft.ACE.OLEDB.15.0;Data Source=" + HostingEnvironment.MapPath("~/App_Data/Sample_Address.xlsx") + ";Extended Properties='Excel 12.0;HDR=NO';"; //for above excel 2007 
            //OleDbConnection oledbConn = new OleDbConnection(connString);
            //    DataTable codeDataTable = new DataTable();
            //    try
            //    {

            //        oledbConn.Open();
            //        string query = String.Format("SELECT * FROM [Sheet1$] where Address={0} And ZipCode={1}", address, zipCode);
            //        using (OleDbCommand cmd = new OleDbCommand(query, oledbConn))
            //        {
            //            OleDbDataAdapter oleda = new OleDbDataAdapter();
            //            oleda.SelectCommand = cmd;
            //            DataSet codeDataSet = new DataSet();
            //            oleda.Fill(codeDataSet);

            //            codeDataTable = codeDataSet.Tables[0];
            //        }
            //    }
            //    catch(Exception e)
            //    {
            //    }
            //    finally
            //    {

            //        oledbConn.Close();
            //    }

            //    return codeDataTable;
            //}

        }
    }
}