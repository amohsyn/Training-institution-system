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
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities.ModelsViews.Trainings;
using TrainingIS.Entities.ModelsViews.GroupModelsViews;
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
    public class BaseGroupTestDataFactory : EntityTestData<Group>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new GroupBLO(UnitOfWork, GAppContext);
        }

        public BaseGroupTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<Group> Load_Data_From_ExcelFile()
        {
            List<Group> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Group.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<Group>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as GroupBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<Group> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<Group> Data = new List<Group>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Group.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/Group.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported

				// Delete Repport_File if exist
                if (!File.Exists(Repport_File))
                {
					ImportReport importReport = (this.BLO as GroupBLO).Import(firstTable, FileName);
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
								"Group",
							importReport.Number_of_inserted_erros_rows, 
							importReport.Number_of_updated_erros_rows
                           
							);
						throw new GAppException(msg_ex);
					}

					// Convert Data Table to Data
					Data = importReport.ImportedObjects.Cast<Group>().ToList();
					is_Insert_Or_Update = true;
                }

				

            }
            return Data;
        }


		/// <summary>
        /// Find the first Group instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Group CreateOrLouadFirstGroup()
        {
            GroupBLO groupBLO = new GroupBLO(UnitOfWork,GAppContext);
           
			Group entity = null;
            if (groupBLO.FindAll()?.Count > 0)
                entity = groupBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Group for Test
                entity = this.CreateValideGroupInstance();
                groupBLO.Save(entity);
            }
            return entity;
        }

        public virtual Group CreateValideGroupInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Group  Valide_Group = this._Fixture.Create<Group>();
            Valide_Group.Id = 0;
            // Many to One 
            //   
			// TrainingType
			var TrainingType = new TrainingTypeTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstTrainingType();
            Valide_Group.TrainingType = TrainingType;
						 Valide_Group.TrainingTypeId = TrainingType.Id;
			           
			// TrainingYear
			var TrainingYear = new TrainingYearTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstTrainingYear();
            Valide_Group.TrainingYear = TrainingYear;
						 Valide_Group.TrainingYearId = TrainingYear.Id;
			           
			// Specialty
			var Specialty = new SpecialtyTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstSpecialty();
            Valide_Group.Specialty = Specialty;
						 Valide_Group.SpecialtyId = Specialty.Id;
			           
			// YearStudy
			var YearStudy = new YearStudyTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstYearStudy();
            Valide_Group.YearStudy = YearStudy;
						 Valide_Group.YearStudyId = YearStudy.Id;
			           
            // One to Many
            //
			Valide_Group.Trainees = null;
            return Valide_Group;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Group can't exist</returns>
        public virtual Group CreateInValideGroupInstance()
        {
            Group group = this.CreateValideGroupInstance();
             
			// Required   
 
			group.TrainingTypeId = 0;
 
			group.TrainingYearId = 0;
 
			group.SpecialtyId = 0;
 
			group.YearStudyId = 0;
 
			group.Code = null;
            //Unique
			var existant_Group = this.CreateOrLouadFirstGroup();
			group.Reference = existant_Group.Reference;
 
            return group;
        }


		public virtual Group CreateInValideGroupInstance_ForEdit()
        {
            Group group = this.CreateOrLouadFirstGroup();
			// Required   
 
			group.TrainingTypeId = 0;
 
			group.TrainingYearId = 0;
 
			group.SpecialtyId = 0;
 
			group.YearStudyId = 0;
 
			group.Code = null;
            //Unique
			var existant_Group = this.CreateOrLouadFirstGroup();
			group.Reference = existant_Group.Reference;
            return group;
        }

		public override void Generate_Excel_File(List<Group> generated_Data)
        {
            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/Group.xlsx";

            var DataTeble = (this.BLO as GroupBLO).Convert_to_DataTable(generated_Data);
            (this.BLO as GroupBLO).ExcelDAO.Save(DataTeble, FileName);
        }
    }

	public partial class GroupTestDataFactory : BaseGroupTestDataFactory{
	
		public GroupTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
