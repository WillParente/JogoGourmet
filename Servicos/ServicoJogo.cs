using JogoGourmet.Modelos;
using JogoGourmet.Models;

namespace JogoGourmet.Servicos;

public class ServicoJogo : IServicoJogo
{
    private List<Categoria> _categorias;
    private Categoria? _categoriaAtual;
    private NivelJogoEnum _nívelJogoEnum;
    private string _prato;
    private string _novoPrato;

    public ServicoJogo()
    {
        _categorias = new List<Categoria>();
    }

    public void IniciarJogo()
    {
        AdicionarCategoria("massa", "Lasanha");
        _categoriaAtual = _categorias.First();
        _nívelJogoEnum = NivelJogoEnum.Categoria;
        _prato = _categoriaAtual.Nome;
        _novoPrato = string.Empty;
    }

    public void ReiniciarJogo()
    {
        _categoriaAtual = _categorias.First();
        _nívelJogoEnum = NivelJogoEnum.Categoria;
        _prato = _categoriaAtual.Nome;
        _novoPrato = string.Empty;
    }

    public NivelJogoEnum NivelJogo => _nívelJogoEnum;

    private Categoria? ObterSubCategoria() => _categorias.Where(c => c.Pai == _categoriaAtual).FirstOrDefault();

    public Categoria? ProximaCategoria()
    {
        int index = _categorias.IndexOf(_categoriaAtual);

        if (index == -1 || index >= _categorias.Count - 1)
            return null;

        return _categorias
            .Skip(index + 1)
            .FirstOrDefault(c => c.Pai == null);
    }

    private string ObterPratoPadrao() => new Prato("Bolo de Chocolate").Nome;

    public string Pergunta() => string.IsNullOrEmpty(_novoPrato) ? $"O prato que você pensou é {_prato}?" : $"{_novoPrato} é __________ mas {_prato} não.";
    
    public void AdicionarCategoria(string nomeCategoria, string nomeprato, Categoria? pai = null)
    {
        _categorias.Add(new Categoria(nome: nomeCategoria, prato: nomeprato, pai: pai));
    }

    public bool BotaoSim()
    {
        switch (_nívelJogoEnum)
        {
            case NivelJogoEnum.Categoria:
            case NivelJogoEnum.SubCategoria:
                var subCategoria = ObterSubCategoria();
                if (subCategoria != null)
                {
                    _nívelJogoEnum = NivelJogoEnum.SubCategoria;
                    _categoriaAtual = subCategoria;
                    _prato = _categoriaAtual.Nome;
                }
                else
                {
                    _nívelJogoEnum = NivelJogoEnum.Prato;
                    _prato = _categoriaAtual.Prato.Nome;
                }
                return true;
            default:
                return false;
        }
    }

    public bool BotaoNao()
    {
        switch (_nívelJogoEnum)
        {
            case NivelJogoEnum.Categoria:
                var proximaCategoria = ProximaCategoria();
                if (proximaCategoria == null)
                {
                    _nívelJogoEnum = NivelJogoEnum.Prato;
                    _prato = ObterPratoPadrao();
                    _categoriaAtual = null;
                }
                else
                {
                    _categoriaAtual = proximaCategoria;
                    _prato = _categoriaAtual.Nome;
                }
                return true;
            case NivelJogoEnum.SubCategoria:
                {
                    _nívelJogoEnum = NivelJogoEnum.Prato;
                    _prato = _categoriaAtual.Pai.Prato.Nome;
                }
                return true;
            default:
                return false;
        }
    }

    public bool BotaoOK(string input)
    {
        if (_nívelJogoEnum == NivelJogoEnum.Prato && string.IsNullOrEmpty(_novoPrato))
        {
            _novoPrato = input;
            return true;
        }

        AdicionarCategoria(nomeCategoria: input, nomeprato: _novoPrato, _categoriaAtual);
        return false;
    }
}
