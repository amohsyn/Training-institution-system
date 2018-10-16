﻿using System;
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
    public class BaseMetierTestDataFactory : ITestDataFactory<Metier>
    {
        private Fixture _Fixture = null;
		protected List<Metier> Data;
        protected Dictionary<Trainee, DataErrorsTypes> Data_with_errors;

	    protected UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        protected GAppContext GAppContext { set; get; }

		public BaseMetierTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
		    this.UnitOfWork = UnitOfWork;
            this.GAppContext = GAppContext;

		    // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

		public List<Metier> All()
        {
            return Data ?? (Data = Generate());
        }
        public virtual List<Metier> Generate()
        {
            return null;
        }
	
		/// <summary>
        /// Find the first Metier instance or create if table is emtpy
        /// </summary>
        /// <returns></returns>
        public virtual Metier CreateOrLouadFirstMetier()
        {
            MetierBLO metierBLO = new MetierBLO(UnitOfWork,GAppContext);
           
			Metier entity = null;
            if (metierBLO.FindAll()?.Count > 0)
                entity = metierBLO.FindAll()?.First();
		   
            if (entity == null)
            {
                // Create Temp Metier for Test
                entity = this.CreateValideMetierInstance();
                metierBLO.Save(entity);
            }
            return entity;
        }

        public virtual Metier CreateValideMetierInstance()
        {
            if(UnitOfWork == null) UnitOfWork = new UnitOfWork<TrainingISModel>();
        
            Metier  Valide_Metier = this._Fixture.Create<Metier>();
            Valide_Metier.Id = 0;
            // Many to One 
            //
            // One to Many
            //
            return Valide_Metier;
        }

        /// <summary>
        /// 
        /// </summary> 
        /// <returns>Return null if InValide Metier can't exist</returns>
        public virtual Metier CreateInValideMetierInstance()
        {
            Metier metier = this.CreateValideMetierInstance();
             
			// Required   
 
			metier.Code = null;
 
			metier.Name = null;
            //Unique
			var existant_Metier = this.CreateOrLouadFirstMetier();
			metier.Reference = existant_Metier.Reference;
 
            return metier;
        }


		public virtual Metier CreateInValideMetierInstance_ForEdit()
        {
            Metier metier = this.CreateOrLouadFirstMetier();
			// Required   
 
			metier.Code = null;
 
			metier.Name = null;
            //Unique
			var existant_Metier = this.CreateOrLouadFirstMetier();
			metier.Reference = existant_Metier.Reference;
            return metier;
        }
    }

	public partial class MetierTestDataFactory : BaseMetierTestDataFactory{
	
		public MetierTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }
	
	}
}