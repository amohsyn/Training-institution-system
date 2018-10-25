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
using TrainingIS.Models.WorkGroups;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;
using GApp.UnitTest.TestData;
using GApp.UnitTest.TestData.Enums;

namespace TestData
{
    public class BaseWorkGroupTestDataFactory : EntityTestData<WorkGroup>
    {
        public BaseWorkGroupTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<WorkGroup> Generate_TestData()
        {
            List<WorkGroup> Data = base.Generate_TestData();
            if(Data == null) Data = new List<WorkGroup>();
            Data.Add(this.CreateValideWorkGroupInstance());
            return Data;
        }
	
		/// <summary>
        /// Find the first WorkGroup instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual WorkGroup CreateOrLouadFirstWorkGroup()
        {
            WorkGroupBLO workgroupBLO = new WorkGroupBLO(UnitOfWork,GAppContext);
           
			WorkGroup entity = null;
            if (workgroupBLO.FindAll()?.Count > 0)
                entity = workgroupBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp WorkGroup for Test
                entity = this.CreateValideWorkGroupInstance();
                workgroupBLO.Save(entity);
            }
            return entity;
        }

        public virtual WorkGroup CreateValideWorkGroupInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            WorkGroup  Valide_WorkGroup = this._Fixture.Create<WorkGroup>();
            Valide_WorkGroup.Id = 0;
            // Many to One 
            //  
            // One to Many
            //
			Valide_WorkGroup.MemebersAdministrators = null;
			Valide_WorkGroup.MemebersFormers = null;
			Valide_WorkGroup.MemebersTrainees = null;
			Valide_WorkGroup.Mission_Working_Groups = null;
            return Valide_WorkGroup;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide WorkGroup can't exist</returns>
        public virtual WorkGroup CreateInValideWorkGroupInstance()
        {
            WorkGroup workgroup = this.CreateValideWorkGroupInstance();
             
			// Required   
 
			workgroup.Name = null;
 
			workgroup.Code = null;
            //Unique
			var existant_WorkGroup = this.CreateOrLouadFirstWorkGroup();
			workgroup.Reference = existant_WorkGroup.Reference;
 
            return workgroup;
        }


		public virtual WorkGroup CreateInValideWorkGroupInstance_ForEdit()
        {
            WorkGroup workgroup = this.CreateOrLouadFirstWorkGroup();
			// Required   
 
			workgroup.Name = null;
 
			workgroup.Code = null;
            //Unique
			var existant_WorkGroup = this.CreateOrLouadFirstWorkGroup();
			workgroup.Reference = existant_WorkGroup.Reference;
            return workgroup;
        }
    }

	public partial class WorkGroupTestDataFactory : BaseWorkGroupTestDataFactory{
	
		public WorkGroupTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
