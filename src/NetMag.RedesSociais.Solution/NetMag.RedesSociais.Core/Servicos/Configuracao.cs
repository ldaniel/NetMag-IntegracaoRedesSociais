using System.Configuration;

namespace NetMag.RedesSociais.Core.Servicos
{
    public static class Configuracao
    {
        public static string GetConsumerKey()
        {
            return ConfigurationManager.AppSettings["ConsumerKey"];
        }

        public static string GetConsumerSecret ()
        {
            return ConfigurationManager.AppSettings["ConsumerSecret"];
        }

        #region LinqToTwitterProxy
        public static string GetTwitterBaseURL() 
        {
            return ConfigurationManager.AppSettings["TwitterBaseURL"];
        }
        
        public static string GetTwitterSearchURL() 
        {
            return ConfigurationManager.AppSettings["TwitterSearchURL"];
        }
        #endregion
    }
}