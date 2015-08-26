using System;
using System.Collections.Generic;
using System.Net;
using AppiumWpfServer.Endpoints;
using AppiumWpfServer.Model;
using Funq;
using NServiceKit.ServiceHost;
using NServiceKit.ServiceInterface;
using NServiceKit.Text;
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

			Logger.Info("Appium for WPF Desktop Server Started at {0}, listening on {1}", DateTime.Now, listeningOn);

			Console.ReadLine();
		}

		public class AppiumWPFService : Service
		{
			// POST /session/:sessionId/keys
			public object Post(Keys_POST request)
			{
				return new Keys_POST.Response(request);
			}

			// DELETE /session/{SessionId}/window
			public object Delete(Window_DELETE request)
			{
				return new Window_DELETE.Response(request);
			}

			// POST /session/{SessionId}/window
			public object Post(Window_POST request)
			{
				return new Window_POST.Response(request);
			}

			// GET /session/{SessionId}/elements
			public object Post(Elements_POST request)
			{
				return new Elements_POST.Response(request);
			}

			// GET /session/{SessionId}/element/{ID}/{Command}
			public object Get(Element_GET request)
			{
				return new Element_GET.Response(request);
			}

			// POST /session/{SessionId}/element/{ID}/{Command}
			public object Post(ElementAction_POST request)
			{
				return new ElementAction_POST.Response(request);
			}

			// POST /session/:sessionId/element
			public object Post(Element_POST request)
			{
				return new Element_POST.Response(request);
			}

			// GET /status
			public object Get(Status_GET request)
			{
				return new Status_GET.Response();
			}

			// GET /sessions
			public object Get(Sessions_GET request)
			{
				return new Sessions_GET.Response();
			}

			// POST session
			public object Post(Session_POST request)
			{
				return new Session_POST.Response(request);
			}

			// GET session/:sessionId
			public object Get(SessionId_GET request)
			{
				return new SessionId_GET.Response(request);
			}

			// POST session/:sessionId/appium/app/close
			public object Post(AppClose_POST request)
			{
				return new AppClose_POST.Response(request);
			}

			// POST session/:sessionId/appium/app/launch
			public object Post(AppLaunch_POST request)
			{
				return new AppLaunch_POST.Response(request);
			}

			public object Any(AllOfIt request)
			{
				return "404";
			}
		}

		[Route("/wd/hub/{Name*}")]
		public class AllOfIt : IReturn<object>
		{
			public string Name { get; set; }
		}

		public class AppHost : AppHostHttpListenerBase
		{
			public AppHost()
				: base("Appium for Windows Phone Desktop Server", typeof (AppiumWPFService).Assembly)
			{
			}

			protected override void ProcessRequest(HttpListenerContext context)
			{
				Logger.Info("Received: " + context.Request.HttpMethod + " " + context.Request.RawUrl);
				base.ProcessRequest(context);
			}

			public override void Configure(Container container)
			{
				// Force a response type of JSON.
				RequestFilters.Add(
					(IHttpRequest httpReq, IHttpResponse httpResp, object requestDto) =>
					{
						var sessionId = httpReq.ResponseContentType = "text/json";
					});

				ResponseFilters.Add((IHttpRequest request, IHttpResponse response, object dto) =>
				{
					var statusCode = dto as string;
					if (statusCode != null)
					{
						response.StatusCode = int.Parse(statusCode);
					}

					var dict = dto as Dictionary<string, object>;
					if (dict != null)
					{
						var val = dict["value"];
						var json = val as JsonObject;
						if (json != null)
						{
							Logger.Info(JsonSerializer.SerializeToString(json));
						}
						else
						{
							Logger.Info(val.ToString());
						}
					}
				});
			}
		}
	}
}