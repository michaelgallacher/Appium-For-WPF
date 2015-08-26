using NServiceKit.ServiceHost;

namespace AppiumWpfServer
{
	[Route("/wd/hub/Status", "GET")]
	public class Status_GET : IReturn<Status_GET.Response>
	{
		public class RepsonseValue
		{
			public BuildInfo Build { get; internal set; }
			public bool IsShuttingDown { get; internal set; }
		}

		public class BuildInfo
		{
			public string Version { get; internal set; }
			public string Revision { get; internal set; }
		}

		public class Response : JsonWireProtocolResponse
		{
			public Response()
			{
				Value = new RepsonseValue
				{
					Build = new BuildInfo
					{
						Version = "1.0.0",
						Revision = ""
					},
					IsShuttingDown = false
				};
			}

			public RepsonseValue Value { get; set; }
		}
	}
}