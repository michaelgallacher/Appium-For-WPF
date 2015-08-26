using AppiumWpfServer.Model;
using NServiceKit.ServiceHost;
using NServiceKit.Text;
using System.Diagnostics;
using AppiumWpfServer;
using TestStack.White;
using System.Collections.Generic;
using System.Threading;
using TestStack.White.UIItems.WindowItems;
using System;

namespace AppiumWpfServer.Endpoints
{
	[Route("/wd/hub/session/{SessionId}/appium/app/close", "POST")]
	public class AppClose_POST : JsonWireProtocolRequest, IReturn<AppClose_POST.Response>
	{
		public string SessionId { get; set; }

		public class Response : JsonWireProtocolResponse
		{
			internal Response(AppClose_POST request)
			{
				var session = Server.Model.Sessions.GetSessionById(request.SessionId);
				session.CloseApp();

				Add("status", 0);
				Add("sessionId", session.ID);
			}

			public Session Value { get; set; }
		}
	}

	[Route("/wd/hub/session/{SessionId}/appium/app/launch", "POST")]
	public class AppLaunch_POST : JsonWireProtocolRequest, IReturn<AppLaunch_POST.Response>
	{
		public string SessionId { get; set; }
		public JsonObject DesiredCapabilities { get; set; }

		public class Response : JsonWireProtocolResponse
		{
			internal Response(AppLaunch_POST request)
			{
				var session = Server.Model.Sessions.GetSessionById(request.SessionId);
				session.LaunchApp();

				Add("status", 0);
				Add("sessionId", session.ID);
			}

			public Session Value { get; set; }
		}
	}
}