using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingIS_UI_Tests.Services.GAppDataTables
{
    public class GAppLineDataTable
    {
        private IWebDriver b;
        private string Line_Path = "";
        public GAppLineDataTable(IWebDriver b,IWebElement Line_Element)
        {

            // Line_Element
            this.Line_Element = Line_Element;


            // ObjectId
            string Line_Element_Id = this.Line_Element.GetAttribute("Id");
            this.ObjectId = Convert.ToInt32(this.Line_Element.GetAttribute("Id").Split('_').Last());

            try
            {
                this.Edit_Element = this.Line_Element.FindElement(By.CssSelector(".edit"));
            } catch (OpenQA.Selenium.NoSuchElementException ) {}

            try
            {
                this.Delete_Element = this.Line_Element.FindElement(By.CssSelector(".delete"));
            } catch (OpenQA.Selenium.NoSuchElementException) { }

            try
            {
                this.Details_Element = this.Line_Element.FindElement(By.CssSelector(".details"));
            }catch (OpenQA.Selenium.NoSuchElementException) { }




            //// Id
            //this.ObjectId = Convert.ToInt32(this.Line_Element.GetAttribute("Id").Split('_')[2]);
            //this.Edit_Element = this.Line_Element.FindElement(By.CssSelector("link_action edit"))

            //// Delete Element
            //string Delete_Link_Path_Format = "//Table[@id='dataTable']/tbody/tr[{0}]//a[@class='link_action delete']";
            //string Delete_Link_Path = string.Format(Delete_Link_Path_Format, line_number);
            //this.Delete_Element = b.FindElement(By.XPath(Delete_Link_Path));

            //// Details Element
            //string Details_Link_Path_Format = "//Table[@id='dataTable']/tbody/tr[{0}]//a[@class='link_action details']";
            //string Details_Link_Path = string.Format(Details_Link_Path_Format, line_number);
            //this.Details_Element = b.FindElement(By.XPath(Details_Link_Path));
        }


        public int ObjectId { set; get; }



        public IWebElement Edit_Element { set; get; }
        public IWebElement Delete_Element { set; get; }
        public IWebElement Details_Element { set; get; }
        public IWebElement Line_Element { set; get; }


        public IWebElement this[int index]
        {
            get
            {
                string element_path = string.Format(".//td[{0}]", index + 1);
                return  this.Line_Element.FindElement( By.XPath(element_path));
            }
        }
        
    }
}
