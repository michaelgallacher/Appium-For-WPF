using AppiumWpfServer;
using NServiceKit.ServiceHost;

namespace AppiumWpfServer.Endpoints
{
	[Route("/wd/hub/session/{SessionId}/keys", "POST")]
	public class Keys_POST : JsonWireProtocolRequest, IReturn<Keys_POST.Response>
	{
		public string Value { get; set; }
		public string SessionId { get; set; }

		public class Response : JsonWireProtocolResponse
		{
			internal Response(Keys_POST request)
			{
				var session = Server.Model.Sessions.GetSessionById(request.SessionId);
				session.SendKeys(request.Value);

				Add("status", 0);
				Add("sessionId", session.ID);
				//Add("value", val);
			}
		}
	}
}