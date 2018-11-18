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
            var entity = base.CreateValideAdministratorInstance();
            entity.Email = "CreateValideAdministratorInstance@gapp.com";
            entity.Photo = null;
            return entity;

         
        }
    }
}
