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
                dataGenerator.Insert_Test_Data();

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
            
            this.Refresh("");
        }

        private void Refresh(string filter)
        {
            TestDataFile_BLO testDataFile_BLO = new TestDataFile_BLO();
            this.testDataFileBindingSource.DataSource = testDataFile_BLO.Find_All(filter);
        }

        private void bt_Update_entity_data_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                TestData_File testData_File = this.testDataFileBindingSource.Current as TestData_File;
                TestDataFile_BLO testDataFile_BLO = new TestDataFile_BLO();
                testDataFile_BLO.Insert_Entity_Data(testData_File.EntityType);
            }
            catch (GAppException ex)
            {

                MessageBox.Show(ex.Message);
                this.Cursor = Cursors.Default;
            }
            this.Cursor = Cursors.Default;


        }

        private void bt_Update_Selected_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                TestData_File testData_File = this.testDataFileBindingSource.Current as TestData_File;
                TestDataFile_BLO testDataFile_BLO = new TestDataFile_BLO();
                testDataFile_BLO.Update_Entity_Data(testData_File.EntityType);
            }
            catch (GAppException ex)
            {

                MessageBox.Show(ex.Message);
                this.Cursor = Cursors.Default;
            }
            this.Cursor = Cursors.Default;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.Refresh(textBox1.Text);
        }

      
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void bt_prepare_data_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                TestData_File testData_File = this.testDataFileBindingSource.Current as TestData_File;
                TestDataFile_BLO testDataFile_BLO = new TestDataFile_BLO();
                testDataFile_BLO.PrepareData(testData_File.EntityType);
            }
            catch (GAppException ex)
            {

                MessageBox.Show(ex.Message);
                this.Cursor = Cursors.Default;
            }
            this.Cursor = Cursors.Default;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            TestData_File testData_File = this.testDataFileBindingSource.Current as TestData_File;
            TestDataFile_BLO testDataFile_BLO = new TestDataFile_BLO();
            this.dataGridView_Data.DataSource = null;
            this.dataGridView_Data.AutoGenerateColumns = true;
            this.dataGridView_Data.DataSource = testDataFile_BLO.GetData(testData_File);

            this.Cursor = Cursors.Default;
        }
    }
}
