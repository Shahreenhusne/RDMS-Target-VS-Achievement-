$(function () {
    $(document).ready(function () {
     $("#cmbCompanyName").select2({
        closeOnSelect: true
    });
    $("#cmbYear").select2({
        closeOnSelect: true
    });
    $("#cmbTargetType").select2({
        closeOnSelect: true
    });
    $("#cmbEmployee").select2({
        closeOnSelect: true
    });
        
        $("#datePicker").datepicker({
            format: "dd-M-yyyy",
            autoclose: true,
            orientation: "bottom",
            startView: 0
        });
        $("#datePicker").datepicker('setDate', new Date());
        TarVsAacheHelper.BuildExpTbl1();
        TarVsAacheHelper.BuildExpTbl2();
    });
    
    $("#btnPreview").click(function () {
        if ($("#cmbCompanyName").val() != null && $("#cmbYear").val() != null) {
            TarVsAacheHelper.GetTarVsAchTbl();
            $("#tblTarVsAchFull").show();
        }
        else {
            swal({
                title: " Warning!",
                text: "Company name , Year  is required ",
                icon: "error",
            });


        }

     });
    $("#btnCompanyName").click(function () {
        TarVsAacheHelper.GenerateCombo($("#cmbCompanyName"), "ERPACCDB.dbo.[SP_PERMISSION]", "USER_WISE_COMP", $('#hdnUserId').val(), "", "", "");
    });
    $("#btnEmployee").click(function () {
        TarVsAacheHelper.GenerateCombo($("#cmbEmployee"), "SP_DROPDOWN_TARVSACH", "SelectEmployeeID", $('#txtEmployee').val(), "", "", "");
    });
    $("#btnYear").click(function () {
        TarVsAacheHelper.GenerateCombo($("#cmbYear"), "SP_DROPDOWN_TARVSACH", "SelectPermitteddYear", "", "", "", "");
    });
    $("#btnPrint").click(function () {
        TarVsAacheHelper.PrintReportByReportType();
    });
});
var TarVsAacheHelper = {
    GenerateCombo: function (objcmb, proName, callName, param1, param2, param3, param4, param5) {
        objcmb.empty();
        $.getJSON("../TarVsAch/GenerateCombo/?procedureName=" + proName + '&callName=' + callName + '&Param1=' + param1 + '&Param2=' + param2 + '&Param3=' + param3 + '&Param4=' + param4)
            .done(function (data) {
                if (data.length == 1) {
                    $.each(data, function (key, item) {
                        objcmb.append($("<option></option>").attr("value", item.Id).text(item.Name));
                    });
                } else {
                    objcmb.append($("<option></option>").attr("value", "").text("-Select-"));
                    $.each(data, function (key, item) {
                        objcmb.append($("<option></option>").attr("value", item.Id).text(item.Name));
                    });
                }

            });
    },
    BuildExpTbl1: function (tbldata) { //datatable
        var table = $("#tblTarVsAch").DataTable({
            data: tbldata,
            "bDestroy": true,
            "scrollX": true,
            "searching": false,
            "columns": [
                { "data": "Sl" }, //0
                { "data": "EmployeeName" },//1
                { "data": "Designation" },//2
                { "data": "JanTar" },//3
                { "data": "JanAch" },//4
                { "data": "FebTar" },//5
                { "data": "FebAch" },//6
                { "data": "MarTar" },//7
                { "data": "MarAch" },//8
                { "data": "ArpTar" },//9
                { "data": "ArpAch" },//10
                { "data": "MayTar" },//11
                { "data": "MayAch" },//12
                { "data": "JunTar" },//13
                { "data": "JunAch" },//14
                { "data": "JulTar" },//15
                { "data": "JulAch" },//16
                { "data": "AugTar" },//17
                { "data": "AugAch" },//18
                { "data": "SepTar" },//19
                { "data": "SepAch" },//20
                { "data": "OctTar" },//21
                { "data": "OctAch" },//22
                { "data": "NovTar" },//23
                { "data": "NovAch" },//24
                { "data": "DecTar" },//25
                { "data": "DecAch" },//26
                { "data": "EmployeeIdhide" }//27
            ],
            "columnDefs": [
                {
                    "targets": [0],
                    "width": "2%",
                    render: function (data, type, row, meta) {
                        return meta.row + meta.settings._iDisplayStart + 1;
                    }

                },
                {
                    "targets": [1],
                    "width": "2%"
                },
                
                {
                    "targets": [2],
                    "width": "2%"
                },
                {
                    "className": "dt-right",
                    "targets": 3 
                },
                {
                    "targets": [4],
                    "className": "dt-right",
                    "render": function (data, type, row, meta, month) {
                        return '<a href="#" onclick="TarVsAacheHelper.GetAchievementDetails(' + row.EmployeeIdhide + ',\'Jan\')">' + row.JanAch + '</a>';
                    }
                },
                {
                    "className": "dt-right",
                    "targets": 5
                },
            
                {
                    "className": "dt-right",
                    "targets": 6,
                    "render": function (data, type, row, meta, month) {
                        return '<a href="#" onclick="TarVsAacheHelper.GetAchievementDetails(' + row.EmployeeIdhide + ',\'Feb\')">' + row.FebAch + '</a>';
                    }
                },
                {
                    "className": "dt-right",
                    "targets": 7
                },
                
                {
                    "className": "dt-right",
                    "targets": 8,
                    "render": function (data, type, row, meta, month) {
                        return '<a href="#" onclick="TarVsAacheHelper.GetAchievementDetails(' + row.EmployeeIdhide + ',\'Mar\')">' + row.MarAch + '</a>';
                    }
                },
                {
                    "className": "dt-right",
                    "targets": 9,
                },
                {
                    "className": "dt-right",
                    "targets": 10,
                    "render": function (data, type, row, meta, month) {
                        return '<a href="#" onclick="TarVsAacheHelper.GetAchievementDetails(' + row.EmployeeIdhide + ',\'Arp\')">' + row.ArpAch + '</a>';
                    }
                   
                },
                {
                    "className": "dt-right",
                    "targets": 11,
                },
                {
                    "className": "dt-right",
                    "targets": 12,
                    "render": function (data, type, row, meta, month) {
                        return '<a href="#" onclick="TarVsAacheHelper.GetAchievementDetails(' + row.EmployeeIdhide + ',\'May\')">' + row.MayAch + '</a>';
                    }
                },
                {
                    "className": "dt-right",
                    "targets": 13,
                },
                {
                    "className": "dt-right",
                    "targets": 14,
                    "render": function (data, type, row, meta, month) {
                        return '<a href="#" onclick="TarVsAacheHelper.GetAchievementDetails(' + row.EmployeeIdhide + ',\'Jun\')">' + row.JunAch + '</a>';
                    }
                },
                {
                    "className": "dt-right",
                    "targets": 15,
                },
                {
                    "className": "dt-right",
                    "targets": 16,
                    "render": function (data, type, row, meta, month) {
                        return '<a href="#" onclick="TarVsAacheHelper.GetAchievementDetails(' + row.EmployeeIdhide + ',\'Jul\')">' + row.JulAch + '</a>';
                    }
                    
                },
                {
                    "className": "dt-right",
                    "targets": 17,
                },
                {
                    "className": "dt-right",
                    "targets": 18,
                    "render": function (data, type, row, meta, month) {
                        return '<a href="#" onclick="TarVsAacheHelper.GetAchievementDetails(' + row.EmployeeIdhide + ',\'Aug\')">' + row.AugAch + '</a>';
                    }
                },
                {
                    "className": "dt-right",
                    "targets": 19,
                },
                {
                    "className": "dt-right",
                    "targets": 20,
                    "render": function (data, type, row, meta, month) {
                        return '<a href="#" onclick="TarVsAacheHelper.GetAchievementDetails(' + row.EmployeeIdhide + ',\'Sep\')">' + row.SepAch + '</a>';
                    }
                },
                {
                    "className": "dt-right",
                    "targets": 21,
                },
                {
                    "className": "dt-right",
                    "targets": 22,
                    "render": function (data, type, row, meta, month) {
                        return '<a href="#" onclick="TarVsAacheHelper.GetAchievementDetails(' + row.EmployeeIdhide + ',\'Oct\')">' + row.OctAch + '</a>';
                    }
                },
                {
                    "className": "dt-right",
                    "targets": 23,
                },
                {
                    "className": "dt-right",
                    "targets": 24,
                    "render": function (data, type, row, meta, month) {
                        return '<a href="#" onclick="TarVsAacheHelper.GetAchievementDetails(' + row.EmployeeIdhide + ',\'Nov\')">' + row.NovAch + '</a>';
                    }
                },
                {
                    "className": "dt-right",
                    "targets": 25,
                },
                {
                    "className": "dt-right",
                    "targets": 26,
                    "render": function (data, type, row, meta, month) {
                        return '<a href="#" onclick="TarVsAacheHelper.GetAchievementDetails(' + row.EmployeeIdhide + ',\'Dec\')">' + row.DecAch + '</a>';
                    }
                },
                {
                    "visible": false, "targets": 27
                }
            ] 
           
        });
    },
    BuildExpTbl2: function (tbldata) { //datatable
        var table = $("#tblAchDetails").DataTable({
            data: tbldata,
            "responsive": true,
            "bDestroy": true,
            /*"scrollX": true,*/
            "searching": false,
            "columns": [
                { "data": "Sl" }, //0
                { "data": "CalanNumber" },//1
                { "data": "CalanDate" },//2
                { "data": "AchievedAmount" }//3
               
            ],
            "columnDefs": [
                {
                    "targets": [0],
                    "width": "2%",
                    render: function (data, type, row, meta) {
                        return meta.row + meta.settings._iDisplayStart + 1;
                    }
                },

                {
                    "className": "dt-right",
                    "targets": 3,
                    
                },
                    

                
            ],
            "footerCallback": function (row, data, start, end, display) {
                //debugger;
                var api = this.api(), data;

                // Remove the formatting to get integer data for summation
                var intVal = function (i) {
                    return typeof i === 'string' ?
                        i.replace(/[\$,]/g, '') * 1 :
                        typeof i === 'number' ?
                            i : 0;
                };

                //Actual Total amount as per Dues (all over pages)
                var actualTotal = api
                    .column(3)
                    .data()                            
                    .reduce(function (a, b) {
                        return intVal(a) + intVal(b);
                    }, 0);



                $(api.column(2).footer()).html('Total');
                $(api.column(3).footer()).html(actualTotal.toLocaleString(2));

            },

        });
    },
    GetTarVsAchTbl: function () {
        var ComName = $("#cmbCompanyName").val();
        var SelectYear = $("#cmbYear").val();
        var EmployeeID = $("#cmbEmployee").val();
        
        $.getJSON("../TarVsAch/GetTarVsAchTbl/?ComName=" + ComName + '&SelectYear=' + SelectYear + '&EmployeeID=' + EmployeeID )
            .done(function (data) {
               
                TarVsAacheHelper.BuildExpTbl1(data.tbl1);
              
            });

    },
    GetAchievementDetails: function (EmpId, month) { 
        $("#tblAchDetailsFull").show();
        var table = $("#tblAchDetails").DataTable();
        var ComName = $("#cmbCompanyName").val(); //1
        var SelectYear = $("#cmbYear").val(); 
        var EmployeeID = EmpId; //3
        var Month = month + "-" + $("#cmbYear").val();
        $.getJSON("../TarVsAch/GetAchievementDetails/?Month=" + Month + '&EmployeeID=' + EmployeeID + '&ComName=' + ComName)
            .done(function (data) {
                $("#lblId").html('ID:' + data.tbl02lbl[0].ID);
                $("#lblName").html('Name:' + data.tbl02lbl[0].Name);
                $("#lblDeg").html('Designation:' + data.tbl02lbl[0].Designation);
                TarVsAacheHelper.BuildExpTbl2(data.tbl02tbl);

            });

    },
    PrintReportByReportType: function () {
        

        var obj = new Object();

        obj.COMC1 = "";
        obj.DESC1 = $("#cmbCompanyName").val();
        obj.DESC2 = $("#cmbYear").val();
        obj.DESC3 = $("#cmbEmployee").val();
        obj.DESC4 = "";
        obj.DESC5 = "";
        obj.DESC6 = "";

        obj.CALLTYPE = "GetTarVsAchReport";

        var objDetails = JSON.stringify(obj);
        var jsonParam = "objReportParameter:" + objDetails;
        var serviceUrl = "/TarVsAch/PrintReportByReportType";
        jQuery.ajax({
            url: serviceUrl,
            async: false,
            type: "POST",
            data: "{" + jsonParam + "}",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.status) {
                    var cmbViewType = "PDF";
                    window.open('../Reports/ReportViewerRDLC.aspx?exp=' + cmbViewType, '_blank');
                } else {
                    swal({
                        title: "Sorry!",
                        text: "No Data Found",
                        type: "info",
                        closeOnConfirm: false
                    });
                }
            }
        });

    },
    

   
}



