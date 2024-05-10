using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;


namespace Login_page.Models
{
    [Serializable]
    public class BaseEquipment
    {
        [DataMember]
        public int Equipment_ID { get; set; }
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int EcCount{ get; set; }
        [DataMember]   
        public int Stock { get; set; }

        [DataMember]
        public DateTime EntryDate { get; set;}
    
        public List<BaseEquipment> ListEquipment {  get; set; }
        public BaseEquipment()
        {
            ListEquipment = new List<BaseEquipment>();
        }
        public static List<BaseEquipment> ListEquipmentData()
        {
            List<BaseEquipment> plsData = new List<BaseEquipment>();
            //DataTable dataTable = new DataTable();

            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            //ApplciationName 
            SqlConnection connection = new SqlConnection(ConnString);
           connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "dbo.ostlistEquipment";
            cmd.Parameters.Clear();
           
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            SqlDataReader mrd = cmd.ExecuteReader();
            if (mrd.HasRows)
            {
                while (mrd.Read())
                {
                    BaseEquipment obj = new BaseEquipment();
                    obj.Equipment_ID = Convert.ToInt32(mrd["Equipment_ID"].ToString());
                    obj.Name = mrd["EquipmentName"].ToString();
                    obj.EcCount = Convert.ToInt16(mrd["Quantity"].ToString());
                    //obj.Stock = Convert.ToInt16(mrd["Stock"].ToString());
                    obj.EntryDate = Convert.ToDateTime(mrd["EntryDate"].ToString());
                    plsData.Add(obj);
                }
            }

            cmd.Dispose();
           connection.Close();


            //for (int i = 0; i < 50; i++)
            //{
            //    BaseEquipment baseEquipment = new BaseEquipment();
            //    baseEquipment.Name = "Laptop_"+i.ToString();
            //    baseEquipment.EcCount = 5+i;
            //    baseEquipment.EntryDate = DateTime.Now.Date;
            //    plstData.Add(baseEquipment);
            //}
            return plsData;
        }
       public int SaveEquipment()         
        {
          
            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(ConnString);
            sqlConnection.Open();
            SqlCommand cmd = sqlConnection.CreateCommand();
            cmd.CommandText = "dbo.OSTINSERTLIST";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@Name", this.Name));
            cmd.Parameters.Add(new SqlParameter("@EcCount", this.EcCount));
            cmd.Parameters.Add(new SqlParameter("@EntryDate", this.EntryDate));
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
             int returnResult= cmd.ExecuteNonQuery();

           //if (reader.HasRows)
           // {
           //     while (reader.Read())
           //     {
           //         BaseEquipment bb = new BaseEquipment();
           //         bb.Name = reader["EquipmentName"].ToString();
           //         bb.EcCount = Convert.ToInt16(reader["Quantity"].ToString());
           //         bb.EntryDate = Convert.ToDateTime(reader["EntryDate"].ToString());
           //         plsData.Add(bb);
           //     }

           // }
            cmd.Dispose();
            sqlConnection.Close();
            return returnResult;
        }
    } 
}