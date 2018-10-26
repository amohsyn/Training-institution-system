using GApp.Core.Context;
using GApp.DAL;
using GApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.DAL;

namespace TestData
{
    public class GPictureTestDataFactory : EntityTestData<GPicture>
    {
        public GPictureTestDataFactory(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext) : base(UnitOfWork, GAppContext)
        {
        }

        public GPicture CreateOrLouadFirstGPicture()
        {
            throw new NotImplementedException();
        }
    }
}
