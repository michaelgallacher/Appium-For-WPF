using AppiumWpfServer.Model;
using NServiceKit.ServiceHost;

namespace AppiumWpfServer.Endpoints
{
    [Route("/wd/hub/sessions", "GET")]
    public class Sessions_GET : IReturn<Sessions_GET.Response>
    {
        public class Response : JsonWireProtocolResponse
        {
            internal Response()
            {
                Value = Server.Model.Sessions.ToArray();
            }

            public Session[] Value { get; set; }
        }
    }
}