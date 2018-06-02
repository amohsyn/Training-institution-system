using App.Gwin.Attributes;
using App.Gwin.Entities;
using App.Gwin.Entities.MultiLanguage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vtts.Entities.ProjectManagement;

namespace vtts.Entities.TraineeManagement
{
    [GwinEntity(Localizable = true,isMaleName =true, DisplayMember = "Name")]
    [Menu(Group = "Trainee")]
    [ManagementForm(TitrePageGridView ="grid_title",Width =790)]
    public class MiniGroup  : BaseEntity
    {
       
        public MiniGroup()
        {
            Name = new LocalizedString();
            Description = new LocalizedString();
        }
        [EntryForm(WidthControl = 300, Ordre = 0,isRequired =true)]
        [DataGrid(WidthColonne = 150, Ordre = 0)]
        [Filter(Ordre = 0,  WidthControl = 150)]
        public LocalizedString Name { set; get; }

        [EntryForm(WidthControl = 300, Ordre = 2,MultiLine =true,NumberLine =5)]
        [DataGrid(WidthColonne = 150, Ordre = 2)]
        [Filter(Ordre = 2, WidthControl = 150)]
        public LocalizedString Description { set; get; }

        [Relationship(Relation =RelationshipAttribute.Relations.OneToMany)]
        public virtual List<Trainee> Trainee { set; get; }

       
        [Relationship(Relation = RelationshipAttribute.Relations.OneToMany)]
        public virtual List<ProjectTask> ProjectTask { set; get; }

        [EntryForm(WidthControl = 300, Ordre = 1)]
        [DataGrid(WidthColonne = 150, Ordre = 1)]
        [Filter(Ordre = 1, WidthControl = 150,isDefaultIsEmpty  =true)]
        [Relationship(Relation =RelationshipAttribute.Relations.ManyToOne)]
        public virtual Group Group { get; set; }
    }
}
