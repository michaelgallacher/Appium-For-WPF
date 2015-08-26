using System;

namespace AppiumWpfServer
{
	internal static class Logger
	{
		internal static void Info(string message)
		{
			var output = DateTime.Now.ToString("[yy-mm-dd hh:mm:ss.fff] ") + message;
			Console.WriteLine(output);
		}

		internal static void Info(string format, params object[] @params)
		{
			Info(string.Format(format, @params));
		}
	}
}