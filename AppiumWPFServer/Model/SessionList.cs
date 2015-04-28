using System.Collections.Generic;
using AppiumWpfServer.Model;

namespace AppiumWpfServer.Model
{
    public class SessionList
    {
        public SessionList()
        {
            Sessions = new List<Session>();
        }

        public List<Session> Sessions { get; private set; }

        public int Count
        {
            get { return Sessions.Count; }
        }

        public Session CreateNewSession()
        {
            var session = new Session();
            Sessions.Add(session);
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