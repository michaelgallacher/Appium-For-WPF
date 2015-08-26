using AppiumWpfServer;
using AppiumWpfServer.Model;
using NServiceKit.ServiceHost;

namespace AppiumWpfServer.Endpoints
{
	[Route("/wd/hub/session/{SessionId}/element/{ID}/{Command}", "GET")]
	public class Element_GET : JsonWireProtocolRequest, IReturn<Element_GET.Response>
	{
		public string SessionId { get; set; }
		public int ID { get; set; }
		public string Command { get; set; }

		public class Response : JsonWireProtocolResponse
		{
			internal Response(Element_GET request)
			{
				var session = Server.Model.Sessions.GetSessionById(request.SessionId);
				var element = session.GetElement(request.ID);
		
				var status = 0;
				switch (request.Command)
				{
					case "displayed":
						Add("value", element.Visible);
						break;

					case "text":
						Add("value", element.Name);
						break;

					default:
						status = (int) Errors.UnknownCommand;
						break;
				}

				Add("status", status);
				Add("sessionId", session.ID);
			}
		}
	}
}