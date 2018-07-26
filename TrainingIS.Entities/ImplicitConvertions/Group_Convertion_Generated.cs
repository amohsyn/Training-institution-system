using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingIS.Entities.ModelsViews.GroupModelsViews;

namespace TrainingIS.Entities
{
    public partial class Group
    {
        // 
        // LineGroupView
        //
        public static implicit operator Group(LineGroupView LineGroupView)
        {
            Group Group = new Group();
			Group.Code = LineGroupView.Code;
			Group.YearStudy = LineGroupView.YearStudy;
			Group.Specialty = LineGroupView.Specialty;
			Group.TrainingType = LineGroupView.TrainingType;
			Group.Id = LineGroupView.Id;
            return Group;
        }
        public static implicit operator LineGroupView(Group Group)
        { 
            LineGroupView LineGroupView = new LineGroupView();
			LineGroupView.TrainingType = Group.TrainingType;
			LineGroupView.Specialty = Group.Specialty;
			LineGroupView.YearStudy = Group.YearStudy;
			LineGroupView.Code = Group.Code;
			LineGroupView.Id = Group.Id;
            return LineGroupView;
        }
		
		
        // 
        // CreateGroupView
        //
        public static implicit operator Group(CreateGroupView CreateGroupView)
        {
            Group Group = new Group();
			Group.TrainingYear = CreateGroupView.TrainingYear;
			Group.TrainingYearId = CreateGroupView.TrainingYearId;
			Group.Specialty = CreateGroupView.Specialty;
			Group.SpecialtyId = CreateGroupView.SpecialtyId;
			Group.TrainingType = CreateGroupView.TrainingType;
			Group.TrainingTypeId = CreateGroupView.TrainingTypeId;
			Group.YearStudy = CreateGroupView.YearStudy;
			Group.YearStudyId = CreateGroupView.YearStudyId;
			Group.Code = CreateGroupView.Code;
			Group.Id = CreateGroupView.Id;
            return Group;
        } 
        public static implicit operator CreateGroupView(Group Group)
        { 

            CreateGroupView CreateGroupView = new CreateGroupView();
			CreateGroupView.TrainingType = Group.TrainingType;
			CreateGroupView.TrainingTypeId = Group.TrainingTypeId;
			CreateGroupView.TrainingYear = Group.TrainingYear;
			CreateGroupView.TrainingYearId = Group.TrainingYearId;
			CreateGroupView.Specialty = Group.Specialty;
			CreateGroupView.SpecialtyId = Group.SpecialtyId;
			CreateGroupView.YearStudy = Group.YearStudy;
			CreateGroupView.YearStudyId = Group.YearStudyId;
			CreateGroupView.Code = Group.Code;
			CreateGroupView.Id = Group.Id;
            return CreateGroupView;
        }
		
        // 
        // EditGroupView
        //
        public static implicit operator Group(EditGroupView EditGroupView)
        {
            Group Group = new Group();
			Group.TrainingYear = EditGroupView.TrainingYear;
			Group.TrainingYearId = EditGroupView.TrainingYearId;
			Group.Specialty = EditGroupView.Specialty;
			Group.SpecialtyId = EditGroupView.SpecialtyId;
			Group.TrainingType = EditGroupView.TrainingType;
			Group.TrainingTypeId = EditGroupView.TrainingTypeId;
			Group.YearStudy = EditGroupView.YearStudy;
			Group.YearStudyId = EditGroupView.YearStudyId;
			Group.Code = EditGroupView.Code;
			Group.Id = EditGroupView.Id;
            return Group;
        } 
        public static implicit operator EditGroupView(Group Group)
        {
            EditGroupView EditGroupView = new EditGroupView();
			EditGroupView.TrainingType = Group.TrainingType;
			EditGroupView.TrainingTypeId = Group.TrainingTypeId;
			EditGroupView.TrainingYear = Group.TrainingYear;
			EditGroupView.TrainingYearId = Group.TrainingYearId;
			EditGroupView.Specialty = Group.Specialty;
			EditGroupView.SpecialtyId = Group.SpecialtyId;
			EditGroupView.YearStudy = Group.YearStudy;
			EditGroupView.YearStudyId = Group.YearStudyId;
			EditGroupView.Code = Group.Code;
			EditGroupView.Id = Group.Id;
            return EditGroupView;
        }
		

        // 
        // DetailsGroupView
        //
        public static implicit operator Group(DetailsGroupView DetailsGroupView)
        {
            Group Group = new Group();
			Group.Code = DetailsGroupView.Code;
			Group.YearStudy = DetailsGroupView.YearStudy;
			Group.Specialty = DetailsGroupView.Specialty;
			Group.TrainingType = DetailsGroupView.TrainingType;
			Group.Id = DetailsGroupView.Id;
            return Group;
        }
        public static implicit operator DetailsGroupView(Group Group)
        {
            DetailsGroupView DetailsGroupView = new DetailsGroupView();
			DetailsGroupView.TrainingType = Group.TrainingType;
			DetailsGroupView.Specialty = Group.Specialty;
			DetailsGroupView.YearStudy = Group.YearStudy;
			DetailsGroupView.Code = Group.Code;
			DetailsGroupView.Id = Group.Id;
            return DetailsGroupView;
        }
		    }
}
