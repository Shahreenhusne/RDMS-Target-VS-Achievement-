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
    public class T_Uwpp
    {
        private SqlDataAdapter da;
        private SqlConnection dbConn;
        private SqlCommand cmd;
        private DataSet ds;
        private DataTable dt;
        private string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["dbconn1"];

        public DataTable Insert_Update(string procedure, Uwpp uwpp)
        {
            dbConn = new SqlConnection(ConnectionString);
            dbConn.Open();

            cmd = new SqlCommand(procedure, dbConn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@p_UWPPNUM", uwpp.UWPPNUM));
            cmd.Parameters.Add(new SqlParameter("@p_UWPPUSERID", uwpp.UWPPUSERID));
            cmd.Parameters.Add(new SqlParameter("@p_UWPPPROJID", uwpp.UWPPPROJID));
            cmd.Parameters.Add(new SqlParameter("@p_UWPPUSER", uwpp.UWPPUSER));
            cmd.Parameters.Add(new SqlParameter("@p_UWPPTERMID", uwpp.UWPPTERMID));
            cmd.Parameters.Add(new SqlParameter("@p_UWPPDATE", uwpp.UWPPDATE));
            cmd.Parameters.Add(new SqlParameter("@p_UWPPACTV", uwpp.UWPPACTV));

            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);

            dbConn.Close();
            return dt;
        }
    }
}
