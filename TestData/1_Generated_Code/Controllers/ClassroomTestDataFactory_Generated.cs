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
    public class BaseClassroomTestDataFactory : ITestDataFactory<Classroom>
    {
        private Fixture _Fixture = null;
		protected List<Classroom> Data;
        protected Dictionary<Trainee, DataErrorsTypes> Data_with_errors;

	    protected UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        protected GAppContext GAppContext { set; get; }

		public BaseClassroomTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
		    this.UnitOfWork = UnitOfWork;
            this.GAppContext = GAppContext;

		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

		public List<Classroom> All()
        {
            return Data ?? (Data = Generate());
        }
        public virtual List<Classroom> Generate()
        {
            return null;
        }
	
		/// <summary>
        /// Find the first Classroom instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Classroom CreateOrLouadFirstClassroom()
        {
            ClassroomBLO classroomBLO = new ClassroomBLO(UnitOfWork,GAppContext);
           
			Classroom entity = null;
            if (classroomBLO.FindAll()?.Count > 0)
                entity = classroomBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Classroom for Test
                entity = this.CreateValideClassroomInstance();
                classroomBLO.Save(entity);
            }
            return entity;
        }

        public virtual Classroom CreateValideClassroomInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Classroom  Valide_Classroom = this._Fixture.Create<Classroom>();
            Valide_Classroom.Id = 0;
            // Many to One 
            //
			// ClassroomCategory
			var ClassroomCategory = new ClassroomCategoryTestDataFactory(UnitOfWork,GAppContext).CreateOrLouadFirstClassroomCategory();
            Valide_Classroom.ClassroomCategory = null;
            Valide_Classroom.ClassroomCategoryId = ClassroomCategory.Id;
            // One to Many
            //
            return Valide_Classroom;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Classroom can't exist</returns>
        public virtual Classroom CreateInValideClassroomInstance()
        {
            Classroom classroom = this.CreateValideClassroomInstance();
             
			// Required   
 
			classroom.Code = null;
 
			classroom.ClassroomCategoryId = 0;
            //Unique
			var existant_Classroom = this.CreateOrLouadFirstClassroom();
			classroom.Code = existant_Classroom.Code;
			classroom.Reference = existant_Classroom.Reference;
 
            return classroom;
        }


		public virtual Classroom CreateInValideClassroomInstance_ForEdit()
        {
            Classroom classroom = this.CreateOrLouadFirstClassroom();
			// Required   
 
			classroom.Code = null;
 
			classroom.ClassroomCategoryId = 0;
            //Unique
			var existant_Classroom = this.CreateOrLouadFirstClassroom();
			classroom.Code = existant_Classroom.Code;
			classroom.Reference = existant_Classroom.Reference;
            return classroom;
        }
    }

	public partial class ClassroomTestDataFactory : BaseClassroomTestDataFactory{
	
		public ClassroomTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
