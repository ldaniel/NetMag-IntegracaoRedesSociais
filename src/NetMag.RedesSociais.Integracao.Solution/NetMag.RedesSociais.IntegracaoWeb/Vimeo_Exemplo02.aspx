<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Vimeo_Exemplo02.aspx.cs" Inherits="NetMag.RedesSociais.IntegracaoWeb.Vimeo_Exemplo02" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        // Username no Vimeo
        var vimeoUserName = 'user3637385';

        // Variáveis que indicarão ao Vimeo qual a ação desejada
        var videoCallback = 'latestVideo';
        var oEmbedCallback = 'embedVideo';

        // URLs para conexão com o Vimeo
        var videosUrl = 'http://vimeo.com/api/v2/' + vimeoUserName
                + '/videos.json?callback=' + videoCallback;
        var oEmbedUrl = 'http://vimeo.com/api/oembed.json';

        // Coloca o vídeo na página
        function embedVideo(video) {
            videoEmbedCode = video.html;
            document.getElementById('embed').innerHTML = unescape(video.html);
        }

        // Utilizando o objeto oEmbed para obter o último vídeo do usuário
        function latestVideo(videos) {
            var videoUrl = videos[0].url;
            loadScript(oEmbedUrl + '?url=' + encodeURIComponent(videoUrl) + '&callback=' + oEmbedCallback);
        }

        // Carrega os dados do Vimeo
        function loadScript(url) {
            var js = document.createElement('script');
            js.setAttribute('type', 'text/javascript');
            js.setAttribute('src', url);
            document.getElementsByTagName('head').item(0).appendChild(js);
        }

        window.onload = loadScript(videosUrl);		
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Vimeo - Exemplo 2</h2>
    <p>Carregando o último vídeo de um usuário.</p>
    <div id="stats"></div>
    <div id="embed">Carregando vídeo do Vimeo. Aguarde...</div>
</asp:Content>
