using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using AddressLookup.Models;

namespace AddressLookup.Repository
{
    public class AddressRepository
    {
        private SqlConnection con;
        //To Handle connection related activities    
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["getconn"].ToString();
            con = new SqlConnection(constr);

        }
        //To Add Employee details    
        public bool AddAddress(List<AddressModel> addressModels)
        {

            connection();
            //SqlCommand com = new SqlCommand("AddAddress", con);
            //com.CommandType = CommandType.StoredProcedure;
            //com.Parameters.AddWithValue("@", obj.Name);
            //com.Parameters.AddWithValue("@", obj.City);
            //com.Parameters.AddWithValue("@Address", obj.Address);
            
            con.Open();
            foreach (var addressModel in addressModels)
            {
                try
                {
                    string query = "INSERT INTO tbl_my_address VALUES(@Code,@Address,@ZipCode,@Status,@AddressID)";
                    SqlCommand sqlCmd = new SqlCommand(query, con);
                    sqlCmd.Parameters.AddWithValue("@Code", addressModel.Code);
                    sqlCmd.Parameters.AddWithValue("@Address", addressModel.Address);
                    sqlCmd.Parameters.AddWithValue("@ZipCode", addressModel.ZipCode);
                    sqlCmd.Parameters.AddWithValue("@Status", addressModel.Status);
                    sqlCmd.Parameters.AddWithValue("@AddressID", addressModel.AddressID);
                    int result = sqlCmd.ExecuteNonQuery();
                    if(result<=0)
                    {
                        con.Close();
                        return false;
                    }
                }
                catch(Exception e)
                {
                    con.Close();
                    return false;
                }
            }
            con.Close();
            return true;

        }

        private List<AddressModel> GetMatchingAddressHelper(string address, string zipCode, string tableName)
        {
            connection();

            //string tableName = "tbl_all_address";
            List<AddressModel> addressList = new List<AddressModel>();
            string code= Utility.GetCodeFromCurrentAddress(address, zipCode);

            DataTable dbMatchingAddress = new DataTable();
            con.Open();
            string query = "SELECT * FROM " + tableName + " Where Address like @Address And Zipcode=@ZipCode";
            SqlDataAdapter sqlDa = new SqlDataAdapter(query, con);            
            sqlDa.SelectCommand.Parameters.AddWithValue(@"Address", "%"+address+"%" ?? (object)DBNull.Value);
            sqlDa.SelectCommand.Parameters.AddWithValue(@"ZipCode", zipCode ?? (object)DBNull.Value);
            sqlDa.Fill(dbMatchingAddress);

            foreach (DataRow row in dbMatchingAddress.Rows)
            {
                AddressModel addressModel = new AddressModel();
                addressModel.Address = row["Address"].ToString();
                addressModel.ZipCode = row["Zipcode"].ToString();
                addressModel.Status = "N";
                addressModel.Code = code;
                addressModel.AddressID = Convert.ToInt32(row["Id"]);

                


                addressList.Add(addressModel);
            }

            con.Close();
            return addressList;

        }
        
        public List<AddressModel> GetAllMatchingAddress(string address, string zipCode)
        {
            string allAddressTable = "tbl_all_address";
            string subAddressTable = "tbl_sub_address";
            List<AddressModel> allAddressMatchingModels=  GetMatchingAddressHelper(address, zipCode, allAddressTable);
            List<AddressModel> subAddressMatchingModels= GetMatchingAddressHelper(address, zipCode, subAddressTable);

            allAddressMatchingModels.AddRange(subAddressMatchingModels);

            return allAddressMatchingModels;

            //Using Procedure
            //SqlCommand com = new SqlCommand("GetAddress", con);
            //com.CommandType = CommandType.StoredProcedure;
            //SqlDataAdapter da = new SqlDataAdapter(com);
            //DataTable dt = new DataTable();

            //con.Open();
            //da.Fill(dt);
            //con.Close();
            ////Bind EmpModel generic list using dataRow     
            //foreach (DataRow dr in dt.Rows)
            //{

            //    addressList.Add(

            //        new AddressModel
            //        {

            //            Address = Convert.ToString(dr["Address"]),
            //            ZipCode = Convert.ToString(dr["ZipCode"]),
            //            Status = Convert.ToString(dr["STatus"]),
            //            AddressID = Convert.ToInt32(dr["AddressID"])

            //        }
            //        );
            //}


            //return addressList;
        }
        //To Update Employee details    
        public bool UpdateAddress()
        {

            connection();
            con.Open();

            string query= "update tbl_my_address set Status =@Status where Address in ( select Address from tbl_my_address group by Address  having count(ZipCode) =1)";
            SqlCommand sqlCmd = new SqlCommand(query, con);
            sqlCmd.Parameters.AddWithValue("@Status", "Y");
            int result=sqlCmd.ExecuteNonQuery();           
            con.Close();
            if (result >= 1)
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteAddress()
        {
            connection();
            con.Open();
            con.Close();
            return true;
        }
    }
}