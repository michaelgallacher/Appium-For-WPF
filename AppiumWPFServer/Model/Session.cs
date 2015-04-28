using System;

namespace AppiumWpfServer.Model
{
    public class Session
    {
        public Session()
        {
            ID = Guid.NewGuid().ToString();
        }

        public string ID { get; set; }
    }
}