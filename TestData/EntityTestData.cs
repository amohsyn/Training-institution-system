using AutoFixture;
using GApp.Core.Context;
using GApp.DAL;
using GApp.Entities;
using GApp.UnitTest.TestData;
using GApp.UnitTest.TestData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;

namespace TestData
{
    public abstract class  EntityTestData<T>  where T : BaseEntity
    {
        protected Fixture _Fixture = null;
        protected List<T> Data;
        protected Dictionary<T, DataErrorsTypes> Data_with_errors;

        protected UnitOfWork<TrainingISModel> UnitOfWork { set; get; }
        protected GAppContext GAppContext { set; get; }
 
        public EntityTestData(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
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
        public virtual List<T> Get_TestData()
        {
            return Data ?? (Data = Generate_TestData());
        }
        protected virtual List<T> Generate_TestData()
        {
            return null;
        }

    }
}
