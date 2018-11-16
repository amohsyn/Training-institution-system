using GApp.UnitTest.Context;
using GApp.UnitTest.UI_Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingIS_UI_Tests.Base
{
    public class Base_Details_Entity_UI_Test<T> : Details_Entity_UI_Test<T>
    {
        public Base_Details_Entity_UI_Test(UI_Test_Context UI_Test_Context) : base(UI_Test_Context)
        {
        }
    }
}
