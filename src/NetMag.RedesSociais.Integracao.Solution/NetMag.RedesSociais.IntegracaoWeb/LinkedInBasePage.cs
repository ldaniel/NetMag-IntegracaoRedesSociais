using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;

using DotNetOpenAuth.OAuth.ChannelElements;
using DotNetOpenAuth.OAuth.Messages;
using LinkedIn;

namespace NetMag.RedesSociais.IntegracaoWeb
{
    public class LinkedInBasePage : Page
    {
        private string AccessToken
        {
            get { return (string)Session["AccessToken"]; }
            set { Session["AccessToken"] = value; }
        }

        protected WebOAuthAuthorization Authorization
        {
            get;
            private set;
        }

        private InMemoryTokenManager TokenManager
        {
            get
            {
                var tokenManager = (InMemoryTokenManager)Application["TokenManager"];
                if (tokenManager == null)
                {
                    string consumerKey = ConfigurationManager.AppSettings["LinkedInConsumerKey"];
                    string consumerSecret = ConfigurationManager.AppSettings["LinkedInConsumerSecret"];
                    if (string.IsNullOrEmpty(consumerKey) == false)
                    {
                        tokenManager = new InMemoryTokenManager(consumerKey, consumerSecret);
                        Application["TokenManager"] = tokenManager;
                    }
                }

                return tokenManager;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Authorization = new WebOAuthAuthorization(this.TokenManager, this.AccessToken);

            if (!IsPostBack)
            {
                string accessToken = this.Authorization.CompleteAuthorize();
                if (accessToken != null)
                {
                    this.AccessToken = accessToken;

                    Response.Redirect(Request.Path);
                }

                if (AccessToken == null)
                {
                    this.Authorization.BeginAuthorize();
                }

            }

            base.OnLoad(e);
        }
    }

    public class InMemoryTokenManager : IConsumerTokenManager
    {
        private Dictionary<string, string> tokensAndSecrets = new Dictionary<string, string>();

        public InMemoryTokenManager(string consumerKey, string consumerSecret)
        {
            if (String.IsNullOrEmpty(consumerKey))
            {
                throw new ArgumentNullException("consumerKey");
            }

            this.ConsumerKey = consumerKey;
            this.ConsumerSecret = consumerSecret;
        }

        public string ConsumerKey { get; private set; }

        public string ConsumerSecret { get; private set; }

        #region ITokenManager Members
        public string GetTokenSecret(string token)
        {
            return this.tokensAndSecrets[token];
        }

        public void StoreNewRequestToken(UnauthorizedTokenRequest request, ITokenSecretContainingMessage response)
        {
            this.tokensAndSecrets[response.Token] = response.TokenSecret;
        }

        public void ExpireRequestTokenAndStoreNewAccessToken(string consumerKey, string requestToken, string accessToken, string accessTokenSecret)
        {
            this.tokensAndSecrets.Remove(requestToken);
            this.tokensAndSecrets[accessToken] = accessTokenSecret;
        }
        #endregion
        
        public TokenType GetTokenType(string token)
        {
            throw new NotImplementedException();
        }
    }
}