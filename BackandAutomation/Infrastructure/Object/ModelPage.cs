﻿using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Dialogs;
using Infrastructure.Base;
using OpenQA.Selenium;

namespace Infrastructure.Object
{
    [BackandPageType(LeftMenuOption.Objects, LeftMenuOption.Model)]
    public class ModelPage : BackandApplicationsBasePage
    {
        public ModelPage(DriverUser driver) : base(driver)
        {
        }

        private IReadOnlyCollection<IWebElement> ModelRectangleElements
            => PageElement.FindElements(By.CssSelector(".draggable-container g[ng-repeat*='chart.nodes'] .node-title"));

        public ModelRectangle GetModelRectangle(string modelName)
        {
            var webElement = ModelRectangleElements.FirstOrDefault(element => element.Text.Contains(modelName));
            return new ModelRectangle(this, webElement.GetParent());
        }

        public ModelPage ValidateAndUpdate()
        {
            Driver.FindElement(By.CssSelector(".db-footer span.ladda-label")).Click();
            WaitUntil.UntilDialogPopUp<OkDialog>().Ok();
            return this;
        }
    }
}