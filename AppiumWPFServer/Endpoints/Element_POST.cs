using System.Collections.Generic;
using AppiumWpfServer;
using NServiceKit.ServiceHost;

namespace AppiumWpfServer.Endpoints
{
	[Route("/wd/hub/session/{SessionId}/element", "POST")]
	public class Element_POST : JsonWireProtocolRequest, IReturn<Element_POST.Response>
	{
		public string Using { get; set; }
		public string Value { get; set; }
		public string SessionId { get; set; }

		public class Response : JsonWireProtocolResponse
		{
			internal Response(Element_POST request)
			{
				var session = Server.Model.Sessions.GetSessionById(request.SessionId);
				var element = session.GetElement(request.Using, request.Value);

				Add("status", 0);
				Add("sessionId", session.ID);
				Add("value", new Dictionary<string, object> {{"ELEMENT", element}});
			}
		}
	}
}