using System;
using System.Linq;
using AppiumWpfServer;
using NServiceKit.ServiceHost;

namespace AppiumWpfServer.Endpoints
{
	[Route("/wd/hub/session/{SessionId}/elements", "POST")]
	public class Elements_POST : JsonWireProtocolRequest, IReturn<Elements_POST.Response>
	{
		public string Using { get; set; }
		public string Value { get; set; }
		public string SessionId { get; set; }

		public class Response : JsonWireProtocolResponse
		{
			internal Response(Elements_POST request)
			{
				var session = Server.Model.Sessions.GetSessionById(request.SessionId);
				var elements = session.GetElements(request.Using, request.Value);

				var val = elements.Select(element => new Tuple<string, object>("ELEMENT", element)).ToList();

				Add("status", 0);
				Add("sessionId", session.ID);
				Add("value", val);
			}
		}
	}
}