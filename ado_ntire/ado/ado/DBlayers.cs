using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace ado
{
    internal class DBlayers
    {
        public static DataTable select(SqlCommand cmd)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=ITI;Integrated Security=True");
         cmd.Connection = con;
            SqlDataAdapter adapter= new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
        public static int dml(SqlCommand cmd)
        {
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=ITI;Integrated Security=True");
            cmd.Connection=con;
            con.Open();
            int roweffect = cmd.ExecuteNonQuery();
            con.Close();
            return roweffect;

        }
    }
}
