using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GwinForm;
using App.BLL;
using App.Entities;
using App.DAL;
using DAL;

namespace App.Presentation.Groups
{
    public partial class GroupForm : GwinFormControl
    {
        public GroupForm()
        {
            InitializeComponent();
            this.specialtyComboBox.DataSource = 
                new SpecialtyBLO(ModelContext.getContext(typeof(Group)))
                .FindAll();
        }
        

        private void groupBoxEntity_Enter(object sender, EventArgs e)
        {

        }

        private void GroupForm_Load(object sender, EventArgs e)
        {
          
        }

        protected override void readEntityFromControls()
        {
            base.readEntityFromControls();
            Group group = this.Entity as Group;
            group.Specialty = this.specialtyComboBox.SelectedItem as Specialty;
        }
        protected override void writeEntityToControls()
        {
            base.writeEntityToControls();

            this.specialtyComboBox.SelectedItem = (this.Entity as Group).Specialty;
        }
    }
}
