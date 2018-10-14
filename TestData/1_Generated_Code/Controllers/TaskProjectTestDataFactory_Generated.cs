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
using TrainingIS.BLL.ModelsViews;
using GApp.UnitTest.TestData;
using GApp.UnitTest.TestData.Enums;

namespace TestData
{
    public class BaseTaskProjectTestDataFactory : ITestDataFactory<TaskProject>
    {
        private Fixture _Fixture = null;
		protected List<TaskProject> Data;
        protected Dictionary<Trainee, DataErrorsTypes> Data_with_errors;

	    protected UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        protected GAppContext GAppContext { set; get; }

		public BaseTaskProjectTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
		    this.UnitOfWork = UnitOfWork;
            this.GAppContext = GAppContext;

		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

		public List<TaskProject> All()
        {
            return Data ?? (Data = Generate());
        }
        public virtual List<TaskProject> Generate()
        {
            return null;
        }
	
		/// <summary>
        /// Find the first TaskProject instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual TaskProject CreateOrLouadFirstTaskProject()
        {
            TaskProjectBLO taskprojectBLO = new TaskProjectBLO(UnitOfWork,GAppContext);
           
			TaskProject entity = null;
            if (taskprojectBLO.FindAll()?.Count > 0)
                entity = taskprojectBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp TaskProject for Test
                entity = this.CreateValideTaskProjectInstance();
                taskprojectBLO.Save(entity);
            }
            return entity;
        }

        public virtual TaskProject CreateValideTaskProjectInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            TaskProject  Valide_TaskProject = this._Fixture.Create<TaskProject>();
            Valide_TaskProject.Id = 0;
            // Many to One 
            //
			// Owner
			//var Owner = new OwnerTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstOwner();
   //         Valide_TaskProject.Owner = null;
   //         Valide_TaskProject.OwnerId = Owner.Id;
			// Project
			var Project = new ProjectTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstProject();
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
        public virtual TaskProject CreateInValideTaskProjectInstance()
        {
            TaskProject taskproject = this.CreateValideTaskProjectInstance();
             
			// Required   
 
			taskproject.Owner = null;
 
			taskproject.Name = null;
            //Unique
			var existant_TaskProject = this.CreateOrLouadFirstTaskProject();
			taskproject.Reference = existant_TaskProject.Reference;
 
            return taskproject;
        }


		public virtual TaskProject CreateInValideTaskProjectInstance_ForEdit()
        {
            TaskProject taskproject = this.CreateOrLouadFirstTaskProject();
			// Required   
 
			taskproject.Owner = null;
 
			taskproject.Name = null;
            //Unique
			var existant_TaskProject = this.CreateOrLouadFirstTaskProject();
			taskproject.Reference = existant_TaskProject.Reference;
            return taskproject;
        }
    }

	public partial class TaskProjectTestDataFactory : BaseTaskProjectTestDataFactory{
	
		public TaskProjectTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
