using GApp.CMS.BLL.DashBoardManager;
using GApp.CMS.BLL.FiltersManager;
using GApp.Win.ApplicationManagerControl;
using GApp.Win.DashBoardControl.Area;
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
            // Init DashBoard
            GApp.Win.DashBoardControl.DashBoard dashBoardControl = new GApp.Win.DashBoardControl.DashBoard();
            dashBoardControl.Dock = DockStyle.Fill;
            this.Controls.Add(dashBoardControl);
            new DashboardItemGroupBLO().FillDashBoard(dashBoardControl);

            // ItemClick
            dashBoardControl.ItemClick += DashBoardControl_ItemClick;
        }

        private void DashBoardControl_ItemClick(object sender, EventArgs e)
        {
            if (sender is ManagerItemArea)
            {
                ManagerItemArea managerItemArea = sender as ManagerItemArea;
                AppManager appManagerControl = new AppManager();
                appManagerControl.setManagers(new ManagerInfoBLO().FindAll());

                // Select Filter 
                // 

                new GApp.Win.ManagerControl.ShowFormManager(this).ShwoForm(appManagerControl);

            }
        }
    }
}
