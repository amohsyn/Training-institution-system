using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrainingIS.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TrainingIS.Entities;
using AutoFixture;
using TrainingIS.BLL;
using TrainingIS.DAL;
using TrainingIS.WebApp.Tests.ViewModels;
using System.ComponentModel.DataAnnotations;
using GApp.WebApp.Tests;
using GApp.WebApp.Manager.Views;
using TrainingIS.WebApp.Tests.TestUtilities;
using GApp.DAL;
using GApp.Entities;
using GApp.Core.Context;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Entities.ModelsViews.Trainings;
using TrainingIS.Entities.ModelsViews.GroupModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Tests.Services 
{
    public class BaseGroupsControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseGroupsControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	
		/// <summary>
        /// Find the first Group instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Group CreateOrLouadFirstGroup(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext )
        {
            GroupBLO groupBLO = new GroupBLO(unitOfWork,GAppContext);
           
			Group entity = null;
            if (groupBLO.FindAll()?.Count > 0)
                entity = groupBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Group for Test
                entity = this.CreateValideGroupInstance(unitOfWork,GAppContext);
                groupBLO.Save(entity);
            }
            return entity;
        }

        public virtual Group CreateValideGroupInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            Group  Valide_Group = this._Fixture.Create<Group>();
            Valide_Group.Id = 0;
            // Many to One 
            //
			// Specialty
			var Specialty = new SpecialtiesControllerTests_Service().CreateOrLouadFirstSpecialty(unitOfWork,GAppContext);
            Valide_Group.Specialty = null;
            Valide_Group.SpecialtyId = Specialty.Id;
			// TrainingType
			var TrainingType = new TrainingTypesControllerTests_Service().CreateOrLouadFirstTrainingType(unitOfWork,GAppContext);
            Valide_Group.TrainingType = null;
            Valide_Group.TrainingTypeId = TrainingType.Id;
			// TrainingYear
			var TrainingYear = new TrainingYearsControllerTests_Service().CreateOrLouadFirstTrainingYear(unitOfWork,GAppContext);
            Valide_Group.TrainingYear = null;
            Valide_Group.TrainingYearId = TrainingYear.Id;
			// YearStudy
			var YearStudy = new YearStudiesControllerTests_Service().CreateOrLouadFirstYearStudy(unitOfWork,GAppContext);
            Valide_Group.YearStudy = null;
            Valide_Group.YearStudyId = YearStudy.Id;
            // One to Many
            //
            return Valide_Group;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Group can't exist</returns>
        public virtual Group CreateInValideGroupInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            Group group = this.CreateValideGroupInstance(unitOfWork, GAppContext);
             
			// Required   
 
			group.TrainingTypeId = 0;
 
			group.TrainingYearId = 0;
 
			group.SpecialtyId = 0;
 
			group.YearStudyId = 0;
 
			group.Code = null;
            //Unique
			var existant_Group = this.CreateOrLouadFirstGroup(new UnitOfWork<TrainingISModel>(),GAppContext);
 
            return group;
        }


		public virtual Group CreateInValideGroupInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            Group group = this.CreateOrLouadFirstGroup(unitOfWork, GAppContext);
			// Required   
 
			group.TrainingTypeId = 0;
 
			group.TrainingYearId = 0;
 
			group.SpecialtyId = 0;
 
			group.YearStudyId = 0;
 
			group.Code = null;
            //Unique
			var existant_Group = this.CreateOrLouadFirstGroup(new UnitOfWork<TrainingISModel>(), GAppContext);
            return group;
        }
    }

	public partial class GroupsControllerTests_Service : BaseGroupsControllerTests_Service{}
}
