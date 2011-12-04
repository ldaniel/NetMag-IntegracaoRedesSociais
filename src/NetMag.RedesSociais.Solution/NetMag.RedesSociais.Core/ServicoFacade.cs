using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using NetMag.RedesSociais.Interfaces;

namespace NetMag.RedesSociais.Core
{
    public class ServicoFacade
    {
        [Dependency] 
        private ITwitter Twitter { get; set; }

        private readonly IUnityContainer _container;

        public ServicoFacade() 
        {
            _container = new UnityContainer()
                .LoadConfiguration("RedesSociaisContainer");
            Twitter = _container.Resolve<ITwitter>(); 
        }

        public void Conectar()
        {
            Twitter.Conectar();
        }
        
        public void DefinirPIN(string pin)
        {
            Twitter.DefinirPIN(pin);
        }

        public List<string > ListarAmigos(string usuario)
        {
            return Twitter.ListarAmigos(usuario);
        }
    }
}