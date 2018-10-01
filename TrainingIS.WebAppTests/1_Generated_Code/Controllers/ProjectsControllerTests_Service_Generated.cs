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
    public class BaseProjectsControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseProjectsControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	
		/// <summary>
        /// Find the first Project instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Project CreateOrLouadFirstProject(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext )
        {
            ProjectBLO projectBLO = new ProjectBLO(unitOfWork,GAppContext);
           
			Project entity = null;
            if (projectBLO.FindAll()?.Count > 0)
                entity = projectBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Project for Test
                entity = this.CreateValideProjectInstance(unitOfWork,GAppContext);
                projectBLO.Save(entity);
            }
            return entity;
        }

        public virtual Project CreateValideProjectInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            Project  Valide_Project = this._Fixture.Create<Project>();
            Valide_Project.Id = 0;
            // Many to One 
            //
			// Owner
			//var Owner = new OwnersControllerTests_Service().CreateOrLouadFirstOwner(unitOfWork,GAppContext);
   //         Valide_Project.Owner = null;
   //         Valide_Project.OwnerId = Owner.Id;
            // One to Many
            //
            return Valide_Project;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Project can't exist</returns>
        public virtual Project CreateInValideProjectInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            Project project = this.CreateValideProjectInstance(unitOfWork, GAppContext);
             
			// Required   
 
			project.Owner = null;
 
			project.Name = null;
 
	
            //Unique
			var existant_Project = this.CreateOrLouadFirstProject(new UnitOfWork<TrainingISModel>(),GAppContext);
			project.Reference = existant_Project.Reference;
 
            return project;
        }


		public virtual Project CreateInValideProjectInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            Project project = this.CreateOrLouadFirstProject(unitOfWork, GAppContext);
			// Required   
 
			project.Owner = null;
 
			project.Name = null;
 
	
            //Unique
			var existant_Project = this.CreateOrLouadFirstProject(new UnitOfWork<TrainingISModel>(), GAppContext);
			project.Reference = existant_Project.Reference;
            return project;
        }
    }

	public partial class ProjectsControllerTests_Service : BaseProjectsControllerTests_Service{}
}
