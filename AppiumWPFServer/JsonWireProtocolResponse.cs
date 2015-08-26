using System.Collections.Generic;

namespace AppiumWpfServer
{
	public class JsonWireProtocolResponse : Dictionary<string, object>
	{
		public JsonWireProtocolResponse()
		{
			status = 0;
		}

		private int status { get; set; }
	}
}