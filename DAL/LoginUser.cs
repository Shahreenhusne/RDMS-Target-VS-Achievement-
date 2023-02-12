using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    //public class LoginUserList
    //{
    //    public LoginUser LoginUser { get; set; }
    //}
    
    public class LoginUser
    {
        public static string  UserId { get; set; }
        public static string UserName { get; set; }
        public static string ComCode { get; set; }
        public static string ComName { get; set; }
        public static string TitmCode { get; set; }
        public static string UsrRight { get; set; }
        public static byte [] Image { get; set; }
        public static string TerMid { get; set; }
        public static string UserFullName { get; set; }
        public static string TktPrintDate { get; set; }
        public static bool IsShow { get; set; }
        public static string LoginDate { get; set; }

        public static byte[] CompanyLogo { get; set; }
        public static byte[] BackgroundImg { get; set; }

        public static string VatRegNo { get; set; }
        public static int EbackDate { get; set; }
        public static int CbackDate { get; set; }

        
    }
}
