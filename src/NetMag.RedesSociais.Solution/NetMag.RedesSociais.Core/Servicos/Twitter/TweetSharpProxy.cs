using System.Collections.Generic;
using System.Linq;
using TweetSharp.Model;
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.Fluent;
using NetMag.RedesSociais.Interfaces;

namespace NetMag.RedesSociais.Core.Servicos.Twitter
{
    internal class TweetSharpProxy : ITwitter
    {
        private IFluentTwitter _twitter;
        private OAuthToken _unauthorizedToken;
        private OAuthToken _twitterOauth;
        private string _pin;
        
        public TweetSharpProxy()
        {
            // Vide Listagem X           
        }

        public void Conectar()
        {
            ObterTokenNaoAutorizado();
            ObterPIN();
        }

        private void ObterTokenAutorizado()
        {
            _twitter = FluentTwitter
                .CreateRequest()
                .Authentication
                .GetAccessToken(
                    Configuracao.GetConsumerKey(), 
                    Configuracao.GetConsumerSecret(),
                    _unauthorizedToken.Token, _pin);

            var accessResponse = _twitter.Request();
            _twitterOauth = accessResponse.AsToken();
        }

        private void ObterPIN()
        {
            var autorizationUrl = FluentTwitter
                .CreateRequest()
                .Authentication
                .GetAuthorizationUrl(_unauthorizedToken.Token);

            _twitter = FluentTwitter
                .CreateRequest()
                .Authentication
                .AuthorizeDesktop(
                    Configuracao.GetConsumerKey(), 
                    Configuracao.GetConsumerSecret(),
                    _unauthorizedToken.Token);            
        }

        public void DefinirPIN(string pin)
        {
            _pin = pin;
            _twitter = FluentTwitter
                .CreateRequest()
                .Authentication
                .SetVerifier(_pin);

            ObterTokenAutorizado();
        }

        private void ObterTokenNaoAutorizado()
        {   
            _twitter = FluentTwitter
                .CreateRequest()
                .Configuration.UseHttps()
                .Authentication
                .GetRequestToken(
                    Configuracao.GetConsumerKey(), 
                    Configuracao.GetConsumerSecret());

            var response = _twitter.Request();
            _unauthorizedToken = response.AsToken();
        }

        public List<string> ListarAmigos(string usuario)
        {
            var consulta = FluentTwitter
                    .CreateRequest()
                    .AuthenticateWith(
                        Configuracao.GetConsumerKey(),
                        Configuracao.GetConsumerSecret(),
                        _twitterOauth.Token,
                        _twitterOauth.TokenSecret)
                    .Users()
                    .GetFriends()
                    .AsXml();

            var resultados = consulta.Request();

            return resultados.AsUsers().Select(amigo => amigo.Name).ToList();
        }
    }
}