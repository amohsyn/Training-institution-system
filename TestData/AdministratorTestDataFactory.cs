using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace TestData
{
    public partial class AdministratorTestDataFactory
    {
        public override Administrator CreateValideAdministratorInstance()
        {
            string reference = Guid.NewGuid().ToString();
            var entity = base.CreateValideAdministratorInstance();
            entity.Email = "CreateValideAdministratorInstance@" + reference + ".com";
            entity.Photo = null;
            return entity;

         
        }
    }
}
