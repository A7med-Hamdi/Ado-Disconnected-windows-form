using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ado
{
    internal class Businesslayer
    {
        public static DataTable getall()
        {
            SqlCommand cmd = new SqlCommand("select * from course");
            return DBlayers.select(cmd);
        }

       
    }
}
