using System.Collections.Generic;
using System.Linq;
using LinqToTwitter;
using NetMag.RedesSociais.Interfaces;

namespace NetMag.RedesSociais.Core.Servicos.Twitter
{
    internal class LinqToTwitterProxy : ITwitter
    {
        private DesktopOAuthAuthorization _auth;
        private TwitterContext _twitterCtx;

        public LinqToTwitterProxy()
        {
            
        }

        public void Conectar()
        {
            _auth = new DesktopOAuthAuthorization
            {
                ConsumerKey = Configuracao.GetConsumerKey(),
                ConsumerSecret = Configuracao.GetConsumerSecret(),
                UseCompression = true
            };

            _twitterCtx = new TwitterContext(_auth,
                                             Configuracao.GetTwitterBaseURL(),
                                             Configuracao.GetTwitterSearchURL());

            _auth = (DesktopOAuthAuthorization)_twitterCtx.AuthorizedClient;
        }

        public void DefinirPIN(string pin)
        {
            _auth.GetVerifier = () => { return pin; };
            _auth.SignOn();
        }

        public List<string> ListarAmigos(string usuario)
        {
            var amigos = new List<string>();

            var usuariosSelecionados = from users in _twitterCtx.User
                                       where 
                                            users.Type == UserType.Friends
                                            && users.ID == usuario
                                       select users;

            amigos.AddRange(usuariosSelecionados.Select(amigo => amigo.Name));

            return amigos;
        }

        ~LinqToTwitterProxy()
        {
            _auth.SignOff();
        }
    }
}