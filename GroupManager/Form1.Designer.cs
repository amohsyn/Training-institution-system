namespace TraineeManager
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupDataGrid1 = new App.Presentation.Groups.GroupDataGrid();
            this.groupForm1 = new App.Presentation.Groups.GroupForm();
            this.btAdd = new System.Windows.Forms.Button();
            this.btUpdate = new System.Windows.Forms.Button();
            this.Delete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // groupDataGrid1
            // 
            this.groupDataGrid1.DataSource = null;
            this.groupDataGrid1.Location = new System.Drawing.Point(12, 231);
            this.groupDataGrid1.Name = "groupDataGrid1";
            this.groupDataGrid1.Size = new System.Drawing.Size(581, 248);
            this.groupDataGrid1.TabIndex = 1;
            this.groupDataGrid1.Title = "Entities";
            // 
            // groupForm1
            // 
            this.groupForm1.BackColor = System.Drawing.Color.Transparent;
            this.groupForm1.Location = new System.Drawing.Point(12, 12);
            this.groupForm1.Name = "groupForm1";
            this.groupForm1.Size = new System.Drawing.Size(388, 213);
            this.groupForm1.TabIndex = 0;
            this.groupForm1.Title = "Group";
            // 
            // btAdd
            // 
            this.btAdd.Location = new System.Drawing.Point(688, 47);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(75, 23);
            this.btAdd.TabIndex = 2;
            this.btAdd.Text = "Add";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // btUpdate
            // 
            this.btUpdate.Location = new System.Drawing.Point(688, 76);
            this.btUpdate.Name = "btUpdate";
            this.btUpdate.Size = new System.Drawing.Size(75, 23);
            this.btUpdate.TabIndex = 3;
            this.btUpdate.Text = "Update";
            this.btUpdate.UseVisualStyleBackColor = true;
            // 
            // Delete
            // 
            this.Delete.Location = new System.Drawing.Point(688, 105);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(75, 23);
            this.Delete.TabIndex = 4;
            this.Delete.Text = "Delete";
            this.Delete.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 540);
            this.Controls.Add(this.Delete);
            this.Controls.Add(this.btUpdate);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.groupDataGrid1);
            this.Controls.Add(this.groupForm1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private App.Presentation.Groups.GroupForm groupForm1;
        private App.Presentation.Groups.GroupDataGrid groupDataGrid1;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btUpdate;
        private System.Windows.Forms.Button Delete;
    }
}

