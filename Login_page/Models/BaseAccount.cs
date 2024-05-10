using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Login_page.Models
{
    public class BaseAccount
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool VerifyLogin()
        {
            DataTable dataTable = new DataTable();
            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            SqlConnection sqlConnection=new SqlConnection(ConnString);
            sqlConnection.Open();
            SqlCommand cmd = sqlConnection.CreateCommand();
            cmd.CommandText = "dbo.OSTuserverification";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@UserName",this.UserName));
            cmd.Parameters.Add(new SqlParameter("@Password",this.Password));
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataTable);
            cmd.Dispose();
            sqlConnection.Close();

            //var pdata=(from p in dataTable.AsEnumerable()
            //           where p.Field<string>("Name")==this.UserName && p.Field<string>("Password")==this.Password
            //           select new
            //           {
            //               UserName = p.Field<string>("Name")
            //           }


            //           ).SingleOrDefault();
            //if (pdata != null)
            //{
            //    return true;
            //}
            //return false;

            if (dataTable.Rows.Count>0)
            {
                return true;
            }
            return false; 

        }
    }
}