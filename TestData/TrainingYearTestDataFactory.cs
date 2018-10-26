using GApp.Core.Context;
using GApp.DAL;
using GApp.UnitTest.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL;
using TrainingIS.DAL;
using TrainingIS.Entities;

namespace TestData
{
    public partial class TrainingYearTestDataFactory
    {

        public TrainingYearBLO BLO { set; get; }

        protected override void Constructor(UnitOfWork<TrainingISModel> UnitOfWork, GAppContext GAppContext)
        {
            base.Constructor(UnitOfWork, GAppContext);
            BLO = new TrainingYearBLO(UnitOfWork, GAppContext);

        }

        protected override List<TrainingYear> Generate_TestData()
        {
            var Data = new List<TrainingYear>();

            // 2019 
            TrainingYear trainingYear_2019 = new TrainingYear();
            trainingYear_2019.StartDate = Convert.ToDateTime("01/09/2018");
            trainingYear_2019.EndtDate = Convert.ToDateTime("31/08/2019");
            trainingYear_2019.Code = "2019";
            trainingYear_2019.Reference = trainingYear_2019.Code;
            Data.Add(trainingYear_2019);

            // 2020
            TrainingYear trainingYear_2020 = new TrainingYear();
            trainingYear_2020.StartDate = Convert.ToDateTime("01/09/2019");
            trainingYear_2020.EndtDate = Convert.ToDateTime("31/08/2020");
            trainingYear_2020.Reference = "2020";
            trainingYear_2020.Code = trainingYear_2020.Reference;
            Data.Add(trainingYear_2020);

            // 2021
            TrainingYear trainingYear_2021 = new TrainingYear();
            trainingYear_2021.StartDate = Convert.ToDateTime("01/09/2020");
            trainingYear_2021.EndtDate = Convert.ToDateTime("31/08/2021");
            trainingYear_2021.Reference = "2021";
            trainingYear_2021.Code = trainingYear_2021.Reference;
            Data.Add(trainingYear_2021);

            return Data;
        }

        
        public void Insert_Test_Data_If_Not_Exist()
        {
            if (!this.is_TestData_Exist())
            {
                foreach (var item in this.Get_TestData())
                {
                    var entity = this.BLO.FindBaseEntityByReference(item.Reference);
                    if (entity == null)
                    {
                        // Insert
                        this.BLO.Save(item);
                    }

                }
            }
        }
        public void Insert_Or_Update_Test_Data()
        {
            if (!this.is_TestData_Exist())
            {
                foreach (var item in this.Get_TestData())
                {
                    var entity = this.BLO.FindBaseEntityByReference(item.Reference);
                    if(entity == null)
                    {
                        // Insert
                        this.BLO.Save(item);
                    }
                    else
                    {
                        // Update
                        item.CopyProperties(entity);
                        this.BLO.Save(entity);
                    }
                   
                }
            }
        }

        protected virtual bool is_TestData_Exist()
        {
            foreach (var item in this.Get_TestData())
            {
                var item_db = this.BLO.FindBaseEntityByReference(item.Reference);
                if (item_db == null) return false;
            }
            return true;
        }
    }
}
