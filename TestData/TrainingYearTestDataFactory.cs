﻿using ClosedXML.Excel;
using GApp.Core.Context;
using GApp.DAL;
using GApp.DAL.ReadExcel;
using GApp.UnitTest.TestData;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL;
using TrainingIS.DAL;
using TrainingIS.Entities;

namespace TestData
{
    public partial class TrainingYearTestDataFactory
    {
 
        //protected override List<TrainingYear> Generate_TestData()
        //{
        //    List<TrainingYear> Data = new List<TrainingYear>();
 
        //    // Create Paths
        //    this.Create_TestData_Files_Directory_If_Not_Exist();
        //    string FileName = this.Get_Solution_Path() + "Data/TrainingYear_TestData.xlsx";
        //    string Repport_File = this.Get_Solution_Path() + "Data/Repports/TrainingYear_TestData.xlsx";


        //    if (File.Exists(FileName))
        //    {
        //        // Load Data from Excel file
        //        var excelData = new ExcelData(FileName);
        //        DataTable firstTable = excelData.getFirstTable();

        //        // Import Data not imported
        //        if (File.Exists(Repport_File))
        //        {
                   

        //            ImportReport importReport = (this.BLO as TrainingYearBLO).Import(firstTable, FileName);
        //            // Save ExcelRepport file to Server
        //            DataSet DataSet_report = importReport.get_DataSet_Report();
        //            using (XLWorkbook wb = new XLWorkbook())
        //            {
        //                wb.Worksheets.Add(DataSet_report);
        //                wb.SaveAs(Repport_File);
        //            }

        //            // Convert Data Table to Data
        //            Data = importReport.ImportedObjects.Cast<TrainingYear>().ToList();

                   
        //        }
        //        else
        //        {
        //            Data = (this.BLO as TrainingYearBLO).Convert_DataTable_to_List(firstTable);
        //        }
        //    }
            
          


        //    //// 2019 
        //    //TrainingYear trainingYear_2019 = new TrainingYear();
        //    //trainingYear_2019.StartDate = Convert.ToDateTime("01/09/2018");
        //    //trainingYear_2019.EndtDate = Convert.ToDateTime("31/08/2019");
        //    //trainingYear_2019.Code = "2019";
        //    //trainingYear_2019.Reference = trainingYear_2019.Code;
        //    //Data.Add(trainingYear_2019);

        //    //// 2020
        //    //TrainingYear trainingYear_2020 = new TrainingYear();
        //    //trainingYear_2020.StartDate = Convert.ToDateTime("01/09/2019");
        //    //trainingYear_2020.EndtDate = Convert.ToDateTime("31/08/2020");
        //    //trainingYear_2020.Reference = "2020";
        //    //trainingYear_2020.Code = trainingYear_2020.Reference;
        //    //Data.Add(trainingYear_2020);

        //    //// 2021
        //    //TrainingYear trainingYear_2021 = new TrainingYear();
        //    //trainingYear_2021.StartDate = Convert.ToDateTime("01/09/2020");
        //    //trainingYear_2021.EndtDate = Convert.ToDateTime("31/08/2021");
        //    //trainingYear_2021.Reference = "2021";
        //    //trainingYear_2021.Code = trainingYear_2021.Reference;
        //    //Data.Add(trainingYear_2021);

        //    return Data;
        //}
    }
}
