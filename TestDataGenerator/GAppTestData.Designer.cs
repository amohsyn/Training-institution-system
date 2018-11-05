namespace TestDataGenerator
{
    partial class GAppTestData
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
            this.bt_Generate = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.bt_Update_entity_data = new System.Windows.Forms.Button();
            this.testDataFileBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Entities = new System.Windows.Forms.GroupBox();
            this.entityNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filePathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.testDataFileBindingSource)).BeginInit();
            this.Entities.SuspendLayout();
            this.SuspendLayout();
            // 
            // bt_Generate
            // 
            this.bt_Generate.Location = new System.Drawing.Point(12, 12);
            this.bt_Generate.Name = "bt_Generate";
            this.bt_Generate.Size = new System.Drawing.Size(161, 54);
            this.bt_Generate.TabIndex = 0;
            this.bt_Generate.Text = "Update All Entnties";
            this.bt_Generate.UseVisualStyleBackColor = true;
            this.bt_Generate.Click += new System.EventHandler(this.bt_Generate_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.entityNameDataGridViewTextBoxColumn,
            this.filePathDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.testDataFileBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 16);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(961, 341);
            this.dataGridView1.TabIndex = 1;
            // 
            // bt_Update_entity_data
            // 
            this.bt_Update_entity_data.Location = new System.Drawing.Point(822, 12);
            this.bt_Update_entity_data.Name = "bt_Update_entity_data";
            this.bt_Update_entity_data.Size = new System.Drawing.Size(150, 51);
            this.bt_Update_entity_data.TabIndex = 2;
            this.bt_Update_entity_data.Text = "Update Selected Entity";
            this.bt_Update_entity_data.UseVisualStyleBackColor = true;
            this.bt_Update_entity_data.Click += new System.EventHandler(this.bt_Update_entity_data_Click);
            // 
            // testDataFileBindingSource
            // 
            this.testDataFileBindingSource.DataSource = typeof(TestDataGenerator.Model.TestData_File);
            // 
            // Entities
            // 
            this.Entities.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Entities.Controls.Add(this.dataGridView1);
            this.Entities.Location = new System.Drawing.Point(12, 88);
            this.Entities.Name = "Entities";
            this.Entities.Size = new System.Drawing.Size(967, 360);
            this.Entities.TabIndex = 3;
            this.Entities.TabStop = false;
            this.Entities.Text = "Entities";
            // 
            // entityNameDataGridViewTextBoxColumn
            // 
            this.entityNameDataGridViewTextBoxColumn.DataPropertyName = "EntityName";
            this.entityNameDataGridViewTextBoxColumn.HeaderText = "EntityName";
            this.entityNameDataGridViewTextBoxColumn.Name = "entityNameDataGridViewTextBoxColumn";
            this.entityNameDataGridViewTextBoxColumn.Width = 200;
            // 
            // filePathDataGridViewTextBoxColumn
            // 
            this.filePathDataGridViewTextBoxColumn.DataPropertyName = "FilePath";
            this.filePathDataGridViewTextBoxColumn.HeaderText = "FilePath";
            this.filePathDataGridViewTextBoxColumn.Name = "filePathDataGridViewTextBoxColumn";
            this.filePathDataGridViewTextBoxColumn.Width = 600;
            // 
            // GAppTestData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 460);
            this.Controls.Add(this.Entities);
            this.Controls.Add(this.bt_Update_entity_data);
            this.Controls.Add(this.bt_Generate);
            this.Name = "GAppTestData";
            this.Text = "GApp - Test data generator";
            this.Load += new System.EventHandler(this.GAppTestData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.testDataFileBindingSource)).EndInit();
            this.Entities.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bt_Generate;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource testDataFileBindingSource;
        private System.Windows.Forms.Button bt_Update_entity_data;
        private System.Windows.Forms.GroupBox Entities;
        private System.Windows.Forms.DataGridViewTextBoxColumn entityNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn filePathDataGridViewTextBoxColumn;
    }
}

