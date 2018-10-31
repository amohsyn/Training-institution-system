using System;
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
using TrainingIS.Models.Trainees;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;
using GApp.UnitTest.TestData;
using GApp.UnitTest.TestData.Enums;
using System.IO;
using System.Data;
using GApp.DAL.ReadExcel;
using ClosedXML.Excel;

namespace TestData
{
    public class BaseTraineeTestDataFactory : EntityTestData<Trainee>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new TraineeBLO(UnitOfWork, GAppContext);
        }

        public BaseTraineeTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<Trainee> Load_Data_From_ExcelFile()
        {
            List<Trainee> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Trainee.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<Trainee>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as TraineeBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<Trainee> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<Trainee> Data = new List<Trainee>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Trainee.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/Trainee.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported
                if (!File.Exists(Repport_File))
                {
                   
                    ImportReport importReport = (this.BLO as TraineeBLO).Import(firstTable, FileName);
                    // Save ExcelRepport file to Server
                    DataSet DataSet_report = importReport.get_DataSet_Report();
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(DataSet_report);
                        wb.SaveAs(Repport_File);
                    }
                    // Convert Data Table to Data
                    Data = importReport.ImportedObjects.Cast<Trainee>().ToList();
                    is_Insert_Or_Update = true;
                }
                else
                {
                    Data = (this.BLO as TraineeBLO).Convert_DataTable_to_List(firstTable);
                }
            }
            return Data;
        }


		/// <summary>
        /// Find the first Trainee instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Trainee CreateOrLouadFirstTrainee()
        {
            TraineeBLO traineeBLO = new TraineeBLO(UnitOfWork,GAppContext);
           
			Trainee entity = null;
            if (traineeBLO.FindAll()?.Count > 0)
                entity = traineeBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Trainee for Test
                entity = this.CreateValideTraineeInstance();
                traineeBLO.Save(entity);
            }
            return entity;
        }

        public virtual Trainee CreateValideTraineeInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Trainee  Valide_Trainee = this._Fixture.Create<Trainee>();
            Valide_Trainee.Id = 0;
            // Many to One 
            //   
			// Photo
			var Photo = new GPictureTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstGPicture();
            Valide_Trainee.Photo = Photo;
			           
			// Schoollevel
			var Schoollevel = new SchoollevelTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstSchoollevel();
            Valide_Trainee.Schoollevel = Schoollevel;
						 Valide_Trainee.SchoollevelId = Schoollevel.Id;
			           
			// Specialty
			var Specialty = new SpecialtyTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstSpecialty();
            Valide_Trainee.Specialty = Specialty;
						 Valide_Trainee.SpecialtyId = Specialty.Id;
			           
			// YearStudy
			var YearStudy = new YearStudyTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstYearStudy();
            Valide_Trainee.YearStudy = YearStudy;
						 Valide_Trainee.YearStudyId = YearStudy.Id;
			           
			// Group
			var Group = new GroupTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstGroup();
            Valide_Trainee.Group = Group;
						 Valide_Trainee.GroupId = Group.Id;
			           
			// Nationality
			var Nationality = new NationalityTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstNationality();
            Valide_Trainee.Nationality = Nationality;
						 Valide_Trainee.NationalityId = Nationality.Id;
			           
            // One to Many
            //
			Valide_Trainee.Member_To_WorkGroups = null;
			Valide_Trainee.StateOfAbseces = null;
            return Valide_Trainee;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Trainee can't exist</returns>
        public virtual Trainee CreateInValideTraineeInstance()
        {
            Trainee trainee = this.CreateValideTraineeInstance();
             
			// Required   
 
			trainee.CNE = null;
 
			trainee.GroupId = 0;
 
			trainee.FirstName = null;
 
			trainee.LastName = null;
 
			trainee.Sex = SexEnum.man;
            //Unique
			var existant_Trainee = this.CreateOrLouadFirstTrainee();
			trainee.CNE = existant_Trainee.CNE;
			trainee.CIN = existant_Trainee.CIN;
			trainee.Email = existant_Trainee.Email;
			trainee.Reference = existant_Trainee.Reference;
 
            return trainee;
        }


		public virtual Trainee CreateInValideTraineeInstance_ForEdit()
        {
            Trainee trainee = this.CreateOrLouadFirstTrainee();
			// Required   
 
			trainee.CNE = null;
 
			trainee.GroupId = 0;
 
			trainee.FirstName = null;
 
			trainee.LastName = null;
 
			trainee.Sex = SexEnum.man;
            //Unique
			var existant_Trainee = this.CreateOrLouadFirstTrainee();
			trainee.CNE = existant_Trainee.CNE;
			trainee.CIN = existant_Trainee.CIN;
			trainee.Email = existant_Trainee.Email;
			trainee.Reference = existant_Trainee.Reference;
            return trainee;
        }
    }

	public partial class TraineeTestDataFactory : BaseTraineeTestDataFactory{
	
		public TraineeTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
