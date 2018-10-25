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
    public class BaseLogWorkTestDataFactory : EntityTestData<LogWork>
    {
        public BaseLogWorkTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<LogWork> Generate_TestData()
        {
            List<LogWork> Data = base.Generate_TestData();
            if(Data == null) Data = new List<LogWork>();
            Data.Add(this.CreateValideLogWorkInstance());
            return Data;
        }
	
		/// <summary>
        /// Find the first LogWork instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual LogWork CreateOrLouadFirstLogWork()
        {
            LogWorkBLO logworkBLO = new LogWorkBLO(UnitOfWork,GAppContext);
           
			LogWork entity = null;
            if (logworkBLO.FindAll()?.Count > 0)
                entity = logworkBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp LogWork for Test
                entity = this.CreateValideLogWorkInstance();
                logworkBLO.Save(entity);
            }
            return entity;
        }

        public virtual LogWork CreateValideLogWorkInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            LogWork  Valide_LogWork = this._Fixture.Create<LogWork>();
            Valide_LogWork.Id = 0;
            // Many to One 
            //   
            // One to Many
            //
            return Valide_LogWork;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide LogWork can't exist</returns>
        public virtual LogWork CreateInValideLogWorkInstance()
        {
            LogWork logwork = this.CreateValideLogWorkInstance();
             
			// Required   
 
			logwork.UserId = null;
 
			logwork.OperationWorkType = OperationWorkTypes.Import;
            //Unique
			var existant_LogWork = this.CreateOrLouadFirstLogWork();
			logwork.Reference = existant_LogWork.Reference;
 
            return logwork;
        }


		public virtual LogWork CreateInValideLogWorkInstance_ForEdit()
        {
            LogWork logwork = this.CreateOrLouadFirstLogWork();
			// Required   
 
			logwork.UserId = null;
 
			logwork.OperationWorkType = OperationWorkTypes.Import;
            //Unique
			var existant_LogWork = this.CreateOrLouadFirstLogWork();
			logwork.Reference = existant_LogWork.Reference;
            return logwork;
        }
    }

	public partial class LogWorkTestDataFactory : BaseLogWorkTestDataFactory{
	
		public LogWorkTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
