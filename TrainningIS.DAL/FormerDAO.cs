using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;

namespace TrainingIS.DAL
{
    public partial class FormerDAO
    {

        public override int Update(Former item)
        {
            if (item == null)
                throw new ArgumentNullException("entity");

            EntityState item_state = this.Context.Entry(item).State;

            // Solution 1
            //T existingEntity = this.DbSet.Find(item.Id);
            //this.Context.Entry(existingEntity).CurrentValues.SetValues(item);
            //EntityState existingEntity_state = this.Context.Entry(existingEntity).State;


            // solution 2
            //this.Context.Entry(item).State = EntityState.Added;
            this.Context.Entry(item).State = EntityState.Modified;

            return this.SaveContext();

            //try
            //{
            //    return this.Context.SaveChanges();
            //}
            //catch (DbEntityValidationException e)
            //{
            //    DbEntityValidationExceptionTreatment(e);
            //    return -1;
            //}

        }
    }
}
