using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entitie_excludes;
using TrainingIS.Entities;

namespace TrainingIS.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base(@"data source = (LocalDb)\MSSQLLocalDB; initial catalog = TrainingIS_Identity; integrated security = True; MultipleActiveResultSets=True;App=EntityFramework", throwIfV1Schema: false)
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
