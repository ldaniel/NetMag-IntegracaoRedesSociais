using System.Collections.Generic;

namespace NetMag.RedesSociais.Interfaces
{
    public interface ITwitter
    {
        void Conectar();
        void DefinirPIN(string pin);
        List<string> ListarAmigos(string usuario);
    }
}