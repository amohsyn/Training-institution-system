using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using GApp.Core.Context;
using GApp.DAL;
using GApp.UnitTest.TestData.Enums;
using TrainingIS.DAL;
using TrainingIS.Entitie_excludes;

namespace TestData
{
    public class ApplicationUserTestDataFactory  
    {
        protected Fixture _Fixture = null;
        protected List<ApplicationUser> Data;
        protected Dictionary<ApplicationUser, DataErrorsTypes> Data_with_errors;

        protected UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        protected GAppContext GAppContext { set; get; }

        public ApplicationUserTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            this.Constructor(UnitOfWork, GAppContext);
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="UnitOfWork"></param>
        /// <param name="GAppContext"></param>
        protected virtual void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            this.UnitOfWork = UnitOfWork;
            this.GAppContext = GAppContext;

            // Create Fixture Instance
            _Fixture = new Fixture();
            _Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                    .ForEach(b => _Fixture.Behaviors.Remove(b));
            _Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        /// <summary>
        /// Get All Test Data
        /// </summary>
        /// <returns></returns>
        public virtual List<ApplicationUser> Get_TestData()
        {
            return Data ?? (Data = Generate_TestData());
        }
        protected virtual List<ApplicationUser> Generate_TestData()
        {
            return null;
        }

        public ApplicationUser CreateOrLouadFirstApplicationUser()
        {
            throw new NotImplementedException();
        }
    }
}
