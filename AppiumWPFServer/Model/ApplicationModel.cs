using System;
using System.Collections.Generic;
using System.Threading;
using TestStack.White.Factory;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.WPFUIItems;
using System.Windows.Automation;
using System.Diagnostics;

namespace AppiumWpfServer.Model
{
	class ApplicationModel
	{
		private readonly List<UIElement> elements = new List<UIElement>();
		private Window mainWindow;
		private Window activeWindow;
		private string appPath;
		internal TestStack.White.Application App { get; private set; }

		public static ApplicationModel Create(string appPath)
		{
			var appModel = new ApplicationModel(appPath);
			appModel.LaunchApp();
			return appModel;
		}

		private ApplicationModel(string appPath)
		{
			this.appPath = appPath;
		}

		internal void LaunchApp()
		{
			Debug.Assert(App == null);
			App = LaunchApp(this.appPath);
		}

		private TestStack.White.Application LaunchApp(string appPath)
		{
			Logger.Info("Launching app from " + appPath);
			var application = TestStack.White.Application.Launch(appPath);

			List<Window> windows = null;
			var attempts = 0;
			while ((windows == null || windows.Count == 0) && ++attempts < 10)
			{
				Logger.Info("Waiting for a window to appear.");
				Thread.Sleep(1000);
				try
				{
					windows = application.GetWindows();
				}
				catch (Exception e)
				{
					Logger.Info("Ignoring exception: " + e.Message);
				}
			}

			Logger.Info("Searching for Sonos main window.");
			mainWindow = application.GetWindow("Sonos", InitializeOption.NoCache);
			activeWindow = mainWindow;

			Logger.Info("Found it.  Waiting for idle.");
			mainWindow.WaitWhileBusy();

			Logger.Info("App is idle.  Session is beginning.");

			return application;
		}

	
		internal void SendKeys(string keys)
		{
			mainWindow.Keyboard.Enter(keys);
		}

		internal IUIItem GetElement(int id)
		{
			var item = elements[id];
			Logger.Info("Returning element {0}: {1} {2}", id, item.selector, item.element.Name);
			return item.element;
		}

		internal void CloseApp()
		{
			mainWindow.Close();

			elements.Clear();
			activeWindow = null;
			mainWindow = null;
			App = null;
		}

		// Doesn't handle deeply stacked windows..... 
		internal void CloseActiveWindow()
		{
			if (activeWindow == mainWindow)
			{
				CloseApp();
			}
			else
			{
				activeWindow.Close();
				activeWindow = mainWindow;
			}
		}

		internal bool SetActiveWindow(string title)
		{
			if (title == mainWindow.Title)
			{
				activeWindow = mainWindow;
				return true;
			}

			Window newWindow = null;
			try
			{
				newWindow = mainWindow.ModalWindow(title);
			}
			catch (Exception)
			{
				// expected
			}

			if (newWindow == null)
			{
				try
				{
					newWindow = App.GetWindow(title);
				}
				catch (Exception)
				{
					//expected
				}
			}

			if (newWindow != null)
			{
				activeWindow = newWindow;
				return true;
			}

			return false;
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

		public int[] GetElements(string locatorType, string locator)
		{
			Logger.Info("Searching for elements:" + locator + " using " + locatorType);
			IUIItem[] items = null;
			var attempts = 5;
			while ((items == null || items.Length == 0) && attempts-- > 0)
			{
				switch (locatorType)
				{
					case "class name":
						items = WPFUIItem.GetMultiple(activeWindow, FromControlType(locator));
						break;
					case "name":
						items = WPFUIItem.GetMultiple(activeWindow, SearchCriteria.ByText(locator));
						break;
					case "id":
						items = WPFUIItem.GetMultiple(activeWindow, SearchCriteria.ByAutomationId(locator));
						break;
					default:
						throw new ArgumentException("invalid locator type:" + locatorType);
				}
				Thread.Sleep(200);
			}

			if (items == null)
			{
				throw new InvalidOperationException();
			}
			var newElements = new List<int>();
			foreach (var item in items)
			{
				elements.Add(new UIElement { element = item, selector = locator });
				newElements.Add(elements.Count - 1);
			}
			return newElements.ToArray();
		}

		public int GetElement(string locatorType, string locator)
		{
			Logger.Info("Searching for element:" + locator + " using " + locatorType);
			IUIItem item = null;
			var attempts = 5;
			while (item == null && attempts-- > 0)
			{
				switch (locatorType)
				{
					case "class name":
						item = WPFUIItem.Get(activeWindow, SearchCriteria.ByClassName(locator));
						break;
					case "name":
						item = WPFUIItem.Get(activeWindow, SearchCriteria.ByText(locator));
						break;
					case "id":
						item = WPFUIItem.Get(activeWindow, SearchCriteria.ByAutomationId(locator));
						break;
					default:
						throw new ArgumentException("invalid locator type:" + locatorType);
				}
				Thread.Sleep(500);
			}
			if (item == null)
			{
				throw new InvalidOperationException();
			}
			elements.Add(new UIElement { element = item, selector = locator });
			return elements.Count - 1;
		}
	}

}
