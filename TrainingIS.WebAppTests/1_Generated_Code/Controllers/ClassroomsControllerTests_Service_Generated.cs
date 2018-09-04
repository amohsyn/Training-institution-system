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
    public class BaseClassroomsControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseClassroomsControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	
		/// <summary>
        /// Find the first Classroom instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Classroom CreateOrLouadFirstClassroom(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext )
        {
            ClassroomBLO classroomBLO = new ClassroomBLO(unitOfWork,GAppContext);
           
			Classroom entity = null;
            if (classroomBLO.FindAll()?.Count > 0)
                entity = classroomBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Classroom for Test
                entity = this.CreateValideClassroomInstance(unitOfWork,GAppContext);
                classroomBLO.Save(entity);
            }
            return entity;
        }

        public virtual Classroom CreateValideClassroomInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            Classroom  Valide_Classroom = this._Fixture.Create<Classroom>();
            Valide_Classroom.Id = 0;
            // Many to One 
            //
			// ClassroomCategory
			var ClassroomCategory = new ClassroomCategoriesControllerTests_Service().CreateOrLouadFirstClassroomCategory(unitOfWork,GAppContext);
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
        public virtual Classroom CreateInValideClassroomInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            Classroom classroom = this.CreateValideClassroomInstance(unitOfWork, GAppContext);
             
			// Required   
 
			classroom.Code = null;
 
			classroom.ClassroomCategoryId = 0;
            //Unique
			var existant_Classroom = this.CreateOrLouadFirstClassroom(new UnitOfWork<TrainingISModel>(),GAppContext);
			classroom.Code = existant_Classroom.Code;
			classroom.Reference = existant_Classroom.Reference;
 
            return classroom;
        }


		public virtual Classroom CreateInValideClassroomInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            Classroom classroom = this.CreateOrLouadFirstClassroom(unitOfWork, GAppContext);
			// Required   
 
			classroom.Code = null;
 
			classroom.ClassroomCategoryId = 0;
            //Unique
			var existant_Classroom = this.CreateOrLouadFirstClassroom(new UnitOfWork<TrainingISModel>(), GAppContext);
			classroom.Code = existant_Classroom.Code;
			classroom.Reference = existant_Classroom.Reference;
            return classroom;
        }
    }

	public partial class ClassroomsControllerTests_Service : BaseClassroomsControllerTests_Service{}
}
