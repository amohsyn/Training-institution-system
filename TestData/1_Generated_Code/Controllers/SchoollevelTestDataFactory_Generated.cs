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
    public class BaseSchoollevelTestDataFactory : EntityTestData<Schoollevel>
    {
		protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new SchoollevelBLO(UnitOfWork, GAppContext);
        }

        public BaseSchoollevelTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<Schoollevel> Generate_TestData()
        {
            List<Schoollevel> Data = base.Generate_TestData();
            if(Data == null) Data = new List<Schoollevel>();
			Schoollevel Schoollevel = this.CreateValideSchoollevelInstance();
            Schoollevel.Reference = "ValideSchoollevelInstance";
            Data.Add(Schoollevel);
            return Data;
        }
	
		/// <summary>
        /// Find the first Schoollevel instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Schoollevel CreateOrLouadFirstSchoollevel()
        {
            SchoollevelBLO schoollevelBLO = new SchoollevelBLO(UnitOfWork,GAppContext);
           
			Schoollevel entity = null;
            if (schoollevelBLO.FindAll()?.Count > 0)
                entity = schoollevelBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Schoollevel for Test
                entity = this.CreateValideSchoollevelInstance();
                schoollevelBLO.Save(entity);
            }
            return entity;
        }

        public virtual Schoollevel CreateValideSchoollevelInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
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
        public virtual Schoollevel CreateInValideSchoollevelInstance()
        {
            Schoollevel schoollevel = this.CreateValideSchoollevelInstance();
             
			// Required   
 
			schoollevel.Code = null;
 
			schoollevel.Name = null;
            //Unique
			var existant_Schoollevel = this.CreateOrLouadFirstSchoollevel();
			schoollevel.Code = existant_Schoollevel.Code;
			schoollevel.Reference = existant_Schoollevel.Reference;
 
            return schoollevel;
        }


		public virtual Schoollevel CreateInValideSchoollevelInstance_ForEdit()
        {
            Schoollevel schoollevel = this.CreateOrLouadFirstSchoollevel();
			// Required   
 
			schoollevel.Code = null;
 
			schoollevel.Name = null;
            //Unique
			var existant_Schoollevel = this.CreateOrLouadFirstSchoollevel();
			schoollevel.Code = existant_Schoollevel.Code;
			schoollevel.Reference = existant_Schoollevel.Reference;
            return schoollevel;
        }
    }

	public partial class SchoollevelTestDataFactory : BaseSchoollevelTestDataFactory{
	
		public SchoollevelTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
