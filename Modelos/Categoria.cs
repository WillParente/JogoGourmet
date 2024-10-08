using JogoGourmet.Models;

namespace JogoGourmet.Modelos;

public class Categoria
{
    public string Nome { get; private set; }
    public Categoria? Pai { get; private set; }
    public Prato Prato { get; private set; }

    public Categoria(string nome, string prato, Categoria? pai = null)
    {
        Nome = nome;
        Pai = pai;
        Prato = new Prato(prato);
    }
}