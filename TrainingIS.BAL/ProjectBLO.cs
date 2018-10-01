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
    public partial class ProjectBLO
    {
        public override int Delete(Project item)
        {
 
            var userBLO = new UserBLO(this.GAppContext);
            if ((userBLO.Is_Current_User_Has_Role(RoleBLO.Root_ROLE) || userBLO.Is_Current_User_Has_Role(RoleBLO.Admin_ROLE)))
            {
                // if the user is not owner, we change the sate ob entity to private
                item.isPublic = false;
                return this.Update(item);
            }
            else
            {
                this.Check_If_Current_User_Is_Owner(item);
                return base.Delete(item);
            }
        }

        public override int Save(Project item)
        {
            // if insert 
            if(item.Id == 0)
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

        public override IQueryable<Project> Find_as_Queryable(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords, Func<Project, bool> Condition = null)
        {

            IQueryable<Project> Query = this.entityDAO.Find_WithOut_Pagination(filterRequestParams, SearchCreteria, out totalRecords);
            Func<Project, bool> condition = Project => Project.Owner.UserName == this.GAppContext.Current_User_Name || Project.isPublic;
            Query = Query.Include("Owner").Where(condition).AsQueryable();
            Query = this.entityDAO.Pagination(Query, filterRequestParams);
            return Query;

 
        }

        private void Check_If_Current_User_Is_Owner(Project item)
        {
           
            if (item.Owner.UserName != this.GAppContext.Current_User_Name)
            {
                // [Localization]
                string msg_ex = string.Format("Vous ne pouvez pas exécuter cet opération, car vous n'est pas le propriétaire de cette objet");
                throw new GAppException(msg_ex);
            }


        }
    }
}
