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
    public class BaseTrainingTypeTestDataFactory : EntityTestData<TrainingType>
    {
        public BaseTrainingTypeTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<TrainingType> Generate_TestData()
        {
            List<TrainingType> Data = base.Generate_TestData();
            if(Data == null) Data = new List<TrainingType>();
            Data.Add(this.CreateValideTrainingTypeInstance());
            return Data;
        }
	
		/// <summary>
        /// Find the first TrainingType instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual TrainingType CreateOrLouadFirstTrainingType()
        {
            TrainingTypeBLO trainingtypeBLO = new TrainingTypeBLO(UnitOfWork,GAppContext);
           
			TrainingType entity = null;
            if (trainingtypeBLO.FindAll()?.Count > 0)
                entity = trainingtypeBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp TrainingType for Test
                entity = this.CreateValideTrainingTypeInstance();
                trainingtypeBLO.Save(entity);
            }
            return entity;
        }

        public virtual TrainingType CreateValideTrainingTypeInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            TrainingType  Valide_TrainingType = this._Fixture.Create<TrainingType>();
            Valide_TrainingType.Id = 0;
            // Many to One 
            //   
            // One to Many
            //
            return Valide_TrainingType;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide TrainingType can't exist</returns>
        public virtual TrainingType CreateInValideTrainingTypeInstance()
        {
            TrainingType trainingtype = this.CreateValideTrainingTypeInstance();
             
			// Required   
 
			trainingtype.Code = null;
 
			trainingtype.Name = null;
            //Unique
			var existant_TrainingType = this.CreateOrLouadFirstTrainingType();
			trainingtype.Code = existant_TrainingType.Code;
			trainingtype.Reference = existant_TrainingType.Reference;
 
            return trainingtype;
        }


		public virtual TrainingType CreateInValideTrainingTypeInstance_ForEdit()
        {
            TrainingType trainingtype = this.CreateOrLouadFirstTrainingType();
			// Required   
 
			trainingtype.Code = null;
 
			trainingtype.Name = null;
            //Unique
			var existant_TrainingType = this.CreateOrLouadFirstTrainingType();
			trainingtype.Code = existant_TrainingType.Code;
			trainingtype.Reference = existant_TrainingType.Reference;
            return trainingtype;
        }
    }

	public partial class TrainingTypeTestDataFactory : BaseTrainingTypeTestDataFactory{
	
		public TrainingTypeTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
