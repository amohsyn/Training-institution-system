﻿using TrainingIS.Entities;
using GApp.DAL;
using System.Data.Entity;
namespace TrainingIS.DAL
{  
    public partial class AuthrorizationAppDAO : BaseDAO<AuthrorizationApp>{
        
		public AuthrorizationAppDAO(DbContext context) : base(context)
		{

        }

		public AuthrorizationAppDAO() : base(null)
		{
			this.Context = new TrainingISModel();
        }
   }
}