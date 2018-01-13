using GwinApp;
using GwinManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using App.BLL;
using App.Presentation.Groups;
using App.Specialties;
//using App.Presentation.Projects;
//using App.Presentation.Tasks;
using App.DAL;
using App.Entities;
using GwinApp.UI;
using GApp.Win.Manager;
using GApp.Win;
using App.Presentation.ProjectCategories;
using App.Presentation.Projects;
using App.Presentation.ProjectTasks;

namespace App.Presentation
{
    public partial class MainForm : FormApplication
    {
        public MainForm()
        {
            InitializeComponent();
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
            GwinManagerControl<Group, GroupForm, GroupDataGrid>
             managerForm = new GwinManagerControl<Group, GroupForm, GroupDataGrid>(
                 new GroupBLO(ModelContext.getContext(nameof(Group)))
                 );

            new ShowFormManager(this).ShwoForm(managerForm, "Gestion des groupes", FormWindowState.Maximized);
        }

        private void SpecialityTile_Click(object sender, EventArgs e)
        {
            GwinManagerControl<Specialty, SpecialtyForm, SpecialtyDataGrid>
             managerForm = new GwinManagerControl<Specialty, SpecialtyForm, SpecialtyDataGrid>(new SpecialtyBLO());

            new ShowFormManager(this).ShwoForm(managerForm, "Gestion des spécialité", FormWindowState.Maximized);
        }

        private void ProjectCategoriesTile_Click(object sender, EventArgs e)
        {
            GwinManagerControl<ProjectCategory, ProjectCategorieForm, ProjectCategorieDataGrid>
                        managerForm = new
              GwinManagerControl<ProjectCategory, ProjectCategorieForm, ProjectCategorieDataGrid>
              (new ProjectCategoryBLO());

            new ShowFormManager(this).ShwoForm(managerForm, "Gestion des catégories des projets", FormWindowState.Maximized);

        }

        private void ProjectsTile_Click(object sender, EventArgs e)
        {
            GwinManagerControl<Project, ProjectForm, ProjectDataGrid>
                    managerForm = new
         GwinManagerControl<Project, ProjectForm, ProjectDataGrid>
          (new ProjectBLO());

            new ShowFormManager(this).ShwoForm(managerForm, "Gestion des projets", FormWindowState.Maximized);

        }

        private void TasksTile_Click(object sender, EventArgs e)
        {
            GwinManagerControl<ProjectTask, ProjectTaskForm, ProjectTaskDataGrid>
                  managerForm = new
       GwinManagerControl<ProjectTask, ProjectTaskForm, ProjectTaskDataGrid>
        (new ProjectTaskBLO());

            new ShowFormManager(this).ShwoForm(managerForm, "Gestion des tâches", FormWindowState.Maximized);

        }
    }
}
