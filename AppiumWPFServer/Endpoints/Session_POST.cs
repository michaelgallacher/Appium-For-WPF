using System.Windows.Automation;
using System.Diagnostics;
using AppiumWpfServer.Model;
using NServiceKit.ServiceHost;
using NServiceKit.Text;

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

                var p = Process.Start(request.DesiredCapabilities["app"]);
                Session = Server.Model.Sessions.CreateNewSession();
            }
            
            public Session Session { get; set; }
        }
    }
}