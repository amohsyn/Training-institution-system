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
using TrainingIS.Entities.ModelsViews.Authorizations;
using TrainingIS.BLL.ModelsViews;
using GApp.Core.Entities.ModelsViews;

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
            AuthrorizationAppFormView AuthrorizationAppFormView = new AuthrorizationAppFormView();
            AuthrorizationAppFormView.Selected_ActionControllerApps = new List<string>() { "1", "3" };

            ViewDataDictionary viewDataDictionary = new ViewDataDictionary();
            viewDataDictionary.Add("model", AuthrorizationAppFormView);
            var htmlHelper =CreateHtmlHelper<string>(viewDataDictionary);

            // Act
           
            List<String> Selected = new List<string>();

            TrainingIS.DAL.UnitOfWork unitOfWork = new TrainingIS.DAL.UnitOfWork();



            Default_ActionControllerAppFormViewBLM Default_ActionControllerAppFormViewBLM = new Default_ActionControllerAppFormViewBLM(unitOfWork);

            List<BaseEntity> data = new ActionControllerAppBLO(unitOfWork).FindAll().ToList<BaseEntity>();



            string select_filter = htmlHelper.EditFor_Select_With_Filter(
                model => AuthrorizationAppFormView.Selected_ActionControllerApps,
                data
                )
                .ToHtmlString() ;

            // Assert
        }
    }
}