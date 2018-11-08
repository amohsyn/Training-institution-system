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
            this.Entities = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bt_Update_Selected = new System.Windows.Forms.Button();
            this.Data = new System.Windows.Forms.GroupBox();
            this.dataGridView_Data = new System.Windows.Forms.DataGridView();
            this.bt_prepare_data = new System.Windows.Forms.Button();
            this.entityNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filePathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.testDataFileBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.Entities.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.Data.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Data)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.testDataFileBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // bt_Generate
            // 
            this.bt_Generate.Location = new System.Drawing.Point(12, 12);
            this.bt_Generate.Name = "bt_Generate";
            this.bt_Generate.Size = new System.Drawing.Size(169, 51);
            this.bt_Generate.TabIndex = 0;
            this.bt_Generate.Text = "Insert All entities if not yet inserted";
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
            this.dataGridView1.Size = new System.Drawing.Size(417, 288);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // bt_Update_entity_data
            // 
            this.bt_Update_entity_data.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_Update_entity_data.Location = new System.Drawing.Point(784, 84);
            this.bt_Update_entity_data.Name = "bt_Update_entity_data";
            this.bt_Update_entity_data.Size = new System.Drawing.Size(192, 51);
            this.bt_Update_entity_data.TabIndex = 2;
            this.bt_Update_entity_data.Text = "Insert Selected Entities if not yet inserted";
            this.bt_Update_entity_data.UseVisualStyleBackColor = true;
            this.bt_Update_entity_data.Click += new System.EventHandler(this.bt_Update_entity_data_Click);
            // 
            // Entities
            // 
            this.Entities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Entities.Controls.Add(this.dataGridView1);
            this.Entities.Location = new System.Drawing.Point(12, 141);
            this.Entities.Name = "Entities";
            this.Entities.Size = new System.Drawing.Size(423, 307);
            this.Entities.TabIndex = 3;
            this.Entities.TabStop = false;
            this.Entities.Text = "Entities";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(335, 20);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Location = new System.Drawing.Point(15, 84);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(347, 51);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter";
            // 
            // bt_Update_Selected
            // 
            this.bt_Update_Selected.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_Update_Selected.Location = new System.Drawing.Point(586, 86);
            this.bt_Update_Selected.Name = "bt_Update_Selected";
            this.bt_Update_Selected.Size = new System.Drawing.Size(192, 51);
            this.bt_Update_Selected.TabIndex = 6;
            this.bt_Update_Selected.Text = "Update Selected Entities";
            this.bt_Update_Selected.UseVisualStyleBackColor = true;
            this.bt_Update_Selected.Click += new System.EventHandler(this.bt_Update_Selected_Click);
            // 
            // Data
            // 
            this.Data.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Data.Controls.Add(this.dataGridView_Data);
            this.Data.Location = new System.Drawing.Point(441, 143);
            this.Data.Name = "Data";
            this.Data.Size = new System.Drawing.Size(535, 302);
            this.Data.TabIndex = 7;
            this.Data.TabStop = false;
            this.Data.Text = "groupBox2";
            // 
            // dataGridView_Data
            // 
            this.dataGridView_Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Data.Location = new System.Drawing.Point(3, 16);
            this.dataGridView_Data.Name = "dataGridView_Data";
            this.dataGridView_Data.Size = new System.Drawing.Size(529, 283);
            this.dataGridView_Data.TabIndex = 0;
            // 
            // bt_prepare_data
            // 
            this.bt_prepare_data.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_prepare_data.Location = new System.Drawing.Point(586, 29);
            this.bt_prepare_data.Name = "bt_prepare_data";
            this.bt_prepare_data.Size = new System.Drawing.Size(192, 51);
            this.bt_prepare_data.TabIndex = 8;
            this.bt_prepare_data.Text = "Prepare Data";
            this.bt_prepare_data.UseVisualStyleBackColor = true;
            this.bt_prepare_data.Click += new System.EventHandler(this.bt_prepare_data_Click);
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
            // testDataFileBindingSource
            // 
            this.testDataFileBindingSource.DataSource = typeof(TestDataGenerator.Model.TestData_File);
            // 
            // GAppTestData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 460);
            this.Controls.Add(this.bt_prepare_data);
            this.Controls.Add(this.Data);
            this.Controls.Add(this.bt_Update_Selected);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Entities);
            this.Controls.Add(this.bt_Update_entity_data);
            this.Controls.Add(this.bt_Generate);
            this.Name = "GAppTestData";
            this.Text = "GApp - Test data generator";
            this.Load += new System.EventHandler(this.GAppTestData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.Entities.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Data.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Data)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.testDataFileBindingSource)).EndInit();
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
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bt_Update_Selected;
        private System.Windows.Forms.GroupBox Data;
        private System.Windows.Forms.DataGridView dataGridView_Data;
        private System.Windows.Forms.Button bt_prepare_data;
    }
}

