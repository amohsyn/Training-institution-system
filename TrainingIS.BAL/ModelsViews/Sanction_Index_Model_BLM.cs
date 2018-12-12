using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Models.GAppComponents;
using GApp.Models.Pages;
using TrainingIS.Entities;
using TrainingIS.Entities.ModelsViews;

namespace TrainingIS.BLL.ModelsViews
{
    public partial class Sanction_Index_ModelBLM
    {
        //public override List<Sanction_Index_Model> Find(FilterRequestParams filterRequestParams, List<string> SearchCreteria, out int totalRecords)
        //{

        //    SanctionBLO entityBLO = new SanctionBLO(this.UnitOfWork, this.GAppContext);

        //    // Filter
        //    var FilterByInfos = DataTable_GAppComponent.ParseFilterBy(filterRequestParams.FilterBy);
        //    var isLastSanction_true_filter = FilterByInfos.Where(e => e.PropertyName == "isLastSanction" && e.Value == "true").FirstOrDefault();

        //    IQueryable<Sanction> Query_Entity = entityBLO
        //            .Find_as_Queryable(filterRequestParams, SearchCreteria, out totalRecords);

        //    // Select LastSanction
        //    if (isLastSanction_true_filter  != null)
        //    {

        //        Query_Entity = Query_Entity
        //            .GroupBy(e => e.TraineeId)
        //            .Select(e => e.OrderByDescending(s => s.SanctionCategory.WorkflowOrder).First());
        //    }

        //    var list_entities = Query_Entity.ToList();

        //    // Converto List of Absences to List of Model
        //    List<Sanction_Index_Model> ls_models = new List<Sanction_Index_Model>();
        //    foreach (var entity in list_entities)
        //    {
        //        ls_models.Add(this.ConverTo_Sanction_Index_Model(entity));
        //    }
        //    return ls_models;

            
        //}
    }
}
