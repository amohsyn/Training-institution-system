namespace App.DAL.Database.Migrations
{
    using App.GroupManagement.Entities;
    using GApp.CMS.Entities.DashBoardManager;
    using GApp.CMS.Entities.FiltersManager;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<ModelContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ModelContext context)
        {

            // To Debugage
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //    System.Diagnostics.Debugger.Launch();

            // 
            // Create ManagerInfo : of group
            //
            ManagerInfo groupManagerInfo = context.ManagerInfos.Where(m => m.Reference == typeof(Group).FullName).FirstOrDefault();
            if(groupManagerInfo == null)
            {
                groupManagerInfo = new ManagerInfo();
                groupManagerInfo.Reference = typeof(Group).FullName;
                groupManagerInfo.Title = "Groups";
                groupManagerInfo.Description = "Groups";
                groupManagerInfo.isSystem = true;
                groupManagerInfo.Form_AssemblyName = "App.GroupManagement.dll";
                groupManagerInfo.Grid_AssemblyName = "App.GroupManagement.dll";
                groupManagerInfo.BLO_AssemblyName = "App.GroupManagement.dll";
                groupManagerInfo.Entity_AssemblyName = "App.GroupManagement.Entities.dll";
                groupManagerInfo.Form_FullTypeName = "App.GroupManagement.Presentation.Groups.GroupForm";
                groupManagerInfo.Gird_FullTypeName = "App.GroupManagement.Presentation.Groups.GroupDataGrid";
                groupManagerInfo.BLO_FullTypeName = "App.GroupManagement.BLL.GroupBLO";
                groupManagerInfo.Entity_FullTypeName = "App.GroupManagement.Entities.Group";
                context.ManagerInfos.AddOrUpdate(p => p.Reference, groupManagerInfo);
                context.SaveChanges();
            }
 
            // Default filter 
            FilterInfo groupManager_Default_Filter = context.FilterInfos.Where(m => m.Reference == "All Groups").FirstOrDefault();
            if(groupManager_Default_Filter == null)
            {
                groupManager_Default_Filter = new FilterInfo();
                groupManager_Default_Filter.Title = "All Groups";
                groupManager_Default_Filter.Reference = "All Groups";
                groupManager_Default_Filter.isDefaultFilter = true;
                groupManager_Default_Filter.ManagerInfo = groupManagerInfo;
                context.FilterInfos.AddOrUpdate(p => p.Reference, groupManager_Default_Filter);
                context.SaveChanges();
            }


            // 
            // Add Dashboard Group management
            //
            DashboardItemGroup group_management_group = context.DashboardItemGroups.Where(m => m.Reference == "Group_management").FirstOrDefault();
            if (group_management_group == null)
            {
                group_management_group = new DashboardItemGroup();
                group_management_group.Title = "Group management";
                group_management_group.Description = "";
                group_management_group.Reference = "Group_management";
                context.DashboardItemGroups.AddOrUpdate(p => p.Reference, group_management_group);
                context.SaveChanges();
            }


            //// items
            DashboardItem group_item = context.DashboardItems.Where(m => m.Reference == "Groups").FirstOrDefault();
            if(group_item == null)
            {
                group_item = new DashboardItem();
                group_item.Id = 3;
                group_item.Title = "Groups";
                group_item.Description = "";
                group_item.Reference = "Groups";
                group_item.FilterInfo = groupManager_Default_Filter;
                group_item.DashBoard_Group = group_management_group;
                context.DashboardItems.AddOrUpdate(p => p.Reference, group_item);
                context.SaveChanges();
            }
            


            //// Create ManagerInfo : of ManagerInfo
            //ManagerInfo managerInfo = new ManagerInfo();
            //managerInfo.Reference = typeof(ManagerInfo).FullName;
            //managerInfo.Title = "Application Managers";
            //managerInfo.Description = "Application Managers";
            //managerInfo.isSystem = true;
            //managerInfo.Form_AssemblyName = "GApp.CMS.Win.dll";
            //managerInfo.Form_FullTypeName = "GApp.CMS.Win.FiltersManager.ManagerInfoForm";
            //managerInfo.Grid_AssemblyName = "GApp.CMS.Win.dll";
            //managerInfo.Gird_FullTypeName = "GApp.CMS.Win.FiltersManager.ManagerInfoGrid";
            //managerInfo.BLO_AssemblyName = "GApp.CMS.dll";
            //managerInfo.BLO_FullTypeName = "GApp.CMS.BLL.FiltersManager.ManagerInfoBLO";
            //managerInfo.Entity_AssemblyName = "GApp.CMS.Entities.dll";
            //managerInfo.Entity_FullTypeName = "GApp.CMS.Entities.FiltersManager.ManagerInfo";
            //// Default filter 
            //FilterInfo managerInfo_Default_Filter = new FilterInfo();
            //managerInfo_Default_Filter.Title = "All Managers";
            //managerInfo_Default_Filter.isDefaultFilter = true;
            //managerInfo.FilterInfos.Add(managerInfo_Default_Filter);
            //// Save Manager Info
            //context.ManagerInfos.AddOrUpdate(p => p.Reference, managerInfo);
            //context.SaveChanges();


            //// 
            //// Add Dashboard GApp CMS Manager
            ////
            //DashboardItemGroup gapp_cms_group = new DashboardItemGroup();
            //gapp_cms_group.Title = "GApp CMS";
            //gapp_cms_group.Description = "GApp CMS Manager";
            //gapp_cms_group.Reference = "GApp_CMS_Manager";
            //context.DashboardItemGroups.AddOrUpdate(p => p.Reference, gapp_cms_group);
            //context.SaveChanges();
            //// items
            //DashboardItem ManagerInfo_dashboardItem = new DashboardItem();
            //ManagerInfo_dashboardItem.Title = "Managers";
            //ManagerInfo_dashboardItem.Reference = "Managers";
            //ManagerInfo_dashboardItem.FilterInfo = managerInfo.getDefaultFilter();
            //ManagerInfo_dashboardItem.DashBoard_Group = gapp_cms_group;
            //context.DashboardItems.AddOrUpdate(p => p.Reference, ManagerInfo_dashboardItem);
            //context.SaveChanges();





        }
    }
}
