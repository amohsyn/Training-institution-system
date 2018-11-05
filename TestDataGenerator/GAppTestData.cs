using GApp.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestDataGenerator.BLL;
using TestDataGenerator.Model;
using TestDataGenerator.TestData;
using TrainingIS.DAL;

namespace TestDataGenerator
{
    public partial class GAppTestData : Form
    {
        public GAppTestData()
        {
            InitializeComponent();
        }

        private void bt_Generate_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                TrainingISModel trainingISModel = new TrainingISModel();
                DataGenerator dataGenerator = new DataGenerator(trainingISModel);
                dataGenerator.Insert_Or_Update_Test_Data();

                this.Cursor = Cursors.Default;
            }
            catch (GAppException ex)
            {
                MessageBox.Show(ex.Message);
                this.Cursor = Cursors.Default;
            }
           
        }

        private void GAppTestData_Load(object sender, EventArgs e)
        {
            TestDataFile_BLO testDataFile_BLO = new TestDataFile_BLO();
            this.testDataFileBindingSource.DataSource = testDataFile_BLO.Find_All();
        }

        private void bt_Update_entity_data_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                TestData_File testData_File = this.testDataFileBindingSource.Current as TestData_File;
                TestDataFile_BLO testDataFile_BLO = new TestDataFile_BLO();
                testDataFile_BLO.Update_Entity_Date(testData_File.EntityType);
            }
            catch (GAppException ex)
            {

                MessageBox.Show(ex.Message);
                this.Cursor = Cursors.Default;
            }
            this.Cursor = Cursors.Default;


        }
    }
}
