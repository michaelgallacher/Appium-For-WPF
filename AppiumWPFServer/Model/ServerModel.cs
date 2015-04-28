namespace AppiumWpfServer.Model
{
    public class ServerModel
    {
        public ServerModel()
        {
            Sessions = new SessionList();
        }

        public SessionList Sessions { get; private set; }
    }
}