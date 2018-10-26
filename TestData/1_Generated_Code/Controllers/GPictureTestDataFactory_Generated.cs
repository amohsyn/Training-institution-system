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
    public class BaseGPictureTestDataFactory : EntityTestData<GPicture>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new GPictureBLO(UnitOfWork, GAppContext);
        }

        public BaseGPictureTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<GPicture> Generate_TestData()
        {
            List<GPicture> Data = base.Generate_TestData();
            if(Data == null) Data = new List<GPicture>();
			GPicture GPicture = this.CreateValideGPictureInstance();
            GPicture.Reference = "ValideGPictureInstance";
            Data.Add(GPicture);
            return Data;
        }
	
		/// <summary>
        /// Find the first GPicture instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual GPicture CreateOrLouadFirstGPicture()
        {
            GPictureBLO gpictureBLO = new GPictureBLO(UnitOfWork,GAppContext);
           
			GPicture entity = null;
            if (gpictureBLO.FindAll()?.Count > 0)
                entity = gpictureBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp GPicture for Test
                entity = this.CreateValideGPictureInstance();
                gpictureBLO.Save(entity);
            }
            return entity;
        }

        public virtual GPicture CreateValideGPictureInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            GPicture  Valide_GPicture = this._Fixture.Create<GPicture>();
            Valide_GPicture.Id = 0;
            // Many to One 
            //   
            // One to Many
            //
            return Valide_GPicture;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide GPicture can't exist</returns>
        public virtual GPicture CreateInValideGPictureInstance()
        {
            GPicture gpicture = this.CreateValideGPictureInstance();
             
			// Required   
            //Unique
			var existant_GPicture = this.CreateOrLouadFirstGPicture();
			gpicture.Reference = existant_GPicture.Reference;
 
            return gpicture;
        }


		public virtual GPicture CreateInValideGPictureInstance_ForEdit()
        {
            GPicture gpicture = this.CreateOrLouadFirstGPicture();
			// Required   
            //Unique
			var existant_GPicture = this.CreateOrLouadFirstGPicture();
			gpicture.Reference = existant_GPicture.Reference;
            return gpicture;
        }
    }

	public partial class GPictureTestDataFactory : BaseGPictureTestDataFactory{
	
		public GPictureTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
