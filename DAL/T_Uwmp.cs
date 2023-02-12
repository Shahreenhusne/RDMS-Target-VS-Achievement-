using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;

namespace DAL
{
    public class T_Uwmp
    {
        SqlDataAdapter da;
        SqlConnection dbConn;
        SqlCommand cmd;
        DataSet ds;
        DataTable dt;
        string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["dbconn1"];

        public DataTable Insert_Update(string procedure, Uwmp uwmp)
        {
            dbConn = new SqlConnection(ConnectionString);
            dbConn.Open();

            cmd = new SqlCommand(procedure, dbConn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@p_UWMPNUM", uwmp.UWMPNUM));
            cmd.Parameters.Add(new SqlParameter("@p_UWMPUSERNUM", uwmp.UWMPUSERNUM));
            cmd.Parameters.Add(new SqlParameter("@p_UWMPMENUD", uwmp.UWMPMENUD));
            cmd.Parameters.Add(new SqlParameter("@p_UWMPACTV", uwmp.UWMPACTV));
            cmd.Parameters.Add(new SqlParameter("@p_UWMPUSER", uwmp.UWMPUSER));
            cmd.Parameters.Add(new SqlParameter("@p_UWMPTERMID", uwmp.UWMPTERMID));
            cmd.Parameters.Add(new SqlParameter("@p_UWMPDATE", uwmp.UWMPDATE));

            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);

            dbConn.Close();
            return dt;
        }
    }
}
