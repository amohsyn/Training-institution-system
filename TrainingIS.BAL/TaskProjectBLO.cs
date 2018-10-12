using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Exceptions;
using GApp.Models.Pages;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public partial class TaskProjectBLO
    {
        public override int Delete(TaskProject item)
        {
            var userBLO = new UserBLO(this.GAppContext);
            if ((userBLO.Is_Current_User_Has_Role(RoleBLO.Root_ROLE) || userBLO.Is_Current_User_Has_Role(RoleBLO.Admin_ROLE)))
            {
                // if the user is not owner, we change the sate ob entity to private
                if(item.Owner.UserName == this.GAppContext.Current_User_Name)
                {
                    this.Check_If_Current_User_Is_Owner(item);
                    return base.Delete(item);
                }
                else
                {
                    item.isPublic = false;
                    return this.Update(item);
                }
               
            }
            else
            {
                this.Check_If_Current_User_Is_Owner(item);
                return base.Delete(item);
            }
        }

        public override int Save(TaskProject item)
        {
            // if insert 
            if (item.Id == 0)
            {
                var UserBLO = new UserBLO(this._UnitOfWork, this.GAppContext).FindByLogin(this.GAppContext.Current_User_Name);
                item.Owner = UserBLO;
            }
            else
            {
                this.Check_If_Current_User_Is_Owner(item);
            }
            return base.Save(item);
        }

        public override IQueryable<TaskProject> Find_as_Queryable(
            FilterRequestParams filterRequestParams, 
            List<string> SearchCreteria, 
            out int totalRecords, Func<TaskProject, bool> Condition = null)
        {

            IQueryable<TaskProject> Query =  this.entityDAO.Find_WithOut_Pagination(filterRequestParams, SearchCreteria, out totalRecords);
            Func<TaskProject,bool> condition = ( task => task.Owner.UserName == this.GAppContext.Current_User_Name || task.isPublic == true);
            Query = Query.Include("Owner").Where(condition).AsQueryable();
            Query = this.entityDAO.Pagination(Query, filterRequestParams);
            return Query;
        }

        /// <summary>
        /// Return false if the user not owner and its root or admin
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool Check_If_Current_User_Is_Owner(TaskProject item)
        {
            if (item.Owner.UserName != this.GAppContext.Current_User_Name)
            {
                // [Localization]
                string msg_ex = string.Format("Vous ne pouvez pas exécuter cet opération, car vous n'est pas le propriétaire de cette objet");
                throw new GAppException(msg_ex);
            }
            return true;
        }
    }
}
