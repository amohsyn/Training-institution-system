namespace TestDataGenerator
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
            this.bt_Generate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bt_Generate
            // 
            this.bt_Generate.Location = new System.Drawing.Point(12, 47);
            this.bt_Generate.Name = "bt_Generate";
            this.bt_Generate.Size = new System.Drawing.Size(161, 54);
            this.bt_Generate.TabIndex = 0;
            this.bt_Generate.Text = "Generate Test Data";
            this.bt_Generate.UseVisualStyleBackColor = true;
            this.bt_Generate.Click += new System.EventHandler(this.bt_Generate_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 239);
            this.Controls.Add(this.bt_Generate);
            this.Name = "Form1";
            this.Text = "GApp - Test data generator";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bt_Generate;
    }
}

