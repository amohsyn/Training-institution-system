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
    public partial class TaskProjectBLO
    {
        public override int Delete(TaskProject item)
        {
            this.Check_If_Current_User_Is_Owner(item);
            return base.Delete(item);
        }

      

        public override int Save(TaskProject item)
        {
            // if insert 
            if (item.Id == 0)
            {
                var UserBLO = new UserBLO(this.GAppContext).FindByLogin(this.GAppContext.Current_User_Name);
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
            Func<TaskProject,bool> condition =  task => task.Owner.UserName == this.GAppContext.Current_User_Name || task.isPublic;

            IQueryable<TaskProject> Query = base.Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords, condition);
            
            // Find only the users entities and entities with public state
        
            return Query;
        }

        private void Check_If_Current_User_Is_Owner(TaskProject item)
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
