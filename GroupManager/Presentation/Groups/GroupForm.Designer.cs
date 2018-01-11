namespace App.Presentation.Groups
{
    partial class GroupForm
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

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupForm));
            System.Windows.Forms.Label nameLabel;
            System.Windows.Forms.Label descriptionLabel;
            System.Windows.Forms.Label specialtyLabel;
            System.Windows.Forms.Label codeLabel;
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.specialtyComboBox = new System.Windows.Forms.ComboBox();
            this.specialtyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.codeTextBox = new System.Windows.Forms.TextBox();
            nameLabel = new System.Windows.Forms.Label();
            descriptionLabel = new System.Windows.Forms.Label();
            specialtyLabel = new System.Windows.Forms.Label();
            codeLabel = new System.Windows.Forms.Label();
            this.groupBoxEntity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.specialtyBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxEntity
            // 
            resources.ApplyResources(this.groupBoxEntity, "groupBoxEntity");
            this.groupBoxEntity.Controls.Add(codeLabel);
            this.groupBoxEntity.Controls.Add(this.codeTextBox);
            this.groupBoxEntity.Controls.Add(specialtyLabel);
            this.groupBoxEntity.Controls.Add(this.specialtyComboBox);
            this.groupBoxEntity.Controls.Add(descriptionLabel);
            this.groupBoxEntity.Controls.Add(this.descriptionTextBox);
            this.groupBoxEntity.Controls.Add(nameLabel);
            this.groupBoxEntity.Controls.Add(this.nameTextBox);
            this.groupBoxEntity.Enter += new System.EventHandler(this.groupBoxEntity_Enter);
            // 
            // nameLabel
            // 
            resources.ApplyResources(nameLabel, "nameLabel");
            nameLabel.Name = "nameLabel";
            // 
            // descriptionLabel
            // 
            resources.ApplyResources(descriptionLabel, "descriptionLabel");
            descriptionLabel.Name = "descriptionLabel";
            // 
            // specialtyLabel
            // 
            resources.ApplyResources(specialtyLabel, "specialtyLabel");
            specialtyLabel.Name = "specialtyLabel";
            // 
            // codeLabel
            // 
            resources.ApplyResources(codeLabel, "codeLabel");
            codeLabel.Name = "codeLabel";
            // 
            // nameTextBox
            // 
            resources.ApplyResources(this.nameTextBox, "nameTextBox");
            this.nameTextBox.Name = "nameTextBox";
            // 
            // descriptionTextBox
            // 
            resources.ApplyResources(this.descriptionTextBox, "descriptionTextBox");
            this.descriptionTextBox.Name = "descriptionTextBox";
            // 
            // specialtyComboBox
            // 
            resources.ApplyResources(this.specialtyComboBox, "specialtyComboBox");
            this.specialtyComboBox.DataSource = this.specialtyBindingSource;
            this.specialtyComboBox.DisplayMember = "Code";
            this.specialtyComboBox.FormattingEnabled = true;
            this.specialtyComboBox.Name = "specialtyComboBox";
            this.specialtyComboBox.ValueMember = "Id";
            // 
            // specialtyBindingSource
            // 
            this.specialtyBindingSource.DataSource = typeof(App.Entities.Specialty);
            // 
            // codeTextBox
            // 
            resources.ApplyResources(this.codeTextBox, "codeTextBox");
            this.codeTextBox.Name = "codeTextBox";
            // 
            // GroupForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "GroupForm";
            this.Title = "Group";
            this.Load += new System.EventHandler(this.GroupForm_Load);
            this.groupBoxEntity.ResumeLayout(false);
            this.groupBoxEntity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.specialtyBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.ComboBox specialtyComboBox;
        private System.Windows.Forms.BindingSource specialtyBindingSource;
        private System.Windows.Forms.TextBox codeTextBox;
    }
}
