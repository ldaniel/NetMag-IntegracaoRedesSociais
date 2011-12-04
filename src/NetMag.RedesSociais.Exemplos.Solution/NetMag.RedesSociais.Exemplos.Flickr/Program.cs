using System;
using System.Diagnostics;
using FlickrNet;

class Program
{
    private const string ConsumerKey = "6e5bf53ab34e8ee90402177e3db7f463";
    private const string ConsumerSecret = "00a3cac682d2f54f";
    private static string _token;
    private const string NSID = "50605734@N02";
    private const string ScreenName = "dantovcorp";

    static void Main()
    {
        ChamarExemplosFlickrNet();

        Console.ReadLine();
    }

    private static void ChamarExemplosFlickrNet()
    {
        // Exemplos que não requerem Token
        var flickr = new Flickr(ConsumerKey, ConsumerSecret);

        ExemplosFlickrNet.ObterTotalDeImagens(flickr, ScreenName);
        ExemplosFlickrNet.ObterInformacoesFotos(flickr, ScreenName);
        ExemplosFlickrNet.ObterFotosFavoritas(flickr, ScreenName);

        // Exemplos que requerem Token
        string frob = flickr.AuthGetFrob();
        string url = flickr.AuthCalcUrl(frob, AuthLevel.Write);
        Process.Start(url);

        Console.ReadKey();

        Auth auth = flickr.AuthGetToken(frob);
        _token = auth.Token;

        flickr = new Flickr(ConsumerKey, ConsumerSecret, _token);

        const string arquivoFoto = @"C:\Users\Leandro Daniel\Pictures\fender.jpg";
        //ExemplosFlickrNet.FazerUploadFoto(flickr, arquivoFoto);
    }
}