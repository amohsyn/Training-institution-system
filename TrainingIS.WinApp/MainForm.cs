using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GApp.Win.Manager;
using GApp.Win;
using App.GroupManagement.Entities;
using App.GroupManagement.Presentation.Groups;
using App.GroupManagement.BLL;
using App.DAL.Database;
using App.GroupManagement.Specialties;
using App.ProjectManagement.Entities;
using App.ProjectManagement.Presentation.ProjectCategories;
using App.ProjectManagement.BLL;
using App.ProjectManagement.Presentation.Projects;
using App.ProjectManagement.Presentation.ProjectTasks;
using GwinApp.UI;
using App.ProjectManagement.Presentation;

namespace App.WinApp
{
    public partial class MainForm : GForm
    {
        public MainForm()
        {
            InitializeComponent();
            Control ProjectManagmentDashboard = new ProjectManagementDashboardControl();
            ProjectManagmentDashboard.Dock = DockStyle.Fill;
            this.TabPageProjetManager.Controls.Add(ProjectManagmentDashboard);
        }

        private void gestionDesSpécialitésToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void gestionDesGroupesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            
        }

        private void projetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void tâchesToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void GroupesTile_Click(object sender, EventArgs e)
        {
            ManagerControl<Group, GroupForm, GroupDataGrid>
             managerForm = new ManagerControl<Group, GroupForm, GroupDataGrid>(
                 new GroupBLO(ModelContext.getContext(nameof(Group)))
                 );

            new ShowFormManager(this).ShwoForm(managerForm, "Gestion des groupes", FormWindowState.Maximized);
        }

        private void SpecialityTile_Click(object sender, EventArgs e)
        {
            ManagerControl<Specialty, SpecialtyForm, SpecialtyDataGrid>
             managerForm = new ManagerControl<Specialty, SpecialtyForm, SpecialtyDataGrid>(new SpecialtyBLO());

            new ShowFormManager(this).ShwoForm(managerForm, "Gestion des spécialité", FormWindowState.Maximized);
        }

        private void ProjectCategoriesTile_Click(object sender, EventArgs e)
        {

        }

        private void ProjectsTile_Click(object sender, EventArgs e)
        {
            ManagerControl<Project, ProjectForm, ProjectDataGrid>
                    managerForm = new
         ManagerControl<Project, ProjectForm, ProjectDataGrid>
          (new ProjectBLO());

            new ShowFormManager(this).ShwoForm(managerForm, "Gestion des projets", FormWindowState.Maximized);

        }

        private void TasksTile_Click(object sender, EventArgs e)
        {
            ManagerControl<ProjectTask, ProjectTaskForm, ProjectTaskDataGrid>
                  managerForm = new
       ManagerControl<ProjectTask, ProjectTaskForm, ProjectTaskDataGrid>
        (new ProjectTaskBLO());

            new ShowFormManager(this).ShwoForm(managerForm, "Gestion des tâches", FormWindowState.Maximized);

        }
    }
}
