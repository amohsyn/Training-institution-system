using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrainingIS.Entities
{
    public class Trainee : Person
    {
         
        public int State { set; get; }

        // Affectation
        public  Group Group { set; get; }
        
        public  MiniGroup MiniGroup { set; get; }

        // Gestion des tâches
        public virtual List<ProjectTask> ProjectTasks { set; get; }

    }
}

