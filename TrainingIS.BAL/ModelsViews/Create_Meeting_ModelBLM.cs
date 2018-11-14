using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.Entities.Base;
using TrainingIS.Entities.ModelsViews;
using TrainingIS.Models.Meetings;

namespace TrainingIS.BLL.ModelsViews
{
    public partial class Form_Meeting_ModelBLM
    {
        public override void ConverTo_Form_Meeting_Model(Form_Meeting_Model Form_Meeting_Model, Meeting Meeting)
        {
            base.ConverTo_Form_Meeting_Model(Form_Meeting_Model, Meeting);

           

            if (Meeting.WorkGroup != null)
            {
                Form_Meeting_Model.WorkGroup = Meeting.WorkGroup;
                // WorkGroup
                Form_Meeting_Model.WorkGroupId = Meeting.WorkGroup.Id;

                // President
                Person President = this.GetPresident(Meeting);
                if (President != null)
                    Form_Meeting_Model.President_Name = President.GetFullName();

                // VicePresident_Name
                Person VicePresident = this.GetVicePresident(Meeting);
                if (VicePresident != null)
                    Form_Meeting_Model.VicePresident_Name = VicePresident.GetFullName();

                // Protractor_Name
                Person Protractor = this.GetProtractor(Meeting);
                if (Protractor != null)
                    Form_Meeting_Model.Protractor_Name = Protractor.GetFullName();
            }
 
        }


        private Person GetProtractor(Meeting Meeting)
        {
            Person Protractor = null;
            if (Meeting.WorkGroup.Protractor_Administrator != null)
            {
                Protractor = Meeting.WorkGroup.Protractor_Administrator;
            }
            else
            {
                if (Meeting.WorkGroup.Protractor_Former != null)
                {
                    Protractor = Meeting.WorkGroup.Protractor_Former;
                }
                else
                {
                    if (Meeting.WorkGroup.Protractor_Trainee != null)
                    {
                        Protractor = Meeting.WorkGroup.Protractor_Trainee;
                    }
                }
            }
            return Protractor;
        }

        private Person GetVicePresident(Meeting Meeting)
        {
            Person VicePresident = null;
            if (Meeting.WorkGroup.VicePresident_Administrator != null)
            {
                VicePresident = Meeting.WorkGroup.VicePresident_Administrator;
            }
            else
            {
                if (Meeting.WorkGroup.VicePresident_Former != null)
                {
                    VicePresident = Meeting.WorkGroup.VicePresident_Former;
                }
                else
                {
                    if (Meeting.WorkGroup.VicePresident_Trainee != null)
                    {
                        VicePresident = Meeting.WorkGroup.VicePresident_Trainee;
                    }
                }
            }
            return VicePresident;
        }

        private Person GetPresident(Meeting Meeting)
        {
            Person President = null;
            if (Meeting.WorkGroup.President_Administrator != null)
            {
                President = Meeting.WorkGroup.President_Administrator;
            }
            else
            {
                if (Meeting.WorkGroup.President_Former != null)
                {
                    President = Meeting.WorkGroup.President_Former;
                }
                else
                {
                    if (Meeting.WorkGroup.VicePresident_Trainee != null)
                    {
                        President = Meeting.WorkGroup.VicePresident_Trainee;
                    }
                }
            }
            return President;
        }
    }
}
