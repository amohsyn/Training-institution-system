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
    public class BaseProjectTestDataFactory : ITestDataFactory<Project>
    {
        private Fixture _Fixture = null;
		protected List<Project> Data;
        protected Dictionary<Trainee, DataErrorsTypes> Data_with_errors;

	    protected UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        protected GAppContext GAppContext { set; get; }

		public BaseProjectTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
		    this.UnitOfWork = UnitOfWork;
            this.GAppContext = GAppContext;

		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

		public List<Project> All()
        {
            return Data ?? (Data = Generate());
        }
        public virtual List<Project> Generate()
        {
            return null;
        }
	
		/// <summary>
        /// Find the first Project instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Project CreateOrLouadFirstProject()
        {
            ProjectBLO projectBLO = new ProjectBLO(UnitOfWork,GAppContext);
           
			Project entity = null;
            if (projectBLO.FindAll()?.Count > 0)
                entity = projectBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Project for Test
                entity = this.CreateValideProjectInstance();
                projectBLO.Save(entity);
            }
            return entity;
        }

        public virtual Project CreateValideProjectInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Project  Valide_Project = this._Fixture.Create<Project>();
            Valide_Project.Id = 0;
            // Many to One 
            //
			//// Owner
			//var Owner = new OwnerTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstOwner();
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
        public virtual Project CreateInValideProjectInstance()
        {
            Project project = this.CreateValideProjectInstance();
             
			// Required   
 
			project.Owner = null;
 
			project.Name = null;
            //Unique
			var existant_Project = this.CreateOrLouadFirstProject();
			project.Reference = existant_Project.Reference;
 
            return project;
        }


		public virtual Project CreateInValideProjectInstance_ForEdit()
        {
            Project project = this.CreateOrLouadFirstProject();
			// Required   
 
			project.Owner = null;
 
			project.Name = null;
            //Unique
			var existant_Project = this.CreateOrLouadFirstProject();
			project.Reference = existant_Project.Reference;
            return project;
        }
    }

	public partial class ProjectTestDataFactory : BaseProjectTestDataFactory{
	
		public ProjectTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
