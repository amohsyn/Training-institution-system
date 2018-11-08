using ClosedXML.Excel;
using GApp.DAL.ReadExcel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL;
using TrainingIS.Entities;

namespace TestData
{
    public partial class SpecialtyTestDataFactory
    {
        protected override List<Specialty> Generate_TestData()
        {
            List<Specialty> Data = new List<Specialty>();
            // BLO
            SectorBLO sectorBLO = new SectorBLO(this.UnitOfWork, this.GAppContext);
            TrainingLevelBLO trainingLevelBLO = new TrainingLevelBLO(this.UnitOfWork, this.GAppContext);

            int code_number = 1;
            foreach (var sector in sectorBLO.FindAll())
            {
                foreach (var trainingLevel in trainingLevelBLO.FindAll())
                {
                    Specialty specialty = new Specialty();
                    specialty.Sector = sector;
                    specialty.TrainingLevel = trainingLevel;
                    specialty.Code = string.Format("Specialty_{0}", code_number);
                    specialty.Name = specialty.Code;
                    specialty.Reference = specialty.CalculateReference();
                    Data.Add(specialty);

                    code_number++;
                }
            }
            return Data;
        }
    }
}
