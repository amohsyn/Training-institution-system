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
    public class BaseMission_Working_GroupTestDataFactory : EntityTestData<Mission_Working_Group>
    {
        public BaseMission_Working_GroupTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) 
            : base(UnitOfWork, GAppContext)
        {
        }

		protected override List<Mission_Working_Group> Generate_TestData()
        {
            List<Mission_Working_Group> Data = base.Generate_TestData();
            if(Data == null) Data = new List<Mission_Working_Group>();
            Data.Add(this.CreateValideMission_Working_GroupInstance());
            return Data;
        }
	
		/// <summary>
        /// Find the first Mission_Working_Group instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Mission_Working_Group CreateOrLouadFirstMission_Working_Group()
        {
            Mission_Working_GroupBLO mission_working_groupBLO = new Mission_Working_GroupBLO(UnitOfWork,GAppContext);
           
			Mission_Working_Group entity = null;
            if (mission_working_groupBLO.FindAll()?.Count > 0)
                entity = mission_working_groupBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Mission_Working_Group for Test
                entity = this.CreateValideMission_Working_GroupInstance();
                mission_working_groupBLO.Save(entity);
            }
            return entity;
        }

        public virtual Mission_Working_Group CreateValideMission_Working_GroupInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Mission_Working_Group  Valide_Mission_Working_Group = this._Fixture.Create<Mission_Working_Group>();
            Valide_Mission_Working_Group.Id = 0;
            // Many to One 
            //   
            // One to Many
            //
			Valide_Mission_Working_Group.WorkGroups = null;
            return Valide_Mission_Working_Group;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Mission_Working_Group can't exist</returns>
        public virtual Mission_Working_Group CreateInValideMission_Working_GroupInstance()
        {
            Mission_Working_Group mission_working_group = this.CreateValideMission_Working_GroupInstance();
             
			// Required   
 
			mission_working_group.Code = null;
 
			mission_working_group.Name = null;
            //Unique
			var existant_Mission_Working_Group = this.CreateOrLouadFirstMission_Working_Group();
			mission_working_group.Reference = existant_Mission_Working_Group.Reference;
 
            return mission_working_group;
        }


		public virtual Mission_Working_Group CreateInValideMission_Working_GroupInstance_ForEdit()
        {
            Mission_Working_Group mission_working_group = this.CreateOrLouadFirstMission_Working_Group();
			// Required   
 
			mission_working_group.Code = null;
 
			mission_working_group.Name = null;
            //Unique
			var existant_Mission_Working_Group = this.CreateOrLouadFirstMission_Working_Group();
			mission_working_group.Reference = existant_Mission_Working_Group.Reference;
            return mission_working_group;
        }
    }

	public partial class Mission_Working_GroupTestDataFactory : BaseMission_Working_GroupTestDataFactory{
	
		public Mission_Working_GroupTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}
