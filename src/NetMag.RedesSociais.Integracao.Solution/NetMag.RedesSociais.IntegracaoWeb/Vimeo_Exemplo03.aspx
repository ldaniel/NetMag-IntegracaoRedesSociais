<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Vimeo_Exemplo03.aspx.cs" Inherits="NetMag.RedesSociais.IntegracaoWeb.Vimeo_Exemplo03" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        #thumbs
        {
            overflow: auto;
            height: 298px;
            width: 300px;
            border: 1px solid #E7E7DE;
            padding: 0;
            float: left;
        }
        #thumbs ul
        {
            list-style-type: none;
            margin: 0 10px 0;
            padding: 0 0 10px 0;
        }
        #thumbs ul li
        {
            height: 75px;
        }
        .thumb
        {
            border: 0;
            float: left;
            width: 100px;
            height: 75px;
            background: url(http://bitcast.vimeo.com/vimeo/thumbnails/defaults/default.75x100.jpg);
            margin-right: 10px;
        }
        #embed
        {
            background-color: #E7E7DE;
            height: 280px;
            width: 504px;
            float: left;
            padding: 10px;
        }
        #portrait
        {
            float: left;
            margin-right: 5px;
            max-width: 100px;
        }
        #stats
        {
            clear: both;
            margin-bottom: 20px;
        }
    </style>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
    <script>

        var apiEndpoint = 'http://vimeo.com/api/v2/';
        var oEmbedEndpoint = 'http://vimeo.com/api/oembed.json'
        var oEmbedCallback = 'switchVideo';
        var videosCallback = 'setupGallery';
        var vimeoUsername = 'user3637385';

        // Obtendo os vídeos do usuário
        $(document).ready(function () {
            $.getScript(apiEndpoint + vimeoUsername + '/videos.json?callback=' + videosCallback);
        });

        function getVideo(url) {
            $.getScript(oEmbedEndpoint + '?url=' + url 
                        + '&width=504&height=280&callback=' + oEmbedCallback);
        }

        function setupGallery(videos) {
            // Definindo um thumbnail do usuário
            $('#stats').prepend('<img id="portrait" src="' + videos[0].user_portrait_medium + '" />');
            $('#stats h1').text("Vídeos da galeria de " + videos[0].user_name);

            // Carregando o primeiro vídeo
            getVideo(videos[0].url);

            // Adicionando demais vídeos na galeria
            for (var i = 0; i < videos.length; i++) {
                var html = '<li><a href="' + videos[i].url + '"><img src="' 
                                + videos[i].thumbnail_medium + '" class="thumb" />';
                html += '<p>' + videos[i].title + '</p></a></li>';
                $('#thumbs ul').append(html);
            }

            // Alterando o thumbnail do vídeo quando clicado
            $('#thumbs a').click(function (event) {
                event.preventDefault();
                getVideo(this.href);
                return false;
            });
        }

        function switchVideo(video) {
            $('#embed').html(unescape(video.html));
        }		
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Vimeo - Exemplo 3</h2>
    <p>Carregando a galeria de vídeos de um usuário.</p>
	<div id="stats">
		<h1></h1>
		<div style="clear: both;"></div>
	</div>
	<div id="wrapper">
		<div id="embed"></div>
		<div id="thumbs">
			<ul></ul>
		</div>
	</div>
</asp:Content>
