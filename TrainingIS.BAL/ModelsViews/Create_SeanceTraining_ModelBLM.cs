using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using TrainingIS.Models.SeanceTrainings;

namespace TrainingIS.BLL.ModelsViews
{
    public partial class Create_SeanceTraining_ModelBLM
    {
        public Create_SeanceTraining_Model CreateNew(DateTime seanceDate, Former former)
        {
            Create_SeanceTraining_Model create_SeanceTraining_Model = base.CreateNew();
            this.Form_SeanceTraining_ModelBLM.Fill(seanceDate, former, null, create_SeanceTraining_Model);
            return create_SeanceTraining_Model;
        }

        public override Create_SeanceTraining_Model ConverTo_Create_SeanceTraining_Model(SeanceTraining SeanceTraining)
        {
            Create_SeanceTraining_Model create_SeanceTraining_Model = base.ConverTo_Create_SeanceTraining_Model(SeanceTraining);
            return create_SeanceTraining_Model;
        }

        
    }
}
