using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Automation;
using AppiumWpfServer;
using TestStack.White;
using TestStack.White.Factory;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.WPFUIItems;

namespace AppiumWpfServer.Model
{
	public class UIElement
	{
		public IUIItem element;
		public string selector;
	}

	public class Session
	{
		private ApplicationModel appModel;
		public string ID { get; set; }

		public static Session CreateNewSession(string appPath) 
		{
			return new Session(appPath);
		}
	
		private Session(string appPath)
		{
			ID = Guid.NewGuid().ToString();

			Logger.Info("Killing existing Sonos processes.");
			var processes = Process.GetProcessesByName("Sonos");
			foreach (var p in processes)
			{
				p.Kill();
				p.WaitForExit();
			}

			appModel = ApplicationModel.Create(appPath);

			Logger.Info("App is idle.  Session is beginning.");
		}

		public int[] GetElements(string locatorType, string locator)
		{
			return appModel.GetElements(locatorType, locator);
		}

		public int GetElement(string locatorType, string locator)
		{
			return appModel.GetElement(locatorType, locator);
		}

		internal void SendKeys(string keys)
		{
			appModel.SendKeys(keys);
		}

		internal IUIItem GetElement(int id)
		{
			return appModel.GetElement(id);
		}

		internal void LaunchApp()
		{
			appModel.LaunchApp();
		}

		internal void CloseApp()
		{
			appModel.CloseApp();
		}

		internal void CloseActiveWindow()
		{
			appModel.CloseActiveWindow();
		}

		internal bool SetActiveWindow(string title)
		{
			return appModel.SetActiveWindow(title);
		}


		private static SearchCriteria FromControlType(string controlType)
		{
			switch (controlType)
			{
				case "ListItem":
					return SearchCriteria.ByControlType(ControlType.ListItem);
				default:
					Debugger.Break();
					return null;
			}
		}

	}
}