using GApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TrainingIS.DAL;
using TrainingIS.Entities;

namespace TrainingIS.BLL
{
    public partial class ControllerAppBLO
    {
        public void Update_ControllerApps(List<Type> All_Controller)
        {
            foreach (Type controller_type in All_Controller)
            {
                string code = controller_type.Name.RemoveFromEnd("Controller");
                ControllerApp controllerApp = this.FindBaseEntityByReference(code);
                if (controllerApp == null)
                {
                    controllerApp = new ControllerApp();
                    controllerApp.Code = code;
                    controllerApp.Name = code;
                    this.Save(controllerApp);
                }
                this.Add_Or_Update_Actions(controllerApp, controller_type);
            }

        }

        private void Add_Or_Update_Actions(ControllerApp ControllerApp, Type controller_type)
        {
            string state = this._UnitOfWork.context.Entry(ControllerApp).State.ToString(); ;

            ActionControllerAppBLO actionControllerAppBLO = new ActionControllerAppBLO(this._UnitOfWork, this.GAppContext);
            List<MethodInfo> Actions = controller_type
                .GetMethods()
                .Where(method => method.IsPublic)
                .Where(method => method.ReturnType == typeof(ActionResult) || method.ReturnType.IsSubclassOf(typeof(ActionResult)) )
                .Where(method => !method.IsDefined(typeof(NonActionAttribute)))
                .ToList();

            foreach (MethodInfo action in Actions)
            {
                // if action exist
                ActionControllerApp controllerApp = actionControllerAppBLO.Find_by_ControllerId_And_ActionReference(ControllerApp.Id, action.Name);
                if(controllerApp == null)
                {
                    controllerApp = new ActionControllerApp();
                    controllerApp.Code = action.Name;
                    controllerApp.Name = action.Name;
                    controllerApp.ControllerApp = ControllerApp;
                    actionControllerAppBLO.Save(controllerApp);
                }
               


            }
        }

        //private List<Type> Find_All_Controller()
        //{

        //    List<Type> Controllers_Types = new List<Type>();

        //    //// Generated Controllers
        //    //EntityService<TrainingISModel> entityService = new EntityService<TrainingISModel>();
        //    //List<string> Generated_Controllers_Codes = entityService.getAllEntities()
        //    //                                    .Select(type => type.Name.Pluralize()).ToList();
        //    //Controllers_Codes.AddRange(Controllers_Codes);

        //    // Developed Controller
        //    // [ToDo] Create generator to generate Developed Controllers

        //    return Controllers_Types;
        //}
    }
}
