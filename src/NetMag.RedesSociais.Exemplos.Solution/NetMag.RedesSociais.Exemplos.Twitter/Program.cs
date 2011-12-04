using System;
using LinqToTwitter;
using TweetSharp.Model;
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.Fluent;

namespace NetMag.RedesSociais.Exemplos.Twitter
{
    class Program
    {
        private static OAuthToken _unauthorizedToken;
        private static OAuthToken _twitterOauth;
        private const string ConsumerKey = "gWfhuhzbuteDFTXTOi5HQ";
        private const string ConsumerSecret = "92dAzQQvuc51N1BXiJhD0h5sTg8kFZaC2BBSx8oXX8";

        static void Main()
        {
            ChamarExemplosLinqToTwitter();
            //ChamarExemplosTweetSharp();

            Console.ReadKey();
        }

        private static void ChamarExemplosTweetSharp()
        {
            var twitter = FluentTwitter
                .CreateRequest()
                .Configuration.UseHttps()
                .Authentication
                .GetRequestToken(ConsumerKey, ConsumerSecret);

            var response = twitter.Request();
            _unauthorizedToken = response.AsToken();

            var autorizationUrl = FluentTwitter
                .CreateRequest()
                .Authentication
                .GetAuthorizationUrl(_unauthorizedToken.Token);

            twitter = FluentTwitter
                .CreateRequest()
                .Authentication
                .AuthorizeDesktop(
                    ConsumerKey, ConsumerSecret,
                    _unauthorizedToken.Token);
            Console.Write("Digite o PIN e pressione <Enter>:");
            var pin = Console.ReadLine();

            twitter = FluentTwitter
                .CreateRequest()
                .Authentication
                .SetVerifier(pin);

            twitter = FluentTwitter
                .CreateRequest()
                .Authentication
                .GetAccessToken(ConsumerKey, ConsumerSecret,
                   _unauthorizedToken.Token, pin);

            var accessResponse = twitter.Request();
            _twitterOauth = accessResponse.AsToken();

            ExemplosTweetSharp.ObterTweetsPublicos(ConsumerKey, ConsumerSecret, _twitterOauth);
            ExemplosTweetSharp.ObterTweetsPublicosHome(ConsumerKey, ConsumerSecret, _twitterOauth);
            ExemplosTweetSharp.ObterMeusTweets(ConsumerKey, ConsumerSecret, _twitterOauth);
            ExemplosTweetSharp.ObterMinhasInformacoes(ConsumerKey, ConsumerSecret, _twitterOauth);
            ExemplosTweetSharp.CriarNovoTweet(ConsumerKey, ConsumerSecret, _twitterOauth);
            ExemplosTweetSharp.ObterListaSeguidores(ConsumerKey, ConsumerSecret, _twitterOauth);
            ExemplosTweetSharp.ObterListaAmigos(ConsumerKey, ConsumerSecret, _twitterOauth);
            ExemplosTweetSharp.ObterMencoes(ConsumerKey, ConsumerSecret, _twitterOauth);
            ExemplosTweetSharp.EnviandoUmaMensagemDireta(ConsumerKey, ConsumerSecret, _twitterOauth, "148784321");
            ExemplosTweetSharp.Retweetando(ConsumerKey, ConsumerSecret, _twitterOauth, "148784321");
            ExemplosTweetSharp.VerificarRelacionamento(ConsumerKey, ConsumerSecret, _twitterOauth, "dantovcorp", "leandronet");        
        }


        private static void ChamarExemplosLinqToTwitter()
        {
            // Autenticação básica.
            //ITwitterAuthorization auth = new UsernamePasswordAuthorization
            //                                 {
            //                                     UserName = usuario,
            //                                     Password = senha,
            //                                     UseCompression = true
            //                                 };

            var auth = new DesktopOAuthAuthorization
                       {
                           ConsumerKey = ConsumerKey,
                           ConsumerSecret = ConsumerSecret,
                           UseCompression = true
                       };

            using (var twitterCtx = new TwitterContext(auth, "http://twitter.com/", "http://search.twitter.com/"))
            {
                if (!auth.IsAuthorized)
                {
                    Console.Write("Digite o PIN e pressione <Enter> em seguida: ");
                    auth = (DesktopOAuthAuthorization) twitterCtx.AuthorizedClient;
                    auth.GetVerifier = () =>
                                           {
                                               return Console.ReadLine();
                                           };
                }

                auth.SignOn();

                //ExemplosLinqToTwitter.ObterTweetsPublicos(twitterCtx);
                //ExemplosLinqToTwitter.ObterTweetsPublicosHome(twitterCtx);
                //ExemplosLinqToTwitter.ObterMeusTweets(twitterCtx);
                //ExemplosLinqToTwitter.ObterMinhasInformacoes(twitterCtx);
                //ExemplosLinqToTwitter.CriarNovoTweet(twitterCtx);
                //ExemplosLinqToTwitter.ObterListaSeguidores(twitterCtx);
                //ExemplosLinqToTwitter.ObterListaAmigos(twitterCtx);
                ExemplosLinqToTwitter.ObterMencoes(twitterCtx);
                //ExemplosLinqToTwitter.EnviandoUmaMensagemDireta(twitterCtx, "148784321");
                //ExemplosLinqToTwitter.Retweetando(twitterCtx, "148784321");
                //ExemplosLinqToTwitter.VerificarRelacionamento(twitterCtx, "dantovcorp", "leandronet");

                auth.SignOff();
            }
        }
    }
}