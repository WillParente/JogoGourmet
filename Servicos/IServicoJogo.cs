using JogoGourmet.Modelos;
using JogoGourmet.Models;

namespace JogoGourmet.Servicos;

public interface IServicoJogo
{
    void IniciarJogo();
    void ReiniciarJogo();
    string Pergunta();
    bool BotaoSim();
    bool BotaoNao();
    bool BotaoOK(string input);
}
