using System.Collections.Generic;
using AppiumWpfServer;
using TestStack.White;

namespace AppiumWpfServer.Model
{
	public class SessionList
	{
		public SessionList()
		{
			Sessions = new List<Session>();
		}

		public List<Session> Sessions { get; private set; }

		private int Count
		{
			get { return Sessions.Count; }
		}

		public Session CreateNewSession(string appPath)
		{
			var session = Session.CreateNewSession(appPath);
			Sessions.Add(session);
			Logger.Info("Created new session {0} for {1}", session.ID, appPath);
			return session;
		}

		public void EndSession(string sessionId)
		{
			var session = GetSessionById(sessionId);
			if (null != session)
			{
				Sessions.Remove(session);
			}
		}

		public Session GetSessionById(string sessionId)
		{
			return Sessions.Find(x => x.ID == sessionId);
		}

		public Session[] ToArray()
		{
			return Sessions.ToArray();
		}
	}
}