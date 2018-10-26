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
    public class BaseApplicationParamTestDataFactory : EntityTestData<ApplicationParam>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new ApplicationParamBLO(UnitOfWork, GAppContext);
        }

        public BaseApplicationParamTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<ApplicationParam> Generate_TestData()
        {
            List<ApplicationParam> Data = base.Generate_TestData();
            if(Data == null) Data = new List<ApplicationParam>();
			ApplicationParam ApplicationParam = this.CreateValideApplicationParamInstance();
            ApplicationParam.Reference = "ValideApplicationParamInstance";
            Data.Add(ApplicationParam);
            return Data;
        }
	
		/// <summary>
        /// Find the first ApplicationParam instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual ApplicationParam CreateOrLouadFirstApplicationParam()
        {
            ApplicationParamBLO applicationparamBLO = new ApplicationParamBLO(UnitOfWork,GAppContext);
           
			ApplicationParam entity = null;
            if (applicationparamBLO.FindAll()?.Count > 0)
                entity = applicationparamBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp ApplicationParam for Test
                entity = this.CreateValideApplicationParamInstance();
                applicationparamBLO.Save(entity);
            }
            return entity;
        }

        public virtual ApplicationParam CreateValideApplicationParamInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            ApplicationParam  Valide_ApplicationParam = this._Fixture.Create<ApplicationParam>();
            Valide_ApplicationParam.Id = 0;
            // Many to One 
            //   
            // One to Many
            //
            return Valide_ApplicationParam;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide ApplicationParam can't exist</returns>
        public virtual ApplicationParam CreateInValideApplicationParamInstance()
        {
            ApplicationParam applicationparam = this.CreateValideApplicationParamInstance();
             
			// Required   
 
			applicationparam.Code = null;
            //Unique
			var existant_ApplicationParam = this.CreateOrLouadFirstApplicationParam();
			applicationparam.Reference = existant_ApplicationParam.Reference;
 
            return applicationparam;
        }


		public virtual ApplicationParam CreateInValideApplicationParamInstance_ForEdit()
        {
            ApplicationParam applicationparam = this.CreateOrLouadFirstApplicationParam();
			// Required   
 
			applicationparam.Code = null;
            //Unique
			var existant_ApplicationParam = this.CreateOrLouadFirstApplicationParam();
			applicationparam.Reference = existant_ApplicationParam.Reference;
            return applicationparam;
        }
    }

	public partial class ApplicationParamTestDataFactory : BaseApplicationParamTestDataFactory{
	
		public ApplicationParamTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
