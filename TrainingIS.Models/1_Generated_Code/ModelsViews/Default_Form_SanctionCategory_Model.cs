using GApp.Core.Entities.ModelsViews;
using System; 
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Models.DataAnnotations;
using GApp.Models;
using GApp.Entities;
using TrainingIS.Entities.enums;
using TrainingIS.Entities.Resources.DisciplineCategoryResources;  
using GApp.Entities.Resources.AppResources;  
using TrainingIS.Entities.Resources.SanctionCategoryResources;  
using GApp.Entities.Resources.BaseEntity;  
 
namespace TrainingIS.Entities.ModelsViews
{
    public class Default_Form_SanctionCategory_Model : BaseModel
    {
		[Required]
		[Display(Name = "SingularName", Order = 0, ResourceType = typeof(msg_DisciplineCategory))]
		[GAppDataTable(PropertyPath = "DisciplineCategoryId", FilterBy = "DisciplineCategoryId", SearchBy = "DisciplineCategoryId", OrderBy = "DisciplineCategoryId",  AutoGenerateFilter = false,isColumn = true )]
		public Int64 DisciplineCategoryId  {set; get;}  
   
		[Required]
		[Display(Name = "Name", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Name", FilterBy = "Name", SearchBy = "Name", OrderBy = "Name",  AutoGenerateFilter = false,isColumn = true )]
		public String Name  {set; get;}  
   
		[Required]
		[Display(Name = "Code", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Code", FilterBy = "Code", SearchBy = "Code", OrderBy = "Code",  AutoGenerateFilter = false,isColumn = true )]
		public String Code  {set; get;}  
   
		[Display(Name = "DecisionAuthority", Order = 0, ResourceType = typeof(msg_SanctionCategory))]
		[GAppDataTable(PropertyPath = "DecisionAuthority", FilterBy = "DecisionAuthority", SearchBy = "DecisionAuthority", OrderBy = "DecisionAuthority",  AutoGenerateFilter = false,isColumn = true )]
		public DecisionsAuthorities DecisionAuthority  {set; get;}  
   
		[Display(Name = "WorkflowOrder", Order = 0, ResourceType = typeof(msg_SanctionCategory))]
		[GAppDataTable(PropertyPath = "WorkflowOrder", FilterBy = "WorkflowOrder", SearchBy = "WorkflowOrder", OrderBy = "WorkflowOrder",  AutoGenerateFilter = false,isColumn = true )]
		public Int32 WorkflowOrder  {set; get;}  
   
		[Display(Name = "Number_Of_Days_Of_Exclusion", Order = 0, ResourceType = typeof(msg_SanctionCategory))]
		[GAppDataTable(PropertyPath = "Number_Of_Days_Of_Exclusion", FilterBy = "Number_Of_Days_Of_Exclusion", SearchBy = "Number_Of_Days_Of_Exclusion", OrderBy = "Number_Of_Days_Of_Exclusion",  AutoGenerateFilter = false,isColumn = true )]
		public Int32 Number_Of_Days_Of_Exclusion  {set; get;}  
   
		[Display(Name = "Plurality_Of_Absences", Order = 0, ResourceType = typeof(msg_SanctionCategory))]
		[GAppDataTable(PropertyPath = "Plurality_Of_Absences", FilterBy = "Plurality_Of_Absences", SearchBy = "Plurality_Of_Absences", OrderBy = "Plurality_Of_Absences",  AutoGenerateFilter = false,isColumn = true )]
		public Int32 Plurality_Of_Absences  {set; get;}  
   
		[Display(Name = "Deducted_Points", Order = 0, ResourceType = typeof(msg_SanctionCategory))]
		[GAppDataTable(PropertyPath = "Deducted_Points", FilterBy = "Deducted_Points", SearchBy = "Deducted_Points", OrderBy = "Deducted_Points",  AutoGenerateFilter = false,isColumn = true )]
		public Int32 Deducted_Points  {set; get;}  
   
		[Display(Name = "Description", Order = 0, ResourceType = typeof(msg_app))]
		[GAppDataTable(PropertyPath = "Description", FilterBy = "Description", SearchBy = "Description", OrderBy = "Description",  AutoGenerateFilter = false,isColumn = true )]
		public String Description  {set; get;}  
   
		[Unique]
		[Display(Name = "Reference", Order = 0, ResourceType = typeof(msg_BaseEntity))]
		[GAppDataTable(PropertyPath = "Reference", FilterBy = "Reference", SearchBy = "Reference", OrderBy = "Reference",  AutoGenerateFilter = false,isColumn = false )]
		public String Reference  {set; get;}  
   
    }
}    
