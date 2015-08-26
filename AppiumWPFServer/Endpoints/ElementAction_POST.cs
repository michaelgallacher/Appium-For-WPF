using System;
using System.Linq;
using AppiumWpfServer;
using NServiceKit.ServiceHost;

namespace AppiumWpfServer.Endpoints
{
	[Route("/wd/hub/session/{SessionId}/element/{Id}/{Command}", "POST")]
	public class ElementAction_POST : JsonWireProtocolRequest, IReturn<ElementAction_POST.Response>
	{
		public string SessionId { get; set; }
		public int Id { get; set; }
		public string Command { get; set; }
		// for 'elements' action
		public string Using { get; set; }
		public string Value { get; set; }

		public class Response : JsonWireProtocolResponse
		{
			internal Response(ElementAction_POST request)
			{
				var session = Server.Model.Sessions.GetSessionById(request.SessionId);
				var element = session.GetElement(request.Id);
				switch (request.Command)
				{
					case "click":
						element.Click();
						break;

					case "elements":
						var elements = session.GetElements(request.Using, request.Value);
						var val = elements.Select(e => new Tuple<string, object>("ELEMENT", e)).ToList();
						Add("value", val);
						break;


					default:
						throw new NotImplementedException();
				}

				Add("status", 0);
				Add("sessionId", session.ID);
			}
		}
	}
}