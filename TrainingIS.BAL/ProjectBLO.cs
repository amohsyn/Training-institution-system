using System;
using System.Collections.Generic;
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
            this.Check_If_Current_User_Is_Owner(item);
            return base.Delete(item);
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

        public override IQueryable<Project> Find_as_Queryable(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        {
            IQueryable<Project>  Query = base.Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

            // Find only the users entities and entities with public state
            Query = Query.Where(project => project.Owner.UserName == this.GAppContext.Current_User_Name || project.isPublic);
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
