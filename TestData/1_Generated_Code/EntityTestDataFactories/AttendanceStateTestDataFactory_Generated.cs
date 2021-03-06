﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using AutoFixture;
using TrainingIS.BLL;
using TrainingIS.DAL;
using System.ComponentModel.DataAnnotations;
using GApp.WebApp.Manager.Views;
using GApp.DAL;
using GApp.Entities;
using GApp.Core.Context;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;
using GApp.UnitTest.TestData;
using GApp.UnitTest.TestData.Enums;
using System.IO;
using System.Data;
using GApp.DAL.ReadExcel;
using ClosedXML.Excel;
using GApp.Exceptions;
namespace TestData
{
    public class BaseAttendanceStateTestDataFactory : EntityTestData<AttendanceState>
    {
		public string Entity_CRUD_Test_Reference { set; get; } 
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new AttendanceStateBLO(UnitOfWork, GAppContext);
			Entity_CRUD_Test_Reference  = "AttendanceState_CRUD_Test";
        }

        public BaseAttendanceStateTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

 
		protected override List<AttendanceState> Load_Data_From_ExcelFile()
        {
            List<AttendanceState> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/AttendanceState.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<AttendanceState>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as AttendanceStateBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<AttendanceState> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<AttendanceState> Data = new List<AttendanceState>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/AttendanceState.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/AttendanceState.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported

				// Delete Repport_File if exist
                if (!File.Exists(Repport_File))
                {
					ImportReport importReport = (this.BLO as AttendanceStateBLO).Import(firstTable, FileName);
					// Save ExcelRepport file to Server
					DataSet DataSet_report = importReport.get_DataSet_Report();
					using (XLWorkbook wb = new XLWorkbook())
					{
						wb.Worksheets.Add(DataSet_report);
						wb.SaveAs(Repport_File);
					}

						// Throw Exceltion if there is error in Import
					if( importReport.Number_of_inserted_erros_rows > 0 || importReport.Number_of_updated_erros_rows > 0)
					{
						string msg_ex = string.Format(" {0} : There are {1} error of Inserts and {2} of Update",
								"AttendanceState",
							importReport.Number_of_inserted_erros_rows, 
							importReport.Number_of_updated_erros_rows
                           
							);
						throw new GAppException(msg_ex);
					}

					// Convert Data Table to Data
					Data = importReport.ImportedObjects.Cast<AttendanceState>().ToList();
					is_Insert_Or_Update = true;
                }

				

            }
            return Data;
        }


		/// <summary>
        /// Find the first AttendanceState instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual AttendanceState CreateOrLouadFirstAttendanceState()
        {
            AttendanceStateBLO attendancestateBLO = new AttendanceStateBLO(UnitOfWork,GAppContext);
           
			AttendanceState entity = null;
            if (attendancestateBLO.FindAll()?.Count > 0)
                entity = attendancestateBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp AttendanceState for Test
                entity = this.CreateValideAttendanceStateInstance();
                attendancestateBLO.Save(entity);
            }
            return entity;
        }

		public virtual AttendanceState Create_CRUD_AttendanceState_Test_Instance()
        {
			AttendanceState AttendanceState = this.CreateValideAttendanceStateInstance();
            AttendanceState.Reference = this.Entity_CRUD_Test_Reference;
            return AttendanceState;
        }

        public virtual AttendanceState CreateValideAttendanceStateInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            AttendanceState  Valide_AttendanceState = this._Fixture.Create<AttendanceState>();
            Valide_AttendanceState.Id = 0;
            // Many to One 
            //   
			// Trainee
			var Trainee = new TraineeTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstTrainee();
            Valide_AttendanceState.Trainee = Trainee;
						 Valide_AttendanceState.TraineeId = Trainee.Id;
			           
			// Valid_Sanction
			var Valid_Sanction = new SanctionTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstSanction();
            Valide_AttendanceState.Valid_Sanction = Valid_Sanction;
			           
			// Invalid_Sanction
			var Invalid_Sanction = new SanctionTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstSanction();
            Valide_AttendanceState.Invalid_Sanction = Invalid_Sanction;
			           
            // One to Many
            //
            return Valide_AttendanceState;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide AttendanceState can't exist</returns>
        public virtual AttendanceState CreateInValideAttendanceStateInstance()
        {
            AttendanceState attendancestate = this.CreateValideAttendanceStateInstance();
             
			// Required   
 
			attendancestate.Trainee = null;
            //Unique
			var existant_AttendanceState = this.CreateOrLouadFirstAttendanceState();
			attendancestate.Reference = existant_AttendanceState.Reference;
 
            return attendancestate;
        }


		public virtual AttendanceState CreateInValideAttendanceStateInstance_ForEdit()
        {
            AttendanceState attendancestate = this.CreateOrLouadFirstAttendanceState();
			// Required   
 
			attendancestate.Trainee = null;
            //Unique
			var existant_AttendanceState = this.CreateOrLouadFirstAttendanceState();
			attendancestate.Reference = existant_AttendanceState.Reference;
            return attendancestate;
        }

		public override void Generate_Excel_File(List<AttendanceState> generated_Data)
        {
            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/AttendanceState.xlsx";

            var DataTeble = (this.BLO as AttendanceStateBLO).Convert_to_DataTable(generated_Data);
            (this.BLO as AttendanceStateBLO).ExcelDAO.Save(DataTeble, FileName);
        }
    }

	public partial class AttendanceStateTestDataFactory : BaseAttendanceStateTestDataFactory{
	
		public AttendanceStateTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
