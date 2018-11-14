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
    public partial class Details_Meeting_ModelBLM
    {
        public override Details_Meeting_Model ConverTo_Details_Meeting_Model(Meeting Meeting)
        {
            Details_Meeting_Model details_Meeting_Model = base.ConverTo_Details_Meeting_Model(Meeting);

            List<Person> people = new List<Person>();
           
            people.AddRange(Meeting.Presences_Of_Administrators);
            people.AddRange(Meeting.Presences_Of_Formers);
            people.AddRange(Meeting.Presences_Of_Trainees);
            people.AddRange(Meeting.Presences_Of_Guests_Administrators);
            people.AddRange(Meeting.Presences_Of_Guests_Formers);
            people.AddRange(Meeting.Presences_Of_Guests_Trainees);

            // President, VicePresident, Protractor
            if (Meeting.WorkGroup.President != null)
                people.Add(Meeting.WorkGroup.President);

            if (Meeting.WorkGroup.VicePresident != null)
                people.Add(Meeting.WorkGroup.VicePresident);

            if (Meeting.WorkGroup.Protractor != null)
                people.Add(Meeting.WorkGroup.Protractor);

            details_Meeting_Model.Presences = String.Join(" , ", people.Select(p => p.GetFullName()));
 
            return details_Meeting_Model;
        }
    }
}
