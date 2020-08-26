using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Runtime.InteropServices;

namespace BillAutomation      //DO NOT change the namespace name
{
    public class DBHandler    //DO NOT change the class name
    {
        public DBHandler()
        {
        }

        //Implement the methods as per the description
        public SqlConnection GetConnection()
        {
            SqlConnection sql;
            sql=new SqlConnection (ConfigurationManager.ConnectionStrings["MyCon"].ConnectionString);
            return sql;
        }
    }
}
