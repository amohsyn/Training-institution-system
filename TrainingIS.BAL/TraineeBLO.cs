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
    /// <summary>
    /// Trainee BLO
    /// </summary>
    public partial class TraineeBLO
    {
        #region Query
        public IQueryable<Trainee> Trainee_Active_Query()
        {
            var trainee_active_query = from trainee in this._UnitOfWork.context.Trainees
                                          where trainee.isActif == IsActifEnum.Yes
                                          select trainee;
            return trainee_active_query;

        }
        #endregion

        #region Find By
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

        #endregion

        #region CRUD

        #endregion
    }
}
