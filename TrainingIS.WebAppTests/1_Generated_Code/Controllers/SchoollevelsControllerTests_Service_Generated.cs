﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class BaseSchoollevelsControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public BaseSchoollevelsControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	
		/// <summary>
        /// Find the first Schoollevel instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Schoollevel CreateOrLouadFirstSchoollevel(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext )
        {
            SchoollevelBLO schoollevelBLO = new SchoollevelBLO(unitOfWork,GAppContext);
           
			Schoollevel entity = null;
            if (schoollevelBLO.FindAll()?.Count > 0)
                entity = schoollevelBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Schoollevel for Test
                entity = this.CreateValideSchoollevelInstance(unitOfWork,GAppContext);
                schoollevelBLO.Save(entity);
            }
            return entity;
        }

        public virtual Schoollevel CreateValideSchoollevelInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork<TrainingISModel>();
        
            Schoollevel  Valide_Schoollevel = this._Fixture.Create<Schoollevel>();
            Valide_Schoollevel.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_Schoollevel;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Schoollevel can't exist</returns>
        public virtual Schoollevel CreateInValideSchoollevelInstance(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            Schoollevel schoollevel = this.CreateValideSchoollevelInstance(unitOfWork, GAppContext);
             
			// Required   
 
			schoollevel.Code = null;
 
			schoollevel.Name = null;
            //Unique
			var existant_Schoollevel = this.CreateOrLouadFirstSchoollevel(new UnitOfWork<TrainingISModel>(),GAppContext);
			schoollevel.Code = existant_Schoollevel.Code;
 
            return schoollevel;
        }


		public virtual Schoollevel CreateInValideSchoollevelInstance_ForEdit(UnitOfWork<TrainingISModel> unitOfWork, GAppContext GAppContext)
        {
            Schoollevel schoollevel = this.CreateOrLouadFirstSchoollevel(unitOfWork, GAppContext);
			// Required   
 
			schoollevel.Code = null;
 
			schoollevel.Name = null;
            //Unique
			var existant_Schoollevel = this.CreateOrLouadFirstSchoollevel(new UnitOfWork<TrainingISModel>(), GAppContext);
			schoollevel.Code = existant_Schoollevel.Code;
            return schoollevel;
        }
    }

	public partial class SchoollevelsControllerTests_Service : BaseSchoollevelsControllerTests_Service{}
}
