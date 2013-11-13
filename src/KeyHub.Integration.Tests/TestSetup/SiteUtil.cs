﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace KeyHub.Integration.Tests.TestSetup
{
    public class SiteUtil
    {
        public static void CreateLocalAccount(KeyHubWebDriver site, string email, string password, Action<RemoteWebDriver> onFinish = null)
        {
            onFinish = onFinish ?? delegate(RemoteWebDriver browser)
            {
                WaitUntilUserIsLoggedIn(browser);
            };

            using (var browser = BrowserUtil.GetBrowser())
            {
                browser.Navigate().GoToUrl(site.UrlFor("Account/Register"));

                SubmitRegistrationForm(browser, email, password);

                onFinish(browser);
            }
        }

        public static IWebElement WaitUntilUserIsLoggedIn(RemoteWebDriver browser)
        {
            return browser.FindElementByCssSelector("a[href='/Account/LogOff']");
        }

        public static void SubmitLoginForm(RemoteWebDriver browser, string email, string password)
        {
            var formSelector = "form[action^='/Account/Login'] ";
            browser.FindElementByCssSelector(formSelector + "input#Email").SendKeys(email);
            browser.FindElementByCssSelector(formSelector + "input#Password").SendKeys(password);
            browser.FindElementByCssSelector(formSelector + "input[value='Log in']").Click();
            WaitUntilUserIsLoggedIn(browser);
        }

        public static void SubmitRegistrationForm(RemoteWebDriver browser, string email, string password)
        {
            browser.FindElementByCssSelector("input[name=Email]").SendKeys(email);
            browser.FindElementByCssSelector("input[name=Password]").SendKeys(password);
            browser.FindElementByCssSelector("input[name=ConfirmPassword]").SendKeys(password);
            browser.FindElementByCssSelector("input[value=Register]").Click();
        }

        public static void SetValueForChosenJQueryControl(RemoteWebDriver browser, string cssSelector, string value)
        {
            // We're using the jQuery Chosen library for some front-end widgets.  These require special 
            // handling to get the timing correct.

            var selector = browser.FindElementByCssSelector(cssSelector);

            selector.Click();
            
            var wait = new WebDriverWait(browser, TimeSpan.FromSeconds(2));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(cssSelector + " input[type=text]")));

            browser.FindElementByCssSelector(cssSelector + " input[type=text]").SendKeys(value + Keys.Enter);
        }
    }
}