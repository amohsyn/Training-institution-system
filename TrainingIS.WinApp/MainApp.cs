using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.WinApp
{
    public partial class MainApp : GApp.Win.UI.Forms.GEmptyForm
    {
        public MainApp()
        {
            InitializeComponent();
        }

        private void MainApp_Load(object sender, EventArgs e)
        {
           
        }

        private void managerSpeciality_Load(object sender, EventArgs e)
        {
           
        }

        private void managerGroup_Load(object sender, EventArgs e)
        {
          
        }

        private void managerGroup_Click(object sender, EventArgs e)
        {
            new App.GroupManagement.Presentation.Groups.ShowGroupManager().ShowManager(this);
        }

        private void managerSpeciality_Click(object sender, EventArgs e)
        {
            new App.GroupManagement.Presentation.Specialties.ShowSpecialtyManager().ShowManager(this);
        }
    }
}
