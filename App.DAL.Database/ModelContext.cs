﻿namespace App.DAL.Database
{
    using App.GroupManagement.Entities;
    using App.ProjectManagement.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class ModelContext : DbContext
    {

        static Dictionary<String, ModelContext> UniqueContextByEntity = new Dictionary<string, ModelContext>();


        public ModelContext()
            : base(@"data source=(LocalDb)\MSSQLLocalDB;initial catalog=TrainingIS;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
        {
        }
        // GApp.CMS
        public virtual DbSet<GApp.CMS.Entities.FiltersManager.FilterInfo> FilterInfos { get; set; }
        public virtual DbSet<GApp.CMS.Entities.FiltersManager.ManagerInfo> ManagerInfos { get; set; }
        public virtual DbSet<GApp.CMS.Entities.DashBoardManager.DashboardItem> DashboardItems { get; set; }
        public virtual DbSet<GApp.CMS.Entities.DashBoardManager.DashboardItemGroup> DashboardItemGroups { get; set; }

        // Group Management
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Specialty> Specialtys { get; set; }

        //// Project Management
        //public virtual DbSet<TaskCategory> TaskCategorys { get; set; }
        //public virtual DbSet<ProjectCategory> ProjectCategorys { get; set; }
        //public virtual DbSet<ProjectTask> ProjectTasks { get; set; }
        //public virtual DbSet<Project> Projects { get; set; }




        #region Get Unique Conrext
        /// <summary>
        /// Get the unique context by Entity Type
        /// </summary>
        /// <param name="EntityName">Entity Name</param>
        /// <returns>Modelc context instance</returns>
        public static ModelContext getContext(Type EntityType)
        {
            return getContext(EntityType.Name);

        }
        /// <summary>
        /// Get the unique context by Entity Name
        /// </summary>
        /// <param name="EntityName">Entity Name</param>
        /// <returns>Modelc context instance</returns>
        public static ModelContext getContext(string EntityName)
        {
            if (UniqueContextByEntity.ContainsKey(EntityName))
                return UniqueContextByEntity[EntityName];
            else
            {
                UniqueContextByEntity[EntityName] = new ModelContext();
                return UniqueContextByEntity[EntityName];
            }

        }
        #endregion
    }


}