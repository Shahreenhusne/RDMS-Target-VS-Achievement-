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
   public class T_TRN_Correction
    {
        SqlDataAdapter da;
        SqlConnection dbConn;
        SqlCommand cmd;
        DataSet ds;
        DataTable dt;
        string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["dbconn"].ToString();

        public DataTable Insert_Update(string comCostID, string ProcName, string callType,
            string requestedByID = "", string trbType = "", string trnID = "", string refType = "", string refNo = "", string postedByID = "", string status = "", string referenceNO = "")
        {
            try
            {
                dbConn = new SqlConnection(ConnectionString);
                dbConn.Open();

                cmd = new SqlCommand(ProcName, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@p_TRNCANCELID", ""));
                cmd.Parameters.Add(new SqlParameter("@ComC1", comCostID));
                cmd.Parameters.Add(new SqlParameter("@CallType", callType));
                cmd.Parameters.Add(new SqlParameter("@p_requestedByID", requestedByID));
                cmd.Parameters.Add(new SqlParameter("@p_trbType", trbType));
                cmd.Parameters.Add(new SqlParameter("@p_trnID", trnID));
                cmd.Parameters.Add(new SqlParameter("@p_refType", refType));
                cmd.Parameters.Add(new SqlParameter("@p_refNo", refNo));
                cmd.Parameters.Add(new SqlParameter("@p_postedByID", postedByID));
                cmd.Parameters.Add(new SqlParameter("@p_status", status));
                cmd.Parameters.Add(new SqlParameter("@p_referenceNO", referenceNO));
                

                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbConn.Close();  
            }
        }
    }
}
