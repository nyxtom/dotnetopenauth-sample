using System;
using DotNetOpenAuth.AspNet.Clients;
using System.Configuration;
using DotNetOpenAuth.AspNet;

namespace OpenAuth.Axioms
{
    public static class AuthenticationHelper
    {
        public enum Networks
        {
            LinkedIn,
            Facebook,
            Twitter,
            Microsoft,
            Google,
            Yahoo,
            OpenId
        }

        /// <summary>
        /// Gets the client based on the given network.
        /// </summary>
        /// <returns>The client.</returns>
        /// <param name="network">Network.</param>
        public static IAuthenticationClient GetClient(Networks network)
        {
            switch (network)
            {
                case Networks.Facebook:
                    return GetFacebookClient();
                default:
                    return null;
            }
        }

        /// <summary>
        /// Gets the facebook client.
        /// </summary>
        /// <returns>The facebook client.</returns>
        public static FacebookClient GetFacebookClient()
        {
            string appId = ConfigurationManager.AppSettings["FACEBOOK_APP_ID"];
            string appSecret = ConfigurationManager.AppSettings["FACEBOOK_APP_SECRET"];

            return new FacebookClient(appId, appSecret);
        }
    }
}

