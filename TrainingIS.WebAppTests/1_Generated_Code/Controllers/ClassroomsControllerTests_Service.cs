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
using TrainingIS.WebApp.Helpers.AlertMessages;
using GApp.WebApp.Tests;
using GApp.WebApp.Manager.Views;
using TrainingIS.WebApp.Tests.TestUtilities;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.BLL.ModelsViews;

namespace TrainingIS.WebApp.Controllers.Tests
{
    public class ClassroomsControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public ClassroomsControllerTests_Service()
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
        public Classroom CreateOrLouadFirstClassroom(UnitOfWork unitOfWork)
        {
            ClassroomBLO classroomBLO = new ClassroomBLO(unitOfWork);
           
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

        public Classroom CreateValideClassroomInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            Classroom  Valide_Classroom = this._Fixture.Create<Classroom>();
            Valide_Classroom.Id = 0;
            // Many to One 
            //
			// ClassroomCategory
			var ClassroomCategory = new ClassroomCategoriesControllerTests_Service().CreateOrLouadFirstClassroomCategory(unitOfWork);
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
        public Classroom CreateInValideClassroomInstance(UnitOfWork unitOfWork = null)
        {
            Classroom classroom = this.CreateValideClassroomInstance(unitOfWork);
             
			// Required   
 
			classroom.Code = null;
 
			classroom.ClassroomCategoryId = 0;
            //Unique
			var existant_Classroom = this.CreateOrLouadFirstClassroom(new UnitOfWork());
			classroom.Code = existant_Classroom.Code;
            
            return classroom;
        }


		  public Classroom CreateInValideClassroomInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            Classroom classroom = this.CreateOrLouadFirstClassroom(unitOfWork);
             
			// Required   
 
			classroom.Code = null;
 
			classroom.ClassroomCategoryId = 0;
            //Unique
			var existant_Classroom = this.CreateOrLouadFirstClassroom(new UnitOfWork());
			classroom.Code = existant_Classroom.Code;
            
            return classroom;
        }
    }
}

