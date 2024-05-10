using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Login_page.Models
{
    public class BaseCustomer
    { 
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMObile { get; set; }
       
        public static List<BaseCustomer> ListCustomer()
        {
            List<BaseCustomer> ListCust = new List<BaseCustomer>();
            //DataTable dataTable = new DataTable();

            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            //ApplciationName 
            SqlConnection connection = new SqlConnection(ConnString);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "dbo.OSTListCustomer";
            cmd.Parameters.Clear();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlDataReader mrd = cmd.ExecuteReader();
            if (mrd.HasRows)
            {
                while (mrd.Read())
                {
                    BaseCustomer obj = new BaseCustomer();
                    obj.CustomerID = Convert.ToInt32(mrd["CustomerID"].ToString());
                    obj.CustomerName = mrd["CustomerName"].ToString();
                    obj.CustomerMObile = mrd["CustomerMObile"].ToString();

                    ListCust.Add(obj);
                }
            }

            cmd.Dispose();
            connection.Close();


            
            return ListCust;
        }
        public static DataTable ListCustomer_Equipment()
        {
           // List<BaseCustomer> ListCust = new List<BaseCustomer>();
            DataTable dataTable = new DataTable();

            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            //ApplciationName 
            SqlConnection connection = new SqlConnection(ConnString);
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "dbo.ListCustomer_EquipmentAssignment";
            cmd.Parameters.Clear();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataTable);

            //SqlDataReader mrd = cmd.ExecuteReader();
            //if (mrd.HasRows)
            //{
            //    while (mrd.Read())
            //    {
            //        BaseCustomer obj = new BaseCustomer();
            //        obj.CustomerID = Convert.ToInt32(mrd["CustomerID"].ToString());
            //        obj.CustomerName = mrd["CustomerName"].ToString();
            //        obj.CustomerMObile = mrd["CustomerMObile"].ToString();

            //        ListCust.Add(obj);
            //    }
            //}

            cmd.Dispose();
            connection.Close();



            return dataTable;
        }
        public static int SaveAssignment(int CustomerID,int Equipment_ID,int Equi_Quantity)
        {

            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(ConnString);
            sqlConnection.Open();
            SqlCommand cmd = sqlConnection.CreateCommand();
            cmd.CommandText = "[dbo].[OSTinsEquiAssignemt]";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@CustomerID", CustomerID));
            cmd.Parameters.Add(new SqlParameter("@Equipment_ID", Equipment_ID));
            cmd.Parameters.Add(new SqlParameter("@EquiCount", Equi_Quantity));
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            int returnResult = cmd.ExecuteNonQuery(); 
            cmd.Dispose();
            sqlConnection.Close();
            return returnResult;
        }
    }
}