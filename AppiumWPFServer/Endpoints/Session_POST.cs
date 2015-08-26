using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using AppiumWpfServer;
using NServiceKit.ServiceHost;
using NServiceKit.Text;
using TestStack.White;
using TestStack.White.UIItems.WindowItems;

namespace AppiumWpfServer.Endpoints
{
	[Route("/wd/hub/session", "POST")]
	public class Session_POST : JsonWireProtocolRequest, IReturn<Session_POST.Response>
	{
		public JsonObject DesiredCapabilities { get; set; }

		public class Response : JsonWireProtocolResponse
		{
			internal Response(Session_POST request)
			{
				var appPath = request.DesiredCapabilities["app"];
				var desired = request.DesiredCapabilities;

				

				var session = Server.Model.Sessions.CreateNewSession(appPath);
				Add("status", 0);
				Add("sessionId", session.ID);
				Add("value", desired);
			}
		}
	}
}