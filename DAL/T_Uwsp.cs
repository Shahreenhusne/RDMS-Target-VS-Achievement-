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
    public class T_Uwsp
    {
        private SqlDataAdapter da;
        private SqlConnection dbConn;
        private SqlCommand cmd;
        private DataSet ds;
        private DataTable dt;
        private string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["dbconn1"];

        public DataTable Insert_Update(string procedure, Uwsp uwsp)
        {
            dbConn = new SqlConnection(ConnectionString);
            dbConn.Open();

            cmd = new SqlCommand(procedure, dbConn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@p_UWSPNUM", uwsp.UWSPNUM));
            cmd.Parameters.Add(new SqlParameter("@p_UWSPUSERID", uwsp.UWSPUSERID));
            cmd.Parameters.Add(new SqlParameter("@p_UWSPCOMCID", uwsp.UWSPCOMCID));
            cmd.Parameters.Add(new SqlParameter("@p_UWSPUSER", uwsp.UWSPUSER));
            cmd.Parameters.Add(new SqlParameter("@p_UWSPTERMID", uwsp.UWSPTERMID));
            cmd.Parameters.Add(new SqlParameter("@p_UWSPDATE", uwsp.UWSPDATE));
            cmd.Parameters.Add(new SqlParameter("@p_UWSPACTV", uwsp.UWSPACTV));

            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);

            dbConn.Close();
            return dt;
        }
    }
}
