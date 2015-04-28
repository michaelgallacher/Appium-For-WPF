using System;
using AppiumWpfServer.Model;
using NServiceKit.ServiceHost;

namespace AppiumWpfServer.Endpoints
{
    [Route("/wd/hub/session/{SessionId}")]
    public class SessionId_GET : JsonWireProtocolRequest
    {
        public string SessionId { get; set; }

        public class Response : JsonWireProtocolResponse
        {
            internal Response(SessionId_GET request)
            {
                Value = Server.Model.Sessions.GetSessionById(request.SessionId);
            }

            public Session Value { get; set; }
        }
    }
}