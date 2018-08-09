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
    public class SeanceNumbersControllerTests_Service : ManagerControllerTests
    {
        private Fixture _Fixture = null;

		public SeanceNumbersControllerTests_Service()
        {
		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
	


		/// <summary>
        /// Find the first SeanceNumber instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public SeanceNumber CreateOrLouadFirstSeanceNumber(UnitOfWork unitOfWork)
        {
            SeanceNumberBLO seancenumberBLO = new SeanceNumberBLO(unitOfWork);
           
		   SeanceNumber entity = null;
            if (seancenumberBLO.FindAll()?.Count > 0)
                entity = seancenumberBLO.FindAll()?.First();
		   
		 
            if (entity == null)
            {
                // Create Temp SeanceNumber for Test
                entity = this.CreateValideSeanceNumberInstance();
                seancenumberBLO.Save(entity);
            }
            return entity;
        }

        public SeanceNumber CreateValideSeanceNumberInstance(UnitOfWork unitOfWork = null)
        {
            if(unitOfWork == null) unitOfWork = new UnitOfWork();
        
            SeanceNumber  Valide_SeanceNumber = this._Fixture.Create<SeanceNumber>();
            Valide_SeanceNumber.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_SeanceNumber;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide SeanceNumber can't exist</returns>
        public SeanceNumber CreateInValideSeanceNumberInstance(UnitOfWork unitOfWork = null)
        {
            SeanceNumber seancenumber = this.CreateValideSeanceNumberInstance(unitOfWork);
             
			// Required   
 
			seancenumber.Code = null;
 
			seancenumber.StartTime = DateTime.Now;
 
			seancenumber.EndTime = DateTime.Now;
            //Unique
			var existant_SeanceNumber = this.CreateOrLouadFirstSeanceNumber(new UnitOfWork());
			seancenumber.Code = existant_SeanceNumber.Code;
            
            return seancenumber;
        }


		  public SeanceNumber CreateInValideSeanceNumberInstance_ForEdit(UnitOfWork unitOfWork = null)
        {
            SeanceNumber seancenumber = this.CreateOrLouadFirstSeanceNumber(unitOfWork);
             
			// Required   
 
			seancenumber.Code = null;
 
			seancenumber.StartTime = DateTime.Now;
 
			seancenumber.EndTime = DateTime.Now;
            //Unique
			var existant_SeanceNumber = this.CreateOrLouadFirstSeanceNumber(new UnitOfWork());
			seancenumber.Code = existant_SeanceNumber.Code;
            
            return seancenumber;
        }
    }
}

