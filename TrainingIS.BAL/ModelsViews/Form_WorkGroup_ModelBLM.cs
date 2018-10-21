using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.Models.WorkGroups;

namespace TrainingIS.BLL.ModelsViews
{
    /// <summary>
    /// Riason to Add a Specific Form_WorkGroup_ModelBLM : 
    /// the Form_WorkGroup_ModelBLM not support Foreign Key Witout Foreign Key Id
    /// </summary>
    public partial class Form_WorkGroup_ModelBLM
    {
        public override Form_WorkGroup_Model ConverTo_Form_WorkGroup_Model(WorkGroup WorkGroup)
        {
            var model = base.ConverTo_Form_WorkGroup_Model(WorkGroup);
            model.President_FormerId = WorkGroup.President_Former?.Id;
            model.President_AdministratorId = WorkGroup.President_Administrator?.Id;
            model.President_TraineeId = WorkGroup.President_Trainee?.Id;
            return model;
        }

        public override WorkGroup ConverTo_WorkGroup(Form_WorkGroup_Model Form_WorkGroup_Model)
        {
            var workGroup = base.ConverTo_WorkGroup(Form_WorkGroup_Model);

            // ForeignKey without ForeignKeyId
            if (Form_WorkGroup_Model.President_FormerId != null)
                workGroup.President_Former = new FormerBLO(this.UnitOfWork, this.GAppContext)
                    .FindBaseEntityByID(Convert.ToInt64( Form_WorkGroup_Model.President_FormerId));

            if (Form_WorkGroup_Model.President_AdministratorId != null)
                workGroup.President_Administrator = new AdministratorBLO(this.UnitOfWork, this.GAppContext)
                    .FindBaseEntityByID(Convert.ToInt64(Form_WorkGroup_Model.President_AdministratorId));

            if (Form_WorkGroup_Model.President_TraineeId != null)
                workGroup.President_Trainee = new TraineeBLO(this.UnitOfWork, this.GAppContext)
                    .FindBaseEntityByID(Convert.ToInt64(Form_WorkGroup_Model.President_TraineeId));


            return workGroup;
        }
    }
}
