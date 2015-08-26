using AppiumWpfServer;
using AppiumWpfServer.Model;
using NServiceKit.ServiceHost;

namespace AppiumWpfServer.Endpoints
{
	[Route("/wd/hub/session/{SessionId}/window", "POST")]
	public class Window_POST : JsonWireProtocolRequest, IReturn<Window_POST.Response>
	{
		public string Name { get; set; }
		public string SessionId { get; set; }

		public class Response : JsonWireProtocolResponse
		{
			internal Response(Window_POST request)
			{
				var session = Server.Model.Sessions.GetSessionById(request.SessionId);
				var status = session.SetActiveWindow(request.Name) ? 0 : (int) Errors.NoSuchWindow;

				Add("status", status);
				Add("sessionId", session.ID);
				//Add("value", val);
			}
		}
	}
}