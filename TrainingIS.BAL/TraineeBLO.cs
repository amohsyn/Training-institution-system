using GApp.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using GApp.BLL;
using TrainingIS.Entities.Resources.TraineeResources;
using static TrainingIS.BLL.MessagesService;

namespace TrainingIS.BLL
{
    public partial class TraineeBLO
    {

        public override string Import(DataTable dataTable)
        {
            // Chekc Reference colone existance
            string refernce_name = nameof(BaseEntity.Reference);
            string local_reference_name = refernce_name;
            if( !dataTable.Columns.Contains(refernce_name) 
                && !dataTable.Columns.Contains(local_reference_name))
            {
                string msg = "La colonne référence n'exist pas dans le fichier Excel d'import";
                throw new ImportException(msg);
            }

            return base.Import(dataTable);
        }
    }
}
