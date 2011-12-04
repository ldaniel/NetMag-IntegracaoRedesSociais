using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using NetMag.RedesSociais.Core;

namespace NetMag.RedesSociais.Notificador
{
    public partial class Main : Window
    {
        WindowState _lastWindowState;
        bool _shouldClose;
        protected ServicoFacade Servico;

        public Main()
        {
            InitializeComponent();
            Servico = new ServicoFacade();
            Servico.Conectar();
        }

        protected override void OnStateChanged(EventArgs e)
        {
            _lastWindowState = WindowState;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!_shouldClose)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void OnNotificationAreaIconDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Open();
            }
        }

        private void OnMenuItemOpenClick(object sender, EventArgs e)
        {
            Open();
        }

        private void Open()
        {
            Show();
            WindowState = _lastWindowState;
        }

        private void OnMenuItemExitClick(object sender, EventArgs e)
        {
            _shouldClose = true;
            Close();
        }

        private void DefinirPINCode_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PINCode.Text))
            {
                Servico.DefinirPIN(PINCode.Text);
                ListarAmigos.IsEnabled = true;
                listaAmigos.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Informe o PIN");
            }
        }

        private void ListarAmigos_Click(object sender, RoutedEventArgs e)
        {
            var amigos = Servico.ListarAmigos("leandronet");

            foreach (var item in amigos)
                listaAmigos.Items.Add(item);
        }

        private void buttomMinimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {  
            Close();            
        }

        private void ButtonClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _shouldClose = true;
            Close();
        }
    }
}