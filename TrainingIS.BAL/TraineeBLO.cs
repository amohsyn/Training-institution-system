using GApp.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using GApp.BLL;
using TrainingIS.Entities.Resources.TraineeResources;

using TrainingIS.BLL.Resources;
using TrainingIS.DAL;
using System.ComponentModel.DataAnnotations;

using System.Linq.Expressions;

namespace TrainingIS.BLL
{
    public partial class TraineeBLO
    {
        public List<Trainee> Find_By_GroupId(long id)
        {
            List<Trainee> trainees = this._UnitOfWork.context.Trainees
                 .Where(t => t.Group != null)
                 .Where(t => t.Group.Id == id).ToList();
            return trainees;
        }

        public Trainee FindByCEF(string trainee_CEF)
        {
            return this._UnitOfWork.context.Trainees.Where(t => t.CNE == trainee_CEF).FirstOrDefault();
        }


        //public override int Save(Trainee item)
        //{

        //    var value = base.Save(item);

        //    if (item.Photo != null && !string.IsNullOrEmpty(item.Photo.Old_Reference))
        //    {
        //        GPictureBLO gPictureBLO = new GPictureBLO(this.GAppContext);

        //        // Delete the old picture
        //        gPictureBLO.Delete(item.Photo.Old_Reference);

        //        // Save the new picture
        //        gPictureBLO.Move_To_Uplpad_Directory(item.Photo.Reference);
        //    }


        //    return value;
        //}

    }
}
