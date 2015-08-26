using AppiumWpfServer;
using NServiceKit.ServiceHost;

namespace AppiumWpfServer.Endpoints
{
	[Route("/wd/hub/session/{SessionId}/window", "DELETE")]
	public class Window_DELETE : JsonWireProtocolRequest, IReturn<Window_DELETE.Response>
	{
		public string Name { get; set; }
		public string SessionId { get; set; }

		public class Response : JsonWireProtocolResponse
		{
			internal Response(Window_DELETE request)
			{
				var session = Server.Model.Sessions.GetSessionById(request.SessionId);
				session.CloseActiveWindow();

				Add("status", 0);
				Add("sessionId", session.ID);
			}
		}
	}
}