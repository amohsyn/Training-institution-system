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
    public class BaseEntityPropertyShortcutTestDataFactory : EntityTestData<EntityPropertyShortcut>
    {
		public string Entity_CRUD_Test_Reference { set; get; } 
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new EntityPropertyShortcutBLO(UnitOfWork, GAppContext);
			Entity_CRUD_Test_Reference  = "EntityPropertyShortcut_CRUD_Test";
        }

        public BaseEntityPropertyShortcutTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

 
		protected override List<EntityPropertyShortcut> Load_Data_From_ExcelFile()
        {
            List<EntityPropertyShortcut> Data = null;

            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/EntityPropertyShortcut.xlsx";

            if (File.Exists(FileName))
            {
                Data = new List<EntityPropertyShortcut>();

                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                Data = (this.BLO as EntityPropertyShortcutBLO).Convert_DataTable_to_List(firstTable);
            }
            return Data;
        }

        protected override List<EntityPropertyShortcut> Insert_Or_Update_ExcelFile_TestData(out bool is_Insert_Or_Update)
        {
            is_Insert_Or_Update = false;
            List<EntityPropertyShortcut> Data = new List<EntityPropertyShortcut>();

            // Create Paths
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/EntityPropertyShortcut.xlsx";
            string Repport_File = this.Get_Solution_Path() + "Data/Repports/EntityPropertyShortcut.xlsx";

            if (File.Exists(FileName))
            {
                // Load Data from Excel file
                var excelData = new ExcelData(FileName);
                DataTable firstTable = excelData.getFirstTable();
                // Import Data not imported

				// Delete Repport_File if exist
                if (!File.Exists(Repport_File))
                {
					ImportReport importReport = (this.BLO as EntityPropertyShortcutBLO).Import(firstTable, FileName);
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
								"EntityPropertyShortcut",
							importReport.Number_of_inserted_erros_rows, 
							importReport.Number_of_updated_erros_rows
                           
							);
						throw new GAppException(msg_ex);
					}

					// Convert Data Table to Data
					Data = importReport.ImportedObjects.Cast<EntityPropertyShortcut>().ToList();
					is_Insert_Or_Update = true;
                }

				

            }
            return Data;
        }


		/// <summary>
        /// Find the first EntityPropertyShortcut instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual EntityPropertyShortcut CreateOrLouadFirstEntityPropertyShortcut()
        {
            EntityPropertyShortcutBLO entitypropertyshortcutBLO = new EntityPropertyShortcutBLO(UnitOfWork,GAppContext);
           
			EntityPropertyShortcut entity = null;
            if (entitypropertyshortcutBLO.FindAll()?.Count > 0)
                entity = entitypropertyshortcutBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp EntityPropertyShortcut for Test
                entity = this.CreateValideEntityPropertyShortcutInstance();
                entitypropertyshortcutBLO.Save(entity);
            }
            return entity;
        }

		public virtual EntityPropertyShortcut Create_CRUD_EntityPropertyShortcut_Test_Instance()
        {
			EntityPropertyShortcut EntityPropertyShortcut = this.CreateValideEntityPropertyShortcutInstance();
            EntityPropertyShortcut.Reference = this.Entity_CRUD_Test_Reference;
            return EntityPropertyShortcut;
        }

        public virtual EntityPropertyShortcut CreateValideEntityPropertyShortcutInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            EntityPropertyShortcut  Valide_EntityPropertyShortcut = this._Fixture.Create<EntityPropertyShortcut>();
            Valide_EntityPropertyShortcut.Id = 0;
            // Many to One 
            //   
            // One to Many
            //
            return Valide_EntityPropertyShortcut;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide EntityPropertyShortcut can't exist</returns>
        public virtual EntityPropertyShortcut CreateInValideEntityPropertyShortcutInstance()
        {
            EntityPropertyShortcut entitypropertyshortcut = this.CreateValideEntityPropertyShortcutInstance();
             
			// Required   
 
			entitypropertyshortcut.EntityName = null;
 
			entitypropertyshortcut.PropertyName = null;
 
			entitypropertyshortcut.PropertyShortcutName = null;
            //Unique
			var existant_EntityPropertyShortcut = this.CreateOrLouadFirstEntityPropertyShortcut();
			entitypropertyshortcut.Reference = existant_EntityPropertyShortcut.Reference;
 
            return entitypropertyshortcut;
        }


		public virtual EntityPropertyShortcut CreateInValideEntityPropertyShortcutInstance_ForEdit()
        {
            EntityPropertyShortcut entitypropertyshortcut = this.CreateOrLouadFirstEntityPropertyShortcut();
			// Required   
 
			entitypropertyshortcut.EntityName = null;
 
			entitypropertyshortcut.PropertyName = null;
 
			entitypropertyshortcut.PropertyShortcutName = null;
            //Unique
			var existant_EntityPropertyShortcut = this.CreateOrLouadFirstEntityPropertyShortcut();
			entitypropertyshortcut.Reference = existant_EntityPropertyShortcut.Reference;
            return entitypropertyshortcut;
        }

		public override void Generate_Excel_File(List<EntityPropertyShortcut> generated_Data)
        {
            // Create Paths 
            this.Create_TestData_Files_Directory_If_Not_Exist();
            string FileName = this.Get_Solution_Path() + "Data/EntityPropertyShortcut.xlsx";

            var DataTeble = (this.BLO as EntityPropertyShortcutBLO).Convert_to_DataTable(generated_Data);
            (this.BLO as EntityPropertyShortcutBLO).ExcelDAO.Save(DataTeble, FileName);
        }
    }

	public partial class EntityPropertyShortcutTestDataFactory : BaseEntityPropertyShortcutTestDataFactory{
	
		public EntityPropertyShortcutTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
