﻿using TrainingIS.Entities;
using GApp.DAL; 
using System.Data.Entity;
using GApp.Entities;
namespace TrainingIS.DAL
{  
    public partial class WarningTraineeDAO : BaseDAO<WarningTrainee>{
        
		public WarningTraineeDAO(DbContext context) : base(context)
		{

        }

   }
}
