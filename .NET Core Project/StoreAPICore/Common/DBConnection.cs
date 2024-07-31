using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAPICore.Common
{
    public class DBConnection
    {
        private string ConStr;
        public SqlConnection con; 
        public SqlCommand cmd;
        public SqlDataReader dr;
        public SqlTransaction tr;

        public DBConnection()
        {
            ConStr = @"Data Source=DESKTOP-R70P3QL\SQLEXPRESS;Initial Catalog=DCE_Test;Persist Security Info=True;User ID=Shehan;Password=Shehan@97;Encrypt=False";
            con = new SqlConnection(ConStr);
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            try 
            { 
                con.Open();
                tr = con.BeginTransaction();
                cmd.Transaction = tr;  
            } 
            catch 
            {
                this.Rollback();
            }
        }

        public void Commit()
        {
            tr.Commit();
            cmd.Dispose();
            con.Close();
        }

        public void Rollback() 
        {
            tr.Rollback();
            cmd.Dispose();
            con.Close();
        }
    }
}
