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

		protected override List<Group> Generate_TestData()
        {
            List<Group> Data = base.Generate_TestData();
            if(Data == null) Data = new List<Group>();
			Group Group = this.CreateValideGroupInstance();
            Group.Reference = "ValideGroupInstance";
            Data.Add(Group);
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
    }

	public partial class GroupTestDataFactory : BaseGroupTestDataFactory{
	
		public GroupTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
