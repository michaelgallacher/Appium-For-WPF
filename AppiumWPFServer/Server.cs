using System;
using System.Net;
using AppiumWpfServer.Endpoints;
using AppiumWpfServer.Model;
using Funq;
using NServiceKit.ServiceHost;
using NServiceKit.ServiceInterface;
using NServiceKit.WebHost.Endpoints;

namespace AppiumWpfServer
{
    internal class Server
    {
        internal static ServerModel Model;

        private static void Main(string[] args)
        {
            Model = new ServerModel();
            var listeningOn = args.Length == 0 ? "http://*:4776/" : args[0];
            var appHost = new AppHost();
            appHost.Init();
            appHost.Start(listeningOn);

            Console.WriteLine("Appium for WPF Desktop Server Started at {0}, listening on {1}",
                DateTime.Now, listeningOn);

            Console.ReadLine();
        }

        private class AppiumWPFService : Service
        {
            // GET /wd/hub/Status
            public object Get(Status_GET request)
            {
                return new Status_GET.Response();
            }

            // GET /wd/hub/sessions
            public object Get(Sessions_GET request)
            {
                return new Sessions_GET.Response();
            }

            // POST /wd/hub/session
            public object Post(Session_POST request)
            {
                return new Session_POST.Response(request);
            }

            // POST /wd/hub/session/:sessionId
            public object Get(SessionId_GET request)
            {
                return new SessionId_GET.Response(request);
            }
        }

        public class AppHost : AppHostHttpListenerBase
        {
            public AppHost()
                : base("Appium for Windows Phone Desktop Server", typeof (AppiumWPFService).Assembly)
            {
            }

            protected override void ProcessRequest(HttpListenerContext context)
            {
                base.ProcessRequest(context);
            }


            public override void Configure(Container container)
            {
            }
        }
    }
}