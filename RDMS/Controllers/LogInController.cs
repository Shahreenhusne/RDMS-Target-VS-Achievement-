using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using DAL;

namespace RDMS.Controllers
{
    public class LogInController : Controller
    {

        private DataSet ds;
        private Common common = new Common();
        public DataSet MenuData = new DataSet();
        // GET: LogIn

        public ActionResult LogIn()
        {
            Session.RemoveAll();
            return View();

        }

        [HttpPost]
        public JsonResult LogIn(string UserName, string Password)
        {
            bool status = false;
            string a = GetVisitorDetails();
            string b = GetMachineNameUsingIPAddress(a);

            string UserNameR = StrReverse(UserName.ToUpper());
            string UserPass = EncodeMD5(UserNameR + Password);
            ds = new DataSet();
            ds = common.select_data_20("", "SP_UTILITY_LOGIN_PROCESS", "LOGININFO1", UserName.ToUpper(), UserPass);
            if (ds == null || ds.Tables[0].Rows.Count <= 0)
            {
                status = false;
            }
            else
            {
                Session["dsLogIn1"] = ds;
                Session["UserID"] = ds.Tables[0].Rows[0][0].ToString();
                Session["UserIP"] = a.ToString();
                Session["TermID"] = b.ToUpper();
                Session["UserloginId"] = ds.Tables[0].Rows[0]["usrsname"].ToString();
                Session["UserName"] = ds.Tables[0].Rows[0]["UsrName"].ToString();
                Session["UserRight"] = ds.Tables[0].Rows[0]["usrright"].ToString();
                Session["UserDesig"] = ds.Tables[0].Rows[0]["USRDESIG"].ToString();
                //Session["Hrimg"] = ds.Tables[0].Rows[0]["hrimg"];
                if (Session["MenuData"] != null)
                    MenuData = (DataSet)Session["MenuData"];
                else
                    GetMenuInfo(Session["UserID"].ToString());

                status = true;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public void GetMenuInfo(string userId)
        {
            System.Web.HttpContext.Current.Session["MenuData"] = common.select_data_20("", "MasterDB.dbo.SP_MENU_SYSTEM", "GET_MENU_INFO", userId);
        }

        public static string EncodeMD5(string originalStr)
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(originalStr);
            encodedBytes = md5.ComputeHash(originalBytes);
            return BitConverter.ToString(encodedBytes);
        }
        public static string StrReverse(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
        internal string GetVisitorDetails()
        {
            string address = "";
            string varIPAddress = string.Empty;
            string varVisitorCountry = string.Empty;
            string varIpAddress = string.Empty;
            varIpAddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(varIpAddress))
            {
                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                {
                    varIpAddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                }
            }

            //varIPAddress = (System.Web.UI.Page)Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (varIPAddress == "" || varIPAddress == null)
            {
                if (System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
                {
                    varIpAddress = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
            }

            if (string.IsNullOrEmpty(varIpAddress) || varIpAddress.Trim() == "::1")
            {
                var addresses = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
                if (addresses == null)
                {
                    varIpAddress = String.Empty;
                }
                else
                {
                    varIpAddress = addresses.ToString();
                }
            }
            return varIpAddress;
        }
        internal string GetMachineNameUsingIPAddress(string varIpAdress)
        {
            string machineName = string.Empty;
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(varIpAdress);

                machineName = hostEntry.HostName;
            }
            catch (Exception ex)
            {
                // Machine not found...
            }
            return machineName;
        }
        internal DataTable GetLocation(string varIPAddress)
        {
            WebRequest varWebRequest = WebRequest.Create("http://freegeoip.net/xml/" + varIPAddress);
            WebProxy px = new WebProxy("http://freegeoip.net/xml/" + varIPAddress, true);
            varWebRequest.Proxy = px;
            varWebRequest.Timeout = 2000;
            try
            {
                WebResponse rep = varWebRequest.GetResponse();
                XmlTextReader xtr = new XmlTextReader(rep.GetResponseStream());
                DataSet ds = new DataSet();
                ds.ReadXml(xtr);
                return ds.Tables[0];
            }
            catch
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult Show(string id)
        {
            string ImgID = Request.QueryString["ImgID"].ToString();
            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";

            DataSet ds = (DataSet)Session["dsLogIn1"];

            byte[] i;
            if (ds.Tables[0].Rows[0]["hrimg"].ToString() != String.Empty)
            {
                i = (byte[])ds.Tables[0].Rows[0]["hrimg"];
                Response.BinaryWrite(i);
            }
            return new JsonResult { Data = new { } };
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("LogIn", "LogIn");
        }

    }
}