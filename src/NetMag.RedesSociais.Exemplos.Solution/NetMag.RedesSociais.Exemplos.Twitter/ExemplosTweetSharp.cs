using System;
using TweetSharp.Model;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;
using TweetSharp.Twitter.Extensions;

namespace NetMag.RedesSociais.Exemplos.Twitter
{
    public static class ExemplosTweetSharp
    {
        public static void ObterTweetsPublicos(string consumerKey, string consumerSecret, OAuthToken twitterOauth)
        {
            
        }

        public static void ObterTweetsPublicosHome(string consumerKey, string consumerSecret, OAuthToken twitterOauth)
        {
            
        }

        public static void ObterMeusTweets(string consumerKey, string consumerSecret, OAuthToken twitterOauth)
        {

        }

        public static void ObterMinhasInformacoes(string consumerKey, string consumerSecret, OAuthToken twitterOauth)
        {

        }

        public static void ObterListaSeguidores(string consumerKey, string consumerSecret, OAuthToken twitterOauth)
        {
            var consulta = FluentTwitter
                    .CreateRequest()
                    .AuthenticateWith(
                        consumerKey, consumerSecret,
                        twitterOauth.Token, twitterOauth.TokenSecret)
                    .Users()
                    .GetFollowers()
                    .AsXml();

            var resultados = consulta.Request();

            foreach (TwitterUser usuario in resultados.AsUsers())
            {
                Console.WriteLine(usuario.Name);
            }
        }

        public static void ObterListaAmigos(string consumerKey, string consumerSecret, OAuthToken twitterOauth)
        {
            var consulta = FluentTwitter
                    .CreateRequest()
                    .AuthenticateWith(
                        consumerKey, consumerSecret, 
                        twitterOauth.Token, twitterOauth.TokenSecret)
                    .Users()
                    .GetFriends()
                    .AsXml();

            var resultados = consulta.Request();

            foreach (TwitterUser usuario in resultados.AsUsers())
            {
                Console.WriteLine(usuario.Name);
            }
        }

        public static void CriarNovoTweet(string consumerKey, string consumerSecret, OAuthToken twitterOauth)
        {
            var request = FluentTwitter
                .CreateRequest()
                .AuthenticateWith(
                    consumerKey, consumerSecret,
                    twitterOauth.Token, twitterOauth.TokenSecret)
                .Statuses()
                .Update("Tweet criado com o #tweetsharp em " + DateTime.Now);

            var result = request.Request();
            Console.Write("Tweet criado com o ID {0}!", result.AsStatus().Id);
        }

        public static void ObterMencoes(string consumerKey, string consumerSecret, OAuthToken twitterOauth)
        {

        }

        public static void EnviandoUmaMensagemDireta(string consumerKey, string consumerSecret, OAuthToken twitterOauth, string id)
        {

        }

        public static void Retweetando(string consumerKey, string consumerSecret, OAuthToken twitterOauth, string tweetID)
        {

        }

        public static void VerificarRelacionamento(string consumerKey, string consumerSecret, OAuthToken twitterOauth, string amigo, string segue)
        {

        }
    }
}
