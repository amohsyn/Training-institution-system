namespace App.WinApp
{
    partial class MainApp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dashBoard1 = new GApp.Win.DashBoardControl.DashBoard();
            this.groupAreaTraineeManager = new GApp.Win.DashBoardControl.Area.GroupArea();
            this.managerGroup = new GApp.Win.DashBoardControl.Area.ManagerItemArea();
            this.managerSpeciality = new GApp.Win.DashBoardControl.Area.ManagerItemArea();
            this.dashBoard1.Container.SuspendLayout();
            this.dashBoard1.SuspendLayout();
            this.groupAreaTraineeManager.Container.SuspendLayout();
            this.groupAreaTraineeManager.SuspendLayout();
            this.SuspendLayout();
            // 
            // dashBoard1
            // 
            this.dashBoard1.BackColor = System.Drawing.Color.Transparent;
            // 
            // dashBoard1.draggableFlowLayoutPanel1
            // 
            this.dashBoard1.Container.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dashBoard1.Container.BackColor = System.Drawing.Color.Transparent;
            this.dashBoard1.Container.Controls.Add(this.groupAreaTraineeManager);
            this.dashBoard1.Container.Location = new System.Drawing.Point(3, 81);
            this.dashBoard1.Container.Name = "draggableFlowLayoutPanel1";
            this.dashBoard1.Container.Size = new System.Drawing.Size(1056, 407);
            this.dashBoard1.Container.TabIndex = 12;
            this.dashBoard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dashBoard1.EditeMode = false;
            this.dashBoard1.isShwoEditable = false;
            this.dashBoard1.Location = new System.Drawing.Point(0, 0);
            this.dashBoard1.Name = "dashBoard1";
            this.dashBoard1.Reference = null;
            this.dashBoard1.Size = new System.Drawing.Size(1062, 488);
            this.dashBoard1.TabIndex = 0;
            // 
            // groupAreaTraineeManager
            // 
            this.groupAreaTraineeManager.BackColor = System.Drawing.Color.Transparent;
            // 
            // groupAreaTraineeManager.dragableFlowLayoutPanel1
            // 
            this.groupAreaTraineeManager.Container.Controls.Add(this.managerGroup);
            this.groupAreaTraineeManager.Container.Controls.Add(this.managerSpeciality);
            this.groupAreaTraineeManager.Container.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupAreaTraineeManager.Container.Location = new System.Drawing.Point(0, 0);
            this.groupAreaTraineeManager.Container.Name = "dragableFlowLayoutPanel1";
            this.groupAreaTraineeManager.Container.Size = new System.Drawing.Size(400, 300);
            this.groupAreaTraineeManager.Container.TabIndex = 0;
            this.groupAreaTraineeManager.isShwoEditable = false;
            this.groupAreaTraineeManager.Location = new System.Drawing.Point(3, 3);
            this.groupAreaTraineeManager.Name = "groupAreaTraineeManager";
            this.groupAreaTraineeManager.Order = 0;
            this.groupAreaTraineeManager.Reference = "DefaultReference";
            this.groupAreaTraineeManager.Size = new System.Drawing.Size(400, 300);
            this.groupAreaTraineeManager.TabIndex = 0;
            this.groupAreaTraineeManager.Title = "Gestion des stagiaires";
            // 
            // managerGroup
            // 
            this.managerGroup.BackColor = System.Drawing.Color.Transparent;
            this.managerGroup.filterInfo = null;
            this.managerGroup.isShwoEditable = false;
            this.managerGroup.Location = new System.Drawing.Point(3, 59);
            this.managerGroup.Name = "managerGroup";
            this.managerGroup.Reference = null;
            this.managerGroup.Size = new System.Drawing.Size(100, 78);
            this.managerGroup.TabIndex = 1;
            this.managerGroup.Title = "Groupes";
            this.managerGroup.Load += new System.EventHandler(this.managerGroup_Load);
            this.managerGroup.Click += new System.EventHandler(this.managerGroup_Click);
            // 
            // managerSpeciality
            // 
            this.managerSpeciality.BackColor = System.Drawing.Color.Transparent;
            this.managerSpeciality.filterInfo = null;
            this.managerSpeciality.isShwoEditable = false;
            this.managerSpeciality.Location = new System.Drawing.Point(109, 59);
            this.managerSpeciality.Name = "managerSpeciality";
            this.managerSpeciality.Reference = null;
            this.managerSpeciality.Size = new System.Drawing.Size(100, 78);
            this.managerSpeciality.TabIndex = 2;
            this.managerSpeciality.Title = "Spécialités";
            this.managerSpeciality.Load += new System.EventHandler(this.managerSpeciality_Load);
            this.managerSpeciality.Click += new System.EventHandler(this.managerSpeciality_Click);
            // 
            // MainApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 488);
            this.Controls.Add(this.dashBoard1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "MainApp";
            this.Text = "MainApp";
            this.Load += new System.EventHandler(this.MainApp_Load);
            this.dashBoard1.Container.ResumeLayout(false);
            this.dashBoard1.ResumeLayout(false);
            this.dashBoard1.PerformLayout();
            this.groupAreaTraineeManager.Container.ResumeLayout(false);
            this.groupAreaTraineeManager.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GApp.Win.DashBoardControl.DashBoard dashBoard1;
        private GApp.Win.DashBoardControl.Area.GroupArea groupAreaTraineeManager;
        private GApp.Win.DashBoardControl.Area.ManagerItemArea managerGroup;
        private GApp.Win.DashBoardControl.Area.ManagerItemArea managerSpeciality;
    }
}