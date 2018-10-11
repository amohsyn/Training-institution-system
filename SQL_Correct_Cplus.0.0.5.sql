-- Update Durraction of SeanceTraining
Select Duration , Datediff(minute  , SeanceNumbers.StartTime , SeanceNumbers.EndTime ) as Durration_Seance
from SeanceTrainings
join SeancePlannings on SeanceTrainings.SeancePlanningId = SeancePlannings.Id
join SeanceNumbers on SeancePlannings.SeanceNumberId = SeanceNumbers.Id
 


Update SeanceTrainings  
set Duration = (Select Datediff(minute  , SeanceNumbers.StartTime , SeanceNumbers.EndTime ) as Durration_Seance
from SeanceTrainings as SR_SeanceTrainings
join SeancePlannings on SR_SeanceTrainings.SeancePlanningId = SeancePlannings.Id
join SeanceNumbers on SeancePlannings.SeanceNumberId = SeanceNumbers.Id
where SR_SeanceTrainings.Id = SeanceTrainings.Id )
 