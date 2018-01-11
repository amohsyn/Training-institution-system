namespace App.Specialties
{
    partial class SpecialtyForm
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
            System.Windows.Forms.Label codeLabel;
            System.Windows.Forms.Label descriptionLabel;
            System.Windows.Forms.Label nameLabel;
            this.specialtyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.codeTextBox = new System.Windows.Forms.TextBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            codeLabel = new System.Windows.Forms.Label();
            descriptionLabel = new System.Windows.Forms.Label();
            nameLabel = new System.Windows.Forms.Label();
            this.groupBoxEntity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.specialtyBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxEntity
            // 
            this.groupBoxEntity.Controls.Add(codeLabel);
            this.groupBoxEntity.Controls.Add(this.codeTextBox);
            this.groupBoxEntity.Controls.Add(descriptionLabel);
            this.groupBoxEntity.Controls.Add(this.descriptionTextBox);
            this.groupBoxEntity.Controls.Add(nameLabel);
            this.groupBoxEntity.Controls.Add(this.nameTextBox);
            this.groupBoxEntity.Size = new System.Drawing.Size(590, 208);
            // 
            // specialtyBindingSource
            // 
            this.specialtyBindingSource.DataSource = typeof(App.Entities.Specialty);
            // 
            // codeLabel
            // 
            codeLabel.AutoSize = true;
            codeLabel.Location = new System.Drawing.Point(7, 50);
            codeLabel.Name = "codeLabel";
            codeLabel.Size = new System.Drawing.Size(35, 13);
            codeLabel.TabIndex = 0;
            codeLabel.Text = "Code:";
            // 
            // codeTextBox
            // 
            this.codeTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.specialtyBindingSource, "Code", true));
            this.codeTextBox.Location = new System.Drawing.Point(106, 47);
            this.codeTextBox.Name = "codeTextBox";
            this.codeTextBox.Size = new System.Drawing.Size(200, 20);
            this.codeTextBox.TabIndex = 1;
            // 
            // descriptionLabel
            // 
            descriptionLabel.AutoSize = true;
            descriptionLabel.Location = new System.Drawing.Point(7, 76);
            descriptionLabel.Name = "descriptionLabel";
            descriptionLabel.Size = new System.Drawing.Size(63, 13);
            descriptionLabel.TabIndex = 6;
            descriptionLabel.Text = "Description:";
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.specialtyBindingSource, "Description", true));
            this.descriptionTextBox.Location = new System.Drawing.Point(106, 73);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(473, 121);
            this.descriptionTextBox.TabIndex = 7;
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Location = new System.Drawing.Point(7, 24);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new System.Drawing.Size(38, 13);
            nameLabel.TabIndex = 10;
            nameLabel.Text = "Name:";
            // 
            // nameTextBox
            // 
            this.nameTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.specialtyBindingSource, "Name", true));
            this.nameTextBox.Location = new System.Drawing.Point(106, 21);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(473, 20);
            this.nameTextBox.TabIndex = 11;
            // 
            // SpecialtyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "SpecialtyForm";
            this.Size = new System.Drawing.Size(590, 208);
            this.groupBoxEntity.ResumeLayout(false);
            this.groupBoxEntity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.specialtyBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource specialtyBindingSource;
        private System.Windows.Forms.TextBox codeTextBox;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.TextBox nameTextBox;
    }
}
