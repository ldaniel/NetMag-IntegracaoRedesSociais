<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Vimeo_Exemplo01.aspx.cs" Inherits="NetMag.RedesSociais.IntegracaoWeb.Vimeo_Exemplo01" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        // Vídeo desejado
        var videoUrl = 'http://www.vimeo.com/13050668';

        // Variável que indica ao Vimeo qual a ação desejada
        var callback = 'embedVideo';

        // URL para conexão com o Vimeo
        var endpoint = 'http://www.vimeo.com/api/oembed.json';
        var url = endpoint + '?url=' + encodeURIComponent(videoUrl) + '&callback=' + callback + '&width=640';

        // Coloca o vídeo na página
        function embedVideo(video) {
            document.getElementById('embed').innerHTML = unescape(video.html);
        }

        // Carrega os dados do Vimeo
        function init() {
            var js = document.createElement('script');
            js.setAttribute('type', 'text/javascript');
            js.setAttribute('src', url);
            document.getElementsByTagName('head').item(0).appendChild(js);
        }

        window.onload = init;
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Vimeo - Exemplo 1</h2>
    <p>Carregando um vídeo qualquer.</p>
	<div id="embed">Carregando vídeo do Vimeo. Aguarde...</div>
</asp:Content>