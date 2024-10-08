using JogoGourmet.Servicos;
using System.Windows;

namespace JogoGourmet.Ui;

public partial class MainWindow : Window
{
    private readonly IServicoJogo _servicoJogo;

    public MainWindow(IServicoJogo servicoJogo)
    {
        InitializeComponent();
        _servicoJogo = servicoJogo;
        _servicoJogo.IniciarJogo();
    }

    private void OkButton_Click(object sender, RoutedEventArgs e)
    {
        OkButton.Visibility = Visibility.Collapsed;
        SimButton.Visibility = Visibility.Visible;
        NaoButton.Visibility = Visibility.Visible;
        Label.Content = _servicoJogo.Pergunta();
    }

    private void SimButton_Click(object sender, RoutedEventArgs e)
    {
        if (_servicoJogo.BotaoSim())
        {
            Label.Content = _servicoJogo.Pergunta();
        }
        else 
        { 
            MessageBox.Show("Acertei de novo!");
            ReiniciarJogo();
        }
    }

    private void NaoButton_Click(object sender, RoutedEventArgs e)
    {
        if (_servicoJogo.BotaoNao())
        {
            Label.Content = _servicoJogo.Pergunta();
        }
        else 
        { 
            SimButton.Visibility = Visibility.Collapsed;
            NaoButton.Visibility = Visibility.Collapsed;
            InputTextBox.Visibility = Visibility.Visible;
            OkInputButton.Visibility = Visibility.Visible;
            CancelarInputButton.Visibility = Visibility.Visible;
            Label.Content = "Qual prato você pensou?";
        }
    }

    private void OkInputButton_Click(object sender, RoutedEventArgs e)
    {
        var novoPrato = InputTextBox.Text;
        if (string.IsNullOrEmpty(novoPrato))
        {
            MessageBox.Show("O nome do prato e categoria devem ser fornecido!");
            return;
        }

        if (_servicoJogo.BotaoOK(novoPrato))
        {
            Label.Content = _servicoJogo.Pergunta();
        }
        else
        {
            ReiniciarJogo();
        }
        InputTextBox.Text = string.Empty;
    }

    private void CancelarInputButton_Click(object sender, RoutedEventArgs e)
    {
        ReiniciarJogo();
    }

    private void ReiniciarJogo() 
    {
        OkInputButton.Visibility = Visibility.Collapsed;
        CancelarInputButton.Visibility = Visibility.Collapsed;
        InputTextBox.Visibility = Visibility.Collapsed;
        SimButton.Visibility = Visibility.Collapsed;
        NaoButton.Visibility = Visibility.Collapsed;

        Label.Content = "Pense em um prato que gosta";
        OkButton.Visibility = Visibility.Visible;

        _servicoJogo.ReiniciarJogo();
    }

}