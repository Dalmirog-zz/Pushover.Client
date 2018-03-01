using System;

namespace Pushover.Client
{
    public class PushoverClient
    {
        public PushoverClient(string token, string user)
        {
            Token = token;
            User = user;
        }

        public string Token { get;}

        public string User { get;}

        public Response Push(PushoverMessage message)
        {
            return new Response();
        }
    }
}
