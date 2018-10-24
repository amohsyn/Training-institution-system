using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.Entities.Base;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.ModelsViews
{
    public partial class Form_Meeting_ModelBLM
    {
        public override Form_Meeting_Model ConverTo_Form_Meeting_Model(Meeting meeting)
        {
            Form_Meeting_Model form_Meeting_Model = base.ConverTo_Form_Meeting_Model(meeting);

            if(meeting.WorkGroup != null)
            {
                form_Meeting_Model.WorkGroup = meeting.WorkGroup;
                // WorkGroup
                form_Meeting_Model.WorkGroupId = meeting.WorkGroup.Id;

                // President
                Person President = this.GetPresident(meeting);
                if (President != null)
                    form_Meeting_Model.President_Name = President.GetFullName();

                // VicePresident_Name
                Person VicePresident = this.GetVicePresident(meeting);
                if (VicePresident != null)
                    form_Meeting_Model.VicePresident_Name = VicePresident.GetFullName();

                // Protractor_Name
                Person Protractor = this.GetProtractor(meeting);
                if (Protractor != null)
                    form_Meeting_Model.Protractor_Name = Protractor.GetFullName();
            }
 
            return form_Meeting_Model;
        }

        private Person GetProtractor(Meeting meeting)
        {
            Person Protractor = null;
            if (meeting.WorkGroup.Protractor_Administrator != null)
            {
                Protractor = meeting.WorkGroup.Protractor_Administrator;
            }
            else
            {
                if (meeting.WorkGroup.Protractor_Former != null)
                {
                    Protractor = meeting.WorkGroup.Protractor_Former;
                }
                else
                {
                    if (meeting.WorkGroup.Protractor_Trainee != null)
                    {
                        Protractor = meeting.WorkGroup.Protractor_Trainee;
                    }
                }
            }
            return Protractor;
        }

        private Person GetVicePresident(Meeting meeting)
        {
            Person VicePresident = null;
            if (meeting.WorkGroup.VicePresident_Administrator != null)
            {
                VicePresident = meeting.WorkGroup.VicePresident_Administrator;
            }
            else
            {
                if (meeting.WorkGroup.VicePresident_Former != null)
                {
                    VicePresident = meeting.WorkGroup.VicePresident_Former;
                }
                else
                {
                    if (meeting.WorkGroup.VicePresident_Trainee != null)
                    {
                        VicePresident = meeting.WorkGroup.VicePresident_Trainee;
                    }
                }
            }
            return VicePresident;
        }

        private Person GetPresident(Meeting meeting)
        {
            Person President = null;
            if (meeting.WorkGroup.President_Administrator != null)
            {
                President = meeting.WorkGroup.President_Administrator;
            }
            else
            {
                if (meeting.WorkGroup.President_Former != null)
                {
                    President = meeting.WorkGroup.President_Former;
                }
                else
                {
                    if (meeting.WorkGroup.VicePresident_Trainee != null)
                    {
                        President = meeting.WorkGroup.VicePresident_Trainee;
                    }
                }
            }
            return President;
        }
    }
}
