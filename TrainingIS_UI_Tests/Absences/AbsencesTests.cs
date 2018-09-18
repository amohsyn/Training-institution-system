using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingIS_UI_Tests.Absences
{
    [TestClass]
    public class AbsencesTests : Base_UI_Tests
    {

        public AbsencesTests():base("essarraj.fouad@gmail.com", "Formateur@123456", "/Absences")
        {
        }

        [TestMethod]
        public void Absence_Index_Show_Test()
        {
            this.GoTo_Index();
        }
    }
}
