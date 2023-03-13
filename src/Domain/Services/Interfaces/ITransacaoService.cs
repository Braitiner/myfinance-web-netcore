using myfinance_web_netcore.Models;

namespace myfinance_web_netcore.Domain.Services.Interfaces
{
    public interface ITransacaoService
    {
        List<TransacaoModel> ListarRegistros();
        void Salvar(TransacaoModel model);
        TransacaoModel RetornarRegistro(int id);
        void Excluir(int id);
        RelatorioTransacoesModel PegarPorPeriodo(DateTime dataInicio, DateTime dataFim);
    }
}