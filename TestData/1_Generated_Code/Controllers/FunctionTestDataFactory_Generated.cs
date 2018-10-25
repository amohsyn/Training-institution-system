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
    public class BaseFunctionTestDataFactory : EntityTestData<Function>
    {
        public BaseFunctionTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<Function> Generate_TestData()
        {
            List<Function> Data = base.Generate_TestData();
            if(Data == null) Data = new List<Function>();
            Data.Add(this.CreateValideFunctionInstance());
            return Data;
        }
	
		/// <summary>
        /// Find the first Function instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Function CreateOrLouadFirstFunction()
        {
            FunctionBLO functionBLO = new FunctionBLO(UnitOfWork,GAppContext);
           
			Function entity = null;
            if (functionBLO.FindAll()?.Count > 0)
                entity = functionBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Function for Test
                entity = this.CreateValideFunctionInstance();
                functionBLO.Save(entity);
            }
            return entity;
        }

        public virtual Function CreateValideFunctionInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Function  Valide_Function = this._Fixture.Create<Function>();
            Valide_Function.Id = 0;
            // Many to One 
            //   
            // One to Many
            //
            return Valide_Function;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Function can't exist</returns>
        public virtual Function CreateInValideFunctionInstance()
        {
            Function function = this.CreateValideFunctionInstance();
             
			// Required   
 
			function.Code = null;
 
			function.Name = null;
            //Unique
			var existant_Function = this.CreateOrLouadFirstFunction();
			function.Reference = existant_Function.Reference;
 
            return function;
        }


		public virtual Function CreateInValideFunctionInstance_ForEdit()
        {
            Function function = this.CreateOrLouadFirstFunction();
			// Required   
 
			function.Code = null;
 
			function.Name = null;
            //Unique
			var existant_Function = this.CreateOrLouadFirstFunction();
			function.Reference = existant_Function.Reference;
            return function;
        }
    }

	public partial class FunctionTestDataFactory : BaseFunctionTestDataFactory{
	
		public FunctionTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
