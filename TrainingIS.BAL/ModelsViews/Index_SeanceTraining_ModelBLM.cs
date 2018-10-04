using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Models.Pages;
using TrainingIS.Entities;
using TrainingIS.Models.SeanceTrainings;

namespace TrainingIS.BLL.ModelsViews
{
    public partial class Index_SeanceTraining_ModelBLM
    {
        public override Index_SeanceTraining_Model ConverTo_Index_SeanceTraining_Model(SeanceTraining SeanceTraining)
        {
            var Index_SeanceTraining = base.ConverTo_Index_SeanceTraining_Model(SeanceTraining);
            Index_SeanceTraining.Absences_Count = SeanceTraining.Absences.Count();
            Index_SeanceTraining.Absences_Description = string.Join(", ",SeanceTraining.Absences.Select(a => a.Trainee.ToString()).ToArray());
            Index_SeanceTraining.Group = SeanceTraining.SeancePlanning.Training.Group;
            Index_SeanceTraining.ModuleTraining = SeanceTraining.SeancePlanning.Training.ModuleTraining;
            Index_SeanceTraining.SeanceNumber = SeanceTraining.SeancePlanning.SeanceNumber;
            return Index_SeanceTraining;
        }

        /// <summary>
        /// Find All SeanceTraining : With AbsenceSeanceTaining, SeanceTraining, PlanifiedSeanceTraining, PrevisionSeanceTraining
        /// </summary>
        /// <param name="filterRequestParams"></param>
        /// <param name="SearchCreteria"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        //public override List<Index_SeanceTraining_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        //{
           
         
        //    SeanceTrainingBLO entityBLO = new SeanceTrainingBLO(this.UnitOfWork, this.GAppContext);
        //    SeancePlanningBLO SeancePlanningBLO = new SeancePlanningBLO(this.UnitOfWork, this.GAppContext);

        //    IQueryable<SeanceTraining> Executed_SeanceTrainings_Query = entityBLO
        //        .Find_WithOut_Pagination(filterRequestParams, SearchCreteria);


        //    IQueryable<SeanceTraining> Planified_SeanceTraining_Query = SeancePlanningBLO.Find_All_Planified_SeanceTraining(filterRequestParams, SearchCreteria);

        //    // Join Left SeanceTrainings_Query with Planified_SeanceTraining_Query

        //    // Find All Absence_SeanceTraining_Query

        //    // Join Query with Absence_SeanceTraining_Query

        //    // Find All Prevision_SeanceTaining_Query

        //    // Join Query with Prevision_SeanceTaining_Query

        //    // Apply Pagination

        //    // Converto to Index_Seance_Training_Model

        //    var list_entities = SeanceTrainings.ToList();

        //    // Converto List of Absences to List of Model
        //    List<Index_SeanceTraining_Model> ls_models = new List<Index_SeanceTraining_Model>();
        //    foreach (var entity in list_entities)
        //    {
        //        ls_models.Add(this.ConverTo_Index_SeanceTraining_Model(entity));
        //    }
        //    return ls_models;
        //}


    }
}
