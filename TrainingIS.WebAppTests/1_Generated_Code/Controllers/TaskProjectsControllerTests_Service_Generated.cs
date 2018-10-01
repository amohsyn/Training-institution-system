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
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Tests.Services 
{
    public class BaseTaskProjectsControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseTaskProjectsControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	
		/// <summary>
        /// Find the first TaskProject instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual TaskProject CreateOrLouadFirstTaskProject(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext )
        {
            TaskProjectBLO taskprojectBLO = new TaskProjectBLO(unitOfWork,GAppContext);
           
			TaskProject entity = null;
            if (taskprojectBLO.FindAll()?.Count > 0)
                entity = taskprojectBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp TaskProject for Test
                entity = this.CreateValideTaskProjectInstance(unitOfWork,GAppContext);
                taskprojectBLO.Save(entity);
            }
            return entity;
        }

        public virtual TaskProject CreateValideTaskProjectInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            TaskProject  Valide_TaskProject = this._Fixture.Create<TaskProject>();
            Valide_TaskProject.Id = 0;
            // Many to One 
            //
			// Owner
			//var Owner = new OwnersControllerTests_Service().CreateOrLouadFirstOwner(unitOfWork,GAppContext);
   //         Valide_TaskProject.Owner = null;
   //         Valide_TaskProject.OwnerId = Owner.Id;
			// Project
			var Project = new ProjectsControllerTests_Service().CreateOrLouadFirstProject(unitOfWork,GAppContext);
            Valide_TaskProject.Project = null;
            Valide_TaskProject.ProjectId = Project.Id;
            // One to Many
            //
            return Valide_TaskProject;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide TaskProject can't exist</returns>
        public virtual TaskProject CreateInValideTaskProjectInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            TaskProject taskproject = this.CreateValideTaskProjectInstance(unitOfWork, GAppContext);
             
			// Required   
 
			taskproject.Owner = null;
 
			taskproject.Name = null;
 
	
            //Unique
			var existant_TaskProject = this.CreateOrLouadFirstTaskProject(new UnitOfWork<TrainingISModel>(),GAppContext);
			taskproject.Reference = existant_TaskProject.Reference;
 
            return taskproject;
        }


		public virtual TaskProject CreateInValideTaskProjectInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            TaskProject taskproject = this.CreateOrLouadFirstTaskProject(unitOfWork, GAppContext);
			// Required   
 
			taskproject.Owner = null;
 
			taskproject.Name = null;
 
            //Unique
			var existant_TaskProject = this.CreateOrLouadFirstTaskProject(new UnitOfWork<TrainingISModel>(), GAppContext);
			taskproject.Reference = existant_TaskProject.Reference;
            return taskproject;
        }
    }

	public partial class TaskProjectsControllerTests_Service : BaseTaskProjectsControllerTests_Service{}
}
