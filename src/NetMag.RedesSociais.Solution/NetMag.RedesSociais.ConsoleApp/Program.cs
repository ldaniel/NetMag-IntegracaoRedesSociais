using System;
using NetMag.RedesSociais.Core;

namespace NetMag.RedesSociais.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            const string usuario = "leandronet";
            var servico = new ServicoFacade();
            servico.Conectar();

            Console.Write("Informe o PIN: ");
            servico.DefinirPIN(Console.ReadLine());

            var amigos = servico.ListarAmigos(usuario);

            Console.WriteLine("\n\rAmigos de {0}", usuario);
            foreach (var item in amigos)
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }
    }
}