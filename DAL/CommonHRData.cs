using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace DAL
{
    public class CommonHRData
    {
        SqlDataAdapter da;
        SqlConnection dbConn;
        SqlCommand cmd;
        DataSet ds;
        DataTable dt;
        string ConnectionString1 = System.Configuration.ConfigurationManager.AppSettings["dbconn2"].ToString();


        public DataSet select_data_20(string comCostID, string ProcName, string CallType,
            string parm1 = "", string parm2 = "", string parm3 = "", string parm4 = "", string parm5 = "", string parm6 = "", string parm7 = "", string parm8 = "",
            string parm9 = "", string parm10 = "", string parm11 = "", string parm12 = "", string parm13 = "", string parm14 = "", string parm15 = "",
            string parm16 = "", string parm17 = "", string parm18 = "", string parm19 = "", string parm20 = "")
        {
            try
            {
                dbConn = new SqlConnection(ConnectionString1);
                dbConn.Open();

                cmd = new SqlCommand(ProcName, dbConn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ComC1", comCostID));
                cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
                cmd.Parameters.Add(new SqlParameter("@Desc1", parm1));
                cmd.Parameters.Add(new SqlParameter("@Desc2", parm2));
                cmd.Parameters.Add(new SqlParameter("@Desc3", parm3));
                cmd.Parameters.Add(new SqlParameter("@Desc4", parm4));
                cmd.Parameters.Add(new SqlParameter("@Desc5", parm5));
                cmd.Parameters.Add(new SqlParameter("@Desc6", parm6));
                cmd.Parameters.Add(new SqlParameter("@Desc7", parm7));
                cmd.Parameters.Add(new SqlParameter("@Desc8", parm8));
                cmd.Parameters.Add(new SqlParameter("@Desc9", parm9));
                cmd.Parameters.Add(new SqlParameter("@Desc10", parm10));
                cmd.Parameters.Add(new SqlParameter("@Desc11", parm11));
                cmd.Parameters.Add(new SqlParameter("@Desc12", parm12));
                cmd.Parameters.Add(new SqlParameter("@Desc13", parm13));
                cmd.Parameters.Add(new SqlParameter("@Desc14", parm14));
                cmd.Parameters.Add(new SqlParameter("@Desc15", parm15));
                cmd.Parameters.Add(new SqlParameter("@Desc16", parm16));
                cmd.Parameters.Add(new SqlParameter("@Desc17", parm17));
                cmd.Parameters.Add(new SqlParameter("@Desc18", parm18));
                cmd.Parameters.Add(new SqlParameter("@Desc19", parm19));
                cmd.Parameters.Add(new SqlParameter("@Desc20", parm20));

                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                dbConn.Close();

                //cmd.ExecuteNonQuery();

                return ds;
            }
            catch
            {
                return null;
            }
        }

        public string EncodeMD5(string originalStr)
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(originalStr);
            encodedBytes = md5.ComputeHash(originalBytes);
            return BitConverter.ToString(encodedBytes);
        }

        public string Encode(string serverName)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(serverName));
        }

        public string Decode(string encodedServername)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(encodedServername));
        }

        public void GetAllProject(DropDownList ddlProjectFor, string user, DropDownList ddlComp)
        {
            ds = new DataSet();
            ds = select_data_20("", "ERPACCDB.dbo.SP_PERMISSION", "ALL_PROJ", user, ddlComp.SelectedValue);
            ddlProjectFor.DataSource = ds.Tables[0];
            ddlProjectFor.DataTextField = "PROJ";
            ddlProjectFor.DataValueField = "PROJID";
            ddlProjectFor.DataBind();
            ddlProjectFor.Items.Insert(0, new ListItem("--Select--", ""));
        }
        public void GetAllProjectForm(DropDownList ddlProjectFor, string user, DropDownList ddlComp)
        {
            ds = new DataSet();
            ds = select_data_20("", "ERPACCDB.dbo.SP_PERMISSION", "GET_PROJECT_LIST", user, ddlComp.SelectedValue);
            ddlProjectFor.DataSource = ds.Tables[0];
            ddlProjectFor.DataTextField = "PROJ";
            ddlProjectFor.DataValueField = "PROJID";
            ddlProjectFor.DataBind();
            ddlProjectFor.Items.Insert(0, new ListItem("--Select--", ""));
        }

        public DataSet select_dropdown_data(string procedure, string call_type, DropDownParam ddlParam)
        {

            dbConn = new SqlConnection(ConnectionString1);
            dbConn.Open();
            cmd = new SqlCommand(procedure, dbConn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@CALLTYPE", call_type));
            cmd.Parameters.Add(new SqlParameter("@param1", ddlParam.Param1));
            cmd.Parameters.Add(new SqlParameter("@param2", ddlParam.Param2));
            cmd.Parameters.Add(new SqlParameter("@param3", ddlParam.Param3));
            cmd.Parameters.Add(new SqlParameter("@param4", ddlParam.Param4));

            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            dbConn.Close();
            return ds;

        }

        public int delete_data(string comCostID, string ProcName, string CallType,
            string parm1 = "", string parm2 = "")
        {

            dbConn = new SqlConnection(ConnectionString1);
            dbConn.Open();

            cmd = new SqlCommand(ProcName, dbConn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@ComC1", comCostID));
            cmd.Parameters.Add(new SqlParameter("@CallType", CallType));
            cmd.Parameters.Add(new SqlParameter("@Desc1", parm1));
            cmd.Parameters.Add(new SqlParameter("@Desc2", parm2));

            return cmd.ExecuteNonQuery();
        }
    }
}
