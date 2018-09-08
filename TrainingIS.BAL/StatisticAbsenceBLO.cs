using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GApp.Core.Context;
using TrainingIS.Entities;
using TrainingIS.Models.StatisticAbsence;

namespace TrainingIS.BLL
{
    public class StatisticAbsenceBLO : Base_NotDb_BLO
    {
        public StatisticAbsenceBLO(GAppContext GAppContext) : base(GAppContext)
        {
        }

        public Statistic Calculate(StatisticAbsenceForm statisticAbsenceForm)
        {
            Statistic statistic = new Statistic();
            statistic.StartDate = statisticAbsenceForm.StartDate;
            statistic.EndDate = statisticAbsenceForm.EndDate;


            // Group
            List<Group> Groups = new List<Group>();
            if (statisticAbsenceForm.GroupId == 0)
            {
                statistic.Name = "Statistique d'absence - tous les groupes";
                Groups = new GroupBLO(this.UnitOfWork, this.GAppContext).FindAll();
            }
            else
            {
                Group group = new GroupBLO(this.UnitOfWork, this.GAppContext).FindBaseEntityByID(statisticAbsenceForm.GroupId);
                statistic.Name = "Statistique d'absence - " +  group.Code;
                Groups.Add(group);
            }

            // Statistic By Group
            StatisticCategory statisticCategory = new StatisticCategory();
            statisticCategory.Code = "Par group";
            statisticCategory.Name = "Par groupe";
            foreach (Group group in Groups)
            {
                StatisticValue statisticValue = this.Statisitic_By_Group(group, statisticAbsenceForm);
                statisticCategory.StatisticValues.Add(statisticValue);
            }
            statistic.Categories.Add(statisticCategory);
            return statistic;
        }

        private StatisticAbsenceValue Statisitic_By_Group(Group group, StatisticAbsenceForm statisticAbsenceForm )
        {
          
            var AbsencesCount = this.UnitOfWork.context.Absences
                .Where(a => a.AbsenceDate >= statisticAbsenceForm.StartDate && a.AbsenceDate <= statisticAbsenceForm.EndDate)
                .Where(a => a.SeancePlanning.Training.Group.Id == group.Id).Count();
           
            var SeanceTrainingCount = this.UnitOfWork.context.SeanceTrainings
                .Where(s => s.SeanceDate >= statisticAbsenceForm.StartDate && s.SeanceDate <= statisticAbsenceForm.EndDate)
                .Where(s => s.SeancePlanning.Training.Group.Id == group.Id).Count();

            var GroupsTraineeCount = group.Trainees.Count();

            StatisticAbsenceValue statisticAbsenceValue = new StatisticAbsenceValue();
            statisticAbsenceValue.Name = group.Code;
            statisticAbsenceValue.Value = AbsencesCount;
            statisticAbsenceValue.SeanceTrainingsCount = SeanceTrainingCount;
            statisticAbsenceValue.GroupsTraineeCount = GroupsTraineeCount;

            return statisticAbsenceValue;
        }
    }
}
