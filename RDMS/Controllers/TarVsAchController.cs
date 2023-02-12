using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DAL; 
using System.Web.Mvc;
using BOL;



namespace RDMS.Controllers
{
    public class TarVsAchController : Controller
    {
        EntityReportsParams objReportParams = new EntityReportsParams();
        public DataSet ds;
        public DataTable dt;
        public Common common = new Common();
        List<DropDownList> _dropdownlistdata = new List<DropDownList>();

        public ActionResult TarVsAch()
        {
            return View();
        }
        public ActionResult GenerateCombo(string procedureName, string callName, string Param1, string Param2, string Param3, string Param4)
        {
            ds = new DataSet();
            ds = common.select_data_20("", procedureName, callName, Param1, Param2, Param3, Param4);

            if (ds != null)
            {
                _dropdownlistdata = (from DataRow dr in ds.Tables[0].Rows
                                     select new DropDownList()
                                     {
                                         Id = dr["Id"].ToString(),
                                         Name = dr["Name"].ToString()
                                     }).ToList();

            }

            var res = _dropdownlistdata;
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTarVsAchTbl(TarVsAchEntity objDetails)
        {
            string tbl1 = "";
            bool status = false;

            ds = common.select_data_20("", "SP_SELECT_TARVSACH", "GetTarVsAchTbl", objDetails.ComName, objDetails.SelectYear, (objDetails.EmployeeID== "null" ? "": objDetails.EmployeeID));

            var GetDetailslist = (from DataRow dr in ds.Tables[1].Rows
                                  select new TarVsAchEntity()
                                  {
                                      EmployeeName = dr["HRNAME"].ToString(),
                                      Designation = dr["HRDESIG"].ToString(),
                                      JanTar = dr["JanTar"].ToString(),
                                      JanAch = dr["JanAch"].ToString(),
                                      FebTar = dr["FebTar"].ToString(),
                                      FebAch = dr["FebAch"].ToString(),
                                      MarTar = dr["MarTar"].ToString(),
                                      MarAch = dr["MarAch"].ToString(),
                                      ArpTar = dr["AprTar"].ToString(),
                                      ArpAch = dr["AprAch"].ToString(),
                                      MayTar = dr["MayTar"].ToString(),
                                      MayAch = dr["MayAch"].ToString(),
                                      JunTar = dr["JunTar"].ToString(),
                                      JunAch = dr["JunAch"].ToString(),
                                      JulTar = dr["JulTar"].ToString(),
                                      JulAch = dr["JulAch"].ToString(),
                                      AugTar = dr["AugTar"].ToString(),
                                      AugAch = dr["AugAch"].ToString(),
                                      SepTar = dr["SepTar"].ToString(),
                                      SepAch = dr["SepAch"].ToString(),
                                      OctTar = dr["OctTar"].ToString(),
                                      OctAch = dr["OctAch"].ToString(),
                                      NovTar = dr["NovTar"].ToString(),
                                      NovAch = dr["NovAch"].ToString(),
                                      DecTar = dr["DecTar"].ToString(),
                                      DecAch = dr["DecAch"].ToString(),
                                      EmployeeIdhide = dr["HRCODE"].ToString()

                                  }).ToList();
            return Json(new { tbl1 = GetDetailslist }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAchievementDetails(AchDetailsEntity objDetails)
        {
            string tbl02lbl = "";
            string tbl02tbl = "";
            bool status = false;
            //string fdate = new DateTime(Convert.ToDateTime(objDetails.Month.Trim()).Year, Convert.ToDateTime(objDetails.Month.Trim()).Month, 1).ToString("dd-MMM-yyyy ");
            string tdate = Convert.ToDateTime(objDetails.Month).AddMonths(1).AddDays(-1).ToString("dd-MMM-yyyy");
            ds = common.select_data_20("", "SP_SELECT_TARVSACH", "GetAchievementDetailsTbl", objDetails.Month, tdate, objDetails.EmployeeID ,objDetails.ComName);
            var GetAchlistlbl = (from DataRow dr in ds.Tables[0].Rows
                                 select new AchDetailsEntity()
                                 {
                                     ID = dr["HRCODE"].ToString(),
                                     Name = dr["HRNAME"].ToString(),
                                     Designation = dr["HRDESIG"].ToString()

                                 }).ToList();
            var GetAchlisttbl = (from DataRow dr in ds.Tables[1].Rows
                                  select new AchDetailsEntity()
                                  {
                                      CalanNumber = dr["CHNUM"].ToString(),
                                      CalanDate = dr["CHDAT"].ToString(),
                                      AchievedAmount = dr["AcheAmount"].ToString()

                                  }).ToList();
            return Json(new { tbl02lbl = GetAchlistlbl, tbl02tbl = GetAchlisttbl }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PrintReportByReportType(EntityDefaultParameter objReportParameter)
        {
            bool status = false;
            string procedureName = "";
            procedureName = "SP_REPORT_TARVSACH";


            objReportParams.RptFileName = "RptTarVsAch.rdlc";
            objReportParams.DataSetName = "Header";
            objReportParams.DataSetName02 = "Table";
            objReportParams.DataSetName03 = "";
            objReportParams.RptFolder = "TarVsAch";

            ds = new DataSet();
            ds = common.select_data_20(objReportParameter.COMC1, procedureName, objReportParameter.CALLTYPE, objReportParameter.DESC1, objReportParameter.DESC2, objReportParameter.DESC3
                , objReportParameter.DESC4, objReportParameter.DESC5, objReportParameter.DESC6, objReportParameter.DESC7, objReportParameter.DESC8
                , objReportParameter.DESC9, objReportParameter.DESC10, objReportParameter.DESC11, objReportParameter.DESC12, objReportParameter.DESC13
                , objReportParameter.DESC14, objReportParameter.DESC15, objReportParameter.DESC16, objReportParameter.DESC17, objReportParameter.DESC18
                , objReportParameter.DESC19, objReportParameter.DESC20);


            if (ds.Tables[0].Rows.Count > 0)
            {
                status = true;
                objReportParams.DataSetSource = ds;
                this.HttpContext.Session["ReportParam"] = objReportParams;
            }
            

            return new JsonResult { Data = new { status = status } };
        }






    }
}