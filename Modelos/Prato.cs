namespace JogoGourmet.Models;

public class Prato
{
    public string Nome { get; private set; }

    public Prato(string nome)
    {
        Nome = nome;
    }
}
