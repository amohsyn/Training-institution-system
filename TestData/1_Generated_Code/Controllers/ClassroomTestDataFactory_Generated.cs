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
    public class BaseClassroomTestDataFactory : EntityTestData<Classroom>
    {
        public BaseClassroomTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<Classroom> Generate_TestData()
        {
            List<Classroom> Data = base.Generate_TestData();
            if(Data == null) Data = new List<Classroom>();
            Data.Add(this.CreateValideClassroomInstance());
            return Data;
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
            Valide_Classroom.ClassroomCategory = ClassroomCategory;
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
