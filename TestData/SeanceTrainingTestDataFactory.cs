using GApp.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.BLL;
using TrainingIS.BLL.ModelsViews;
using TrainingIS.Entities;
using TrainingIS.Models.SeanceInfos;

namespace TestData
{
    public partial class SeanceTrainingTestDataFactory
    {
        protected override List<SeanceTraining> Generate_TestData()
        {
            // Test Data
            if (this.Data != null)
                return this.Data;

            this.Data = new List<SeanceTraining>();

            // SeancesInfo
            List<SeanceInfo> SeancesInfo = new List<SeanceInfo>();
            //SeancesInfo.AddRange(this.Add_10_SeanceTraining());
            //SeancesInfo.AddRange(this.Add_50_First_SeanceTraining_of_First_Former());
            SeancesInfo.AddRange(this.Add_23_First_SeanceTraining_of_each_Groupe());

            // Distinct
            SeancesInfo
                .GroupBy(s => s.CalendarDay.Date, s => s.SeancePlanning.Id)
                .Select(g => g.First()).ToList();


            // Add first 10 SeanceTraining
            foreach (var SeanceInfo in SeancesInfo)
            {
                SeanceTraining seanceTraining = new SeanceTraining();
                seanceTraining.SeanceDate = SeanceInfo.CalendarDay.Date;
                seanceTraining.SeancePlanning = SeanceInfo.SeancePlanning;
                seanceTraining.Reference = seanceTraining.CalculateReference();
                Data.Add(seanceTraining);
            }

            return Data;
        }

        private IEnumerable<SeanceInfo> Add_50_First_SeanceTraining_of_FirstGroup()
        {
            SeanceInfoBLM seanceInfoBLM = new SeanceInfoBLM(this.UnitOfWork, this.GAppContext);
            Group group = this.UnitOfWork.context.Groups.First();
            FilterRequestParams filterRequestParam = new FilterRequestParams();

            string Group_Property = "SeancePlanning.Training.Group.Reference";
            filterRequestParam.FilterBy = string.Format("[{0},{1}]", Group_Property, group.Reference);
            filterRequestParam.pageSize = 50;
            var seances = seanceInfoBLM.Find(filterRequestParam, null, out int total2);
            return seances;
        }

        /// <summary>
        /// Add SeanceTraining to Test CalculateInvalideSanction
        /// Arrage : 1j, 2j, 3j, 4j, 5j, 6j, 7j, 8j, 9j, 10j, 11j, 12j
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SeanceInfo> Add_23_First_SeanceTraining_of_each_Groupe()
        {
            List<SeanceInfo> seances_ls = new List<SeanceInfo>();
            SeanceInfoBLM seanceInfoBLM = new SeanceInfoBLM(this.UnitOfWork, this.GAppContext);
            var Groups = this.UnitOfWork.context.Groups.OrderBy(g => g.Ordre).ToList();
            var Groups_count = Groups.Count;
            for (int i = 0; i < 23; i++)
            {
                if (i > Groups_count - 1) break;

                var Group = Groups[i];
                FilterRequestParams filterRequestParam = new FilterRequestParams();

                string Group_Property = "SeancePlanning.Training.Group.Reference";
                filterRequestParam.FilterBy = string.Format("[{0},{1}]", Group_Property, Group.Reference);
                filterRequestParam.pageSize = i + 1;
                var seances = seanceInfoBLM.Find(filterRequestParam, null, out int total2);
                seances_ls.AddRange(seances);

            }

            return seances_ls;
        }
        /// <summary>
        ///  // Add First 10 Seance of Former_1 in First Schedule
        /// </summary>
        /// <returns></returns>
        private List<SeanceInfo> Add_50_First_SeanceTraining_of_First_Former()
        {
            SeanceInfoBLM seanceInfoBLM = new SeanceInfoBLM(this.UnitOfWork, this.GAppContext);
            Former former = this.UnitOfWork.context.Formers.First();
            FilterRequestParams filterRequestParam = new FilterRequestParams();

            string Former_Property = "SeancePlanning.Training.Former.Reference";
            filterRequestParam.FilterBy = string.Format("[{0},{1}]", Former_Property, former.Reference);
            filterRequestParam.pageSize = 50;
            var seances = seanceInfoBLM.Find(filterRequestParam, null, out int total2);
            return seances;
        }

        /// <summary>
        ///  First 10 SeanceTraining SeanceInfo
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SeanceInfo> Add_10_SeanceTraining()
        {
            SeanceInfoBLM seanceInfoBLM = new SeanceInfoBLM(this.UnitOfWork, this.GAppContext);

            FilterRequestParams filterRequestParam = new FilterRequestParams();
            string Schedule_Id = "SeancePlanning.Schedule.Id";
            filterRequestParam.pageSize = 10;
            var SeanceInfos = seanceInfoBLM.Find(filterRequestParam, null, out int total);
            return SeanceInfos;
        }
    }
}
