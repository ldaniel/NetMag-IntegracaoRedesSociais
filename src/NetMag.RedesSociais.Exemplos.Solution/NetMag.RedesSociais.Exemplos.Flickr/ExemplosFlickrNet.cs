using System;
using System.Collections.Generic;
using FlickrNet;

public static class ExemplosFlickrNet
{
    public static void ObterTotalDeImagens(Flickr flickr, string screenName)
    {
        FoundUser usuario = flickr.PeopleFindByUserName(screenName);

        var opcoes = new PhotoSearchOptions
        {
            UserId = usuario.UserId,
            PerPage = 100
        };

        PhotoCollection fotos = flickr.PhotosSearch(opcoes);
        Console.WriteLine("Total de fotos: {0}\n", fotos.Total);
    }

    public static void ObterInformacoesFotos(Flickr flickr, string screenName)
    {
        FoundUser usuario = flickr.PeopleFindByUserName(screenName);

        var opcoes = new PhotoSearchOptions
        {
            UserId = usuario.UserId,
            PerPage = 100
        };

        PhotoCollection fotos = flickr.PhotosSearch(opcoes);

        var todasAsFotos = new List<Photo>();
        todasAsFotos.AddRange(fotos);

        foreach (Photo foto in todasAsFotos)
        {
            Console.WriteLine("Foto {0} na URL {1}\n",
                foto.Title, foto.WebUrl);
        }
    }

    public static void ObterFotosFavoritas(Flickr flickr, string screenName)
    {
        FoundUser usuario = flickr.PeopleFindByUserName(screenName);

        PhotoCollection fotosPublicasFavoritas = 
            flickr.FavoritesGetPublicList(usuario.UserId);

        var todasAsFotos = new List<Photo>();
        todasAsFotos.AddRange(fotosPublicasFavoritas);

        foreach (Photo foto in todasAsFotos)
        {
            Console.WriteLine("Foto {0} na URL {1}\n",
                foto.Title, foto.WebUrl);
        }
    }

    public static void FazerUploadFoto(Flickr flickr, string arquivoImagem)
    {
        const bool uploadAsPublic = false;

        const string titulo = "Teste de upload";
        const string descricao = "Teste de Upload através do FlickrNet";
        const string tags = "guitarra, fender";

        string photoId = flickr.UploadPicture(
            arquivoImagem, titulo, descricao, tags,
            uploadAsPublic, false, false);

        Console.WriteLine("Upload realizado com sucesso!");
        Console.WriteLine("ID da foto: {0}", photoId);
    }
}
