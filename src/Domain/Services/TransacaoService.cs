using Microsoft.EntityFrameworkCore;
using myfinance_web_netcore.Domain.Entities;
using myfinance_web_netcore.Domain.Services.Interfaces;
using myfinance_web_netcore.Models;

namespace myfinance_web_netcore.Domain.Services
{
    public class TransacaoService : ITransacaoService
    {
        private readonly MyFinanceDbContext _dbContext;

        public TransacaoService(MyFinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<TransacaoModel> ListarRegistros()
        {
            var result = new List<TransacaoModel>();
            var dbSet = _dbContext.Transacao.Include(x=> x.PlanoConta);

            foreach (var item in dbSet)
            {
                var itemTransacao = new TransacaoModel()
                {
                    Id = item.Id,
                    Data = item.Data,
                    Historico = item.Historico,
                    Valor = item.Valor,
                    PlanoContaId = item.PlanoContaId,
                    ItemPlanoConta = new PlanoContaModel() {
                        Id = item.PlanoConta.Id,
                        Descricao = item.PlanoConta.Descricao,
                        Tipo = item.PlanoConta.Tipo}
                };
                result.Add(itemTransacao);
            }
            return result;
        }
        public void Salvar(TransacaoModel model)
        {
            var dbSet = _dbContext.Transacao;
            var entidade = new Transacao()
            {
                Id = model.Id,
                Data = model.Data,
                Historico = model.Historico,
                Valor = model.Valor,
                PlanoContaId = model.PlanoContaId
            };

            if (entidade.Id == null)
            {
                dbSet.Add(entidade);
            }
            else
            {
                dbSet.Attach(entidade);
                _dbContext.Entry(entidade).State = EntityState.Modified;
            }
            _dbContext.SaveChanges();
        }

        public TransacaoModel RetornarRegistro(int id)
        {
            var item = _dbContext.Transacao.Where(x => x.Id == id).First();
            var itemTransacao = new TransacaoModel()
            {
                Id = item.Id,
                Data = item.Data,
                Historico = item.Historico,
                Valor = item.Valor,
                PlanoContaId = item.PlanoContaId
            };
            return itemTransacao;
        }
        public void Excluir(int id)
        {
            var item = _dbContext.Transacao.Where(x => x.Id == id).First();

            _dbContext.Attach(item);
            _dbContext.Remove(item);
            _dbContext.SaveChanges();
        }

         public RelatorioTransacoesModel PegarPorPeriodo(DateTime dataInicial, DateTime dataFinal)
        {

            var dbSet = _dbContext.Transacao
                .Include(x => x.PlanoConta)
                .Where(x => x.Data >= dataInicial.Date && x.Data <= dataFinal.Date);

            List<TransacaoModel> ListaTransacao = new List<TransacaoModel>();

            foreach (var item in dbSet)
            {
                ListaTransacao.Add(new TransacaoModel
                {
                    Id = item.Id,
                    Data = item.Data,
                    Historico = item.Historico,
                    Valor = item.Valor,
                    ItemPlanoConta =
                        new PlanoContaModel
                        {
                            Id = item.PlanoConta.Id,
                            Descricao = item.PlanoConta.Descricao,
                            Tipo = item.PlanoConta.Tipo
                        },
                    PlanoContaId = (int)item.PlanoConta.Id,
                    PlanoContaTipo = item.PlanoConta.Tipo
                });
            }

            RelatorioTransacoesModel relatorio = new RelatorioTransacoesModel();
            relatorio.DataInicio = dataInicial;
            relatorio.DataFim = dataFinal;
            relatorio.Transacao = ListaTransacao;
            relatorio.TransacaoReceitas = ListaTransacao
                .Where(x => x.PlanoContaTipo.Equals("R"))
                .ToList()
                .Count();
            relatorio.TransacaoDespesas = ListaTransacao
                .Where(x => x.PlanoContaTipo.Equals("D"))
                .ToList()
                .Count();
            return relatorio;
        }
    }
}