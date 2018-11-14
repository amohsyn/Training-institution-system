using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Models.WorkGroups;

namespace TrainingIS.BLL.ModelsViews
{
    /// <summary>
    /// Riason to Add a Specific Form_WorkGroup_ModelBLM : 
    /// the Form_WorkGroup_ModelBLM not support Foreign Key Witout Foreign Key Id
    /// </summary>
    public partial class Form_WorkGroup_ModelBLM
    {
        public override void ConverTo_Form_WorkGroup_Model(Form_WorkGroup_Model Form_WorkGroup_Model, WorkGroup WorkGroup)
        {
            base.ConverTo_Form_WorkGroup_Model(Form_WorkGroup_Model, WorkGroup);

            
            Form_WorkGroup_Model.President_FormerId = WorkGroup.President_Former?.Id;
            Form_WorkGroup_Model.President_AdministratorId = WorkGroup.President_Administrator?.Id;
            Form_WorkGroup_Model.President_TraineeId = WorkGroup.President_Trainee?.Id;

            Form_WorkGroup_Model.VicePresident_AdministratorId = WorkGroup.VicePresident_Administrator?.Id;
            Form_WorkGroup_Model.VicePresident_FormerId = WorkGroup.VicePresident_Former?.Id;
            Form_WorkGroup_Model.VicePresident_TraineeId = WorkGroup.VicePresident_Trainee?.Id;

            Form_WorkGroup_Model.Protractor_AdministratorId = WorkGroup.Protractor_Administrator?.Id;
            Form_WorkGroup_Model.Protractor_FormerId = WorkGroup.Protractor_Former?.Id;
            Form_WorkGroup_Model.Protractor_TraineeId = WorkGroup.Protractor_Trainee?.Id;
 
        }
      

        public override WorkGroup ConverTo_WorkGroup(Form_WorkGroup_Model Form_WorkGroup_Model)
        {
            var workGroup = base.ConverTo_WorkGroup(Form_WorkGroup_Model);

            //
            // ForeignKey without ForeignKeyId
            //
            // President
            if (Form_WorkGroup_Model.President_FormerId != null)
            {
                workGroup.President_Former = new FormerBLO(this.UnitOfWork, this.GAppContext)
                                    .FindBaseEntityByID(Convert.ToInt64(Form_WorkGroup_Model.President_FormerId));
            }
            else
            {
                var load_to_remove = workGroup.President_Former?.Id;
                workGroup.President_Former = null;
            }


            if (Form_WorkGroup_Model.President_AdministratorId != null)
                workGroup.President_Administrator = new AdministratorBLO(this.UnitOfWork, this.GAppContext)
                    .FindBaseEntityByID(Convert.ToInt64(Form_WorkGroup_Model.President_AdministratorId));
            else
            {
                var load_to_remove = workGroup.President_Administrator?.Id;
                workGroup.President_Administrator = null;
            }
               


            if (Form_WorkGroup_Model.President_TraineeId != null)
                workGroup.President_Trainee = new TraineeBLO(this.UnitOfWork, this.GAppContext)
                    .FindBaseEntityByID(Convert.ToInt64(Form_WorkGroup_Model.President_TraineeId));
            else
            {
                var load_to_remove = workGroup.President_Trainee?.Id;
                workGroup.President_Trainee = null;
            }
               

            // VicePresident
            if (Form_WorkGroup_Model.VicePresident_FormerId != null)
                workGroup.VicePresident_Former = new FormerBLO(this.UnitOfWork, this.GAppContext)
                    .FindBaseEntityByID(Convert.ToInt64(Form_WorkGroup_Model.VicePresident_FormerId));
            else
            {
                var load_to_remove = workGroup.VicePresident_Former?.Id;
                workGroup.VicePresident_Former = null;
            }
            

            if (Form_WorkGroup_Model.VicePresident_AdministratorId != null)
                workGroup.VicePresident_Administrator = new AdministratorBLO(this.UnitOfWork, this.GAppContext)
                    .FindBaseEntityByID(Convert.ToInt64(Form_WorkGroup_Model.VicePresident_AdministratorId));
            else
            {
                var load_to_remove = workGroup.VicePresident_Administrator?.Id;
                workGroup.VicePresident_Administrator = null;
            }

            if (Form_WorkGroup_Model.VicePresident_TraineeId != null)
                workGroup.VicePresident_Trainee = new TraineeBLO(this.UnitOfWork, this.GAppContext)
                    .FindBaseEntityByID(Convert.ToInt64(Form_WorkGroup_Model.VicePresident_TraineeId));
            else
            {
                var load_to_remove = workGroup.VicePresident_Trainee?.Id;
                workGroup.VicePresident_Trainee = null;
            }

            // Protractor
            if (Form_WorkGroup_Model.Protractor_FormerId != null)
                workGroup.Protractor_Former = new FormerBLO(this.UnitOfWork, this.GAppContext)
                    .FindBaseEntityByID(Convert.ToInt64(Form_WorkGroup_Model.Protractor_FormerId));
            else
            {
                var load_to_remove = workGroup.Protractor_Former?.Id;
                workGroup.Protractor_Former = null;
            }

            if (Form_WorkGroup_Model.Protractor_AdministratorId != null)
                workGroup.Protractor_Administrator = new AdministratorBLO(this.UnitOfWork, this.GAppContext)
                    .FindBaseEntityByID(Convert.ToInt64(Form_WorkGroup_Model.Protractor_AdministratorId));
            else
            {
                var load_to_remove = workGroup.Protractor_Administrator?.Id;
                workGroup.Protractor_Administrator = null;
            }


            if (Form_WorkGroup_Model.Protractor_TraineeId != null)
                workGroup.Protractor_Trainee = new TraineeBLO(this.UnitOfWork, this.GAppContext)
                    .FindBaseEntityByID(Convert.ToInt64(Form_WorkGroup_Model.Protractor_TraineeId));
            else
            {
                var load_to_remove = workGroup.Protractor_Trainee?.Id;
                workGroup.Protractor_Trainee = null;
            }


            return workGroup;
        }
    }
}
