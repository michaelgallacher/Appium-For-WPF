using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NServiceKit.ServiceClient.Web;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Remote;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestServer()
        {
            DesiredCapabilities capabilities = new DesiredCapabilities();

            capabilities.SetCapability("deviceName", "iPhone Retina (4-inch 64-bit)");
            capabilities.SetCapability("platformName", "iOS");
            capabilities.SetCapability("platformVersion", "7.1");
            capabilities.SetCapability("app", @"C:\SonosApps\Sonos.exe");
     
            AppiumDriver driver = new MyAppiumDriver(new Uri("http://127.0.0.1:4776/wd/hub"), capabilities);       
        
        }

        private class MyAppiumDriver : AppiumDriver
        {
            public MyAppiumDriver(ICommandExecutor commandExecutor, ICapabilities desiredCapabilities) : base(commandExecutor, desiredCapabilities)
            {
            }

            public MyAppiumDriver(ICapabilities desiredCapabilities) : base(desiredCapabilities)
            {
            }

            public MyAppiumDriver(Uri remoteAddress, ICapabilities desiredCapabilities) : base(remoteAddress, desiredCapabilities)
            {
            }

            public MyAppiumDriver(Uri remoteAddress, ICapabilities desiredCapabilities, TimeSpan commandTimeout) : base(remoteAddress, desiredCapabilities, commandTimeout)
            {
            }
        }

        public void TestMethod1()
        {
            //var restClient = new JsonServiceClient("http://localhost:4776");
            //Status_GET.Response status = restClient.Get(new Status_GET());
            //Sessions_GET.Response sessions = restClient.Get(new Sessions_GET());
            //Session_POST.Response session = restClient.Post(new Session_POST()); 
            
            
            // Count = 0

            //var todo = restClient.Post( new Session());      // todo.Id = 1
            //all = restClient.Get(new Todos());                      // Count = 1

            //todo.Content = "Updated TODO";
            //todo = restClient.Put(todo);                            // todo.Content = Updated TODO

            //restClient.Delete(new Todos(todo.Id));
            //all = restClient.Get(new Todos());    
        }
    }
}
