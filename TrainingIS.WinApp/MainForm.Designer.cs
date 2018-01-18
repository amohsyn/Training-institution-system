namespace App.WinApp
{
    partial class MainForm
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
            this.metroSetTabControl1 = new MetroSet_UI.Controls.MetroSetTabControl();
            this.TabPageEstablishment = new MetroSet_UI.Child.MetroSetTabPage();
            this.SpecialityTile = new MetroSet_UI.Controls.MetroSetTile();
            this.GroupesTile = new MetroSet_UI.Controls.MetroSetTile();
            this.TabPageProjetManager = new MetroSet_UI.Child.MetroSetTabPage();
            this.TasksTile = new MetroSet_UI.Controls.MetroSetTile();
            this.ProjectsTile = new MetroSet_UI.Controls.MetroSetTile();
            this.ProjectCategoriesTile = new MetroSet_UI.Controls.MetroSetTile();
            this.metroSetTabControl1.SuspendLayout();
            this.TabPageEstablishment.SuspendLayout();
            this.TabPageProjetManager.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroSetTabControl1
            // 
            this.metroSetTabControl1.Controls.Add(this.TabPageEstablishment);
            this.metroSetTabControl1.Controls.Add(this.TabPageProjetManager);
            this.metroSetTabControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.metroSetTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroSetTabControl1.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.metroSetTabControl1.ItemSize = new System.Drawing.Size(100, 38);
            this.metroSetTabControl1.Location = new System.Drawing.Point(13, 89);
            this.metroSetTabControl1.Name = "metroSetTabControl1";
            this.metroSetTabControl1.SelectedIndex = 1;
            this.metroSetTabControl1.Size = new System.Drawing.Size(797, 268);
            this.metroSetTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.metroSetTabControl1.Speed = 0;
            this.metroSetTabControl1.Style = MetroSet_UI.Design.Style.Light;
            this.metroSetTabControl1.StyleManager = null;
            this.metroSetTabControl1.TabIndex = 1;
            this.metroSetTabControl1.TabStyle = MetroSet_UI.Enums.TabStyle.Style1;
            this.metroSetTabControl1.ThemeAuthor = "Narwin";
            this.metroSetTabControl1.ThemeName = "MetroLite";
            this.metroSetTabControl1.UseAnimation = false;
            // 
            // TabPageEstablishment
            // 
            this.TabPageEstablishment.BaseColor = System.Drawing.Color.White;
            this.TabPageEstablishment.Controls.Add(this.SpecialityTile);
            this.TabPageEstablishment.Controls.Add(this.GroupesTile);
            this.TabPageEstablishment.ImageIndex = 0;
            this.TabPageEstablishment.ImageKey = null;
            this.TabPageEstablishment.Location = new System.Drawing.Point(4, 42);
            this.TabPageEstablishment.Name = "TabPageEstablishment";
            this.TabPageEstablishment.Size = new System.Drawing.Size(789, 222);
            this.TabPageEstablishment.Style = MetroSet_UI.Design.Style.Light;
            this.TabPageEstablishment.StyleManager = null;
            this.TabPageEstablishment.TabIndex = 0;
            this.TabPageEstablishment.Text = "Group Manager";
            this.TabPageEstablishment.ThemeAuthor = "Narwin";
            this.TabPageEstablishment.ThemeName = "MetroLite";
            this.TabPageEstablishment.ToolTipText = null;
            // 
            // SpecialityTile
            // 
            this.SpecialityTile.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.SpecialityTile.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.SpecialityTile.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.SpecialityTile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.SpecialityTile.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.SpecialityTile.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.SpecialityTile.HoverTextColor = System.Drawing.Color.White;
            this.SpecialityTile.Location = new System.Drawing.Point(10, 107);
            this.SpecialityTile.Name = "SpecialityTile";
            this.SpecialityTile.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.SpecialityTile.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.SpecialityTile.NormalTextColor = System.Drawing.Color.White;
            this.SpecialityTile.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.SpecialityTile.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.SpecialityTile.PressTextColor = System.Drawing.Color.White;
            this.SpecialityTile.Size = new System.Drawing.Size(182, 83);
            this.SpecialityTile.Style = MetroSet_UI.Design.Style.Light;
            this.SpecialityTile.StyleManager = null;
            this.SpecialityTile.TabIndex = 1;
            this.SpecialityTile.Text = "Filières";
            this.SpecialityTile.ThemeAuthor = "Narwin";
            this.SpecialityTile.ThemeName = "MetroLite";
            this.SpecialityTile.TileAlign = MetroSet_UI.Enums.TileAlign.BottmLeft;
            this.SpecialityTile.Click += new System.EventHandler(this.SpecialityTile_Click);
            // 
            // GroupesTile
            // 
            this.GroupesTile.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.GroupesTile.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.GroupesTile.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.GroupesTile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.GroupesTile.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.GroupesTile.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.GroupesTile.HoverTextColor = System.Drawing.Color.White;
            this.GroupesTile.Location = new System.Drawing.Point(10, 22);
            this.GroupesTile.Name = "GroupesTile";
            this.GroupesTile.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.GroupesTile.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.GroupesTile.NormalTextColor = System.Drawing.Color.White;
            this.GroupesTile.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.GroupesTile.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.GroupesTile.PressTextColor = System.Drawing.Color.White;
            this.GroupesTile.Size = new System.Drawing.Size(182, 79);
            this.GroupesTile.Style = MetroSet_UI.Design.Style.Light;
            this.GroupesTile.StyleManager = null;
            this.GroupesTile.TabIndex = 0;
            this.GroupesTile.Text = "Groupes";
            this.GroupesTile.ThemeAuthor = "Narwin";
            this.GroupesTile.ThemeName = "MetroLite";
            this.GroupesTile.TileAlign = MetroSet_UI.Enums.TileAlign.BottmLeft;
            this.GroupesTile.Click += new System.EventHandler(this.GroupesTile_Click);
            // 
            // TabPageProjetManager
            // 
            this.TabPageProjetManager.BaseColor = System.Drawing.Color.White;
            this.TabPageProjetManager.Controls.Add(this.TasksTile);
            this.TabPageProjetManager.Controls.Add(this.ProjectsTile);
            this.TabPageProjetManager.Controls.Add(this.ProjectCategoriesTile);
            this.TabPageProjetManager.ImageIndex = 0;
            this.TabPageProjetManager.ImageKey = null;
            this.TabPageProjetManager.Location = new System.Drawing.Point(4, 42);
            this.TabPageProjetManager.Name = "TabPageProjetManager";
            this.TabPageProjetManager.Size = new System.Drawing.Size(789, 222);
            this.TabPageProjetManager.Style = MetroSet_UI.Design.Style.Light;
            this.TabPageProjetManager.StyleManager = null;
            this.TabPageProjetManager.TabIndex = 1;
            this.TabPageProjetManager.Text = "Projects manager";
            this.TabPageProjetManager.ThemeAuthor = "Narwin";
            this.TabPageProjetManager.ThemeName = "MetroLite";
            this.TabPageProjetManager.ToolTipText = null;
            // 
            // TasksTile
            // 
            this.TasksTile.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.TasksTile.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.TasksTile.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.TasksTile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.TasksTile.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.TasksTile.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.TasksTile.HoverTextColor = System.Drawing.Color.White;
            this.TasksTile.Location = new System.Drawing.Point(191, 88);
            this.TasksTile.Name = "TasksTile";
            this.TasksTile.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.TasksTile.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.TasksTile.NormalTextColor = System.Drawing.Color.White;
            this.TasksTile.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.TasksTile.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.TasksTile.PressTextColor = System.Drawing.Color.White;
            this.TasksTile.Size = new System.Drawing.Size(182, 79);
            this.TasksTile.Style = MetroSet_UI.Design.Style.Light;
            this.TasksTile.StyleManager = null;
            this.TasksTile.TabIndex = 3;
            this.TasksTile.Text = "Tasks";
            this.TasksTile.ThemeAuthor = "Narwin";
            this.TasksTile.ThemeName = "MetroLite";
            this.TasksTile.TileAlign = MetroSet_UI.Enums.TileAlign.BottmLeft;
            this.TasksTile.Click += new System.EventHandler(this.TasksTile_Click);
            // 
            // ProjectsTile
            // 
            this.ProjectsTile.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.ProjectsTile.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.ProjectsTile.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.ProjectsTile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ProjectsTile.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.ProjectsTile.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.ProjectsTile.HoverTextColor = System.Drawing.Color.White;
            this.ProjectsTile.Location = new System.Drawing.Point(3, 88);
            this.ProjectsTile.Name = "ProjectsTile";
            this.ProjectsTile.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.ProjectsTile.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.ProjectsTile.NormalTextColor = System.Drawing.Color.White;
            this.ProjectsTile.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.ProjectsTile.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.ProjectsTile.PressTextColor = System.Drawing.Color.White;
            this.ProjectsTile.Size = new System.Drawing.Size(182, 79);
            this.ProjectsTile.Style = MetroSet_UI.Design.Style.Light;
            this.ProjectsTile.StyleManager = null;
            this.ProjectsTile.TabIndex = 2;
            this.ProjectsTile.Text = "Projects";
            this.ProjectsTile.ThemeAuthor = "Narwin";
            this.ProjectsTile.ThemeName = "MetroLite";
            this.ProjectsTile.TileAlign = MetroSet_UI.Enums.TileAlign.BottmLeft;
            this.ProjectsTile.Click += new System.EventHandler(this.ProjectsTile_Click);
            // 
            // ProjectCategoriesTile
            // 
            this.ProjectCategoriesTile.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.ProjectCategoriesTile.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(155)))), ((int)(((byte)(155)))));
            this.ProjectCategoriesTile.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            this.ProjectCategoriesTile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ProjectCategoriesTile.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.ProjectCategoriesTile.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.ProjectCategoriesTile.HoverTextColor = System.Drawing.Color.White;
            this.ProjectCategoriesTile.Location = new System.Drawing.Point(3, 3);
            this.ProjectCategoriesTile.Name = "ProjectCategoriesTile";
            this.ProjectCategoriesTile.NormalBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.ProjectCategoriesTile.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.ProjectCategoriesTile.NormalTextColor = System.Drawing.Color.White;
            this.ProjectCategoriesTile.PressBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.ProjectCategoriesTile.PressColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(177)))), ((int)(((byte)(225)))));
            this.ProjectCategoriesTile.PressTextColor = System.Drawing.Color.White;
            this.ProjectCategoriesTile.Size = new System.Drawing.Size(182, 79);
            this.ProjectCategoriesTile.Style = MetroSet_UI.Design.Style.Light;
            this.ProjectCategoriesTile.StyleManager = null;
            this.ProjectCategoriesTile.TabIndex = 1;
            this.ProjectCategoriesTile.Text = "Project categories";
            this.ProjectCategoriesTile.ThemeAuthor = "Narwin";
            this.ProjectCategoriesTile.ThemeName = "MetroLite";
            this.ProjectCategoriesTile.TileAlign = MetroSet_UI.Enums.TileAlign.BottmLeft;
            this.ProjectCategoriesTile.Click += new System.EventHandler(this.ProjectCategoriesTile_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 369);
            this.Controls.Add(this.metroSetTabControl1);
            this.Name = "MainForm";
            this.Text = "Institution Management";
            this.Controls.SetChildIndex(this.metroSetTabControl1, 0);
            this.metroSetTabControl1.ResumeLayout(false);
            this.TabPageEstablishment.ResumeLayout(false);
            this.TabPageProjetManager.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private MetroSet_UI.Controls.MetroSetTabControl metroSetTabControl1;
        private MetroSet_UI.Child.MetroSetTabPage TabPageEstablishment;
        private MetroSet_UI.Controls.MetroSetTile SpecialityTile;
        private MetroSet_UI.Controls.MetroSetTile GroupesTile;
        private MetroSet_UI.Child.MetroSetTabPage TabPageProjetManager;
        private MetroSet_UI.Controls.MetroSetTile TasksTile;
        private MetroSet_UI.Controls.MetroSetTile ProjectsTile;
        private MetroSet_UI.Controls.MetroSetTile ProjectCategoriesTile;
    }
}