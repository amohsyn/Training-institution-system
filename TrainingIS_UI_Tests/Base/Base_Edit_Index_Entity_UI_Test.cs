using GApp.UnitTest.Context;
using GApp.UnitTest.UI_Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingIS_UI_Tests.Base
{
    public class Base_Edit_Index_Entity_UI_Test<T> : Edit_Index_Entity_UI_Test<T>
    {
        public Base_Edit_Index_Entity_UI_Test(UI_Test_Context UI_Test_Context) : base(UI_Test_Context)
        {
        }
    }
}
