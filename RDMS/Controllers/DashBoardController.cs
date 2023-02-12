using BOL;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace RDMS.Controllers
{
    public class DashBoardController : Controller
    {
        // GET: DashBoard
        LogInController logInController = new LogInController();
        private Common common = new Common();
        public DataSet MenuData = new DataSet();
        public ActionResult DashBoard(EntityDefaultParameter Param)
        {
            if (Param.DESC1 != null)
            {
               // Session["UserData"] = Param.DESC1;
                string DecUserData = AESEncrytDecry.DecryptStringAES(Param.DESC1);
                string[] UsrInf = DecUserData.Split(',');
                LogInAuth(UsrInf[0], UsrInf[1]);
                return View();
            }
            else
            {
                if (Session["UserID"] == null)
                {
                    return RedirectToAction("LogIn", "Home");
                }
                else
                {
                    return View();
                }
            }
            //if (Session["MenuData"] == null)
            //    return RedirectToAction("LogIn", "LogIn");
            //else
            //{
            //    return View();
            //}
        }
        public void LogInAuth(string UserName, string Password)
        {
            Session.RemoveAll();
            bool status = false;
            string a = logInController.GetVisitorDetails();
            string b = logInController.GetMachineNameUsingIPAddress(a);

            string UserNameR = StrReverse(UserName.ToUpper());
            string UserPass = EncodeMD5(UserNameR + Password);
            DataSet ds = new DataSet();
            ds = common.select_data_20("", "SP_UTILITY_LOGIN_PROCESS", "LOGININFO1", UserName.ToUpper(), UserPass);
            if (ds == null || ds.Tables[0].Rows.Count <= 0)
            {
                status = false;
            }
            else
            {

                Session["dsLogIn1"] = ds;
                Session["UserID"] = ds.Tables[0].Rows[0][0].ToString();

                Session["TermID"] = b.ToUpper();
                Session["UserloginId"] = ds.Tables[0].Rows[0]["usrsname"].ToString();
                Session["UserName"] = ds.Tables[0].Rows[0]["UsrName"].ToString();
                Session["UserRight"] = ds.Tables[0].Rows[0]["usrright"].ToString();

                if (Session["MenuData"] != null)
                    MenuData = (DataSet)Session["MenuData"];
                else
                    logInController.GetMenuInfo(Session["UserID"].ToString());

                status = true;
            }
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
    }
}