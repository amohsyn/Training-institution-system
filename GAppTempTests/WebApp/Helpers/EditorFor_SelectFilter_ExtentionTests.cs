using Microsoft.VisualStudio.TestTools.UnitTesting;
using GApp.WebApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TrainingIS.Entities;
using Moq;
using System.Web;
using System.Web.Routing;
using System.IO;
using TrainingIS.BLL;
using GApp.Entities;

namespace GApp.WebApp.Helpers.Tests
{
    [TestClass()]
    public class EditorFor_SelectFilter_ExtentionTests
    {

        public HtmlHelper<T> CreateHtmlHelper<T>(ViewDataDictionary viewData)
        {
            var controllerContext = new Mock<ControllerContext>(
                new Mock<HttpContextBase>().Object,
                new RouteData(),
                new Mock<ControllerBase>().Object);

            var mockViewContext = new Mock<ViewContext>(
                controllerContext.Object,
                new Mock<IView>().Object,
                viewData,
                new TempDataDictionary(),
                TextWriter.Null);

            var mockViewDataContainer = new Mock<IViewDataContainer>();

            mockViewDataContainer.Setup(v => v.ViewData).Returns(viewData);

            return new HtmlHelper<T>(
                mockViewContext.Object, mockViewDataContainer.Object);
        }


        [TestMethod()]
        public void EditFor_Select_With_FilterTest()
        {
            // HtmlHelper<AuthrorizationApp> htmlHelper = new HtmlHelper<AuthrorizationApp>();

            // Arrange
            var htmlHelper =
                CreateHtmlHelper<string>(
                    new ViewDataDictionary("Hello World"));

            // Act
            AuthrorizationAppFormView authrorizationApp = new AuthrorizationAppFormView();
            List<String> Selected = new List<string>();

            List<BaseEntity> data = new ActionControllerAppBLO(new TrainingIS.DAL.UnitOfWork())
                .FindAll().ToList<BaseEntity>();

 
            string select_filter = htmlHelper.EditFor_Select_With_Filter(
                model => authrorizationApp.ActionControllerApps,
                data, Selected
                
                )
                .ToHtmlString() ;

            // Assert
        }
    }
}