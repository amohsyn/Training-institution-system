using ClosedXML.Excel;
using GApp.DAL.ReadExcel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL;
using TrainingIS.Entities;

namespace TestData
{
    public partial class SpecialtyTestDataFactory
    {
        //protected override List<Specialty> Load_Data_From_ExcelFile()
        //{
        //    List<Specialty> Data = null;

        //    // Create Paths
        //    this.Create_TestData_Files_Directory_If_Not_Exist();
        //    string FileName = this.Get_Solution_Path() + "Data/Specialty.xlsx";

        //    if (File.Exists(FileName))
        //    {
        //        Data = new List<Specialty>();

        //        // Load Data from Excel file
        //        var excelData = new ExcelData(FileName);
        //        DataTable firstTable = excelData.getFirstTable();
        //        Data = (this.BLO as SpecialtyBLO).Convert_DataTable_to_List(firstTable);
        //    }
        //    return Data;
        //}

        //protected override List<Specialty> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        //{
        //    is_Insert_Or_Update = false;
        //    List<Specialty> Data = new List<Specialty>();

        //    // Create Paths
        //    this.Create_TestData_Files_Directory_If_Not_Exist();
        //    string FileName = this.Get_Solution_Path() + "Data/Specialty.xlsx";
        //    string Repport_File = this.Get_Solution_Path() + "Data/Repports/Specialty.xlsx";

        //    if (File.Exists(FileName))
        //    {
        //        // Load Data from Excel file
        //        var excelData = new ExcelData(FileName);
        //        DataTable firstTable = excelData.getFirstTable();
        //        // Import Data not imported
        //        if (!File.Exists(Repport_File))
        //        {
                   
        //            ImportReport importReport = (this.BLO as SpecialtyBLO).Import(firstTable, FileName);
        //            // Save ExcelRepport file to Server
        //            DataSet DataSet_report = importReport.get_DataSet_Report();
        //            using (XLWorkbook wb = new XLWorkbook())
        //            {
        //                wb.Worksheets.Add(DataSet_report);
        //                wb.SaveAs(Repport_File);
        //            }
        //            // Convert Data Table to Data
        //            Data = importReport.ImportedObjects.Cast<Specialty>().ToList();
        //            is_Insert_Or_Update = true;
        //        }
        //        else
        //        {
        //            Data = (this.BLO as SpecialtyBLO).Convert_DataTable_to_List(firstTable);
        //        }
        //    }
        //    return Data;
        //}
    }
}
