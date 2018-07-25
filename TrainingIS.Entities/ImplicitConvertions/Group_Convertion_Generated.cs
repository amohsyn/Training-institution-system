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
        // Create View 
        //
        public static implicit operator Group(CreateGroupView CreateGroupViewd)
        {
            Group group = new Group();
            return group;
        }
        public static implicit operator CreateGroupView(Group Group)
        {
            CreateGroupView CreateGroupView = new CreateGroupView();
            return CreateGroupView;
        }
		
		         // 
        // Edit View 
        //
        public static implicit operator Group(EditGroupView CreateGroupViewd)
        {
            Group group = new Group();
            return group;
        } 
        public static implicit operator EditGroupView(Group Group)
        {
            EditGroupView EditGroupView = new EditGroupView();
            return EditGroupView;
        }
		

		        // 
        // Details View 
        //
        public static implicit operator Group(DetailsGroupView DetailsGroupView)
        {
            Group group = new Group();
            return group;
        }
        public static implicit operator DetailsGroupView(Group Group)
        {
            DetailsGroupView DetailsGroupView = new DetailsGroupView();
            return DetailsGroupView;
        }
		    }
}
