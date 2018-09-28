using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
namespace TrainingIS_UI_Tests.Services.GAppPages
{
    public class GAppIndexPage : GAppPage
    {
        public GAppIndexPage(IWebDriver b, string URL, string entity_Path) : base(b, URL, entity_Path)
        {
        }

        public void GoTo_Index()
        {
            var Former_URL = this.URL + this.Entity_Path;
            b.Navigate().GoToUrl(Former_URL);
        }

     

        private bool IsAuthenticated()
        {
            throw new NotImplementedException();
        }


        ////public bool Is_In_IndexPage()
        ////{
        ////    return this.IsElementIdExist("Index_Page_Title");
        ////}
    }
}
